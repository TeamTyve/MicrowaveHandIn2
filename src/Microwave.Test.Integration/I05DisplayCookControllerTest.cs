using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwave.Test.Integration
{

    [TestFixture]
    class I05DisplayCookControllerTest
    {
        private CookController input;
        private Display display;
        private ITimer timer;
        private IPowerTube powerTube;
        private IOutput output;

        private static StringWriter sw;

        [SetUp]
        public void Setup()
        {
            timer = Substitute.For<ITimer>();
            powerTube = Substitute.For<IPowerTube>();

            output = Substitute.For<IOutput>();
            display = new Display(output);
            input = new CookController(timer, display, powerTube);
        }

        [TestCase(115,1,115)]
        [TestCase(10, 0, 10)]
        [TestCase(0, 0, 0)]
        public void TimerTick_DisplaysCorrectOutput(int a, int b, int c)
        {
            input.StartCooking(50, 60);
            timer.TimeRemaining.Returns(a);
            timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);


            display.ShowTime(a, b);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"Display shows: {a:D2}:{b:D2}")));
            input.Stop();


        }
    }
}