using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using Tic_Tac_Toe.Data.Models.Players;

namespace Tic_Tac_Toe.Data.Models
{
    public class Board
    {
        public char[][] GameBoard { get; set; }

        private const char INITIALIZATION_SYMBOL = ' ';

        public Board(int size)
        {
            GameBoard = Enumerable
                .Range(0, size)
                .Select(_ => Enumerable
                    .Repeat(INITIALIZATION_SYMBOL, size)
                    .ToArray())
                .ToArray();
        }

        public char GetCell(int row, int column)
        {
            return GameBoard[row][column];
        }

        public bool PutSymbol(int row, int column, char symbol)
        {
            if (GameBoard[row][column] == INITIALIZATION_SYMBOL)
            {
                GameBoard[row][column] = symbol;
                return true;
            }
            return false;
        }

        public GameState CheckGameBoardStatus(char symbol)
        {
            if (CheckForWin(symbol)) return GameState.Win;

            if (CheckBoardForDraw()) return GameState.Draw;


            return GameState.InProgress;
        }

        private bool CheckForWin(char symbol)
        {
            return CheckLinesForWin(symbol, CheckHorizontalLine) ||
                   CheckLinesForWin(symbol, CheckVerticalLine) ||
                   CheckDiagonalLinesForWin(symbol);
        }

        private bool CheckLinesForWin(char symbol, Func<char, bool> checkLineFunc)
        {
            return Enumerable.Range(0, GameBoard.Length).Any(index => checkLineFunc(symbol));
        }

        private bool CheckHorizontalLine(char symbol)
        {
            return GameBoard.Any(row => row.All(cell => cell == symbol));
        }

        private bool CheckVerticalLine(char symbol)
        {
            for (int col = 0; col < GameBoard.Length; col++)
            {
                if (Enumerable.Range(0, GameBoard.Length).All(row => GameBoard[row][col] == symbol))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckDiagonalLinesForWin(char symbol)
        {
            return CheckDiagonalLine(symbol, (i) => GameBoard[i][i] == symbol) ||
                   CheckDiagonalLine(symbol, (i) => GameBoard[i][GameBoard.Length - 1 - i] == symbol);
        }

        private bool CheckDiagonalLine(char symbol, Func<int, bool> checkDiagonalFunc)
        {
            return Enumerable.Range(0, GameBoard.Length).All(i => checkDiagonalFunc(i));
        }

        private bool CheckBoardForDraw()
        {
            return GameBoard.All(row => row.All(cell => cell != ' '));
        }

        public JObject ToJson()
        {
            return new JObject
        {
            { "Board", new JArray(GameBoard.Select(row => new JArray(row.Select(cell => cell.ToString())))) }
        };
        }

        public static Board FromJson(JObject json)
        {
            var boardArray = (JArray)json["Board"];
            var board = boardArray.Select(rowArray =>
                rowArray.Select(cell => (char)((string)cell)[0]).ToArray()
            ).ToArray();

            var size = board.Length; // Припускаємо, що Board квадратний
            var boardObject = new Board(size);
            boardObject.GameBoard = board;
            return boardObject;
        }
    }
}
