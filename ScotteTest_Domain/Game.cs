using System;
using System.Collections.Generic;
using System.Text;

namespace ScotteTest_Domain
{
    public class Game
    {
        public string Name { get; set; }

        public int NumberOfGuesses { get; set; }
        public List<Player> CurrentPlayers { get; set; }

        public int SecretNumber { get; set; }

        public Game()
        {
            CurrentPlayers = new List<Player>(); 
        }
    }
}
