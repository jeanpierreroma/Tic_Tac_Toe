namespace Tic_Tac_Toe.Data.Models.Players
{
    public class EasyComputerPlayer : ComputerPlayer
    {
        public EasyComputerPlayer()
        {
            
        }
        public EasyComputerPlayer(string name, char symbol) 
            : base(name, symbol) { }

        private readonly ComputerDifficulty difficulty = ComputerDifficulty.Easy;

        public ComputerDifficulty GetDifficulty()
        {
            return difficulty;
        }

        public override void GetMove(Board board, Move move)
        {
            Random random = new Random();

            int row = 0;
            int column = 0;
            do
            {
                int randValue = random.Next(board.GameBoard.Length * board.GameBoard.Length - 1);

                row = randValue / board.GameBoard.Length;
                column = randValue % board.GameBoard.Length;

            } while (board.GetCell(row, column) != ' ');

            board.PutSymbol(row, column, this.Symbol);
        }
    }
}
