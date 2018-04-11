using System;
using System.Runtime.InteropServices;
using System.Threading;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace MicrowaveOven.Automatic.Application
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

            Console.WriteLine("--------------- Menu --------------");
            Console.WriteLine($"Press 'P' to Power the microwave");
            Console.WriteLine($"Press 'T' to Set the time for the microwave");
            Console.WriteLine($"Press 'S' or 'R'  to Start/Reset the microwave");
            Console.WriteLine($"Press 'O' to Open the microwave");
            Console.WriteLine($"Press 'C' to Close the microwave");
            Console.WriteLine($"Press 'E' to leave the microwave");
            Console.WriteLine();

            Thread.Sleep(1000);
            Console.WriteLine("Powerbutton pressed");

            powerBtn.Press();

            Thread.Sleep(250);
            Console.WriteLine("Powerbutton pressed");

            powerBtn.Press();
            Thread.Sleep(250);
            Console.WriteLine("Powerbutton pressed");
            powerBtn.Press();
            Thread.Sleep(250);

            Console.WriteLine("Timebutton pressed");
            timeBtn.Press();
            Thread.Sleep(250);

            Console.WriteLine("Startbutton pressed");
            startCancelBtn.Press();
            Thread.Sleep(10000);

            Console.WriteLine("Startbutton pressed");
            startCancelBtn.Press();

            Thread.Sleep(250);

            Console.WriteLine("Door Opens");
            door.Open();

            Thread.Sleep(250);

            Console.WriteLine("Door Closes");
            door.Close();

        }
    }
}
