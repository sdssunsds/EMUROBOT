using EMU.Parameter;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace EMU.Util
{
    public class ImageManager
    {
        private static PixelFormat[] indexedPixelFormats =
        {
            PixelFormat.Undefined, PixelFormat.DontCare,
            PixelFormat.Format16bppArgb1555,
            PixelFormat.Format1bppIndexed,
            PixelFormat.Format4bppIndexed,
            PixelFormat.Format8bppIndexed
        };
        /// <summary>
        /// 读取图片
        /// </summary>
        public static Image ReadImage(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    return Image.FromFile(path);
                }
                catch (Exception e)
                {
                    e.Message.AddLog(LogType.ErrorLog);
                    try
                    {
                        using (FileStream stream = new FileStream(path, FileMode.Open))
                        {
                            return Image.FromStream(stream);
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.Message.AddLog(LogType.ErrorLog);
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        public static bool SaveImage(Image image, string path)
        {
            int i = 0;
        Save:
            try
            {
                FileManager.ExistsDirectory(path);
                image.Save(path);
                return true;
            }
            catch (Exception e)
            {
                e.Message.AddLog(LogType.ErrorLog);
                i++;
                if (i <= 5)
                {
                    goto Save;
                }
                return false;
            }
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        public static bool SaveImage(byte[] data, string path)
        {
            int i = 0;
        Save:
            try
            {
                FileManager.ExistsDirectory(path);
                File.WriteAllBytes(path, data);
                return true;
            }
            catch (Exception e)
            {
                e.Message.AddLog(LogType.ErrorLog);
                i++;
                if (i <= 5)
                {
                    goto Save;
                }
                return false;
            }
        }
        public static bool IsPixelFormatIndexed(PixelFormat format)
        {
            foreach (PixelFormat item in indexedPixelFormats)
            {
                if (item.Equals(format))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// byte数组转图片
        /// </summary>
        public static Image BytesToImage(byte[] buffer)
        {
            try
            {
                MemoryStream ms = new MemoryStream(buffer);
                Image image = Image.FromStream(ms);
                return image;
            }
            catch (Exception e)
            {
                e.Message.AddLog(LogType.ErrorLog);
                return null;
            }
        }
        /// <summary>
        /// 图片转数据流
        /// </summary>
        /// <param name="isDispose">是否清理转换后的图片</param>
        public static byte[] ImageToBytes(Image image, bool isDispose = false)
        {
            try
            {
                if (image == null) return null;
                using (Bitmap bitmap = new Bitmap(image))
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        bitmap.Save(stream, image.RawFormat);
                        return stream.GetBuffer();
                    }
                }
            }
            catch (Exception e)
            {
                e.Message.AddLog(LogType.ErrorLog);
                return null;
            }
            finally
            {
                if (image != null && isDispose)
                {
                    image.Dispose();
                    image = null;
                }
            }
        }
        /// <summary>
        /// 调整图片亮度
        /// </summary>
        /// <param name="b">图片</param>
        /// <param name="degree">亮度[-255, 255]</param>
        /// <returns>返回调整后的图片</returns>
        public static Bitmap KiLighten(Bitmap b, int degree)
        {
            if (b == null)
            {
                return null;
            }

            if (degree < -255) degree = -255;
            if (degree > 255) degree = 255;

            try
            {

                int width = b.Width;
                int height = b.Height;
                int pix = 0;
                BitmapData data = b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                unsafe
                {
                    byte* p = (byte*)data.Scan0;
                    int offset = data.Stride - width * 3;
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                pix = p[i] + degree;

                                if (degree < 0) p[i] = (byte)Math.Max(0, pix);
                                if (degree > 0) p[i] = (byte)Math.Min(255, pix);

                            }
                            p += 3;
                        }
                        p += offset;
                    }
                }
                b.UnlockBits(data);
                return b;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 截取图片
        /// </summary>
        /// <param name="image">原图片</param>
        public static Bitmap CaptureImage(Image image, int x, int y, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawImage(image, 0, 0, new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
            return bitmap;
        }
    }
}
