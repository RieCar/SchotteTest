using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
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
            if (string.IsNullOrEmpty(CurrentGame.Name))
            {
                Console.WriteLine("Name can't be empty! Input your name once more");
                CurrentGame.Name = Console.ReadLine();
            }
         
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
            //CurrentGame.NumberOfGuesses = int.Parse(Console.ReadLine());
            int amountOfGuesses;
            while (!int.TryParse(Console.ReadLine(), out amountOfGuesses))
            {
                Console.WriteLine("Please Enter a valid numerical value!");
         
            }
            CurrentGame.NumberOfGuesses = amountOfGuesses;
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
            var guessANumber = true; 
            var counter = 0;
            do
            {
                while (counter < CurrentGame.NumberOfGuesses)
                {
                    Console.WriteLine("Round: " + (counter +1));
                    foreach (var player in CurrentGame.CurrentPlayers)
                    {
                        Console.WriteLine($"{player.Name.ToUpper()} guess your number: ");
               
                        int guess; 
                        while (!int.TryParse(Console.ReadLine(), out guess))
                        {
                            Console.WriteLine("Please Enter a valid numerical value!");

                        }
                        player.PlayersGuesses.Add(guess);
                        if (guess == CurrentGame.SecretNumber)
                        {
                            guessANumber = false;
                            player.IsAWinner = true; 
                            counter = CurrentGame.NumberOfGuesses; 
                            break; 
                        }
                    }
                    counter++;
                }
                guessANumber = false; 
            } while (guessANumber);
     
        }

        private int GetRandomNumber(string min, string max)
        {
            Random rnd = new Random();
            int secretNumber = rnd.Next(int.Parse(min), int.Parse(max)); // creates a number between min and max
            Console.WriteLine("secret " + secretNumber);
            return secretNumber;
        }

        public Player Conclusion()
        {
            foreach(var player in CurrentGame.CurrentPlayers)
            {
                player.ClosestNumber = player.PlayersGuesses.OrderBy(item => Math.Abs(CurrentGame.SecretNumber - item)).First();
            }
            int currentNearest = CurrentGame.CurrentPlayers[0].ClosestNumber;
            int currentDifference = Math.Abs(currentNearest - CurrentGame.SecretNumber);
          
            for (int i = 1; i < CurrentGame.CurrentPlayers.Count; i++)
            {
                int diff = Math.Abs(CurrentGame.CurrentPlayers[i].ClosestNumber - CurrentGame.SecretNumber);
                if (diff < currentDifference)
                {
                    currentDifference = diff;
                    currentNearest = CurrentGame.CurrentPlayers[i].ClosestNumber;
                    CurrentGame.CurrentPlayers[i].Diff = currentDifference; 
              
                }
            }
            var winner = CurrentGame.CurrentPlayers.Where(p => p.PlayersGuesses.Contains(currentNearest)).FirstOrDefault(); 
            return winner; 
        }
    }
}
