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
            light = Substitute.For<ILight>();
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
        public void OnStartCancelPressed()
        {
            powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        }

    }
}
