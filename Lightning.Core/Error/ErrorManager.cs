using Lightning.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Lightning.Core
{
    /// <summary>
    /// Global error manager
    /// </summary>
    public static class ErrorManager
    {
        public static ErrorCollection Errors { get; set; }

        public static void Init()
        {
            Errors = new ErrorCollection();
        }

        private static bool DoesErrorExist(Error Err) => Errors.ErrorList.Contains(Err);

        /// <summary>
        /// temp: pre-result class
        /// 
        /// Get an error with the name ErrorName.
        /// </summary>
        /// <param name="ErrName"></param>
        /// <returns></returns>
        private static GetErrorResult GetError(string ErrName)
        {
            GetErrorResult ErrResult = new GetErrorResult();

            foreach (Error Err in Errors)
            {
                if (Err.Name == ErrName)
                {
                    // by default a bool is false
                    ErrResult.Successful = true;
                    ErrResult.Error = Err;
                    return ErrResult;
                }

            }

            return ErrResult;
        }

        /// <summary>
        /// Get an error with the name ErrorName.
        /// </summary>
        /// <param name="ErrName"></param>
        /// <returns></returns>
        private static GetErrorResult GetError(int ErrorId)
        {
            GetErrorResult ErrResult = new GetErrorResult();

            foreach (Error Err in Errors)
            {
                if (Err.Id == ErrorId)
                {
                    ErrResult.Successful = true;
                    ErrResult.Error = Err;
                    return ErrResult;
                }
            }

            return ErrResult; 
        }

        /// <summary>
        /// Throw an error from Component <paramref name="Component"/> with the <see cref="Error"/> object <paramref name="Err"/>.
        /// </summary>
        /// <param name="Component">The component you wish to throw the error from. Optional </param>
        /// <param name="Err"></param>
        public static void ThrowError(string Component, Error Err) => HandleError(Component, Err);

        public static void ThrowError(string Component, string ErrorName)
        {
            GetErrorResult ErrToThrow = GetError(ErrorName);

            if (ErrToThrow.Successful)
            {
                ThrowError(Component, ErrToThrow.Error);     
            }
            else
            {
                ThrowError("Error Handler - InnerException", new Error { Id = 0xDEADBABE, Severity = MessageSeverity.FatalError, Name = "AttemptedToThrowInvalidErrorException", Description = $"Internal error: Attempted to throw nonexistent error name {ErrorName}" });
            }
        }

        public static void ThrowError(string Component, int ErrorId)
        {
            GetErrorResult ErrToThrow = GetError(ErrorId);

            if (ErrToThrow.Successful)
            {
                ThrowError(Component, ErrToThrow.Error);
            }
            else
            {
                ThrowError("Error Handler - InnerException", new Error { Id = 0xD3ADBABE, Severity = MessageSeverity.FatalError, Name = "AttemptedToThrowInvalidErrorException", Description = $"Internal error: Attempted to throw nonexistent error ID {ErrorId}" });
            }

        }

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
                    Logging.LogError(Err);
                }
                else
                {
                    Logging.LogError(Err, Component);
                }

                switch (Err.Severity)
                {
                    case MessageSeverity.Message:
                        MessageBox.Show($"{Err.Description}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    case MessageSeverity.Warning:
                        MessageBox.Show($"Warning ({Err.Id}; {Err.Name}): {Err.Description}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    case MessageSeverity.Error:
                        MessageBox.Show($"Error ({Err.Id}; {Err.Name}): {Err.Description}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    case MessageSeverity.FatalError:

                        // Temporary - we don't have a clean shutdown method yet
                        MessageBox.Show($"Guru Meditation {Err.Id}\n\n{Err.Name}: {Err.Description}\n\nLightning must exit. Sorry!\nYou may wish to file a bug report with the game's developers.", "Lightning Game Engine", MessageBoxButton.OK, MessageBoxImage.Error);
                        // Temporary Code (yeah this is dumb)
                        Environment.Exit(0xDEAD * (int)Err.Id);

                        return;

                }
            }
        }

        public static GenericResult SerialiseErrors(string Path)
        {
            GenericResult GR = new GenericResult();
            try
            {


                XmlReader XR = XmlReader.Create(Path);

                XmlSerializer XS = new XmlSerializer(typeof(ErrorCollection));
                Errors = (ErrorCollection)XS.Deserialize(XR);
            }
            catch (InvalidOperationException err)
            {
                string ErrorString = $"Error serialising error: {err}";
                ThrowError(ErrorString, new Error { Name = "ErrorSerialisingErrorXmlException", Description = "", Id = 0x4444DEAD, Severity = MessageSeverity.FatalError });
                // prevent compile error

                GR.FailureReason = ErrorString;
                return GR;
            }

        }

        public static void SerialiseErrors_Validate()
        {

        }
    }
}
