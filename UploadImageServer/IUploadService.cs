using System.Runtime.Serialization;
using System.ServiceModel;

namespace UploadImageServer
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [FaultContract(typeof(FaultMessage))]
        void UploadImage(int picIndex, int dataIndex, int dataLength, string id, byte[] imgData, string robotID);

        [OperationContract]
        [FaultContract(typeof(FaultMessage))]
        void UploadImage2(string picIndex, int dataIndex, int dataLength, string id, byte[] imgData, string robotID);

        [OperationContract]
        [FaultContract(typeof(FaultMessage))]
        void UploadComplete(string id, string robotID);

        [OperationContract]
        [FaultContract(typeof(FaultMessage))]
        string UploadPictrue(string parsIndex, int robot, int dataIndex, int dataLength, string id, byte[] imgData, string robotID);

        [OperationContract]
        [FaultContract(typeof(FaultMessage))]
        void Upload3DData(string parsIndex, int robot, string data, string id, string robotID);
    }

    [DataContract]
    public class FaultMessage
    {
        [DataMember] public string Message;
        [DataMember] public int ErrorCode;
    }
}
