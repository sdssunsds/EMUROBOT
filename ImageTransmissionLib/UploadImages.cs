//#define newService

using EMU.Parameter;
using EMU.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using UploadImageServer;

namespace EMU.ImageTransmission
{
    public class UploadImages : IService
    {
        private int uploadSize = 1024000;
        private string uploadID = "";
        private NewServiceReference.IService picService = null;
        private UploadImages()
        {
            picService = new NewServiceReference.ServiceClient();
        }
        private static UploadImages instance;
        public static UploadImages Instance
        {
            get
            {
                return instance ?? (instance = new UploadImages());
            }
        }

        public void AddLog(string log, int type = 6)
        {
            picService.AddLog(log, type);
        }

        public int GetLocation(string id, string picName1, string picName2, string picName3, string robotID, int state)
        {
            return picService.GetLocation(id, picName1, picName2, picName3, robotID, state);
        }

        public void UploadImage(Image image, int picIndex)
        {
            byte[] bytes = ImageManager.ImageToBytes(image);
            Task.Run(() =>
            {
                try
                {
                    int remainder = bytes.Length % uploadSize;
                    int length = bytes.Length / uploadSize + (remainder > 0 ? 1 : 0);
                    for (int i = 0; i < length; i++)
                    {
                        List<byte> rom = new List<byte>();
                        for (int j = 0; j < uploadSize; j++)
                        {
                            int index = i * uploadSize + j;
                            if (index < bytes.Length)
                            {
                                rom.Add(bytes[index]);
                            }
                            else
                            {
                                break;
                            }
                        }
                        UploadImage(picIndex, i, length, uploadID, rom.ToArray(), Parameter.Properties.Settings.Default.RobotID);
                    }
                }
                catch (Exception e)
                {
                    e.Message.AddLog(LogType.ErrorLog);
                }
            });
        }

        public void UploadImage(int picIndex, int dataIndex, int dataLength, string id, byte[] imgData, string robotID)
        {
            picService.UploadImage(picIndex, dataIndex, dataLength, id, imgData, robotID);
        }

        public void UploadImage2(string picIndex, int dataIndex, int dataLength, string id, byte[] imgData, string robotID)
        {
            picService.UploadImage2(picIndex, dataIndex, dataLength, id, imgData, robotID);
        }

        public void UploadImage3(string picName, int dataIndex, int dataLength, string id, byte[] imgData, string robotID)
        {
            picService.UploadImage3(picName, dataIndex, dataLength, id, imgData, robotID);
        }

        public void UploadComplete(string id, string robotID, int number)
        {
            try
            {
                picService.UploadComplete(id, robotID, number);
            }
            catch (Exception e)
            {
                e.Message.AddLog(LogType.ErrorLog);
            }
        }

        public void UploadParameter(float[] kc, float[] kk, string robotID)
        {
            picService.UploadParameter(kc, kk, robotID);
        }

        public void UploadComplete()
        {
            UploadComplete(uploadID, Parameter.Properties.Settings.Default.RobotID, 0);
        }

        public string UploadPictrue(string parsIndex, int robot, int dataIndex, int dataLength, string id, byte[] imgData, string robotID)
        {
#if newService
            return picService.UploadPictrue(parsIndex, robot, dataIndex, dataLength, id, imgData, robotID);
#else
            return "";
#endif
        }

        public void Upload3DData(string parsIndex, int robot, string data, string id, string robotID)
        {
#if newService
            picService.Upload3DData(parsIndex, robot, data, id, robotID);
#endif
        }
    }
}
