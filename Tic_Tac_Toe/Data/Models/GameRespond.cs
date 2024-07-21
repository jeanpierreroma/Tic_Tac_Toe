using Tic_Tac_Toe.Data.Models.Players;

namespace Tic_Tac_Toe.Data.Models
{
    public class GameRespond
    {
        public Board Board { get; set; } = null!;
        public Player CurrentPlayer { get; set; } = null!;
        public GameState GameState { get; set; }
        public bool IsSucced { get; set; }
        public string Message { get; set; } = null!;
    }
}
