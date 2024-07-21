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
        public char[][] Board { get; set; }
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
            CreateBoard(3);
        }

        private void CreateBoard(int size)
        {
            char initializationSymbol = ' ';

            Board = new char[size][];
            for (int i = 0; i < size; i++)
            {
                Board[i] = new char[size];
                for (int j = 0; j < size; j++)
                {
                    Board[i][j] = initializationSymbol;
                }
            }

        }

        public GameRespond MakeMove(Move move)
        {
            if (Status != GameState.InProgress)
            {
                return new GameRespond
                {
                    Board = Board,
                    CurrentPlayer = CurrentPlayer,
                    GameState = GameState.Invalid,
                    IsSucced = false,
                    Message = "Game is not in progress."
                };
            }

            if (!CurrentPlayer.Name.Equals("Computer") && Board[move.Row][move.Column] != ' ')
            {
                return new GameRespond
                {
                    Board = Board,
                    CurrentPlayer = CurrentPlayer,
                    GameState = GameState.InProgress,
                    IsSucced = false,
                    Message = "Invalid move."
                };
            }

            CurrentPlayer.GetMove(Board, move);

            if (UpdateGameStatus(CurrentPlayer.Symbol))
            {
                return new GameRespond
                {
                    Board = Board,
                    CurrentPlayer = CurrentPlayer,
                    IsSucced = true,
                    Message = Status == GameState.Win ?
                            $"{CurrentPlayer.Name} won the game!" :
                            "The result of the game is Draw!"
                };
            }

            ToggleCurrentPlayer();

            if (CurrentPlayer.Name.Equals("Computer"))
            {
                CurrentPlayer.GetMove(Board, new Move());

                if (UpdateGameStatus(CurrentPlayer.Symbol))
                {
                    return new GameRespond
                    {
                        Board = Board,
                        CurrentPlayer = CurrentPlayer,
                        IsSucced = true,
                        Message = Status == GameState.Win ? 
                            $"{CurrentPlayer.Name} won the game!" :
                            "The result of the game is Draw!"
                    };
                }

                ToggleCurrentPlayer();
            }

            return new GameRespond
            {
                Board = Board,
                CurrentPlayer = CurrentPlayer,
                IsSucced = true,
                Message = "Game in progress"
            };
        }

        private void ToggleCurrentPlayer()
        {
            CurrentPlayer = CurrentPlayer.Equals(Player1) ? Player2 : Player1;
        }

        private bool UpdateGameStatus(char currentSign)
        {
            // Check for win
            if (CheckHorizontalLinesForWin(currentSign) ||
                CheckVerticalLinesForWin(currentSign) ||
                CheckDiagonalLinesForWin(currentSign))
            {
                Status = GameState.Win;
                return true;
            }

            // Check for draw
            if (Board.All(row => row.All(cell => cell != ' ')))
            {
                Status = GameState.Draw;
                return true;
            }

            Status = GameState.InProgress;
            return false;
        }

        private bool CheckHorizontalLinesForWin(char currentSign)
        {
            foreach (var row in Board)
            {
                if (row.All(cell => cell == currentSign))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckVerticalLinesForWin(char currentSign)
        {
            for (int col = 0; col < Board.Length; col++)
            {
                bool win = true;
                for (int row = 0; row < Board.Length; row++)
                {
                    if (Board[row][col] != currentSign)
                    {
                        win = false;
                        break;
                    }
                }
                if (win)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckDiagonalLinesForWin(char currentSign)
        {
            return CheckMainDiagonalLineWin(currentSign) || CheckSideDiagonalLineWin(currentSign);
        }

        private bool CheckMainDiagonalLineWin(char currentSign)
        {
            for (int i = 0; i < Board.Length; i++)
            {
                if (Board[i][i] != currentSign)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckSideDiagonalLineWin(char currentSign)
        {
            for (int i = 0; i < Board.Length; i++)
            {
                if (Board[i][Board.Length - 1 - i] != currentSign)
                {
                    return false;
                }
            }
            return true;
        }



        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Board", Board);
            info.AddValue("Player1", Player1.ToJson());
            info.AddValue("Player2", Player2.ToJson());
            info.AddValue("CurrentPlayer", CurrentPlayer.ToJson());
            info.AddValue("GameStatus", Status);
        }

        public Game(SerializationInfo info, StreamingContext context)
        {
            Board = (char[][])info.GetValue("Board", typeof(char[][]));

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
