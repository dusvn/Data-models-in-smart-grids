using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Curve : IdentifiedObject
    {
        private CurveStyle curveStyle;
        private UnitMultiplier xMultiplier;
        private UnitSymbol xUnit;
        private UnitMultiplier y1Multiplier;
        private UnitMultiplier y2Multiplier;
        private UnitMultiplier y3Multiplier;
        private UnitSymbol y1Unit;
        private UnitSymbol y2Unit;
        private UnitSymbol y3Unit;

        private List<long> curveDatas = new List<long>();

        public CurveStyle CurveStyle
        {
            get { return curveStyle; }
            set { curveStyle = value; }
        }
        public UnitMultiplier XMultiplier
        {
            get { return xMultiplier; }
            set { xMultiplier = value; }
        } 

        public UnitSymbol XUnit
        {
            get { return xUnit; }
            set {  xUnit = value; }
        }

        public UnitMultiplier Y1Multiplier
        {
            get { return y1Multiplier; }
            set { y1Multiplier = value;  }
        }

        public UnitMultiplier Y2Multiplier
        {
            get { return y2Multiplier; }
            set { y2Multiplier = value; }
        }

        public UnitMultiplier Y3Multiplier
        {
            get { return y3Multiplier; }
            set { y3Multiplier = value; }
        }
        public UnitSymbol Y1Unit
        {
            get { return Y1Unit; }
            set { Y1Unit = value; }
        }

        public UnitSymbol Y2Unit
        {
            get { return Y2Unit; }
            set { Y2Unit = value; }
        }
        public UnitSymbol Y3Unit
        {
            get { return Y3Unit; }
            set { Y3Unit = value; }
        }

        public List<long> CurveDatas
        {
            get { return curveDatas; }
            set { curveDatas = value; }
        }
        public Curve(long globalId) : base(globalId)
        {
        }

        public override bool Equals(object x)
        {
            if (base.Equals(x))
            {
                Curve c = (Curve)x;
                return (c.curveStyle == curveStyle && c.xMultiplier == xMultiplier && c.xUnit == xUnit && c.Y1Multiplier == y1Multiplier && c.Y2Multiplier == y2Multiplier && c.Y3Multiplier == y3Multiplier && c.Y1Unit == y1Unit && c.Y2Unit == y2Unit && c.y3Unit == y3Unit);
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.CURVE_CURVESTYLE:
                case ModelCode.CURVE_XMULTIPLIER:
                case ModelCode.CURVE_XUNIT:
                case ModelCode.CURVE_Y1MULTIPLIER:
                case ModelCode.CURVE_Y2MULTIPLIER:
                case ModelCode.CURVE_Y3MULTIPLIER:
                case ModelCode.CURVE_Y1UNIT:
                case ModelCode.CURVE_Y2UNIT:
                case ModelCode.CURVE_Y3UNIT:
                case ModelCode.CURVE_CURVEDATAS:
                    return true;    
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id) {
                case ModelCode.CURVE_XUNIT:
                    property.SetValue((short)xUnit);
                    break;
                case ModelCode.CURVE_CURVESTYLE:
                    property.SetValue((short)curveStyle);
                    break;
                case ModelCode.CURVE_XMULTIPLIER:
                    property.SetValue((short)xMultiplier);
                    break;
                case ModelCode.CURVE_Y1MULTIPLIER:
                    property.SetValue((short)y1Multiplier);
                    break;
                case ModelCode.CURVE_Y2MULTIPLIER:
                    property.SetValue((short)y2Multiplier);
                    break;
                case ModelCode.CURVE_Y3MULTIPLIER:
                    property.SetValue((short)y3Multiplier);
                    break;
                case ModelCode.CURVE_Y1UNIT:
                    property.SetValue((short)y1Unit);
                    break;
                case ModelCode.CURVE_Y2UNIT:
                    property.SetValue((short)y2Unit);
                    break;  
                case ModelCode.CURVE_Y3UNIT:
                    property.SetValue((short)y3Unit);
                    break;
                case ModelCode.CURVE_CURVEDATAS:
                    property.SetValue(curveDatas);
                    break;
                default:
            base.GetProperty(property);
                    break;
        }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id) {
                case ModelCode.CURVE_CURVESTYLE:
                    curveStyle = (CurveStyle)property.AsEnum();
                    break;
                case ModelCode.CURVE_XMULTIPLIER:
                    xMultiplier = (UnitMultiplier)property.AsEnum();
                    break;
                case ModelCode.CURVE_XUNIT:
                    xUnit = (UnitSymbol)property.AsEnum();
                    break;
                case ModelCode.CURVE_Y1MULTIPLIER:
                    y1Multiplier = (UnitMultiplier) property.AsEnum();
                    break;  
                case ModelCode.CURVE_Y2MULTIPLIER:
                    y2Multiplier = (UnitMultiplier)property.AsEnum();
                    break;
                case ModelCode.CURVE_Y3MULTIPLIER:
                    y3Multiplier = (UnitMultiplier)property.AsEnum();
                    break;
                case ModelCode.CURVE_Y1UNIT:
                    y1Unit = (UnitSymbol)property.AsEnum();
                    break;
                case ModelCode.CURVE_Y2UNIT:
                    y2Unit = (UnitSymbol)property.AsEnum();
                    break;
                case ModelCode.CURVE_Y3UNIT:
                    y3Unit = (UnitSymbol)property.AsEnum();
                    break;
                
                default:
                base.SetProperty(property);
                break;
        }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {

            if (curveDatas != null && curveDatas.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.CURVE_CURVEDATAS] = curveDatas.GetRange(0, curveDatas.Count);
            }
            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId) {
                case ModelCode.CURVEDATA_CURVE:
                    curveDatas.Add(globalId);
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
                case ModelCode.CURVEDATA_CURVE:
                    if(curveDatas.Contains(globalId))
                    {
                        curveDatas.Remove(globalId);    
                    }else
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
