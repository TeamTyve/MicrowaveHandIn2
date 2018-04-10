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
    class I08UserInterfaceDisplayTest
    {
        private Display display;
        private UserInterface input;
        private IOutput output;
        private IButton powerButton;
        private IButton timeButton;
        private IButton startCancelButton;
        private IDoor door;
        private ILight light;
        private ICookController cookController;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();
            display = new Display(output);
            powerButton = Substitute.For<IButton>();
            timeButton = Substitute.For<IButton>();
            startCancelButton = Substitute.For<IButton>();
            door = Substitute.For<IDoor>();
            light = Substitute.For<ILight>();
            cookController = Substitute.For<ICookController>();
            input = new UserInterface(powerButton,
                timeButton,
                startCancelButton,
                door,
                display,
                light,
                cookController);
        }


        [Test]
        public void Ready_DoorOpenClose_ready_power50()
        {
            door.Opened += Raise.EventWith(this, EventArgs.Empty);
            door.Closed += Raise.EventWith(this, EventArgs.Empty);
            powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50 W")));
        }

        [Test]
        public void Ready_powerbutton_power50()
        {

            powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50 W")));


        }

        [Test]
        public void Ready_21powerbutton_power50()
        {

            for (int i = 50; i <= 1000; i += 50)
            {
                powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            }
            powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("350")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("700")));
        }

        [Test]
        public void CancelButton_DisplayCleared_power50()
        {
            powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("cleared")));
        }

        [Test]
        public void Dooropened_DisplayCleared_power50()
        {

            powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            door.Opened += Raise.EventWith(this, EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("cleared")));
        }

        [Test]
        public void TimeButton_power50()
        {

            powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

            timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("01:00")));
        }

        [Test]
        public void TimeButton_Pressed2_power50()
        {

            powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("02:00")));
        }

        [Test]
        public void DoorOpened_TimeButtonPressed_power50()
        {

            powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            door.Opened += Raise.EventWith(this, EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("cleared")));
        }

        [Test]
        public void Cooking_CookingDone_DisplayCleares()
        {

            powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("cleared")));
        }
    }
}
