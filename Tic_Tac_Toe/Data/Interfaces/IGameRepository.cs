using Tic_Tac_Toe.Data.Models.Players;
using Tic_Tac_Toe.Data.Models;

namespace Tic_Tac_Toe.Data.Interfaces
{
    public interface IGameRepository
    {
        Game CreateGame(Player player1, Player player2);
        GameRespond MakeMove(Move move);
        void SaveGame(Game currentGame);
    }
}
