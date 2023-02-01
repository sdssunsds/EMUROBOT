using System.Drawing;

namespace MapLib
{
    public class Line
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public Line() { }
        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }
    }
}
