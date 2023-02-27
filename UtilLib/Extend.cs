using EMU.Parameter;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Ports;
using System.Text;

namespace EMU.Util
{
    public static class Extend
    {
        public static void AddLog(this string log, LogType type)
        {
            LogManager.AddLog(log, type);
        }
        public static byte[] HexStringToByteArray(this string cmd)
        {
            cmd = cmd.Replace(" ", "");
            byte[] buffer = new byte[cmd.Length / 2];
            for (int i = 0; i < cmd.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(cmd.Substring(i, 2), 16);
            return buffer;
        }
        public static bool Upload(this object obj)
        {
            return UploadDataManager.UploadData(obj);
        }
        public static bool SaveImage(this Image image, string path)
        {
            return ImageManager.SaveImage(image, path);
        }
        public static bool SaveImage(this byte[] data, string path)
        {
            return ImageManager.SaveImage(data, path);
        }
        /// <summary>
        /// 计算角度
        /// </summary>
        /// <param name="radian">弧度（±3.14）</param>
        public static double GetAngle(double radian)
        {
            if (radian >= 0)
            {
                return radian / Math.PI * 180;
            }
            else
            {
                return radian / Math.PI * 180 + 360;
            }
        }
        /// <summary>
        /// 计算弧度（±3.14）
        /// </summary>
        public static double GetRadian(double angle)
        {
            if (angle <= 180)
            {
                return angle * Math.PI / 180;
            }
            else
            {
                return (angle - 360) * Math.PI / 180;
            }
        }
        /// <summary>
        /// 获取串口16进制数据
        /// </summary>
        public static string GetHexData(this SerialPort serialPort)
        {
            byte[] buffer = new byte[serialPort.BytesToRead];
            int length = serialPort.Read(buffer, 0, buffer.Length);
            if (length > 0)
            {
                return Encoding.ASCII.GetString(buffer, 0, buffer.Length);
            }
            return string.Empty;
        }
        public static string ToJson(this object obj)
        {
            return JsonManager.ObjectToJson(obj);
        }
        public static string ToHexData(this string s, int length)
        {
            while (length - s.Length > 0)
            {
                s = "0" + s;
            }
            return s;
        }
        /// <summary>
        /// 根据弧度计算线段终点
        /// </summary>
        /// <param name="radian">弧度</param>
        /// <param name="radius">半径</param>
        public static Point GetRadianLineEnd(double radian, double radius, Point origin)
        {
            double db = Math.Sin(radian) * radius;
            double lb = Math.Cos(radian) * radius;
            return new Point((int)(origin.X + lb), (int)(origin.Y + db));
        }
        public static Bitmap PGM2BitMap(string pgmPath)
        {
            using (FileStream fs = new FileStream(pgmPath, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fs, Encoding.ASCII))
                {
                    if (reader.ReadChar() == 'P' && reader.ReadChar() == '5')
                    {
                        reader.ReadChar();
                        if (reader.PeekChar() == '#')
                        {
                            while (reader.PeekChar() != '\n')
                            {
                                reader.ReadChar();
                            }
                            reader.ReadChar();
                        }

                        StringBuilder sbTemp = new StringBuilder();
                        int width = ReadNumber(reader, sbTemp);
                        int height = ReadNumber(reader, sbTemp);
                        int level = ReadNumber(reader, sbTemp);
                        bool bTwo = level > 255;

                        Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
                        ColorPalette grayScale = bmp.Palette;
                        for (int i = 0; i < 256; i++)
                        {
                            grayScale.Entries[i] = Color.FromArgb(i, i, i);
                        }
                        bmp.Palette = grayScale;
                        BitmapData dt = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                        int offset = dt.Stride - dt.Width;

                        unsafe
                        {
                            byte* ptr = (byte*)dt.Scan0;
                            for (int i = 0; i < height; i++)
                            {
                                for (int j = 0; j < width; j++)
                                {
                                    byte v;
                                    if (bTwo)
                                    {
                                        v = (byte)(((double)((reader.ReadByte() << 8) + reader.ReadByte()) / level) * 255.0);
                                    }
                                    else
                                    {
                                        v = reader.ReadByte();
                                    }
                                    *ptr = v;
                                    ptr++;
                                }
                                ptr += offset;
                            }
                        }

                        bmp.UnlockBits(dt);
                        return bmp;
                    }
                    else
                    {
                        throw new Exception("无法转换地图文件");
                    }
                }
            }
        }
        public static Image ToImage(this string path)
        {
            return ImageManager.ReadImage(path);
        }
        public static Image ToImage(this byte[] data)
        {
            return ImageManager.BytesToImage(data);
        }
        public static T ToObject<T>(this string json)
        {
            return JsonManager.JsonToObject<T>(json);
        }
        /// <summary>
        /// 16进制字符串转ASCII码
        /// </summary>
        public static byte[] ToBytes(this string hex)
        {
            hex = hex.Replace(" ", "");
            byte[] buffer = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                buffer[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return buffer;
        }
        public static byte[] ToBytes(this Image image, bool isDispose = false)
        {
            return ImageManager.ImageToBytes(image, isDispose);
        }
        public static byte[] ToBytes2(this Image image, bool isDispose = false)
        {
            return ImageManager.Image2Byte(image, isDispose);
        }
        public static byte[] ToBytes(this SerialPort serialPort)
        {
            byte[] buffer = new byte[serialPort.BytesToRead];
            serialPort.Read(buffer, 0, buffer.Length);
            return buffer;
        }
        private static int ReadNumber(BinaryReader reader, StringBuilder sb)
        {
            char c = '\0';
            sb.Length = 0;
            while (Char.IsDigit(c = reader.ReadChar()))
            {
                sb.Append(c);
            }
            return int.Parse(sb.ToString());
        }
    }
}
