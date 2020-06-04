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
            int maxvalue = -1;
            int minvalue = -1;
            bool isNotValid = true;
            var values = new List<int>(); 
            Console.WriteLine("What's the games name? : ");
            var name = Console.ReadLine();
            validateIsEmpty(name); 
         
         
            Console.WriteLine("Set min and max values(integers) for the random value (use a ',' between the min and max value) ");

            do
            {
                var input = Console.ReadLine().Split(',').ToList();

                bool isTwoNumbers = input.Count == 2;

                var list = input
                        .Select(s =>
                        {
                            int i;
                            return Int32.TryParse(s, out i) ? i : -1;
                        }).ToList();
                bool isParseFailure = list.Contains(-1); 

                if(isTwoNumbers == false && isParseFailure == true)
                {
                    Console.WriteLine("You need to set the values by patterna ex '1,10' for min , max value");
                    Console.WriteLine("The values has to be integers"); 
                }
                else if(isTwoNumbers == true && isParseFailure == true)
                {
                    Console.WriteLine("The values has to be integers");
                }
                else if(isTwoNumbers == false && isParseFailure == false)
                {
                    Console.WriteLine("You need to set the values by patterna ex '1,10' for min , max value");
                }
                else
                {
                    isNotValid = false;
                    values = list; 

                }

            } while (isNotValid);      

            if (values[0] < values[1])
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
           // string numberOfPlayers = Console.ReadLine();

            int numberOfPlayers;
            while (!int.TryParse(Console.ReadLine(), out numberOfPlayers))
            {
                Console.WriteLine("Please Enter a valid numerical value!");

            }
         
            for (var i=1; i <= numberOfPlayers; i++)
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
          
        }

        public void Validate()
        {
            var guessANumber = true; 
            var counter = 0;
            do
            {
                var guessedNumber = new List<int>();
                while (counter < CurrentGame.NumberOfGuesses)
                {
                    int guess = -1; 
                    Console.WriteLine("Round: " + (counter +1));
                    foreach (var player in CurrentGame.CurrentPlayers)
                    {
                        Console.WriteLine($"{player.Name.ToUpper()} guess your number: ");
                       
                        bool isNotValid = true;

                        do
                        {
                            var input = Console.ReadLine();
                            int isInteger;
                            bool parseIntSuccess = int.TryParse(input, out isInteger);

                            bool isTaken = guessedNumber.Contains(isInteger);

                            if (isTaken == true && parseIntSuccess == false)
                            {
                                Console.WriteLine("Please Enter a numerical value that not has been taken before!");
                                Console.WriteLine("Please Enter a valid numerical value!");
                            }
                            else if (isTaken == false && parseIntSuccess == false)
                            {
                                Console.WriteLine("Please Enter a valid numerical value!");
                            }
                            else if (isTaken == true && parseIntSuccess == true)
                            {
                                Console.WriteLine("Please Enter a numerical value that not has been taken before!");
                            }
                            else
                            {
                                isNotValid = false;
                                guess = isInteger;
                            }
                        }
                        while (isNotValid);
                                                  
                        guessedNumber.Add(guess);
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

        private int GetRandomNumber(int min, int max)
        {
            Random rnd = new Random();
            int secretNumber = rnd.Next(min, max); // creates a number between min and max
            Console.WriteLine("secret " + secretNumber);
            return secretNumber;
        }

        public List<Player> Conclusion()
        {

            foreach(var player in CurrentGame.CurrentPlayers)
            {
                player.ClosestNumber = player.PlayersGuesses.OrderBy(item => Math.Abs(CurrentGame.SecretNumber - item)).First();
            }
            int currentNearest = CurrentGame.CurrentPlayers[0].ClosestNumber;
            int currentDifference = Math.Abs(currentNearest - CurrentGame.SecretNumber);

            //setting start value for diff
            foreach (var item in CurrentGame.CurrentPlayers)
            {
                item.Diff = Math.Abs(item.ClosestNumber - CurrentGame.SecretNumber); 
            }
          
            for (int i = 1; i < CurrentGame.CurrentPlayers.Count; i++)
            {
                int diff = Math.Abs(CurrentGame.CurrentPlayers[i].ClosestNumber - CurrentGame.SecretNumber);

                if (diff < currentDifference)
                {
                    currentDifference = diff;
                    currentNearest = CurrentGame.CurrentPlayers[i].ClosestNumber;     
                }
            }
            var winner = CurrentGame.CurrentPlayers.Where(p => p.Diff == currentDifference).ToList(); 
            return winner; 
        }

        private void validateIsEmpty(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("This can't be empty!");
                CurrentGame.Name = Console.ReadLine();
            }
            else
            {
                CurrentGame.Name = input;
            }
        }
    }
}
