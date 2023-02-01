using System;
using System.Collections.Generic;
using System.Drawing;

namespace MapLib
{
    /// <summary>
    /// 区域（虚拟）地图类
    /// </summary>
    public class RangeMap
    {
        private int itemSize = 1;
        /// <summary>
        /// 单元区域大小
        /// </summary>
        public int ItemSize
        {
            set { itemSize = value; }
        }
        private Rectangle range;
        /// <summary>
        /// 区域地图大小范围
        /// </summary>
        public Rectangle Range
        {
            get
            {
                return range;
            }
        }
        private Dictionary<Point, RangeMapItem> itemDict;
        /// <summary>
        /// 区域地图单元字典缓存
        /// </summary>
        public Dictionary<Point, RangeMapItem> RangeItemDict
        {
            get
            {
                return itemDict;
            }
        }
        public RangeMap()
        {
            range = new Rectangle(0, 0, 0, 0);
            itemDict = new Dictionary<Point, RangeMapItem>();
        }
        public RangeMap(int width, int height)
        {
            range = new Rectangle(0, 0, width, height);
            itemDict = new Dictionary<Point, RangeMapItem>();
            CreateRangeDict();
        }
        public RangeMap(Map map)
        {
            Size size = GetMapSize(map);
            range = new Rectangle(0, 0, size.Width, size.Height);
            itemDict = new Dictionary<Point, RangeMapItem>();
            CreateRangeDict();
        }
        /// <summary>
        /// 创建新地图
        /// </summary>
        public void CreateNewMap(int width, int height)
        {
            range = new Rectangle(0, 0, width, height);
            itemDict.Clear();
            CreateRangeDict();
        }
        /// <summary>
        /// 创建新地图
        /// </summary>
        public void CreateNewMap(Map map)
        {
            Size size = GetMapSize(map);
            range = new Rectangle(0, 0, size.Width, size.Height);
            itemDict.Clear();
            CreateRangeDict(map);
        }
        /// <summary>
        /// 根据区域地图坐标获取真实地图坐标
        /// </summary>
        public Point GetMapPoint(Map map, Point point)
        {
            point = new Point(point.X * itemSize, point.Y * itemSize);
            point = new Point(point.X + map.StartPoint.X, point.Y + map.StartPoint.Y);
            int x, y;
            if (point.X > map.EndPoint.X)
            {
                x = map.EndPoint.X;
            }
            else
            {
                x = point.X;
            }
            if (point.Y > map.EndPoint.Y)
            {
                y = map.EndPoint.Y;
            }
            else
            {
                y = point.Y;
            }
            return new Point(x, y);
        }
        /// <summary>
        /// 根据区域地图坐标获取真实地图中心坐标
        /// </summary>
        public Point GetMapCenterPoint(Map map, Point point)
        {
            point = GetMapPoint(map, point);
            int l = itemSize / 2;
            point = new Point(point.X + l, point.Y + l);
            return point;
        }
        /// <summary>
        /// 根据真实地图坐标获取区域地图坐标
        /// </summary>
        public Point GetRangeMapPoint(Map map, Point point)
        {
            int xLength = point.X - map.StartPoint.X;
            int yLength = point.Y - map.StartPoint.Y;
            int x = xLength / itemSize + (xLength % itemSize > 0 ? 1 : 0);
            int y = yLength / itemSize + (yLength % itemSize > 0 ? 1 : 0);
            return new Point(x, y);
        }
        /// <summary>
        /// 刷新并更换地图
        /// </summary>
        public List<RangeMapItem> RefreshMap(int width, int height)
        {
            int addXCount = width - range.Width;
            int addYCount = height - range.Height;
            List<RangeMapItem> addList = new List<RangeMapItem>();
            for (int i = 0; i < Math.Abs(addXCount); i++)
            {
                if (addXCount > 0)
                {
                    for (int j = 0; j < range.Height; j++)
                    {
                        Point point = new Point(i + range.Width, j);
                        if (!itemDict.ContainsKey(point))
                        {
                            RangeMapItem item = new RangeMapItem() { NumId = j * range.Width + i, Point = point };
                            addList.Add(item);
                            itemDict.Add(point, item);
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < range.Height; j++)
                    {
                        Point point = new Point(range.Width - i - 1, j);
                        if (itemDict.ContainsKey(point))
                        {
                            itemDict.Remove(point);
                        }
                    }
                }
            }
            for (int j = 0; j < Math.Abs(addYCount); j++)
            {
                if (addYCount > 0)
                {
                    for (int i = 0; i < range.Width; i++)
                    {
                        Point point = new Point(i, j + range.Height);
                        if (!itemDict.ContainsKey(point))
                        {
                            RangeMapItem item = new RangeMapItem() { NumId = j * range.Width + i, Point = point };
                            addList.Add(item);
                            itemDict.Add(point, item);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < range.Width; i++)
                    {
                        Point point = new Point(i, range.Height - j - 1);
                        if (itemDict.ContainsKey(point))
                        {
                            itemDict.Remove(point);
                        }
                    }
                }
            }
            range = new Rectangle(0, 0, width, height);
            return addList;
        }
        /// <summary>
        /// 刷新并更换地图
        /// </summary>
        public void RefreshMap(Map map)
        {
            Size size = GetMapSize(map);
            List<RangeMapItem> list = RefreshMap(size.Width, size.Height);
            foreach (RangeMapItem item in list)
            {
                SetItemType(map, item);
            }
        }
        private void CreateRangeDict(Map map = null)
        {
            for (int i = 0; i < range.Width; i++)
            {
                for (int j = 0; j < range.Height; j++)
                {
                    Point point = new Point(i, j);
                    RangeMapItem item = new RangeMapItem() { NumId = j * range.Width + i, Point = point };
                    itemDict.Add(point, item);
                    if (map != null)
                    {
                        SetItemType(map, item);
                    }
                }
            }
        }
        private void SetItemType(Map map, RangeMapItem item)
        {
            Point start = GetMapPoint(map, item.Point);
            for (int x = start.X; x < itemSize; x++)
            {
                for (int y = start.Y; y < itemSize; y++)
                {
                    Point p = new Point(x, y);
                    if (map.MapUnitDict.ContainsKey(p))
                    {
                        item.ItemType = map.MapUnitDict[p].UnitType();
                        if (item.ItemType != ItemType.road && item.ItemType != ItemType.slope)
                        {
                            return;
                        }
                    }
                }
            }
        }
        private Size GetMapSize(Map map)
        {
            int width = map.MapSize.Width;
            int height = map.MapSize.Height;
            width += width % itemSize > 0 ? 1 : 0;
            height += height % itemSize > 0 ? 1 : 0;
            return new Size(width, height);
        }
    }
}
