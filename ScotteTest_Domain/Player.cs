using System;
using System.Collections.Generic;
using System.Text;

namespace ScotteTest_Domain
{
    public class Player
    {
        public Player()
        {
            PlayersGuesses = new List<int>(); 
        }
        public string Id { get; set; }
        public string Name { get; set; }

        public bool IsAWinner { get; set; }

        public int ClosestNumber { get; set; }

        public int Diff { get; set; }
        public List<int> PlayersGuesses { get; set; }
    }
}
