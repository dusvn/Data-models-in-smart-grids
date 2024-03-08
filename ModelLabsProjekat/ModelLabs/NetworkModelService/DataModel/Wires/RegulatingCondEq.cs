using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class RegulatingCondEq : ConductingEquipment
    {
        private long regulatingControl;
        private List<long> controls = new List<long>();
        public RegulatingCondEq(long globalId) : base(globalId)
        {
        }

        public long RegulatingControl
        {
            get { return regulatingControl; }
            set { regulatingControl = value; }
        }

        public List<long> Controls
        {
            get { return controls; }
            set { controls = value; }
        }

        public override bool IsReferenced
        {
            get
            {
                return controls.Count != 0 || base.IsReferenced;
            }
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.CONTROL_REGULATINGCONDEQUIPMENT:
                    controls.Add(globalId);
                    break;
                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                RegulatingCondEq e = (RegulatingCondEq)obj;
                return (e.controls == controls && e.regulatingControl == regulatingControl);
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.REGULATINGCONDUCTINGEQUIPMENT_CONTROLS:
                    prop.SetValue(controls);
                    break;
                case ModelCode.REGULATINGCONDUCTINGEQUIPMENT_REGULATINGCONTROL:
                    prop.SetValue(regulatingControl);
                    break;
                default:
                    base.GetProperty(prop);
                    break;
            }
        }


        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (controls != null && controls.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.REGULATINGCONDUCTINGEQUIPMENT_CONTROLS] = controls.GetRange(0, controls.Count);
            }
            if (regulatingControl != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.REGULATINGCONDUCTINGEQUIPMENT_REGULATINGCONTROL] = new List<long>();
                references[ModelCode.REGULATINGCONDUCTINGEQUIPMENT_REGULATINGCONTROL].Add(regulatingControl);
            }
            base.GetReferences(references, refType);
        }

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.REGULATINGCONDUCTINGEQUIPMENT_CONTROLS:
                case ModelCode.REGULATINGCONDUCTINGEQUIPMENT_REGULATINGCONTROL:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.CONTROL_REGULATINGCONDEQUIPMENT:

                    if (controls.Contains(globalId))
                    {
                        controls.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;

                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.REGULATINGCONDUCTINGEQUIPMENT_REGULATINGCONTROL:
                    regulatingControl = property.AsReference();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }
    }
}
