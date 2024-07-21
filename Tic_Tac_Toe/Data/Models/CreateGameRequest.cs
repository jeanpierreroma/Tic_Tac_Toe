namespace Tic_Tac_Toe.Data.Models
{
    public class CreateGameRequest
    {
        public string Player1Name { get; set; }
        public string Player2Name { get; set; }
        public bool PlayAgainstComputer { get; set; }
        public ComputerDifficulty Difficulty { get; set; }
    }
}
