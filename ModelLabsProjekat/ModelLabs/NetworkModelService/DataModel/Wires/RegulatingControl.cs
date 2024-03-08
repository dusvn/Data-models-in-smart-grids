using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class RegulatingControl : PowerSystemResource
    {
        private bool discrete;
        private RegulatingControlModeKind mode;
        private PhaseCode monitoredPhase;
        private float targetRange;
        private float targetValue;
        private List<long> regulatingCondEqs = new List<long>(); 

        public bool Discrete
        {
            get { return discrete; }    
            set { discrete = value; }
        }

        public RegulatingControlModeKind Mode
        {
            get { return mode; }
            set { mode = value; }   
        }
        
        public PhaseCode MonitoredPhase
        {
            get { return monitoredPhase; }
            set { monitoredPhase = value; }
        }
        public float TargetRange
        {
            get { return targetRange; }
            set { targetRange = value; }
        }
        public float TargetValue
        {
            get { return targetValue; }
            set { targetValue = value; }
        }

        private List<long> RegulatingCondEqs
        {
            get { return regulatingCondEqs; }
            set { regulatingCondEqs = value; }
        }

        public override bool IsReferenced
        {
            get
            {
                return regulatingCondEqs.Count != 0 || base.IsReferenced;
            }
        }

        public RegulatingControl(long globalId) : base(globalId)
        {
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (regulatingCondEqs != null && regulatingCondEqs.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.REGULATINGCONTROL_REGULATINGCONDUCTINGEQUIPMENTS] = regulatingCondEqs.GetRange(0, regulatingCondEqs.Count);
            }
            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId) 
            {
                case ModelCode.REGULATINGCONDUCTINGEQUIPMENT_REGULATINGCONTROL:
                    regulatingCondEqs.Add(globalId); 
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
                case ModelCode.REGULATINGCONDUCTINGEQUIPMENT_REGULATINGCONTROL:

                    if (regulatingCondEqs.Contains(globalId))
                    {
                        regulatingCondEqs.Remove(globalId);
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

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
               RegulatingControl rc = (RegulatingControl)obj;   
               return (rc.discrete == this.discrete && rc.mode == mode && rc.MonitoredPhase == monitoredPhase && rc.targetRange == targetRange && rc.targetValue == targetValue);
            }
            else return true; 
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool HasProperty(ModelCode property)
        {
            switch (property) {
                case ModelCode.REGULATINGCONTROL_MODE:
                case ModelCode.REGULATINGCONTROL_DISCRETE:
                case ModelCode.REGULATINGCONTROL_MONITOREDPHASE:
                case ModelCode.REGULATINGCONTROL_TARGETRANGE:
                case ModelCode.REGULATINGCONTROL_TARGETVALUE:
                case ModelCode.REGULATINGCONTROL_REGULATINGCONDUCTINGEQUIPMENTS:
                    return true; 
                default:
            return base.HasProperty(property);
        }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id) 
            {
                case ModelCode.REGULATINGCONTROL_DISCRETE:
                    property.SetValue(discrete);
                    break;
                case ModelCode.REGULATINGCONTROL_MODE:
                    property.SetValue((short)mode);
                    break;
                case ModelCode.REGULATINGCONTROL_MONITOREDPHASE:
                    property.SetValue((short)monitoredPhase);
                    break;
                case ModelCode.REGULATINGCONTROL_TARGETRANGE:
                    property.SetValue(targetRange);
                    break;
                case ModelCode.REGULATINGCONTROL_TARGETVALUE:
                    property.SetValue(targetValue);
                    break;
                case ModelCode.REGULATINGCONTROL_REGULATINGCONDUCTINGEQUIPMENTS:
                    property.SetValue(regulatingCondEqs);
                    break;
                default:
            base.GetProperty(property);
                    break;
        }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.REGULATINGCONTROL_MODE:
                    mode = (RegulatingControlModeKind)property.AsEnum();
                    break;
                case ModelCode.REGULATINGCONTROL_DISCRETE:
                    discrete = property.AsBool();
                    break;
                case ModelCode.REGULATINGCONTROL_MONITOREDPHASE:
                    monitoredPhase = (PhaseCode)property.AsEnum();
                    break;
                case ModelCode.REGULATINGCONTROL_TARGETRANGE:
                    targetRange = property.AsFloat();
                    break;
                case ModelCode.REGULATINGCONTROL_TARGETVALUE:
                    targetValue = property.AsFloat();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

    }
}
