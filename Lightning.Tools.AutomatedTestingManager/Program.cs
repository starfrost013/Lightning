using NuCore.Utilities; 
using System;
using System.IO;

namespace Lightning.Tools.AutomatedTestingManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lightning Automated Test Manager - July 28, 2021");
            Console.WriteLine("Manages automated testing of Lightning in preparation for the 8/13 SDK release.");

            GetLaunchArgsResult GLAR = ParseArgs(args);

            Tester Test = new Tester();

            if (GLAR.Successful
            || GLAR.LaunchArguments == null)
            {
                LaunchArgs LA = GLAR.LaunchArguments;

                Console.WriteLine("Testing...");
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
                    ShowHelp();
                    GLAR.FailureReason = "No options supplied!";
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
                                    continue; 
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
                        else if (LaunchArgument.ContainsCaseInsensitive("-lightningpath"))
                        {
                            if (LaunchArgument.Length - i > 1)
                            {
                                string LightningFolderPath = LaunchArgs[i + 1];

                                if (Directory.Exists(LightningFolderPath))
                                {
                                    GLAR.LaunchArguments.LightningDirectory = LightningFolderPath;
                                    continue; 
                                }
                                else
                                {
                                    GLAR.FailureReason = "Invalid folder path supplied for -lightningpath!";
                                    return GLAR; 
                                }
                            }
                            else
                            {
                                GLAR.FailureReason = "-lightningpath supplied but no folder supplied!";
                                return GLAR;
                            }
                        }
                        else if (LaunchArgument.ContainsCaseInsensitive("-processlifetime"))
                        {
                            if (LaunchArgument.Length - i > 1)
                            {
                                string LightningLaunchTime = LaunchArgs[i + 1];

                                try
                                {
                                    GLAR.LaunchArguments.ProcessLifetime = Convert.ToInt32(LightningLaunchTime);
                                    continue;
                                }
                                catch (Exception)
                                {
                                    GLAR.FailureReason = "Invalid process lifetime supplied!";
                                    return GLAR;
                                }
                            }
                            else
                            {
                                GLAR.FailureReason = "-processlifetime supplied but no process lifetime supplied!";
                                return GLAR;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }

                    GLAR.Successful = true;
                    return GLAR; 
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Lightning.Tools.AutomatedTestingManager [options]" +
            "\n\nOptions:\n-folderpath [folder path]: The path of the XML or LGX files you wish to test with. Default: <directory>\\Tests \n" +
            "-recurse: If supplied, AutomatedTestingManager will search for XML/LGX files to launch Lightning with in the folder name supplied by -folderpath recursively.\n" +
            "-lightningpath: Path to the folder containing the Lightning EXE file.\n" +
            "-processlifetime: Time to test each Lightning instance for in milliseconds.");

        }
    }
}
