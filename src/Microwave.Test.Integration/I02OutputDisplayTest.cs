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
    [TestFixture]
    class I02OutputDisplayTest
    {
        private Display input;
        private Output output;

        [SetUp]
        public void Setup()
        {
            output = new Output();
            input = new Display(output);
        }

        [TestCase(2,30)]
        public void ShowTime_MinAndSec_FormattedString(int a, int b)
        {

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                input.ShowTime(a, b);
                string expected = string.Format($"Display shows: {a:D2}:{b:D2}{Environment.NewLine}");
                Assert.That(expected, Is.EqualTo(sw.ToString()));
            }
        }

        [TestCase(50)]
        public void ShowPower_Power_FormattedString(int a)
        {

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                input.ShowPower(a);
                string expected = string.Format($"Display shows: {a} W{Environment.NewLine}");
                Assert.That(expected, Is.EqualTo(sw.ToString()));
            }
        }

        [Test]
        public void Clear_ReturnsString()
        {

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                input.Clear();
                string expected = string.Format($"Display cleared{Environment.NewLine}");
                Assert.That(expected, Is.EqualTo(sw.ToString()));
            }
        }
    }
}
