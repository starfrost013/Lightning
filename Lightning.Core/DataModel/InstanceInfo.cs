using System;
using System.Collections.Generic;
using System.Reflection; 
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// InstanceInfo class.
    /// 
    /// Holds information about an instance - its methods, properties, and their metadata. Used for the IDE. ReflectionMetadata?
    /// 
    /// Converted from System.Reflection types at boot.
    /// 
    /// Translated to and from .NET System.Reflection types as required for the IDE and the datamodel serialiser. 
    /// </summary>
    public class InstanceInfo : Instance
    {
        public override InstanceTags Attributes => InstanceTags.Instantiable;
        public List<InstanceInfoMethod> Methods { get; set; }
        public List<InstanceInfoProperty> Properties { get; set; }

        /// <summary>
        /// Get InstanceInfo from a type.
        /// 
        /// fucking compiler stifling my generic type parameters again!
        /// </summary>
        /// <typeparam name="T">The DataModel-conformant type you wish to get the InstanceInfo from.</typeparam>
        /// <returns></returns>
        public static InstanceInfoResult FromType(Type TType) 
        {
            InstanceInfoResult IIR = new InstanceInfoResult();

            if (!TType.IsAssignableFrom(typeof(Instance)))
            {
                // Successful is true by default
                IIR.FailureReason = "Type is not DataModel compliant!";
                return IIR;
            }
            else
            {
                MemberInfo[] MI = TType.GetMembers();
                List<PropertyInfo> PIList = new List<PropertyInfo>();
                List<MethodInfo> MIList = new List<MethodInfo>();

                foreach (MemberInfo MemberInformation in MI)
                {
                    switch (MemberInformation.MemberType)
                    {
                        case MemberTypes.Property:
                            // Get the name of the current member.
                            PropertyInfo CurrentPI = TType.GetProperty(MemberInformation.Name);

                            PIList.Add(CurrentPI);

                            Type PropertyType = CurrentPI.PropertyType;

                            // This code allows for things like color3.
                            // we check for properties that have their own members and if so, iterate through those
                            // 2021-03-11
                            MemberInfo[] PIInfo = PropertyType.GetMembers();

                            if (PIInfo.Length > 0)
                            {
                                //todo: cache type ifnormation
                                if (PropertyType.IsAssignableFrom(typeof(Instance)))
                                {
                                    FromType(PropertyType);
                                }
                                else 
                                {
                                    continue; 
                                }
                            }
                            else
                            {
                                continue;
                            }


                            continue;
                        case MemberTypes.Method:
                            MethodInfo CurrentMI = TType.GetMethod(MemberInformation.Name);

                            MIList.Add(CurrentMI);
                            continue;
                    }
                }
            
                // Process each method and add its parameters
                foreach (MethodInfo CurMethod in MIList)
                {
                    InstanceInfoMethod IIM = (InstanceInfoMethod)DataModel.CreateInstance(typeof(InstanceInfoMethod).Name);

                    IIM.MethodName = CurMethod.Name;

                    ParameterInfo[] PIPList = CurMethod.GetParameters();

                    foreach (ParameterInfo PI in PIPList)
                    {
                        InstanceInfoMethodParameter IIMP = (InstanceInfoMethodParameter)DataModel.CreateInstance(typeof(InstanceInfoMethodParameter).Name);
                        IIMP.ParamName = PI.Name;
                        IIMP.ParamType = PI.ParameterType;

                        IIM.Parameters.Add(IIMP);
                    }

                    IIR.InstanceInformation.Methods.Add(IIM);
                }

                foreach (PropertyInfo CurProperty in PIList)
                {
                    InstanceInfoProperty IIP = new InstanceInfoProperty();

                    IIP.Name = CurProperty.Name;
                    IIP.Type = CurProperty.PropertyType;
                    //todo: set property type

                    //IIP.Security = CurProperty.
                }

                IIR.Successful = true;
                return IIR; 
            }
            
        } 

        public InstanceInfo()
        {
            Methods = new List<InstanceInfoMethod>();
            Properties = new List<InstanceInfoProperty>(); 
        }
    }
}
