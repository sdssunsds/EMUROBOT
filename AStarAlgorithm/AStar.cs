using MapLib;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AStarAlgorithm
{
    public class AStar
    {
        private static AlgorithmType algorithmType = AlgorithmType.direction8;
        private static List<StarNode> openList = new List<StarNode>();
        private static List<StarNode> closeList = new List<StarNode>();
        private static Dictionary<Point, StarNode> nodeDict = new Dictionary<Point, StarNode>();
        public static event Action<RangeMap> TestMap;
        public static event Action<List<Point>> TestRun;
        public static List<Point> Find(Point start, Point end, AlgorithmType type, RangeMap map)
        {
            TestMap?.Invoke(map);
            algorithmType = type;
            List<Point> points = new List<Point>();
            openList.Clear();
            closeList.Clear();
            nodeDict.Clear();
            StarNode sn = new StarNode();
            sn.Point = start;
            StarNode en = new StarNode();
            en.Point = end;
            Point min = map.Range.Location;
            Point max = new Point(map.Range.Width - 1, map.Range.Height - 1);
            openList.Add(sn);
            while (!IsInList(en.Point.X, en.Point.Y, openList) || openList.Count == 0)
            {
                StarNode node = GetMinFFromList(openList);
                if (node != null)
                {
                    openList.Remove(node);
                    closeList.Add(node);
                    CheckAround(node, en, min, max, map);
                }
                else
                {
                    return points;
                }
            }
            StarNode tmpn = GetNodeFromList(en, openList);
            points.AddRange(GetPoints(tmpn, map));
            points.RemoveAt(0);
            points.Add(end);
            TestRun?.Invoke(points);
            return points;
        }
        private static void CheckAround(StarNode start, StarNode end, Point min, Point max, RangeMap map)
        {
            for (int i = start.Point.X - 1; i < start.Point.X + 2; i++)
            {
                for (int j = start.Point.Y - 1; j < start.Point.Y + 2; j++)
                {
                    if (i < min.X || i > max.X || j < min.Y || j > max.Y)
                    {
                        continue;
                    }
                    if (algorithmType == AlgorithmType.direction4)
                    {
                        if (Math.Abs(i - start.Point.X) == 1 && Math.Abs(j - start.Point.Y) == 1)
                        {
                            continue;
                        }
                    }
                    Point point = new Point(i, j);
                    StarNode starNode = new StarNode() { Point = point };
                    if (nodeDict.ContainsKey(point))
                    {
                        starNode = nodeDict[point];
                    }
                    else
                    {
                        nodeDict.Add(point, starNode);
                    }

                    if (map.RangeItemDict[point].ItemType != ItemType.road || CanLine(point, start, map.RangeItemDict[point]) || IsInList(i, j, closeList) || (i == start.Point.X && j == start.Point.Y))
                    {
                        continue;
                    }
                    starNode.CostH = GetNodeCostH(i, j, end);
                    if (!IsInList(i, j, openList))
                    {
                        starNode.Parent = start;
                        starNode.CostG = GetNodeCostG(i, j, start);
                        openList.Add(starNode);
                    }
                    else if (starNode.CostG > GetNodeCostG(i, j, start))
                    {
                        starNode.CostG = GetNodeCostG(i, j, start);
                        starNode.Parent = start;
                    }
                }
            }
        }
        private static bool CanLine(Point point, StarNode start, RangeMapItem item)
        {
            bool exp = true;
            if (item.ItemType != ItemType.slope)
            {
                return false;
            }
            RangeMapItem.CheckSlopeType(item, (SlopeType slope) =>
            {
                switch (slope)
                {
                    case SlopeType.top:
                        if (point.Y - start.Point.Y == 1 && point.X == start.Point.X)
                        {
                            exp = false;
                        }
                        break;
                    case SlopeType.right:
                        if (start.Point.X - point.X == 1 && point.Y == start.Point.Y)
                        {
                            exp = false;
                        }
                        break;
                    case SlopeType.bottom:
                        if (start.Point.Y - point.Y == 1 && point.X == start.Point.X)
                        {
                            exp = false;
                        }
                        break;
                    case SlopeType.left:
                        if (point.X - start.Point.X == 1 && point.Y == start.Point.Y)
                        {
                            exp = false;
                        }
                        break;
                }
            });
            return exp;
        }
        private static bool IsInList(int x, int y, List<StarNode> list)
        {
            foreach (StarNode item in list)
            {
                if (item.Point.X == x && item.Point.Y == y)
                {
                    return true;
                }
            }
            return false;
        }
        private static int GetNodeCostG(int x, int y, StarNode node)
        {
            if (node.Parent != null)
            {
                return node.Point.X == x || node.Point.Y == y ? node.Parent.CostH + 10 : node.Parent.CostH + 14;
            }
            else
            {
                return 0;
            }
        }
        private static int GetNodeCostH(int x, int y, StarNode node)
        {
            return Math.Abs(x - node.Point.X) + Math.Abs(y - node.Point.Y);
        }
        private static StarNode GetMinFFromList(List<StarNode> list)
        {
            if (list.Count == 0)
            {
                return null;
            }
            int tmpF = list[0].CostG + list[0].CostH;
            foreach (StarNode item in list)
            {
                if (item.CostG + item.CostH < tmpF)
                {
                    return item;
                }

            }
            return list[0];
        }
        private static StarNode GetNodeFromList(StarNode node, List<StarNode> list)
        {
            foreach (StarNode item in list)
            {
                if (item.Point.X == node.Point.X && item.Point.Y == node.Point.Y)
                {
                    return item;
                }
            }
            return null;
        }
        private static List<Point> GetPoints(StarNode node, RangeMap map)
        {
            List<Point> points = new List<Point>();
            if (node.Parent != null)
            {
                points.AddRange(GetPoints(node.Parent, map));
            }
            Point point = node.Point;
            points.Add(point);
            return points;
        }
    }
    public class StarNode
    {
        public int CostG { get; set; }
        public int CostH { get; set; }
        public Point Point { get; set; }
        public StarNode Parent { get; set; }
    }
}
