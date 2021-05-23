using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Polaris.Core
{
    /// <summary>
    /// ConsoleRedirector
    /// 
    /// May 22, 2021
    /// 
    /// Redirects the Console for the Polaris Output UI.
    /// </summary>
    public class ConsoleRedirector : TextWriter
    {
        private Encoding _encoding { get; set; }

        public EventHandler<ConsoleRedirectorEventArgs> StringRead { get; set; }
        public EventHandler<ConsoleRedirectorEventArgs> StringWritten { get; set; }

        /// <summary>
        /// Console.Write / Console.WriteLine compatible
        /// 
        /// The last string written to the fake console.
        /// </summary>
        public string LastStringWritten { get; set; }

        public override Encoding Encoding
        {
            get
            {
                return _encoding;
            }
        }

        public string Read() => LastStringWritten;

        public override void Write(char value)
        {
            LastStringWritten = value.ToString();

            ConsoleRedirectorEventArgs CREA = new ConsoleRedirectorEventArgs();
            CREA.TheString = LastStringWritten;

            StringWritten(this, CREA);
        }

        public override void Write(string value)
        {
            LastStringWritten = value; 

            ConsoleRedirectorEventArgs CREA = new ConsoleRedirectorEventArgs();
            CREA.TheString = LastStringWritten;

            StringWritten(this, CREA);
        }

        public override void WriteLine(string value) => Write($"{value}\n");

        public override void Write(decimal value) => Write(value.ToString());
         
        public override void Write(float value) => Write(value.ToString());

        public override void Write(double value) => Write(value.ToString());

        public override void Write(int value) => Write(value.ToString());

        public override void Write(uint value) => Write(value.ToString());

        public override void Write(long value) => Write(value.ToString());

        public override void Write(ulong value) => Write(value.ToString());

        
    }
}
