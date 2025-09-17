namespace ChessLogic
{
    public class Pawn : Piece
    {         public override PieceType Type => PieceType.Pawn;
        public override string Color { get; }
        public Pawn(string color)
        {
            Color = color;
        }
        public override Piece Copy()
        {
            Pawn copy = new(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
    }
}

