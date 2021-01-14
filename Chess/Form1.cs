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
        static List<Piece> pieces = new List<Piece>();

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
        /// Stores the last move
        /// </summary>
        String[] lastMove = new string[4];

        /// <summary>
        /// turns true if en Passant is selected
        /// </summary>
        bool enPassant = false;

        /// <summary>
        /// selected piece
        /// </summary>
        static Piece selected;

        /// <summary>
        /// 0 = checkmate for white
        /// 1 = checkmate for black
        /// </summary>
        static bool[] check = { false, false };

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
            #region ADD PIECES TO ARRAY
            pieces.Add(new Piece("pawn", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wpawn.png"), 0, 6));
            pieces.Add(new Piece("pawn", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wpawn.png"), 1, 6));
            pieces.Add(new Piece("pawn", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wpawn.png"), 2, 6));
            pieces.Add(new Piece("pawn", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wpawn.png"), 3, 6));
            pieces.Add(new Piece("pawn", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wpawn.png"), 4, 6));
            pieces.Add(new Piece("pawn", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wpawn.png"), 5, 6));
            pieces.Add(new Piece("pawn", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wpawn.png"), 6, 6));
            pieces.Add(new Piece("pawn", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wpawn.png"), 7, 6));

            pieces.Add(new Piece("pawn", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bpawn.png"), 0, 1));
            pieces.Add(new Piece("pawn", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bpawn.png"), 1, 1));
            pieces.Add(new Piece("pawn", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bpawn.png"), 2, 1));
            pieces.Add(new Piece("pawn", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bpawn.png"), 3, 1));
            pieces.Add(new Piece("pawn", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bpawn.png"), 4, 1));
            pieces.Add(new Piece("pawn", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bpawn.png"), 5, 1));
            pieces.Add(new Piece("pawn", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bpawn.png"), 6, 1));
            pieces.Add(new Piece("pawn", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bpawn.png"), 7, 1));

            pieces.Add(new Piece("rook", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wrook.png"), 0, 7));
            pieces.Add(new Piece("rook", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wrook.png"), 7, 7));

            pieces.Add(new Piece("rook", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Brook.png"), 0, 0));
            pieces.Add(new Piece("rook", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Brook.png"), 7, 0));

            pieces.Add(new Piece("horse", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Whorse.png"), 1, 7));
            pieces.Add(new Piece("horse", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Whorse.png"), 6, 7));

            pieces.Add(new Piece("horse", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bhorse.png"), 1, 0));
            pieces.Add(new Piece("horse", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bhorse.png"), 6, 0));

            pieces.Add(new Piece("bishop", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wbishop.png"), 2, 7));
            pieces.Add(new Piece("bishop", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wbishop.png"), 5, 7));

            pieces.Add(new Piece("bishop", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bbishop.png"), 2, 0));
            pieces.Add(new Piece("bishop", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bbishop.png"), 5, 0));

            pieces.Add(new Piece("queen", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wqueen.png"), 3, 7));
            pieces.Add(new Piece("queen", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bqueen.png"), 3, 0));

            pieces.Add(new Piece("king", Color.White, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wking.png"), 4, 7));
            pieces.Add(new Piece("king", Color.Black, Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bking.png"), 4, 0));
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
            foreach (Piece piece in pieces)
            {
                board.Rows[piece.y].Cells[piece.x].Value = piece.icon;
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

            //move a piece if the board is highlighted
            if (board.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == Color.Blue)
            {
                foreach (Piece piece in pieces)
                {
                    //if piece matches the selected piece
                    if (selected.x == piece.x && selected.y == piece.y && preventCheck(piece).name == "")
                    {
                        //if the piece is not in currently in check
                        if ((piece.color == Color.White && !check[0]) || (piece.color == Color.Black && !check[1]))
                        {
                            int checkd = 0;
                            //check for double move for en pessant
                            lastMove[0] = piece.name;
                            if (piece.name == "pawn" && piece.y - e.RowIndex == 2)
                            {
                                lastMove[1] = "double move";
                            }
                            else
                            {
                                lastMove[1] = "move";
                            }
                            lastMove[2] = e.ColumnIndex.ToString();
                            lastMove[3] = e.RowIndex.ToString();
                            board.Rows[piece.y].Cells[piece.x].Value = null;
                            piece.x = e.ColumnIndex;
                            piece.y = e.RowIndex;
                            board.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = piece.icon;
                            piece.moved = true;
                            ogColor();
                            //if there are threats to the king, that side is in check
                            if (getCheckTiles(turn ? Color.White : Color.Black)[0].Count != 0)
                            {
                                checkd = 1;
                                check[turn ? 1 : 0] = true;
                                checkLabel.Text = "In Check: " + (turn ? "Black" : "White");

                                //see if pieces can protect the king or the king can escape
                                bool movable = false;
                                foreach (Piece protect in pieces)
                                {
                                    if (protect.color == (turn ? Color.Black : Color.White))
                                    {
                                        highlightSpaces(protect, true);
                                    }
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
                                whiteMoveBox.AppendText("\r\n"+move(piece.name, piece.x, piece.y, false, checkd, 0, false));
                            }
                            else
                            {
                                blackMoveBox.AppendText("\r\n"+move(piece.name, piece.x, piece.y, false, checkd, 0, false));
                            }

                            changeTurn();
                            break;
                        }
                        //else if the piece is in check
                        else if ((piece.color == Color.White && check[0]) || (piece.color == Color.Black && check[1]))
                        {
                            int checkd = 0;
                            lastMove[0] = piece.name;
                            if (piece.name == "pawn" && piece.y - e.RowIndex == 2)
                            {
                                lastMove[1] = "double move";
                            }
                            else
                            {
                                lastMove[1] = "move";
                            }
                            lastMove[2] = e.ColumnIndex.ToString();
                            lastMove[3] = e.RowIndex.ToString();
                            board.Rows[piece.y].Cells[piece.x].Value = null;
                            piece.x = e.ColumnIndex;
                            piece.y = e.RowIndex;
                            board.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = piece.icon;
                            piece.moved = true;
                            ogColor();
                            //if there are threats to the other king, that side is in check
                            if (getCheckTiles(turn ? Color.White : Color.Black)[0].Count != 0)
                            {
                                checkd = 1;
                                check[turn ? 1 : 0] = true;
                                checkLabel.Text = "In Check: " + (turn ? "Black" : "White");

                                //see if pieces can protect the king or the king can escape
                                bool movable = false;
                                foreach (Piece protect in pieces)
                                {
                                    if (protect.color == (turn ? Color.Black : Color.White))
                                    {
                                        highlightSpaces(protect, true);
                                    }
                                }

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
                            //if the current side is in check and managed to cover there king, they are no longer in check
                            else if (getCheckTiles(turn ? Color.White : Color.Black)[0].Count == 0 && check[turn ? 0 : 1] == true)
                            {
                                check[turn ? 0 : 1] = false;
                                checkLabel.Text = "In Check:";
                            }

                            if (turn)
                            {
                                whiteMoveBox.AppendText("\r\n" + move(piece.name, piece.x, piece.y, false, checkd, 0, false));
                            }
                            else
                            {
                                blackMoveBox.AppendText("\r\n" + move(piece.name, piece.x, piece.y, false, checkd, 0, false));
                            }

                            changeTurn();
                            break;
                        }
                    }
                }
            }
            //kill a piece
            else if (board.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == Color.Red)
            {
                Piece remove = new Piece();
                //finds the peice beign removed
                if (enPassant)
                {
                    foreach (Piece piece in pieces)
                    {
                        if (e.ColumnIndex == piece.x && e.RowIndex == piece.y - 1)
                        {
                            remove = piece;
                            break;
                        }
                    }
                    enPassant = false;
                }
                else
                {
                    foreach (Piece piece in pieces)
                    {
                        if (e.ColumnIndex == piece.x && e.RowIndex == piece.y)
                        {
                            remove = piece;
                            break;
                        }
                    }
                }

                foreach (Piece piece in pieces)
                {
                    if (selected.x == piece.x && selected.y == piece.y && (preventCheck(piece).name == "" || (preventCheck(piece).name != "" && !checkForACheck(piece,remove.x,remove.y,true))))
                    {
                        lastMove[0] = piece.name;
                        lastMove[1] = "kill";
                        lastMove[2] = e.ColumnIndex.ToString();
                        lastMove[3] = e.RowIndex.ToString();
                        board.Rows[remove.y].Cells[remove.x].Value = null;
                        pieces.Remove(remove);

                        board.Rows[piece.y].Cells[piece.x].Value = null;

                        piece.x = e.ColumnIndex;
                        piece.y = e.RowIndex;
                        board.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = piece.icon;

                        piece.moved = true;

                        enPassant = false;
                        //if the piece is not in currently in check
                        if ((piece.color == Color.White && !check[0]) || (piece.color == Color.Black && !check[1]))
                        {
                            ogColor();
                            if (getCheckTiles(turn ? Color.White : Color.Black)[0].Count != 0)
                            {
                                if (turn)
                                {
                                    whiteMoveBox.AppendText("\r\n"+move(piece.name, piece.x, piece.y, true, 1, 0, false));
                                }
                                else
                                {
                                    blackMoveBox.AppendText("\r\n"+move(piece.name, piece.x, piece.y, true, 1, 0, false));
                                }
                                check[turn ? 1 : 0] = true;
                                checkLabel.Text = "In Check: " + (turn ? "Black" : "White");
                            }
                            if (turn)
                            {
                                whiteMoveBox.AppendText("\r\n"+move(piece.name, piece.x, piece.y, true, 0, 0, false));
                            }
                            else
                            {
                                blackMoveBox.AppendText("\r\n"+move(piece.name, piece.x, piece.y, true, 0, 0, false));
                            }
                            changeTurn();
                            break;
                        }
                        else if ((piece.color == Color.White && check[0]) || (piece.color == Color.Black && check[1]))
                        {
                            ogColor();
                            if (getCheckTiles(turn ? Color.White : Color.Black)[0].Count == 0)
                            {
                                if (turn)
                                {
                                    whiteMoveBox.AppendText("\r\n"+move(piece.name, piece.x, piece.y, true, 0, 0, false));
                                }
                                else
                                {
                                    blackMoveBox.AppendText("\r\n"+move(piece.name, piece.x, piece.y, true, 0, 0, false));
                                }
                                check[piece.color == Color.White ? 0 : 1] = false;
                                checkLabel.Text = "In Check: ";
                                changeTurn();
                                break;
                            }
                            else
                            {
                                if (turn)
                                {
                                    whiteMoveBox.AppendText("\r\n"+move(piece.name, piece.x, piece.y, true, 2, 0, false));
                                }
                                else
                                {
                                    blackMoveBox.AppendText("\r\n"+move(piece.name, piece.x, piece.y, true, 2, 0, false));
                                }
                                endGame(turn ? Color.Black : Color.White);
                            }
                            return;
                        }
                    }
                }
            }
            //castling
            else if (board.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == Color.Green)
            {
                //find the rook
                Piece rook = new Piece();
                foreach (Piece piece in pieces)
                {
                    if (piece.x == e.ColumnIndex && piece.y == e.RowIndex)
                    {
                        rook = piece;
                    }
                }

                //find the king
                Piece king = new Piece();
                foreach (Piece piece in pieces)
                {
                    if (rook.color == piece.color && piece.name == "king")
                    {
                        king = piece;
                    }
                }

                if (king.color == Color.White)
                {
                    //Check to see what king of castle is beign done
                    bool kingSide = false;
                    if (king.x + 3 == rook.x)
                    {
                        kingSide = true;
                    }

                    if (kingSide)
                    {
                        if (rook.color == Color.White)
                        {
                            board.Rows[king.y].Cells[king.x].Value = null;
                            board.Rows[rook.y].Cells[rook.x].Value = null;
                            board.Rows[king.y].Cells[king.x + 2].Value = king.icon;
                            board.Rows[rook.y].Cells[rook.x - 2].Value = rook.icon;
                            king.x += 2;
                            rook.x -= 2;
                            whiteMoveBox.AppendText("\r\n" + move("", -1, -1, false, 0, 1, false));
                        }
                        else
                        {
                            board.Rows[king.y].Cells[king.x].Value = null;
                            board.Rows[rook.y].Cells[rook.x].Value = null;
                            board.Rows[king.y].Cells[king.x - 2].Value = king.icon;
                            board.Rows[rook.y].Cells[rook.x + 2].Value = rook.icon;
                            king.x -= 2;
                            rook.x += 2;
                            blackMoveBox.AppendText("\r\n" + move("", -1, -1, false, 0, 1, false));
                        }
                    }
                    else
                    {
                        if (rook.color == Color.White)
                        {
                            board.Rows[king.y].Cells[king.x].Value = null;
                            board.Rows[rook.y].Cells[rook.x].Value = null;
                            board.Rows[king.y].Cells[king.x - 2].Value = king.icon;
                            board.Rows[rook.y].Cells[rook.x + 3].Value = rook.icon;
                            king.x -= 2;
                            rook.x += 3;
                            whiteMoveBox.AppendText("\r\n" + move("", -1, -1, false, 0, 2, false));

                        }
                        else
                        {
                            board.Rows[king.y].Cells[king.x].Value = null;
                            board.Rows[rook.y].Cells[rook.x].Value = null;
                            board.Rows[king.y].Cells[king.x + 2].Value = king.icon;
                            board.Rows[rook.y].Cells[rook.x - 3].Value = rook.icon;
                            king.x += 2;
                            rook.x -= 3;
                            blackMoveBox.AppendText("\r\n" + move("", -1, -1, false, 0, 2, false));
                        }
                    }
                }
                else
                {
                    //Check to see what king of castle is beign done
                    bool kingSide = false;
                    if (king.x - 3 == rook.x)
                    {
                        kingSide = true;
                    }

                    if (kingSide)
                    {
                        if (rook.color == Color.White)
                        {
                            lastMove[0] = king.name;
                            lastMove[1] = "castle";
                            lastMove[2] = king.x.ToString();
                            lastMove[3] = king.y.ToString(); 
                            board.Rows[king.y].Cells[king.x].Value = null;
                            board.Rows[rook.y].Cells[rook.x].Value = null;
                            board.Rows[king.y].Cells[king.x + 2].Value = king.icon;
                            board.Rows[rook.y].Cells[rook.x - 2].Value = rook.icon;
                            king.x += 2;
                            rook.x -= 2;
                            whiteMoveBox.AppendText("\r\n" + move("", -1, -1, false, 0, 1, false));
                        }
                        else
                        {
                            lastMove[0] = king.name;
                            lastMove[1] = "castle";
                            lastMove[2] = king.x.ToString();
                            lastMove[3] = king.y.ToString(); board.Rows[king.y].Cells[king.x].Value = null;
                            board.Rows[rook.y].Cells[rook.x].Value = null;
                            board.Rows[king.y].Cells[king.x - 2].Value = king.icon;
                            board.Rows[rook.y].Cells[rook.x + 2].Value = rook.icon;
                            king.x -= 2;
                            rook.x += 2;
                            blackMoveBox.AppendText("\r\n" + move("", -1, -1, false, 0, 1, false));
                        }
                    }
                    else
                    {
                        if (rook.color == Color.White)
                        {
                            lastMove[0] = king.name;
                            lastMove[1] = "castle";
                            lastMove[2] = king.x.ToString();
                            lastMove[3] = king.y.ToString(); board.Rows[king.y].Cells[king.x].Value = null;
                            board.Rows[rook.y].Cells[rook.x].Value = null;
                            board.Rows[king.y].Cells[king.x + 2].Value = king.icon;
                            board.Rows[rook.y].Cells[rook.x - 3].Value = rook.icon;
                            king.x += 2;
                            rook.x -= 3;
                            whiteMoveBox.AppendText("\r\n" + move("", -1, -1, false, 0, 2, false));

                        }
                        else
                        {
                            lastMove[0] = king.name;
                            lastMove[1] = "castle";
                            lastMove[2] = king.x.ToString();
                            lastMove[3] = king.y.ToString(); board.Rows[king.y].Cells[king.x].Value = null;
                            board.Rows[rook.y].Cells[rook.x].Value = null;
                            board.Rows[king.y].Cells[king.x - 2].Value = king.icon;
                            board.Rows[rook.y].Cells[rook.x + 3].Value = rook.icon;
                            king.x -= 2;
                            rook.x += 3;
                            blackMoveBox.AppendText("\r\n" + move("", -1, -1, false, 0, 2, false));
                        }
                    }
                }

                ogColor();
                changeTurn();
            }
            //Promotion //TODO Add support for other piece options
            else if(board.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == Color.Purple)
            {
                foreach (Piece piece in pieces)
                {
                    if (piece.x == selected.x && piece.y == selected.y && preventCheck(piece).name == "")
                    {
                        if (turn && piece.color == Color.White)
                        {
                            piece.icon = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Wqueen.png");
                            piece.name = "queen";
                        }
                        else
                        {
                            piece.icon = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Resources\\Bqueen.png");
                            piece.name = "queen";
                        }

                        Piece remove = new Piece();
                        //check to see if there is a piece in the way that will be killed
                        foreach (Piece kill in pieces)
                        {
                            if (kill.color != piece.color && kill.x == e.ColumnIndex && kill.y == e.RowIndex)
                            {
                                remove = kill;
                                break;
                            }
                        }
                        if (remove.x != -1)
                        {
                            pieces.Remove(remove);
                        }

                        int checkd = 0;
                        lastMove[0] = piece.name;
                        lastMove[1] = "promotion";
                        lastMove[2] = e.ColumnIndex.ToString();
                        lastMove[3] = e.RowIndex.ToString(); 
                        board.Rows[piece.y].Cells[piece.x].Value = null;
                        piece.x = e.ColumnIndex;
                        piece.y = e.RowIndex;
                        board.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = piece.icon;
                        piece.moved = true;
                        ogColor();

                        //if there are threats to the king, that side is in check
                        if (getCheckTiles(turn ? Color.White : Color.Black)[0].Count != 0)
                        {
                            checkd = 1;
                            check[turn ? 1 : 0] = true;
                            checkLabel.Text = "In Check: " + (turn ? "Black" : "White");

                            //see if pieces can protect the king or the king can escape
                            bool movable = false;
                            foreach (Piece protect in pieces)
                            {
                                if (protect.color == (turn ? Color.Black : Color.White))
                                {
                                    highlightSpaces(protect, true);
                                }
                            }

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
                            whiteMoveBox.AppendText("\r\n" + move(piece.name, piece.x, piece.y, false, checkd, 0, true));
                        }
                        else
                        {
                            blackMoveBox.AppendText("\r\n" + move(piece.name, piece.x, piece.y, false, checkd, 0, true));
                        }

                        changeTurn();
                        break;
                    }
                }
            }
            //Just an empty if to handle accidentally clicking on an empty cell
            else if (board.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
            {

            }
            //show possible moves
            else
            {
                ogColor();
                selected = null;
                foreach (Piece piece in pieces)
                {
                    if (piece.color == (turn ? Color.White : Color.Black))
                    {
                        if (piece.x == e.ColumnIndex && piece.y == e.RowIndex && preventCheck(piece).name == "")
                        {
                            highlightSpaces(piece, check[(piece.color == Color.White) ? 0 : 1]);
                            selected = piece;
                            break;
                        }
                        else if (piece.x == e.ColumnIndex && piece.y == e.RowIndex && preventCheck(piece).name != "")
                        {
                            highlightSpaces(piece, false);
                            selected = piece;
                            break;
                        }
                    }
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
        /// Checks if there is a stalemate
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public bool checkStale(Color color)
        {
            foreach (Piece piece in pieces)
            {
                if (piece.color == color)
                {
                    highlightSpaces(piece, false);
                }
            }

            foreach (DataGridViewRow row in board.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Style.BackColor == Color.Blue || cell.Style.BackColor == Color.Red || cell.Style.BackColor == Color.Green || cell.Style.BackColor == Color.Purple)
                    {
                        ogColor();
                        return false;
                    }
                }
            }
            ogColor();
            return true;
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
        public String move(String piece, int x, int y, bool kill, int check, int castle, bool promotion)
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
        /// Returns a 2 array lists. 
        /// The first list shows the spaces the team in check must cover to protect the king and get out of check. 
        /// The second list shows the spaces the king cannot move to since that would result in the kings death during the next turn
        /// int[0] = x
        /// int[1] = y
        /// int[2] = (true = has enemy, false = empty)
        /// </summary>
        /// <param name="attack">the team attacking (the team not in check)</param>
        /// <returns></returns>
        public List<int[]>[] getCheckTiles(Color attack)
        {
            Piece king = new Piece();
            foreach (Piece piece in pieces)
            {
                if (piece.name == "king" && piece.color != attack)
                {
                    king = piece;
                    break;
                }
            }

            // int[0] == x cord
            // int[1] == y cord
            // int[2] == (1 == piece) (0 == space)
            //tiles that are curently threatening the king
            List<int[]> threats = new List<int[]>();

            //tiles that the enemy can move and kill if you go there next turn (FOR THE KING)
            List<int[]> future = new List<int[]>();

            foreach (Piece piece in pieces)
            {
                //checks if piece is attacking
                if (piece.color == attack)
                {
                    int x = piece.x;
                    int y = piece.y;

                    //if the pawn can kill the king, the cord of the pawn in recorded as necessary to kill
                    //if the pawn kill tile is empty, it is marked as a future threat
                    if (piece.name == "pawn" && piece.color == Color.White && attack == Color.White)
                    {
                        if (turn)
                        {
                            if (x - 1 > -1 && y - 1 > -1)
                            {
                                if (king.x == x - 1 && king.y == y - 1)
                                {
                                    threats.Add(new int[] { x, y, 1 });
                                    future.Add(new int[] { x - 1, y - 1, 0 });
                                }
                                else
                                {
                                    future.Add(new int[] { x - 1, y - 1, 0 });
                                }
                            }
                            if (x + 1 < 8 && y - 1 > -1)
                            {
                                if (king.x == x + 1 && king.y == y - 1)
                                {
                                    threats.Add(new int[] { x, y, 1 });
                                    future.Add(new int[] { x + 1, y - 1, 0 });
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
                                if (king.x == x - 1 && king.y == y + 1)
                                {
                                    threats.Add(new int[] { x, y, 1 });
                                    future.Add(new int[] { x - 1, y + 1, 0 });
                                }
                                else
                                {
                                    future.Add(new int[] { x - 1, y + 1, 0 });
                                }
                            }
                            if (x + 1 < 8 && y + 1 < 8)
                            {
                                if (king.x == x + 1 && king.y == y + 1)
                                {
                                    threats.Add(new int[] { x, y, 1 });
                                    future.Add(new int[] { x + 1, y + 1, 0 });
                                }
                                else
                                {
                                    future.Add(new int[] { x + 1, y + 1, 0 });
                                }
                            }
                        }
                        
                    }
                    else if (piece.name == "pawn" && piece.color == Color.Black && attack == Color.Black)
                    {
                        if (turn)
                        {
                            if (x - 1 > -1 && y + 1 < 8)
                            {
                                if (king.x == x - 1 && king.y == y + 1)
                                {
                                    threats.Add(new int[] { x, y, 1 });
                                    future.Add(new int[] { x - 1, y + 1, 0 });
                                }
                                else
                                {
                                    future.Add(new int[] { x - 1, y + 1, 0 });
                                }
                            }
                            if (x + 1 < 8 && y + 1 < 8)
                            {
                                if (king.x == x + 1 && king.y == y + 1)
                                {
                                    threats.Add(new int[] { x, y, 1 });
                                    future.Add(new int[] { x + 1, y + 1, 0 });
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
                                if (king.x == x - 1 && king.y == y - 1)
                                {
                                    threats.Add(new int[] { x, y, 1 });
                                    future.Add(new int[] { x - 1, y - 1, 0 });
                                }
                                else
                                {
                                    future.Add(new int[] { x - 1, y - 1, 0 });
                                }
                            }
                            if (x + 1 < 8 && y - 1 > -1)
                            {
                                if (king.x == x + 1 && king.y == y - 1)
                                {
                                    threats.Add(new int[] { x, y, 1 });
                                    future.Add(new int[] { x + 1, y - 1, 0 });
                                }
                                else
                                {
                                    future.Add(new int[] { x + 1, y - 1, 0 });
                                }
                            }
                        }
                    }
                    //if the blue spots lead to a kill on the king, the possible tiles in List(possible) are added to tiles
                    else if (piece.name == "rook")
                    {
                        //lits of possible spaces the rook can go to kill the king
                        List<int[]> possible = new List<int[]>();

                        //adding rook cords
                        possible.Add(new int[] { x, y, 1 });

                        //above
                        for (int i = y - 1; i > -1; i--)
                        {
                            if (board.Rows[i].Cells[x].Value == null)
                            {
                                possible.Add(new int[] { x, i, 0 });
                                future.Add(new int[] { x, i, 0 });
                            }
                            else if (king.x == x && king.y == i)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { x, i, 0 });
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i))
                            {
                                future.Add(new int[] { x, i, 0 });
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                            else if (king.x == x && king.y == i)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { x, i, 0 });
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i))
                            {
                                future.Add(new int[] { x, i, 0 });
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                            else if (king.x == i && king.y == y)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { i, y, 0 });
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y))
                            {
                                future.Add(new int[] { i, y, 0 });
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
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
                            else if (king.x == i && king.y == y)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { i, y, 0 });
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y))
                            {
                                future.Add(new int[] { i, y, 0 });
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
                            {
                                future.Add(new int[] { i, y, 0 });
                                break;
                            }
                        }
                        possible.Clear();
                    }
                    //if the horse can kill the king, the cords of the horse are recorded as necessary to kill
                    else if (piece.name == "horse")
                    {
                        //up left
                        if (y - 2 > -1 && x - 1 > -1)
                        {
                            if (king.x == x - 1 && king.y == y - 2)
                            {
                                threats.Add(new int[] {x, y, 1});
                            }
                            else
                            {
                                future.Add(new int[] {x - 1, y - 2, 0});
                            }
                        }

                        //up right
                        if (y - 2 > -1 && x + 1 < 8 )
                        {
                            if (king.x == x + 1 && king.y == y - 2)
                            {
                                threats.Add(new int[] { x, y, 1 });
                            }
                            else
                            {
                                future.Add(new int[] { x + 1, y - 2, 0 });
                            }
                        }

                        //right up
                        if (y - 1 > -1 && x + 2 < 8)
                        {
                            if (king.x == x + 2 && king.y == y - 1)
                            {
                                threats.Add(new int[] { x, y, 1 });
                            }
                            else
                            {
                                future.Add(new int[] { x + 2, y - 1, 0 });
                            }
                        }

                        //right down
                        if (y + 1 < 8 && x + 2 < 8)
                        {
                            if (king.x == x + 2 && king.y == y + 1)
                            {
                                threats.Add(new int[] { x, y, 1 });
                            }
                            else
                            {
                                future.Add(new int[] { x + 2, y + 1, 0 });
                            }
                        }

                        //down left
                        if (y + 2 < 8 && x - 1 > -1)
                        {
                            if (king.x == x - 1 && king.y == y + 2)
                            {
                                threats.Add(new int[] { x, y, 1 });
                            }
                            else
                            {
                                future.Add(new int[] { x - 1, y + 2, 0 });
                            }
                        }

                        //down right
                        if (y + 2 < 8 && x + 1 < 8)
                        {
                            if (king.x == x + 1 && king.y == y + 2)
                            {
                                threats.Add(new int[] { x, y, 1 });
                            }
                            else
                            {
                                future.Add(new int[] { x + 1, y + 2, 0 });
                            }
                        }

                        //left up
                        if (y + 1 < 8 && x - 2 > -1)
                        {
                            if (king.x == x - 2 && king.y == y + 1)
                            {
                                threats.Add(new int[] { x, y, 1 });
                            }
                            else
                            {
                                future.Add(new int[] { x - 2, y + 1, 0 });
                            }
                        }

                        //left down
                        if (y - 1 > -1 && x - 2 > -1)
                        {
                            if (king.x == x - 2 && king.y == y - 1)
                            {
                                threats.Add(new int[] { x, y, 1 });
                            }
                            else
                            {
                                future.Add(new int[] { x - 2, y - 1, 0 });
                            }
                        }
                    }
                    else if (piece.name == "bishop")
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
                            else if (king.x == i && king.y == j)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] {i, j, 0});
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
                            {
                                future.Add(new int[] { i, j, 0 });
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
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
                            else if (king.x == i && king.y == j)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { i, j, 0 });
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
                            {
                                future.Add(new int[] { i, j, 0 });
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                            else if (king.x == i && king.y == j)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { i, j, 0 });
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
                            {
                                future.Add(new int[] { i, j, 0 });
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                            else if (king.x == i && king.y == j)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { i, j, 0 });
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
                            {
                                future.Add(new int[] { i, j, 0 });
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
                            {
                                future.Add(new int[] { i, j, 0 });
                                break;
                            }
                        }
                        possible.Clear();
                    }
                    else if (piece.name == "queen")
                    {
                        //x
                        int i;

                        //y
                        int j;

                        //lits of possible spaces the rook can go to kill the king
                        List<int[]> possible = new List<int[]>();

                        bool found = false;

                        //adding bishop cords
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
                            else if (king.x == i && king.y == j)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { i, j, 0 });
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
                            {
                                future.Add(new int[] { i, j, 0 });
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                            if (j > 7 || j < 0)
                            {
                                break;
                            }

                            if (board.Rows[j].Cells[i].Value == null)
                            {
                                possible.Add(new int[] { i, j, 0 });
                                future.Add(new int[] { i, j, 0 });
                            }
                            else if (king.x == i && king.y == j)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { i, j, 0 });
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
                            {
                                future.Add(new int[] { i, j, 0 });
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                            else if (king.x == i && king.y == j)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { i, j, 0 });
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
                            {
                                future.Add(new int[] { i, j, 0 });
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                            else if (king.x == i && king.y == j)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { i, j, 0 });
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
                            {
                                future.Add(new int[] { x, i, 0 });
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
                            {
                                future.Add(new int[] { i, j, 0 });
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
                            else if (king.x == x && king.y == i)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { x, i, 0 });
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i))
                            {
                                future.Add(new int[] { x, i, 0 });
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                            else if (king.x == x && king.y == i)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { x, i, 0 });
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i))
                            {
                                future.Add(new int[] { x, i, 0 });
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                            else if (king.x == i && king.y == y)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { i, y, 0 });
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y))
                            {
                                future.Add(new int[] { i, y, 0 });
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
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
                            else if (king.x == i && king.y == y)
                            {
                                threats.AddRange(possible);
                                future.Add(new int[] { i, y, 0 });
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y))
                            {
                                future.Add(new int[] { i, y, 0 });
                                break;
                            }
                            else if (board.Rows[i].Cells[y].Value != null && !containsEnemy(piece.color, i, y))
                            {
                                future.Add(new int[] { i, y, 0 });
                                break;
                            }
                        }
                        possible.Clear();
                    }
                    else if (piece.name == "king")
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
            return new List<int[]>[] { threats, future };
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
                int x = piece.x;
                int y = piece.y;
                if (piece.name == "pawn" && ((piece.color == Color.White && turn) || (piece.color == Color.Black && !turn)))
                {
                    //left kill
                    if (x - 1 != -1 && y - 1 != -1 && board.Rows[y - 1].Cells[x - 1].Value != null && containsEnemy(piece.color, x - 1, y - 1) && !checkForACheck(piece, x - 1, y - 1,true))
                    {
                        board.Rows[y - 1].Cells[x - 1].Style.BackColor = Color.Red;
                    }

                    //right kill
                    if (x + 1 != 8 && y - 1 != -1 && board.Rows[y - 1].Cells[x + 1].Value != null && containsEnemy(piece.color, x + 1, y - 1) && !checkForACheck(piece,x+1,y-1,true))
                    {
                        board.Rows[y - 1].Cells[x + 1].Style.BackColor = Color.Red;
                    }

                    //left en Passant
                    if (x - 1 > -1 && y - 1 > -1 && board.Rows[y].Cells[x - 1].Value != null && board.Rows[y - 1].Cells[x - 1].Value == null && containsEnemy(piece.color, x - 1, y) && !checkForACheck(piece, x - 1, y - 1, true) && lastMove[0] == "pawn" && lastMove[1] == "double move" && int.Parse(lastMove[2]) == x - 1 && int.Parse(lastMove[3]) == y)
                    {
                        board.Rows[y - 1].Cells[x - 1].Style.BackColor = Color.Red;
                        //board.Rows[y - 1].Cells[x + 1].Style.BackColor = Color.FromArgb(255,1,1);
                        enPassant = true;
                    }

                    //right en Passant
                    if (x + 1 < 8 && y - 1 > -1 && board.Rows[y].Cells[x + 1].Value != null && board.Rows[y - 1].Cells[x + 1].Value == null && containsEnemy(piece.color, x + 1, y) && !checkForACheck(piece, x + 1, y - 1, true) && lastMove[0] == "pawn" && lastMove[1] == "double move" && int.Parse(lastMove[2]) == x + 1 && int.Parse(lastMove[3]) == y)
                    {
                        board.Rows[y - 1].Cells[x + 1].Style.BackColor = Color.Red;
                        //board.Rows[y - 1].Cells[x + 1].Style.BackColor = Color.FromArgb(255, 1, 1);
                        enPassant = true;
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
                    if (y - 1 == 0 && x + 1 < 8 && board.Rows[y - 1].Cells[x + 1].Value != null && containsEnemy(piece.color,x + 1,y - 1) && !checkForACheck(piece, x + 1, y - 1, false))
                    {
                        board.Rows[y - 1].Cells[x + 1].Style.BackColor = Color.Purple;
                    }

                    //promotion & kill right
                    if (y - 1 == 0 && x - 1 > -1 && board.Rows[y - 1].Cells[x - 1].Value != null && containsEnemy(piece.color, x - 1, y - 1) && !checkForACheck(piece, x - 1, y - 1, false))
                    {
                        board.Rows[y - 1].Cells[x - 1].Style.BackColor = Color.Purple;
                    }
                }
                else if (piece.name == "pawn" && ((piece.color == Color.White && !turn) || (piece.color == Color.Black && turn)))
                {
                    if (x + 1 < 8 && y + 1 < 8 && board.Rows[y + 1].Cells[x + 1].Value != null && containsEnemy(piece.color, x + 1, y + 1) && !checkForACheck(piece, x + 1, y + 1, true))
                    {
                        board.Rows[y + 1].Cells[x + 1].Style.BackColor = Color.Red;
                    }
                    if (x - 1 > -1 && y + 1 < 8 && board.Rows[y + 1].Cells[x - 1].Value != null && containsEnemy(piece.color, x - 1, y + 1) && !checkForACheck(piece, x - 1, y + 1, true))
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
                    if (y + 1 < 8 && x - 1 > -1 && board.Rows[y + 1].Cells[x - 1].Value != null && containsEnemy(piece.color, x - 1, y + 1) && !checkForACheck(piece, x - 1, y + 1, false))
                    {
                        board.Rows[y + 1].Cells[x - 1].Style.BackColor = Color.Purple;
                    }
                    if (y + 1 < 8 && x + 1 < 8 && board.Rows[y + 1].Cells[x + 1].Value != null && containsEnemy(piece.color, x + 1, y + 1) && !checkForACheck(piece, x + 1, y + 1, false))
                    {
                        board.Rows[y + 1].Cells[x + 1].Style.BackColor = Color.Purple;
                    }
                }
                else if (piece.name == "rook" && ((piece.color == Color.White && turn) || (piece.color == Color.Black && !turn)))
                {
                    //above (i represents y/row)
                    for (int i = y - 1; i > -1; i--)
                    {
                        if (board.Rows[i].Cells[x].Value == null && !checkForACheck(piece, x, i, false))
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i) && !checkForACheck(piece, x, i,true))
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i) && !checkForACheck(piece, x, i,true))
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y) && !checkForACheck(piece, i, y,true))
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
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
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y) && !checkForACheck(piece, i, y,true))
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
                        {
                            break;
                        }
                    }
                }
                else if (piece.name == "horse" && ((piece.color == Color.White && turn) || (piece.color == Color.Black && !turn)))
                {
                    //up left
                    if (y - 2 > -1 && x - 1 > -1 && board.Rows[y - 2].Cells[x - 1].Value == null && !checkForACheck(piece, x - 1, y - 2, false))
                    {
                        board.Rows[y - 2].Cells[x - 1].Style.BackColor = Color.Blue;
                    }
                    else if (y - 2 > -1 && x - 1 > -1 && board.Rows[y - 2].Cells[x - 1].Value != null && containsEnemy(piece.color, x - 1, y - 2) && !checkForACheck(piece, x - 1, y - 2,true))
                    {
                        board.Rows[y - 2].Cells[x - 1].Style.BackColor = Color.Red;
                    }

                    //up right
                    if (y - 2 > -1 && x + 1 < 8 && board.Rows[y - 2].Cells[x + 1].Value == null && !checkForACheck(piece, x + 1, y - 2, false))
                    {
                        board.Rows[y - 2].Cells[x + 1].Style.BackColor = Color.Blue;
                    }
                    else if (y - 2 > -1 && x + 1 < 8 && board.Rows[y - 2].Cells[x + 1].Value != null && containsEnemy(piece.color, x + 1, y - 2) && !checkForACheck(piece, x + 1, y - 2,true))
                    {
                        board.Rows[y - 2].Cells[x + 1].Style.BackColor = Color.Red;
                    }

                    //right up
                    if (y - 1 > -1 && x + 2 < 8 && board.Rows[y - 1].Cells[x + 2].Value == null && !checkForACheck(piece, x + 2, y - 1, false))
                    {
                        board.Rows[y - 1].Cells[x + 2].Style.BackColor = Color.Blue;
                    }
                    else if (y - 1 > -1 && x + 2 < 8 && board.Rows[y - 1].Cells[x + 2].Value != null && containsEnemy(piece.color, x + 2, y - 1) && !checkForACheck(piece, x + 2, y - 1,true))
                    {
                        board.Rows[y - 1].Cells[x + 2].Style.BackColor = Color.Red;
                    }

                    //right down
                    if (y + 1 < 8 && x + 2 < 8 && board.Rows[y + 1].Cells[x + 2].Value == null && !checkForACheck(piece, x + 2, y + 1, false))
                    {
                        board.Rows[y + 1].Cells[x + 2].Style.BackColor = Color.Blue;
                    }
                    else if (y + 1 < 8 && x + 2 < 8 && board.Rows[y + 1].Cells[x + 2].Value != null && containsEnemy(piece.color, x + 2, y + 1) && !checkForACheck(piece, x + 2, y + 1,true))
                    {
                        board.Rows[y + 1].Cells[x + 2].Style.BackColor = Color.Red;
                    }

                    //down left
                    if (y + 2 < 8 && x - 1 > -1 && board.Rows[y + 2].Cells[x - 1].Value == null && !checkForACheck(piece, x - 1, y + 2, false))
                    {
                        board.Rows[y + 2].Cells[x - 1].Style.BackColor = Color.Blue;
                    }
                    else if (y + 2 < 8 && x - 1 > -1 && board.Rows[y + 2].Cells[x - 1].Value != null && containsEnemy(piece.color, x - 1, y + 2) && !checkForACheck(piece, x - 1, y + 2,true))
                    {
                        board.Rows[y + 2].Cells[x - 1].Style.BackColor = Color.Red;
                    }

                    //down right
                    if (y + 2 < 8 && x + 1 < 8 && board.Rows[y + 2].Cells[x + 1].Value == null && !checkForACheck(piece, x + 1, y + 2, false))
                    {
                        board.Rows[y + 2].Cells[x + 1].Style.BackColor = Color.Blue;
                    }
                    else if (y + 2 < 8 && x + 1 < 8 && board.Rows[y + 2].Cells[x + 1].Value != null && containsEnemy(piece.color, x + 1, y + 2) && !checkForACheck(piece, x + 1, y + 2,true))
                    {
                        board.Rows[y + 2].Cells[x + 1].Style.BackColor = Color.Red;
                    }

                    //left up
                    if (y + 1 < 8 && x - 2 > -1 && board.Rows[y + 1].Cells[x - 2].Value == null && !checkForACheck(piece, x - 2, y + 1, false))
                    {
                        board.Rows[y + 1].Cells[x - 2].Style.BackColor = Color.Blue;
                    }
                    else if (y + 1 < 8 && x - 2 > -1 && board.Rows[y + 1].Cells[x - 2].Value != null && containsEnemy(piece.color, x - 2, y + 1) && !checkForACheck(piece, x - 2, y + 1,true))
                    {
                        board.Rows[y + 1].Cells[x - 2].Style.BackColor = Color.Red;
                    }

                    //left down
                    if (y - 1 > -1 && x - 2 > -1 && board.Rows[y - 1].Cells[x - 2].Value == null && !checkForACheck(piece, x - 2, y - 1, false))
                    {
                        board.Rows[y - 1].Cells[x - 2].Style.BackColor = Color.Blue;
                    }
                    else if (y - 1 > -1 && x - 2 > -1 && board.Rows[y - 1].Cells[x - 2].Value != null && containsEnemy(piece.color, x - 2, y - 1) && !checkForACheck(piece, x - 2, y - 1,true))
                    {
                        board.Rows[y - 1].Cells[x - 2].Style.BackColor = Color.Red;
                    }
                }
                else if (piece.name == "bishop" && ((piece.color == Color.White && turn) || (piece.color == Color.Black && !turn)))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && !checkForACheck(piece, i, j,true))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && !checkForACheck(piece, i, j,true))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && !checkForACheck(piece, i, j,true))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && !checkForACheck(piece, i, j,true))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
                        {
                            break;
                        }
                    }
                }
                else if (piece.name == "queen" && ((piece.color == Color.White && turn) || (piece.color == Color.Black && !turn)))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && !checkForACheck(piece, i, j,true))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && !checkForACheck(piece, i, j,true))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && !checkForACheck(piece, i, j,true))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && !checkForACheck(piece, i, j,true))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i) && !checkForACheck(piece, x, i,true))
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i) && !checkForACheck(piece, x, i,true))
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y) && !checkForACheck(piece, i, y,true))
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
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
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y) && !checkForACheck(piece, i, y,true))
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
                        {
                            break;
                        }
                    }
                }
                else if (piece.name == "king" && ((piece.color == Color.White && turn) || (piece.color == Color.Black && !turn)))
                {
                    List<int[]> future = getCheckTiles(piece.color == Color.White ? Color.Black:Color.White)[1];
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
                            else if (board.Rows[y].Cells[x + 1].Value != null && containsEnemy(piece.color, x + 1, y))
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
                            else if (board.Rows[y].Cells[x - 1].Value != null && containsEnemy(piece.color, x - 1, y))
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
                            else if (board.Rows[y + 1].Cells[x].Value != null && containsEnemy(piece.color, x, y + 1))
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
                            else if (board.Rows[y - 1].Cells[x].Value != null && containsEnemy(piece.color, x, y - 1))
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
                            else if (board.Rows[y - 1].Cells[x + 1].Value != null && containsEnemy(piece.color, x + 1, y - 1))
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
                            else if (board.Rows[y - 1].Cells[x - 1].Value != null && containsEnemy(piece.color, x - 1, y - 1))
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
                            else if (board.Rows[y + 1].Cells[x + 1].Value != null && containsEnemy(piece.color, x + 1, y + 1))
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
                            else if (board.Rows[y + 1].Cells[x - 1].Value != null && containsEnemy(piece.color, x - 1, y + 1))
                            {
                                board.Rows[y + 1].Cells[x - 1].Style.BackColor = Color.Red;
                            }
                        }   
                    }
                    //CASTLE
                    //check if white or black
                    if (piece.color == Color.White)
                    {
                        //if king has not moved and the spaces between him and the rook are check free
                        if (!piece.moved)
                        {
                            //KINGSIDE CASTLE
                            bool castle = true;

                            //find kingside rook
                            Piece kingRook = new Piece();
                            foreach (Piece findRook in pieces)
                            {
                                if (findRook.name == "rook" && findRook.color == piece.color && !findRook.moved && findRook.x == 7)
                                {
                                    kingRook = findRook;
                                }
                            }

                            //check if rook was found successfully and the spots between king and rook are empty
                            if (kingRook.name == "rook" && board.Rows[y].Cells[piece.x + 1].Value == null && board.Rows[y].Cells[piece.x + 2].Value == null)
                            {
                                //check each future move of the enemy to see if spaces in between king and rook are not in check
                                foreach (int[] tile in future)
                                {
                                    //if the 2 spots are checkable, castle is illegal
                                    if ((piece.x + 1 == tile[0] && piece.y == tile[1]) || (piece.x + 2 == tile[0] && piece.y == tile[1]))
                                    {
                                        castle = false;
                                    }
                                }

                                if (castle)
                                {
                                    board.Rows[kingRook.y].Cells[kingRook.x].Style.BackColor = Color.Green;
                                }
                            }


                            //QUEENSIDE CASTLE
                            castle = true;

                            //find kingside rook
                            Piece queenRook = new Piece();
                            foreach (Piece findRook in pieces)
                            {
                                if (findRook.name == "rook" && findRook.color == piece.color && !findRook.moved && findRook.x == 0)
                                {
                                    queenRook = findRook;
                                }
                            }

                            //check if rook was found successfully and the spots between king and rook are empty
                            if (queenRook.name == "rook" && board.Rows[y].Cells[piece.x - 1].Value == null && board.Rows[y].Cells[piece.x - 2].Value == null && board.Rows[y].Cells[piece.x - 3].Value == null)
                            {
                                //check each future move of the enemy to see if spaces in between king and rook are not in check
                                foreach (int[] tile in future)
                                {
                                    //if the 2 spots are checkable, castle is illegal
                                    if ((piece.x - 1 == tile[0] && piece.y == tile[1]) || (piece.x - 2 == tile[0] && piece.y == tile[1]))
                                    {
                                        castle = false;
                                    }
                                }

                                if (castle)
                                {
                                    board.Rows[queenRook.y].Cells[queenRook.x].Style.BackColor = Color.Green;
                                }
                            }
                        }
                    }
                    else
                    {
                        //if king has not moved and the spaces between him and the rook are check free
                        if (!piece.moved)
                        {
                            //KINGSIDE CASTLE
                            bool castle = true;

                            //find kingside rook
                            Piece kingRook = new Piece();
                            foreach (Piece findRook in pieces)
                            {
                                if (findRook.name == "rook" && findRook.color == piece.color && !findRook.moved && findRook.x == 0)
                                {
                                    kingRook = findRook;
                                }
                            }

                            //check if rook was found successfully and the spots between king and rook are empty
                            if (kingRook.name == "rook" && board.Rows[y].Cells[piece.x - 1].Value == null && board.Rows[y].Cells[piece.x - 2].Value == null)
                            {
                                //check each future move of the enemy to see if spaces in between king and rook are not in check
                                foreach (int[] tile in future)
                                {
                                    //if the 2 spots are checkable, castle is illegal
                                    if ((piece.x - 1 == tile[0] && piece.y == tile[1]) || (piece.x - 2 == tile[0] && piece.y == tile[1]))
                                    {
                                        castle = false;
                                    }
                                }

                                if (castle)
                                {
                                    board.Rows[kingRook.y].Cells[kingRook.x].Style.BackColor = Color.Green;
                                }
                            }


                            //QUEENSIDE CASTLE
                            castle = true;

                            //find kingside rook
                            Piece queenRook = new Piece();
                            foreach (Piece findRook in pieces)
                            {
                                if (findRook.name == "rook" && findRook.color == piece.color && !findRook.moved && findRook.x == 7)
                                {
                                    queenRook = findRook;
                                }
                            }

                            //check if rook was found successfully and the spots between king and rook are empty
                            if (queenRook.name == "rook" && board.Rows[y].Cells[piece.x + 1].Value == null && board.Rows[y].Cells[piece.x + 2].Value == null && board.Rows[y].Cells[piece.x + 3].Value == null)
                            {
                                //check each future move of the enemy to see if spaces in between king and rook are not in check
                                foreach (int[] tile in future)
                                {
                                    //if the 2 spots are checkable, castle is illegal
                                    if ((piece.x + 1 == tile[0] && piece.y == tile[1]) || (piece.x + 2 == tile[0] && piece.y == tile[1]))
                                    {
                                        castle = false;
                                    }
                                }

                                if (castle)
                                {
                                    board.Rows[queenRook.y].Cells[queenRook.x].Style.BackColor = Color.Green;
                                }
                            }
                        }
                    }


                }
            }
            else
            {
                List<int[]>[] getCheck = getCheckTiles(piece.color == Color.White ? Color.Black:Color.White);
                List<int[]> threats = getCheck[0];
                List<int[]> future = getCheck[1];

                int x = piece.x;
                int y = piece.y;

                if (piece.name == "king")
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
                    else if (newy > -1 && newy < 8 && board.Rows[newy].Cells[x].Value != null && containsEnemy(piece.color, x, newy))
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
                    else if (newy < 8 && newy > -1 && board.Rows[newy].Cells[x].Value != null && containsEnemy(piece.color, x, newy))
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
                    else if (newx > -1 && newx < 8 && board.Rows[y].Cells[newx].Value != null && containsEnemy(piece.color, newx, y))
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
                    else if (newx < 8 && newx > -1 && board.Rows[y].Cells[newx].Value != null && containsEnemy(piece.color, newx, y))
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
                    else if (newx > -1 && newy > -1 && newx < 8 && newy < 8 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.color, newx, newy))
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
                    else if (newx < 8 && newy > -1 && newx > -1 && newy < 8 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.color, newx, newy))
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
                    else if (newx > -1 && newy < 8 && newx < 8 && newy > -1 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.color, newx, newy))
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
                    else if (newx < 8 && newy < 8 && newx > -1 && newy > -1 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.color, newx, newy))
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
                else if (piece.name == "pawn")
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

                    //left en Passant
                    if (x - 1 > -1 && y - 1 > -1 && board.Rows[y].Cells[x - 1].Value != null && board.Rows[y - 1].Cells[x - 1].Value == null && containsEnemy(piece.color, x - 1, y) && !checkForACheck(piece, x - 1, y - 1, true) && lastMove[0] == "pawn" && lastMove[1] == "double move" && int.Parse(lastMove[2]) == x - 1 && int.Parse(lastMove[3]) == y)
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == x - 1 && tile[1] == y)
                            {
                                board.Rows[y - 1].Cells[x - 1].Style.BackColor = Color.Red;
                                enPassant = true;
                                break;
                            }
                        }
                    }

                    //right en Passant
                    if (x + 1 < 8 && y - 1 > -1 && board.Rows[y].Cells[x + 1].Value != null && board.Rows[y - 1].Cells[x + 1].Value == null && containsEnemy(piece.color, x + 1, y) && !checkForACheck(piece, x + 1, y - 1, true) && lastMove[0] == "pawn" && lastMove[1] == "double move" && int.Parse(lastMove[2]) == x + 1 && int.Parse(lastMove[3]) == y)
                    {
                        foreach (int[] tile in threats)
                        {
                            if (tile[0] == x + 1 && tile[1] == y - 1)
                            {
                                board.Rows[y - 1].Cells[x + 1].Style.BackColor = Color.Red;
                                enPassant = true;
                                break;
                            }
                        }
                    }
                }
                else if (piece.name == "rook")
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
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y))
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
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
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
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y))
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
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
                        {
                            break;
                        }
                    }

                }
                else if (piece.name == "horse")
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
                    else if (newy > -1 && newx > -1 && board.Rows[newy].Cells[x - 1].Value != null && containsEnemy(piece.color, newx, newy))
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
                    else if (newy > -1 && newx < 8 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.color, newx, newy))
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
                    else if (newy > -1 && newx < 8 && board.Rows[newy].Cells[x - 1].Value != null && containsEnemy(piece.color, newx, newy))
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
                    else if (newy < 8 && newx < 8 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.color, newx, newy))
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
                    else if (newy < 8 && newx < 8 && board.Rows[newy].Cells[x - 1].Value != null && containsEnemy(piece.color, newx, newy))
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
                    else if (newy < 8 && newx > -1 && board.Rows[newy].Cells[x - 1].Value != null && containsEnemy(piece.color, newx, newy))
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
                    else if (newy < 8 && newx > -1 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.color, newx, newy))
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
                    else if (newy > -1 && newx > -1 && board.Rows[newy].Cells[x - 1].Value != null && containsEnemy(piece.color, newx, newy))
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
                else if (piece.name == "bishop")
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
                        {
                            break;
                        }
                    }
                }
                else if (piece.name == "queen")
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y))
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
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
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
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y))
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
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
                        {
                            break;
                        }
                    }
                }
            }

            foreach (DataGridViewRow row in board.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Style.BackColor == Color.Blue)
                    {
                        foreach (Piece findPiece in pieces)
                        {
                            if (findPiece.x == cell.ColumnIndex && findPiece.y == cell.RowIndex)
                            {
                                //just change it to anythign other than blue so the algorithm doesnt get confused. It'll all get sorted out when ogColors() is called later
                                ogColor(cell.ColumnIndex,cell.RowIndex);
                            }
                        }
                    }
                }
            }
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
                int x = piece.x;
                int y = piece.y;
                if (piece.name == "pawn" && ((piece.color == Color.White && !turn) || (piece.color == Color.Black && turn)))
                {
                    if (x - 1 != -1 && y - 1 != -1 && board.Rows[y - 1].Cells[x - 1].Value != null && containsEnemy(piece.color, x - 1, y - 1))
                    {
                        board.Rows[y - 1].Cells[x - 1].Style.BackColor = Color.Red;
                    }
                    if (x + 1 != 8 && y - 1 != -1 && board.Rows[y - 1].Cells[x + 1].Value != null && containsEnemy(piece.color, x + 1, y - 1))
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
                else if (piece.name == "rook" && ((piece.color == Color.White && !turn) || (piece.color == Color.Black && turn)))
                {
                    //above (i represents y/row)
                    for (int i = y - 1; i > -1; i--)
                    {
                        if (board.Rows[i].Cells[x].Value == null)
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Blue;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i))
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i))
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y))
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
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
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y))
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
                        {
                            break;
                        }
                    }
                }
                else if (piece.name == "horse" && ((piece.color == Color.White && !turn) || (piece.color == Color.Black && turn)))
                {
                    //up left
                    if (y - 2 > -1 && x - 1 > -1 && board.Rows[y - 2].Cells[x - 1].Value == null)
                    {
                        board.Rows[y - 2].Cells[x - 1].Style.BackColor = Color.Blue;
                    }
                    else if (y - 2 > -1 && x - 1 > -1 && board.Rows[y - 2].Cells[x - 1].Value != null && containsEnemy(piece.color, x - 1, y - 2))
                    {
                        board.Rows[y - 2].Cells[x - 1].Style.BackColor = Color.Red;
                    }

                    //up right
                    if (y - 2 > -1 && x + 1 < 8 && board.Rows[y - 2].Cells[x + 1].Value == null)
                    {
                        board.Rows[y - 2].Cells[x + 1].Style.BackColor = Color.Blue;
                    }
                    else if (y - 2 > -1 && x + 1 < 8 && board.Rows[y - 2].Cells[x + 1].Value != null && containsEnemy(piece.color, x + 1, y - 2))
                    {
                        board.Rows[y - 2].Cells[x + 1].Style.BackColor = Color.Red;
                    }

                    //right up
                    if (y - 1 > -1 && x + 2 < 8 && board.Rows[y - 1].Cells[x + 2].Value == null)
                    {
                        board.Rows[y - 1].Cells[x + 2].Style.BackColor = Color.Blue;
                    }
                    else if (y - 1 > -1 && x + 2 < 8 && board.Rows[y - 1].Cells[x + 2].Value != null && containsEnemy(piece.color, x + 2, y - 1))
                    {
                        board.Rows[y - 1].Cells[x + 2].Style.BackColor = Color.Red;
                    }

                    //right down
                    if (y + 1 < 8 && x + 2 < 8 && board.Rows[y + 1].Cells[x + 2].Value == null)
                    {
                        board.Rows[y + 1].Cells[x + 2].Style.BackColor = Color.Blue;
                    }
                    else if (y + 1 < 8 && x + 2 < 8 && board.Rows[y + 1].Cells[x + 2].Value != null && containsEnemy(piece.color, x + 2, y + 1))
                    {
                        board.Rows[y + 1].Cells[x + 2].Style.BackColor = Color.Red;
                    }

                    //down left
                    if (y + 2 < 8 && x - 1 > -1 && board.Rows[y + 2].Cells[x - 1].Value == null)
                    {
                        board.Rows[y + 2].Cells[x - 1].Style.BackColor = Color.Blue;
                    }
                    else if (y + 2 < 8 && x - 1 > -1 && board.Rows[y + 2].Cells[x - 1].Value != null && containsEnemy(piece.color, x - 1, y + 2))
                    {
                        board.Rows[y + 2].Cells[x - 1].Style.BackColor = Color.Red;
                    }

                    //down right
                    if (y + 2 < 8 && x + 1 < 8 && board.Rows[y + 2].Cells[x + 1].Value == null)
                    {
                        board.Rows[y + 2].Cells[x + 1].Style.BackColor = Color.Blue;
                    }
                    else if (y + 2 < 8 && x + 1 < 8 && board.Rows[y + 2].Cells[x + 1].Value != null && containsEnemy(piece.color, x + 1, y + 2))
                    {
                        board.Rows[y + 2].Cells[x + 1].Style.BackColor = Color.Red;
                    }

                    //left up
                    if (y + 1 < 8 && x - 2 > -1 && board.Rows[y + 1].Cells[x - 2].Value == null)
                    {
                        board.Rows[y + 1].Cells[x - 2].Style.BackColor = Color.Blue;
                    }
                    else if (y + 1 < 8 && x - 2 > -1 && board.Rows[y + 1].Cells[x - 2].Value != null && containsEnemy(piece.color, x - 2, y + 1))
                    {
                        board.Rows[y + 1].Cells[x - 2].Style.BackColor = Color.Red;
                    }

                    //left down
                    if (y - 1 > -1 && x - 2 > -1 && board.Rows[y - 1].Cells[x - 2].Value == null)
                    {
                        board.Rows[y - 1].Cells[x - 2].Style.BackColor = Color.Blue;
                    }
                    else if (y - 1 > -1 && x - 2 > -1 && board.Rows[y - 1].Cells[x - 2].Value != null && containsEnemy(piece.color, x - 2, y - 1))
                    {
                        board.Rows[y - 1].Cells[x - 2].Style.BackColor = Color.Red;
                    }
                }
                else if (piece.name == "bishop" && ((piece.color == Color.White && !turn) || (piece.color == Color.Black && turn)))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
                        {
                            break;
                        }
                    }
                }
                else if (piece.name == "queen" && ((piece.color == Color.White && !turn) || (piece.color == Color.Black && turn)))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
                        {
                            board.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i))
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i))
                        {
                            board.Rows[i].Cells[x].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y))
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
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
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y))
                        {
                            board.Rows[y].Cells[i].Style.BackColor = Color.Red;
                            break;
                        }
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
                        {
                            break;
                        }
                    }
                }
                else if (piece.name == "king" && ((piece.color == Color.White && !turn) || (piece.color == Color.Black && turn)))
                {
                    List<int[]> future = getCheckTiles(piece.color == Color.White ? Color.Black : Color.White)[1];
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
                            else if (board.Rows[y].Cells[x + 1].Value != null && containsEnemy(piece.color, x + 1, y))
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
                            else if (board.Rows[y].Cells[x - 1].Value != null && containsEnemy(piece.color, x - 1, y))
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
                            else if (board.Rows[y + 1].Cells[x].Value != null && containsEnemy(piece.color, x, y + 1))
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
                            else if (board.Rows[y - 1].Cells[x].Value != null && containsEnemy(piece.color, x, y - 1))
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
                            else if (board.Rows[y - 1].Cells[x + 1].Value != null && containsEnemy(piece.color, x + 1, y - 1))
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
                            else if (board.Rows[y - 1].Cells[x - 1].Value != null && containsEnemy(piece.color, x - 1, y - 1))
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
                            else if (board.Rows[y + 1].Cells[x + 1].Value != null && containsEnemy(piece.color, x + 1, y + 1))
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
                            else if (board.Rows[y + 1].Cells[x - 1].Value != null && containsEnemy(piece.color, x - 1, y + 1))
                            {
                                board.Rows[y + 1].Cells[x - 1].Style.BackColor = Color.Red;
                            }
                        }
                    }
                    //CASTLE
                    //if king has not moved and the spaces between him and the rook are check free
                    if (!piece.moved)
                    {
                        //KINGSIDE CASTLE
                        bool castle = true;

                        //find kingside rook
                        Piece kingRook = new Piece();
                        foreach (Piece findRook in pieces)
                        {
                            if (findRook.name == "rook" && findRook.color == piece.color && !findRook.moved && findRook.x == 7)
                            {
                                kingRook = findRook;
                            }
                        }

                        //check if rook was found successfully and the spots between king and rook are empty
                        if (kingRook.name == "rook" && board.Rows[y].Cells[piece.x + 1].Value == null && board.Rows[y].Cells[piece.x + 2].Value == null)
                        {
                            //check each future move of the enemy to see if spaces in between king and rook are not in check
                            foreach (int[] tile in future)
                            {
                                //if the 2 spots are checkable, castle is illegal
                                if ((piece.x + 1 == tile[0] && piece.y == tile[1]) || (piece.x + 2 == tile[0] && piece.y == tile[1]))
                                {
                                    castle = false;
                                }
                            }

                            if (castle)
                            {
                                board.Rows[kingRook.y].Cells[kingRook.x].Style.BackColor = Color.Green;
                            }
                        }


                        //QUEENSIDE CASTLE
                        castle = true;

                        //find kingside rook
                        Piece queenRook = new Piece();
                        foreach (Piece findRook in pieces)
                        {
                            if (findRook.name == "rook" && findRook.color == piece.color && !findRook.moved && findRook.x == 0)
                            {
                                queenRook = findRook;
                            }
                        }

                        //check if rook was found successfully and the spots between king and rook are empty
                        if (queenRook.name == "rook" && board.Rows[y].Cells[piece.x - 1].Value == null && board.Rows[y].Cells[piece.x - 2].Value == null && board.Rows[y].Cells[piece.x - 3].Value == null)
                        {
                            //check each future move of the enemy to see if spaces in between king and rook are not in check
                            foreach (int[] tile in future)
                            {
                                //if the 2 spots are checkable, castle is illegal
                                if ((piece.x - 1 == tile[0] && piece.y == tile[1]) || (piece.x - 2 == tile[0] && piece.y == tile[1]))
                                {
                                    castle = false;
                                }
                            }

                            if (castle)
                            {
                                board.Rows[queenRook.y].Cells[queenRook.x].Style.BackColor = Color.Green;
                            }
                        }
                    }

                }
            }
            else
            {
                List<int[]>[] getCheck = getCheckTiles(piece.color == Color.White ? Color.Black : Color.White);
                List<int[]> threats = getCheck[0];
                List<int[]> future = getCheck[1];

                int x = piece.x;
                int y = piece.y;

                if (piece.name == "king")
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
                    else if (newy > -1 && newy < 8 && board.Rows[newy].Cells[x].Value != null && containsEnemy(piece.color, x, newy))
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
                    else if (newy < 8 && newy > -1 && board.Rows[newy].Cells[x].Value != null && containsEnemy(piece.color, x, newy))
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
                    else if (newx > -1 && newx < 8 && board.Rows[y].Cells[newx].Value != null && containsEnemy(piece.color, newx, y))
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
                    else if (newx > -1 && newx > -1 && board.Rows[y].Cells[newx].Value != null && containsEnemy(piece.color, newx, y))
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
                    else if (newx > -1 && newy > -1 && newx < 8 && newy < 8 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.color, newx, newy))
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
                    else if (newx < 8 && newy > -1 && newx > -1 && newy < 8 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.color, newx, newy))
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
                    else if (newx > -1 && newy < 8 && newx < 8 && newy > -1 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.color, newx, newy))
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
                    else if (newx < 8 && newy < 8 && newx > -1 && newy > -1 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.color, newx, newy))
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
                else if (piece.name == "pawn")
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
                else if (piece.name == "rook")
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
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y))
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
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
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
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y))
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
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
                        {
                            break;
                        }
                    }

                }
                else if (piece.name == "horse")
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
                    else if (newy > -1 && newx > -1 && board.Rows[newy].Cells[x - 1].Value != null && containsEnemy(piece.color, newx, newy))
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
                    else if (newy > -1 && newx < 8 && board.Rows[newy].Cells[x - 1].Value != null && containsEnemy(piece.color, newx, newy))
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
                    else if (newy > -1 && newx < 8 && board.Rows[newy].Cells[x - 1].Value != null && containsEnemy(piece.color, newx, newy))
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
                    else if (newy < 8 && newx < 8 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.color, newx, newy))
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
                    else if (newy < 8 && newx < 8 && board.Rows[newy].Cells[x - 1].Value != null && containsEnemy(piece.color, newx, newy))
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
                    else if (newy < 8 && newx > -1 && board.Rows[newy].Cells[x - 1].Value != null && containsEnemy(piece.color, newx, newy))
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
                    else if (newy < 8 && newx > -1 && board.Rows[newy].Cells[newx].Value != null && containsEnemy(piece.color, newx, newy))
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
                    else if (newy > -1 && newx > -1 && board.Rows[newy].Cells[x - 1].Value != null && containsEnemy(piece.color, newx, newy))
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
                else if (piece.name == "bishop")
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
                        {
                            break;
                        }
                    }
                }
                else if (piece.name == "queen")
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y))
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
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
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
                        else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y))
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
                        else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
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
            List<Piece> testPieces = new List<Piece>();
            //hard copy of pieces
            foreach (Piece copy in pieces)
            {
                testPieces.Add(copy);
            }

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
            int oldx = piece.x;
            int oldy = piece.y;
            piece.x = newx;
            piece.y = newy;
            board.Rows[oldy].Cells[oldx].Value = null;
            board.Rows[newy].Cells[newx].Value = piece.icon;

            Piece kill = new Piece();
            if (killed)
            {
                //see if that spot would be a kill
                foreach (Piece found in testPieces)
                {
                    if (found != piece && found.x == newx && found.y == newy)
                    {
                        kill = found;
                    }
                }
                if (kill.x != -1)
                {
                    testPieces.Remove(kill);
                }
            }

            //highlights all spaces
            foreach (Piece check in testPieces)
            {
                if (check.color != piece.color)
                {
                    oldHighlightSpaces(check, false);
                }
            }

            //find the king 
            Piece king = new Piece();
            foreach (Piece findKing in testPieces)
            {
                if (findKing.name == "king" && findKing.color == piece.color)
                {
                    king = findKing;
                    break;
                }
            }

            //see if any red spaces hold the king
            bool checkd = false;
            foreach (DataGridViewRow row in board.Rows)
            {
                foreach (DataGridViewImageCell cell in row.Cells)
                {
                    if (cell.Style.BackColor == Color.Red && cell.RowIndex == king.y && cell.ColumnIndex == king.x)
                    {
                        checkd = true;
                    }
                }
            }

            //reset back to before
            board.Rows[newy].Cells[newx].Value = null;
            if (kill.x != -1)
            {
                testPieces.Add(kill);
                board.Rows[kill.y].Cells[kill.x].Value = kill.icon;
            }
            piece.x = oldx;
            piece.y = oldy;
            board.Rows[oldy].Cells[oldx].Value = piece.icon;

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
            foreach(Piece piece in pieces)
            {
                if(color != piece.color && piece.x == x && piece.y == y)
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
            Piece king = new Piece();
            foreach (Piece piece in pieces)
            {
                if (piece.name == "king" && piece.color == protector.color)
                {
                    king = piece;
                }
            }

            //iterate through the enemy to see if there is a path for them to check if protector were to move
            foreach (Piece piece in pieces)
            {
                //ignores if the piece is on the side of the protector
                if (protector.color != piece.color)
                {
                    int x = piece.x;
                    int y = piece.y;
                    if (piece.name == "rook")
                    {
                        //records if a peice is in the way between a check
                        bool Protected = false;

                        //records if the piece can kill the king if they had a clear path w/o the protector
                        bool kingInPath = false;

                        for (int i = y - 1; i > -1; i--)
                        {
                            //if there is king along the piece's path
                            if (king.x == x && king.y == i)
                            {
                                kingInPath = true;
                                break;
                            }
                            //if there is already someone blocking piece's path on protector's team, then anyways theres no need for protector to be restricted since his teamate is covering him
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i) && (protector.x != x || protector.y != i))
                            {
                                break;
                            }
                            //if the protector is first in the path of the piece
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i) && protector.x == x && protector.y == i)
                            {
                                Protected = true;
                            }
                            //if piece is blocoked by his own teammate, there is no need to see if protector is in the way or if protector's king is in the way
                            else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                            if (king.x == x && king.y == i)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i) && (protector.x != x || protector.y != i))
                            {
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i) && protector.x == x && protector.y == i)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                            if (king.x == i && king.y == y)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y) && (protector.x != i || protector.y != y))
                            {
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y) && protector.x == i && protector.y == y)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
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
                            if (king.x == i && king.y == y)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y) && (protector.x != i || protector.y != y))
                            {
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y) && protector.x == i && protector.y == y)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
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
                    else if (piece.name == "bishop")
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

                            if (king.x == i && king.y == j)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && (protector.x != i || protector.y != j))
                            {
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && protector.x == i && protector.y == j)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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

                            if (king.x == i && king.y == j)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && (protector.x != i || protector.y != j))
                            {
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && protector.x == i && protector.y == j)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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

                            if (king.x == i && king.y == j)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && (protector.x != i || protector.y != j))
                            {
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && protector.x == i && protector.y == j)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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

                            if (king.x == i && king.y == j)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && (protector.x != i || protector.y != j))
                            {
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && protector.x == i && protector.y == j)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
                    else if (piece.name == "queen")
                    {
                        //records if a peice is in the way between a check
                        bool Protected = false;

                        //records if the piece can kill the king if they had a clear path w/o the protector
                        bool kingInPath = false;

                        int i;
                        int j;

                        for (i = y - 1; i > -1; i--)
                        {
                            if (king.x == x && king.y == i)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i) && (protector.x != x || protector.y != i))
                            {
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i) && protector.x == x && protector.y == i)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                            if (king.x == x && king.y == i)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i) && (protector.x != x || protector.y != i))
                            {
                                break;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && containsEnemy(piece.color, x, i) && protector.x == x && protector.y == i)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[i].Cells[x].Value != null && !containsEnemy(piece.color, x, i))
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
                            if (king.x == i && king.y == y)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y) && (protector.x != i || protector.y != y))
                            {
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y) && protector.x == i && protector.y == y)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
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
                            if (king.x == i && king.y == y)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y) && (protector.x != i || protector.y != y))
                            {
                                break;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && containsEnemy(piece.color, i, y) && protector.x == i && protector.y == y)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[y].Cells[i].Value != null && !containsEnemy(piece.color, i, y))
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

                            if (king.x == i && king.y == j)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && (protector.x != i || protector.y != j))
                            {
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && protector.x == i && protector.y == j)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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

                            if (king.x == i && king.y == j)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && (protector.x != i || protector.y != j))
                            {
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && protector.x == i && protector.y == j)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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

                            if (king.x == i && king.y == j)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && (protector.x != i || protector.y != j))
                            {
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && protector.x == i && protector.y == j)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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

                            if (king.x == i && king.y == j)
                            {
                                kingInPath = true;
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && (protector.x != i || protector.y != j))
                            {
                                break;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && containsEnemy(piece.color, i, j) && protector.x == i && protector.y == j)
                            {
                                Protected = true;
                            }
                            else if (board.Rows[j].Cells[i].Value != null && !containsEnemy(piece.color, i, j))
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
            lastMove[2] = (7 - int.Parse(lastMove[2])).ToString();
            lastMove[3] = (7 - int.Parse(lastMove[3])).ToString();
            foreach (Piece piece in pieces)
            {
                piece.x = 7 - piece.x;
                piece.y = 7 - piece.y;
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
            foreach (Piece piece in pieces)
            {
                board.Rows[piece.y].Cells[piece.x].Value = piece.icon;
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
