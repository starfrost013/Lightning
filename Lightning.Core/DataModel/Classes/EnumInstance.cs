using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core
{
    public class EnumInstance : SerialisableObject
    {
        public override InstanceTags Attributes => InstanceTags.Instantiable;
        public override string ClassName => "EnumInstance";
        public List<EnumValue> Values { get; set; }

        public EnumInstance()
        {
            Values = new List<EnumValue>();
        }

        public void AddValue(EnumValue Val) => Values.Add(Val);

        public void AddValue(string Res)
        {
            if (Res == null || Res.Length == 0)
            {
                ErrorManager.ThrowError(ClassName, "AttemptedToAddInvalidEnumNameException");
                return;
            }
            else
            {
                EnumValue EV = (EnumValue)DataModel.CreateInstance(typeof(EnumValue).Name);

                EV.Name = Res;
                Values.Add(EV);
            }

        }

        public void AddValue(string Res, int Id)
        {
            if (Res == null || Res.Length == 0
                || Id <= 0)
            {

                ErrorManager.ThrowError(ClassName, "AttemptedToAddInvalidEnumNameException");
                return;

            }
            else
            {
                EnumValue EV = (EnumValue)DataModel.CreateInstance(typeof(EnumValue).Name);

                EV.Id = Id;
                EV.Name = Res;


                Values.Add(EV);
            }

        }
    }
}
