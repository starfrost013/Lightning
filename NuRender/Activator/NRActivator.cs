using NuCore.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NuRender
{
    /// <summary>
    /// NRActivator
    /// 
    /// September 13, 2021 (modified September 17, 2021)
    /// 
    /// Defines the NuRender object activator. 
    /// </summary>
    public static class NRActivator
    {
        internal static string ClassName => "NRActivator";

        public static object NRActivate(string CName)
        {
            try
            {
                Type CurType = Type.GetType($"{NuRender.NURENDER_NAMESPACE_PATH}.{CName}"); // NuRender. - TEMP

                if (CurType == null)
                {
                    ErrorManager.ThrowError(ClassName, "NRCannotCreateObjectWithInvalidTypeException", $"Cannot create NRObject of type {CName}, as it does not exist!");
                    return null; // objectcreateresult? 
                }
                else
                {
                    return NRActivate(CurType);
                }
            }
            catch (Exception)
            {
                return null; // already handled by the other NRActivate
            }
        }

        public static object NRActivate(Type TypeName)
        {
            try
            {
                object Instance = Activator.CreateInstance(TypeName);

                return Instance;

            }
            catch (TypeLoadException ex)
            {
#if DEBUG
                ErrorManager.ThrowError(ClassName, "NRCannotCreateNonNRObjectException", $"Cannot create NRObject of type {ClassName}, as it is not within the NuRender assembly!\n\n{ex}");
#else
                ErrorManager.ThrowError(ClassName, "NRCannotCreateNonNRObjectException", $"Cannot create NRObject of type {ClassName}, as it is not within the NuRender assembly!");
#endif
                return null; // objectcreateresult? 
            }
            catch (Exception ex)
            {
#if DEBUG
                ErrorManager.ThrowError(ClassName, "NRUnknownErrorCreatingNRObjectException", $"Unknown error creating NRObject of type {ClassName}!\n\n{ex}");
#else
                ErrorManager.ThrowError(ClassName, "NRUnknownErrorCreatingNRObjectException", $"Unknown error creating NRObject of type {ClassName}!");
#endif
                return null; // objectcreateresult? 
            }
        }
    }
}