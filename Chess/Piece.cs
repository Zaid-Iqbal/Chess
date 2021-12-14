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
        public String Name { get; set; }
        public Color Color { get;}
        public Image Icon { get; set; }
        public bool Locked { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public bool moved { get; set; }

        public Piece(String n, Color c, Image i, int X, int Y)
        {
            Name = n;
            Color = c;
            Icon = i;
            Locked = false;
            this.X = X;
            this.Y = Y;
            moved = false;
        }

        public Piece()
        {
            Name = "";
            Icon = null;
            Locked = false;
            X = -1;
            Y = -1;
            moved = false;
        }

        public String ToString()
        {
            return "Name: " + this.Name + "\nColor: " + this.Color.ToString() + "\nX: " + this.X + "\nY: " + this.Y + "\nLocked: " + Locked.ToString();
        }

        //public bool equals(Piece peice)
        //{
        //    if (this.name == name)
        //    {

        //    }
        //}

    }
}
