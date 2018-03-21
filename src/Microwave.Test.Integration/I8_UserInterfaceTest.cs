using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Smtp;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    class I8_UserInterfaceTest
    {
        private IUserInterface Iut;
        private ICookController cooker;
        private IDisplay display;
        private ILight light;
        private IButton powerButton, timeButton, startCancelButton;
        private IDoor door;

        [SetUp]
        public void Setup()
        {
            cooker = Substitute.For<ICookController>();
            display = Substitute.For<IDisplay>();
            light = Substitute.For<ILight>();
            powerButton = Substitute.For<IButton>();
            timeButton = Substitute.For<IButton>();
            startCancelButton = Substitute.For<IButton>();
            door = Substitute.For<IDoor>();
            Iut = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cooker);
        }

        [Test]
        public void OnButtonPressed_ResultIs_CookingStarted()
        {
            powerButton.Press();
            startCancelButton.Press();
            startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            cooker.Received().StartCooking(20, 20);

        }
    }
}
