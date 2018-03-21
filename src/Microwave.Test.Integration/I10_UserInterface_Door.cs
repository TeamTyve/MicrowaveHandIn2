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
    public class I10_UserInterface_Door
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

        }
    }
}
