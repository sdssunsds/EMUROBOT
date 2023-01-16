using EMU.Parameter;
using System.Collections.Generic;

namespace EMU.ApplicationData
{
    public class BackDataModel : ParentDataModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 总编号
        /// </summary>
        public string OnlyID { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public string Mode { get; set; }

        /// <summary>
        /// 车号
        /// </summary>
        public string Sn { get; set; }

        /// <summary>
        /// Rgv位置点
        /// </summary>
        public int RgvRunDistacnce { get; set; }

        /// <summary>
        /// 位置（车厢索引_转向架索引）
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 点位（z1或z2）
        /// </summary>
        public string Point { get; set; }

        /// <summary>
        /// 组编号
        /// </summary>
        public int GroupID { get; set; }

        /// <summary>
        /// 组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 相机类型编号
        /// </summary>
        public int CameraTypeId { get; set; }

        /// <summary>
        /// 3D算法编号
        /// </summary>
        public int Camera3d_Id { get; set; }

        /// <summary>
        /// 相机类型
        /// </summary>
        public CameraType CameraType
        {
            get { return (CameraType)CameraTypeId; }
            set { CameraTypeId = (int)value; }
        }

        /// <summary>
        /// 部件类型编号
        /// </summary>
        public string PartsTypeId { get; set; }

        /// <summary>
        /// 部件类型
        /// </summary>
        public PartsType PartsType
        {
            get { return PartsDict.GetType(PartsTypeId); }
            set { PartsTypeId = PartsDict.GetID(value); }
        }

        /// <summary>
        /// 机械臂名称
        /// </summary>
        public RobotName RobotName { get; set; }

        /// <summary>
        /// 机械臂坐标
        /// </summary>
        public RobotDataPack RobotLocation { get; set; }

        /// <summary>
        /// 当前位置是否拍照
        /// </summary>
        public bool CanSort { get; set; }
    }

    public class BackDataExtend
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// 总编号
        /// </summary>
        public string OnlyID { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public string Mode { get; private set; }

        /// <summary>
        /// 车号
        /// </summary>
        public string Sn { get; private set; }

        /// <summary>
        /// 组编号
        /// </summary>
        public int GroupID { get; private set; }

        /// <summary>
        /// 组名称
        /// </summary>
        public string GroupName { get; private set; }

        /// <summary>
        /// Rgv位置点
        /// </summary>
        public int RgvRunDistacnce { get; private set; }

        /// <summary>
        /// 位置（车厢索引_转向架索引）
        /// </summary>
        public string Location { get; private set; }

        /// <summary>
        /// 点位（z1或z2）
        /// </summary>
        public string Point { get; private set; }

        /// <summary>
        /// 相机类型编号
        /// </summary>
        public int CameraTypeId { get; private set; }

        /// <summary>
        /// 3D算法编号
        /// </summary>
        public int Camera3d_Id { get; private set; }

        /// <summary>
        /// 部件类型编号
        /// </summary>
        public string PartsTypeId { get; private set; }

        /// <summary>
        /// 机械臂名称
        /// </summary>
        public int RobotName { get; private set; }

        /// <summary>
        /// 当前位置是否拍照
        /// </summary>
        public bool CanSort { get; private set; }

        public string J1 { get; private set; }
        public string J2 { get; private set; }
        public string J3 { get; private set; }
        public string J4 { get; private set; }
        public string J5 { get; private set; }
        public string J6 { get; private set; }

        public BackDataExtend(BackDataModel model)
        {
            ID = model.ID;
            OnlyID = model.OnlyID;
            Mode = model.Mode;
            Sn = model.Sn;
            GroupID = model.GroupID;
            GroupName = model.GroupName;
            RgvRunDistacnce = model.RgvRunDistacnce;
            Location = model.Location;
            Point = model.Point;
            Camera3d_Id = model.Camera3d_Id;
            CameraTypeId = model.CameraTypeId;
            PartsTypeId = model.PartsTypeId;
            RobotName = (int)model.RobotName;
            CanSort = model.CanSort;
            J1 = model.RobotLocation.j1;
            J2 = model.RobotLocation.j2;
            J3 = model.RobotLocation.j3;
            J4 = model.RobotLocation.j4;
            J5 = model.RobotLocation.j5;
            J6 = model.RobotLocation.j6;
        }
    }

    public static class BackDataExtendFunc
    {
        public static List<BackDataExtend> ToExtend(this List<BackDataModel> list)
        {
            List<BackDataExtend> collection = new List<BackDataExtend>();
            foreach (BackDataModel item in list)
            {
                collection.Add(new BackDataExtend(item));
            }
            return collection;
        }
    }
}
