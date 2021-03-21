using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    /// <summary>
    /// Controls services. 2021-03-10
    /// </summary>
    public class ServiceControlManager : Instance
    {
        public override string ClassName => "ServiceControlManager";
        public List<Service> Services { get; set; }

        public ServiceControlManager()
        {
            Services = new List<Service>();
        }
        
        public ServiceStartResult StartService(Type TypeOfService)
        {
            try
            {
                object ObjX = DataModel.CreateInstance(TypeOfService.Name);

                Service Svc = (Service)ObjX;

                return Svc.OnStart();
            }
            catch (ArgumentException err)
            {
                ServiceStartResult SvcSR = new ServiceStartResult();
#if DEBUG
                SvcSR.Information = $"Attempted to instantiate an invalid service\n\n{err}";
#else
                SvcSR.Information = "Attempted to instantiate an invalid service";
#endif

                return SvcSR; 
            }

        }
    }
}
