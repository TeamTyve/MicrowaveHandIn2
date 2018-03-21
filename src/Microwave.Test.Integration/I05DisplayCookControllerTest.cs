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
        private Display output;
        private ITimer timer;
        private IPowerTube powerTube;
        private IOutput Outputfortest;
        private static StringWriter sw;
        [SetUp]
        public void Setup()
        {
            timer = Substitute.For<ITimer>();
            powerTube = Substitute.For<IPowerTube>();

            Outputfortest = new Output();
            output = new Display(Outputfortest);
            input = new CookController(timer,output,powerTube);
            sw = new StringWriter();
            Console.SetOut(sw);
        }

        [TestCase(115,1,55)]
        [TestCase(10, 0, 10)]
        [TestCase(0, 0, 0)]
        public void TimerTick_DisplaysCorrectOutput(int a, int b, int c)
        {
            input.StartCooking(50, 60);
            timer.TimeRemaining.Returns(a);
            timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);


            sw.GetStringBuilder().Clear();
            output.ShowTime(a, b);
                string expected = string.Format($"Display shows: {a:D2}:{b:D2}{Environment.NewLine}");
                Assert.That(expected, Is.EqualTo(sw.ToString()));
            input.Stop();

        }
    }
}