using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Control
{
    public class Control : IdentifiedObject
    {
        private long regulatingConducingEquipment;
        public Control(long globalId) : base(globalId)
        {
        }

        public long RegulatingConductingEquipment
        {
            get { return regulatingConducingEquipment; }
            set { regulatingConducingEquipment = value; }
        }

        public override bool Equals(object x)
        {
            if(base.Equals(x))
            {
                Control c = (Control)x;
                return (c.regulatingConducingEquipment == regulatingConducingEquipment);
            }else return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.CONTROL_REGULATINGCONDEQUIPMENT:
                    property.SetValue(regulatingConducingEquipment);
                    break;

                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override bool HasProperty(ModelCode property)
        {
            switch (property) {

                case ModelCode.CONTROL_REGULATINGCONDEQUIPMENT:
                    return true;
                default:

                return base.HasProperty(property);
        }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.CONTROL_REGULATINGCONDEQUIPMENT:
                    regulatingConducingEquipment = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (regulatingConducingEquipment != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.CONTROL_REGULATINGCONDEQUIPMENT] = new List<long>();
                references[ModelCode.CONTROL_REGULATINGCONDEQUIPMENT].Add(regulatingConducingEquipment);
            }
            base.GetReferences(references, refType);
        }
    }
}
