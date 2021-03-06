using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Utilities
{
    /// <summary>
    /// Global error manager
    /// </summary>
    public static class ErrorManager
    {
        public static List<Error> Errors { get; set; }

        public static void Init()
        {
            Errors = new List<Error>();
        }

        private static bool DoesErrorExist(Error Err) => Errors.Contains(Err);
        private static bool DoesErrorExist(string ErrName)
        {
            foreach (Error Err in Errors)
            {
                if (Err.Name == ErrName) return true;
            }

            return false;
        }

        /// <summary>
        /// temp: pre-result class
        /// 
        /// Get an error with the name ErrorName.
        /// </summary>
        /// <param name="ErrName"></param>
        /// <returns></returns>
        private static Error GetError(string ErrName)
        {
            foreach (Error Err in Errors)
            {
                if (Err.Name == ErrName) return Err;
            }

            return null;
        }


        private static bool DoesErrorExist(int Id)
        {
            foreach (Error Err in Errors)
            {
                if (Err.Id == Id) return true;
            }

            return false;
        }


        public static void ThrowError(string Component, Error Err) => HandleError(Component, Err);

        private static void HandleError(string Component, Error Err)
        {
            if (Err.CustomErrHandler != null)
            {
                Err.CustomErrHandler(Err);
                return; 
            }
            else
            {
                if (Component != null)
                {
                    Logging.Log($"");
                }
                else
                {

                }

                switch (Err.Severity)
                {
                    case MessageSeverity.Message:

                        return;
                    case MessageSeverity.Warning:
                        return;
                    case MessageSeverity.Error:
                        return;
                    case MessageSeverity.FatalError:

                        // Temporary - we don't have a clean shutdown method yet

                        return;

                }
            }
        }
    }
}
