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
            CurrentGame.Name = "This new game";
            Console.WriteLine ($"Welcome to {CurrentGame.Name}");
            Console.WriteLine("This is the game you will winn or loose. It´s all depends on change! ");
            Console.WriteLine("Take a guess at a number and good luck! ");
            Console.WriteLine("Min value for conquest: ");
            var minValue = Console.ReadLine();
            Console.WriteLine("Max value for conquest: "); //TODO USe split to read both i same readline
            var maxValue = Console.ReadLine();

            CurrentGame.SecretNumber = GetRandomNumber(minValue, maxValue); 
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
