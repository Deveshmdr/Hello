using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp1
{
    /// <summary>
    /// for polygon
    /// </summary>
    public class Polygon : Shape
    {
        /// <summary>
        /// def. construtor
        /// </summary>
        public Polygon()
        {

        }
        /// <summary>
        /// set and get values
        /// </summary>
        public PointF[] polygon_vertices { get; set; }

        /// <summary>
        /// methods called for polygon
        /// </summary>
        /// <param name="g"></param>
        /// <param name="c"></param>
        /// <param name="thickness"></param>
        public override void draw(Graphics g, Color c, int thickness)
        {
            Pen p = new Pen(Color.Green, thickness);
            g.DrawPolygon(p, polygon_vertices);
        }
    }
}
