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
    class I8_UserInterfaceTest
    {
        private IUserInterface iut;
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

            
            iut = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cooker);
        }

        [Test]
        public void OnButtonPressed_ResultIs_CookingStarted()
        {
            iut.OnPowerPressed(powerButton, EventArgs.Empty);
            iut.OnTimePressed(timeButton, EventArgs.Empty);
            iut.OnStartCancelPressed(startCancelButton, EventArgs.Empty);
            cooker.Received().StartCooking(50, 60);
        }

        [Test]
        public void CookingIsDone_ResultIsFunctionsCalled()
        {
            iut.OnPowerPressed(powerButton, EventArgs.Empty);
            iut.OnTimePressed(timeButton, EventArgs.Empty);
            iut.OnStartCancelPressed(startCancelButton, EventArgs.Empty);
            iut.CookingIsDone();
            display.Received().Clear();
            light.Received().TurnOff();
        }

        [Test]
        public void DoorOpened_StopCooking()
        {
            iut.OnPowerPressed(powerButton, EventArgs.Empty);
            iut.OnTimePressed(timeButton, EventArgs.Empty);
            iut.OnStartCancelPressed(startCancelButton, EventArgs.Empty);
            iut.OnDoorOpened(door, EventArgs.Empty);
            cooker.Received().Stop();
        }

        [Test]
        public void StartCancelButtonPressed_ResultIsStopCooking()
        {
            iut.OnPowerPressed(powerButton, EventArgs.Empty);
            iut.OnTimePressed(timeButton, EventArgs.Empty);
            iut.OnStartCancelPressed(startCancelButton, EventArgs.Empty);
            iut.OnStartCancelPressed(startCancelButton, EventArgs.Empty);
            cooker.Received().Stop();
        }
    }
}
