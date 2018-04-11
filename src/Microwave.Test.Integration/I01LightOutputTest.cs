using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy.Internal;
using NUnit.Framework;
using MicrowaveOvenClasses.Boundary;
using NSubstitute;
using NUnit.Framework.Internal;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class I01LightOutputTest
    {
        private Light input;
        private Output output;
        private StringWriter sw;

        [SetUp]
        public void Setup()
        {
            output = new Output();
            input = new Light(output);
            sw = new StringWriter();
            Console.SetOut(sw);
        }

        [Test]
        public void TurnOn_WasOff_CorrectOutput()
        {

            input.TurnOn();
            string expected = $"Light is turned on{Environment.NewLine}";
            Assert.That(sw.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void TurnOff_WasOn_CorrectOutput()
        {
            input.TurnOn();
            sw.Flush();
            input.TurnOff();
            string expected = $"Light is turned off{Environment.NewLine}";
            Assert.That(sw.ToString().Contains("turned off"), Is.EqualTo(expected.Contains("turned off")));
        }

        [Test]
        public void TurnOff_WasOff_Output()
        {
            sw.Flush();
            input.TurnOff();
            string expected = $"";
            Assert.That(sw.ToString(), Is.EqualTo(expected));
        }

        [TearDown]
        public void Dispose()
        {
            sw.Flush();
        }
    }
}
