namespace EMU.ApplicationData
{
    public class FowardDataModel : ParentDataModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public string Mode { get; set; }

        /// <summary>
        /// 车号
        /// </summary>
        public string Sn { get; set; }

        /// <summary>
        /// 车厢长度（每节车设置的长度）
        /// </summary>
        public int CarriageLength { get; set; }

        /// <summary>
        /// 拍照数量（每节车设置的拍照上限）
        /// </summary>
        public int ShotCount { get; set; }
    }
}
