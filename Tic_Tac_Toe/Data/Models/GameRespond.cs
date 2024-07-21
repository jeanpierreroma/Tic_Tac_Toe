using Tic_Tac_Toe.Data.Models.Players;

namespace Tic_Tac_Toe.Data.Models
{
    public class GameRespond
    {
        public Board Board { get; set; }
        public Player CurrentPlayer { get; set; }
        public GameState GameState { get; set; }
        public bool IsSucced { get; set; }
        public string Message { get; set; }
    }
}
