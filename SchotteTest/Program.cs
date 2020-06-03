using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using SchotteTest_Application;
using SchotteTest_Infrastructure;
using ScotteTest_Domain;

namespace SchotteTest
{
    class Program
    {
        static void Main(string[] args)
        {

            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddTransient<IGame, PlayGame>()
                .BuildServiceProvider();


            var game = serviceProvider.GetService<IGame>();
            Console.WriteLine("Hello! Start with some settings for the game:"); 
            game.LoadGame();
            Console.WriteLine("**********************************************************************");
            Console.WriteLine(""); 

            Console.WriteLine($"Welcome to {game.CurrentGame.Name.ToUpper()}");
            Console.WriteLine("This is the game you will winn or loose. It´s all depends on change! ");
            Console.WriteLine("Take a guess at a number and good luck! ");
         
            game.StartGame();

            game.Validate();
            var winner = game.CurrentGame.CurrentPlayers.Where(p => p.IsAWinner == true).FirstOrDefault();
            if (winner != null)
            {
                Console.WriteLine($"Congrats  {winner.Name.ToUpper()} you guessed the correct number"); 
            }
            else
            {
                Console.WriteLine("No obviuos winner"); 
            }
        }
    }
}
