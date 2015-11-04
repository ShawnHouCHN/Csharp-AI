using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().speak();
        }
        private void speak()
        {
            SpeechSynthesizer ss = new SpeechSynthesizer();
            ss.Speak("Velkommen til kapellet!");
        }
    }
}
