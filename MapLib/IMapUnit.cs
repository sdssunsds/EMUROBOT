using System.Drawing;

namespace MapLib
{
    /// <summary>
    /// 地图单元
    /// </summary>
    public interface IMapUnit
    {
        /// <summary>
        /// 单元坐标
        /// </summary>
        Point Location { get; set; }
        /// <summary>
        /// 单元类别
        /// </summary>
        ItemType UnitType();
    }
}
