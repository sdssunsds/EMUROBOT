using System;
using System.Drawing;

namespace MapLib
{
    public class RangeMapItem
    {
        private static Array slopeType = null;

        public int NumId { get; set; }
        public string Name { get; set; }
        public Point Point { get; set; }
        public ItemType ItemType { get; set; } = ItemType.road;
        public SlopeType SlopeType { get; set; }

        public static void CheckSlopeType(SlopeType slope, Action<SlopeType> action)
        {
            if (slopeType == null)
            {
                slopeType = Enum.GetValues(typeof(SlopeType));
            }
            foreach (int item in slopeType)
            {
                SlopeType type = (SlopeType)item & slope;
                action(type);
            }
        }
        public static void CheckSlopeType(RangeMapItem mapItem, Action<SlopeType> action)
        {
            CheckSlopeType(mapItem.SlopeType, action);
        }
    }

    public enum ItemType
    {
        /// <summary>
        /// 障碍
        /// </summary>
        obstacle,
        agv,
        /// <summary>
        /// 空气墙
        /// </summary>
        airWall,
        /// <summary>
        /// 可通行道路
        /// </summary>
        road,
        /// <summary>
        /// 斜坡
        /// </summary>
        slope
    }

    public enum SlopeType
    {
        top = 1,
        right = 2,
        bottom = 4,
        left = 8
    }
}
