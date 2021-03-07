using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    public static class Logging
    {
        public static string FileName { get; set; }
        public static void Log(string Text, string Component = null, MessageSeverity Severity = MessageSeverity.Message)
        {
            switch (Severity)
            {
                case MessageSeverity.Message:
                    LogText(Text, Component, Severity); 
                    return;
                case MessageSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    LogText(Text, Component, Severity);
                    return;
                case MessageSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    LogText(Text, Component, Severity);
                    return;
                // "BSOD" style for fatals
                case MessageSeverity.FatalError:
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    LogText(Text, Component, Severity);
                    return;
            }
        }

        public static void LogError(Error Err, string Component = null)
        {
            if (Component == null)
            {
                Log($"Error {Err.Id}: Severity {Err.Severity}: {Err.Name} ({Err.Description})", "Error Logging Component", Err.Severity);
            }
            else
            {
                Log($"Error {Err.Id}: Severity {Err.Severity}: {Err.Name} ({Err.Description})", "Error Logging Component", Err.Severity);
            }
            
        }
        
        private static void LogText(string Text, string Component = null, MessageSeverity Severity = MessageSeverity.Message)
        {

            StringBuilder LogTextSB = new StringBuilder();

            string DateString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            // ISO date format
            LogTextSB.Append(DateString);

            if (Component == null)
            {
                LogTextSB.Append($" [Lightning Game Engine: {Severity}");
            }
            else
            {
                LogTextSB.Append($"[{Component}]");
            }

            LogTextSB.Append($" - {Text}");

            // Write the line we have built, and a newline, to the console.
            Console.WriteLine(LogTextSB.ToString());

            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
