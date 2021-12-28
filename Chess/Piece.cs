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

        /// <summary>
        /// Piece is in check and cannot move if true
        /// </summary>
        public bool Locked { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public bool Moved { get; set; }
        public bool DoubleMoved { get; set; }

        public Piece(Piece piece)
        {
            Name = piece.Name;
            Color = piece.Color;
            Icon = piece.Icon;
            Locked = piece.Locked;
            X = piece.X;
            Y = piece.Y;
            Moved = piece.Moved;
            DoubleMoved = piece.DoubleMoved;
        }

        public Piece(String n, Color c, Image i, int X, int Y)
        {
            Name = n;
            Color = c;
            Icon = i;
            Locked = false;
            this.X = X;
            this.Y = Y;
            Moved = false;
            DoubleMoved = false;
        }

        public Piece()
        {
            Name = "";
            Icon = null;
            Locked = false;
            X = -1;
            Y = -1;
            Moved = false;
            DoubleMoved = false;
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
