using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    /// <summary>
    /// ScriptErrorManager
    /// 
    /// April 20, 2021 (GUILTY Edition)
    /// 
    /// Handles the throwing of script errors.
    /// </summary>
    public static class ScriptErrorManager
    {

        public static void ThrowScriptError(string Name)
        {
           GetErrorResult Err = ErrorManager.GetError(Name);

           if (!Err.Successful)
           {
                ErrorManager.ThrowError("Script Error Handler", new Error { Id = 0x7171DEAD, Name = "TriedToThrowInvalidErrorException", Description = $"Attempted to throw an invalid Error - {Name} does not exist!", Severity = MessageSeverity.FatalError });
                return; 
           }
           else
            {
                HandleScriptError(Err.Error);
            }
        }

        /// <summary>
        /// ScriptErrors require special handling. Therefore we put them here. Woooooooooooo!
        /// </summary>
        /// <param name="Err"></param>
        private static void HandleScriptError(ScriptError Err)
        {

            string WarningErrText = $"Script Warning:\n\nIn script: {Err.Name} at line {Err.Line}:\n\n{Err.Id}: {Err.Description}!";
            string ErrorErrText = $"Script Error:\n\nIn script: {Err.Name} at line {Err.Line}:\n\n{Err.Id}: {Err.Description}!";

            switch (Err.Severity)
            {
                case MessageSeverity.Message:
                    Logging.Log(WarningErrText, "Script Error Handler");
                    return;
                case MessageSeverity.Warning:
                    Logging.Log(WarningErrText, "Script Error Handler");
                    MessageBox.Show(ErrorErrText, "Script Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
            }
        }
    }
}
