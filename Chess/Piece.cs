using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Piece
    {
        public String name { get; set; }

        public Color color { get;}
        public Image icon { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public bool moved { get; set; }

        public Piece(String n, Color c, Image i, int X, int Y)
        {
            name = n;
            color = c;
            icon = i;
            x = X;
            y = Y;
            moved = false;
        }

        public Piece()
        {
            name = "";
            icon = null;
            x = -1;
            y = -1;
            moved = false;
        }

        public String ToString()
        {
            return "Name: " + this.name + "\nColor: " + this.color.ToString() + "\nX: " + this.x + "\nY: " + this.y;
        }

        //public bool equals(Piece peice)
        //{
        //    if (this.name == name)
        //    {

        //    }
        //}

    }
}
