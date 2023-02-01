using System.Collections.Generic;
using System.Drawing;

namespace MapLib
{
    /// <summary>
    /// 现实地图类
    /// </summary>
    public class Map
    {
        private Size mapSize = new Size(0, 0);
        /// <summary>
        /// 地图0点坐标
        /// </summary>
        public Point StartPoint { get; set; }
        /// <summary>
        /// 地图尾点坐标
        /// </summary>
        public Point EndPoint { get; set; }
        /// <summary>
        /// 地图大小
        /// </summary>
        public Size MapSize
        {
            get
            {
                if (mapSize.Width == 0 || mapSize.Height == 0)
                {
                    mapSize = new Size(EndPoint.X - StartPoint.X, EndPoint.Y - StartPoint.Y);
                }
                return mapSize;
            }
        }
        /// <summary>
        /// 地图单元字典
        /// </summary>
        public Dictionary<Point, IMapUnit> MapUnitDict { get; set; }
        public Map()
        {
            MapUnitDict = new Dictionary<Point, IMapUnit>();
        }
        /// <summary>
        /// 设置地图单元
        /// </summary>
        public void SetMapUnit(IMapUnit mapUnit)
        {
            if (MapUnitDict.ContainsKey(mapUnit.Location))
            {
                MapUnitDict[mapUnit.Location] = mapUnit;
            }
            else
            {
                MapUnitDict.Add(mapUnit.Location, mapUnit);
            }
        }
        /// <summary>
        /// 设置地图单元
        /// </summary>
        public void SetMaoUnits(params IMapUnit[] mapUnits)
        {
            if (mapUnits != null)
            {
                foreach (IMapUnit item in mapUnits)
                {
                    SetMapUnit(item);
                }
            }
        }
    }
}
