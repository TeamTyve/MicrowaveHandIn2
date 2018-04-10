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
    public class I06LightUserInterfaceTest
    {
        private IUserInterface iut;

        private IButton powerButton;
        private IButton timeButton;
        private IButton startCancelButton;
        private IDoor door;

        private IDisplay display;
        private ILight light;
        private ICookController cooker;
        private IOutput output;

        [SetUp]
        public void Setup()
        {
            powerButton = Substitute.For<IButton>();
            timeButton = Substitute.For<IButton>();
            startCancelButton = Substitute.For<IButton>();
            door = Substitute.For<IDoor>();

            output = Substitute.For<IOutput>();
            light = new Light(output);
            display = Substitute.For<IDisplay>();
            cooker = Substitute.For<ICookController>();

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
        public void OnTimePressed_DoorOpens_TurnOnLight()
        {
            iut.OnPowerPressed(powerButton, EventArgs.Empty);
            iut.OnTimePressed(timeButton, EventArgs.Empty);
            iut.OnDoorOpened(door, EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }

        [Test]
        public void OnCooking_DoorOpens_TurnOnLight()
        {
            iut.OnPowerPressed(powerButton, EventArgs.Empty);
            iut.OnTimePressed(timeButton, EventArgs.Empty);
            iut.OnStartCancelPressed(startCancelButton, EventArgs.Empty);
            iut.OnDoorOpened(door, EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }


    }
}
