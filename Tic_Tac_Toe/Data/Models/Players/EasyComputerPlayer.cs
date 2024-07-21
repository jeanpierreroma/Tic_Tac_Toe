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

        public override void GetMove(char[][] board, Move move)
        {
            Random random = new Random();

            int row = 0;
            int column = 0;
            do
            {
                int randValue = random.Next(board.Length * board.Length - 1);

                row = randValue / board.Length;
                column = randValue % board.Length;

            } while (board[row][column] != ' ');

            board[row][column] = this.Symbol;
        }
    }
}
