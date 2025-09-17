namespace ChessLogic
{
    public class Bishop: Piece
    {
        public override PieceType Type => PieceType.Bishop;
        public override string Color { get; }
        public Bishop(string color)
        {
            Color = color;
        }
        public override Piece Copy()
        {
            Bishop copy = new(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
    }
}

