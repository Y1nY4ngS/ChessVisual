namespace ChessLogic
{
    public class Knight:Piece
    {
        public override PieceType Type => PieceType.Knight;
        public override string Color { get; }
        public Knight(string color)
        {
            Color = color;
        }
        public override Piece Copy()
        {
            Knight copy = new(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
    }
}
