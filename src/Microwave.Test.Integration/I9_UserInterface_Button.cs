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
    [TestFixture()]
    // ReSharper disable once InconsistentNaming
    public class I9_UserInterface_Button
    {
        private IUserInterface iut;
        private IButton powerButton;
        private IButton timeButton;
        private IButton startCancelButton;
        private IDoor door;
        private IOutput output;
        private IDisplay display;
        private ILight light;
        private ICookController cookController;

        [SetUp]
        public void SetUp()
        {
            output = Substitute.For<IOutput>();
            powerButton = Substitute.For<IButton>();
            timeButton = Substitute.For<IButton>();
            startCancelButton = new Button();
            door = new Door();
            display = new Display(output);
            light = new Light(output);
            cookController = Substitute.For<ICookController>();
            iut = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cookController);
        }

        // Start-CancelButton

        [Test]
        public void StartCancelButton_SetPower_DoesntTurnLightOff()
        {
            iut.OnPowerPressed(powerButton, EventArgs.Empty);
            iut.OnStartCancelPressed(startCancelButton, EventArgs.Empty);

            // In the code Light is being turned off, even though it was never turned on. This negates the call and is an error.
            // We've chosen not to change the source code.
            output.DidNotReceive().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void StartCancelButton_SetPower_ReturnsReadyState()
        {
            iut.OnPowerPressed(powerButton, EventArgs.Empty);
            iut.OnStartCancelPressed(startCancelButton, EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Display")));
        }

        [Test]
        public void StartCancelButton_StartCooking_ReturnsReadyState()
        {
            iut.OnPowerPressed(powerButton, EventArgs.Empty);
            iut.OnTimePressed(timeButton, EventArgs.Empty);
            iut.OnStartCancelPressed(startCancelButton, EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }

        [Test]
        public void StartCancelButton_StopCooking_ReturnsReadyState_ClearedCalled()
        {
            iut.OnPowerPressed(powerButton, EventArgs.Empty);
            iut.OnTimePressed(timeButton, EventArgs.Empty);
            iut.OnTimePressed(timeButton, EventArgs.Empty);
            iut.OnStartCancelPressed(startCancelButton, EventArgs.Empty);

            // Turn off light redundant again.
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("cleared")));
        }

        // PowerButton

        [Test]
        public void PowerButton_IncreasePower_ReturnsPowerState()
        {
            iut.OnPowerPressed(powerButton, EventArgs.Empty);
            iut.OnPowerPressed(powerButton, EventArgs.Empty);

            // Turn off light redundant again.
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("100")));
        }

        // TimeButton
        [Test]
        public void TimeButton_TimeButtonPressed_ReturnsTimeState()
        {
            iut.OnPowerPressed(powerButton, EventArgs.Empty);
            iut.OnTimePressed(timeButton, EventArgs.Empty);
            iut.OnTimePressed(timeButton, EventArgs.Empty);

            // Turn off light redundant again.
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("01:00")));
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("02:00")));
        }
    }
}
