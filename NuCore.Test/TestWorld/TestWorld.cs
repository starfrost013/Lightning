using NuCore.Utilities;
using NuRender.SDL2; 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NuRender.Test
{
    /// <summary>
    /// TestWorld
    /// 
    /// November 2021
    /// 
    /// Defines the test world, where all tests are held.
    /// </summary>
    public class TestWorld
    {
        /// <summary>
        /// Stopwatch used for managing tests.
        /// </summary>
        private Stopwatch TestStopwatch { get; set; }
        
        /// <summary>
        /// Settings of this testworld instance - see <see cref="WorldSettings"/>
        /// </summary>
        public WorldSettings Settings { get; set; }

        /// <summary>
        /// The list of tests - see <see cref="Test"/> and <see cref="RegisterTests"/>
        /// </summary>
        public List<Test> Tests { get; set; }

        private Test CurTest { get; set; }

        private int _curtestnumber { get; set; }

        /// <summary>
        /// Fake class name for use with the Error Manager
        /// </summary>
        internal string ClassName => "TestWorld";

        private int CurTestNumber
        {
            get
            {
                return _curtestnumber;    
            }
            set
            {
                _curtestnumber = value;

                if (_curtestnumber >= 0 && _curtestnumber < Tests.Count)
                {
                    CurTest = Tests[_curtestnumber]; 
                }
                
            }
        }

        public TestWorld()
        {
            Settings = new WorldSettings();
            TestStopwatch = new Stopwatch();
            Tests = new List<Test>();
        }

        
        public void RunTests(Scene Sc)
        {
            TestStopwatch.Start();

            TestLoop(Sc);

            TestStopwatch.Stop();

        }

        private void TestLoop(Scene Sc)
        {

            if (Tests.Count < 1) return;

            int TotalTestTime = Settings.TestTime * (Tests.Count + 1);
            CurTestNumber = 0;

            while (TestStopwatch.ElapsedMilliseconds < TotalTestTime)
            {
                switch (CurTest.State) // test state machine
                {
                    case TestState.ToRun:
                        if (CurTest.Name == null)
                        {
                            SDL.SDL_SetWindowTitle(Sc.Windows[0].Settings.RenderingInformation.WindowPtr, $"{Sc.Windows[0].Settings.ApplicationName} - PLEASE NAME THIS TEST!");
                        }
                        else
                        {
                            SDL.SDL_SetWindowTitle(Sc.Windows[0].Settings.RenderingInformation.WindowPtr, $"{Sc.Windows[0].Settings.ApplicationName} - Running test {CurTest.Name}");
                        }

                        CurTest.State = TestState.Running;
                       
                        TestResult TR = CurTest.Method(Sc); // todo: return value
                        
                        if (TR.Successful)
                        {
                            CurTest.State = TestState.Completed;
                        }
                        else
                        {
                            CurTest.State = TestState.Error;

                        }
                        continue; // its a while loop ok kate
                    case TestState.Running:
                    case TestState.Completed:
                        int MaxForCurTest = Settings.TestTime * (CurTestNumber + 1);

                        if (TestStopwatch.ElapsedMilliseconds > MaxForCurTest)
                        {
                            CurTestNumber++;
                        }

                        continue; 
                    case TestState.Error:
                        ErrorManager.ThrowError("TestWord", "NRAttemptedToPerformInvalidTestException", $"The test {CurTest.Name} failed."); //todo: get reason
                        continue; 
                    
                }
            }

        }


    }
}
