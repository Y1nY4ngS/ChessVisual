namespace ChessLogic
{
    public class King : Piece
    {
        public override PieceType Type => PieceType.King;
        public override string Color { get; }
        public King(string color)
        {
            Color = color;
        }
        public override Piece Copy()
        {
            King copy = new(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
    }
}
