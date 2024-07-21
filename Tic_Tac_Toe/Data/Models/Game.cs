using Tic_Tac_Toe.Data.Models.Players;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Runtime.Serialization;

namespace Tic_Tac_Toe.Data.Models
{
    [Serializable]
    public class Game : ISerializable
    {
        public Board Board { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player CurrentPlayer { get; set; }
        public GameState Status { get; set; }

        public Game()
        {
            
        }

        public Game(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
            CurrentPlayer = player1;
            Status = GameState.InProgress;
            Board = new Board(3);
        }

        public GameRespond MakeMove(Move move)
        {
            if (Status != GameState.InProgress)
            {
                return CreateGameRespond(GameState.Invalid, false, "Game is not in progress.");
            }

            if (Board.GetCell(move.Row, move.Column) != ' ')
            {
                return CreateGameRespond(GameState.InProgress, false, "Invalid move.");
            }

            CurrentPlayer.GetMove(Board, move);
            UpdateGameStatus(CurrentPlayer.Symbol);

            if (Status == GameState.Win || Status == GameState.Draw)
            {
                return CreateGameRespond(Status, true, Status == GameState.Win ?
                    $"{CurrentPlayer.Name} won the game!" :
                    "The result of the game is Draw!");
            }

            ToggleCurrentPlayer();

            if (CurrentPlayer.Name.Equals("Computer"))
            {
                CurrentPlayer.GetMove(Board, new Move());
                UpdateGameStatus(CurrentPlayer.Symbol);

                if (Status == GameState.Win || Status == GameState.Draw)
                {
                    return CreateGameRespond(Status, true, Status == GameState.Win ?
                        $"{CurrentPlayer.Name} won the game!" :
                        "The result of the game is Draw!");
                }

                ToggleCurrentPlayer();
            }

            return CreateGameRespond(GameState.InProgress, true, "Game in progress");
        }

        private GameRespond CreateGameRespond(GameState gameState, bool isSucced, string message)
        {
            return new GameRespond
            {
                Board = Board,
                CurrentPlayer = CurrentPlayer,
                GameState = gameState,
                IsSucced = isSucced,
                Message = message
            };
        }

        private void ToggleCurrentPlayer()
        {
            CurrentPlayer = CurrentPlayer.Equals(Player1) ? Player2 : Player1;
        }

        private void UpdateGameStatus(char currentSign)
        {
            Status = Board.CheckGameBoardStatus(currentSign);
        }



        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Board", Board.ToJson());
            info.AddValue("Player1", Player1.ToJson());
            info.AddValue("Player2", Player2.ToJson());
            info.AddValue("CurrentPlayer", CurrentPlayer.ToJson());
            info.AddValue("GameStatus", Status);
        }

        public Game(SerializationInfo info, StreamingContext context)
        {
            var boardJson = (JObject)info.GetValue("Board", typeof(JObject));
            Board = Board.FromJson(boardJson);

            var player1Json = (JObject)info.GetValue("Player1", typeof(JObject));
            Player1 = Player.FromJson(player1Json);

            var player2Json = (JObject)info.GetValue("Player2", typeof(JObject));
            Player2 = Player.FromJson(player2Json);

            var currentPlayerJson = (JObject)info.GetValue("CurrentPlayer", typeof(JObject));
            CurrentPlayer = Player.FromJson(currentPlayerJson);

            Status = (GameState)info.GetValue("GameStatus", typeof(GameState));
        }
    }
}
