using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    class IT4_CookControllerTest
    {
        //private UserInterface input;
        private Timer outputTimer;
        private PowerTube outputPowerTube;
        private Output outputOutput;

        private IUserInterface ui;

        [SetUp]
        public void Setup()
        {
            ui = Substitute.For<IUserInterface>();
            outputTimer = new Timer();
            outputPowerTube = new PowerTube(outputOutput);
            outputOutput = new Output();

        }

        [Test]

    }
}
