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
            //pre-globalsettings
            SerialiseErrors(@"Content\EngineContent\Errors.xml");
#if DEBUG
            ATest_CheckErrorSerialisedCorrectly(); 
#endif
        }

        private static bool DoesErrorExist(Error Err) => Errors.ErrorList.Contains(Err);
#if DEBUG
        private static void ATest_CheckErrorSerialisedCorrectly()
        {
            foreach (Error Err in Errors.ErrorList)
            {
                string CompName = "Error Manager";

                Logging.Log("Error: ", CompName);
                Logging.Log($"Name: {Err.Name}", CompName);
                Logging.Log($"Description: {Err.Description}", CompName);
                Logging.Log($"Id: {Err.Id}", CompName);
                Logging.Log($"Severity: {Err.Severity}", CompName);
            }
        }
#endif
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

        /// <summary>
        /// Throws the error <paramref name="ErrorName"/>, logging component <paramref name="Component"/>
        /// </summary>
        /// <param name="Component">The component</param>
        /// <param name="ErrorName"></param>
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

        public static void ThrowError(string Component, string ErrorName, string ErrorDescription)
        {
            GetErrorResult ErrToThrow = GetError(ErrorName);
            

            if (ErrToThrow.Successful)
            {
                if (ErrorDescription == null)
                {
                    ThrowError("Error Handler - InnerException", new Error { Id = 0x2222BABE, Severity = MessageSeverity.FatalError, Name = "CannotOverrideNonexistentErrorException", Description = "Cannot override an error's description with null!" });
                }
                else
                {
                    ErrToThrow.Error.Description = ErrorDescription;
                    ThrowError(Component, ErrToThrow.Error);
                }

               
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
                ThrowError("Error Handler - InnerException", new Error { Id = 0x9999BABE, Severity = MessageSeverity.FatalError, Name = "AttemptedToThrowInvalidErrorException", Description = $"Internal error: Attempted to throw nonexistent error ID {ErrorId}" });
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

        private static GenericResult SerialiseErrors(string Path)
        {
            GenericResult GR = new GenericResult();

            XmlSchemaResult XSR = SerialiseErrors_Validate(Path);
            
            if (!XSR.Successful)
            {
                GR.FailureReason = $"Failed to validate Error XML (Schema-based validation failure): {XSR.FailureReason}";
                return GR;
            }
            else
            {
                ErrorSerialisationResult GR2 = SerialiseErrors_Serialise(Path);

                if (!GR2.Successful)
                {
                    GR.FailureReason = $"Error serialising error XML!: {GR.FailureReason}";
                    return GR;
                }
                else
                {
                    // successful
                    Errors = GR2.ErrorCollection;
                    GR.Successful = true;
                    return GR;
                }
            }



        }

        private static XmlSchemaResult SerialiseErrors_Validate(string Path)
        {
            LightningXMLSchema LXMLS = new LightningXMLSchema();

            // todo: engineglobalsettings
            LXMLS.XSI.SchemaPath = @"Content\Schema\Errors.xsd";
            LXMLS.XSI.XmlPath = Path;

            return LXMLS.Validate();
        }

        
        private static ErrorSerialisationResult SerialiseErrors_Serialise(string Path)
        {
            ErrorSerialisationResult GR = new ErrorSerialisationResult();

            try
            {


                XmlReader XR = XmlReader.Create(Path);

                XmlSerializer XS = new XmlSerializer(typeof(ErrorCollection));
                GR.ErrorCollection = (ErrorCollection)XS.Deserialize(XR);

                GR.Successful = true;
                return GR; 
            }
            catch (InvalidOperationException err)
            {
                // Throw an error
                string ErrorString = $"Error serialising error: {err}";
                ThrowError(ErrorString, new Error { Name = "ErrorSerialisingErrorXmlException", Description = ErrorString, Id = 0x4444DEAD, Severity = MessageSeverity.FatalError, BaseException = err });
                // prevent compile error

                // Successful is false by default
                GR.FailureReason = ErrorString;
                return GR;
            }
        }

        
    }
}
