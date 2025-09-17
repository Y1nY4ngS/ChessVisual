namespace ChessLogic
{
    public class Rook: Piece
    {
        public override PieceType Type => PieceType.Rook;
        public override string Color { get; }
        public Rook(string color)
        {
            Color = color;
        }
        public override Piece Copy()
        {
            Rook copy = new(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
    }
}
