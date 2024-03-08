using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class SynchronousMachine : RotatingMachine
    {
        private long reactiveCapabilityCurve;
        public SynchronousMachine(long globalId) : base(globalId)
        {
        }
        public long ReactiveCapabilityCurve
        {
            get { return reactiveCapabilityCurve;}
            set { reactiveCapabilityCurve = value; }    
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                SynchronousMachine other = (SynchronousMachine)obj;
                return (other.reactiveCapabilityCurve == this.reactiveCapabilityCurve);
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
                case ModelCode.SYNCHRONOUSMACHINE_REACTIVECAPABILITYCURVE:
                    prop.SetValue(reactiveCapabilityCurve);
                    break;
                
                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (reactiveCapabilityCurve != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.SYNCHRONOUSMACHINE_REACTIVECAPABILITYCURVE] = new List<long>();
                references[ModelCode.SYNCHRONOUSMACHINE_REACTIVECAPABILITYCURVE].Add(reactiveCapabilityCurve);
            }
            base.GetReferences(references, refType);
        }

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                case ModelCode.SYNCHRONOUSMACHINE_REACTIVECAPABILITYCURVE:
                    return true;
                default:
                    return base.HasProperty(t);
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SYNCHRONOUSMACHINE_REACTIVECAPABILITYCURVE:
                    reactiveCapabilityCurve = property.AsReference();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }
    }
}
