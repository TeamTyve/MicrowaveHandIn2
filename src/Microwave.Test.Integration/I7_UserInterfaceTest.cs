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
    class I7_UserInterfaceTest
    {
        private Display output;
        private UserInterface input;
        private IOutput Outputfortest;
        private IButton powerButton;
        private IButton timeButton;
        private IButton startCancelButton;
        private IDoor door;
        private ILight light;
        private ICookController cookController;
        private static StringWriter sw;

        [SetUp]
        public void Setup()
        {
            Outputfortest = new Output();
            output = new Display(Outputfortest);
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
                output,
                light,
                cookController);
            sw = new StringWriter();
            Console.SetOut(sw);

        }


        [Test]
        public void Ready_DoorOpenClose_ready_power50()
        {
            door.Opened += Raise.EventWith(this, EventArgs.Empty);
            door.Closed += Raise.EventWith(this, EventArgs.Empty);
            powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            string expected = string.Format($"Display shows: 50 W{Environment.NewLine}");
            Assert.That(expected, Is.EqualTo(sw.ToString()));

        }

        [Test]
        public void Ready_powerbutton_power50()
        {

            sw.GetStringBuilder().Clear();
            powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            string expected = string.Format($"Display shows: 50 W{Environment.NewLine}");
            Assert.That(expected, Is.EqualTo(sw.ToString()));

        }

        [Test]
        public void Ready_21powerbutton_power50()
        {

            for (int i = 50; i <= 1000; i += 50)
            {
                powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            }
            sw.GetStringBuilder().Clear();
            powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            string expected = string.Format($"Display shows: 350 W{Environment.NewLine}");
            Assert.That(expected, Is.EqualTo(sw.ToString()));

        }

        [Test]
        public void CancelButton_DisplayCleared_power50()
        {
            powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            sw.GetStringBuilder().Clear();
            startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

            string expected = string.Format($"Display cleared{Environment.NewLine}");
            Assert.That(expected, Is.EqualTo(sw.ToString()));


        }

        [Test]
        public void Dooropened_DisplayCleared_power50()
        {

            powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            sw.GetStringBuilder().Clear();
            door.Opened += Raise.EventWith(this, EventArgs.Empty);
            string expected = string.Format($"Display cleared{Environment.NewLine}");
            Assert.That(expected, Is.EqualTo(sw.ToString()));

        }

        [Test]
        public void TimeButton_power50()
        {

            powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            sw.GetStringBuilder().Clear();

            timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            string expected = string.Format($"Display shows: 01:00{Environment.NewLine}");
            Assert.That(expected, Is.EqualTo(sw.ToString()));

        }

        [Test]
        public void TimeButton_Pressed2_power50()
        {

                powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
                timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            sw.GetStringBuilder().Clear();
            timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

                string expected = string.Format($"Display shows: 02:00{Environment.NewLine}");
                Assert.That(expected, Is.EqualTo(sw.ToString()));

        }

        [Test]
        public void DoorOpened_TimeButtonPressed_power50()
        {

                powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
                timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            sw.GetStringBuilder().Clear();
            door.Opened += Raise.EventWith(this, EventArgs.Empty);

                string expected = string.Format($"Display cleared{Environment.NewLine}");
                Assert.That(expected, Is.EqualTo(sw.ToString()));

        }

        [Test]
        public void Cooking_CookingDone_DisplayCleares()
        {

                powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
                timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            sw.GetStringBuilder().Clear();
            startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

                string expected = string.Format($"Display cleared{Environment.NewLine}");
                Assert.That(expected, Is.EqualTo(sw.ToString()));

        }
    }
}
