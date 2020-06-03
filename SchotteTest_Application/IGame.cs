using System;
using System.Collections.Generic;
using System.Text;
using ScotteTest_Domain; 

namespace SchotteTest_Application
{
   public interface IGame
    {
        public Game CurrentGame { get; set; }


 
        Game LoadGame(); 
        void StartGame();
        void Validate();

        void Conclusion(); 

    }
}
