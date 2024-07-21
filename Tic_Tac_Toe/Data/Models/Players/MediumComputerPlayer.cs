namespace Tic_Tac_Toe.Data.Models.Players
{
    public class MediumComputerPlayer : ComputerPlayer
    {
        public MediumComputerPlayer()
        {
            
        }
        public MediumComputerPlayer(string name, char symbol) : base(name, symbol) { }

        private readonly ComputerDifficulty difficulty = ComputerDifficulty.Medium;

        public ComputerDifficulty GetDifficulty()
        {
            return difficulty;
        }

        public override void GetMove(Board board, Move move)
        {
            // Check for win by 1 move

            // Distract oppenent by 1 move

            // Random move
        }
    }
}
