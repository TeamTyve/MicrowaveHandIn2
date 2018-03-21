using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;

namespace MicrowaveOven.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var output = new Output();
            var timer = new Timer();
            var display = new Display(output);
            var powerTube = new PowerTube(output);
            var powerBtn = new Button();
            var timeBtn = new Button();
            var startCancelBtn = new Button();
            var door = new Door();
            var cookController = new CookController(timer, display, powerTube);

            var oven = new UserInterface(powerBtn,
                timeBtn,
                startCancelBtn,
                door, display,
                new Light(output),
                cookController);
            cookController.UI = oven;

            bool running = true;
            while (running)
            {
                Console.WriteLine("--------------- Menu --------------");
                Console.WriteLine($"Press 'P' to Power the microwave");
                Console.WriteLine($"Press 'T' to Set the time for the microwave");
                Console.WriteLine($"Press 'S' or 'R'  to Start/Reset the microwave");
                Console.WriteLine($"Press 'O' to Open the microwave");
                Console.WriteLine($"Press 'C' to Close the microwave");
                Console.WriteLine($"Press 'E' to leave the microwave");
                Console.WriteLine();

                var action = Console.ReadKey(true);

                Console.Clear();

                switch (action.Key)
                {
                    case ConsoleKey.P:
                        powerBtn.Press();
                        break;
                    case ConsoleKey.T:
                        timeBtn.Press();
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.R:
                        startCancelBtn.Press();
                        break;
                    case ConsoleKey.O:
                        door.Open();
                        break;
                    case ConsoleKey.C:
                        door.Close();
                        break;
                    case ConsoleKey.E:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Not a valid Command, try something from the menu");
                        break;
                }
            }

            Console.WriteLine("Action ---");

        }
    }
}
