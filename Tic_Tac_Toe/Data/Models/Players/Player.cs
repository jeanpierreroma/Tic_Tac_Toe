using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Tic_Tac_Toe.Data.Models.Players
{
    public abstract class Player : IEquatable<Player>
    {
        public string Name { get; set; }
        public char Symbol { get; set; }

        protected Player()
        {

        }
        protected Player(string name, char symbol)
        {
            Name = name;
            Symbol = symbol;
        }

        public abstract void GetMove(Board board, Move move);

        public static Player FromJson(JObject json)
        {
            var type = json["Type"]?.ToString();
            return type switch
            {
                "EasyComputerPlayer" => json.ToObject<EasyComputerPlayer>(),
                "MediumComputerPlayer" => json.ToObject<MediumComputerPlayer>(),
                "HardComputerPlayer" => json.ToObject<HardComputerPlayer>(),
                "HumanPlayer" => json.ToObject<HumanPlayer>(),
                _ => throw new NotSupportedException($"Unknown player type: {type}")
            };
        }
        public virtual JObject ToJson()
        {
            return new JObject
            {
                { "Type", GetType().Name },
                { "Name", Name },
                { "Symbol", Symbol }
            };
        }


        public bool Equals(Player? other)
        {
            if(other is null)
            {
                return false;
            }

            if (this.Name.Equals(other.Name)
                && this.Symbol == other.Symbol)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
