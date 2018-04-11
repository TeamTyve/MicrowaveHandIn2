using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class I03PowerTubeOutputTest
    {
        private IPowerTube input;
        private IOutput output;
        private StringWriter sw;


        [SetUp]
        public void Setup()
        {
            output = new Output();
            input = new PowerTube(output);
            sw = new StringWriter();
            Console.SetOut(sw);
        }

        [TestCase(0)]
        [TestCase(701)]
        public void TurnOn_ThrowsException(int power)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => input.TurnOn(power));
        }

        [TestCase(50)]
        [TestCase(700)]
        public void TurnOn_NoThrow(int power)
        {
            Assert.DoesNotThrow(() => input.TurnOn(power));
        }

        [Test]
        public void TurnOn_IsOn_ThrowsException()
        {
            input.TurnOn(50);
            Assert.Throws<ApplicationException>(() => input.TurnOn(50));
        }

        [Test]
        public void TurnOff_IsOn_OutputsTurnedOff()
        {
            input.TurnOn(50);
            sw = new StringWriter();
            Console.SetOut(sw);
            input.TurnOff();
            var expected = $"PowerTube turned off{Environment.NewLine}";
            Assert.That(expected, Is.EqualTo(sw.ToString()));
        }

        [Test]
        public void TurnOn_IsOff_OutputIsOn()
        {
            sw = new StringWriter();
            Console.SetOut(sw);
            input.TurnOn(50);
            var expected = $"PowerTube works with 50 W{Environment.NewLine}";
            Assert.That(expected, Is.EqualTo(sw.ToString()));
        }

        [TearDown]
        public void Dispose()
        {
            sw.Flush();
        }

    }
}
