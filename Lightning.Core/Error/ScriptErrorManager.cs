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


        /// <summary>
        /// ScriptErrors require special handling. Therefore we put them here. Woooooooooooo!
        /// </summary>
        /// <param name="Err"></param>
        public static void ThrowScriptError(ScriptError Err)
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
