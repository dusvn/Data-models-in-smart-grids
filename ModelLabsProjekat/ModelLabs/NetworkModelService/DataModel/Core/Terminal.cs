using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Terminal : IdentifiedObject
    {
        private long conductingEquipment;
        public Terminal(long globalId) : base(globalId)
        {
        }

        public long ConductingEquipment
        {
            get { return conductingEquipment; }
            set { conductingEquipment = value; }
        }

        public override bool Equals(object x)
        {
            if (base.Equals(x))
            {
                Terminal t = (Terminal)x;
                return (t.conductingEquipment == this.conductingEquipment);
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.TERMINAL_CONDUCTINGEQUIPMENT:
                    property.SetValue(conductingEquipment);
                    break;
                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.TERMINAL_CONDUCTINGEQUIPMENT: return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.TERMINAL_CONDUCTINGEQUIPMENT:
                    conductingEquipment = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (conductingEquipment != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_CONDUCTINGEQUIPMENT] = new List<long>();
                references[ModelCode.TERMINAL_CONDUCTINGEQUIPMENT].Add(conductingEquipment);
            }
            base.GetReferences(references, refType);
        }
    }
}
