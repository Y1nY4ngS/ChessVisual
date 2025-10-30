using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public static class MoveNotationFormatter
    {
        public static string Format(GameState gameState, Move move)
        {
            Board board = gameState.Board;
            Player player = gameState.CurrentPlayer;
            Piece movingPiece = board[move.FromPos];

            if (movingPiece == null)
            {
                return string.Empty;
            }

            return move.Type switch
            {
                MoveType.CastleKS => AppendCheckSymbols("O-O", board, move, player),
                MoveType.CastleQS => AppendCheckSymbols("O-O-O", board, move, player),
                _ => AppendCheckSymbols(FormatStandardMove(board, move, movingPiece), board, move, player)
            };
        }

        private static string FormatStandardMove(Board board, Move move, Piece movingPiece)
        {
            bool isCapture = IsCapture(board, move);
            string destination = SquareName(move.ToPos);
            string promotionSuffix = GetPromotionSuffix(move);

            if (movingPiece.Type == PieceType.Pawn)
            {
                string fromFile = ((char)('a' + move.FromPos.Column)).ToString();
                string captureIndicator = isCapture ? "x" : string.Empty;
                string prefix = isCapture ? fromFile : string.Empty;
                return $"{prefix}{captureIndicator}{destination}{promotionSuffix}";
            }

            string pieceLetter = PieceLetter(movingPiece.Type);
            string disambiguation = GetDisambiguation(board, move, movingPiece);
            string capture = isCapture ? "x" : string.Empty;

            return $"{pieceLetter}{disambiguation}{capture}{destination}{promotionSuffix}";
        }

        private static bool IsCapture(Board board, Move move)
        {
            return !board.IsEmpty(move.ToPos) || move.Type == MoveType.EnPassant;
        }

        private static string GetPromotionSuffix(Move move)
        {
            if (move is PawnPromotion promotion)
            {
                return "=" + PieceLetter(promotion.PromotionType);
            }

            return string.Empty;
        }

        private static string GetDisambiguation(Board board, Move move, Piece movingPiece)
        {
            if (movingPiece.Type == PieceType.Pawn)
            {
                return string.Empty;
            }

            IEnumerable<Position> ambiguousPositions = board
                .PiecePositionsFor(movingPiece.Color)
                .Where(pos => pos != move.FromPos)
                .Where(pos => board[pos].Type == movingPiece.Type)
                .Where(pos => board[pos]
                    .GetMoves(pos, board)
                    .Any(candidate => candidate.ToPos == move.ToPos && candidate.IsLegal(board)));

            if (!ambiguousPositions.Any())
            {
                return string.Empty;
            }

            bool sameFile = ambiguousPositions.Any(pos => pos.Column == move.FromPos.Column);
            bool sameRank = ambiguousPositions.Any(pos => pos.Row == move.FromPos.Row);

            if (!sameFile && !sameRank)
            {
                return ((char)('a' + move.FromPos.Column)).ToString();
            }

            if (sameFile && sameRank)
            {
                return SquareName(move.FromPos);
            }

            if (sameFile)
            {
                return (8 - move.FromPos.Row).ToString();
            }

            return ((char)('a' + move.FromPos.Column)).ToString();
        }

        private static string PieceLetter(PieceType type)
        {
            return type switch
            {
                PieceType.King => "K",
                PieceType.Queen => "Q",
                PieceType.Rook => "R",
                PieceType.Bishop => "B",
                PieceType.Knight => "N",
                _ => string.Empty
            };
        }

        private static string AppendCheckSymbols(string notation, Board board, Move move, Player player)
        {
            Board boardCopy = board.Copy();
            boardCopy.SetPawnSkipPosition(player, null);
            move.Execute(boardCopy);

            Player opponent = player.Opponent();
            bool isCheck = boardCopy.IsInCheck(opponent);

            if (!isCheck)
            {
                return notation;
            }

            bool hasResponse = OpponentHasLegalMove(boardCopy, opponent);
            return notation + (hasResponse ? "+" : "#");
        }

        private static bool OpponentHasLegalMove(Board board, Player opponent)
        {
            foreach (Position pos in board.PiecePositionsFor(opponent))
            {
                Piece piece = board[pos];

                foreach (Move candidate in piece.GetMoves(pos, board))
                {
                    if (candidate.IsLegal(board))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static string SquareName(Position pos)
        {
            char file = (char)('a' + pos.Column);
            int rank = 8 - pos.Row;
            return $"{file}{rank}";
        }
    }
}
