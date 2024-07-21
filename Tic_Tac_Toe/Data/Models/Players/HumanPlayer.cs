namespace Tic_Tac_Toe.Data.Models.Players
{
    public class HumanPlayer : Player
    {
        public HumanPlayer()
        {
            
        }
        public HumanPlayer(string name, char symbol) : base(name, symbol) { }

        public override void GetMove(char[][] board, Move move)
        {
            board[move.Row][move.Column] = this.Symbol;
        }
    }
}
