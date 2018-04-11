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
using NUnit.Framework.Internal;

namespace Microwave.Test.Integration
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    public class I10ButtonUserInterfaceTest
    {
        private IUserInterface userInterface;
        private IButton powerButton;
        private IButton timeButton;
        private IButton startCancelButton;
        private IDoor door;
        private IOutput output;
        private IDisplay display;
        private ILight light;
        private ICookController cookController;
        private ITimer timer;
        private IPowerTube powerTube;

        [SetUp]
        public void SetUp()
        {
            output = Substitute.For<IOutput>();
            powerButton = new Button();
            timeButton = new Button();
            startCancelButton = new Button();
            door = new Door();
            display = new Display(output);
            light = new Light(output);
            timer = new Timer();
            powerTube = new PowerTube(output);
            cookController = new CookController(timer, display, powerTube, userInterface);
            userInterface = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cookController);
        }

        // Start-CancelButton

        [Test]
        public void StartCancelButton_SetPower_DoesntTurnLightOff()
        {
            powerButton.Press();
            startCancelButton.Press();

            // In the code Light is being turned off, even though it was never turned on. This negates the call and is an error.
            // We've chosen not to change the source code.
            output.DidNotReceive().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void StartCancelButton_SetPower_ReturnsReadyState()
        {
            powerButton.Press();
            startCancelButton.Press();

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Display")));
        }

        [Test]
        public void StartCancelButton_StartCooking_ReturnsReadyState()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }

        [Test]
        public void StartCancelButton_StopCooking_ReturnsReadyState_ClearedCalled()
        {
            powerButton.Press();
            timeButton.Press();
            timeButton.Press();
            startCancelButton.Press();

            // Turn off light redundant again.
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("cleared")));
        }

        // PowerButton

        [Test]
        public void PowerButton_IncreasePower_ReturnsPowerState()
        {
            powerButton.Press();
            powerButton.Press();

            // Turn off light redundant again.
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("100")));
        }

        // TimeButton
        [Test]
        public void TimeButton_TimeButtonPressed_ReturnsTimeState()
        {
            powerButton.Press();
            timeButton.Press();
            timeButton.Press();

            // Turn off light redundant again.
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("01:00")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("02:00")));
            userInterface.OnStartCancelPressed(startCancelButton, EventArgs.Empty);
        }
    }
}
