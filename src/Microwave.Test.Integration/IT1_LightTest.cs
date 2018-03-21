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
    public class IT1_LightTest
    {
        private Light input;
        private Output output;

        [SetUp]
        public void Setup()
        {
            output = new Output();
            input = new Light(output);  
        }

        [Test]
        public void TurnOn_WasOff_CorrectOutput()
        { 
        
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                input.TurnOn();
                string expected = string.Format("Light is turned on{0}", Environment.NewLine);
                Assert.That(expected, Is.EqualTo(sw.ToString()));
            }

        }

        [Test]
        public void TurnOff_WasOn_CorrectOutput()
        {
            input.TurnOn();
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                input.TurnOff();
                string expected = string.Format("Light is turned off{0}", Environment.NewLine);
                Assert.That(expected, Is.EqualTo(sw.ToString()));
            }
                
        }

    }
}
