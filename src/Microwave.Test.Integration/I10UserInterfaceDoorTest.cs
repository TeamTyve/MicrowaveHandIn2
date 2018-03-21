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
    public class I10UserInterfaceDoorTest
    {
        private IUserInterface iut;
        private IButton powerButton;
        private IButton timeButton;
        private IButton startCancelButton;
        private IDoor door;
        private IDisplay display;
        private ILight light;
        private ICookController cookController;
        private IOutput output;

        [SetUp]
        public void SetUp()
        {
            powerButton = Substitute.For<IButton>();
            timeButton = Substitute.For<IButton>();
            startCancelButton = Substitute.For<IButton>();
            door = new Door();
            display = Substitute.For<IDisplay>();
            output = Substitute.For<IOutput>();
            light = new Light(output);
            cookController = Substitute.For<ICookController>();
            iut = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cookController);
        }

        [Test]
        public void DoorOpens_TurnsOnLight()
        {
            iut.OnDoorOpened(door, EventArgs.Empty);

            // Turns Light on (Opens door)
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }

        [Test]
        public void DoorOpens_DoorClosing_TurnsOffLight()
        {
            iut.OnDoorOpened(door, EventArgs.Empty);

            iut.OnDoorClosed(door, EventArgs.Empty);

            // Turns Light on (Opens door)
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
            // Turns light off (closes door)
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void PressPower_DoorOpening_OpensDoor_TurnsOnLight()
        {
            iut.OnPowerPressed(powerButton, EventArgs.Empty);

            iut.OnDoorOpened(door, EventArgs.Empty);

            // Turns Light on (Opens door)
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }

        [Test]
        public void PressPowerAndTime_OpensDoor_TurnsOnLight()
        {
            iut.OnPowerPressed(powerButton, EventArgs.Empty);
            iut.OnTimePressed(timeButton, EventArgs.Empty);

            iut.OnDoorOpened(door, EventArgs.Empty);

            // Opens Door
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }

        [Test]
        public void PressPowerAndTime_StartCooking_DoorOpen_TurnsLightOn()
        {
            iut.OnPowerPressed(powerButton, EventArgs.Empty);
            iut.OnTimePressed(timeButton, EventArgs.Empty);
            iut.OnStartCancelPressed(startCancelButton, EventArgs.Empty);

            iut.OnDoorOpened(door, EventArgs.Empty);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }
    }
}
