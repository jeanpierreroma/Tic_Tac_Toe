using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tic_Tac_Toe.Data.Interfaces;
using Tic_Tac_Toe.Data.Models;
using Tic_Tac_Toe.Data.Models.Players;

namespace Tic_Tac_Toe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;

        public GameController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [HttpPost("create")]
        public IActionResult CreateGame([FromBody] CreateGameRequest request)
        {
            Player player1 = new HumanPlayer(request.Player1Name, 'X');
            Player player2;

            if (request.PlayAgainstComputer)
            {
                player2 = request.Difficulty switch
                {
                    ComputerDifficulty.Easy => new EasyComputerPlayer("Computer", 'O'),
                    ComputerDifficulty.Medium => new MediumComputerPlayer("Computer", 'O'),
                    ComputerDifficulty.Hard => new HardComputerPlayer("Computer", 'O'),
                    _ => throw new ArgumentException("Invalid difficulty level")
                };
            }
            else
            {
                player2 = new HumanPlayer(request.Player2Name, 'O');
            }

            var game = _gameRepository.CreateGame(player1, player2);
            return Ok(game);
        }

        [HttpPost("move")]
        public IActionResult MakeMove([FromBody] MoveRequest request)
        {
            var gameRespond = _gameRepository.MakeMove(
                new Move
                {
                    Row = request.Row,
                    Column = request.Column,
                });
            return Ok(gameRespond);
        }

        [HttpGet("get-computer-difficulties")]
        public IActionResult GetAllComputerDifficulties()
        {
            var difficulties = Enum.GetValues(typeof(ComputerDifficulty))
                .Cast<ComputerDifficulty>()
                .Select(d => new
                {
                    Name = d.ToString(),
                    Value = (int)d
                })
                .ToList();

            return Ok(difficulties);
        }
    }
}
