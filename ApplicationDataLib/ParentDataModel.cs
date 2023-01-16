namespace EMU.ApplicationData
{
    public class ParentDataModel
    {
        /// <summary>
        /// 车辆信息
        /// </summary>
        public RgvGlobalInfo RgvGlobalInfo
        {
            get
            {
                return RgvGlobalInfo.Instance;
            }
        }
    }
}
