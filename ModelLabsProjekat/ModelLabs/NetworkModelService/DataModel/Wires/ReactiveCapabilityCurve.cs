using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class ReactiveCapabilityCurve : Curve
    {
        private List<long> synchronousMachines = new List<long>();
        public ReactiveCapabilityCurve(long globalId) : base(globalId)
        {
        }

        public override bool IsReferenced
        {
            get
            {
                return synchronousMachines.Count != 0 || base.IsReferenced;
            }
        }
        public List<long> SynchronousMachines
        {
            get { return synchronousMachines; }
            set { synchronousMachines = value; }
        }

        public override bool Equals(object x)
        {
            if (base.Equals(x))
            {
                ReactiveCapabilityCurve c = (ReactiveCapabilityCurve)x;
                return (c.synchronousMachines == synchronousMachines);
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
                case ModelCode.REACTIVECAPABILITYCURVE_SYNCHRONOUSMACHINES:
                    property.SetValue(synchronousMachines);
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
                case ModelCode.REACTIVECAPABILITYCURVE_SYNCHRONOUSMACHINES:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (synchronousMachines != null && synchronousMachines.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.REACTIVECAPABILITYCURVE_SYNCHRONOUSMACHINES] = synchronousMachines.GetRange(0, synchronousMachines.Count);
            }
            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SYNCHRONOUSMACHINE_REACTIVECAPABILITYCURVE:
                    synchronousMachines.Add(globalId);
                    break;
                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SYNCHRONOUSMACHINE_REACTIVECAPABILITYCURVE:

                    if (synchronousMachines.Contains(globalId))
                    {
                        synchronousMachines.Remove(globalId);
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

    }
}
