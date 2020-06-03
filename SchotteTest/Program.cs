using System;
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

            game.LoadGame();


            // Console.WriteLine(current.Name);

            game.StartGame();

        }
    }
}
