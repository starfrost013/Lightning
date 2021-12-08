using NuCore.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    /// <summary>
    /// WindowCollection
    /// 
    /// September 1, 2021 (modified September 7, 2021)
    /// 
    /// Defines a collection of windows.
    /// </summary>
    public class WindowCollection : IEnumerable
    {
        /// <summary>
        /// Fake ClassName used for WindowCollection
        /// </summary>
        private string ClassName => "WindowCollection";

        /// <summary>
        /// INTERNAL: List of windows used for windowcollection
        /// </summary>
        public List<Window> Windows { get; set; }

        public WindowCollection()
        {
            Windows = new List<Window>();
        }

        public WindowCollection(List<Window> WindowList)
        {
            Windows = WindowList; 
        }

        IEnumerator IEnumerable.GetEnumerator() => (WindowCollectionEnumerator)GetEnumerator(); 
        public WindowCollectionEnumerator GetEnumerator() => new WindowCollectionEnumerator(Windows); 

        public void Add(WindowSettings Settings)
        {
            Logging.Log("Creating window...", ClassName);

            if (Settings == null)
            {
                ErrorManager.ThrowError("WindowCollection.Add()", "NRInvalidWindowSettingsException");
                return; 
            }
            else
            {
                Window Win = new Window();
                Win.Settings = Settings;
                Add_CheckForDefaultSettings(Win);
                Add_PerformAdd(Win);
                return;
            }

        }

        private void Add_CheckForDefaultSettings(Window Window)
        {
            if (Window.Settings.ApplicationName == null) Window.Settings.ApplicationName = "NuRender Window";

            if (Window.Settings.WindowSize == null) Window.Settings.WindowSize = new Vector2Internal(1152, 864);
            if (Window.Settings.Viewport == null) Window.Settings.Viewport = new Vector2Internal(1152, 864);


        }

        private void Add_PerformAdd(Window Window)
        {
            Windows.Add(Window);
            Window.Init(); // initialise the window.
        }

        public Window this[int i] => Windows[i];
    }

    public class WindowCollectionEnumerator : IEnumerator
    {
        public int Position = -1;

        object IEnumerator.Current => (object)Current;

        /// <summary>
        /// Fake ClassName used for error manager
        /// </summary>
        private string ClassName => "WindowCollectionEnumerator";

        public Window Current
        {
            get
            {
                try
                {
                    return Windows[Position];
                }
                catch (IndexOutOfRangeException ex)
                {
#if DEBUG
                    ErrorManager.ThrowError("WindowCollection", "NRAttemptedToAccessNonexistentWindowException", $"Attempted to access invalid window ID {Position} (max {Windows.Count - 1}!).\n\n{ex}");
#else
                    ErrorManager.ThrowError("WindowCollection", "NRAttemptedToAccessNonexistentWindowException", "Attempted to access invalid window ID {Position} (max {Windows.Count - 1}!).");
#endif
                    return null;
                }
                
            }
            set
            {
                throw new InvalidOperationException("Attempted to call WindowCollection.Current.set(); somehow!");
            }
        }

        public List<Window> Windows { get; set; }

        public WindowCollectionEnumerator(List<Window> WindowList)
        {
            Windows = WindowList; 
        }

        public bool MoveNext()
        {
            Position++;
            return (Position < Windows.Count);
        }

        public void Reset() => Position = -1; 
     
        public void SendEventToAllWindows()
        {
            foreach (Window Win in Windows)
            {
                Win.Main(); 
            }
        }
    }
}
