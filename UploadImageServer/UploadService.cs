#define puzzleC  // 使用C++拼图

using EMU.Parameter;
using EMU.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace UploadImageServer
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class UploadService : IService
    {
#if puzzleC
        public static Action<string, string, int> CompleteAction;
#endif
        public static Action<string, int> Addlog;
        public static Func<string, string, string, string, string, int, int> Location;

        private static Dictionary<string, List<int>> savePicArray = null;
        private static Dictionary<string, List<string>> savePicArray2 = null;
        private static Dictionary<string, Dictionary<int, byte[]>> picDataRom = null;
        private static Dictionary<string, Dictionary<int, byte[]>> picDataRom2 = null;
        private static Dictionary<string, Dictionary<int, byte[]>> picDataRom3 = null;
        private static Dictionary<string, Dictionary<RobotName, Dictionary<int, byte[]>>> picDataRoms = null;

        /// <summary>
        /// 每个待拼接图片的默认宽度
        /// </summary>
        public static int DefaultWidth { get; set; } = 4096;
        /// <summary>
        /// 每个待拼接图片的默认高度
        /// </summary>
        public static int DefaultHeight { get; set; } = 2260;
        /// <summary>
        /// 每次拼图的拼接数目（0代表压缩拼图方式）
        /// </summary>
        public static int PuzzleCount { get; set; } = 0;
        /// <summary>
        /// 上传路径
        /// </summary>
        public static string UploadPath { get { return Properties.Settings.Default.UploadPath; } }
        /// <summary>
        /// 拼图路径
        /// </summary>
        public static string PuzzlePath { get { return Properties.Settings.Default.PuzzlePath; } }
        /// <summary>
        /// 压缩后路径
        /// </summary>
        public static string ZipPath { get { return Properties.Settings.Default.ZipPath; } }
        /// <summary>
        /// 附加拼图程序配置文件位置
        /// </summary>
        public static string PuzzleExeConfig { get { return Properties.Settings.Default.PuzzleExeConfig; } }
        
        public static Action<Image, string, string, string> XzImage { get; set; }
        public static Action<string, Image, string, string, RobotName, string> MzImage { get; set; }
        public static Action<Image, string, string> LocImage { get; set; }
        public static Action<string, string, string, string, RobotName, string> _3dData { get; set; }

        public UploadService()
        {
            if (savePicArray == null)
            {
                savePicArray = new Dictionary<string, List<int>>();
            }
            if (savePicArray2 == null)
            {
                savePicArray2 = new Dictionary<string, List<string>>();
            }
            if (picDataRom == null)
            {
                picDataRom = new Dictionary<string, Dictionary<int, byte[]>>();
            }
            if (picDataRom2 == null)
            {
                picDataRom2 = new Dictionary<string, Dictionary<int, byte[]>>();
            }
            if (picDataRom3 == null)
            {
                picDataRom3 = new Dictionary<string, Dictionary<int, byte[]>>();
            }
            if (picDataRoms == null)
            {
                picDataRoms = new Dictionary<string, Dictionary<RobotName, Dictionary<int, byte[]>>>();
            }
        }

        public void AddLog(string log, int type = 6)
        {
            Addlog?.Invoke(log, type);
        }

        public int GetLocation(string id, string picName1, string picName2, string picName3, string robotID, int state)
        {
            return Location != null ? Location(id, picName1, picName2, picName3, robotID, state) : 0;
        }

        public void Upload3DData(string parsIndex, int robot, string data, string id, string robotID)
        {
            RobotName robotName = (RobotName)robot;
            string[] vs = GetModeSn(id);
            _3dData?.Invoke(parsIndex, data, vs[0], vs[1], robotName, robotID);
        }

        public void UploadComplete(string id, string robotID, int number)
        {
#if puzzleC
            Task.Run(() =>
            {
                CompleteAction?.Invoke(id, robotID, number);
            });
            if (savePicArray.ContainsKey(id))
            {
#else
                string[] ids = id.Split('_');
                if (PuzzleCount == 0)
                {
                    Directory.CreateDirectory(PuzzlePath + @"\" + id);
                    int remainder = savePicArray[id].Count % 8;
                    int length = savePicArray[id].Count / 8;
                    int index = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        if (i == 0)
                        {
                            length += remainder;
                        }
                        else
                        {
                            length += savePicArray[id].Count / 8;
                        }

                        #region 压缩拼图
                        int currentHeight = 0;
                        int height = 2260 * (i == 0 ? length : savePicArray[id].Count / 8);
                        using (Bitmap tableChartImage = new Bitmap(2048, height / 2))
                        {
                            using (Graphics graph = Graphics.FromImage(tableChartImage))
                            {
                                for (int j = index; j < length; j++)
                                {
                                    using (Image img = Image.FromFile(UploadPath + @"\" + id + @"\" + savePicArray[id][j] + FileManager.GetImageExtend()))
                                    {
                                        using (Bitmap tmp = new Bitmap(img, 2048, 1130))
                                        {
                                            graph.DrawImage(tmp, 0, currentHeight, tmp.Width, tmp.Height);
                                            currentHeight += 1130;
                                        }
                                    }
                                }
                            }
                            CompressImg(PuzzlePath + @"\" + id + @"\" + ids[0] + "_" + ids[1] + "_" + (i + 1) + "_" + robotID + FileManager.SaveImageExtend(), tableChartImage, 100);
                        }
                        #endregion
                        index = length;
                    }
                }
                else
                {
                    int count = PuzzleCount;
                    int length = savePicArray[id].Count / count + (savePicArray[id].Count % count > 0 ? 1 : 0);
                    for (int i = 0; i < length; i++)
                    {
                        int drawY = 0;
                        Bitmap bitmap = new Bitmap(DefaultWidth, DefaultHeight * count);
                        Graphics g = Graphics.FromImage(bitmap);
                        for (int j = 0; j < count; j++)
                        {
                            int index = i * count + j;
                            if (index < savePicArray[id].Count)
                            {
                                Image image = Image.FromFile(UploadPath + @"\" + id + @"\" + savePicArray[id][index] + FileManager.GetImageExtend());
                                g.DrawImage(image, 0, drawY, DefaultWidth, DefaultHeight);
                                image.Dispose();
                                drawY += DefaultHeight;
                            }
                        }
                        ImageToBytes(bitmap).SaveImage(PuzzlePath + @"\" + id + @"\" + ids[0] + "_" + ids[1] + "_" + (i + 1) + "_" + robotID + FileManager.SaveImageExtend());
                    }
                }
                ZipImage(id, robotID);
#endif
                savePicArray.Remove(id);
                savePicArray2.Remove(id);
                picDataRom.Clear();
                picDataRom2.Clear();
            }
        }

        public void UploadImage(int picIndex, int dataIndex, int dataLength, string id, byte[] imgData, string robotID)
        {
            if (!savePicArray.ContainsKey(id))
            {
                savePicArray.Add(id, new List<int>());
            }

            if (!savePicArray[id].Contains(picIndex))
            {
                savePicArray[id].Add(picIndex);
            }

            if (!picDataRom.ContainsKey(picIndex.ToString()))
            {
                picDataRom.Add(picIndex.ToString(), new Dictionary<int, byte[]>());
            }

            if (picDataRom[picIndex.ToString()].ContainsKey(dataIndex))
            {
                picDataRom[picIndex.ToString()][dataIndex] = imgData;
            }
            else
            {
                picDataRom[picIndex.ToString()].Add(dataIndex, imgData);
            }

            if (picDataRom[picIndex.ToString()].Count == dataLength)
            {
                string dir = UploadPath + @"\" + id + "_" + robotID;
                List<byte> bufferRom = new List<byte>();
                for (int i = 0; i < dataLength; i++)
                {
                    bufferRom.AddRange(picDataRom[picIndex.ToString()][i]);
                }
                bufferRom.ToArray().SaveImage(dir + @"\" + picIndex + FileManager.SaveImageExtend());
                picDataRom[picIndex.ToString()].Clear();
                picDataRom.Remove(picIndex.ToString());
                GC.Collect();
                savePicArray[id].Sort();
                string[] vs = id.Split('_');
#if !puzzleC
                using (StreamWriter sw = new StreamWriter(PuzzleExeConfig + "\\config.txt"))
                {
                    sw.WriteLine(vs[2] + "-" + vs[3] + "-" + vs[4] + " " + vs[5] + ":" + vs[6]);
                    sw.WriteLine(dir);
                    sw.WriteLine(vs[0] + "_" + vs[1]);
                }
#endif
                XzImage?.Invoke(bufferRom.ToArray().ToImage(), vs[0], vs[1], robotID);
            }
        }

        public void UploadImage2(string picIndex, int dataIndex, int dataLength, string id, byte[] imgData, string robotID)
        {
            if (!savePicArray2.ContainsKey(id))
            {
                savePicArray2.Add(id, new List<string>());
            }

            if (!savePicArray2[id].Contains(picIndex))
            {
                savePicArray2[id].Add(picIndex);
            }

            if (!picDataRom2.ContainsKey(picIndex))
            {
                picDataRom2.Add(picIndex, new Dictionary<int, byte[]>());
            }

            if (picDataRom2[picIndex].ContainsKey(dataIndex))
            {
                picDataRom2[picIndex][dataIndex] = imgData;
            }
            else
            {
                picDataRom2[picIndex].Add(dataIndex, imgData);
            }

            if (picDataRom2[picIndex].Count == dataLength)
            {
                try
                {
                    string dir = UploadPath + @"\" + id + "_" + robotID;
                    List<byte> bufferRom = new List<byte>();
                    for (int i = 0; i < dataLength; i++)
                    {
                        bufferRom.AddRange(picDataRom2[picIndex][i]);
                    }
                    bufferRom.ToArray().SaveImage(dir + @"\" + picIndex + FileManager.SaveImageExtend());
                    picDataRom2[picIndex.ToString()].Clear();
                    picDataRom2.Remove(picIndex.ToString());
                    GC.Collect();
                    savePicArray2[id].Sort();
                    string[] vs = id.Split('_');
                    XzImage?.Invoke(bufferRom.ToArray().ToImage(), vs[0], vs[1], robotID);
                }
                catch (Exception e)
                {
                    AddLog("执行保存线阵图片Error：" + e.Message);
                }
            }
        }

        public void UploadImage3(string picName, int dataIndex, int dataLength, string id, byte[] imgData, string robotID)
        {
            if (!picDataRom3.ContainsKey(picName))
            {
                picDataRom3.Add(picName, new Dictionary<int, byte[]>());
            }

            if (picDataRom3[picName].ContainsKey(dataIndex))
            {
                picDataRom3[picName][dataIndex] = imgData;
            }
            else
            {
                picDataRom3[picName].Add(dataIndex, imgData);
            }

            if (picDataRom3[picName].Count == dataLength)
            {
                try
                {
                    string dir = UploadPath + id + "_" + robotID;
                    List<byte> bufferRom = new List<byte>();
                    for (int i = 0; i < dataLength; i++)
                    {
                        bufferRom.AddRange(picDataRom3[picName][i]);
                    }
                    bufferRom.ToArray().SaveImage(dir + @"\" + picName);
                    picDataRom3[picName].Clear();
                    picDataRom3.Remove(picName);
                    GC.Collect();
                    LocImage?.Invoke(bufferRom.ToArray().ToImage(), picName, robotID);
                }
                catch (Exception e)
                {
                    AddLog("执行保存定位图片Error：" + e.Message);
                }
            }
        }

        public void UploadParameter(float[] kc, float[] kk, string robotID)
        {

        }

        public string UploadPictrue(string parsIndex, int robot, int dataIndex, int dataLength, string id, byte[] imgData, string robotID)
        {
            RobotName robotName = (RobotName)robot;
            if (!picDataRoms.ContainsKey(parsIndex))
            {
                picDataRoms.Add(parsIndex, new Dictionary<RobotName, Dictionary<int, byte[]>>());
            }

            if (!picDataRoms[parsIndex].ContainsKey(robotName))
            {
                picDataRoms[parsIndex].Add(robotName, new Dictionary<int, byte[]>());
            }

            if (!picDataRoms[parsIndex][robotName].ContainsKey(dataIndex))
            {
                picDataRoms[parsIndex][robotName].Add(dataIndex, null);
            }
            picDataRoms[parsIndex][robotName][dataIndex] = imgData;

            if (picDataRoms[parsIndex][robotName].Count == dataLength)
            {
                string dir = UploadPath + @"\" + id + "_" + robotID + @"\" + robotName.ToString();
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                List<byte> bufferRom = new List<byte>();
                for (int i = 0; i < dataLength; i++)
                {
                    bufferRom.AddRange(picDataRoms[parsIndex][robotName][i]);
                }
                string path = dir + @"\" + parsIndex + FileManager.SaveImageExtend();
                bufferRom.ToArray().SaveImage(path);
                picDataRoms[parsIndex][robotName].Clear();
                picDataRoms[parsIndex].Remove(robotName);
                if (picDataRoms[parsIndex].Count == 0)
                {
                    picDataRoms.Remove(parsIndex); 
                }
                GC.Collect();
                string[] vs = GetModeSn(id);
                MzImage?.Invoke(parsIndex, bufferRom.ToArray().ToImage(), vs[0], vs[1], robotName, robotID);
                return path;
            }
            return "";
        }

        /// <summary>
        /// 图片转byte数组
        /// </summary>
        private byte[] ImageToBytes(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                byte[] buffer = new byte[ms.Length];
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        /// <summary>
        /// 图片压缩
        /// </summary>
        private void ZipImage(string id, string robotID)
        {
            ZipManager.CreateZipFile(PuzzlePath + @"\" + id, ZipPath + @"\" + id + "_" + robotID + ".zip");
        }

        private void CompressImg(string path, Image img, int quality)
        {
            int i = 0;
        SaveImage:
            try
            {
                EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                ImageCodecInfo jpegCodec = null;
                ImageCodecInfo[] codes = ImageCodecInfo.GetImageEncoders();
                for (int j = 0; j < codes.Length; j++)
                {
                    if (codes[j].MimeType == "image/jpeg")
                    {
                        jpegCodec = codes[j];
                        break;
                    }
                }

                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = qualityParam;
                img.Save(path, jpegCodec, encoderParams);
            }
            catch (Exception)
            {
                if (i < 10)
                {
                    Thread.Sleep(1000);
                    i++;
                    goto SaveImage;
                }
            }
        }

        private string[] GetModeSn(string id)
        {
            string[] vs = id.Split('_');
            if (vs == null || vs.Length < 2)
            {
                vs = new string[2];
            }
            return vs;
        }
    }
}
