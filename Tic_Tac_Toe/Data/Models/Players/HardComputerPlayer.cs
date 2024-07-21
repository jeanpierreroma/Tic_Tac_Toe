namespace Tic_Tac_Toe.Data.Models.Players
{
    public class HardComputerPlayer : ComputerPlayer
    {
        public HardComputerPlayer()
        {
            
        }
        public HardComputerPlayer(string name, char symbol) 
            : base(name, symbol) { }

        private readonly ComputerDifficulty difficulty = ComputerDifficulty.Hard;

        public ComputerDifficulty GetDifficulty()
        {
            return difficulty;
        }
        public override void GetMove(char[][] board, Move move)
        {
            // Логіка отримання ходу для високої складності
            throw new NotImplementedException();
        }
    }
}
