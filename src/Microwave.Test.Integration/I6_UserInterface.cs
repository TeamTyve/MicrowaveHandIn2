using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    public class I6_UserInterface
    {
        private IUserInterface iut;

        private IButton powerButton;
        private IButton timeButton;
        private IButton startCancelButton;
        private IDoor door;

        private IDisplay display;
        private ILight light;
        private IPowerTube powerTube;
        private ICookController cooker;
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
            timer = Substitute.For<ITimer>();
            powerTube = Substitute.For<IPowerTube>();
            light = new Light(output);
            display = Substitute.For<IDisplay>();
            cooker = new CookController(timer, display, powerTube);

            iut = new UserInterface(
                powerButton, timeButton, startCancelButton,
                door,
                display,
                light,
                cooker);
        }


        [Test]
        public void DoorOpens_TurnsOnLight()
        {
            iut.OnDoorOpened(door, EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }

        [Test]
        public void DoorCloses_TurnsOffLight()
        {
            iut.OnDoorOpened(door, EventArgs.Empty);
            output.ClearReceivedCalls();
            iut.OnDoorClosed(door, EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void OnPowerPressed_DoorOpens_TurnOnLight()
        {
            iut.OnPowerPressed(powerButton, EventArgs.Empty);
            iut.OnDoorOpened(door, EventArgs.Empty);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }






        [Test]
        public void OnStartCancelPressed_DoorOpened_NoOutput()
        {
            door.Open();
            output.ClearReceivedCalls();

            iut.OnStartCancelPressed(startCancelButton, EventArgs.Empty);

            output.DidNotReceive().OutputLine(Arg.Any<string>());
        }

        [Test]
        public void OnStartCancelPressed_DoorClosed_NoOutput()
        {
            door.Open();
            output.ClearReceivedCalls();
            door.Close();

            iut.OnStartCancelPressed(startCancelButton, EventArgs.Empty);

            output.DidNotReceive().OutputLine(Arg.Any<string>());
        }

        [Test]
        public void OnStartCancelPressed_ReadyState_NoOutput()
        {
            iut.OnStartCancelPressed(startCancelButton, EventArgs.Empty);

            output.DidNotReceive().OutputLine(Arg.Any<string>());
        }

        [Test]
        public void OnStartCancelPressed_SetPowerState_NoOutput()
        {
            iut.OnPowerPressed(powerButton, EventArgs.Empty);
            iut.OnStartCancelPressed(startCancelButton, EventArgs.Empty);

            output.DidNotReceive().OutputLine(Arg.Any<string>());
            //output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void OnStartCancelPressed_SetPowerState_LightIsTurnedOn()
        {
            iut.OnPowerPressed(powerButton, EventArgs.Empty);

            light.TurnOff();

            iut.OnStartCancelPressed(startCancelButton, EventArgs.Empty);

            output.DidNotReceive().OutputLine(Arg.Any<string>());
        }

        [Test]
        public void OnStartCancelPressed_SetPowerState_LightsTurnOff()
        {
            iut.OnPowerPressed(powerButton, EventArgs.Empty);
            light.TurnOn();

            iut.OnStartCancelPressed(startCancelButton, EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void OnTimeButtonPressedAndOnStartCancelPressed_SetTimeState_LightTurnOn()
        {
            iut.OnPowerPressed(powerButton, EventArgs.Empty);
            iut.OnTimePressed(timeButton, EventArgs.Empty);
            iut.OnStartCancelPressed(startCancelButton, EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }

        [Test]
        public void OnStartCancelPressed_SetTimeState_LightIsTurnedOn()
        {
            iut.OnPowerPressed(powerButton, EventArgs.Empty);
            iut.OnTimePressed(timeButton, EventArgs.Empty);

            light.TurnOff();

            iut.OnStartCancelPressed(startCancelButton, EventArgs.Empty);

            output.DidNotReceive().OutputLine(Arg.Any<string>());
        }

    }
}
