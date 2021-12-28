using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Chess
{
    public partial class Form1 : Form
    {

        /// <summary>
        /// list of pieces
        /// </summary>
        static List<Piece> Pieces = new List<Piece>();


        /// <summary>
        /// checks if the game is over. If true, no more moves are accepted and you can stare at the board and marvel at how badly you suck at chess
        /// </summary>
        static bool gameEnd = false;

        /// <summary>
        /// checks if the game has started. if it hasn't no one can make a move
        /// </summary>
        static bool startGame = false;

        static int whiteCounter;
        static int blackCounter;

        bool timing = false;

        /// <summary>
        /// If the last move was a pawn, save its coords
        /// 2 = X
        /// 3 = Y
        /// </summary>
        String lastMove;

        /// <summary>
        /// turns true if en Passant is selected
        /// </summary>
        bool doubleMove = false;

        /// <summary>
        /// selected piece
        /// </summary>
        static Piece selected;

        /// <summary>
        /// 0 = checkmate for white
        /// 1 = checkmate for black
        /// </summary>
        static bool[] check = { false, false };
        static bool WhiteCheck = false;
        static bool BlackCheck = false;

        /// <summary>
        /// true = white's turn
        /// false = black's turn
        /// </summary>
        static bool turn = true;

        public Form1()
        {
            InitializeComponent();
        }

        //REMEMBER
        //X == COLUMNS
        //Y == ROWS

        private void Form1_Load(object sender, EventArgs e)
        {
            #region ADD PIECES TO DICTIONARY
            Pieces.Add(new Piece("pawn", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wpawn.png"), 0, 6));
            Pieces.Add(new Piece("pawn", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wpawn.png"), 1, 6));
            Pieces.Add(new Piece("pawn", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wpawn.png"), 2, 6));
            Pieces.Add(new Piece("pawn", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wpawn.png"), 3, 6));
            Pieces.Add(new Piece("pawn", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wpawn.png"), 4, 6));
            Pieces.Add(new Piece("pawn", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wpawn.png"), 5, 6));
            Pieces.Add(new Piece("pawn", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wpawn.png"), 6, 6));
            Pieces.Add(new Piece("pawn", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wpawn.png"), 7, 6));

            Pieces.Add(new Piece("pawn", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bpawn.png"), 0, 1));
            Pieces.Add(new Piece("pawn", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bpawn.png"), 1, 1));
            Pieces.Add(new Piece("pawn", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bpawn.png"), 2, 1));
            Pieces.Add(new Piece("pawn", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bpawn.png"), 3, 1));
            Pieces.Add(new Piece("pawn", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bpawn.png"), 4, 1));
            Pieces.Add(new Piece("pawn", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bpawn.png"), 5, 1));
            Pieces.Add(new Piece("pawn", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bpawn.png"), 6, 1));
            Pieces.Add(new Piece("pawn", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bpawn.png"), 7, 1));

            Pieces.Add(new Piece("rook", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wrook.png"), 0, 7));
            Pieces.Add(new Piece("rook", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wrook.png"), 7, 7));

            Pieces.Add(new Piece("rook", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Brook.png"), 0, 0));
            Pieces.Add(new Piece("rook", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Brook.png"), 7, 0));

            Pieces.Add(new Piece("horse", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Whorse.png"), 1, 7));
            Pieces.Add(new Piece("horse", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Whorse.png"), 6, 7));

            Pieces.Add(new Piece("horse", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bhorse.png"), 1, 0));
            Pieces.Add(new Piece("horse", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bhorse.png"), 6, 0));

            Pieces.Add(new Piece("bishop", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wbishop.png"), 2, 7));
            Pieces.Add(new Piece("bishop", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wbishop.png"), 5, 7));

            Pieces.Add(new Piece("bishop", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bbishop.png"), 2, 0));
            Pieces.Add(new Piece("bishop", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bbishop.png"), 5, 0));

            Pieces.Add(new Piece("queen", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wqueen.png"), 3, 7));
            Pieces.Add(new Piece("queen", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bqueen.png"), 3, 0));

            Pieces.Add(new Piece("king", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wking.png"), 4, 7));
            Pieces.Add(new Piece("king", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bking.png"), 4, 0));
            #endregion

            #region FORMAT COLUMNS
            //Format columns to accpet images properly and remove annoying null image
            foreach (DataGridViewImageColumn col in board.Columns)
            {
                col.DefaultCellStyle.NullValue = null;
                col.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }
            #endregion

            #region ADD ROWS
            //ADD ROWS
            for (int x = 0; x < 8; x++)
            {
                DataGridViewRow row = new DataGridViewRow();
                if (x == 0)
                {
                    row.HeaderCell.Value = "8";
                }
                else if (x == 1)
                {
                    row.HeaderCell.Value = "7";

                }
                else if (x == 2)
                {
                    row.HeaderCell.Value = "6";

                }
                else if (x == 3)
                {
                    row.HeaderCell.Value = "5";

                }
                else if (x == 4)
                {
                    row.HeaderCell.Value = "4";

                }
                else if (x == 5)
                {
                    row.HeaderCell.Value = "3";

                }
                else if (x == 6)
                {
                    row.HeaderCell.Value = "2";

                }
                else if (x == 7)
                {
                    row.HeaderCell.Value = "1";

                }
                row.Height = 50;
                board.Rows.Add(row);
            }
            #endregion

            #region ADD WHITE AND GREY SPACES
            ogColor();
            #endregion

            #region ADD PIECES
            //add black pawns
            foreach (Piece piece in Pieces)
            {
                board.Rows[piece.Y].Cells[piece.X].Value = piece.Icon;
            }
            #endregion

            turnLabel.Hide();
            turnLabelText.Hide();
            winnerLabel.Hide();
            checkLabel.Hide();
            drawLabel.Hide();
            drawButton.Hide();
            timeLabel.Hide();
            whiteTimeLabel.Hide();
            blackTimeLabel.Hide();
            resignButton.Hide();
            offerDrawButton.Hide();
            whiteMoveBox.Hide();
            blackMoveBox.Hide();
        }

        private void board_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!startGame)
            {
                return;
            }
            if (gameEnd)
            {
                return;
            }

            board.Refresh();

            //move a piece if the board is highlighted
            if (board.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == Color.Blue)
            {
                //if the piece is not in currently in check
                if ((selected.Color == Color.White && !check[0]) || (selected.Color == Color.Black && !check[1]))
                {
                    int checkd = 0; //See MoveToString(check) for what this number means
                                    //check for double move for en pessant
                    if (selected.Name == "pawn" && selected.Y - e.RowIndex == 2)
                    {
                        doubleMove = true;
                    }
                    board.Rows[selected.Y].Cells[selected.X].Value = null;    //remove the icon from the old position of the 

                    //Set the position of the piece to the new coordinates
                    selected.X = e.ColumnIndex;
                    selected.Y = e.RowIndex;

                    board.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = selected.Icon;     //Place the pieces icon at the new coordinates
                    selected.Moved = true;

                    ogColor();

                    //if there are threats to the king, that side is in check
                    if (getCheckTiles(turn ? Color.White : Color.Black)[0].Count != 0)
                    {
                        checkd = 1;
                        check[turn ? 1 : 0] = true;
                        checkLabel.Text = "In Check: " + (turn ? "Black" : "White");

                        //see if pieces can protect the king or the king can escape
                        bool movable = false;

                        //highlight all the pieces tiles from the enemy team
                        foreach (Piece protect in Pieces.Where(k => turn ? k.Color == Color.Black : k.Color == Color.White))
                        {
                            highlightSpaces(protect, true);
                        }

                        //iterate throught he board and see if there are blue spaces where the king can move
                        foreach (DataGridViewRow row in board.Rows)
                        {
                            foreach (DataGridViewImageCell cell in row.Cells)
                            {
                                if (cell.Style.BackColor == Color.Blue || cell.Style.BackColor == Color.Red)
                                {
                                    movable = true;
                                }
                            }
                        }

                        //if the king cant move anywhere or other pieces cant come to protect the king
                        if (!movable)
                        {
                            checkd = 2;
                            endGame(turn ? Color.Black : Color.White);
                        }
                        else
                        {
                            ogColor();
                        }
                    }

                    if (turn)
                    {
                        whiteMoveBox.AppendText("\r\n" + MoveToString(selected.Name, selected.X, selected.Y, false, checkd, 0, false));
                    }
                    else
                    {
                        blackMoveBox.AppendText("\r\n" + MoveToString(selected.Name, selected.X, selected.Y, false, checkd, 0, false));
                    }

                    changeTurn();
                }
                //else if the piece is in check
                else if ((selected.Color == Color.White && check[0]) || (selected.Color == Color.Black && check[1]))
                {
                    int checkd = 0; //See MoveToString(check) for what this number means
                                    //check for double move for en pessant
                    if (selected.Name == "pawn" && selected.Y - e.RowIndex == 2)
                    {
                        lastMove.Item1 = selected.X;
                        lastMove.Item2 = selected.Y;
                    }
                    board.Rows[selected.Y].Cells[selected.X].Value = null;    //remove the icon from the old position of the 

                    //Set the position of the piece to the new coordinates
                    selected.X = e.ColumnIndex;
                    selected.Y = e.RowIndex;

                    board.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = selected.Icon;     //Place the pieces icon at the new coordinates
                    selected.Moved = true;

                    ogColor();

                    //if there are threats to the other king, that side is in check
                    if (getCheckTiles(turn ? Color.White : Color.Black)[0].Count != 0)
                    {
                        checkd = 1;
                        check[turn ? 1 : 0] = true;
                        checkLabel.Text = "In Check: " + (turn ? "Black" : "White");

                        //see if pieces can protect the king or the king can escape
                        bool movable = false;
                        foreach (Piece protect in Pieces.Where(x => x.Color == (turn ? Color.Black : Color.White)))
                        {
                            highlightSpaces(protect, true);
                        }

                        //iterate throught he board and see if there are blue spaces where the king can move
                        foreach (DataGridViewRow row in board.Rows)
                        {
                            foreach (DataGridViewImageCell cell in row.Cells.Cast<DataGridViewImageCell>().Where(x => x.Style.BackColor == Color.Blue))
                            {
                                movable = true;
                                goto MovableFound;
                            }
                        }
                    MovableFound:

                        //if the king cant move anywhere or other pieces cant come to protect the king
                        if (!movable)
                        {
                            checkd = 2;
                            endGame(turn ? Color.Black : Color.White);
                        }
                        else
                        {
                            ogColor();
                        }
                    }

                    //Diff from first if statement
                    //if the current side is in check and managed to cover there king, they are no longer in check
                    else if (getCheckTiles(turn ? Color.White : Color.Black)[0].Count == 0 && check[turn ? 0 : 1] == true)
                    {
                        check[turn ? 0 : 1] = false;
                        checkLabel.Text = "In Check:";
                    }

                    if (turn)
                    {
                        whiteMoveBox.AppendText("\r\n" + MoveToString(selected.Name, selected.X, selected.Y, false, checkd, 0, false));
                    }
                    else
                    {
                        blackMoveBox.AppendText("\r\n" + MoveToString(selected.Name, selected.X, selected.Y, false, checkd, 0, false));
                    }

                    changeTurn();
                }
            }
            //kill a piece
            else if (board.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == Color.Red)
            {
                //Check to see moving this piece to kill will result in a check. Only follow through if the move won't lead to a check
                if (!checkForACheck(selected, e.ColumnIndex, e.RowIndex, true))
                {
                    //set last move values
                    lastMove.Item1 = selected.X;
                    lastMove.Item2 = selected.Y;
                    //remove the picture of the killed piece
                    board.Rows[selected.Y].Cells[selected.X].Value = null;

                    //remove killed piece
                    Pieces.Remove(Pieces.Find(k => k.X == e.ColumnIndex && k.Y == e.RowIndex));

                    //update the new position of the piece that did the kill
                    selected.X = e.ColumnIndex;
                    selected.Y = e.RowIndex;

                    //Pieces.Find(k => k.Name == selected.Name && k.Color == selected.Color && k.X == selected.X && k.Y == selected.Y)

                    //move the picture of the piece that killed
                    board.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = selected.Icon;

                    //update the value of the piece that just killed saying it has been selected
                    selected.Moved = true;

                    //if the piece is not in currently in check
                    if ((selected.Color == Color.White && !check[0]) || (selected.Color == Color.Black && !check[1]))
                    {
                        ogColor();
                        if (getCheckTiles(turn ? Color.White : Color.Black)[0].Count != 0)
                        {
                            if (turn)
                            {
                                whiteMoveBox.AppendText("\r\n" + MoveToString(selected.Name, selected.X, selected.Y, true, 1, 0, false));
                            }
                            else
                            {
                                blackMoveBox.AppendText("\r\n" + MoveToString(selected.Name, selected.X, selected.Y, true, 1, 0, false));
                            }
                            check[turn ? 1 : 0] = true;
                            checkLabel.Text = "In Check: " + (turn ? "Black" : "White");
                        }
                        if (turn)
                        {
                            whiteMoveBox.AppendText("\r\n" + MoveToString(selected.Name, selected.X, selected.Y, true, 0, 0, false));
                        }
                        else
                        {
                            blackMoveBox.AppendText("\r\n" + MoveToString(selected.Name, selected.X, selected.Y, true, 0, 0, false));
                        }
                        changeTurn();
                    } //else if the piece is in check
                    else if ((selected.Color == Color.White && check[0]) || (selected.Color == Color.Black && check[1]))
                    {
                        ogColor();
                        if (getCheckTiles(turn ? Color.White : Color.Black)[0].Count == 0)
                        {
                            if (turn)
                            {
                                whiteMoveBox.AppendText("\r\n" + MoveToString(selected.Name, selected.X, selected.Y, true, 0, 0, false));
                            }
                            else
                            {
                                blackMoveBox.AppendText("\r\n" + MoveToString(selected.Name, selected.X, selected.Y, true, 0, 0, false));
                            }
                            check[selected.Color == Color.White ? 0 : 1] = false;
                            checkLabel.Text = "In Check: ";
                            changeTurn();
                        }
                        else
                        {
                            if (turn)
                            {
                                whiteMoveBox.AppendText("\r\n" + MoveToString(selected.Name, selected.X, selected.Y, true, 2, 0, false));
                            }
                            else
                            {
                                blackMoveBox.AppendText("\r\n" + MoveToString(selected.Name, selected.X, selected.Y, true, 2, 0, false));
                            }
                            endGame(turn ? Color.Black : Color.White);
                        }
                    }
                }
            }
            //castling
            else if (board.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == Color.Green)
            {
                //find the rook
                Piece rook = Pieces.Find(k => k.X == e.ColumnIndex && k.Y == e.RowIndex);

                //find the king
                Piece king = Pieces.Find(k => k.Color == rook.Color && k.Name == "king");

                if (king.Color == Color.White)
                {
                    //Check to see what king of castle is beign done
                    bool kingSide = false;
                    if (king.X + 3 == rook.X)
                    {
                        kingSide = true;
                    }

                    //white kingside castle
                    if (kingSide)
                    {
                        if (rook.Color == Color.White)
                        {
                            board.Rows[king.Y].Cells[king.X].Value = null;
                            board.Rows[rook.Y].Cells[rook.X].Value = null;
                            board.Rows[king.Y].Cells[king.X + 2].Value = king.Icon;
                            board.Rows[rook.Y].Cells[rook.X - 2].Value = rook.Icon;
                            king.X += 2;
                            rook.X -= 2;
                            whiteMoveBox.AppendText("\r\n" + MoveToString("", -1, -1, false, 0, 1, false));
                        }
                        else
                        {
                            board.Rows[king.Y].Cells[king.X].Value = null;
                            board.Rows[rook.Y].Cells[rook.X].Value = null;
                            board.Rows[king.Y].Cells[king.X - 2].Value = king.Icon;
                            board.Rows[rook.Y].Cells[rook.X + 2].Value = rook.Icon;
                            king.X -= 2;
                            rook.X += 2;
                            blackMoveBox.AppendText("\r\n" + MoveToString("", -1, -1, false, 0, 1, false));
                        }
                    } //white queenside castle
                    else
                    {
                        if (rook.Color == Color.White)
                        {
                            board.Rows[king.Y].Cells[king.X].Value = null;
                            board.Rows[rook.Y].Cells[rook.X].Value = null;
                            board.Rows[king.Y].Cells[king.X - 2].Value = king.Icon;
                            board.Rows[rook.Y].Cells[rook.X + 3].Value = rook.Icon;
                            king.X -= 2;
                            rook.X += 3;
                            whiteMoveBox.AppendText("\r\n" + MoveToString("", -1, -1, false, 0, 2, false));

                        }
                        else
                        {
                            board.Rows[king.Y].Cells[king.X].Value = null;
                            board.Rows[rook.Y].Cells[rook.X].Value = null;
                            board.Rows[king.Y].Cells[king.X + 2].Value = king.Icon;
                            board.Rows[rook.Y].Cells[rook.X - 3].Value = rook.Icon;
                            king.X += 2;
                            rook.X -= 3;
                            blackMoveBox.AppendText("\r\n" + MoveToString("", -1, -1, false, 0, 2, false));
                        }
                    }
                }
                else
                {
                    //Check to see what king of castle is beign done
                    bool kingSide = false;
                    if (king.X - 3 == rook.X)
                    {
                        kingSide = true;
                    }

                    //black kingside castle
                    if (kingSide)
                    {
                        if (rook.Color == Color.White)
                        {
                            board.Rows[king.Y].Cells[king.X].Value = null;
                            board.Rows[rook.Y].Cells[rook.X].Value = null;
                            board.Rows[king.Y].Cells[king.X + 2].Value = king.Icon;
                            board.Rows[rook.Y].Cells[rook.X - 2].Value = rook.Icon;
                            king.X += 2;
                            rook.X -= 2;
                            whiteMoveBox.AppendText("\r\n" + MoveToString("", -1, -1, false, 0, 1, false));
                        }
                        else
                        {
                            board.Rows[rook.Y].Cells[rook.X].Value = null;
                            board.Rows[king.Y].Cells[king.X - 2].Value = king.Icon;
                            board.Rows[rook.Y].Cells[rook.X + 2].Value = rook.Icon;
                            king.X -= 2;
                            rook.X += 2;
                            blackMoveBox.AppendText("\r\n" + MoveToString("", -1, -1, false, 0, 1, false));
                        }
                    }
                    //black queenside castle
                    else
                    {
                        if (rook.Color == Color.White)
                        {
                            board.Rows[rook.Y].Cells[rook.X].Value = null;
                            board.Rows[king.Y].Cells[king.X + 2].Value = king.Icon;
                            board.Rows[rook.Y].Cells[rook.X - 3].Value = rook.Icon;
                            king.X += 2;
                            rook.X -= 3;
                            whiteMoveBox.AppendText("\r\n" + MoveToString("", -1, -1, false, 0, 2, false));

                        }
                        else
                        {
                            board.Rows[rook.Y].Cells[rook.X].Value = null;
                            board.Rows[king.Y].Cells[king.X - 2].Value = king.Icon;
                            board.Rows[rook.Y].Cells[rook.X + 3].Value = rook.Icon;
                            king.X -= 2;
                            rook.X += 3;
                            blackMoveBox.AppendText("\r\n" + MoveToString("", -1, -1, false, 0, 2, false));
                        }
                    }
                }

                ogColor();
                changeTurn();
            }
            //Promotion //TODO Add support for other piece options
            else if(board.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == Color.Purple)
            {
                Piece piece = Pieces.Find(k => k.X == selected.X && k.Y == selected.Y);
                if (preventCheck(piece).Name == "")
                {
                    if (turn && piece.Color == Color.White)
                    {
                        piece.Icon = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wqueen.png");
                        piece.Name = "queen";
                    }
                    else
                    {
                        piece.Icon = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bqueen.png");
                        piece.Name = "queen";
                    }

                    //If a piece is in the way and will be killed, just remove it from the pieces list. It will be removed from the board when the killer pic is written over the killed pic
                    try
                    {
                        Pieces.Remove(Pieces.Find(k => k.Color != piece.Color && k.X == e.ColumnIndex && k.Y == e.RowIndex));
                    }
                    catch (ArgumentNullException){}
                    
                    int checkd = 0;
                    board.Rows[piece.Y].Cells[piece.X].Value = null;
                    piece.X = e.ColumnIndex;
                    piece.Y = e.RowIndex;
                    board.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = piece.Icon;
                    piece.Moved = true;
                    ogColor();

                    //if there are threats to the king, that side is in check
                    if (getCheckTiles(turn ? Color.White : Color.Black)[0].Count != 0)
                    {
                        checkd = 1;
                        check[turn ? 1 : 0] = true;
                        checkLabel.Text = "In Check: " + (turn ? "Black" : "White");

                        //see if pieces can protect the king or the king can escape
                        bool movable = false;

                        //highlight all the spaces of the enemy team
                        highlightSpaces(Pieces.Find(k => k.Color == (turn ? Color.Black : Color.White)), true);

                        //iterate throught he board and see if there are blue spaces where the king can move
                        foreach (DataGridViewRow row in board.Rows)
                        {
                            foreach (DataGridViewImageCell cell in row.Cells)
                            {
                                if (cell.Style.BackColor == Color.Blue)
                                {
                                    movable = true;
                                }
                            }
                        }

                        //if the king cant move anywhere or other pieces cant come to protect the king
                        if (!movable)
                        {
                            checkd = 2;
                            endGame(turn ? Color.Black : Color.White);
                        }
                        else
                        {
                            ogColor();
                        }
                    }

                    if (turn)
                    {
                        whiteMoveBox.AppendText("\r\n" + MoveToString(piece.Name, piece.X, piece.Y, false, checkd, 0, true));
                    }
                    else
                    {
                        blackMoveBox.AppendText("\r\n" + MoveToString(piece.Name, piece.X, piece.Y, false, checkd, 0, true));
                    }

                    changeTurn();
                }
            }
            //Just an empty if to handle accidentally clicking on an empty cell
            else if (board.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
            {

            }
            //show possible moves
            else
            {
                //if player clicks on a piece that isn't theirs, end early
                if (Pieces.Any(k => k.X == e.ColumnIndex && k.Y == e.RowIndex && k.Color != (turn ? Color.White : Color.Black)))
                {
                    return;
                }
                ogColor();
                selected = null;

                //Get the selected piece and highlight it's spots
                Piece piece = Pieces.Find(k => k.Color == (turn ? Color.White : Color.Black) && k.X == e.ColumnIndex && k.Y == e.RowIndex);
                if (preventCheck(piece).Name == "")
                {
                    highlightSpaces(piece, check[(piece.Color == Color.White) ? 0 : 1]);
                    selected = piece;
                }
                else if (preventCheck(piece).Name != "")
                {
                    highlightSpaces(piece, false);
                    selected = piece;
                }
            }
        }

        /// <summary>
        /// return the tile while taking the flipboard changes into account
        /// </summary>
        /// <param name="turn">true is white turn, false is black turn</param>
        /// <param name="x">column</param>
        /// <param name="y">row</param>
        /// <returns></returns>
        public String getTile(int x, int y)
        {
            String row = "";
            
            foreach (DataGridViewRow findRow in board.Rows)
            {
                if (y == findRow.Index)
                {
                    row = findRow.HeaderCell.Value.ToString().ToLower();
                    break;
                }
            }

            String col = "";
            foreach (DataGridViewColumn column in board.Columns)
            {
                if (x == column.Index)
                {
                    col = column.HeaderText;
                    break;
                }
            }

            return col + row;
        }

        /// <summary>
        /// Checks to see if the color team can make a move. If not, there is a stalemate
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public bool checkStale(Color color)
        {
            DataTable table = GetMoves(color == Color.White ? Color.Black:Color.White);
            Piece king = Pieces.Where(k => k.Name == "king" && k.Color == color).First();

            if (table.Rows[king.X][king.Y - 1].ToString() == "E" ||
                table.Rows[king.X + 1][king.Y - 1].ToString() == "E" ||
                table.Rows[king.X + 1][king.Y].ToString() == "E" ||
                table.Rows[king.X + 1][king.Y + 1].ToString() == "E" ||
                table.Rows[king.X][king.Y + 1].ToString() == "E" ||
                table.Rows[king.X - 1][king.Y + 1].ToString() == "E" ||
                table.Rows[king.X - 1][king.Y].ToString() == "E" ||
                table.Rows[king.X][king.Y - 1].ToString() == "E")
            {
                return false;
            }
            return true;

            //TODO replace this approach with a datatable that contains the possible moves
            ////highlight all the spaces of the color
            //foreach (Piece piece in Pieces)
            //{
            //    if (piece.Color == color)
            //    {
            //        highlightSpaces(piece, false);
            //    }
            //}
            //foreach (DataGridViewRow row in board.Rows)
            //{
            //    foreach (DataGridViewCell cell in row.Cells)
            //    {
            //        if (cell.Style.BackColor == Color.Blue || cell.Style.BackColor == Color.Red || cell.Style.BackColor == Color.Green || cell.Style.BackColor == Color.Purple)
            //        {
            //            ogColor();
            //            return false;
            //        }
            //    }
            //}
            ogColor();
            return true;
        }

        /// <summary>
        /// Get the Moves of the entire team
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public DataTable GetMoves(Color color)
        {
            DataTable table = new DataTable();

            foreach (String letter in new[] { "a", "b", "c", "d", "e", "f", "g", "h" })
            {
                DataColumn dtColumn = new DataColumn();
                dtColumn.DataType = typeof(String);
                dtColumn.ColumnName = letter;
                table.Columns.Add(dtColumn);
            }

            for (int i = 0; i < 8; i++)
            {
                table.Rows.Add(table.NewRow());
            }

            foreach (Piece piece in Pieces.Where(k => k.Color == color))
            {
                int x = piece.X;
                int y = piece.Y;
                table.Rows[y][x] = color == Color.White ? "W" : "B";

                //not in check
                if ((!check[0] && piece.Color == Color.White) || (!check[1] && piece.Color == Color.Black))
                {
                    //piece is not preventing a check
                    if (!piece.Locked)
                    {
                        //bottom side pawn
                        if (piece.Name == "pawn" && ((piece.Color == Color.White && turn) || (piece.Color == Color.Black && !turn)))// bottom side 
                        {
                            //left kill
                            if (x - 1 != -1 && y - 1 != -1 && board.Rows[y - 1].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y - 1) && !piece.Locked)
                            {
                                table.Rows[y - 1][x - 1] = "K";
                            }

                            //right kill
                            if (x + 1 != 8 && y - 1 != -1 && board.Rows[y - 1].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y - 1) && !piece.Locked)
                            {
                                table.Rows[y - 1][x + 1] = "K";
                            }

                            Piece LeftEnemy = Pieces.FirstOrDefault(k => k.X == x - 1 && k.Y == y);
                            Piece RightEnemy = Pieces.FirstOrDefault(k => k.X == x + 1 && k.Y == y);

                            //left en Passant
                            if (x - 1 > -1 && y - 1 > -1 && LeftEnemy != null && board.Rows[y - 1].Cells[x - 1].Value == null && LeftEnemy.Color != color && !piece.Locked && piece.DoubleMoved)
                            {
                                table.Rows[y - 1][x - 1] = "M";
                                table.Rows[y][x - 1] = "K";
                            }

                            //right en Passant
                            if (x + 1 < 8 && y - 1 > -1 && LeftEnemy != null && board.Rows[y - 1].Cells[x + 1].Value == null && LeftEnemy.Color != color && !piece.Locked && piece.DoubleMoved)
                            {
                                table.Rows[y - 1][x + 1] = "M";
                                table.Rows[y][x + 1] = "K";
                            }

                            //move 
                            if (y - 1 > -1 && board.Rows[y - 1].Cells[x].Value == null && !piece.Locked)
                            {
                                table.Rows[y - 1][x] = "M";
                            }

                            //double move
                            if (y == 6 && board.Rows[y - 2].Cells[x].Value == null && !piece.Locked)
                            {
                                table.Rows[y - 2][x + 1] = "M";
                            }

                            //promotion
                            if (y - 1 == 0 && board.Rows[y - 1].Cells[x].Value == null && !piece.Locked)
                            {
                                table.Rows[y - 1][x] = "P";
                            }

                            //promotion & kill right
                            if (y - 1 == 0 && x + 1 < 8 && board.Rows[y - 1].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y - 1) && !piece.Locked)
                            {
                                table.Rows[y - 1][x + 1] = "P";
                            }

                            //promotion & kill right
                            if (y - 1 == 0 && x - 1 > -1 && board.Rows[y - 1].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y - 1) && !piece.Locked)
                            {
                                table.Rows[y - 1][x - 1] = "P";
                            }
                        }
                        //top side pawn
                        else if (piece.Name == "pawn" && ((piece.Color == Color.White && !turn) || (piece.Color == Color.Black && turn)))
                        {
                            //left kill
                            if (x + 1 < 8 && y + 1 < 8 && board.Rows[y + 1].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y + 1) && !piece.Locked)
                            {
                                table.Rows[y + 1][x + 1] = "K";

                            }

                            //right kill
                            if (x - 1 > -1 && y + 1 < 8 && board.Rows[y + 1].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y + 1) && !piece.Locked)
                            {
                                table.Rows[y + 1][x - 1] = "K";
                            }

                            //single move
                            if (y + 1 < 8 && board.Rows[y + 1].Cells[x].Value == null && !piece.Locked)
                            {
                                table.Rows[y + 1][x] = "M";
                            }

                            //double move
                            if (y == 1 && y + 2 < 8 && board.Rows[y + 2].Cells[x].Value == null && !piece.Locked)
                            {
                                table.Rows[y + 2][x] = "M";
                            }

                            Piece LeftEnemy = Pieces.FirstOrDefault(k => k.X == x + 1 && k.Y == y);
                            Piece RightEnemy = Pieces.FirstOrDefault(k => k.X == x - 1 && k.Y == y);

                            //left en Passant
                            if (x + 1 < 8 && y - 1 > -1 && LeftEnemy != null && board.Rows[y - 1].Cells[x + 1].Value == null && LeftEnemy.Color != color && !piece.Locked && piece.DoubleMoved)
                            {
                                table.Rows[y - 1][x + 1] = "M";
                                table.Rows[y][x + 1] = "K";
                            }

                            //right en Passant
                            if (x - 1 > -1 && y - 1 > -1 && LeftEnemy != null && board.Rows[y - 1].Cells[x - 1].Value == null && LeftEnemy.Color != color && !piece.Locked && piece.DoubleMoved)
                            {
                                table.Rows[y - 1][x - 1] = "M";
                                table.Rows[y][x - 1] = "K";
                            }

                            //promotions
                            if (y + 1 < 8 && board.Rows[y + 1].Cells[x].Value == null && !piece.Locked)
                            {
                                table.Rows[y + 1][x] = "P";
                            }
                            if (y + 1 < 8 && x - 1 > -1 && board.Rows[y + 1].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y + 1) && !piece.Locked)
                            {
                                table.Rows[y + 1][x - 1] = "P";
                            }
                            if (y + 1 < 8 && x + 1 < 8 && board.Rows[y + 1].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y + 1) && !piece.Locked)
                            {
                                table.Rows[y + 1][x + 1] = "P";
                            }
                        }
                        else if (piece.Name == "rook")
                        {
                            //above (i represents y/row)
                            for (int i = y - 1; i > -1; i--)
                            {
                                if (board.Rows[i].Cells[x].Value == null && !piece.Locked)
                                {
                                    table.Rows[i][x] = "M";
                                }
                                else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i) && !piece.Locked)
                                {
                                    table.Rows[i][x] = "K";
                                    break;
                                }
                                else if (board.Rows[i].Cells[x].Value != null)
                                {
                                    break;
                                }
                            }

                            //below (i represents y/row)
                            for (int i = y + 1; i < 8; i++)
                            {
                                if (board.Rows[i].Cells[x].Value == null && !piece.Locked)
                                {
                                    table.Rows[i][x] = "M";
                                }
                                else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i) && !piece.Locked)
                                {
                                    table.Rows[i][x] = "K";
                                    break;
                                }
                                else if (board.Rows[i].Cells[x].Value != null)
                                {
                                    break;
                                }
                            }

                            //left (i represents x/column)
                            for (int i = x - 1; i > -1; i--)
                            {
                                if (board.Rows[y].Cells[i].Value == null && !piece.Locked)
                                {
                                    table.Rows[y][i] = "M";
                                }
                                else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y) && !piece.Locked)
                                {
                                    table.Rows[y][i] = "K";
                                    break;
                                }
                                else if (board.Rows[y].Cells[i].Value != null)
                                {
                                    break;
                                }
                            }

                            //right (i represents x/column)
                            for (int i = x + 1; i < 8; i++)
                            {
                                if (board.Rows[y].Cells[i].Value == null && !piece.Locked)
                                {
                                    table.Rows[y][i] = "M";
                                }
                                else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y) && !piece.Locked)
                                {
                                    table.Rows[y][i] = "K";
                                    break;
                                }
                                else if (board.Rows[y].Cells[i].Value != null)
                                {
                                    break;
                                }
                            }
                        }
                        else if (piece.Name == "horse")
                        {
                            //up left
                            if (y - 2 > -1 && x - 1 > -1 && board.Rows[y - 2].Cells[x - 1].Value == null && !piece.Locked)
                            {
                                table.Rows[y - 2][x - 1] = "M";
                            }
                            else if (y - 2 > -1 && x - 1 > -1 && board.Rows[y - 2].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y - 2) && !piece.Locked)
                            {
                                table.Rows[y - 2][x - 1] = "K";
                            }

                            //up right
                            if (y - 2 > -1 && x + 1 < 8 && board.Rows[y - 2].Cells[x + 1].Value == null && !piece.Locked)
                            {
                                table.Rows[y - 2][x + 1] = "M";
                            }
                            else if (y - 2 > -1 && x + 1 < 8 && board.Rows[y - 2].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y - 2) && !piece.Locked)
                            {
                                table.Rows[y - 2][x + 1] = "K";
                            }

                            //right up
                            if (y - 1 > -1 && x + 2 < 8 && board.Rows[y - 1].Cells[x + 2].Value == null && !piece.Locked)
                            {
                                table.Rows[y - 1][x + 2] = "M";
                            }
                            else if (y - 1 > -1 && x + 2 < 8 && board.Rows[y - 1].Cells[x + 2].Value != null && containsEnemy(piece.Color, x + 2, y - 1) && !piece.Locked)
                            {
                                table.Rows[y - 1][x + 2] = "K";
                            }

                            //right down
                            if (y + 1 < 8 && x + 2 < 8 && board.Rows[y + 1].Cells[x + 2].Value == null && !piece.Locked)
                            {
                                table.Rows[y + 1][x + 2] = "M";
                            }
                            else if (y + 1 < 8 && x + 2 < 8 && board.Rows[y + 1].Cells[x + 2].Value != null && containsEnemy(piece.Color, x + 2, y + 1) && !piece.Locked)
                            {
                                table.Rows[y + 1][x + 2] = "K";
                            }

                            //down left
                            if (y + 2 < 8 && x - 1 > -1 && board.Rows[y + 2].Cells[x - 1].Value == null && !piece.Locked)
                            {
                                table.Rows[y + 2][x - 1] = "M";
                            }
                            else if (y + 2 < 8 && x - 1 > -1 && board.Rows[y + 2].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y + 2) && !piece.Locked)
                            {
                                table.Rows[y + 2][x - 1] = "K";
                            }

                            //down right
                            if (y + 2 < 8 && x + 1 < 8 && board.Rows[y + 2].Cells[x + 1].Value == null && !piece.Locked)
                            {
                                table.Rows[y + 2][x + 1] = "M";
                            }
                            else if (y + 2 < 8 && x + 1 < 8 && board.Rows[y + 2].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y + 2) && !piece.Locked)
                            {
                                table.Rows[y + 2][x + 1] = "K";
                            }

                            //left up
                            if (y + 1 < 8 && x - 2 > -1 && board.Rows[y + 1].Cells[x - 2].Value == null && !piece.Locked)
                            {
                                table.Rows[y + 1][x - 2] = "M";
                            }
                            else if (y + 1 < 8 && x - 2 > -1 && board.Rows[y + 1].Cells[x - 2].Value != null && containsEnemy(piece.Color, x - 2, y + 1) && !piece.Locked)
                            {
                                table.Rows[y + 1][x - 2] = "K";
                            }

                            //left down
                            if (y - 1 > -1 && x - 2 > -1 && board.Rows[y - 1].Cells[x - 2].Value == null && !piece.Locked)
                            {
                                table.Rows[y - 1][x - 2] = "M";
                            }
                            else if (y - 1 > -1 && x - 2 > -1 && board.Rows[y - 1].Cells[x - 2].Value != null && containsEnemy(piece.Color, x - 2, y - 1) && !piece.Locked)
                            {
                                table.Rows[y - 1][x - 2] = "K";
                            }
                        }
                        else if (piece.Name == "bishop")
                        {
                            int i;
                            int j;

                            //up right
                            for (i = x + 1, j = y - 1; i < 8; i++, j--)
                            {
                                if (j > 7 || j < 0)
                                {
                                    break;
                                }

                                if (board.Rows[j].Cells[i].Value == null && !piece.Locked)
                                {
                                    table.Rows[j][i] = "M";
                                }
                                else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && !piece.Locked)
                                {
                                    table.Rows[j][i] = "K";
                                    break;
                                }
                                else if (board.Rows[j].Cells[i].Value != null && !piece.Locked)
                                {
                                    break;
                                }
                            }

                            //up left
                            for (i = x - 1, j = y - 1; i > -1; i--, j--)
                            {
                                if (j > 7 || j < 0)
                                {
                                    break;
                                }

                                if (board.Rows[j].Cells[i].Value == null && !piece.Locked)
                                {
                                    table.Rows[j][i] = "M";
                                }
                                else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && !piece.Locked)
                                {
                                    table.Rows[j][i] = "K";
                                    break;
                                }
                                else if (board.Rows[j].Cells[i].Value != null && !piece.Locked)
                                {
                                    break;
                                }
                            }

                            //down right
                            for (i = x + 1, j = y + 1; i < 8; i++, j++)
                            {
                                if (j > 7 || j < 0)
                                {
                                    break;
                                }

                                if (board.Rows[j].Cells[i].Value == null && !piece.Locked)
                                {
                                    table.Rows[j][i] = "M";
                                }
                                else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && !piece.Locked)
                                {
                                    table.Rows[j][i] = "K";
                                    break;
                                }
                                else if (board.Rows[j].Cells[i].Value != null && !piece.Locked)
                                {
                                    break;
                                }
                            }

                            //down left
                            for (i = x - 1, j = y + 1; i > -1; i--, j++)
                            {
                                if (j > 7 || j < 0)
                                {
                                    break;
                                }

                                if (board.Rows[j].Cells[i].Value == null && !piece.Locked)
                                {
                                    table.Rows[j][i] = "M";
                                }
                                else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && !piece.Locked)
                                {
                                    table.Rows[j][i] = "K";
                                    break;
                                }
                                else if (board.Rows[j].Cells[i].Value != null && !piece.Locked)
                                {
                                    break;
                                }
                            }
                        }
                        else if (piece.Name == "queen")
                        {
                            int i;
                            int j;

                            //up right
                            for (i = x + 1, j = y - 1; i < 8; i++, j--)
                            {
                                if (j < 0)
                                {
                                    break;
                                }

                                if (board.Rows[j].Cells[i].Value == null && !piece.Locked)
                                {
                                    table.Rows[j][i] = "M";
                                }
                                else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && !piece.Locked)
                                {
                                    table.Rows[j][i] = "K";
                                    break;
                                }
                                else if (board.Rows[j].Cells[i].Value != null && !piece.Locked)
                                {
                                    break;
                                }
                            }

                            //up left
                            for (i = x - 1, j = y - 1; i > -1; i--, j--)
                            {
                                if (j < 0)
                                {
                                    break;
                                }

                                if (board.Rows[j].Cells[i].Value == null && !piece.Locked)
                                {
                                    table.Rows[j][i] = "M";
                                }
                                else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && !piece.Locked)
                                {
                                    table.Rows[j][i] = "K";
                                    break;
                                }
                                else if (board.Rows[j].Cells[i].Value != null && !piece.Locked)
                                {
                                    break;
                                }
                            }

                            //down right
                            for (i = x + 1, j = y + 1; i < 8; i++, j++)
                            {
                                if (j > 7)
                                {
                                    break;
                                }

                                if (board.Rows[j].Cells[i].Value == null && !piece.Locked)
                                {
                                    table.Rows[j][i] = "M";
                                }
                                else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && !piece.Locked)
                                {
                                    table.Rows[j][i] = "K";
                                    break;
                                }
                                else if (board.Rows[j].Cells[i].Value != null && !piece.Locked)
                                {
                                    break;
                                }
                            }

                            //down left
                            for (i = x - 1, j = y + 1; i > -1; i--, j++)
                            {
                                if (j > 7)
                                {
                                    break;
                                }

                                if (board.Rows[j].Cells[i].Value == null && !piece.Locked)
                                {
                                    table.Rows[j][i] = "M";
                                }
                                else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && !piece.Locked)
                                {
                                    table.Rows[j][i] = "K";
                                    break;
                                }
                                else if (board.Rows[j].Cells[i].Value != null && !piece.Locked)
                                {
                                    break;
                                }
                            }

                            //above (i represents y/row)
                            for (i = y - 1; i > -1; i--)
                            {
                                if (board.Rows[i].Cells[x].Value == null && !piece.Locked)
                                {
                                    table.Rows[i][x] = "M";
                                }
                                else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i) && !piece.Locked)
                                {
                                    table.Rows[i][x] = "K";
                                    break;
                                }
                                else if (board.Rows[i].Cells[x].Value != null && !piece.Locked)
                                {
                                    break;
                                }
                            }

                            //below (i represents y/row)
                            for (i = y + 1; i < 8; i++)
                            {
                                if (board.Rows[i].Cells[x].Value == null && !piece.Locked)
                                {
                                    table.Rows[i][x] = "M";
                                }
                                else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i) && !piece.Locked)
                                {
                                    table.Rows[i][x] = "K";
                                    break;
                                }
                                else if (board.Rows[i].Cells[x].Value != null && !piece.Locked)
                                {
                                    break;
                                }
                            }

                            //left (i represents x/column)
                            for (i = x - 1; i > -1; i--)
                            {
                                if (board.Rows[y].Cells[i].Value == null && !piece.Locked)
                                {
                                    table.Rows[y][i] = "M";
                                }
                                else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y) && !piece.Locked)
                                {
                                    table.Rows[y][i] = "K";
                                    break;
                                }
                                else if (board.Rows[y].Cells[i].Value != null && !piece.Locked)
                                {
                                    break;
                                }
                            }

                            //right (i represents x/column)
                            for (i = x + 1; i < 8; i++)
                            {
                                if (board.Rows[y].Cells[i].Value == null && !piece.Locked)
                                {
                                    table.Rows[y][i] = "M";
                                }
                                else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y) && !piece.Locked)
                                {
                                    table.Rows[y][i] = "K";
                                    break;
                                }
                                else if (board.Rows[y].Cells[i].Value != null && !piece.Locked)
                                {
                                    break;
                                }
                            }
                        }
                        else if (piece.Name == "king")
                        {
                            List<int[]> future = getCheckTiles(piece.Color == Color.White ? Color.Black : Color.White)[1];

                            //right
                            if (x + 1 < 8)
                            {
                                bool covered = false;
                                //make sure the king cant go on a space covered by the enemy
                                foreach (int[] tile in future)
                                {
                                    if (x + 1 == tile[0] && y == tile[1])
                                    {
                                        covered = true;
                                    }
                                }
                                if (!covered)
                                {
                                    if (board.Rows[y].Cells[x + 1].Value == null)
                                    {
                                        table.Rows[y][x + 1] = "M";
                                    }
                                    else if (board.Rows[y].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y))
                                    {
                                        table.Rows[y][x + 1] = "K";
                                    }
                                }

                            }

                            //left
                            if (x - 1 > -1)
                            {
                                bool covered = false;
                                //make sure the king cant go on a space covered by the enemy
                                foreach (int[] tile in future)
                                {
                                    if (x - 1 == tile[0] && y == tile[1])
                                    {
                                        covered = true;
                                    }
                                }
                                if (!covered)
                                {
                                    if (board.Rows[y].Cells[x - 1].Value == null)
                                    {
                                        table.Rows[y][x - 1] = "M";
                                    }
                                    else if (board.Rows[y].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y))
                                    {
                                        table.Rows[y][x - 1] = "K";
                                    }
                                }

                            }

                            //down
                            if (y + 1 < 8)
                            {
                                bool covered = false;
                                //make sure the king cant go on a space covered by the enemy
                                foreach (int[] tile in future)
                                {
                                    if (x == tile[0] && y + 1 == tile[1])
                                    {
                                        covered = true;
                                    }
                                }
                                if (!covered)
                                {
                                    if (board.Rows[y + 1].Cells[x].Value == null)
                                    {
                                        table.Rows[y + 1][x] = "M";
                                    }
                                    else if (board.Rows[y + 1].Cells[x].Value != null && containsEnemy(piece.Color, x, y + 1))
                                    {
                                        table.Rows[y + 1][x] = "K";
                                    }
                                }

                            }

                            //up
                            if (y - 1 > -1)
                            {
                                bool covered = false;
                                //make sure the king cant go on a space covered by the enemy
                                foreach (int[] tile in future)
                                {
                                    if (x == tile[0] && y - 1 == tile[1])
                                    {
                                        covered = true;
                                    }
                                }
                                if (!covered)
                                {
                                    if (board.Rows[y - 1].Cells[x].Value == null)
                                    {
                                        table.Rows[y - 1][x] = "M";
                                    }
                                    else if (board.Rows[y - 1].Cells[x].Value != null && containsEnemy(piece.Color, x, y - 1))
                                    {
                                        table.Rows[y - 1][x] = "M";
                                    }
                                }

                            }

                            //top right
                            if (x + 1 < 8 && y - 1 > -1)
                            {
                                bool covered = false;
                                //make sure the king cant go on a space covered by the enemy
                                foreach (int[] tile in future)
                                {
                                    if (x + 1 == tile[0] && y - 1 == tile[1])
                                    {
                                        covered = true;
                                    }
                                }
                                if (!covered)
                                {
                                    if (board.Rows[y - 1].Cells[x + 1].Value == null)
                                    {
                                        table.Rows[y - 1][x + 1] = "M";
                                    }
                                    else if (board.Rows[y - 1].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y - 1))
                                    {
                                        table.Rows[y - 1][x + 1] = "K";
                                    }
                                }

                            }

                            //top left
                            if (x - 1 > -1 && y - 1 > -1)
                            {
                                bool covered = false;
                                //make sure the king cant go on a space covered by the enemy
                                foreach (int[] tile in future)
                                {
                                    if (x - 1 == tile[0] && y - 1 == tile[1])
                                    {
                                        covered = true;
                                    }
                                }
                                if (!covered)
                                {
                                    if (board.Rows[y - 1].Cells[x - 1].Value == null)
                                    {
                                        table.Rows[y - 1][x - 1] = "M";
                                    }
                                    else if (board.Rows[y - 1].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y - 1))
                                    {
                                        table.Rows[y - 1][x - 1] = "K";
                                    }
                                }

                            }

                            //bottom right
                            if (x + 1 < 8 && y + 1 < 8)
                            {
                                bool covered = false;
                                //make sure the king cant go on a space covered by the enemy
                                foreach (int[] tile in future)
                                {
                                    if (x + 1 == tile[0] && y + 1 == tile[1])
                                    {
                                        covered = true;
                                    }
                                }
                                if (!covered)
                                {
                                    if (board.Rows[y + 1].Cells[x + 1].Value == null)
                                    {
                                        table.Rows[y + 1][x + 1] = "M";
                                    }
                                    else if (board.Rows[y + 1].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y + 1))
                                    {
                                        table.Rows[y + 1][x + 1] = "K";
                                    }
                                }

                            }

                            //bottom left
                            if (x - 1 > -1 && y + 1 < 8)
                            {
                                bool covered = false;
                                //make sure the king cant go on a space covered by the enemy
                                foreach (int[] tile in future)
                                {
                                    if (x - 1 == tile[0] && y + 1 == tile[1])
                                    {
                                        covered = true;
                                    }
                                }
                                if (!covered)
                                {
                                    if (board.Rows[y + 1].Cells[x - 1].Value == null)
                                    {
                                        table.Rows[y + 1][x - 1] = "M";
                                    }
                                    else if (board.Rows[y + 1].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y + 1))
                                    {
                                        table.Rows[y + 1][x - 1] = "K";
                                    }
                                }
                            }
                            //CASTLE
                            //check if white or black
                            if (piece.Color == Color.White)
                            {
                                //if king has not moved and the spaces between him and the rook are check free
                                if (!piece.Moved)
                                {
                                    //KINGSIDE CASTLE
                                    bool castle = true;

                                    //find kingside rook
                                    Piece kingRook = new Piece();
                                    foreach (Piece findRook in Pieces)
                                    {
                                        if (findRook.Name == "rook" && findRook.Color == piece.Color && !findRook.Moved && findRook.X == 7)
                                        {
                                            kingRook = findRook;
                                        }
                                    }

                                    //check if rook was found successfully and the spots between king and rook are empty
                                    if (kingRook.Name == "rook" && board.Rows[y].Cells[piece.X + 1].Value == null && board.Rows[y].Cells[piece.X + 2].Value == null)
                                    {
                                        //check each future move of the enemy to see if spaces in between king and rook are not in check
                                        foreach (int[] tile in future)
                                        {
                                            //if the 2 spots are checkable, castle is illegal
                                            if ((piece.X + 1 == tile[0] && piece.Y == tile[1]) || (piece.X + 2 == tile[0] && piece.Y == tile[1]))
                                            {
                                                castle = false;
                                            }
                                        }

                                        if (castle)
                                        {
                                            table.Rows[kingRook.Y][kingRook.X] = "C";
                                        }
                                    }


                                    //QUEENSIDE CASTLE
                                    castle = true;

                                    //find kingside rook
                                    Piece queenRook = new Piece();
                                    foreach (Piece findRook in Pieces)
                                    {
                                        if (findRook.Name == "rook" && findRook.Color == piece.Color && !findRook.Moved && findRook.X == 0)
                                        {
                                            queenRook = findRook;
                                        }
                                    }

                                    //check if rook was found successfully and the spots between king and rook are empty
                                    if (queenRook.Name == "rook" && board.Rows[y].Cells[piece.X - 1].Value == null && board.Rows[y].Cells[piece.X - 2].Value == null && board.Rows[y].Cells[piece.X - 3].Value == null)
                                    {
                                        //check each future move of the enemy to see if spaces in between king and rook are not in check
                                        foreach (int[] tile in future)
                                        {
                                            //if the 2 spots are checkable, castle is illegal
                                            if ((piece.X - 1 == tile[0] && piece.Y == tile[1]) || (piece.X - 2 == tile[0] && piece.Y == tile[1]))
                                            {
                                                castle = false;
                                            }
                                        }

                                        if (castle)
                                        {
                                            table.Rows[queenRook.Y][queenRook.X] = "C";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //if king has not moved and the spaces between him and the rook are check free
                                if (!piece.Moved)
                                {
                                    //KINGSIDE CASTLE
                                    bool castle = true;

                                    //find kingside rook
                                    Piece kingRook = new Piece();
                                    foreach (Piece findRook in Pieces)
                                    {
                                        if (findRook.Name == "rook" && findRook.Color == piece.Color && !findRook.Moved && findRook.X == 0)
                                        {
                                            kingRook = findRook;
                                        }
                                    }

                                    //check if rook was found successfully and the spots between king and rook are empty
                                    if (kingRook.Name == "rook" && board.Rows[y].Cells[piece.X - 1].Value == null && board.Rows[y].Cells[piece.X - 2].Value == null)
                                    {
                                        //check each future move of the enemy to see if spaces in between king and rook are not in check
                                        foreach (int[] tile in future)
                                        {
                                            //if the 2 spots are checkable, castle is illegal
                                            if ((piece.X - 1 == tile[0] && piece.Y == tile[1]) || (piece.X - 2 == tile[0] && piece.Y == tile[1]))
                                            {
                                                castle = false;
                                            }
                                        }

                                        if (castle)
                                        {
                                            table.Rows[kingRook.Y][kingRook.X] = "C";
                                        }
                                    }


                                    //QUEENSIDE CASTLE
                                    castle = true;

                                    //find kingside rook
                                    Piece queenRook = new Piece();
                                    foreach (Piece findRook in Pieces)
                                    {
                                        if (findRook.Name == "rook" && findRook.Color == piece.Color && !findRook.Moved && findRook.X == 7)
                                        {
                                            queenRook = findRook;
                                        }
                                    }

                                    //check if rook was found successfully and the spots between king and rook are empty
                                    if (queenRook.Name == "rook" && board.Rows[y].Cells[piece.X + 1].Value == null && board.Rows[y].Cells[piece.X + 2].Value == null && board.Rows[y].Cells[piece.X + 3].Value == null)
                                    {
                                        //check each future move of the enemy to see if spaces in between king and rook are not in check
                                        foreach (int[] tile in future)
                                        {
                                            //if the 2 spots are checkable, castle is illegal
                                            if ((piece.X + 1 == tile[0] && piece.Y == tile[1]) || (piece.X + 2 == tile[0] && piece.Y == tile[1]))
                                            {
                                                castle = false;
                                            }
                                        }

                                        if (castle)
                                        {
                                            table.Rows[queenRook.Y][queenRook.X] = "C";
                                        }
                                    }
                                }
                            }


                        }
                    }
                    else
                    {
                        List<int[]>[] getCheck = getCheckTiles(piece.Color == Color.White ? Color.Black : Color.White);
                        List<int[]> threats = getCheck[0];
                        List<int[]> future = getCheck[1];
                    }
                    
                }
                //in check
                else if((check[0] && piece.Color == Color.White) || (check[1] && piece.Color == Color.Black))
                {
                    List<int[]>[] getCheck = getCheckTiles(piece.Color == Color.White ? Color.Black : Color.White);
                    List<int[]> threats = getCheck[0];
                    List<int[]> future = getCheck[1];
                    int threatPieces = getCheck[2][0][0]; //TODO implement this

                    if (piece.Name == "king")
                    {
                        //up
                        if (y - 1 > -1 && board.Rows[y - 1].Cells[x].Value == null)
                        {
                            bool open = true;
                            foreach (int[] tile in threats)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x && tile[1] == y - 1 && tile[2] == 0)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            foreach (int[] tile in future)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x && tile[1] == y - 1)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            if (open)
                            {
                                table.Rows[y - 1][x] = "M";
                            }
                        }
                        //if an enemy is on that tile
                        else if (y - 1 > -1 && board.Rows[y - 1].Cells[x].Value != null && containsEnemy(piece.Color, x, y - 1))
                        {
                            bool open = true;
                            foreach (int[] tile in future)
                            {
                                //if that tile is also covered by another enemy
                                if (tile[0] == x && tile[1] == y - 1)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            foreach (int[] tile in threats)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x && tile[1] == y - 1 && tile[2] == 0)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            if (open)
                            {
                                table.Rows[y - 1][x] = "K";
                            }
                        }

                        //down
                        if (y + 1 < 8 && board.Rows[y + 1].Cells[x].Value == null)
                        {
                            bool open = true;
                            foreach (int[] tile in threats)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x && tile[1] == y + 1 && tile[2] == 0)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            foreach (int[] tile in future)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x && tile[1] == y + 1)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            if (open)
                            {
                                table.Rows[y + 1][x] = "M";
                            }
                        }
                        //if an enemy is on that tile
                        else if (y + 1 < 8 && board.Rows[y + 1].Cells[x].Value != null && containsEnemy(piece.Color, x, y + 1))
                        {
                            bool open = true;
                            foreach (int[] tile in future)
                            {
                                //if that tile is also covered by another enemy
                                if (tile[0] == x && tile[1] == y + 1)
                                {
                                    open = false;
                                }
                            }
                            foreach (int[] tile in threats)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x && tile[1] == y + 1 && tile[2] == 0)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            if (open)
                            {
                                table.Rows[y + 1][x] = "K";
                            }
                        }

                        //left
                        if (x - 1 > -1 && board.Rows[y].Cells[x - 1].Value == null)
                        {
                            bool open = true;
                            foreach (int[] tile in threats)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x - 1 && tile[1] == y && tile[2] == 0)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            foreach (int[] tile in future)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x - 1 && tile[1] == y)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            if (open)
                            {
                                table.Rows[y][x - 1] = "M";
                            }
                        }
                        //if an enemy is on that tile
                        else if (x - 1 > -1 && board.Rows[y].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y))
                        {
                            bool open = true;
                            foreach (int[] tile in future)
                            {
                                //if that tile is also covered by another enemy
                                if (tile[0] == x - 1 && tile[1] == y)
                                {
                                    open = false;
                                }
                            }
                            foreach (int[] tile in threats)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x - 1 && tile[1] == y && tile[2] == 0)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            if (open)
                            {
                                table.Rows[y][x - 1] = "K";
                            }
                        }

                        //right
                        if (x + 1 < 8 && board.Rows[y].Cells[x + 1].Value == null)
                        {
                            bool open = true;
                            foreach (int[] tile in threats)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x + 1 && tile[1] == y && tile[2] == 0)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            foreach (int[] tile in future)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x + 1 && tile[1] == y)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            if (open)
                            {
                                table.Rows[y][x + 1] = "M";
                            }
                        }
                        //if an enemy is on that tile
                        else if (x + 1 < 8 && board.Rows[y].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y))
                        {
                            bool open = true;
                            foreach (int[] tile in future)
                            {
                                //if that tile is also covered by another enemy
                                if (tile[0] == x + 1 && tile[1] == y)
                                {
                                    open = false;
                                }
                            }
                            foreach (int[] tile in threats)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x + 1 && tile[1] == y && tile[2] == 0)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            if (open)
                            {
                                table.Rows[y][x + 1] = "K";
                            }
                        }

                        //up left
                        if (x - 1 > -1 && y - 1 > -1 && board.Rows[y - 1].Cells[x - 1].Value == null)
                        {
                            bool open = true;
                            foreach (int[] tile in threats)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x - 1 && tile[1] == y - 1 && tile[2] == 0)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            foreach (int[] tile in future)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x - 1 && tile[1] == y - 1)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            if (open)
                            {
                                table.Rows[y - 1][x - 1] = "M";
                            }
                        }
                        //if an enemy is on that tile
                        else if (x - 1 > -1 && y - 1 > -1 && board.Rows[y - 1].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y - 1))
                        {
                            bool open = true;
                            foreach (int[] tile in future)
                            {
                                //if that tile is also covered by another enemy
                                if (tile[0] == x - 1 && tile[1] == y - 1)
                                {
                                    open = false;
                                }
                            }
                            foreach (int[] tile in threats)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x - 1 && tile[1] == y - 1 && tile[2] == 0)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            if (open)
                            {
                                table.Rows[y - 1][x - 1] = "K";
                            }
                        }

                        //up right
                        if (x + 1 < 8 && y - 1 > -1 && board.Rows[y - 1].Cells[x + 1].Value == null)
                        {
                            bool open = true;
                            foreach (int[] tile in threats)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x + 1 && tile[1] == y - 1 && tile[2] == 0)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            foreach (int[] tile in future)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x + 1 && tile[1] == y - 1)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            if (open)
                            {
                                table.Rows[y - 1][x + 1] = "M";
                            }
                        }
                        //if an enemy is on that tile
                        else if (x + 1 < 8 && y - 1 > -1 && board.Rows[y - 1].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y - 1))
                        {
                            bool open = true;
                            foreach (int[] tile in future)
                            {
                                //if that tile is also covered by another enemy
                                if (tile[0] == x + 1 && tile[1] == y - 1)
                                {
                                    open = false;
                                }
                            }
                            foreach (int[] tile in threats)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x + 1 && tile[1] == y - 1 && tile[2] == 0)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            if (open)
                            {
                                table.Rows[y - 1][x + 1] = "K";
                            }
                        }

                        //down left
                        if (x - 1 > -1 && y + 1 < 8 && board.Rows[y + 1].Cells[x - 1].Value == null)
                        {
                            bool open = true;
                            foreach (int[] tile in threats)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x - 1 && tile[1] == y + 1 && tile[2] == 0)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            foreach (int[] tile in future)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x - 1 && tile[1] == y + 1)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            if (open)
                            {
                                table.Rows[y + 1][x - 1] = "M";
                            }
                        }
                        //if an enemy is on that tile
                        else if (x - 1 > -1 && y + 1 < 8 && board.Rows[y + 1].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y + 1))
                        {
                            bool open = true;
                            foreach (int[] tile in future)
                            {
                                //if that tile is also covered by another enemy
                                if (tile[0] == x - 1 && tile[1] == y + 1)
                                {
                                    open = false;
                                }
                            }
                            foreach (int[] tile in threats)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x - 1 && tile[1] == y + 1 && tile[2] == 0)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            if (open)
                            {
                                table.Rows[y + 1][x - 1] = "K";
                            }
                        }

                        //down right
                        if (x + 1 < 8 && y + 1 < 8 && board.Rows[y + 1].Cells[x + 1].Value == null)
                        {
                            bool open = true;
                            foreach (int[] tile in threats)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x + 1 && tile[1] == y + 1 && tile[2] == 0)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            foreach (int[] tile in future)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x + 1 && tile[1] == y + 1)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            if (open)
                            {
                                table.Rows[y + 1][x + 1] = "M";
                            }
                        }
                        //if an enemy is on that tile
                        else if (x + 1 < 8 && y + 1 < 8 && board.Rows[y + 1].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y + 1))
                        {
                            bool open = true;
                            foreach (int[] tile in future)
                            {
                                //if that tile is also covered by another enemy
                                if (tile[0] == x + 1 && tile[1] == y + 1)
                                {
                                    open = false;
                                }
                            }
                            foreach (int[] tile in threats)
                            {
                                //if the tile is covered by the enemy, the king cant move there and the opening is false
                                if (tile[0] == x + 1 && tile[1] == y + 1 && tile[2] == 0)
                                {
                                    open = false;
                                    break;
                                }
                            }
                            if (open)
                            {
                                table.Rows[y + 1][x + 1] = "K";
                            }
                        }
                    }
                    //bottom side pawn
                    else if (piece.Name == "pawn" && ((piece.Color == Color.White && turn) || (piece.Color == Color.Black && !turn)))
                    {
                        //up
                        if (y - 1 > -1 && board.Rows[y - 1].Cells[x].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                //if the tile is threatening the king, the piece can move to block that threat
                                if (tile[0] == x && tile[1] == y - 1 && tile[2] == 0)
                                {
                                    table.Rows[y - 1][x] = "M";
                                    break;
                                }
                            }
                        }

                        //up 2
                        if (y == 6 && board.Rows[y - 2].Cells[x].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                //if the tile is threatening the king, the piece can move to block that threat
                                if (tile[0] == x && tile[1] == y - 2 && tile[2] == 0)
                                {
                                    table.Rows[y - 2][x] = "M";
                                    break;
                                }
                            }
                        }

                        //up left
                        if (y - 1 > -1 && x - 1 > -1 && board.Rows[y - 1].Cells[x - 1].Value != null && containsEnemy(Color.White, x - 1, y - 1))
                        {
                            foreach (int[] tile in threats)
                            {
                                //if the tile is threatening the king, the piece can move to block that threat
                                if (tile[0] == x - 1 && tile[1] == y - 1)
                                {
                                    table.Rows[y - 1][x - 1] = "K";
                                    break;
                                }
                            }
                        }

                        //up right
                        if (y - 1 > -1 && x + 1 < 8 && board.Rows[y - 1].Cells[x + 1].Value != null && containsEnemy(Color.White, x + 1, y - 1))
                        {
                            foreach (int[] tile in threats)
                            {
                                //if the tile is threatening the king, the piece can move to block that threat
                                if (tile[0] == x + 1 && tile[1] == y - 1)
                                {
                                    table.Rows[y - 1][x + 1] = "K";
                                    break;
                                }
                            }
                        }

                        Piece LeftEnemy = Pieces.FirstOrDefault(k => k.X == x - 1 && k.Y == y);
                        Piece RightEnemy = Pieces.FirstOrDefault(k => k.X == x + 1 && k.Y == y);

                        //left en Passant
                        if (x - 1 > -1 && y - 1 > -1 && LeftEnemy != null && board.Rows[y - 1].Cells[x - 1].Value == null && LeftEnemy.Color != color && !piece.Locked && piece.DoubleMoved)
                        {
                            foreach (int[] tile in threats)
                            {
                                //if the tile is threatening the king, the piece can move to block that threat
                                if (tile[0] == x - 1 && tile[1] == y - 1)
                                {
                                    table.Rows[y - 1][x - 1] = "M";
                                    table.Rows[y][x - 1] = "K";
                                    break;
                                }
                            }
                        }

                        //right en Passant
                        if (x + 1 < 8 && y - 1 > -1 && LeftEnemy != null && board.Rows[y - 1].Cells[x + 1].Value == null && LeftEnemy.Color != color && !piece.Locked && piece.DoubleMoved)
                        {
                            foreach (int[] tile in threats)
                            {
                                //if the tile is threatening the king, the piece can move to block that threat
                                if (tile[0] == x - 1 && tile[1] == y - 1)
                                {
                                    table.Rows[y - 1][x + 1] = "M";
                                    table.Rows[y][x + 1] = "K";
                                    break;
                                }
                            }

                        }
                    }
                    //top side pawn
                    else if (piece.Name == "pawn" && ((piece.Color == Color.White && !turn) || (piece.Color == Color.Black && turn)))
                    {
                        //move
                        if (y + 1 < 8 && board.Rows[y + 1].Cells[x].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                //if the tile is threatening the king, the piece can move to block that threat
                                if (tile[0] == x && tile[1] == y + 1 && tile[2] == 0)
                                {
                                    table.Rows[y + 1][x] = "M";
                                    break;
                                }
                            }
                        }

                        //double move
                        if (y == 1 && board.Rows[y + 2].Cells[x].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                //if the tile is threatening the king, the piece can move to block that threat
                                if (tile[0] == x && tile[1] == y + 2 && tile[2] == 0)
                                {
                                    table.Rows[y + 2][x] = "M";
                                    break;
                                }
                            }
                        }

                        //left kill
                        if (y + 1 < 8 && x - 1 > -1 && board.Rows[y + 1].Cells[x - 1].Value != null && containsEnemy(Color.White, x - 1, y + 1))
                        {
                            foreach (int[] tile in threats)
                            {
                                //if the tile is threatening the king, the piece can move to block that threat
                                if (tile[0] == x - 1 && tile[1] == y + 1)
                                {
                                    table.Rows[y + 1][x - 1] = "K";
                                    break;
                                }
                            }
                        }

                        //right kill
                        if (y + 1 < 8 && x + 1 < 8 && board.Rows[y + 1].Cells[x + 1].Value != null && containsEnemy(Color.White, x + 1, y + 1))
                        {
                            foreach (int[] tile in threats)
                            {
                                //if the tile is threatening the king, the piece can move to block that threat
                                if (tile[0] == x + 1 && tile[1] == y + 1)
                                {
                                    table.Rows[y + 1][x + 1] = "K";
                                    break;
                                }
                            }
                        }

                        Piece LeftEnemy = Pieces.FirstOrDefault(k => k.X == x - 1 && k.Y == y);
                        Piece RightEnemy = Pieces.FirstOrDefault(k => k.X == x + 1 && k.Y == y);

                        //right en Passant
                        if (x - 1 > -1 && y + 1 < 8 && LeftEnemy != null && board.Rows[y + 1].Cells[x - 1].Value == null && LeftEnemy.Color != color && !piece.Locked && piece.DoubleMoved)
                        {
                            foreach (int[] tile in threats)
                            {
                                //if the tile is threatening the king, the piece can move to block that threat
                                if (tile[0] == x - 1 && tile[1] == y + 1)
                                {
                                    table.Rows[y + 1][x - 1] = "M";
                                    table.Rows[y][x - 1] = "K";
                                    break;
                                }
                            }
                        }

                        //left en Passant
                        if (x + 1 < 8 && y + 1 > -1 && LeftEnemy != null && board.Rows[y + 1].Cells[x + 1].Value == null && LeftEnemy.Color != color && !piece.Locked && piece.DoubleMoved)
                        {
                            foreach (int[] tile in threats)
                            {
                                //if the tile is threatening the king, the piece can move to block that threat
                                if (tile[0] == x - 1 && tile[1] == y + 1)
                                {
                                    table.Rows[y + 1][x + 1] = "M";
                                    table.Rows[y][x + 1] = "K";
                                    break;
                                }
                            }

                        }
                    }
                    else if (piece.Name == "rook")
                    {
                        //above (i represents y/row)
                        for (int i = y - 1; i > -1; i--)
                        {
                            if (board.Rows[i].Cells[x].Value == null)
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == x && tile[1] == i)
                                    {
                                        table.Rows[i][x] = "M";
                                    }
                                }
                            }
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i))
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == x && tile[1] == i)
                                    {
                                        table.Rows[i][x] = "K";
                                        break;
                                    }
                                }
                            }
                            else if (board.Rows[i].Cells[x].Value != null)
                            {
                                break;
                            }
                        }

                        //below (i represents y/row)
                        for (int i = y + 1; i < 8; i++)
                        {
                            if (board.Rows[i].Cells[x].Value == null)
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == x && tile[1] == i)
                                    {
                                        table.Rows[i][x] = "M";
                                    }
                                }
                            }
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i))
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == x && tile[1] == i)
                                    {
                                        table.Rows[i][x] = "K";
                                        break;
                                    }
                                }
                            }
                            else if (board.Rows[i].Cells[x].Value != null)
                            {
                                break;
                            }
                        }

                        //left (i represents x/column)
                        for (int i = x - 1; i > -1; i--)
                        {
                            if (board.Rows[y].Cells[i].Value == null)
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == i && tile[1] == y)
                                    {
                                        table.Rows[y][i] = "M";
                                    }
                                }
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y))
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == i && tile[1] == y)
                                    {
                                        table.Rows[y][i] = "K";
                                        break;
                                    }
                                }
                            }
                            else if (board.Rows[y].Cells[i].Value != null)
                            {
                                break;
                            }
                        }

                        //right (i represents x/column)
                        for (int i = x + 1; i < 8; i++)
                        {
                            if (board.Rows[y].Cells[i].Value == null)
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == i && tile[1] == y)
                                    {
                                        table.Rows[y][i] = "M";
                                    }
                                }
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y))
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == i && tile[1] == y)
                                    {
                                        table.Rows[y][i] = "K";
                                        break;
                                    }
                                }
                            }
                            else if (board.Rows[y].Cells[i].Value != null)
                            {
                                break;
                            }
                        }

                    }
                    else if (piece.Name == "horse")
                    {
                        //up left
                        if (y - 2 > -1 && x - 1 > -1 && board.Rows[y - 2].Cells[x - 1].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x - 1 && tile[1] == y - 2)
                                {
                                    table.Rows[y - 2][x - 1] = "M";
                                }
                            }
                        }
                        else if (y - 2 > -1 && x - 1 > -1 && board.Rows[y - 2].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y - 2))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x - 1 && tile[1] == y - 2)
                                {
                                    table.Rows[y - 2][x - 1] = "K";
                                }
                            }
                        }

                        //up right
                        if (y - 2 > -1 && x + 1 < 8 && board.Rows[y - 2].Cells[x + 1].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x + 1 && tile[1] == y - 2)
                                {
                                    table.Rows[y - 2][x + 1] = "M";
                                }
                            }
                        }
                        else if (y - 2 > -1 && x + 1 < 8 && board.Rows[y - 2].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y - 2))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x + 1 && tile[1] == y - 2)
                                {
                                    table.Rows[y - 2][x + 1] = "K";
                                }
                            }
                        }

                        //right up
                        if (y - 1 > -1 && x + 2 < 8 && board.Rows[y - 1].Cells[x + 2].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x + 2 && tile[1] == y - 1)
                                {
                                    table.Rows[y - 1][x + 2] = "M";
                                }
                            }
                        }
                        else if (y - 1 > -1 && x + 2 < 8 && board.Rows[y - 1].Cells[x - 1].Value != null && containsEnemy(piece.Color, x + 2, y - 1))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x + 2 && tile[1] == y - 1)
                                {
                                    table.Rows[y - 1][x + 2] = "K";
                                }
                            }
                        }

                        //right down
                        if (y + 1 < 8 && x + 2 < 8 && board.Rows[y + 1].Cells[x + 2].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x + 2 && tile[1] == y + 1)
                                {
                                    table.Rows[y + 1][x + 2] = "M";
                                }
                            }
                        }
                        else if (y + 1 < 8 && x + 2 < 8 && board.Rows[y + 1].Cells[x + 2].Value != null && containsEnemy(piece.Color, x + 2, y + 1))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x + 2 && tile[1] == y + 1)
                                {
                                    table.Rows[y + 1][x + 2] = "K";
                                }
                            }
                        }

                        //down right
                        if (y + 2 < 8 && x + 1 < 8 && board.Rows[y + 2].Cells[x + 1].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x + 1 && tile[1] == y + 2)
                                {
                                    table.Rows[y + 2][x + 1] = "M";
                                }
                            }
                        }
                        else if (y + 2 < 8 && x + 1 < 8 && board.Rows[y + 2].Cells[x - 1].Value != null && containsEnemy(piece.Color, x + 1, y + 2))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x + 1 && tile[1] == y + 2)
                                {
                                    table.Rows[y + 2][x + 1] = "K";
                                }
                            }
                        }

                        //down left
                        if (y + 2 < 8 && x - 1 > -1 && board.Rows[y + 2].Cells[x - 1].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x - 1 && tile[1] == y + 2)
                                {
                                    table.Rows[y + 2][x - 1] = "M";
                                }
                            }
                        }
                        else if (y + 2 < 8 && x - 1 > -1 && board.Rows[y + 2].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y + 2))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x - 1 && tile[1] == y + 2)
                                {
                                    table.Rows[y + 2][x - 1] = "K";
                                }
                            }
                        }

                        //left down
                        if (y + 1 < 8 && x - 2 > -1 && board.Rows[y + 1].Cells[x - 2].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x - 2 && tile[1] == y + 1)
                                {
                                    table.Rows[y + 1][x - 2] = "M";
                                }
                            }
                        }
                        else if (y + 1 < 8 && x - 2 > -1 && board.Rows[y + 1].Cells[x - 2].Value != null && containsEnemy(piece.Color, x - 2, y + 1))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x - 2 && tile[1] == y + 1)
                                {
                                    table.Rows[y + 1][x - 2] = "K";
                                }
                            }
                        }

                        //left up
                        if (y - 1 > -1 && x - 2 > -1 && board.Rows[y - 1].Cells[x - 2].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x - 2 && tile[1] == y - 1)
                                {
                                    table.Rows[y - 1][x - 2] = "M";
                                }
                            }
                        }
                        else if (y - 1 > -1 && x - 2 > -1 && board.Rows[y - 1].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 2, y - 1))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x - 2 && tile[1] == y - 1)
                                {
                                    table.Rows[y - 1][x - 2] = "K";
                                }
                            }
                        }
                    }
                    else if (piece.Name == "bishop")
                    {
                        int i;
                        int j;
                        //up right
                        for (i = x + 1, j = y - 1; i < 8; i++, j--)
                        {
                            if (j < 0)
                            {
                                break;
                            }

                            if (board.Rows[j].Cells[i].Value == null)
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == i && tile[1] == j)
                                    {
                                        table.Rows[j][i] = "M";
                                    }
                                }
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == i && tile[1] == j)
                                    {
                                        table.Rows[j][i] = "K";
                                    }
                                }
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null)
                            {
                                break;
                            }
                        }

                        //down right
                        for (i = x + 1, j = y + 1; i < 8; i++, j++)
                        {
                            if (j > 7)
                            {
                                break;
                            }

                            if (board.Rows[j].Cells[i].Value == null)
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == i && tile[1] == j)
                                    {
                                        table.Rows[j][i] = "M";
                                    }
                                }
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == i && tile[1] == j)
                                    {
                                        table.Rows[j][i] = "K";
                                    }
                                }
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null)
                            {
                                break;
                            }
                        }

                        //down left
                        for (i = x - 1, j = y + 1; i > -1; i--, j++)
                        {
                            if (j > 7)
                            {
                                break;
                            }

                            if (board.Rows[j].Cells[i].Value == null)
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == i && tile[1] == j)
                                    {
                                        table.Rows[j][i] = "M";
                                    }
                                }
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == i && tile[1] == j)
                                    {
                                        table.Rows[j][i] = "K";
                                    }
                                }
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null)
                            {
                                break;
                            }
                        }

                        //up left
                        for (i = x - 1, j = y - 1; i > -1; i--, j--)
                        {
                            if (j < 0)
                            {
                                break;
                            }

                            if (board.Rows[j].Cells[i].Value == null)
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == i && tile[1] == j)
                                    {
                                        table.Rows[j][i] = "M";
                                    }
                                }
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == i && tile[1] == j)
                                    {
                                        table.Rows[j][i] = "K";
                                    }
                                }
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null)
                            {
                                break;
                            }
                        }
                    }
                    else if (piece.Name == "queen")
                    {
                        int i;
                        int j;
                        //up right
                        for (i = x + 1, j = y - 1; i < 8; i++, j--)
                        {
                            if (j < 0)
                            {
                                break;
                            }

                            if (board.Rows[j].Cells[i].Value == null)
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == x && tile[1] == i)
                                    {
                                        table.Rows[i][x] = "M";
                                    }
                                }
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == x && tile[1] == i)
                                    {
                                        table.Rows[i][x] = "K";
                                    }
                                }
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                            {
                                break;
                            }
                        }

                        //down right
                        for (i = x + 1, j = y + 1; i < 8; i++, j++)
                        {
                            if (j > 7)
                            {
                                break;
                            }

                            if (board.Rows[j].Cells[i].Value == null)
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == x && tile[1] == i)
                                    {
                                        table.Rows[i][x] = "M";
                                    }
                                }
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == x && tile[1] == i)
                                    {
                                        table.Rows[i][x] = "K";
                                    }
                                }
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                            {
                                break;
                            }
                        }

                        //down left
                        for (i = x - 1, j = y + 1; i > -1; i--, j++)
                        {
                            if (j > 7)
                            {
                                break;
                            }

                            if (board.Rows[j].Cells[i].Value == null)
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == x && tile[1] == i)
                                    {
                                        table.Rows[i][x] = "M";
                                    }
                                }
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == x && tile[1] == i)
                                    {
                                        table.Rows[i][x] = "K";
                                    }
                                }
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                            {
                                break;
                            }
                        }

                        //up left
                        for (i = x - 1, j = y - 1; i > -1; i--, j--)
                        {
                            if (j < 0)
                            {
                                break;
                            }

                            if (board.Rows[j].Cells[i].Value == null)
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == x && tile[1] == i)
                                    {
                                        table.Rows[i][x] = "M";
                                    }
                                }
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == x && tile[1] == i)
                                    {
                                        table.Rows[i][x] = "K";
                                    }
                                }
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                            {
                                break;
                            }
                        }

                        //above (i represents y/row)
                        for (i = y - 1; i > -1; i--)
                        {
                            if (board.Rows[i].Cells[x].Value == null)
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == x && tile[1] == i)
                                    {
                                        table.Rows[i][x] = "M";
                                    }
                                }
                            }
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i))
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == x && tile[1] == i)
                                    {
                                        table.Rows[i][x] = "K";
                                        break;
                                    }
                                }
                            }
                            else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.Color, x, i))
                            {
                                break;
                            }
                        }

                        //below (i represents y/row)
                        for (i = y + 1; i < 8; i++)
                        {
                            if (board.Rows[i].Cells[x].Value == null)
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == x && tile[1] == i)
                                    {
                                        table.Rows[i][x] = "M";
                                    }
                                }
                            }
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i))
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == x && tile[1] == i)
                                    {
                                        table.Rows[i][x] = "K";
                                        break;
                                    }
                                }
                            }
                            else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.Color, x, i))
                            {
                                break;
                            }
                        }

                        //left (i represents x/column)
                        for (i = x - 1; i > -1; i--)
                        {
                            if (board.Rows[y].Cells[i].Value == null)
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == i && tile[1] == y)
                                    {
                                        table.Rows[y][i] = "M";
                                    }
                                }
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y))
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == i && tile[1] == y)
                                    {
                                        table.Rows[y][i] = "K";
                                        break;
                                    }
                                }
                            }
                            else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.Color, i, y))
                            {
                                break;
                            }
                        }

                        //right (i represents x/column)
                        for (i = x + 1; i > 8; i++)
                        {
                            if (board.Rows[y].Cells[i].Value == null)
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == i && tile[1] == y)
                                    {
                                        table.Rows[y][i] = "M";
                                    }
                                }
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y))
                            {
                                foreach (int[] tile in threats)
                                {
                                    if (tile[0] == i && tile[1] == y)
                                    {
                                        table.Rows[y][i] = "K";
                                        break;
                                    }
                                }
                            }
                            else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.Color, i, y))
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// returns the move notation in String form
        /// </summary>
        /// <param name="piece">the piece being moved. K = King, Q = Queen, B = Bishop, N = Knight, R = Rook, blank = Pawn</param>
        /// <param name="x">x cord of move</param>
        /// <param name="y">y cord of move</param>
        /// <param name="kill">if a piece was killed</param>
        /// <param name="check">if the move results in a check 0 = no check, 1 = check, 2 = checkmate</param>
        /// <param name="castle">if a castle occured. 0 = no castle, 1 = kingside, 2 = queenside</param>
        /// /// <param name="promotion">if a pawn is promoted</param>
        /// <returns></returns>
        public String MoveToString(String piece, int x, int y, bool kill, int check, int castle, bool promotion)
        {
            String tile = getTile(x,y);

            String name = "";

            if (piece == "king")
            {
                name = "k";
            }
            else if (piece == "queen")
            {
                name = "q";
            }
            else if (piece == "bishop")
            {
                name = "b";
            }
            else if (piece == "horse")
            {
                name = "n";
            }
            else if (piece == "rook")
            {
                name = "r";
            }

            if (castle == 1)
            {
                return "0-0";
            }
            else if (castle == 2)
            {
                return "0-0-0";
            }

            if (check == 1 && kill)
            {
                return name + "x" + tile + "+";
            }

            if (check == 1)
            {
                return name + tile + "+";
            }

            if (check == 2)
            {
                return name + tile + "#";
            }

            if (kill)
            {
                return name + "x" + tile;
            }

            if (promotion)
            {
                return tile + "=Q";
            }
            return name + tile;
        }

        /// <summary>
        /// ends the game
        /// </summary>
        /// <param name="winner">side that won</param>
        public void endGame(Color winner)
        {
            gameEnd = true;
            if (winner == Color.Yellow)
            {
                winnerLabel.Text = "Winner: Stalemate";
            }
            else
            {
                winnerLabel.Text = "Winner:" + (winner == Color.White ? "White" : "Black");
            }
            winnerLabel.Show();
        }

        /// <summary>
        /// Returns a 3 array lists. 
        /// The First list shows the spaces the team in check must cover to protect the king and get out of check. 
        /// The Second list shows the spaces the king cannot move to since that would result in the kings death during the next turn
        /// The Third array list has one element (int) showing how many pieces are holding the king in check at this moment
        /// int[0] = x
        /// int[1] = y
        /// int[2] = (true = has enemy, false = empty)
        /// </summary>
        /// <param name="attack">the team attacking (the team not in check)</param>
        /// <returns></returns>
        public List<int[]>[] getCheckTiles(Color attack)
        {
            //king that is being attacked (other color's king)
            Piece king = Pieces.Find(k => k.Name == "king" && k.Color != attack);

            // int[0] == x cord
            // int[1] == y cord
            // int[2] == (1 == piece) (0 == space)
            //only tiles that are curently threatening the king
            List<int[]> threats = new List<int[]>();

            //tiles that the king cannot move to because it would still put him in danger 
            //(pawn side kill is an example of a future tile but not a tile that is currently threatneing the king)
            //basically all the tiles the attacking team can move to
            List<int[]> future = new List<int[]>();

            //# of pieces holding the king in check rn
            int count = 0;

            foreach (Piece piece in Pieces)
            {
                //checks if piece is attacking
                if (piece.Color == attack)
                {
                    int x = piece.X;
                    int y = piece.Y;

                    //if the pawn can kill the king, the cord of the pawn in recorded as necessary to kill
                    //if the pawn kill tile is empty, it is marked as a future threat
                    if (piece.Name == "pawn" && piece.Color == Color.White && attack == Color.White)
                    {
                        if (turn)
                        {
                            if (x - 1 > -1 && y - 1 > -1)
                            {
                                if (king.X == x - 1 && king.Y == y - 1)
                                {
                                    threats.Add(new int[] { x, y, 1 });
                                    future.Add(new int[] { x - 1, y - 1, 0 });
                                    count++;
                                }
                                else
                                {
                                    future.Add(new int[] { x - 1, y - 1, 0 });
                                }
                            }
                            if (x + 1 < 8 && y - 1 > -1)
                            {
                                if (king.X == x + 1 && king.Y == y - 1)
                                {
                                    threats.Add(new int[] { x, y, 1 });
                                    future.Add(new int[] { x + 1, y - 1, 0 });
                                    count++;
                                }
                                else
                                {
                                    future.Add(new int[] { x + 1, y - 1, 0 });
                                }
                            }
                        }
                        else
                        {
                            if (x - 1 > -1 && y + 1 < 8)
                            {
                                if (king.X == x - 1 && king.Y == y + 1)
                                {
                                    threats.Add(new int[] { x, y, 1 });
                                    future.Add(new int[] { x - 1, y + 1, 0 });
                                    count++;
                                }
                                else
                                {
                                    future.Add(new int[] { x - 1, y + 1, 0 });
                                }
                            }
                            if (x + 1 < 8 && y + 1 < 8)
                            {
                                if (king.X == x + 1 && king.Y == y + 1)
                                {
                                    threats.Add(new int[] { x, y, 1 });
                                    future.Add(new int[] { x + 1, y + 1, 0 });
                                    count++;
                                }
                                else
                                {
                                    future.Add(new int[] { x + 1, y + 1, 0 });
                                }
                            }
                        }
                        
                    }
                    else if (piece.Name == "pawn" && piece.Color == Color.Black && attack == Color.Black)
                    {
                        if (turn)
                        {
                            if (x - 1 > -1 && y + 1 < 8)
                            {
                                if (king.X == x - 1 && king.Y == y + 1)
                                {
                                    threats.Add(new int[] { x, y, 1 });
                                    future.Add(new int[] { x - 1, y + 1, 0 });
                                    count++;
                                }
                                else
                                {
                                    future.Add(new int[] { x - 1, y + 1, 0 });
                                }
                            }
                            if (x + 1 < 8 && y + 1 < 8)
                            {
                                if (king.X == x + 1 && king.Y == y + 1)
                                {
                                    threats.Add(new int[] { x, y, 1 });
                                    future.Add(new int[] { x + 1, y + 1, 0 });
                                    count++;
                                }
                                else
                                {
                                    future.Add(new int[] { x + 1, y + 1, 0 });
                                }
                            }
                        }
                        else
                        {
                            if (x - 1 > -1 && y - 1 > -1)
                            {
                                if (king.X == x - 1 && king.Y == y - 1)
                                {
                                    threats.Add(new int[] { x, y, 1 });
                                    future.Add(new int[] { x - 1, y - 1, 0 });
                                    count++;
                                }
                                else
                                {
                                    future.Add(new int[] { x - 1, y - 1, 0 });
                                }
                            }
                            if (x + 1 < 8 && y - 1 > -1)
                            {
                                if (king.X == x + 1 && king.Y == y - 1)
                                {
                                    threats.Add(new int[] { x, y, 1 });
                                    future.Add(new int[] { x + 1, y - 1, 0 });
                                    count++;
                                }
                                else
                                {
                                    future.Add(new int[] { x + 1, y - 1, 0 });
                                }
                            }
                        }
                    }
                    //if the blue spots lead to a kill on the king, the possible tiles in List(possible) are added to tiles
                    else if (piece.Name == "rook")
                    {
                        //lits of possible spaces the rook can go to kill the king
                        List<int[]> possible = new List<int[]>();

                        //adding rook cords
                        possible.Add(new int[] { x, y, 1 });

                        //above
                        for (int i = y - 1; i > -1; i--)
                        {
                            //empty
                            if (board.Rows[i].Cells[x].Value == null)
                            {
                                possible.Add(new int[] { x, i, 0 });
                                future.Add(new int[] { x, i, 0 });
                            }
                            //piece is aiming at king
                            else if (king.X == x && king.Y == i)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { x, i, 0 });
                                count++;
                                break;
                            }
                            //The piece is being blocked some other piece (doesn't matter which side. If a piece is in the way, the line stops here)
                            else if (board.Rows[i].Cells[x].Value != null)
                            {
                                future.Add(new int[] { x, i, 0 });
                                break;
                            }
                        }

                        //if the king wasn't killable through the spaces above, it clears the possible tiles and checks the next direction
                        possible.Clear();

                        //adding rook cords
                        possible.Add(new int[] { x, y, 1 });

                        //below (i represents y/row)
                        for (int i = y + 1; i < 8; i++)
                        {
                            if (board.Rows[i].Cells[x].Value == null)
                            {
                                possible.Add(new int[] { x, i, 0 });
                                future.Add(new int[] { x, i, 0 });
                            }
                            else if (king.X == x && king.Y == i)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { x, i, 0 });
                                count++;
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null)
                            {
                                future.Add(new int[] { x, i, 0 });
                                break;
                            }
                        }

                        //if the king wasn't killable through the spaces above, it clears the possible tiles and checks the next direction
                        possible.Clear();

                        //adding rook cords
                        possible.Add(new int[] { x, y, 1 });

                        //left (i represents x/column)
                        for (int i = x - 1; i > -1; i--)
                        {
                            if (board.Rows[y].Cells[i].Value == null)
                            {
                                possible.Add(new int[] { i, y, 0 });
                                future.Add(new int[] { i, y, 0 });
                            }
                            else if (king.X == i && king.Y == y)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { i, y, 0 });
                                count++;
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null)
                            {
                                future.Add(new int[] { i, y, 0 });
                                break;
                            }
                        }

                        //if the king wasn't killable through the spaces above, it clears the possible tiles and checks the next direction
                        possible.Clear();

                        //adding rook cords
                        possible.Add(new int[] { x, y, 1 });

                        //right (i represents x/column)
                        for (int i = x + 1; i < 8; i++)
                        {
                            if (board.Rows[y].Cells[i].Value == null)
                            {
                                possible.Add(new int[] { i, y, 0 });
                                future.Add(new int[] { i, y, 0 });
                            }
                            else if (king.X == i && king.Y == y)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { i, y, 0 });
                                count++;
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null)
                            {
                                future.Add(new int[] { i, y, 0 });
                                break;
                            }
                        }
                        possible.Clear();
                    }
                    //if the horse can kill the king, the cords of the horse are recorded as necessary to kill
                    else if (piece.Name == "horse")
                    {
                        //up left
                        if (y - 2 > -1 && x - 1 > -1)
                        {
                            if (king.X == x - 1 && king.Y == y - 2)
                            {
                                threats.Add(new int[] {x, y, 1});
                                count++;
                            }
                            else
                            {
                                future.Add(new int[] {x - 1, y - 2, 0});
                            }
                        }

                        //up right
                        if (y - 2 > -1 && x + 1 < 8 )
                        {
                            if (king.X == x + 1 && king.Y == y - 2)
                            {
                                threats.Add(new int[] { x, y, 1 });
                                count++;
                            }
                            else
                            {
                                future.Add(new int[] { x + 1, y - 2, 0 });
                            }
                        }

                        //right up
                        if (y - 1 > -1 && x + 2 < 8)
                        {
                            if (king.X == x + 2 && king.Y == y - 1)
                            {
                                threats.Add(new int[] { x, y, 1 });
                                count++;
                            }
                            else
                            {
                                future.Add(new int[] { x + 2, y - 1, 0 });
                            }
                        }

                        //right down
                        if (y + 1 < 8 && x + 2 < 8)
                        {
                            if (king.X == x + 2 && king.Y == y + 1)
                            {
                                threats.Add(new int[] { x, y, 1 });
                                count++;
                            }
                            else
                            {
                                future.Add(new int[] { x + 2, y + 1, 0 });
                            }
                        }

                        //down left
                        if (y + 2 < 8 && x - 1 > -1)
                        {
                            if (king.X == x - 1 && king.Y == y + 2)
                            {
                                threats.Add(new int[] { x, y, 1 });
                                count++;
                            }
                            else
                            {
                                future.Add(new int[] { x - 1, y + 2, 0 });
                            }
                        }

                        //down right
                        if (y + 2 < 8 && x + 1 < 8)
                        {
                            if (king.X == x + 1 && king.Y == y + 2)
                            {
                                threats.Add(new int[] { x, y, 1 });
                                count++;
                            }
                            else
                            {
                                future.Add(new int[] { x + 1, y + 2, 0 });
                            }
                        }

                        //left up
                        if (y + 1 < 8 && x - 2 > -1)
                        {
                            if (king.X == x - 2 && king.Y == y + 1)
                            {
                                threats.Add(new int[] { x, y, 1 });
                                count++;
                            }
                            else
                            {
                                future.Add(new int[] { x - 2, y + 1, 0 });
                            }
                        }

                        //left down
                        if (y - 1 > -1 && x - 2 > -1)
                        {
                            if (king.X == x - 2 && king.Y == y - 1)
                            {
                                threats.Add(new int[] { x, y, 1 });
                                count++;
                            }
                            else
                            {
                                future.Add(new int[] { x - 2, y - 1, 0 });
                            }
                        }
                    }
                    else if (piece.Name == "bishop")
                    {
                        //x
                        int i;

                        //y
                        int j;

                        //lits of possible spaces the rook can go to kill the king
                        List<int[]> possible = new List<int[]>();

                        //adding bishop cords
                        possible.Add(new int[] { x, y, 1 });

                        //up right
                        for (i = x + 1, j = y - 1; i < 8; i++, j--)
                        {
                            if (j > 7 || j < 0)
                            {
                                break;
                            }

                            if (board.Rows[j].Cells[i].Value == null)
                            {
                                possible.Add(new int[] {i, j, 0});
                                future.Add(new int[] { i, j, 0 });
                            }
                            else if (king.X == i && king.Y == j)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] {i, j, 0});
                                count++;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null)
                            {
                                future.Add(new int[] { i, j, 0 });
                                break;
                            }
                        }

                        //if the king wasn't killable through the spaces above, it clears the possible tiles and checks the next direction
                        possible.Clear();

                        //adding rook cords
                        possible.Add(new int[] { x, y, 1 });

                        //up left
                        for (i = x - 1, j = y - 1; i > -1; i--, j--)
                        {
                            if (j > 7 || j < 0)
                            {
                                break;
                            }

                            if (board.Rows[j].Cells[i].Value == null)
                            {
                                possible.Add(new int[] { i, j, 0 });
                                future.Add(new int[] { i, j, 0 });
                            }
                            else if (king.X == i && king.Y == j)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { i, j, 0 });
                                count++;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null)
                            {
                                future.Add(new int[] { i, j, 0 });
                                break;
                            }
                        }

                        //if the king wasn't killable through the spaces above, it clears the possible tiles and checks the next direction
                        possible.Clear();

                        //adding rook cords
                        possible.Add(new int[] { x, y, 1 });

                        //down right
                        for (i = x + 1, j = y + 1; i < 8; i++, j++)
                        {
                            if (j > 7)
                            {
                                break;
                            }

                            if (board.Rows[j].Cells[i].Value == null)
                            {
                                possible.Add(new int[] { i, j, 0 });
                                future.Add(new int[] { i, j, 0 });
                            }
                            else if (king.X == i && king.Y == j)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { i, j, 0 });
                                count++;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null)
                            {
                                future.Add(new int[] { i, j, 0 });
                                break;
                            }
                        }

                        //if the king wasn't killable through the spaces above, it clears the possible tiles and checks the next direction
                        possible.Clear();

                        //adding rook cords
                        possible.Add(new int[] { x, y, 1 });

                        //down left
                        for (i = x - 1, j = y + 1; i > -1; i--, j++)
                        {
                            if (j > 7 || i < 0)
                            {
                                break;
                            }

                            if (board.Rows[j].Cells[i].Value == null)
                            {
                                possible.Add(new int[] { i, j, 0 });
                                future.Add(new int[] { i, j, 0 });
                            }
                            else if (king.X == i && king.Y == j)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { i, j, 0 });
                                count++;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null)
                            {
                                future.Add(new int[] { i, j, 0 });
                                break;
                            }
                        }
                        possible.Clear();
                    }
                    else if (piece.Name == "queen")
                    {
                        //x
                        int i;

                        //y
                        int j;

                        //lits of possible spaces the rook can go to kill the king
                        List<int[]> possible = new List<int[]>();

                        //adding bishop cords
                        possible.Add(new int[] { x, y, 1 });

                        //up left
                        for (i = x - 1, j = y - 1; i > -1; i--, j--)
                        {
                            if (j < 0)
                            {
                                break;
                            }

                            if (board.Rows[j].Cells[i].Value == null)
                            {
                                possible.Add(new int[] { i, j, 0 });
                                future.Add(new int[] { i, j, 0 });
                            }
                            else if (king.X == i && king.Y == j)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { i, j, 0 });
                                count++;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null)
                            {
                                future.Add(new int[] { i, j, 0 });
                                break;
                            }
                        }

                        //if the king wasn't killable through the spaces above, it clears the possible tiles and checks the next direction
                        possible.Clear();

                        //adding rook cords
                        possible.Add(new int[] { x, y, 1 });

                        //up right
                        for (i = x + 1, j = y - 1; i < 8; i++, j--)
                        {
                            if (j < 0)
                            {
                                break;
                            }

                            if (board.Rows[j].Cells[i].Value == null)
                            {
                                possible.Add(new int[] { i, j, 0 });
                                future.Add(new int[] { i, j, 0 });
                            }
                            else if (king.X == i && king.Y == j)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { i, j, 0 });
                                count++;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null)
                            {
                                future.Add(new int[] { i, j, 0 });
                                break;
                            }
                        }

                        //if the king wasn't killable through the spaces above, it clears the possible tiles and checks the next direction
                        possible.Clear();

                        //adding rook cords
                        possible.Add(new int[] { x, y, 1 });

                        //down right
                        for (i = x + 1, j = y + 1; i < 8; i++, j++)
                        {
                            if (j > 7)
                            {
                                break;
                            }

                            if (board.Rows[j].Cells[i].Value == null)
                            {
                                possible.Add(new int[] { i, j, 0 });
                                future.Add(new int[] { i, j, 0 });
                            }
                            else if (king.X == i && king.Y == j)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { i, j, 0 });
                                count++;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null)
                            {
                                future.Add(new int[] { i, j, 0 });
                                break;
                            }
                        }

                        //if the king wasn't killable through the spaces above, it clears the possible tiles and checks the next direction
                        possible.Clear();

                        //adding rook cords
                        possible.Add(new int[] { x, y, 1 });

                        //down left
                        for (i = x - 1, j = y + 1; i > -1; i--, j++)
                        {
                            if (j > 7)
                            {
                                break;
                            }

                            if (board.Rows[j].Cells[i].Value == null)
                            {
                                possible.Add(new int[] { i, j, 0 });
                                future.Add(new int[] { i, j, 0 });
                            }
                            else if (king.X == i && king.Y == j)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { i, j, 0 });
                                count++;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null)
                            {
                                future.Add(new int[] { x, i, 0 });
                                break;
                            }
                        }
                        possible.Clear();

                        //adding queen cords
                        possible.Add(new int[] { x, y, 1 });

                        //above (i represents y/row)
                        for (i = y - 1; i > -1; i--)
                        {
                            if (board.Rows[i].Cells[x].Value == null)
                            {
                                possible.Add(new int[] { x, i, 0 });
                                future.Add(new int[] { x, i, 0 });
                            }
                            else if (king.X == x && king.Y == i)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { x, i, 0 });
                                count++;
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null)
                            {
                                future.Add(new int[] { x, i, 0 });
                                break;
                            }
                        }

                        //if the king wasn't killable through the spaces above, it clears the possible tiles and checks the next direction
                        possible.Clear();

                        //adding rook cords
                        possible.Add(new int[] { x, y, 1 });

                        //below (i represents y/row)
                        for (i = y + 1; i < 8; i++)
                        {
                            if (board.Rows[i].Cells[x].Value == null)
                            {
                                possible.Add(new int[] { x, i, 0 });
                                future.Add(new int[] { x, i, 0 });
                            }
                            else if (king.X == x && king.Y == i)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { x, i, 0 });
                                count++;
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null)
                            {
                                future.Add(new int[] { x, i, 0 });
                                break;
                            }
                        }

                        //if the king wasn't killable through the spaces above, it clears the possible tiles and checks the next direction
                        possible.Clear();

                        //adding rook cords
                        possible.Add(new int[] { x, y, 1 });

                        //left (i represents x/column)
                        for (i = x - 1; i > -1; i--)
                        {
                            if (board.Rows[y].Cells[i].Value == null)
                            {
                                possible.Add(new int[] { i, y, 0 });
                                future.Add(new int[] { i, y, 0 });
                            }
                            else if (king.X == i && king.Y == y)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { i, y, 0 });
                                count++;
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null)
                            {
                                future.Add(new int[] { i, y, 0 });
                                break;
                            }
                        }

                        //if the king wasn't killable through the spaces above, it clears the possible tiles and checks the next direction
                        possible.Clear();

                        //adding rook cords
                        possible.Add(new int[] { x, y, 1 });

                        //right (i represents x/column)
                        for (i = x + 1; i < 8; i++)
                        {
                            if (board.Rows[y].Cells[i].Value == null)
                            {
                                possible.Add(new int[] { i, y, 0 });
                                future.Add(new int[] { i, y, 0 });
                            }
                            else if (king.X == i && king.Y == y)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { i, y, 0 });
                                count++;
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null)
                            {
                                future.Add(new int[] { i, y, 0 });
                                break;
                            }
                        }
                        possible.Clear();
                    }
                    else if (piece.Name == "king")
                    {
                        //right
                        if (x + 1 < 8)
                        {
                            if (board.Rows[y].Cells[x + 1].Value == null)
                            {
                                future.Add(new int[] { x + 1, y, 0});
                            }
                        }

                        //left
                        if (x - 1 > -1)
                        {
                            if (board.Rows[y].Cells[x - 1].Value == null)
                            {
                                future.Add(new int[] { x - 1, y, 0 });
                            }
                        }

                        //down
                        if (y + 1 < 8)
                        {
                            if (board.Rows[y + 1].Cells[x].Value == null)
                            {
                                future.Add(new int[] { x, y + 1, 0 });
                            }
                        }

                        //up
                        if (y - 1 > -1)
                        {
                            if (board.Rows[y - 1].Cells[x].Value == null)
                            {
                                future.Add(new int[] { x, y - 1, 0 });
                            }
                        }

                        //top right
                        if (x + 1 < 8 && y - 1 > -1)
                        {
                            if (board.Rows[y - 1].Cells[x + 1].Value == null)
                            {
                                future.Add(new int[] { x + 1, y - 1, 0 });
                            }
                        }

                        //top left
                        if (x - 1 > -1 && y - 1 > -1)
                        {
                            if (board.Rows[y - 1].Cells[x - 1].Value == null)
                            {
                                future.Add(new int[] { x + 1, y - 1, 0 });
                            }
                        }

                        //bottom right
                        if (x + 1 < 8 && y + 1 < 8)
                        {
                            if (board.Rows[y + 1].Cells[x + 1].Value == null)
                            {
                                future.Add(new int[] { x + 1, y + 1, 0 });
                            }
                        }

                        //bottom left
                        if (x - 1 > -1 && y + 1 < 8)
                        {
                            if (board.Rows[y + 1].Cells[x - 1].Value == null)
                            {
                                future.Add(new int[] { x - 1, y + 1, 0 });
                            }
                        }
                    }
                }
            }
            return new List<int[]>[] { threats, future, new List<int[]> { new int[] { count } } };
        }

        /// <summary>
        /// recolors the board to all grey and white spaces
        /// </summary>
        public void ogColor()
        {
            //ADD WHITE AND GREY SPACES
            foreach (DataGridViewRow row in board.Rows)
            {
                foreach (DataGridViewImageCell cell in row.Cells)
                {
                    if ((row.Index % 2) + 1 == 1)
                    {
                        if ((cell.ColumnIndex % 2) + 1 == 1)
                        {
                            cell.Style.BackColor = Color.White;
                            //MessageBox.Show(cell.RowIndex + "," + cell.ColumnIndex + " : changed to white");
                        }
                        else
                        {
                            cell.Style.BackColor = Color.Gray;
                            //MessageBox.Show(cell.RowIndex + "," + cell.ColumnIndex + " : changed to grey");
                        }
                    }
                    else
                    {
                        if ((cell.ColumnIndex % 2) + 1 == 1)
                        {
                            cell.Style.BackColor = Color.Gray;
                            //MessageBox.Show(cell.RowIndex + "," + cell.ColumnIndex + " : changed to grey");
                        }
                        else
                        {
                            cell.Style.BackColor = Color.White;
                            //MessageBox.Show(cell.RowIndex + "," + cell.ColumnIndex + " : changed to white");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// recolors a specific tile back to its original color
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void ogColor(int x, int y)
        {
            if ((y % 2) + 1 == 1)
            {
                if ((x % 2) + 1 == 1)
                {
                    board.Rows[y].Cells[x].Style.BackColor = Color.White;
                    //MessageBox.Show(cell.RowIndex + "," + cell.ColumnIndex + " : changed to white");
                }
                else
                {
                    board.Rows[y].Cells[x].Style.BackColor = Color.Gray;
                    //MessageBox.Show(cell.RowIndex + "," + cell.ColumnIndex + " : changed to grey");
                }
            }
            else
            {
                if ((x % 2) + 1 == 1)
                {
                    board.Rows[y].Cells[x].Style.BackColor = Color.Gray;
                    //MessageBox.Show(cell.RowIndex + "," + cell.ColumnIndex + " : changed to grey");
                }
                else
                {
                    board.Rows[y].Cells[x].Style.BackColor = Color.White;
                    //MessageBox.Show(cell.RowIndex + "," + cell.ColumnIndex + " : changed to white");
                }
            }
        }
        
        /// <summary>
        /// Highlight the spaces the piece can move to
        /// </summary>
        /// <param name="piece">the piece being moved</param>
        /// <param name="check">if the piece is on a team in check</param>
        public void highlightSpaces(Piece piece, bool check)
        {
            if(!check)
            {
                int x = piece.X;
                int y = piece.Y;
                if (piece.Name == "pawn" && ((piece.Color == Color.White && turn) || (piece.Color == Color.Black && !turn)))
                {
                    //left kill
                    if (x - 1 != -1 && y - 1 != -1 && board.Rows[y - 1].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y - 1) && !checkForACheck(piece, x - 1, y - 1,true))
                    {
                        board.Rows[y - 1].Cells[x - 1].Style.BackColor = Color.Red;
                    }

                    //right kill
                    if (x + 1 != 8 && y - 1 != -1 && board.Rows[y - 1].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y - 1) && !checkForACheck(piece,x+1,y-1,true))
                    {
                        board.Rows[y - 1].Cells[x + 1].Style.BackColor = Color.Red;
                    }

                    if (Pieces.Find(k => k.X == x && k.Y == y).Name == "pawn")
                    {
                        //left en Passant
                        if (x - 1 > -1 && y - 1 > -1 && board.Rows[y].Cells[x - 1].Value != null && board.Rows[y - 1].Cells[x - 1].Value == null && containsEnemy(piece.Color, x - 1, y) && !checkForACheck(piece, x - 1, y - 1, true) && doubleMove == true && lastMove.Item1 == x - 1 && lastMove.Item2 == y)
                        {
                            board.Rows[y - 1].Cells[x - 1].Style.BackColor = Color.Red;
                        }

                        //right en Passant
                        if (x + 1 < 8 && y - 1 > -1 && board.Rows[y].Cells[x + 1].Value != null && board.Rows[y - 1].Cells[x + 1].Value == null && containsEnemy(piece.Color, x + 1, y) && !checkForACheck(piece, x + 1, y - 1, true) && doubleMove == true && lastMove.Item1 == x + 1 && lastMove.Item2 == y)
                        {
                            board.Rows[y - 1].Cells[x + 1].Style.BackColor = Color.Red;
                        }
                    }
                    

                    //move 
                    if (y - 1 != -1 && board.Rows[y - 1].Cells[x].Value == null && !checkForACheck(piece, x, y - 1,false))
                    {
                        board.Rows[y - 1].Cells[x].Style.BackColor = Color.Blue;
                    }

                    //double move
                    if (y == 6 && board.Rows[y - 2].Cells[x].Value == null && !checkForACheck(piece, x, y - 2,false))
                    {
                        board.Rows[y - 2].Cells[x].Style.BackColor = Color.Blue;
                    }

                    //promotion
                    if (y-1 == 0 && board.Rows[y - 1].Cells[x].Value == null && !checkForACheck(piece, x, y - 1,false))
                    {
                        board.Rows[y - 1].Cells[x].Style.BackColor = Color.Purple;
                    }

                    //promotion & kill right
                    if (y - 1 == 0 && x + 1 < 8 && board.Rows[y - 1].Cells[x + 1].Value != null && containsEnemy(piece.Color,x + 1,y - 1) && !checkForACheck(piece, x + 1, y - 1, false))
                    {
                        board.Rows[y - 1].Cells[x + 1].Style.BackColor = Color.Purple;
                    }

                    //promotion & kill right
                    if (y - 1 == 0 && x - 1 > -1 && board.Rows[y - 1].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y - 1) && !checkForACheck(piece, x - 1, y - 1, false))
                    {
                        board.Rows[y - 1].Cells[x - 1].Style.BackColor = Color.Purple;
                    }
                }
                else if (piece.Name == "pawn" && ((piece.Color == Color.White && !turn) || (piece.Color == Color.Black && turn)))
                {
                    if (x + 1 < 8 && y + 1 < 8 && board.Rows[y + 1].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y + 1) && !checkForACheck(piece, x + 1, y + 1, true))
                    {
                        board.Rows[y + 1].Cells[x + 1].Style.BackColor = Color.Red;
                    }
                    if (x - 1 > -1 && y + 1 < 8 && board.Rows[y + 1].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y + 1) && !checkForACheck(piece, x - 1, y + 1, true))
                    {
                        board.Rows[y + 1].Cells[x - 1].Style.BackColor = Color.Red;
                    }
                    if (y + 1 < 8 && board.Rows[y + 1].Cells[x].Value == null && !checkForACheck(piece, x, y + 1, false))
                    {
                        board.Rows[y + 1].Cells[x].Style.BackColor = Color.Blue;
                    }
                    if (y == 1 && y+2 < 8 && board.Rows[y + 2].Cells[x].Value == null && !checkForACheck(piece, x, y + 2, false))
                    {
                        board.Rows[y + 2].Cells[x].Style.BackColor = Color.Blue;
                    }
                    if (y + 1 < 8 && board.Rows[y + 1].Cells[x].Value == null && !checkForACheck(piece, x, y + 1, false))
                    {
                        board.Rows[y + 1].Cells[x].Style.BackColor = Color.Purple;
                    }
                    if (y + 1 < 8 && x - 1 > -1 && board.Rows[y + 1].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y + 1) && !checkForACheck(piece, x - 1, y + 1, false))
                    {
                        board.Rows[y + 1].Cells[x - 1].Style.BackColor = Color.Purple;
                    }
                    if (y + 1 < 8 && x + 1 < 8 && board.Rows[y + 1].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y + 1) && !checkForACheck(piece, x + 1, y + 1, false))
                    {
                        board.Rows[y + 1].Cells[x + 1].Style.BackColor = Color.Purple;
                    }
                }
                else if (piece.Name == "rook" && ((piece.Color == Color.White && turn) || (piece.Color == Color.Black && !turn)))
                {
                    //above (i represents y/row)
                    for (int i = y - 1; i > -1; i--)
                    {
                        if (board.Rows[i].Cells[x].Value == null && !checkForACheck(piece, x, i, false))
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i) && !checkForACheck(piece, x, i,true))
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.Color, x, i))
                        {
                            break;
                        }
                    }

                    //below (i represents y/row)
                    for (int i = y + 1; i < 8; i++)
                    {
                        if (board.Rows[i].Cells[x].Value == null && !checkForACheck(piece, x, i, false))
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i) && !checkForACheck(piece, x, i,true))
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.Color, x, i))
                        {
                            break;
                        }
                    }

                    //left (i represents x/column)
                    for (int i = x - 1; i > -1; i--)
                    {
                        if (board.Rows[y].Cells[i].Value == null && !checkForACheck(piece, i, y, false))
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y) && !checkForACheck(piece, i, y,true))
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.Color, i, y))
                        {
                            break;
                        }
                    }

                    //right (i represents x/column)
                    for (int i = x + 1; i < 8; i++)
                    {
                        if (board.Rows[y].Cells[i].Value == null && !checkForACheck(piece, i, y, false))
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y) && !checkForACheck(piece, i, y,true))
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.Color, i, y))
                        {
                            break;
                        }
                    }
                }
                else if (piece.Name == "horse" && ((piece.Color == Color.White && turn) || (piece.Color == Color.Black && !turn)))
                {
                    //up left
                    if (y - 2 > -1 && x - 1 > -1 && board.Rows[y - 2].Cells[x - 1].Value == null && !checkForACheck(piece, x - 1, y - 2, false))
                    {
                        board.Rows[y - 2].Cells[x - 1].Style.BackColor = Color.Blue;
                    }
                    else if (y - 2 > -1 && x - 1 > -1 && board.Rows[y - 2].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y - 2) && !checkForACheck(piece, x - 1, y - 2,true))
                    {
                        board.Rows[y - 2].Cells[x - 1].Style.BackColor = Color.Red;
                    }

                    //up right
                    if (y - 2 > -1 && x + 1 < 8 && board.Rows[y - 2].Cells[x + 1].Value == null && !checkForACheck(piece, x + 1, y - 2, false))
                    {
                        board.Rows[y - 2].Cells[x + 1].Style.BackColor = Color.Blue;
                    }
                    else if (y - 2 > -1 && x + 1 < 8 && board.Rows[y - 2].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y - 2) && !checkForACheck(piece, x + 1, y - 2,true))
                    {
                        board.Rows[y - 2].Cells[x + 1].Style.BackColor = Color.Red;
                    }

                    //right up
                    if (y - 1 > -1 && x + 2 < 8 && board.Rows[y - 1].Cells[x + 2].Value == null && !checkForACheck(piece, x + 2, y - 1, false))
                    {
                        board.Rows[y - 1].Cells[x + 2].Style.BackColor = Color.Blue;
                    }
                    else if (y - 1 > -1 && x + 2 < 8 && board.Rows[y - 1].Cells[x + 2].Value != null && containsEnemy(piece.Color, x + 2, y - 1) && !checkForACheck(piece, x + 2, y - 1,true))
                    {
                        board.Rows[y - 1].Cells[x + 2].Style.BackColor = Color.Red;
                    }

                    //right down
                    if (y + 1 < 8 && x + 2 < 8 && board.Rows[y + 1].Cells[x + 2].Value == null && !checkForACheck(piece, x + 2, y + 1, false))
                    {
                        board.Rows[y + 1].Cells[x + 2].Style.BackColor = Color.Blue;
                    }
                    else if (y + 1 < 8 && x + 2 < 8 && board.Rows[y + 1].Cells[x + 2].Value != null && containsEnemy(piece.Color, x + 2, y + 1) && !checkForACheck(piece, x + 2, y + 1,true))
                    {
                        board.Rows[y + 1].Cells[x + 2].Style.BackColor = Color.Red;
                    }

                    //down left
                    if (y + 2 < 8 && x - 1 > -1 && board.Rows[y + 2].Cells[x - 1].Value == null && !checkForACheck(piece, x - 1, y + 2, false))
                    {
                        board.Rows[y + 2].Cells[x - 1].Style.BackColor = Color.Blue;
                    }
                    else if (y + 2 < 8 && x - 1 > -1 && board.Rows[y + 2].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y + 2) && !checkForACheck(piece, x - 1, y + 2,true))
                    {
                        board.Rows[y + 2].Cells[x - 1].Style.BackColor = Color.Red;
                    }

                    //down right
                    if (y + 2 < 8 && x + 1 < 8 && board.Rows[y + 2].Cells[x + 1].Value == null && !checkForACheck(piece, x + 1, y + 2, false))
                    {
                        board.Rows[y + 2].Cells[x + 1].Style.BackColor = Color.Blue;
                    }
                    else if (y + 2 < 8 && x + 1 < 8 && board.Rows[y + 2].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y + 2) && !checkForACheck(piece, x + 1, y + 2,true))
                    {
                        board.Rows[y + 2].Cells[x + 1].Style.BackColor = Color.Red;
                    }

                    //left up
                    if (y + 1 < 8 && x - 2 > -1 && board.Rows[y + 1].Cells[x - 2].Value == null && !checkForACheck(piece, x - 2, y + 1, false))
                    {
                        board.Rows[y + 1].Cells[x - 2].Style.BackColor = Color.Blue;
                    }
                    else if (y + 1 < 8 && x - 2 > -1 && board.Rows[y + 1].Cells[x - 2].Value != null && containsEnemy(piece.Color, x - 2, y + 1) && !checkForACheck(piece, x - 2, y + 1,true))
                    {
                        board.Rows[y + 1].Cells[x - 2].Style.BackColor = Color.Red;
                    }

                    //left down
                    if (y - 1 > -1 && x - 2 > -1 && board.Rows[y - 1].Cells[x - 2].Value == null && !checkForACheck(piece, x - 2, y - 1, false))
                    {
                        board.Rows[y - 1].Cells[x - 2].Style.BackColor = Color.Blue;
                    }
                    else if (y - 1 > -1 && x - 2 > -1 && board.Rows[y - 1].Cells[x - 2].Value != null && containsEnemy(piece.Color, x - 2, y - 1) && !checkForACheck(piece, x - 2, y - 1,true))
                    {
                        board.Rows[y - 1].Cells[x - 2].Style.BackColor = Color.Red;
                    }
                }
                else if (piece.Name == "bishop" && ((piece.Color == Color.White && turn) || (piece.Color == Color.Black && !turn)))
                {
                    int i;
                    int j;

                    //up right
                    for (i = x + 1, j = y - 1; i < 8; i++, j--)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null && !checkForACheck(piece, i, j, false))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && !checkForACheck(piece, i, j,true))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //up left
                    for (i = x - 1, j = y - 1; i > -1; i--, j--)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null && !checkForACheck(piece, i, j, false))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && !checkForACheck(piece, i, j,true))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //down right
                    for (i = x + 1, j = y + 1; i < 8; i++, j++)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null && !checkForACheck(piece, i, j, false))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && !checkForACheck(piece, i, j,true))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //down left
                    for (i = x - 1, j = y + 1; i > -1; i--, j++)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null && !checkForACheck(piece, i, j, false))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && !checkForACheck(piece, i, j,true))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }
                }
                else if (piece.Name == "queen" && ((piece.Color == Color.White && turn) || (piece.Color == Color.Black && !turn)))
                {
                    int i;
                    int j;

                    //up right
                    for (i = x + 1, j = y - 1; i < 8; i++, j--)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null && !checkForACheck(piece, i, j, false))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && !checkForACheck(piece, i, j,true))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //up left
                    for (i = x - 1, j = y - 1; i > -1; i--, j--)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null && !checkForACheck(piece, i, j, false))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && !checkForACheck(piece, i, j,true))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //down right
                    for (i = x + 1, j = y + 1; i < 8; i++, j++)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null && !checkForACheck(piece, i, j, false))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && !checkForACheck(piece, i, j,true))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //down left
                    for (i = x - 1, j = y + 1; i > -1; i--, j++)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null && !checkForACheck(piece, i, j, false))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && !checkForACheck(piece, i, j,true))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //above (i represents y/row)
                    for (i = y - 1; i > -1; i--)
                    {
                        if (board.Rows[i].Cells[x].Value == null && !checkForACheck(piece, x, i, false))
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i) && !checkForACheck(piece, x, i,true))
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.Color, x, i))
                        {
                            break;
                        }
                    }

                    //below (i represents y/row)
                    for (i = y + 1; i < 8; i++)
                    {
                        if (board.Rows[i].Cells[x].Value == null && !checkForACheck(piece, x, i, false))
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i) && !checkForACheck(piece, x, i,true))
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.Color, x, i))
                        {
                            break;
                        }
                    }

                    //left (i represents x/column)
                    for (i = x - 1; i > -1; i--)
                    {
                        if (board.Rows[y].Cells[i].Value == null && !checkForACheck(piece, i, y, false))
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y) && !checkForACheck(piece, i, y,true))
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.Color, i, y))
                        {
                            break;
                        }
                    }

                    //right (i represents x/column)
                    for (i = x + 1; i < 8; i++)
                    {
                        if (board.Rows[y].Cells[i].Value == null && !checkForACheck(piece, i, y, false))
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y) && !checkForACheck(piece, i, y,true))
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.Color, i, y))
                        {
                            break;
                        }
                    }
                }
                else if (piece.Name == "king" && ((piece.Color == Color.White && turn) || (piece.Color == Color.Black && !turn)))
                {
                    List<int[]> future = getCheckTiles(piece.Color == Color.White ? Color.Black:Color.White)[1];
                    //right
                    if (x + 1 < 8)
                    {
                        bool covered = false;
                        //make sure the king cant go on a space covered by the enemy
                        foreach (int[] tile in future)
                        {
                            if (x+1 == tile[0] && y == tile[1])
                            {
                                covered = true;
                            }
                        }
                        if (!covered)
                        {
                            if (board.Rows[y].Cells[x + 1].Value == null)
                            {
                                board.Rows[y].Cells[x + 1].Style.BackColor = Color.Blue;
                            }
                            else if (board.Rows[y].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y))
                            {
                                board.Rows[y].Cells[x + 1].Style.BackColor = Color.Red;
                            }
                        }
                        
                    }

                    //left
                    if (x - 1 > -1)
                    {
                        bool covered = false;
                        //make sure the king cant go on a space covered by the enemy
                        foreach (int[] tile in future)
                        {
                            if (x - 1 == tile[0] && y == tile[1])
                            {
                                covered = true;
                            }
                        }
                        if (!covered)
                        {
                            if (board.Rows[y].Cells[x - 1].Value == null)
                            {
                                board.Rows[y].Cells[x - 1].Style.BackColor = Color.Blue;
                            }
                            else if (board.Rows[y].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y))
                            {
                                board.Rows[y].Cells[x - 1].Style.BackColor = Color.Red;
                            }
                        }
                        
                    }

                    //down
                    if (y + 1 < 8)
                    {
                        bool covered = false;
                        //make sure the king cant go on a space covered by the enemy
                        foreach (int[] tile in future)
                        {
                            if (x == tile[0] && y + 1 == tile[1])
                            {
                                covered = true;
                            }
                        }
                        if (!covered)
                        {
                            if (board.Rows[y + 1].Cells[x].Value == null)
                            {
                                board.Rows[y + 1].Cells[x].Style.BackColor = Color.Blue;
                            }
                            else if (board.Rows[y + 1].Cells[x].Value != null && containsEnemy(piece.Color, x, y + 1))
                            {
                                board.Rows[y + 1].Cells[x].Style.BackColor = Color.Red;
                            }
                        }
                        
                    }

                    //up
                    if (y - 1 > -1)
                    {
                        bool covered = false;
                        //make sure the king cant go on a space covered by the enemy
                        foreach (int[] tile in future)
                        {
                            if (x == tile[0] && y - 1 == tile[1])
                            {
                                covered = true;
                            }
                        }
                        if (!covered)
                        {
                            if (board.Rows[y - 1].Cells[x].Value == null)
                            {
                                board.Rows[y - 1].Cells[x].Style.BackColor = Color.Blue;
                            }
                            else if (board.Rows[y - 1].Cells[x].Value != null && containsEnemy(piece.Color, x, y - 1))
                            {
                                board.Rows[y - 1].Cells[x].Style.BackColor = Color.Red;
                            }
                        }
                        
                    }

                    //top right
                    if (x + 1 < 8 && y - 1 > -1)
                    {
                        bool covered = false;
                        //make sure the king cant go on a space covered by the enemy
                        foreach (int[] tile in future)
                        {
                            if (x + 1 == tile[0] && y - 1 == tile[1])
                            {
                                covered = true;
                            }
                        }
                        if (!covered)
                        {
                            if (board.Rows[y - 1].Cells[x + 1].Value == null)
                            {
                                board.Rows[y - 1].Cells[x + 1].Style.BackColor = Color.Blue;
                            }
                            else if (board.Rows[y - 1].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y - 1))
                            {
                                board.Rows[y - 1].Cells[x + 1].Style.BackColor = Color.Red;
                            }
                        }
                       
                    }

                    //top left
                    if (x - 1 > -1 && y - 1 > -1)
                    {
                        bool covered = false;
                        //make sure the king cant go on a space covered by the enemy
                        foreach (int[] tile in future)
                        {
                            if (x - 1 == tile[0] && y - 1 == tile[1])
                            {
                                covered = true;
                            }
                        }
                        if (!covered)
                        {
                            if (board.Rows[y - 1].Cells[x - 1].Value == null)
                            {
                                board.Rows[y - 1].Cells[x - 1].Style.BackColor = Color.Blue;
                            }
                            else if (board.Rows[y - 1].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y - 1))
                            {
                                board.Rows[y - 1].Cells[x - 1].Style.BackColor = Color.Red;
                            }
                        }
                        
                    }

                    //bottom right
                    if (x + 1 < 8 && y + 1 < 8)
                    {
                        bool covered = false;
                        //make sure the king cant go on a space covered by the enemy
                        foreach (int[] tile in future)
                        {
                            if (x + 1 == tile[0] && y + 1 == tile[1])
                            {
                                covered = true;
                            }
                        }
                        if (!covered)
                        {
                            if (board.Rows[y + 1].Cells[x + 1].Value == null)
                            {
                                board.Rows[y + 1].Cells[x + 1].Style.BackColor = Color.Blue;
                            }
                            else if (board.Rows[y + 1].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y + 1))
                            {
                                board.Rows[y + 1].Cells[x + 1].Style.BackColor = Color.Red;
                            }
                        }
                        
                    }

                    //bottom left
                    if (x - 1 > -1 && y + 1 < 8)
                    {
                        bool covered = false;
                        //make sure the king cant go on a space covered by the enemy
                        foreach (int[] tile in future)
                        {
                            if (x - 1 == tile[0] && y + 1 == tile[1])
                            {
                                covered = true;
                            }
                        }
                        if (!covered)
                        {
                            if (board.Rows[y + 1].Cells[x - 1].Value == null)
                            {
                                board.Rows[y + 1].Cells[x - 1].Style.BackColor = Color.Blue;
                            }
                            else if (board.Rows[y + 1].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y + 1))
                            {
                                board.Rows[y + 1].Cells[x - 1].Style.BackColor = Color.Red;
                            }
                        }   
                    }
                    //CASTLE
                    //check if white or black
                    if (piece.Color == Color.White)
                    {
                        //if king has not moved and the spaces between him and the rook are check free
                        if (!piece.Moved)
                        {
                            //KINGSIDE CASTLE
                            bool castle = true;

                            //find kingside rook
                            Piece kingRook = new Piece();
                            foreach (Piece findRook in Pieces)
                            {
                                if (findRook.Name == "rook" && findRook.Color == piece.Color && !findRook.Moved && findRook.X == 7)
                                {
                                    kingRook = findRook;
                                }
                            }

                            //check if rook was found successfully and the spots between king and rook are empty
                            if (kingRook.Name == "rook" && board.Rows[y].Cells[piece.X + 1].Value == null && board.Rows[y].Cells[piece.X + 2].Value == null)
                            {
                                //check each future move of the enemy to see if spaces in between king and rook are not in check
                                foreach (int[] tile in future)
                                {
                                    //if the 2 spots are checkable, castle is illegal
                                    if ((piece.X + 1 == tile[0] && piece.Y == tile[1]) || (piece.X + 2 == tile[0] && piece.Y == tile[1]))
                                    {
                                        castle = false;
                                    }
                                }

                                if (castle)
                                {
                                    board.Rows[kingRook.Y].Cells[kingRook.X].Style.BackColor = Color.Green;
                                }
                            }


                            //QUEENSIDE CASTLE
                            castle = true;

                            //find kingside rook
                            Piece queenRook = new Piece();
                            foreach (Piece findRook in Pieces)
                            {
                                if (findRook.Name == "rook" && findRook.Color == piece.Color && !findRook.Moved && findRook.X == 0)
                                {
                                    queenRook = findRook;
                                }
                            }

                            //check if rook was found successfully and the spots between king and rook are empty
                            if (queenRook.Name == "rook" && board.Rows[y].Cells[piece.X - 1].Value == null && board.Rows[y].Cells[piece.X - 2].Value == null && board.Rows[y].Cells[piece.X - 3].Value == null)
                            {
                                //check each future move of the enemy to see if spaces in between king and rook are not in check
                                foreach (int[] tile in future)
                                {
                                    //if the 2 spots are checkable, castle is illegal
                                    if ((piece.X - 1 == tile[0] && piece.Y == tile[1]) || (piece.X - 2 == tile[0] && piece.Y == tile[1]))
                                    {
                                        castle = false;
                                    }
                                }

                                if (castle)
                                {
                                    board.Rows[queenRook.Y].Cells[queenRook.X].Style.BackColor = Color.Green;
                                }
                            }
                        }
                    }
                    else
                    {
                        //if king has not moved and the spaces between him and the rook are check free
                        if (!piece.Moved)
                        {
                            //KINGSIDE CASTLE
                            bool castle = true;

                            //find kingside rook
                            Piece kingRook = new Piece();
                            foreach (Piece findRook in Pieces)
                            {
                                if (findRook.Name == "rook" && findRook.Color == piece.Color && !findRook.Moved && findRook.X == 0)
                                {
                                    kingRook = findRook;
                                }
                            }

                            //check if rook was found successfully and the spots between king and rook are empty
                            if (kingRook.Name == "rook" && board.Rows[y].Cells[piece.X - 1].Value == null && board.Rows[y].Cells[piece.X - 2].Value == null)
                            {
                                //check each future move of the enemy to see if spaces in between king and rook are not in check
                                foreach (int[] tile in future)
                                {
                                    //if the 2 spots are checkable, castle is illegal
                                    if ((piece.X - 1 == tile[0] && piece.Y == tile[1]) || (piece.X - 2 == tile[0] && piece.Y == tile[1]))
                                    {
                                        castle = false;
                                    }
                                }

                                if (castle)
                                {
                                    board.Rows[kingRook.Y].Cells[kingRook.X].Style.BackColor = Color.Green;
                                }
                            }


                            //QUEENSIDE CASTLE
                            castle = true;

                            //find kingside rook
                            Piece queenRook = new Piece();
                            foreach (Piece findRook in Pieces)
                            {
                                if (findRook.Name == "rook" && findRook.Color == piece.Color && !findRook.Moved && findRook.X == 7)
                                {
                                    queenRook = findRook;
                                }
                            }

                            //check if rook was found successfully and the spots between king and rook are empty
                            if (queenRook.Name == "rook" && board.Rows[y].Cells[piece.X + 1].Value == null && board.Rows[y].Cells[piece.X + 2].Value == null && board.Rows[y].Cells[piece.X + 3].Value == null)
                            {
                                //check each future move of the enemy to see if spaces in between king and rook are not in check
                                foreach (int[] tile in future)
                                {
                                    //if the 2 spots are checkable, castle is illegal
                                    if ((piece.X + 1 == tile[0] && piece.Y == tile[1]) || (piece.X + 2 == tile[0] && piece.Y == tile[1]))
                                    {
                                        castle = false;
                                    }
                                }

                                if (castle)
                                {
                                    board.Rows[queenRook.Y].Cells[queenRook.X].Style.BackColor = Color.Green;
                                }
                            }
                        }
                    }


                }
            }
            else
            {
                List<int[]>[] getCheck = getCheckTiles(piece.Color == Color.White ? Color.Black:Color.White);
                List<int[]> threats = getCheck[0];
                List<int[]> future = getCheck[1];

                int x = piece.X;
                int y = piece.Y;

                if (piece.Name == "king")
                {
                    int newy = y - 1;
                    //up
                    if (newy > -1 && newy < 8 && board.Rows[newy].Cells[x].Value == null)
                    {
                        bool open = true;
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == x && tile[1] == newy && tile[2] == 0)
                            {
                                open = false;
                                break;
                            }
                        }
                        foreach (int[] tile in future)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == x && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[newy].Cells[x].Style.BackColor = Color.Blue;
                        }
                    }
                    //if an enemy is on that tile
                    else if (newy > -1 && newy < 8 && board.Rows[newy].Cells[x].Value != null && containsEnemy(piece.Color, x, newy))
                    {
                        bool open = true;
                        foreach (int[] tile in future)
                        {
                            //if that tile is also covered by another enemy
                            if (tile[0] == x && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == x && tile[1] == newy && tile[2] == 0)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[newy].Cells[x].Style.BackColor = Color.Red;
                        }
                    }

                    newy = y + 1;
                    //down
                    if (newy < 8 && newy > -1 && board.Rows[newy].Cells[x].Value == null)
                    {
                        bool open = true;
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == x && tile[1] == newy && tile[2] == 0)
                            {
                                open = false;
                                break;
                            }
                        }
                        foreach (int[] tile in future)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == x && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[y + 1].Cells[x].Style.BackColor = Color.Blue;
                        }
                    }
                    //if an enemy is on that tile
                    else if (newy < 8 && newy > -1 && board.Rows[newy].Cells[x].Value != null && containsEnemy(piece.Color, x, newy))
                    {
                        bool open = true;
                        foreach (int[] tile in future)
                        {
                            //if that tile is also covered by another enemy
                            if (tile[0] == x && tile[1] == newy)
                            {
                                open = false;
                            }
                        }
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == x && tile[1] == newy && tile[2] == 0)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[newy].Cells[x].Style.BackColor = Color.Red;
                        }
                    }

                    int newx = x - 1;
                    //left
                    if (newx > -1 && newx < 8 && board.Rows[y].Cells[newx].Value == null)
                    {
                        bool open = true;
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == y && tile[2] == 0)
                            {
                                open = false;
                                break;
                            }
                        }
                        foreach (int[] tile in future)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == y)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[y].Cells[newx].Style.BackColor = Color.Blue;
                        }
                    }
                    //if an enemy is on that tile
                    else if (newx > -1 && newx < 8 && board.Rows[y].Cells[newx].Value != null && containsEnemy(piece.Color, newx, y))
                    {
                        bool open = true;
                        foreach (int[] tile in future)
                        {
                            //if that tile is also covered by another enemy
                            if (tile[0] == newx && tile[1] == y)
                            {
                                open = false;
                            }
                        }
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == y && tile[2] == 0)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[y].Cells[newx].Style.BackColor = Color.Red;
                        }
                    }

                    newx = x + 1;
                    //right
                    if (newx < 8 && newx > -1 && board.Rows[y].Cells[newx].Value == null)
                    {
                        bool open = true;
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == y && tile[2] == 0)
                            {
                                open = false;
                                break;
                            }
                        }
                        foreach (int[] tile in future)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == y)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[y].Cells[newx].Style.BackColor = Color.Blue;
                        }
                    }
                    //if an enemy is on that tile
                    else if (newx < 8 && newx > -1 && board.Rows[y].Cells[newx].Value != null && containsEnemy(piece.Color, newx, y))
                    {
                        bool open = true;
                        foreach (int[] tile in future)
                        {
                            //if that tile is also covered by another enemy
                            if (tile[0] == newx && tile[1] == y)
                            {
                                open = false;
                            }
                        }
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == y && tile[2] == 0)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[y].Cells[newx].Style.BackColor = Color.Red;
                        }
                    }

                    newx = x - 1;
                    newy = y - 1;
                    //up left
                    if (newx > -1 && newy > -1 && newx < 8 && newy < 8 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        bool open = true;
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy && tile[2] == 0)
                            {
                                open = false;
                                break;
                            }
                        }
                        foreach (int[] tile in future)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                        }
                    }
                    //if an enemy is on that tile
                    else if (newx > -1 && newy > -1 && newx < 8 && newy < 8 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        bool open = true;
                        foreach (int[] tile in future)
                        {
                            //if that tile is also covered by another enemy
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                            }
                        }
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy && tile[2] == 0)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                        }
                    }

                    newx = x + 1;
                    newy = y - 1;
                    //up right
                    if (newx < 8 && newy > -1 && newx > -1 && newy < 8 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        bool open = true;
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy && tile[2] == 0)
                            {
                                open = false;
                                break;
                            }
                        }
                        foreach (int[] tile in future)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                        }
                    }
                    //if an enemy is on that tile
                    else if (newx < 8 && newy > -1 && newx > -1 && newy < 8 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        bool open = true;
                        foreach (int[] tile in future)
                        {
                            //if that tile is also covered by another enemy
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                            }
                        }
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy && tile[2] == 0)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                        }
                    }

                    newx = x - 1;
                    newy = y + 1;
                    //down left
                    if (newx > -1 && newy < 8 && newx < 8 && newy > -1 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        bool open = true;
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy && tile[2] == 0)
                            {
                                open = false;
                                break;
                            }
                        }
                        foreach (int[] tile in future)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                        }
                    }
                    //if an enemy is on that tile
                    else if (newx > -1 && newy < 8 && newx < 8 && newy > -1 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        bool open = true;
                        foreach (int[] tile in future)
                        {
                            //if that tile is also covered by another enemy
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                            }
                        }
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy && tile[2] == 0)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                        }
                    }

                    newx = x + 1;
                    newy = y + 1;
                    //down right
                    if (newx < 8 && newy < 8 && newx > -1 && newy > -1 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        bool open = true;
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy && tile[2] == 0)
                            {
                                open = false;
                                break;
                            }
                        }
                        foreach (int[] tile in future)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                        }
                    }
                    //if an enemy is on that tile
                    else if (newx < 8 && newy < 8 && newx > -1 && newy > -1 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        bool open = true;
                        foreach (int[] tile in future)
                        {
                            //if that tile is also covered by another enemy
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                            }
                        }
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy && tile[2] == 0)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                        }
                    }
                }
                else if (piece.Name == "pawn")
                {
                    int newy = y - 1;
                    //up
                    if (newy > -1 && board.Rows[newy].Cells[x].Value == null)
                    {
                        foreach (int[] tile in threats)
                        {
                            //if the tile is threatening the king, the piece can move to block that threat
                            if (tile[0] == x && tile[1] == newy && tile[2] == 0)
                            {
                                board.Rows[newy].Cells[x].Style.BackColor = Color.Blue;
                                break;
                            }
                        }
                    }

                    newy = y - 2;
                    //up 2
                    if (y == 6 && board.Rows[newy].Cells[x].Value == null)
                    {
                        foreach (int[] tile in threats)
                        {
                            //if the tile is threatening the king, the piece can move to block that threat
                            if (tile[0] == x && tile[1] == newy && tile[2] == 0)
                            {
                                board.Rows[newy].Cells[x].Style.BackColor = Color.Blue;
                                break;
                            }
                        }
                    }

                    newy = y - 1;
                    int newx = x - 1;
                    //up left
                    if (newy > -1 && newx > -1 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(Color.White, newx, newy))
                    {
                        foreach (int[] tile in threats)
                        {
                            //if the tile is threatening the king, the piece can move to block that threat
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                                break;
                            }
                        }
                    }

                    newy = y - 1;
                    newx = x + 1;
                    //up right
                    if (newy > -1 && newx < 8 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(Color.White, newx, newy))
                    {
                        foreach (int[] tile in threats)
                        {
                            //if the tile is threatening the king, the piece can move to block that threat
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                                break;
                            }
                        }
                    }

                    if (Pieces.Find(k => k.X == x - 1 && k.Y == y).Name == "pawn")
                    {
                        //left en Passant
                        if (x - 1 > -1 && y - 1 > -1 && board.Rows[y].Cells[x - 1].Value != null && board.Rows[y - 1].Cells[x - 1].Value == null && containsEnemy(piece.Color, x - 1, y) && !checkForACheck(piece, x - 1, y - 1, true) && doubleMove == true && lastMove.Item1 == x - 1 && lastMove.Item2 == y)
                        {
                            board.Rows[y - 1].Cells[x - 1].Style.BackColor = Color.Red;
                        }

                        //right en Passant
                        if (x + 1 < 8 && y - 1 > -1 && board.Rows[y].Cells[x + 1].Value != null && board.Rows[y - 1].Cells[x + 1].Value == null && containsEnemy(piece.Color, x + 1, y) && !checkForACheck(piece, x + 1, y - 1, true) && doubleMove == true && lastMove.Item1 == x + 1 && lastMove.Item2 == y)
                        {
                            board.Rows[y - 1].Cells[x + 1].Style.BackColor = Color.Red;
                        }
                    }
                }
                else if (piece.Name == "rook")
                {
                    //above (i represents y/row)
                    for (int i = y - 1; i > -1; i--)
                    {
                        if (board.Rows[i].Cells[x].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                                    break;
                                }
                            }
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.Color, x, i))
                        {
                            break;
                        }
                    }

                    //below (i represents y/row)
                    for (int i = y + 1; i < 8; i++)
                    {
                        if (board.Rows[i].Cells[x].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                                    break;
                                }
                            }
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.Color, x, i))
                        {
                            break;
                        }
                    }

                    //left (i represents x/column)
                    for (int i = x - 1; i > -1; i--)
                    {
                        if (board.Rows[y].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == y)
                                {
                                    board.Rows[y].Cells[i].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == y)
                                {
                                    board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                                    break;
                                }
                            }
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.Color, i, y))
                        {
                            break;
                        }
                    }

                    //right (i represents x/column)
                    for (int i = x + 1; i < 8; i++)
                    {
                        if (board.Rows[y].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == y)
                                {
                                    board.Rows[y].Cells[i].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == y)
                                {
                                    board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                                    break;
                                }
                            }
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.Color, i, y))
                        {
                            break;
                        }
                    }

                }
                else if (piece.Name == "horse")
                {
                    int newx = x - 1;
                    int newy = y - 2;
                    //up left
                    if (newy > -1 && newx > -1 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                            }
                        }
                    }
                    else if (newy > -1 && newx > -1 && board.Rows[newy].Cells[x - 1].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                            }
                        }
                    }

                    newx = x + 1;
                    newy = y - 2;
                    //up right
                    if (newy > -1 && newx < 8 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                            }
                        }
                    }
                    else if (newy > -1 && newx < 8 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                            }
                        }
                    }

                    newx = x + 2;
                    newy = y - 1;
                    //right up
                    if (newy > -1 && newx < 8 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                            }
                        }
                    }
                    else if (newy > -1 && newx < 8 && board.Rows[newy].Cells[x - 1].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                            }
                        }
                    }

                    newx = x + 2;
                    newy = y + 1;
                    //right down
                    if (newy < 8 && newx < 8 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                            }
                        }
                    }
                    else if (newy < 8 && newx < 8 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                            }
                        }
                    }

                    newx = x + 1;
                    newy = y + 2;
                    //down right
                    if (newy < 8 && newx < 8 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                            }
                        }
                    }
                    else if (newy < 8 && newx < 8 && board.Rows[newy].Cells[x - 1].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                            }
                        }
                    }

                    newx = x - 1;
                    newy = y + 2;
                    //down left
                    if (newy < 8 && newx > -1 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                            }
                        }
                    }
                    else if (newy < 8 && newx > -1 && board.Rows[newy].Cells[x - 1].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                            }
                        }
                    }

                    newx = x - 2;
                    newy = y + 1;
                    //left down
                    if (newy < 8 && newx > -1 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                            }
                        }
                    }
                    else if (newy < 8 && newx > -1 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                            }
                        }
                    }

                    newx = x - 2;
                    newy = y - 1;
                    //left up
                    if (newy > -1 && newx > -1 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                            }
                        }
                    }
                    else if (newy > -1 && newx > -1 && board.Rows[newy].Cells[x - 1].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                            }
                        }
                    }
                }
                else if (piece.Name == "bishop")
                {
                    int i;
                    int j;
                    //up right
                    for (i = x + 1, j = y - 1; i < 8; i++, j--)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == j)
                                {
                                    board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == j)
                                {
                                    board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                                }
                            }
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //down right
                    for (i = x + 1, j = y + 1; i < 8; i++, j++)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == j)
                                {
                                    board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == j)
                                {
                                    board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                                }
                            }
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //down left
                    for (i = x - 1, j = y + 1; i > -1; i--, j++)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == j)
                                {
                                    board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == j)
                                {
                                    board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                                }
                            }
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //up left
                    for (i = x - 1, j = y - 1; i > -1; i--, j--)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == j)
                                {
                                    board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == j)
                                {
                                    board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                                }
                            }
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }
                }
                else if (piece.Name == "queen")
                {
                    int i;
                    int j;
                    //up right
                    for (i = x + 1, j = y - 1; i < 8; i++, j--)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                                }
                            }
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //down right
                    for (i = x + 1, j = y + 1; i < 8; i++, j++)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                                }
                            }
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //down left
                    for (i = x - 1, j = y + 1; i > -1; i--, j++)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                                }
                            }
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //up left
                    for (i = x - 1, j = y - 1; i > -1; i--, j--)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                                }
                            }
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //above (i represents y/row)
                    for (i = y - 1; i > -1; i--)
                    {
                        if (board.Rows[i].Cells[x].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                                    break;
                                }
                            }
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.Color, x, i))
                        {
                            break;
                        }
                    }

                    //below (i represents y/row)
                    for (i = y + 1; i < 8; i++)
                    {
                        if (board.Rows[i].Cells[x].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                                    break;
                                }
                            }
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.Color, x, i))
                        {
                            break;
                        }
                    }

                    //left (i represents x/column)
                    for (i = x - 1; i > -1; i--)
                    {
                        if (board.Rows[y].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == y)
                                {
                                    board.Rows[y].Cells[i].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == y)
                                {
                                    board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                                    break;
                                }
                            }
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.Color, i, y))
                        {
                            break;
                        }
                    }

                    //right (i represents x/column)
                    for (i = x + 1; i > 8; i++)
                    {
                        if (board.Rows[y].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == y)
                                {
                                    board.Rows[y].Cells[i].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == y)
                                {
                                    board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                                    break;
                                }
                            }
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.Color, i, y))
                        {
                            break;
                        }
                    }
                }
            }

            //idk why this is here. Uncomment this if you need it later
            //foreach (DataGridViewRow row in board.Rows)
            //{
            //    foreach (DataGridViewCell cell in row.Cells)
            //    {
            //        if (cell.Style.BackColor == Color.Blue)
            //        {
            //            ogColor(cell.ColumnIndex, cell.RowIndex);
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Highlight the spaces the piece can move to
        /// </summary>
        /// <param name="piece">the piece being moved</param>
        /// <param name="check">if the piece is on a team in check</param>
        public void oldHighlightSpaces(Piece piece, bool check)
        {
            if (!check)
            {
                int x = piece.X;
                int y = piece.Y;
                if (piece.Name == "pawn" && ((piece.Color == Color.White && !turn) || (piece.Color == Color.Black && turn)))
                {
                    if (x - 1 != -1 && y - 1 != -1 && board.Rows[y - 1].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y - 1))
                    {
                        board.Rows[y - 1].Cells[x - 1].Style.BackColor = Color.Red;
                    }
                    if (x + 1 != 8 && y - 1 != -1 && board.Rows[y - 1].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y - 1))
                    {
                        board.Rows[y - 1].Cells[x + 1].Style.BackColor = Color.Red;
                    }
                    if (y - 1 != -1 && board.Rows[y - 1].Cells[x].Value == null)
                    {
                        board.Rows[y - 1].Cells[x].Style.BackColor = Color.Blue;
                    }
                    if (y == 6 && board.Rows[y - 2].Cells[x].Value == null)
                    {
                        board.Rows[y - 2].Cells[x].Style.BackColor = Color.Blue;
                    }
                    if (y + 1 == 8 && board.Rows[y + 1].Cells[x].Value == null)
                    {
                        board.Rows[y + 1].Cells[x].Style.BackColor = Color.Purple;
                    }
                }
                else if (piece.Name == "rook" && ((piece.Color == Color.White && !turn) || (piece.Color == Color.Black && turn)))
                {
                    //above (i represents y/row)
                    for (int i = y - 1; i > -1; i--)
                    {
                        if (board.Rows[i].Cells[x].Value == null)
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i))
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.Color, x, i))
                        {
                            break;
                        }
                    }

                    //below (i represents y/row)
                    for (int i = y + 1; i < 8; i++)
                    {
                        if (board.Rows[i].Cells[x].Value == null)
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i))
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.Color, x, i))
                        {
                            break;
                        }
                    }

                    //left (i represents x/column)
                    for (int i = x - 1; i > -1; i--)
                    {
                        if (board.Rows[y].Cells[i].Value == null)
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y))
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.Color, i, y))
                        {
                            break;
                        }
                    }

                    //right (i represents x/column)
                    for (int i = x + 1; i < 8; i++)
                    {
                        if (board.Rows[y].Cells[i].Value == null)
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y))
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.Color, i, y))
                        {
                            break;
                        }
                    }
                }
                else if (piece.Name == "horse" && ((piece.Color == Color.White && !turn) || (piece.Color == Color.Black && turn)))
                {
                    //up left
                    if (y - 2 > -1 && x - 1 > -1 && board.Rows[y - 2].Cells[x - 1].Value == null)
                    {
                        board.Rows[y - 2].Cells[x - 1].Style.BackColor = Color.Blue;
                    }
                    else if (y - 2 > -1 && x - 1 > -1 && board.Rows[y - 2].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y - 2))
                    {
                        board.Rows[y - 2].Cells[x - 1].Style.BackColor = Color.Red;
                    }

                    //up right
                    if (y - 2 > -1 && x + 1 < 8 && board.Rows[y - 2].Cells[x + 1].Value == null)
                    {
                        board.Rows[y - 2].Cells[x + 1].Style.BackColor = Color.Blue;
                    }
                    else if (y - 2 > -1 && x + 1 < 8 && board.Rows[y - 2].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y - 2))
                    {
                        board.Rows[y - 2].Cells[x + 1].Style.BackColor = Color.Red;
                    }

                    //right up
                    if (y - 1 > -1 && x + 2 < 8 && board.Rows[y - 1].Cells[x + 2].Value == null)
                    {
                        board.Rows[y - 1].Cells[x + 2].Style.BackColor = Color.Blue;
                    }
                    else if (y - 1 > -1 && x + 2 < 8 && board.Rows[y - 1].Cells[x + 2].Value != null && containsEnemy(piece.Color, x + 2, y - 1))
                    {
                        board.Rows[y - 1].Cells[x + 2].Style.BackColor = Color.Red;
                    }

                    //right down
                    if (y + 1 < 8 && x + 2 < 8 && board.Rows[y + 1].Cells[x + 2].Value == null)
                    {
                        board.Rows[y + 1].Cells[x + 2].Style.BackColor = Color.Blue;
                    }
                    else if (y + 1 < 8 && x + 2 < 8 && board.Rows[y + 1].Cells[x + 2].Value != null && containsEnemy(piece.Color, x + 2, y + 1))
                    {
                        board.Rows[y + 1].Cells[x + 2].Style.BackColor = Color.Red;
                    }

                    //down left
                    if (y + 2 < 8 && x - 1 > -1 && board.Rows[y + 2].Cells[x - 1].Value == null)
                    {
                        board.Rows[y + 2].Cells[x - 1].Style.BackColor = Color.Blue;
                    }
                    else if (y + 2 < 8 && x - 1 > -1 && board.Rows[y + 2].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y + 2))
                    {
                        board.Rows[y + 2].Cells[x - 1].Style.BackColor = Color.Red;
                    }

                    //down right
                    if (y + 2 < 8 && x + 1 < 8 && board.Rows[y + 2].Cells[x + 1].Value == null)
                    {
                        board.Rows[y + 2].Cells[x + 1].Style.BackColor = Color.Blue;
                    }
                    else if (y + 2 < 8 && x + 1 < 8 && board.Rows[y + 2].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y + 2))
                    {
                        board.Rows[y + 2].Cells[x + 1].Style.BackColor = Color.Red;
                    }

                    //left up
                    if (y + 1 < 8 && x - 2 > -1 && board.Rows[y + 1].Cells[x - 2].Value == null)
                    {
                        board.Rows[y + 1].Cells[x - 2].Style.BackColor = Color.Blue;
                    }
                    else if (y + 1 < 8 && x - 2 > -1 && board.Rows[y + 1].Cells[x - 2].Value != null && containsEnemy(piece.Color, x - 2, y + 1))
                    {
                        board.Rows[y + 1].Cells[x - 2].Style.BackColor = Color.Red;
                    }

                    //left down
                    if (y - 1 > -1 && x - 2 > -1 && board.Rows[y - 1].Cells[x - 2].Value == null)
                    {
                        board.Rows[y - 1].Cells[x - 2].Style.BackColor = Color.Blue;
                    }
                    else if (y - 1 > -1 && x - 2 > -1 && board.Rows[y - 1].Cells[x - 2].Value != null && containsEnemy(piece.Color, x - 2, y - 1))
                    {
                        board.Rows[y - 1].Cells[x - 2].Style.BackColor = Color.Red;
                    }
                }
                else if (piece.Name == "bishop" && ((piece.Color == Color.White && !turn) || (piece.Color == Color.Black && turn)))
                {
                    int i;
                    int j;

                    //up right
                    for (i = x + 1, j = y - 1; i < 8; i++, j--)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //up left
                    for (i = x - 1, j = y - 1; i > -1; i--, j--)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //down right
                    for (i = x + 1, j = y + 1; i < 8; i++, j++)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //down left
                    for (i = x - 1, j = y + 1; i > -1; i--, j++)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }
                }
                else if (piece.Name == "queen" && ((piece.Color == Color.White && !turn) || (piece.Color == Color.Black && turn)))
                {
                    int i;
                    int j;

                    //up right
                    for (i = x + 1, j = y - 1; i < 8; i++, j--)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //up left
                    for (i = x - 1, j = y - 1; i > -1; i--, j--)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //down right
                    for (i = x + 1, j = y + 1; i < 8; i++, j++)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //down left
                    for (i = x - 1, j = y + 1; i > -1; i--, j++)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //above (i represents y/row)
                    for (i = y - 1; i > -1; i--)
                    {
                        if (board.Rows[i].Cells[x].Value == null)
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i))
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.Color, x, i))
                        {
                            break;
                        }
                    }

                    //below (i represents y/row)
                    for (i = y + 1; i < 8; i++)
                    {
                        if (board.Rows[i].Cells[x].Value == null)
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i))
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.Color, x, i))
                        {
                            break;
                        }
                    }

                    //left (i represents x/column)
                    for (i = x - 1; i > -1; i--)
                    {
                        if (board.Rows[y].Cells[i].Value == null)
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y))
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.Color, i, y))
                        {
                            break;
                        }
                    }

                    //right (i represents x/column)
                    for (i = x + 1; i < 8; i++)
                    {
                        if (board.Rows[y].Cells[i].Value == null)
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y))
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.Color, i, y))
                        {
                            break;
                        }
                    }
                }
                else if (piece.Name == "king" && ((piece.Color == Color.White && !turn) || (piece.Color == Color.Black && turn)))
                {
                    List<int[]> future = getCheckTiles(piece.Color == Color.White ? Color.Black : Color.White)[1];
                    //right
                    if (x + 1 < 8)
                    {
                        bool covered = false;
                        //make sure the king cant go on a space covered by the enemy
                        foreach (int[] tile in future)
                        {
                            if (x + 1 == tile[0] && y == tile[1])
                            {
                                covered = true;
                            }
                        }
                        if (!covered)
                        {
                            if (board.Rows[y].Cells[x + 1].Value == null)
                            {
                                board.Rows[y].Cells[x + 1].Style.BackColor = Color.Blue;
                            }
                            else if (board.Rows[y].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y))
                            {
                                board.Rows[y].Cells[x + 1].Style.BackColor = Color.Red;
                            }
                        }

                    }

                    //left
                    if (x - 1 > -1)
                    {
                        bool covered = false;
                        //make sure the king cant go on a space covered by the enemy
                        foreach (int[] tile in future)
                        {
                            if (x - 1 == tile[0] && y == tile[1])
                            {
                                covered = true;
                            }
                        }
                        if (!covered)
                        {
                            if (board.Rows[y].Cells[x - 1].Value == null)
                            {
                                board.Rows[y].Cells[x - 1].Style.BackColor = Color.Blue;
                            }
                            else if (board.Rows[y].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y))
                            {
                                board.Rows[y].Cells[x - 1].Style.BackColor = Color.Red;
                            }
                        }

                    }

                    //down
                    if (y + 1 < 8)
                    {
                        bool covered = false;
                        //make sure the king cant go on a space covered by the enemy
                        foreach (int[] tile in future)
                        {
                            if (x == tile[0] && y + 1 == tile[1])
                            {
                                covered = true;
                            }
                        }
                        if (!covered)
                        {
                            if (board.Rows[y + 1].Cells[x].Value == null)
                            {
                                board.Rows[y + 1].Cells[x].Style.BackColor = Color.Blue;
                            }
                            else if (board.Rows[y + 1].Cells[x].Value != null && containsEnemy(piece.Color, x, y + 1))
                            {
                                board.Rows[y + 1].Cells[x].Style.BackColor = Color.Red;
                            }
                        }

                    }

                    //up
                    if (y - 1 > -1)
                    {
                        bool covered = false;
                        //make sure the king cant go on a space covered by the enemy
                        foreach (int[] tile in future)
                        {
                            if (x == tile[0] && y - 1 == tile[1])
                            {
                                covered = true;
                            }
                        }
                        if (!covered)
                        {
                            if (board.Rows[y - 1].Cells[x].Value == null)
                            {
                                board.Rows[y - 1].Cells[x].Style.BackColor = Color.Blue;
                            }
                            else if (board.Rows[y - 1].Cells[x].Value != null && containsEnemy(piece.Color, x, y - 1))
                            {
                                board.Rows[y - 1].Cells[x].Style.BackColor = Color.Red;
                            }
                        }

                    }

                    //top right
                    if (x + 1 < 8 && y - 1 > -1)
                    {
                        bool covered = false;
                        //make sure the king cant go on a space covered by the enemy
                        foreach (int[] tile in future)
                        {
                            if (x + 1 == tile[0] && y - 1 == tile[1])
                            {
                                covered = true;
                            }
                        }
                        if (!covered)
                        {
                            if (board.Rows[y - 1].Cells[x + 1].Value == null)
                            {
                                board.Rows[y - 1].Cells[x + 1].Style.BackColor = Color.Blue;
                            }
                            else if (board.Rows[y - 1].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y - 1))
                            {
                                board.Rows[y - 1].Cells[x + 1].Style.BackColor = Color.Red;
                            }
                        }

                    }

                    //top left
                    if (x - 1 > -1 && y - 1 > -1)
                    {
                        bool covered = false;
                        //make sure the king cant go on a space covered by the enemy
                        foreach (int[] tile in future)
                        {
                            if (x - 1 == tile[0] && y - 1 == tile[1])
                            {
                                covered = true;
                            }
                        }
                        if (!covered)
                        {
                            if (board.Rows[y - 1].Cells[x - 1].Value == null)
                            {
                                board.Rows[y - 1].Cells[x - 1].Style.BackColor = Color.Blue;
                            }
                            else if (board.Rows[y - 1].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y - 1))
                            {
                                board.Rows[y - 1].Cells[x - 1].Style.BackColor = Color.Red;
                            }
                        }

                    }

                    //bottom right
                    if (x + 1 < 8 && y + 1 < 8)
                    {
                        bool covered = false;
                        //make sure the king cant go on a space covered by the enemy
                        foreach (int[] tile in future)
                        {
                            if (x + 1 == tile[0] && y + 1 == tile[1])
                            {
                                covered = true;
                            }
                        }
                        if (!covered)
                        {
                            if (board.Rows[y + 1].Cells[x + 1].Value == null)
                            {
                                board.Rows[y + 1].Cells[x + 1].Style.BackColor = Color.Blue;
                            }
                            else if (board.Rows[y + 1].Cells[x + 1].Value != null && containsEnemy(piece.Color, x + 1, y + 1))
                            {
                                board.Rows[y + 1].Cells[x + 1].Style.BackColor = Color.Red;
                            }
                        }

                    }

                    //bottom left
                    if (x - 1 > -1 && y + 1 < 8)
                    {
                        bool covered = false;
                        //make sure the king cant go on a space covered by the enemy
                        foreach (int[] tile in future)
                        {
                            if (x - 1 == tile[0] && y + 1 == tile[1])
                            {
                                covered = true;
                            }
                        }
                        if (!covered)
                        {
                            if (board.Rows[y + 1].Cells[x - 1].Value == null)
                            {
                                board.Rows[y + 1].Cells[x - 1].Style.BackColor = Color.Blue;
                            }
                            else if (board.Rows[y + 1].Cells[x - 1].Value != null && containsEnemy(piece.Color, x - 1, y + 1))
                            {
                                board.Rows[y + 1].Cells[x - 1].Style.BackColor = Color.Red;
                            }
                        }
                    }
                    //CASTLE
                    //if king has not moved and the spaces between him and the rook are check free
                    if (!piece.Moved)
                    {
                        //KINGSIDE CASTLE
                        bool castle = true;

                        //find kingside rook
                        Piece kingRook = new Piece();
                        foreach (Piece findRook in Pieces)
                        {
                            if (findRook.Name == "rook" && findRook.Color == piece.Color && !findRook.Moved && findRook.X == 7)
                            {
                                kingRook = findRook;
                            }
                        }

                        //check if rook was found successfully and the spots between king and rook are empty
                        if (kingRook.Name == "rook" && board.Rows[y].Cells[piece.X + 1].Value == null && board.Rows[y].Cells[piece.X + 2].Value == null)
                        {
                            //check each future move of the enemy to see if spaces in between king and rook are not in check
                            foreach (int[] tile in future)
                            {
                                //if the 2 spots are checkable, castle is illegal
                                if ((piece.X + 1 == tile[0] && piece.Y == tile[1]) || (piece.X + 2 == tile[0] && piece.Y == tile[1]))
                                {
                                    castle = false;
                                }
                            }

                            if (castle)
                            {
                                board.Rows[kingRook.Y].Cells[kingRook.X].Style.BackColor = Color.Green;
                            }
                        }


                        //QUEENSIDE CASTLE
                        castle = true;

                        //find kingside rook
                        Piece queenRook = new Piece();
                        foreach (Piece findRook in Pieces)
                        {
                            if (findRook.Name == "rook" && findRook.Color == piece.Color && !findRook.Moved && findRook.X == 0)
                            {
                                queenRook = findRook;
                            }
                        }

                        //check if rook was found successfully and the spots between king and rook are empty
                        if (queenRook.Name == "rook" && board.Rows[y].Cells[piece.X - 1].Value == null && board.Rows[y].Cells[piece.X - 2].Value == null && board.Rows[y].Cells[piece.X - 3].Value == null)
                        {
                            //check each future move of the enemy to see if spaces in between king and rook are not in check
                            foreach (int[] tile in future)
                            {
                                //if the 2 spots are checkable, castle is illegal
                                if ((piece.X - 1 == tile[0] && piece.Y == tile[1]) || (piece.X - 2 == tile[0] && piece.Y == tile[1]))
                                {
                                    castle = false;
                                }
                            }

                            if (castle)
                            {
                                board.Rows[queenRook.Y].Cells[queenRook.X].Style.BackColor = Color.Green;
                            }
                        }
                    }

                }
            }
            else
            {
                List<int[]>[] getCheck = getCheckTiles(piece.Color == Color.White ? Color.Black : Color.White);
                List<int[]> threats = getCheck[0];
                List<int[]> future = getCheck[1];

                int x = piece.X;
                int y = piece.Y;

                if (piece.Name == "king")
                {
                    int newy = y - 1;
                    //up
                    if (newy > -1 && newy < 8 && board.Rows[newy].Cells[x].Value == null)
                    {
                        bool open = true;
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == x && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        foreach (int[] tile in future)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == x && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[newy].Cells[x].Style.BackColor = Color.Blue;
                        }
                    }
                    //if an enemy is on that tile
                    else if (newy > -1 && newy < 8 && board.Rows[newy].Cells[x].Value != null && containsEnemy(piece.Color, x, newy))
                    {
                        bool open = true;
                        foreach (int[] tile in future)
                        {
                            //if that tile is also covered by another enemy
                            if (tile[0] == x && tile[1] == newy)
                            {
                                open = false;
                            }
                        }
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == x && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[newy].Cells[x].Style.BackColor = Color.Red;
                        }
                    }

                    newy = y + 1;
                    //down
                    if (newy < 8 && newy > -1 && board.Rows[newy].Cells[x].Value == null)
                    {
                        bool open = true;
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == x && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == x && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[y + 1].Cells[x].Style.BackColor = Color.Blue;
                        }
                    }
                    //if an enemy is on that tile
                    else if (newy < 8 && newy > -1 && board.Rows[newy].Cells[x].Value != null && containsEnemy(piece.Color, x, newy))
                    {
                        bool open = true;
                        foreach (int[] tile in future)
                        {
                            //if that tile is also covered by another enemy
                            if (tile[0] == x && tile[1] == newy)
                            {
                                open = false;
                            }
                        }
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == x && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[newy].Cells[x].Style.BackColor = Color.Red;
                        }
                    }

                    int newx = x - 1;
                    //left
                    if (newx > -1 && newx < 8 && board.Rows[y].Cells[newx].Value == null)
                    {
                        bool open = true;
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == y)
                            {
                                open = false;
                                break;
                            }
                        }
                        foreach (int[] tile in future)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == y)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[y].Cells[newx].Style.BackColor = Color.Blue;
                        }
                    }
                    //if an enemy is on that tile
                    else if (newx > -1 && newx < 8 && board.Rows[y].Cells[newx].Value != null && containsEnemy(piece.Color, newx, y))
                    {
                        bool open = true;
                        foreach (int[] tile in future)
                        {
                            //if that tile is also covered by another enemy
                            if (tile[0] == newx && tile[1] == y)
                            {
                                open = false;
                            }
                        }
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == y)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[y].Cells[newx].Style.BackColor = Color.Red;
                        }
                    }

                    newx = x + 1;
                    //right
                    if (newx < 8 && newx > -1 && board.Rows[y].Cells[newx].Value == null)
                    {
                        bool open = true;
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == y)
                            {
                                open = false;
                                break;
                            }
                        }
                        foreach (int[] tile in future)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == y)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[y].Cells[newx].Style.BackColor = Color.Blue;
                        }
                    }
                    //if an enemy is on that tile
                    else if (newx > -1 && newx > -1 && board.Rows[y].Cells[newx].Value != null && containsEnemy(piece.Color, newx, y))
                    {
                        bool open = true;
                        foreach (int[] tile in future)
                        {
                            //if that tile is also covered by another enemy
                            if (tile[0] == newx && tile[1] == y)
                            {
                                open = false;
                            }
                        }
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == y)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[y].Cells[newx].Style.BackColor = Color.Red;
                        }
                    }

                    newx = x - 1;
                    newy = y - 1;
                    //up left
                    if (newx > -1 && newy > -1 && newx < 8 && newy < 8 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        bool open = true;
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        foreach (int[] tile in future)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                        }
                    }
                    //if an enemy is on that tile
                    else if (newx > -1 && newy > -1 && newx < 8 && newy < 8 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        bool open = true;
                        foreach (int[] tile in future)
                        {
                            //if that tile is also covered by another enemy
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                            }
                        }
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                        }
                    }

                    newx = x + 1;
                    newy = y - 1;
                    //up right
                    if (newx < 8 && newy > -1 && newx < -1 && newy < 8 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        bool open = true;
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                        }
                    }
                    //if an enemy is on that tile
                    else if (newx < 8 && newy > -1 && newx > -1 && newy < 8 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        bool open = true;
                        foreach (int[] tile in future)
                        {
                            //if that tile is also covered by another enemy
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                            }
                        }
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                        }
                    }

                    newx = x - 1;
                    newy = y + 1;
                    //down left
                    if (newx > -1 && newy < 8 && newx < 8 && newy > -1 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        bool open = true;
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        foreach (int[] tile in future)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                        }
                    }
                    //if an enemy is on that tile
                    else if (newx > -1 && newy < 8 && newx < 8 && newy > -1 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        bool open = true;
                        foreach (int[] tile in future)
                        {
                            //if that tile is also covered by another enemy
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                            }
                        }
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                        }
                    }

                    newx = x + 1;
                    newy = y + 1;
                    //down right
                    if (newx < 8 && newy < 8 && newx > -1 && newy > -1 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        bool open = true;
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        foreach (int[] tile in future)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                        }
                    }
                    //if an enemy is on that tile
                    else if (newx < 8 && newy < 8 && newx > -1 && newy > -1 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        bool open = true;
                        foreach (int[] tile in future)
                        {
                            //if that tile is also covered by another enemy
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                            }
                        }
                        foreach (int[] tile in threats)
                        {
                            //if the tile is covered by the enemy, the king cant move there and the opening is false
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                open = false;
                                break;
                            }
                        }
                        if (open)
                        {
                            board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                        }
                    }
                }
                else if (piece.Name == "pawn")
                {
                    int newy = y - 1;
                    //up
                    if (newy > -1 && board.Rows[newy].Cells[x].Value == null)
                    {
                        foreach (int[] tile in threats)
                        {
                            //if the tile is threatening the king, the piece can move to block that threat
                            if (tile[0] == x && tile[1] == newy && tile[2] == 0)
                            {
                                board.Rows[newy].Cells[x].Style.BackColor = Color.Blue;
                                break;
                            }
                        }
                    }

                    newy = y - 2;
                    //up 2
                    if (y == 6 && board.Rows[newy].Cells[x].Value == null)
                    {
                        foreach (int[] tile in threats)
                        {
                            //if the tile is threatening the king, the piece can move to block that threat
                            if (tile[0] == x && tile[1] == newy && tile[2] == 0)
                            {
                                board.Rows[newy].Cells[x].Style.BackColor = Color.Blue;
                                break;
                            }
                        }
                    }

                    newy = y - 1;
                    int newx = x - 1;
                    //up left
                    if (newy > -1 && newx > -1 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(Color.White, newx, newy))
                    {
                        foreach (int[] tile in threats)
                        {
                            //if the tile is threatening the king, the piece can move to block that threat
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                                break;
                            }
                        }
                    }

                    newy = y - 1;
                    newx = x + 1;
                    //up right
                    if (newy > -1 && newx < 8 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(Color.White, newx, newy))
                    {
                        foreach (int[] tile in threats)
                        {
                            //if the tile is threatening the king, the piece can move to block that threat
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                                break;
                            }
                        }
                    }
                }
                else if (piece.Name == "rook")
                {
                    //above (i represents y/row)
                    for (int i = y - 1; i > -1; i--)
                    {
                        if (board.Rows[i].Cells[x].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                                    break;
                                }
                            }
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.Color, x, i))
                        {
                            break;
                        }
                    }

                    //below (i represents y/row)
                    for (int i = y + 1; i < 8; i++)
                    {
                        if (board.Rows[i].Cells[x].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                                    break;
                                }
                            }
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.Color, x, i))
                        {
                            break;
                        }
                    }

                    //left (i represents x/column)
                    for (int i = x - 1; i > -1; i--)
                    {
                        if (board.Rows[y].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == y)
                                {
                                    board.Rows[y].Cells[i].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == y)
                                {
                                    board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                                    break;
                                }
                            }
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.Color, i, y))
                        {
                            break;
                        }
                    }

                    //right (i represents x/column)
                    for (int i = x + 1; i > 8; i++)
                    {
                        if (board.Rows[y].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == y)
                                {
                                    board.Rows[y].Cells[i].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == y)
                                {
                                    board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                                    break;
                                }
                            }
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.Color, i, y))
                        {
                            break;
                        }
                    }

                }
                else if (piece.Name == "horse")
                {
                    int newx = x - 1;
                    int newy = y - 2;
                    //up left
                    if (newy > -1 && newx > -1 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                            }
                        }
                    }
                    else if (newy > -1 && newx > -1 && board.Rows[newy].Cells[x - 1].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                            }
                        }
                    }

                    newx = x + 1;
                    newy = y - 2;
                    //up right
                    if (newy > -1 && newx < 8 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                            }
                        }
                    }
                    else if (newy > -1 && newx < 8 && board.Rows[newy].Cells[x - 1].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                            }
                        }
                    }

                    newx = x + 2;
                    newy = y - 1;
                    //right up
                    if (newy > -1 && newx < 8 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                            }
                        }
                    }
                    else if (newy > -1 && newx < 8 && board.Rows[newy].Cells[x - 1].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                            }
                        }
                    }

                    newx = x + 2;
                    newy = y + 1;
                    //right down
                    if (newy < 8 && newx < 8 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                            }
                        }
                    }
                    else if (newy < 8 && newx < 8 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                            }
                        }
                    }

                    newx = x + 1;
                    newy = y + 2;
                    //down right
                    if (newy < 8 && newx < 8 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                            }
                        }
                    }
                    else if (newy < 8 && newx < 8 && board.Rows[newy].Cells[x - 1].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                            }
                        }
                    }

                    newx = x - 1;
                    newy = y + 2;
                    //down left
                    if (newy < 8 && newx > -1 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                            }
                        }
                    }
                    else if (newy < 8 && newx > -1 && board.Rows[newy].Cells[x - 1].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                            }
                        }
                    }

                    newx = x - 2;
                    newy = y + 1;
                    //left down
                    if (newy < 8 && newx > -1 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                            }
                        }
                    }
                    else if (newy < 8 && newx > -1 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                            }
                        }
                    }

                    newx = x - 2;
                    newy = y - 1;
                    //left up
                    if (newy > -1 && newx > -1 && board.Rows[newy].Cells[newx].Value == null)
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Blue;
                            }
                        }
                    }
                    else if (newy > -1 && newx > -1 && board.Rows[newy].Cells[x - 1].Value != null && containsEnemy(piece.Color, newx, newy))
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == newx && tile[1] == newy)
                            {
                                board.Rows[newy].Cells[newx].Style.BackColor = Color.Red;
                            }
                        }
                    }
                }
                else if (piece.Name == "bishop")
                {
                    int i;
                    int j;
                    //up right
                    for (i = x + 1, j = y - 1; i < 8; i++, j--)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == j)
                                {
                                    board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == j)
                                {
                                    board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                                }
                            }
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //down right
                    for (i = x + 1, j = y + 1; i < 8; i++, j++)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == j)
                                {
                                    board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == j)
                                {
                                    board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                                }
                            }
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //down left
                    for (i = x - 1, j = y + 1; i > -1; i--, j++)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == j)
                                {
                                    board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == j)
                                {
                                    board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                                }
                            }
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //up left
                    for (i = x - 1, j = y - 1; i > -1; i--, j--)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == j)
                                {
                                    board.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == j)
                                {
                                    board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                                }
                            }
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }
                }
                else if (piece.Name == "queen")
                {
                    int i;
                    int j;
                    //up right
                    for (i = x + 1, j = y - 1; i < 8; i++, j--)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                                }
                            }
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //down right
                    for (i = x + 1, j = y + 1; i < 8; i++, j++)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                                }
                            }
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //down left
                    for (i = x - 1, j = y + 1; i > -1; i--, j++)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                                }
                            }
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //up left
                    for (i = x - 1, j = y - 1; i > -1; i--, j--)
                    {
                        if (j > 7 || j < 0)
                        {
                            break;
                        }

                        if (board.Rows[j].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                                }
                            }
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                        {
                            break;
                        }
                    }

                    //above (i represents y/row)
                    for (i = y - 1; i > -1; i--)
                    {
                        if (board.Rows[i].Cells[x].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                                    break;
                                }
                            }
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.Color, x, i))
                        {
                            break;
                        }
                    }

                    //below (i represents y/row)
                    for (i = y + 1; i < 8; i++)
                    {
                        if (board.Rows[i].Cells[x].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == x && tile[1] == i)
                                {
                                    board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                                    break;
                                }
                            }
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.Color, x, i))
                        {
                            break;
                        }
                    }

                    //left (i represents x/column)
                    for (i = x - 1; i > -1; i--)
                    {
                        if (board.Rows[y].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == y)
                                {
                                    board.Rows[y].Cells[i].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == y)
                                {
                                    board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                                    break;
                                }
                            }
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.Color, i, y))
                        {
                            break;
                        }
                    }

                    //right (i represents x/column)
                    for (i = x + 1; i > 8; i++)
                    {
                        if (board.Rows[y].Cells[i].Value == null)
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == y)
                                {
                                    board.Rows[y].Cells[i].Style.BackColor = Color.Blue;
                                }
                            }
                        }
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y))
                        {
                            foreach (int[] tile in threats)
                            {
                                if (tile[0] == i && tile[1] == y)
                                {
                                    board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                                    break;
                                }
                            }
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.Color, i, y))
                        {
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// checks the check tiles (USED FOR DEBUGGING)
        /// </summary>
        /// <param name="checkTiles"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool checkCheckTiles(List<int[]> checkTiles, int x, int y)
        {
            foreach (int[] tile in checkTiles)
            {
                if (x == tile[0] && y == tile[1])
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// checks to see if the move will result in a check on your side
        /// </summary>
        /// <param name="piece">piece beign moved</param>
        /// <param name="newx">the x cord being moved to</param>
        /// <param name="newy">the y cord beign moved to</param>
        /// <returns></returns>
        public bool checkForACheck(Piece piece, int newx, int newy, bool killed)
        {
            //hard copy of Pieces list
            List<Piece> testPieces = new List<Piece>(Pieces);

            //stores all the already highlighted spaces
            //int[2] = 0 is blue
            //int[2] = 1 is red
            //int[2] = 2 is green
            //int[2] = 3 is purple
            List<int[]> highlights = new List<int[]>();
            foreach (DataGridViewRow row in board.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Style.BackColor == Color.Blue)
                    {
                        highlights.Add(new int[] { cell.ColumnIndex, cell.RowIndex, 0 });
                    }
                    else if (cell.Style.BackColor == Color.Red)
                    {
                        highlights.Add(new int[] { cell.ColumnIndex, cell.RowIndex, 1 });
                    }
                    else if (cell.Style.BackColor == Color.Green)
                    {
                        highlights.Add(new int[] { cell.ColumnIndex, cell.RowIndex, 2 });
                    }
                    else if (cell.Style.BackColor == Color.Purple)
                    {
                        highlights.Add(new int[] { cell.ColumnIndex, cell.RowIndex, 3 });
                    }
                }
            }

            //move to new spot
            int oldx = piece.X;
            int oldy = piece.Y;
            piece.X = newx;
            piece.Y = newy;
            board.Rows[oldy].Cells[oldx].Value = null;
            board.Rows[newy].Cells[newx].Value = piece.Icon;


            //Piece kill = new Piece();
            //if (killed)
            //{
            //    try
            //    {
            //        kill = new Piece(Pieces.Find(k => k.X == newx && k.Y == newy && k != piece));
            //        testPieces.Remove(kill);
            //    }
            //    catch (ArgumentNullException)
            //    {

            //    }
            //    //see if that spot would be a kill
            //    //foreach (Piece found in testPieces)
            //    //{
            //    //    if (found != piece && found.X == newx && found.Y == newy)
            //    //    {
            //    //        kill = found;
            //    //        break;
            //    //    }
            //    //}
            //    //if (kill.X != -1)
            //    //{
            //    //    testPieces.Remove(kill);
            //    //}
            //}

            //highlights all spaces
            foreach (Piece check in testPieces)
            {
                if (check.Color != piece.Color)
                {
                    oldHighlightSpaces(check, false);
                }
            }

            //find the king 
            Piece king = Pieces.Find(k => k.Name == "king" && k.Color == piece.Color);

            //see if any red spaces hold the king
            bool checkd = false;
            foreach (DataGridViewRow row in board.Rows)
            {
                foreach (DataGridViewImageCell cell in row.Cells)
                {
                    if (cell.Style.BackColor == Color.Red && cell.RowIndex == king.Y && cell.ColumnIndex == king.X)
                    {
                        checkd = true;
                    }
                }
            }

            //reset back to before
            board.Rows[newy].Cells[newx].Value = null;
            if (kill.X != -1)
            {
                testPieces.Add(kill);
                board.Rows[kill.Y].Cells[kill.X].Value = kill.Icon;
            }
            piece.X = oldx;
            piece.Y = oldy;
            board.Rows[oldy].Cells[oldx].Value = piece.Icon;

            ogColor();
            foreach (int[] tile in highlights)
            {
                if (tile[2] == 0)
                {
                    board.Rows[tile[1]].Cells[tile[0]].Style.BackColor = Color.Blue;
                }
                else if (tile[2] == 1)
                {
                    board.Rows[tile[1]].Cells[tile[0]].Style.BackColor = Color.Red;
                }
                else if (tile[2] == 2)
                {
                    board.Rows[tile[1]].Cells[tile[0]].Style.BackColor = Color.Green;
                }
                else if (tile[2] == 3)
                {
                    board.Rows[tile[1]].Cells[tile[0]].Style.BackColor = Color.Purple;
                }
            }

            return checkd;
        }

        /// <summary>
        /// checks if the space contains an enemy
        /// </summary>
        /// <param name="color">the color of the piece looking for enemies</param>
        /// <param name="x">column of piece</param>
        /// <param name="y">row of piece</param>
        /// <returns>True = that space does contain an enemy. False = that space doesn't contain an enemy</returns>
        public bool containsEnemy(Color color, int x, int y)
        {
            foreach(Piece piece in Pieces)
            {
                if(color != piece.Color && piece.X == x && piece.Y == y)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// changes the turn to the other player
        /// </summary>
        public void changeTurn()
        {
            if (timing && turn)
            {
                whiteTimer.Stop();
                blackTimer.Start();
            }
            else if (timing && !turn)
            {
                whiteTimer.Start();
                blackTimer.Stop();
            }

            if (checkStale(Color.White) || checkStale(Color.Black))
            {
                endGame(Color.Yellow);
            }

            drawLabel.Hide();
            drawButton.Hide();
            turn = turn ? false : true;
            turnLabel.Text = turn ? "White" : "Black";
            turnLabel.BackColor = turn ? Color.Black : Color.White;
            turnLabel.ForeColor = turn ? Color.White : Color.Black;
            flipBoard();
        }

        /// <summary>
        /// checks to see if the piece is protecting its king from a check
        /// </summary>
        /// <param name="protector">the piece that we are checking to see if it is protecting its king</param>
        /// <returns>If piece is contructed witht he defualt contructor, there is nothing forcing the protector there (no need to rpevent a check). Otherwise, the peice keeping the protector in place is returned</returns>
        public Piece preventCheck(Piece protector)
        {
            //find the king
            Piece king = Pieces.Find(k => k.Name == "king" && k.Color == protector.Color);

            //iterate through the enemy to see if there is a path for them to check if protector were to move
            foreach (Piece piece in Pieces)
            {
                //ignores if the piece is on the side of the protector
                if (protector.Color != piece.Color)
                {
                    int x = piece.X;
                    int y = piece.Y;
                    if (piece.Name == "rook")
                    {
                        //records if a peice is in the way between a check
                        bool Protected = false;

                        //records if the piece can kill the king if they had a clear path w/o the protector
                        bool kingInPath = false;

                        for (int i = y - 1; i > -1; i--)
                        {
                            //if there is king along the piece's path
                            if (king.X == x && king.Y == i)
                            {
                                kingInPath = true;
                                break;
                            }
                            //if there is already someone blocking piece's path on protector's team, then anyways theres no need for protector to be restricted since his teamate is covering him
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i) && (protector.X != x || protector.Y != i))
                            {
                                break;
                            }
                            //if the protector is first in the path of the piece
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i) && protector.X == x && protector.Y == i)
                            {
                                Protected = true;
                            }
                            //if piece is blocoked by his own teammate, there is no need to see if protector is in the way or if protector's king is in the way
                            else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.Color, x, i))
                            {
                                break;
                            }
                        }

                        if (Protected && kingInPath)
                        {
                            return piece;
                        }
                        else
                        {
                            Protected = false;
                            kingInPath = false;
                        }

                        //below (i represents y/row)
                        for (int i = y + 1; i < 8; i++)
                        {
                            if (king.X == x && king.Y == i)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i) && (protector.X != x || protector.Y != i))
                            {
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i) && protector.X == x && protector.Y == i)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.Color, x, i))
                            {
                                break;
                            }
                        }

                        if (Protected && kingInPath)
                        {
                            return piece;
                        }
                        else
                        {
                            Protected = false;
                            kingInPath = false;
                        }

                        //left (i represents x/column)
                        for (int i = x - 1; i > -1; i--)
                        {
                            if (king.X == i && king.Y == y)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y) && (protector.X != i || protector.Y != y))
                            {
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y) && protector.X == i && protector.Y == y)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.Color, i, y))
                            {
                                break;
                            }
                        }

                        if (Protected && kingInPath)
                        {
                            return piece;
                        }
                        else
                        {
                            Protected = false;
                            kingInPath = false;
                        }

                        //right (i represents x/column)
                        for (int i = x + 1; i < 8; i++)
                        {
                            if (king.X == i && king.Y == y)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y) && (protector.X != i || protector.Y != y))
                            {
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y) && protector.X == i && protector.Y == y)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.Color, i, y))
                            {
                                break;
                            }
                        }

                        if (Protected && kingInPath)
                        {
                            return piece;
                        }
                        else
                        {
                            Protected = false;
                            kingInPath = false;
                        }
                    }
                    else if (piece.Name == "bishop")
                    {
                        //records if a peice is in the way between a check
                        bool Protected = false;

                        //records if the piece can kill the king if they had a clear path w/o the protector
                        bool kingInPath = false;

                        int i;
                        int j;
                        //up right
                        for (i = x + 1, j = y - 1; i < 8; i++, j--)
                        {
                            if (j > 7 || j < 0)
                            {
                                break;
                            }

                            if (king.X == i && king.Y == j)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && (protector.X != i || protector.Y != j))
                            {
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && protector.X == i && protector.Y == j)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                            {
                                break;
                            }
                        }

                        if (Protected && kingInPath)
                        {
                            return piece;
                        }
                        else
                        {
                            Protected = false;
                            kingInPath = false;
                        }

                        //down right
                        for (i = x + 1, j = y + 1; i < 8; i++, j++)
                        {
                            if (j > 7 || j < 0)
                            {
                                break;
                            }

                            if (king.X == i && king.Y == j)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && (protector.X != i || protector.Y != j))
                            {
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && protector.X == i && protector.Y == j)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                            {
                                break;
                            }
                        }

                        if (Protected && kingInPath)
                        {
                            return piece;
                        }
                        else
                        {
                            Protected = false;
                            kingInPath = false;
                        }

                        //down left
                        for (i = x - 1, j = y + 1; i > -1; i--, j++)
                        {
                            if (j > 7 || j < 0)
                            {
                                break;
                            }

                            if (king.X == i && king.Y == j)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && (protector.X != i || protector.Y != j))
                            {
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && protector.X == i && protector.Y == j)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                            {
                                break;
                            }
                        }

                        if (Protected && kingInPath)
                        {
                            return piece;
                        }
                        else
                        {
                            Protected = false;
                            kingInPath = false;
                        }

                        //up left
                        for (i = x - 1, j = y - 1; i > -1; i--, j--)
                        {
                            if (j > 7 || j < 0)
                            {
                                break;
                            }

                            if (king.X == i && king.Y == j)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && (protector.X != i || protector.Y != j))
                            {
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && protector.X == i && protector.Y == j)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                            {
                                break;
                            }
                        }

                        if (Protected && kingInPath)
                        {
                            return piece;
                        }
                        else
                        {
                            Protected = false;
                            kingInPath = false;
                        }
                    }
                    else if (piece.Name == "queen")
                    {
                        //records if a peice is in the way between a check
                        bool Protected = false;

                        //records if the piece can kill the king if they had a clear path w/o the protector
                        bool kingInPath = false;

                        int i;
                        int j;

                        for (i = y - 1; i > -1; i--)
                        {
                            if (king.X == x && king.Y == i)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i) && (protector.X != x || protector.Y != i))
                            {
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i) && protector.X == x && protector.Y == i)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.Color, x, i))
                            {
                                break;
                            }
                        }

                        if (Protected && kingInPath)
                        {
                            return piece;
                        }
                        else
                        {
                            Protected = false;
                            kingInPath = false;
                        }

                        //below (i represents y/row)
                        for (i = y + 1; i < 8; i++)
                        {
                            if (king.X == x && king.Y == i)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i) && (protector.X != x || protector.Y != i))
                            {
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.Color, x, i) && protector.X == x && protector.Y == i)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.Color, x, i))
                            {
                                break;
                            }
                        }

                        if (Protected && kingInPath)
                        {
                            return piece;
                        }
                        else
                        {
                            Protected = false;
                            kingInPath = false;
                        }

                        //left (i represents x/column)
                        for (i = x - 1; i > -1; i--)
                        {
                            if (king.X == i && king.Y == y)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y) && (protector.X != i || protector.Y != y))
                            {
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y) && protector.X == i && protector.Y == y)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.Color, i, y))
                            {
                                break;
                            }
                        }

                        if (Protected && kingInPath)
                        {
                            return piece;
                        }
                        else
                        {
                            Protected = false;
                            kingInPath = false;
                        }

                        //right (i represents x/column)
                        for (i = x + 1; i < 8; i++)
                        {
                            if (king.X == i && king.Y == y)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y) && (protector.X != i || protector.Y != y))
                            {
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.Color, i, y) && protector.X == i && protector.Y == y)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.Color, i, y))
                            {
                                break;
                            }
                        }

                        if (Protected && kingInPath)
                        {
                            return piece;
                        }
                        else
                        {
                            Protected = false;
                            kingInPath = false;
                        }

                        //up right
                        for (i = x + 1, j = y - 1; i < 8; i++, j--)
                        {
                            if (j > 7 || j < 0)
                            {
                                break;
                            }

                            if (king.X == i && king.Y == j)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && (protector.X != i || protector.Y != j))
                            {
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && protector.X == i && protector.Y == j)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                            {
                                break;
                            }
                        }

                        if (Protected && kingInPath)
                        {
                            return piece;
                        }
                        else
                        {
                            Protected = false;
                            kingInPath = false;
                        }

                        //down right
                        for (i = x + 1, j = y + 1; i < 8; i++, j++)
                        {
                            if (j > 7 || j < 0)
                            {
                                break;
                            }

                            if (king.X == i && king.Y == j)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && (protector.X != i || protector.Y != j))
                            {
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && protector.X == i && protector.Y == j)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                            {
                                break;
                            }
                        }

                        if (Protected && kingInPath)
                        {
                            return piece;
                        }
                        else
                        {
                            Protected = false;
                            kingInPath = false;
                        }

                        //down left
                        for (i = x - 1, j = y + 1; i > -1; i--, j++)
                        {
                            if (j > 7 || j < 0)
                            {
                                break;
                            }

                            if (king.X == i && king.Y == j)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && (protector.X != i || protector.Y != j))
                            {
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && protector.X == i && protector.Y == j)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                            {
                                break;
                            }
                        }

                        if (Protected && kingInPath)
                        {
                            return piece;
                        }
                        else
                        {
                            Protected = false;
                            kingInPath = false;
                        }

                        //up left
                        for (i = x - 1, j = y - 1; i > -1; i--, j--)
                        {
                            if (j > 7 || j < 0)
                            {
                                break;
                            }

                            if (king.X == i && king.Y == j)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && (protector.X != i || protector.Y != j))
                            {
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.Color, i, j) && protector.X == i && protector.Y == j)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.Color, i, j))
                            {
                                break;
                            }
                        }

                        if (Protected && kingInPath)
                        {
                            return piece;
                        }
                        else
                        {
                            Protected = false;
                            kingInPath = false;
                        }
                    }
                }
            }
            return new Piece();
        }

        /// <summary>
        /// flips the board for each turn, the player can play with their side on the bottom like normal chess. This is so that its easier for black to play without having to turn the screen upside down
        /// </summary>
        public void flipBoard()
        {
            foreach (Piece piece in Pieces)
            {
                piece.X = 7 - piece.X;
                piece.Y = 7 - piece.Y;
            }
            foreach (DataGridViewRow row in board.Rows)
            {
                if (row.HeaderCell.Value.ToString() == "1")
                {
                    row.HeaderCell.Value = "8";
                }
                else if (row.HeaderCell.Value.ToString() == "2")
                {
                    row.HeaderCell.Value = "7";
                }
                else if (row.HeaderCell.Value.ToString() == "3")
                {
                    row.HeaderCell.Value = "6";
                }
                else if (row.HeaderCell.Value.ToString() == "4")
                {
                    row.HeaderCell.Value = "5";
                }
                else if (row.HeaderCell.Value.ToString() == "5")
                {
                    row.HeaderCell.Value = "4";
                }
                else if (row.HeaderCell.Value.ToString() == "6")
                {
                    row.HeaderCell.Value = "3";
                }
                else if (row.HeaderCell.Value.ToString() == "7")
                {
                    row.HeaderCell.Value = "2";
                }
                else if (row.HeaderCell.Value.ToString() == "8")
                {
                    row.HeaderCell.Value = "1";
                }

                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Value = null;
                }
            }
            foreach (Piece piece in Pieces)
            {
                board.Rows[piece.Y].Cells[piece.X].Value = piece.Icon;
            }
            foreach (DataGridViewColumn col in board.Columns)
            {
                if (col.HeaderText == "A")
                {
                    col.HeaderText = "H";
                }
                else if (col.HeaderText == "B")
                {
                    col.HeaderText = "G";
                }
                else if (col.HeaderText == "C")
                {
                    col.HeaderText = "F";
                }
                else if (col.HeaderText == "D")
                { 
                    col.HeaderText = "E";
                }
                else if (col.HeaderText == "E")
                {
                    col.HeaderText = "D";
                }
                else if (col.HeaderText == "F")
                {
                    col.HeaderText = "C";
                }
                else if (col.HeaderText == "G")
                {
                    col.HeaderText = "B";
                }
                else if (col.HeaderText == "H")
                {
                    col.HeaderText = "A";
                }
            }
        }



        private void startButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(timerBox.Text, out int a))
            {
                titleLabel.Hide();
                timerLabel.Hide();
                timerBox.Hide();
                minuteLabel.Hide();
                startButton.Hide();

                whiteMoveBox.Show();
                blackMoveBox.Show();
                resignButton.Show();
                offerDrawButton.Show();
                turnLabelText.Show();
                turnLabel.Show();
                checkLabel.Show();
                winnerLabel.Show();

                startGame = true;
            }
            else
            {
                MessageBox.Show("Make sure only numbers are in the timer box and moves box");
            }

            if (int.Parse(timerBox.Text) != 0)
            {
                timeLabel.Show();
                whiteTimeLabel.Show();
                blackTimeLabel.Show();
                blackTime.Show();
                whiteTime.Show();
                timing = true;
                int time = int.Parse(timerBox.Text) * 60;
                
                whiteCounter = time;
                blackCounter = time;
                whiteTimer.Start();
                whiteTime.Text = whiteCounter / 60 + ":" + ((whiteCounter % 60 < 10) ? ("0" + (whiteCounter % 60).ToString()) : (whiteCounter % 60).ToString());
                blackTime.Text = blackCounter / 60 + ":" + ((blackCounter % 60 < 10) ? ("0" + (blackCounter % 60).ToString()) : (blackCounter % 60).ToString());
            }
        }

        private void whiteTimer_Tick(object sender, EventArgs e)
        {
            whiteCounter--;
            //Win condition if white runs out of time
            if (whiteCounter == 0)
            {
                whiteTimer.Stop();
                endGame(Color.Black);
            }
            whiteTime.Text = whiteCounter / 60 + ":" + ((whiteCounter % 60 < 10) ? ("0" + (whiteCounter % 60).ToString()) : (whiteCounter % 60).ToString());
        }

        private void blackTimer_Tick(object sender, EventArgs e)
        {
            blackCounter--;
            //Win condition if black runs out of time
            if (blackCounter == 0)
            {
                blackTimer.Stop();
                endGame(Color.Black);
            }
            blackTime.Text = blackCounter / 60 + ":" + ((blackCounter%60<10) ? ("0" + (blackCounter % 60).ToString()): (blackCounter % 60).ToString());
        }

        private void resignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            endGame(turn ? Color.Black:Color.White);
        }

        private void offerADrawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gameEnd)
            {
                return;
            }
            else
            {
                changeTurn();
                drawLabel.Show();
                drawButton.Show();
            }

        }

        private void drawButton_Click(object sender, EventArgs e)
        {
            endGame(turn ? Color.White:Color.Black);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!startGame || gameEnd)
            {
                return;
            }
            if (e.Button == MouseButtons.Right)
            {
                mouseMenu.Show(e.X,e.Y);
            }
            else if (e.Button == MouseButtons.Left && mouseMenu.Visible == true)
            {
                mouseMenu.Hide();
            }
        }

        private void resignButton_Click(object sender, EventArgs e)
        {
            if (gameEnd)
            {
                return;
            }
            else
            {
                endGame(turn ? Color.Black : Color.White);
            }
        }

        private void offerDrawButton_Click(object sender, EventArgs e)
        {
            if (gameEnd)
            {
                return;
            }
            else
            {
                changeTurn();
                drawLabel.Show();
                drawButton.Show();
            }
        }

        #region accidents
        private void board_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
        private void board_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
        private void board_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }



        #endregion
    }
}
