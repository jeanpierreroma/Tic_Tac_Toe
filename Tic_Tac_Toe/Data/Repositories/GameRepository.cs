using Tic_Tac_Toe.Data.Models.Players;
using Tic_Tac_Toe.Data.Models;
using Tic_Tac_Toe.Data.Interfaces;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Text.Json;
using Newtonsoft.Json;

namespace Tic_Tac_Toe.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly string _filePath = "GameData.json";

        private Game _game;

        public GameRepository()
        {
            _game = LoadGame();
        }

        private Game LoadGame()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    var json = File.ReadAllText(_filePath);
                    return JsonConvert.DeserializeObject<Game>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading game: {ex.Message}");
            }

            return null;
        }
        public Game CreateGame(Player player1, Player player2)
        {
            _game = new Game(player1, player2);
            if(File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            SaveGame(_game);
            return _game;
        }

        public GameRespond MakeMove(Move move)
        {
            _game = LoadGame();
            var gameRespond = _game.MakeMove(move);
            SaveGame(_game);
            return gameRespond;
        }

        public void SaveGame(Game currentGame)
        {
            try
            {
                var json = JsonConvert.SerializeObject(currentGame);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving game: {ex.Message}");
            }
        }
    }
}
