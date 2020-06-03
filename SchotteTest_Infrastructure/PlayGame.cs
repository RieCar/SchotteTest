using System;
using System.Collections.Generic;
using System.Text;
using SchotteTest_Application;
using ScotteTest_Domain;

namespace SchotteTest_Infrastructure
{
    public class PlayGame : IGame
    {
        public Game CurrentGame { get; set; }

        public PlayGame()
        {
            CurrentGame = new Game(); 
        }

        public Game LoadGame()
        {
            string maxvalue = "";
            string minvalue = ""; 
            Console.WriteLine("What's the games name? : ");
            CurrentGame.Name = Console.ReadLine();          

            Console.WriteLine("Set min and max values for the random value (use a ',' between min and max value) ");
            var values = Console.ReadLine().Split(',');
            if(int.Parse(values[0]) < int.Parse(values[1]))
            {
                minvalue = values[0]; 
                maxvalue = values[1];
            }
            else
            {
                minvalue = values[1];
                maxvalue = values[0];
            }
    
            CurrentGame.SecretNumber = GetRandomNumber(minvalue, maxvalue);

            Console.WriteLine("How many attemps will the players have? : ");
            CurrentGame.NumberOfGuesses = int.Parse(Console.ReadLine()); 
            return CurrentGame; 
           
        }

        public void StartGame()
        {
            Console.WriteLine("How many players?: ");
            string numberOfPlayers = Console.ReadLine(); 
            for(var i=1; i <= int.Parse(numberOfPlayers); i++)
            {
                Player player= new Player();
                Console.WriteLine($"Name for player {i}");
                player.Name = Console.ReadLine();
                player.Id = $"{i}";
                CurrentGame.CurrentPlayers.Add(player);
            }

            Console.Write("Up to test ");
            for (var i = 0; i < CurrentGame.CurrentPlayers.Count; i++)
            {
                if(i == CurrentGame.CurrentPlayers.Count -1)
                {
                    Console.Write($" {CurrentGame.CurrentPlayers[i].Name} ");
                }
                else
                {
                    Console.Write($" {CurrentGame.CurrentPlayers[i].Name} vs ");
                }                            
            }
            Console.WriteLine("");
            Console.WriteLine("Let´s go!");
        }

        public void Validate()
        {
            throw new NotImplementedException();
        }

        private int GetRandomNumber(string min, string max)
        {
            Random rnd = new Random();
            int secretNumber = rnd.Next(int.Parse(min), int.Parse(max)); // creates a number between min and max
       
            return secretNumber;
        }
    }
}
