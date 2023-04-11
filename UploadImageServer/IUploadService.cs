using System.Runtime.Serialization;
using System.ServiceModel;

namespace UploadImageServer
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [FaultContract(typeof(FaultMessage))]
        void AddLog(string log, int type = 6);

        [OperationContract]
        [FaultContract(typeof(FaultMessage))]
        int GetLocation(string id, string picName1, string picName2, string picName3, string robotID, int state);

        [OperationContract]
        [FaultContract(typeof(FaultMessage))]
        void Upload3DData(string parsIndex, int robot, string data, string id, string robotID);

        [OperationContract]
        [FaultContract(typeof(FaultMessage))]
        void UploadImage(int picIndex, int dataIndex, int dataLength, string id, byte[] imgData, string robotID);

        [OperationContract]
        [FaultContract(typeof(FaultMessage))]
        void UploadImage2(string picIndex, int dataIndex, int dataLength, string id, byte[] imgData, string robotID);

        [OperationContract]
        [FaultContract(typeof(FaultMessage))]
        void UploadImage3(string picName, int dataIndex, int dataLength, string id, byte[] imgData, string robotID);

        [OperationContract]
        [FaultContract(typeof(FaultMessage))]
        void UploadComplete(string id, string robotID, int number);

        [OperationContract]
        [FaultContract(typeof(FaultMessage))]
        string UploadPictrue(string parsIndex, int robot, int dataIndex, int dataLength, string id, byte[] imgData, string robotID);

        [OperationContract]
        [FaultContract(typeof(FaultMessage))]
        void UploadParameter(float[] kc, float[] kk, string robotID);
    }

    [DataContract]
    public class FaultMessage
    {
        [DataMember] public string Message;
        [DataMember] public int ErrorCode;
    }
}
