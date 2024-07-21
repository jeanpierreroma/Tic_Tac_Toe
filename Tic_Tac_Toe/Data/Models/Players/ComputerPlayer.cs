namespace Tic_Tac_Toe.Data.Models.Players
{
    public abstract class ComputerPlayer : Player
    {
        protected ComputerPlayer()
        {
            
        }
        protected ComputerPlayer(string name, char symbol) 
            : base(name, symbol) { }

        public ComputerDifficulty Difficulty { get; set; }
        public override abstract void GetMove(Board board, Move move);

    }
}
