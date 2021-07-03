using Lightning.Utilities; 
using System;
using System.Linq; 

/// <summary>
/// Lightning.Tools.ErrorConvert
/// 
/// July 2, 2021
/// 
/// Converts old-style error registration system (Errors.xml) to new-style (ErrorRegistration.cs). Quick and dirty. Requires Lightning.Core.API 
/// </summary>
namespace Lightning.Tools.ErrorConvert
{
    public class Program
    {
        static void Main(string[] Args)
        {
            
            if (Args.Length == 0) 
            {
                PrintHelpMsgAndExit();
            }
            else
            {
                StandardRun(Args);
            }
            
        }

        private static void PrintHelpMsgAndExit()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(Strings.STRING_HELPMSG);

            Console.ForegroundColor = ConsoleColor.White; 

            Environment.Exit(0);
        }

        private static void StandardRun(string[] Args)
        {
            GetLaunchArgumentResult GLAR = GetLaunchArguments(Args);

            if (GLAR.Successful
                || GLAR.Arguments == null)
            {
                ErrorConverter EC = new ErrorConverter();
                GenericResult GR = EC.ConvertOldToNew(GLAR.Arguments);

                if (!GR.Successful)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {GR.FailureReason}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Successfully wrote new-format error registration code to {GLAR.Arguments.NewFile}!");
                }

                Console.ResetColor();
            }
            else
            {
                Environment.Exit(0xD15EA5E);
            }
        }

        private static GetLaunchArgumentResult GetLaunchArguments(string[] Args)
        {
            GetLaunchArgumentResult LAR = new GetLaunchArgumentResult();

            if (Args.Length < 2)
            {
                LAR.FailureReason = Strings.STRING_ERROR_NOT_ENOUGH_ARGUMENTS;
                return LAR;
            }
            else
            {

                // ignore any parameter not supplied with actual information
                LAR.Arguments.OldFile = Args[0];
                LAR.Arguments.NewFile = Args[1];

                if (Args.Length > 2)
                {
                    string[] NonFileArgs = Args.GetRange(2);

                    for (int i = 0; i < NonFileArgs.Length; i++)
                    {
                        string NonFileArgument = NonFileArgs[i];

                        switch (NonFileArgument)
                        {
                            case "-lightning-namespace": // namespace
                                if ((NonFileArgs.Length - 1) - i < 2)
                                {
                                    LAR.Successful = true; 
                                    return LAR;
                                }
                                else
                                {
                                    LAR.Arguments.Namespace = NonFileArgs[i + 1];
                                    LAR.Successful = true; 
                                    return LAR; 
                                }

                               
                        }
                    }
                }
                else
                {
                    LAR.Successful = true; 
                    return LAR;
                }

            }

            return LAR; // should never run 
        }
    }
}
