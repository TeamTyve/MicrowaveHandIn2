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
        private UserInterface userInterface;
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
            userInterface = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cookController);
        }
    }
}
