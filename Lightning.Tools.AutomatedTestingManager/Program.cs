using Lightning.Utilities; 
using System;
using System.IO;

namespace Lightning.Tools.AutomatedTestingManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lightning Automated Test Manager - July 28, 2021");
            Console.WriteLine("Automatically tests Lightning by launching all test XMLs");

            Console.WriteLine("Now testing...");

            GetLaunchArgsResult GLAR = ParseArgs(args);

            Tester Test = new Tester();

            if (GLAR.Successful
            || GLAR.LaunchArguments == null)
            {
                LaunchArgs LA = GLAR.LaunchArguments;
                Test.StandardRun(LA);
            }
            else
            {
                Console.WriteLine(GLAR.FailureReason);
                Environment.Exit(-0xd15ea5e);
            }
            

        }

        private static GetLaunchArgsResult ParseArgs(string[] LaunchArgs)
        {
            GetLaunchArgsResult GLAR = new GetLaunchArgsResult();

            switch (LaunchArgs.Length)
            {
                case 0:
                    GLAR.Successful = true;
                    return GLAR;
                default:
                    for (int i = 0; i < LaunchArgs.Length; i++)
                    {
                        string LaunchArgument = LaunchArgs[i];

                        if (LaunchArgument.ContainsCaseInsensitive("-folderpath"))
                        {
                            if (LaunchArgs.Length - i > 1)
                            {
                                string FolderPath = LaunchArgs[i + 1];

                                if (Directory.Exists(FolderPath))
                                {
                                    GLAR.LaunchArguments.Directory = FolderPath;
                                }
                                else
                                {
                                    GLAR.FailureReason = "Invalid folder path supplied for -folderpath!";
                                    return GLAR;
                                }
                            }
                            else
                            {
                                GLAR.FailureReason = "-folderpath supplied but no folder supplied!";
                                return GLAR;
                            }
                        }
                        else if (LaunchArgument.ContainsCaseInsensitive("-recurse"))
                        {
                            GLAR.LaunchArguments.Recurse = true;
                            continue; 
                        }
                        else
                        {
                            continue;
                        }
                    }

                    return GLAR; 
            }
        }

    }
}
