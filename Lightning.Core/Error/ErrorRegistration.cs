using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{

 /// <summary>
 /// ErrorRegistration
 ///
 /// Generated by Lightning.Tools.ErrorConvert - July 3, 2021 19:34:00
 ///
 /// Registers errors for use with the new (internally based) Error Manager
 /// </summary>
 public static class ErrorRegistration
 {
     public static void RegisterErrors()
     {
         ErrorManager.RegisterError( new Error { Name = "TestErrorException", Description = "Test Error from XML", Severity = MessageSeverity.FatalError, Id = 0 } );
         ErrorManager.RegisterError( new Error { Name = "AttemptedToAcquireInvalidInstanceException", Description = "InstanceCollection: Attempted to acquire invalid Instance!", Severity = MessageSeverity.FatalError, Id = 1 } );
         ErrorManager.RegisterError( new Error { Name = "InvalidVersionException", Description = "Versioning: Acquired invalid version - must have 4 components (delimiter is .)", Severity = MessageSeverity.Error, Id = 2 } );
         ErrorManager.RegisterError( new Error { Name = "InvalidVersionAuxillaryInformationException", Description = "Versioning: Acquired invalid version - invalid owner or build date!", Severity = MessageSeverity.Error, Id = 3 } );
         ErrorManager.RegisterError( new Error { Name = "InvalidVersionInformationException", Description = "Versioning: Acquired invalid version - major, minor, revision, or private/compressed build date", Severity = MessageSeverity.Error, Id = 4 } );
         ErrorManager.RegisterError( new Error { Name = "AttemptedToAddNonErrorToErrorsException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 5 } );
         ErrorManager.RegisterError( new Error { Name = "AttemptedToAddInvalidEnumNameException", Description = "EnumValue: Attempted to add EnumValue with zero-length or null to EnumInstance!", Severity = MessageSeverity.FatalError, Id = 6 } );
         ErrorManager.RegisterError( new Error { Name = "DataModelInstanceCreationFailedException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 7 } );
         ErrorManager.RegisterError( new Error { Name = "DataModelInstanceCreationUnknownErrorException", Description = "An unknown exception occurred during the creation of an Instance and its addition to the DataModel.", Severity = MessageSeverity.Error, Id = 8 } );
         ErrorManager.RegisterError( new Error { Name = "InstanceInfoGenerationFailedException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 9 } );
         ErrorManager.RegisterError( new Error { Name = "InstanceCannotBeParentException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 10 } );
         ErrorManager.RegisterError( new Error { Name = "CannotAcquireNullParentException", Description = "Attempted to acquire a null parent for an Instance that does not have InstanceTags.CanHaveParentNull!", Severity = MessageSeverity.Error, Id = 11 } );
         ErrorManager.RegisterError( new Error { Name = "DDMSSchemaValidationWarningException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Warning, Id = 12 } );
         ErrorManager.RegisterError( new Error { Name = "DDMSSchemaValidationErrorException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 13 } );
         ErrorManager.RegisterError( new Error { Name = "DDMSMetadataValidationError", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 14 } );
         ErrorManager.RegisterError( new Error { Name = "DDMSDataModelSerialisationErrorException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 15 } );
         ErrorManager.RegisterError( new Error { Name = "GlobalSettingsValidationFailureException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.FatalError, Id = 16 } );
         ErrorManager.RegisterError( new Error { Name = "GlobalSettingsValidationWarningException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Warning, Id = 17 } );
         ErrorManager.RegisterError( new Error { Name = "CannotAddThatInstanceAsChildException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 18 } );
         ErrorManager.RegisterError( new Error { Name = "FailedToSerialiseGlobalSettingsException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.FatalError, Id = 19 } );
         ErrorManager.RegisterError( new Error { Name = "ThatIsNotAnInstancePleaseDoNotTryToAddItToTheDataModelException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 20 } );
         ErrorManager.RegisterError( new Error { Name = "TheWorkspaceHasBeenDestroyedException", Description = "The Workspace has been destroyed and is no longer in the DataModel.", Severity = MessageSeverity.FatalError, Id = 21 } );
         ErrorManager.RegisterError( new Error { Name = "AttemptedToAddNonServiceStartupCommandToServiceStartupCommandCollectionException", Description = "Attempted to add an object that is not a ServiceStartupCommand to a ServiceStartupCommandCollection", Severity = MessageSeverity.Error, Id = 22 } );
         ErrorManager.RegisterError( new Error { Name = "InternalSerialisationErrorException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.FatalError, Id = 23 } );
         ErrorManager.RegisterError( new Error { Name = "CannotFindLgxFileException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 24 } );
         ErrorManager.RegisterError( new Error { Name = "CannotAcquireUnloadedGlobalSettingsException", Description = "Cannot acquire GlobalSettings when it is not yet loaded!", Severity = MessageSeverity.Error, Id = 25 } );
         ErrorManager.RegisterError( new Error { Name = "GameSettingsFailedToLoadException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.FatalError, Id = 26 } );
         ErrorManager.RegisterError( new Error { Name = "FailedToObtainGameSettingException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 27 } );
         ErrorManager.RegisterError( new Error { Name = "FailedToObtainCriticalGameSettingException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.FatalError, Id = 28 } );
         ErrorManager.RegisterError( new Error { Name = "CannotDestroyNonDestroyableInstanceException", Description = "!!this should be overidden in code!!", Severity = MessageSeverity.Error, Id = 29 } );
         ErrorManager.RegisterError( new Error { Name = "ThatIsNotAnInstancePleaseDoNotTryToRemoveItFromTheDataModelException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 30 } );
         ErrorManager.RegisterError( new Error { Name = "AttemptedToRemoveInstanceThatIsNotPartOfDataModelException", Description = "Attempted to remove an Instance that is not in the DataModel or Workspace.", Severity = MessageSeverity.Error, Id = 31 } );
         ErrorManager.RegisterError( new Error { Name = "AttemptedToRemoveInstanceThatIsNotAChildOfItsParentException", Description = "Attempted to remove an Instance from a Parent that it is not a child of.", Severity = MessageSeverity.Error, Id = 32 } );
         ErrorManager.RegisterError( new Error { Name = "ServiceControlManagerFailureException", Description = "The Service Control Manager has failed.", Severity = MessageSeverity.FatalError, Id = 33 } );
         ErrorManager.RegisterError( new Error { Name = "RenderServiceInitialisationFailedException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 34 } );
         ErrorManager.RegisterError( new Error { Name = "CriticalServiceFailureException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.FatalError, Id = 35 } );
         ErrorManager.RegisterError( new Error { Name = "AttemptedToPassInvalidServiceNotificationException", Description = "Attempted to pass an invalid ServiceNotification to the Service Control Manager.", Severity = MessageSeverity.Error, Id = 36 } );
         ErrorManager.RegisterError( new Error { Name = "AttemptedToHandleServiceNotificationAboutANonServiceException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 37 } );
         ErrorManager.RegisterError( new Error { Name = "CannotFindXmlFileOrSchemaException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 38 } );
         ErrorManager.RegisterError( new Error { Name = "ErrorValidatingLGXFileException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 39 } );
         ErrorManager.RegisterError( new Error { Name = "Vector2ConversionInvalidNumberOfComponentsException", Description = "Cannot convert a string without a comma or with more than one comma to a Vector2!", Severity = MessageSeverity.Error, Id = 40 } );
         ErrorManager.RegisterError( new Error { Name = "Vector2InvalidConversionException", Description = "Attempted to convert an invalid string or an object that is not a string! to a Vector2!", Severity = MessageSeverity.Error, Id = 41 } );
         ErrorManager.RegisterError( new Error { Name = "AttemptedToRenderInvalidTextureException", Description = "Attempted to render an invalid texture!", Severity = MessageSeverity.Error, Id = 42 } );
         ErrorManager.RegisterError( new Error { Name = "CannotLoadNonexistentTextureException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 43 } );
         ErrorManager.RegisterError( new Error { Name = "ErrorLoadingTextureException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 44 } );
         ErrorManager.RegisterError( new Error { Name = "ErrorLoadingSDL2DllException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 45 } );
         ErrorManager.RegisterError( new Error { Name = "ErrorConvertingRelativeColourException", Description = "Attempted to convert an invalid length relative colour string to a Color3!", Severity = MessageSeverity.Error, Id = 46 } );
         ErrorManager.RegisterError( new Error { Name = "ErrorConvertingRelativeColourFormatException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 47 } );
         ErrorManager.RegisterError( new Error { Name = "ErrorConvertingCommaColourException", Description = "Attempted to convert an invalid length colour string to a Color3!", Severity = MessageSeverity.Error, Id = 48 } );
         ErrorManager.RegisterError( new Error { Name = "ErrorConvertingCommaColourFormatException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 49 } );
         ErrorManager.RegisterError( new Error { Name = "FailedToOpenLgxException", Description = "Failed to open the specified game. Currently this is just an error message - in the future we will have some nice UI.", Severity = MessageSeverity.Error, Id = 50 } );
         ErrorManager.RegisterError( new Error { Name = "FailedToSaveLgxException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 51 } );
         ErrorManager.RegisterError( new Error { Name = "InvalidDataModelCannotSaveLgxException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 52 } );
         ErrorManager.RegisterError( new Error { Name = "AttemptedToAcquireInvalidPropertyInfoException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 53 } );
         ErrorManager.RegisterError( new Error { Name = "AttemptedToAcquireInvalidCameraTargetException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 54 } );
         ErrorManager.RegisterError( new Error { Name = "TokenisationCannotConvertTypeInternalException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.FatalError, Id = 55 } );
         ErrorManager.RegisterError( new Error { Name = "ErrorExposingMethodToScriptingException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 56 } );
         ErrorManager.RegisterError( new Error { Name = "CannotRunScriptWithInvalidNameException", Description = "Cannot run a script with an invalid name!", Severity = MessageSeverity.Error, Id = 57 } );
         ErrorManager.RegisterError( new Error { Name = "ErrorLoadingOrTokenisingScriptException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Warning, Id = 58 } );
         ErrorManager.RegisterError( new Error { Name = "ErrorLoadingSoundChunkException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 59 } );
         ErrorManager.RegisterError( new Error { Name = "ErrorAcquiringSoundListException", Description = "An error occurred obtaining a list of sounds to play!", Severity = MessageSeverity.Error, Id = 60 } );
         ErrorManager.RegisterError( new Error { Name = "Err3DSoundRequiresSoundPositionAndRadiusException", Description = "To use 3D sound, a Sound must have a Position and Radius!", Severity = MessageSeverity.Error, Id = 61 } );
         ErrorManager.RegisterError( new Error { Name = "Err3DSoundRequiresTargetObjectException", Description = "To use 3D sound, a Sound must have a target object!", Severity = MessageSeverity.Error, Id = 62 } );
         ErrorManager.RegisterError( new Error { Name = "Err3DSoundTargetObjectDoesNotExistException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 63 } );
         ErrorManager.RegisterError( new Error { Name = "ErrorObtainingListOfPhysicalObjectsToRenderException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 64 } );
         ErrorManager.RegisterError( new Error { Name = "PolarisTabCollectionEnumeratorOverflowException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.FatalError, Id = 65 } );
         ErrorManager.RegisterError( new Error { Name = "OverrideAppIdSpecifiedWithNoAppId", Description = "--overrideappid was specified without an appname to override, ignoring", Severity = MessageSeverity.Warning, Id = 66 } );
         ErrorManager.RegisterError( new Error { Name = "GameXmlFileRequiredException", Description = "Please provide a Game XML file to load!", Severity = MessageSeverity.FatalError, Id = 67 } );
         ErrorManager.RegisterError( new Error { Name = "PolarisNoDataModelLoadedException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.FatalError, Id = 68 } );
         ErrorManager.RegisterError( new Error { Name = "PolarisCannotAddNonTabToTabCollectionException", Description = "Cannot add a non-Tab to a TabCollection!", Severity = MessageSeverity.FatalError, Id = 69 } );
         ErrorManager.RegisterError( new Error { Name = "PolarisCannotPopulateTabUIWithNoTabsException", Description = "Attempted to populate TabUI when Tabs have not been loaded!", Severity = MessageSeverity.FatalError, Id = 70 } );
         ErrorManager.RegisterError( new Error { Name = "PolarisCannotLoadNonexistentUserControlForTabUseException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 71 } );
         ErrorManager.RegisterError( new Error { Name = "PolarisCannotLoadNonUserControlForTabUseException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 72 } );
         ErrorManager.RegisterError( new Error { Name = "PolarisCannotAddNullMessageToPolarisOutputTab", Description = "Cannot add a null message to the Polaris Output tab!", Severity = MessageSeverity.Error, Id = 73 } );
         ErrorManager.RegisterError( new Error { Name = "InstanceInfoErrorRemovingAmbiguousMatchesException", Description = "An error occurred removing ambiguous matches for generation of instance information.", Severity = MessageSeverity.Error, Id = 75 } );
         ErrorManager.RegisterError( new Error { Name = "ErrorOccurredAcquiringPhysicalObjectListException", Description = "An error occurred acquiring the list of physical objects to render.", Severity = MessageSeverity.FatalError, Id = 76 } );
         ErrorManager.RegisterError( new Error { Name = "SkyObjectMustHaveTextureException", Description = "A Sky object must have a Texture!", Severity = MessageSeverity.Error, Id = 77 } );
         ErrorManager.RegisterError( new Error { Name = "TokenCollectionIndexOutOfRangeException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.FatalError, Id = 78 } );
         ErrorManager.RegisterError( new Error { Name = "CannotAddNonTokenToTokenCollectionException", Description = "Cannot add a non-Token to a TokenCollection!", Severity = MessageSeverity.Error, Id = 79 } );
         ErrorManager.RegisterError( new Error { Name = "CannotAddTokenWithParentThatIsNotInTokenCollectionException", Description = "Cannot add a Token with a Parent that is not in the TokenCollection to a TokenCollection!", Severity = MessageSeverity.Error, Id = 80 } );
         ErrorManager.RegisterError( new Error { Name = "OldTokeniserRemovedException", Description = "The old Tokeniser has been removed as of June 1, 2021. Please use the AST Tokeniser instead!", Severity = MessageSeverity.Error, Id = 81 } );
         ErrorManager.RegisterError( new Error { Name = "CannotAddNonexistentInstanceToInstanceCollectionException", Description = "Cannot add an Instance to a Parent that is not in the InstanceCollection you are attempting to add it to!", Severity = MessageSeverity.Error, Id = 82 } );
         ErrorManager.RegisterError( new Error { Name = "AstPatternMustHaveAtLeast3Elements", Description = "An ASTPattern must have at least three elements - cannot access its index 2 or below!", Severity = MessageSeverity.Error, Id = 83 } );
         ErrorManager.RegisterError( new Error { Name = "SyntaxErrorException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 84 } );
         ErrorManager.RegisterError( new Error { Name = "ErrorAcquiredInvalidLineException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.FatalError, Id = 85 } );
         ErrorManager.RegisterError( new Error { Name = "LuaScriptCrashedException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 86 } );
         ErrorManager.RegisterError( new Error { Name = "LuaStateFailureException", Description = "Lua state invalid or trying to parse a script before ScriptingService has started.", Severity = MessageSeverity.Error, Id = 87 } );
         ErrorManager.RegisterError( new Error { Name = "LuaCannotObtainListOfScriptsToRunException", Description = "Cannot obtain a list of Lua scripts to run!", Severity = MessageSeverity.FatalError, Id = 88 } );
         ErrorManager.RegisterError( new Error { Name = "ReinitialisingBeforeInitialisingDataModelException", Description = "Attempted to reinitialise the DataModel before it has been initialised!", Severity = MessageSeverity.FatalError, Id = 89 } );
         ErrorManager.RegisterError( new Error { Name = "CannotHaveMultipleUnpausedScriptsRunningException", Description = "Multiple scripts are running while not paused! That is not allowed - stopping all except the first...", Severity = MessageSeverity.FatalError, Id = 90 } );
         ErrorManager.RegisterError( new Error { Name = "InternalLuaStateErrorException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.FatalError, Id = 91 } );
         ErrorManager.RegisterError( new Error { Name = "AttemptedToRunLuaScriptWithNoContentException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 92 } );
         ErrorManager.RegisterError( new Error { Name = "AttemptedToRunLuaCoreScriptWithNoProtectedContentExceptionn", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 93 } );
         ErrorManager.RegisterError( new Error { Name = "InternalInstanceAdditionErrorException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 94 } );
         ErrorManager.RegisterError( new Error { Name = "AttemptedToAddInvalidFunctionToSandboxException", Description = "Attempted to add an invalid function to the Lua sandbox!", Severity = MessageSeverity.FatalError, Id = 95 } );
         ErrorManager.RegisterError( new Error { Name = "AttemptedToConvertNonStringToStringListException", Description = "Attempted to convert a non-String to a list of strings.", Severity = MessageSeverity.Error, Id = 96 } );
         ErrorManager.RegisterError( new Error { Name = "AttemptedToConvertInvalidStringToStringListException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 97 } );
         ErrorManager.RegisterError( new Error { Name = "AttemptedToAddANonTextChunkToATextChunkCollection", Description = "Attempted to add a non-TextChunk to a TextChunkCollection!", Severity = MessageSeverity.Error, Id = 98 } );
         ErrorManager.RegisterError( new Error { Name = "AttemptedToAddATextChunkWithNoLengthToATextChunkCollection", Description = "Attempted to add a TextChunk with no length to a TextChunkCollection!", Severity = MessageSeverity.Error, Id = 99 } );
         ErrorManager.RegisterError( new Error { Name = "AttemptedToPassInvalidW32PointToVector2FromNativePointException", Description = "[WINDOWS ONLY] Attempted to pass a null or invalid Win32Point to Vector2.FromNativePoint(); -- position will be set to (0,0)!", Severity = MessageSeverity.Error, Id = 100 } );
         ErrorManager.RegisterError( new Error { Name = "FailedToObtainListOfGuiRootsException", Description = "Failed to obtain list of GuiRoots - UIService will now crash!", Severity = MessageSeverity.Error, Id = 101 } );
         ErrorManager.RegisterError( new Error { Name = "FailedToInitialiseSDL2TtfException", Description = "Failed to initialise SDL2_ttf!", Severity = MessageSeverity.Error, Id = 102 } );
         ErrorManager.RegisterError( new Error { Name = "NullOrZeroLengthFileFontNameException", Description = "Failed to load font: Null or zero-length font file name specified! - font will not be loaded.", Severity = MessageSeverity.Error, Id = 103 } );
         ErrorManager.RegisterError( new Error { Name = "InvalidFontSizeException", Description = "Failed to load font: Invalid font size specified! - font will not be loaded.", Severity = MessageSeverity.Error, Id = 104 } );
         ErrorManager.RegisterError( new Error { Name = "FailedToLoadFontException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 105 } );
         ErrorManager.RegisterError( new Error { Name = "FailedToObtainListOfFontsException", Description = "Failed to obtain list of fonts!", Severity = MessageSeverity.FatalError, Id = 106 } );
         ErrorManager.RegisterError( new Error { Name = "NoFontsInstalledCannotUseUIException", Description = "There are no fonts installed - cannot use UI!", Severity = MessageSeverity.Error, Id = 107 } );
         ErrorManager.RegisterError( new Error { Name = "FailedToRenderTextException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 108 } );
         ErrorManager.RegisterError( new Error { Name = "FontMustDeclareNameException", Description = "Fonts must declare a name: This font will not be loaded. This will probably cause errors later down the line. Oh well", Severity = MessageSeverity.Warning, Id = 109 } );
         ErrorManager.RegisterError( new Error { Name = "RelativeColourOutOfRangeException", Description = "Attempted to load a RelativeColour where one or more of the components was outside of the range [0,1]!", Severity = MessageSeverity.Error, Id = 110 } );
         ErrorManager.RegisterError( new Error { Name = "Err3DSoundTargetObjectMustInheritFromOrBeAPhysicalObjectException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 111 } );
         ErrorManager.RegisterError( new Error { Name = "ErrorConvertingHexadecimalColourException", Description = "Attempted to convert an invalid length hexadecimal colour string to a Color3!", Severity = MessageSeverity.Error, Id = 112 } );
         ErrorManager.RegisterError( new Error { Name = "ErrorConvertingHexadecimalColourFormatException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 113 } );
         ErrorManager.RegisterError( new Error { Name = "FailedToObtainListOfGuiElementsException", Description = "Failed to get a list of GUI elements", Severity = MessageSeverity.Error, Id = 114 });
         ErrorManager.RegisterError( new Error { Name = "FailedToObtainSurfaceGuiTargetObjectException", Description = "Failed to obtain the target for a SurfaceGui! This SurfaceGui will not be rendered.", Severity = MessageSeverity.Error, Id = 115 });
         ErrorManager.RegisterError( new Error { Name = "MustDefineFontForGuiElementException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 115 });
         ErrorManager.RegisterError( new Error { Name = "FailedToAcquireListOfPhysicsControllersException", Description = "Failed to obtain a list of physics controllers!", Severity = MessageSeverity.FatalError, Id = 116 }); // possibly nonfatal?  
         ErrorManager.RegisterError( new Error { Name = "EnablePhysicsSetButNoPhysicsControllerSet", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 117 });
         ErrorManager.RegisterError( new Error { Name = "FailedToAcquireListOfControllableObjectsException", Description = "Failed to obtain a list of controllable objects!", Severity = MessageSeverity.FatalError, Id = 118 });
         ErrorManager.RegisterError( new Error { Name = "FailedToObtainPhysicsControllerException", Description = "Failed to obtain physics controller", Severity = MessageSeverity.Error, Id = 119 });
         ErrorManager.RegisterError( new Error { Name = "AttemptedToApplyInvalidImpulseException", Description = "Attempted to apply an invalid impulse to a PhysicalObject!", Severity = MessageSeverity.Error, Id = 120 });
         ErrorManager.RegisterError( new Error { Name = "PhysicsStatePositionalCorrectionPercentageMustBeAboveZeroException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Warning, Id = 121 });
         ErrorManager.RegisterError( new Error { Name = "PhysicsStatePositionalCorrectionSlopMustBeAboveZeroException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Warning, Id = 122 });
         ErrorManager.RegisterError( new Error { Name = "AttemptedToSetInvalidPropertyException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Warning, Id = 123 });
         ErrorManager.RegisterError( new Error { Name = "FailedToObtainListOfGradientStopsException", Description = "Failed to obtain list of GradientStops!", Severity = MessageSeverity.FatalError, Id = 124 }); // nonfatal?
         ErrorManager.RegisterError( new Error { Name = "GradientMustHaveAboveZeroStopsException", Description = "A Gradient must have above zero stops!", Severity = MessageSeverity.Error, Id = 125 });
         ErrorManager.RegisterError( new Error { Name = "InvalidGradientException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 126 });
         ErrorManager.RegisterError( new Error { Name = "BrushMustHavePhysicalObjectParentException", Description = "Brushes require a PhysicalObject as their parent!", Severity = MessageSeverity.Error, Id = 127 });
         ErrorManager.RegisterError( new Error { Name = "GradientParentMustHavePositionException", Description = "Gradients require that their parent has a set position and size!", Severity = MessageSeverity.Error, Id = 127 });
         ErrorManager.RegisterError( new Error { Name = "ErrorTryingToGetBrushException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 128 });
         ErrorManager.RegisterError( new Error { Name = "BrushMustHaveDefinedSizeException", Description = "Brushes must have a defined size! This object will not be rendered.", Severity = MessageSeverity.Error, Id = 129 });
         ErrorManager.RegisterError( new Error { Name = "InvalidTextureLoadMessageException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 130 });
         ErrorManager.RegisterError( new Error { Name = "ServiceMessageMustHaveNameException", Description = "ServiceMessages must have a name! This ServiceMessage will not be delivered.", Severity = MessageSeverity.Error, Id = 131 }); // warning?
         ErrorManager.RegisterError( new Error { Name = "PhysicsStateEpsilonVelocityPercentageMustBeAboveZeroException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Warning, Id = 132 });
         ErrorManager.RegisterError( new Error { Name = "AttemptedToAcquireInvalidAnimationFrameException", Description = "!!this should be overridden in code!!", Severity = MessageSeverity.Error, Id = 133 });
         ErrorManager.RegisterError( new Error { Name = "FailedToAcquireListOfAnimationsException", Description = "Failed to acquire a list of animations!", Severity = MessageSeverity.FatalError, Id = 134 });
         ErrorManager.RegisterError( new Error { Name = "FailedToAcquireListOfAnimationFramesException", Description = "Failed to acquire a list of animation frames!", Severity = MessageSeverity.FatalError, Id = 135 }); ;
         ErrorManager.RegisterError( new Error { Name = "AnimationFrameMustBeChildOfChildOfAnimatedImageBrushException", Description = "AnimationFrames must be a child of an Animation which is a part of an AnimatedImageBrush! This AnimationFrame will be destroyed.", Severity = MessageSeverity.Warning, Id = 136 });
         ErrorManager.RegisterError( new Error { Name = "DisplayViewportMustBeLargerThanSizeForTilingException", Description = "To display an ImageBrush or AnimationFrame with TextureDisplayMode set to Tile, its DisplayViewport must be set and be set to a larger value than its Size value.", Severity = MessageSeverity.Warning, Id = 137 });
     }
  }
}
