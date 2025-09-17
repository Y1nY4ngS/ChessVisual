namespace ChessLogic
{
    public class Queen: Piece
    {
        public override PieceType Type => PieceType.Queen;
        public override string Color { get; }
        public Queen(string color)
        {
            Color = color;
        }
        public override Piece Copy()
        {
            Queen copy = new(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
    }
}
