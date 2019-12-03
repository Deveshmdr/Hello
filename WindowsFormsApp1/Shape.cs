using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp1
{
    /// <summary>
    /// abstract class
    /// base class for all shapes
    /// </summary>
    public abstract class Shape
    {
        //variable declaration and initialization
        protected int x = 0, y = 0, z = 0;


        /// <summary>
        /// parameterized constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Shape(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// default constructor
        /// </summary>
        public Shape()
        {
        }


        /// <summary>
        /// X setter
        /// </summary>
        /// <param name="x"></param>
        public void setX(int x)
        {
            this.x = x;
        }


        /// <summary>
        /// X getter
        /// </summary>
        /// <returns></returns>
        public int getX()
        {
            return x;
        }


        /// <summary>
        /// Y setter
        /// </summary>
        /// <param name="y"></param>
        public void setY(int y)
        {
            this.y = y;
        }



        /// <summary>
        /// Y getter
        /// </summary>
        /// <returns></returns>
        public int getY()
        {
            return y;
        }


        /// <summary>
        /// draw method
        /// </summary>
        /// <param name="g"></param>
        public abstract void draw(Graphics g, Color c, int thickness);
    }
}
