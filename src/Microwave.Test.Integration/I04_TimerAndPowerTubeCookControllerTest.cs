using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    class I04_TimerAndPowerTubeCookControllerTest
    {
        private CookController input;
        private ITimer outputTimer;
        private IPowerTube powerTube;
        private IOutput outputOutput;
        private IDisplay display;
        private IUserInterface ui;



        [SetUp]
        public void Setup()
        {
            outputTimer = Substitute.For<ITimer>();
            powerTube = Substitute.For<IPowerTube>();
            outputOutput = Substitute.For<IOutput>();
            display = Substitute.For<IDisplay>();
            ui = Substitute.For<IUserInterface>();
            input = new CookController(outputTimer, display, powerTube, ui);

        }

        [TestCase(50,30)]
        [TestCase(0,0)]
        [TestCase(1, 1000)]
        public void StartCooking_ResultIsCallToTimerAndPowerTube(int a, int b)
        {
            input.StartCooking(a,b);
            outputTimer.Received(1).Start(b);
            powerTube.Received(1).TurnOn(a);
        }

        [Test]
        public void TimerExpired_ResultIsCallToTurnOffPowerTube()
        {
            input.StartCooking(50, 50);
            outputTimer.Expired += Raise.EventWith(this, EventArgs.Empty);
            powerTube.Received(1).TurnOff();
        }

        [Test]
        public void StopCooking_ResultIsFunctionsCalled()
        {
            input.Stop();
            outputTimer.Received(1).Stop();
            powerTube.Received(1).TurnOff();
        }

    }
}
