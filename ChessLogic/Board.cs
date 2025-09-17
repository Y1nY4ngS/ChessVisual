using System.Reflection.Metadata.Ecma335;

namespace ChessLogic
{
    public class Board
    {
        private readonly Piece[,] pieces = new Piece[8, 8];

        public Piece this[int row , int col]
        {
            get { return pieces[row, col]; }
            set { pieces[row, col] = value; }
        }

        public Piece this[Position pos]
        {
            get { return this[pos.Row, pos.Column]; }
            set { this[pos.Row, pos.Column] = value; }
        }

        public static Board Initial()
        {
            Board board = new();
            board.AddStartPieces();
            return board;
        }
        private void AddStartPieces()
        {
            // Place pawns
            for (int col = 0; col < 8; col++)
            {
                this[1, col] = new Pawn("Black");
                this[6, col] = new Pawn("White");
            }
            // Place rooks
            this[0, 0] = new Rook("Black");
            this[0, 7] = new Rook("Black");
            this[7, 0] = new Rook("White");
            this[7, 7] = new Rook("White");
            // Place knights
            this[0, 1] = new Knight("Black");
            this[0, 6] = new Knight("Black");
            this[7, 1] = new Knight("White");
            this[7, 6] = new Knight("White");
            // Place bishops
            this[0, 2] = new Bishop("Black");
            this[0, 5] = new Bishop("Black");
            this[7, 2] = new Bishop("White");
            this[7, 5] = new Bishop("White");
            // Place queens
            this[0, 3] = new Queen("Black");
            this[7, 3] = new Queen("White");
            // Place kings
            this[0, 4] = new King("Black");
            this[7, 4] = new King("White");
        }

        public static bool IsInside(Position pos)
        {
            return pos.Row >= 0 && pos.Row < 8 && pos.Column >= 0 && pos.Column < 8;
        }

        public bool IsEmpty(Position pos)
        {
            return this[pos] == null;
        }
    }
}
