using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Smtp;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class I09UserInterfaceCookControllerTest
    {
        private IUserInterface input;
        private ICookController cooker;
        private IDisplay display;
        private ILight light;
        private IButton powerButton, timeButton, startCancelButton;
        private IDoor door;
        private IPowerTube powerTube;
        private IOutput output;
        private ITimer timer;

        [SetUp]
        public void Setup()
        {
            powerButton = Substitute.For<IButton>();
            timeButton = Substitute.For<IButton>();
            startCancelButton = Substitute.For<IButton>();
            door = Substitute.For<IDoor>();
            output = Substitute.For<IOutput>();
            powerTube = new PowerTube(output);
            display = new Display(output);
            light = new Light(output);

            timer = new Timer();

            cooker = new CookController(timer, display, powerTube, input);
            input = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cooker);
        }

        [Test]
        public void OnButtonPressed_ResultIs_CookingStarted()
        {
            input.OnPowerPressed(powerButton, EventArgs.Empty);
            input.OnTimePressed(timeButton, EventArgs.Empty);
            input.OnStartCancelPressed(startCancelButton, EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50 W")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("01:00")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("cleared")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50 W")));

        }

        [Test]
        public void CookingIsDone_TurnedOff()
        {
            input.OnPowerPressed(powerButton, EventArgs.Empty);
            input.OnTimePressed(timeButton, EventArgs.Empty);
            input.OnStartCancelPressed(startCancelButton, EventArgs.Empty);
            input.CookingIsDone();

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50 W")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("01:00")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("cleared")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50 W")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void DoorOpened_StopCooking_DisplayTurnedOff()
        {
            input.OnPowerPressed(powerButton, EventArgs.Empty);
            input.OnTimePressed(timeButton, EventArgs.Empty);
            input.OnStartCancelPressed(startCancelButton, EventArgs.Empty);
            input.OnDoorOpened(door, EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50 W")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("01:00")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("cleared")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50 W")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void StartCancelButtonPressed_DisplayCleared()
        {
            input.OnPowerPressed(powerButton, EventArgs.Empty);
            input.OnTimePressed(timeButton, EventArgs.Empty);
            input.OnStartCancelPressed(startCancelButton, EventArgs.Empty);
            input.OnStartCancelPressed(startCancelButton, EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50 W")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("01:00")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("cleared")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50 W")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("cleared")));

        }
    }
}
