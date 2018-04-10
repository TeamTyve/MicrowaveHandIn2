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
    public class I11DoorUserInterfaceTest
    {
        private IUserInterface userInterface;
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
            userInterface = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cookController);
        }

        [Test]
        public void DoorOpens_TurnsOnLight()
        {
            door.Open();

            // Turns Light on (Opens door)
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }

        [Test]
        public void DoorOpens_DoorClosing_TurnsOffLight()
        {
            door.Open();

            door.Close();

            // Turns Light on (Opens door)
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
            // Turns light off (closes door)
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void PressPower_DoorOpening_OpensDoor_TurnsOnLight()
        {
            userInterface.OnPowerPressed(powerButton, EventArgs.Empty);

            door.Open();

            // Turns Light on (Opens door)
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }

        [Test]
        public void PressPowerAndTime_OpensDoor_TurnsOnLight()
        {
            userInterface.OnPowerPressed(powerButton, EventArgs.Empty);
            userInterface.OnTimePressed(timeButton, EventArgs.Empty);

            door.Open();

            // Opens Door
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }

        [Test]
        public void PressPowerAndTime_StartCooking_DoorOpen_TurnsLightOn()
        {
            userInterface.OnPowerPressed(powerButton, EventArgs.Empty);
            userInterface.OnTimePressed(timeButton, EventArgs.Empty);
            userInterface.OnStartCancelPressed(startCancelButton, EventArgs.Empty);

            door.Open();

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }
    }
}
