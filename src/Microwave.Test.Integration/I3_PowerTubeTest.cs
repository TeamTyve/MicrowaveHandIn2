using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    class I3_PowerTubeTest
    {
        private PowerTube input;
        private Output output;


        [SetUp]
        public void Setup()
        {
            output = new Output();
            input = new PowerTube(output);
            Console.WriteLine();
        }

        [TestCase(0)]
        [TestCase(101)]
        public void TurnOn_ThrowsException(int power)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => input.TurnOn(power));
        }

        [TestCase(1)]
        [TestCase(100)]
        public void TurnOn_NoThrow(int power)
        {
            Assert.DoesNotThrow(() => input.TurnOn(power));
            Console.WriteLine();
        }

        [Test]
        public void TurnOn_IsOn_ThrowsException()
        {
            input.TurnOn(1);
            Assert.Throws<ApplicationException>(() => input.TurnOn(1));
        }

        [Test]
        public void TurnOff_IsOn_OutputsTurnedOff()
        {
            input.TurnOn(1);
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                input.TurnOff();
                string expected = string.Format("PowerTube turned off{0}", Environment.NewLine);
                Assert.That(expected, Is.EqualTo(sw.ToString()));
            }
        }

    }
}
