namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;
    using System;

    /// <summary>
    /// PowerTransformerConverter has methods for populating
    /// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
    /// </summary>
	public static class PowerTransformerConverter
	{

        
		#region Populate ResourceDescription
		public static void PopulateIndetifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject,ResourceDescription rd)
		{
			if((cimIdentifiedObject!=null) && (rd!=null))
			{
				return; 
			}
		}

		public static void PopulateControlProperties(FTN.Control cimControl,ResourceDescription rd,ImportHelper importHelper,TransformAndLoadReport report)
		{
			if( (cimControl!=null) && (rd!=null) ) 
			{
				PowerTransformerConverter.PopulateIndetifiedObjectProperties (cimControl,rd);
				if (cimControl.RegulatingCondEqHasValue)
				{
					long gid = importHelper.GetMappedGID(cimControl.RegulatingCondEq.ID);
					if (gid < 0)
					{
                        report.Report.Append("WARNING: Convert ").Append(cimControl.GetType().ToString()).Append(" rdfID = \"").Append(cimControl.ID);
                        report.Report.Append("\" - Failed to set reference to RegulatingCondEq: rdfID \"").Append(cimControl.RegulatingCondEq.ID).AppendLine(" \" is not mapped to GID!");
                    }
					rd.AddProperty(new Property(ModelCode.CONTROL_REGULATINGCONDEQUIPMENT,gid));
				}
			}
		}

		public static void PopulateTerminalProperties(FTN.Terminal cimTerminal,ResourceDescription rd,ImportHelper importHelper, TransformAndLoadReport report)
		{
			if((cimTerminal!=null)  && (rd!=null))
			{
				PowerTransformerConverter.PopulateIndetifiedObjectProperties(cimTerminal,rd);
				if(cimTerminal.ConductingEquipmentHasValue)
				{
					long gid = importHelper.GetMappedGID(cimTerminal.ConductingEquipment.ID);
					if(gid < 0)
					{
                        report.Report.Append("WARNING: Convert ").Append(cimTerminal.GetType().ToString()).Append(" rdfID = \"").Append(cimTerminal.ID);
                        report.Report.Append("\" - Failed to set reference to ConductingEquipment: rdfID \"").Append(cimTerminal.ConductingEquipment.ID).AppendLine(" \" is not mapped to GID!");
                    }
					rd.AddProperty(new Property(ModelCode.TERMINAL_CONDUCTINGEQUIPMENT,gid));	
                }
			}
		}

		public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPSR, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if( (cimPSR!=null) && (rd!=null))
			{
				PowerTransformerConverter.PopulateIndetifiedObjectProperties(cimPSR,rd);
			}
		}

		public static void PopulateCurveDataProperties(FTN.CurveData cimCurveData, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
            if ((cimCurveData!=null) && (rd!=null))
            {
				PowerTransformerConverter.PopulateIndetifiedObjectProperties(cimCurveData,rd);
				
				if (cimCurveData.XvalueHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVEDATA_XVALUE, cimCurveData.Xvalue));
				}
				if(cimCurveData.Y1valueHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVEDATA_Y1VALUE, cimCurveData.Y1value));
				}

				if( cimCurveData.Y2valueHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVEDATA_Y2VALUE, cimCurveData.Y2value));
				}
				if(cimCurveData.Y3valueHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVEDATA_Y3VALUE, cimCurveData.Y3value));
				}
                if (cimCurveData.CurveHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimCurveData.Curve.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimCurveData.GetType().ToString()).Append(" rdfID = \"").Append(cimCurveData.ID);
                        report.Report.Append("\" - Failed to set reference to Curve: rdfID \"").Append(cimCurveData.Curve.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.CURVEDATA_CURVE, gid));
                }
            }
        }

		public static void PopulateCurveProperties(FTN.Curve cimCurve, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimCurve != null) && (rd!=null))
			{
				PowerTransformerConverter.PopulateIndetifiedObjectProperties(cimCurve, rd);
				if (cimCurve.CurveStyleHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_CURVESTYLE, (short)GetDMSCurveStyle(cimCurve.CurveStyle)));
				}
				if(cimCurve.XMultiplierHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_XMULTIPLIER,(short)GetDMSUnitMultiplier(cimCurve.XMultiplier)));
				}
				if(cimCurve.XUnitHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_XUNIT, (short)GetDMSUnitSymbol(cimCurve.XUnit)));
				}
				if (cimCurve.Y1MultiplierHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_Y1MULTIPLIER, (short)GetDMSUnitMultiplier(cimCurve.Y1Multiplier)));
				}
				if(cimCurve.Y1UnitHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_Y1UNIT, (short)GetDMSUnitSymbol(cimCurve.Y1Unit)));
				}

                if (cimCurve.Y2MultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_Y2MULTIPLIER, (short)GetDMSUnitMultiplier(cimCurve.Y2Multiplier)));
                }
                if (cimCurve.Y2UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_Y2UNIT, (short)GetDMSUnitSymbol(cimCurve.Y2Unit)));
                }

                if (cimCurve.Y3MultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_Y3MULTIPLIER, (short)GetDMSUnitMultiplier(cimCurve.Y3Multiplier)));
                }
                if (cimCurve.Y3UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_Y3UNIT, (short)GetDMSUnitSymbol(cimCurve.Y3Unit)));
                }
				
            }
		}

		public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ( (cimEquipment != null) && (rd!=null)) 
			{
				PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd, importHelper, report);
				if (cimEquipment.AggregateHasValue)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_AGGREGATE, cimEquipment.Aggregate));
				}
				if (cimEquipment.NormallyInServiceHasValue)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_NORMALLYINSERVICE,cimEquipment.NormallyInService));
				}
			}
		}

        public static void PopulateRegulatingControlProperties(FTN.RegulatingControl cimRegulatingControl, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimRegulatingControl != null) && (rd != null))
            {
                PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimRegulatingControl, rd, importHelper, report);
				if(cimRegulatingControl.DiscreteHasValue)
				{
					rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_DISCRETE, cimRegulatingControl.Discrete));
				}
				if (cimRegulatingControl.ModeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_MODE,(short)GetDMSRegulatingControlModeKind(cimRegulatingControl.Mode)));	
				}
				if(cimRegulatingControl.MonitoredPhaseHasValue)
				{
					rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_MONITOREDPHASE,(short)GetDMSPhaseCode(cimRegulatingControl.MonitoredPhase)));
				}
				if (cimRegulatingControl.TargetRangeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_TARGETRANGE, cimRegulatingControl.TargetRange));
				}
				if(cimRegulatingControl.TargetValueHasValue)
				{
					rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_TARGETVALUE,cimRegulatingControl.TargetValue));
				}
				
            }
        }


		public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
            if ((cimConductingEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateEquipmentProperties(cimConductingEquipment, rd, importHelper, report);	
			}
        }

        public static void PopulateRegulatingCondEquipmentProperties(FTN.RegulatingCondEq cimRegulatingCondEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimRegulatingCondEquipment != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConductingEquipmentProperties(cimRegulatingCondEquipment, rd, importHelper, report);
				if (cimRegulatingCondEquipment.RegulatingControlHasValue)
				{
                    long gid = importHelper.GetMappedGID(cimRegulatingCondEquipment.RegulatingControl.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimRegulatingCondEquipment.GetType().ToString()).Append(" rdfID = \"").Append(cimRegulatingCondEquipment.ID);
                        report.Report.Append("\" - Failed to set reference to RegulatingControl: rdfID \"").Append(cimRegulatingCondEquipment.RegulatingControl.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.REGULATINGCONDUCTINGEQUIPMENT_REGULATINGCONTROL, gid));
                }
            }
        }

		public static void PopulateFrequencyConverterProperties(FTN.FrequencyConverter cimFrequencyConverter, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if((cimFrequencyConverter != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateRegulatingCondEquipmentProperties(cimFrequencyConverter, rd, importHelper, report);	
			}
		}

        public static void PopulateShuntCompensatorProperties(FTN.ShuntCompensator cimFrequencyConverter, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimFrequencyConverter != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateRegulatingCondEquipmentProperties(cimFrequencyConverter, rd, importHelper, report);
            }
        }

        public static void PopulateStaticVarCompensatorProperties(FTN.StaticVarCompensator cimFrequencyConverter, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimFrequencyConverter != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateRegulatingCondEquipmentProperties(cimFrequencyConverter, rd, importHelper, report);
            }
        }

        public static void PopulateRotatingMachineProperties(FTN.RotatingMachine cimFrequencyConverter, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimFrequencyConverter != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateRegulatingCondEquipmentProperties(cimFrequencyConverter, rd, importHelper, report);
            }
        }

        public static void PopulateSynchronousMachineProperties(FTN.SynchronousMachine cimSynchronousMachine, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimSynchronousMachine != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateRotatingMachineProperties(cimSynchronousMachine, rd, importHelper, report);
				if (cimSynchronousMachine.ReactiveCapabilityCurvesHasValue)
				{
                    long gid = importHelper.GetMappedGID(cimSynchronousMachine.ReactiveCapabilityCurves.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimSynchronousMachine.GetType().ToString()).Append(" rdfID = \"").Append(cimSynchronousMachine.ID);
                        report.Report.Append("\" - Failed to set reference to ReactiveCapabilityCurve: rdfID \"").Append(cimSynchronousMachine.ReactiveCapabilityCurves.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.SYNCHRONOUSMACHINE_REACTIVECAPABILITYCURVE, gid));
                }
            }
        }

		public static void PopulateReactiveCapabilityCurve(FTN.ReactiveCapabilityCurve cimReactiveCapabilityCurve, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if((cimReactiveCapabilityCurve != null) && (rd!=null))
			{
				PowerTransformerConverter.PopulateCurveProperties(cimReactiveCapabilityCurve, rd, importHelper, report);	

			}
		}


        
        #endregion Populate ResourceDescription

        #region Enums convert
        public static PhaseCode GetDMSPhaseCode(FTN.PhaseCode phases)
        {
            switch (phases)
            {
                case FTN.PhaseCode.A:
                    return PhaseCode.A;
                case FTN.PhaseCode.AB:
                    return PhaseCode.AB;
                case FTN.PhaseCode.ABC:
                    return PhaseCode.ABC;
                case FTN.PhaseCode.ABCN:
                    return PhaseCode.ABCN;
                case FTN.PhaseCode.ABN:
                    return PhaseCode.ABN;
                case FTN.PhaseCode.AC:
                    return PhaseCode.AC;
                case FTN.PhaseCode.ACN:
                    return PhaseCode.ACN;
                case FTN.PhaseCode.AN:
                    return PhaseCode.AN;
                case FTN.PhaseCode.B:
                    return PhaseCode.B;
                case FTN.PhaseCode.BC:
                    return PhaseCode.BC;
                case FTN.PhaseCode.BCN:
                    return PhaseCode.BCN;
                case FTN.PhaseCode.BN:
                    return PhaseCode.BN;
                case FTN.PhaseCode.C:
                    return PhaseCode.C;
                case FTN.PhaseCode.CN:
                    return PhaseCode.CN;
                case FTN.PhaseCode.N:
                    return PhaseCode.N;
                case FTN.PhaseCode.s12N:
                    return PhaseCode.ABN;
                case FTN.PhaseCode.s1N:
                    return PhaseCode.AN;
                case FTN.PhaseCode.s2N:
                    return PhaseCode.BN;
                default:
                    throw new ArgumentException("Unknown FTN.PhaseCode: " + phases);
            }
        }

        public static RegulatingControlModeKind GetDMSRegulatingControlModeKind(FTN.RegulatingControlModeKind regulatingControlModeKind)
        {
            switch (regulatingControlModeKind)
            {
                case FTN.RegulatingControlModeKind.reactivePower:
                    return RegulatingControlModeKind.reactivePower;
                case FTN.RegulatingControlModeKind.powerFactor:
                    return RegulatingControlModeKind.powerFactor;
                case FTN.RegulatingControlModeKind.activePower:
                    return RegulatingControlModeKind.activePower;
                case FTN.RegulatingControlModeKind.temperature:
                    return RegulatingControlModeKind.temperature;
                case FTN.RegulatingControlModeKind.voltage:
                    return RegulatingControlModeKind.voltage;
                case FTN.RegulatingControlModeKind.@fixed:
                    return RegulatingControlModeKind.@fixed;
                case FTN.RegulatingControlModeKind.timeScheduled:
                    return RegulatingControlModeKind.timeScheduled;
                case FTN.RegulatingControlModeKind.admittance:
                    return RegulatingControlModeKind.admittance;
                case FTN.RegulatingControlModeKind.currentFlow:
                    return RegulatingControlModeKind.currentFlow;
                default:
                    throw new ArgumentException("Unknown FTN.RegulatingControlModeKind: " + regulatingControlModeKind);
            }
        }

        public static CurveStyle GetDMSCurveStyle(FTN.CurveStyle curveStyle)
        {
            switch (curveStyle)
            {
                case FTN.CurveStyle.formula:
                    return CurveStyle.formula;
                case FTN.CurveStyle.constantYValue:
                    return CurveStyle.constantYValue;
                case FTN.CurveStyle.straightLineYValues:
                    return CurveStyle.straightLineYValues;
                case FTN.CurveStyle.rampYValue:
                    return CurveStyle.rampYValue;
                default:
                    throw new ArgumentException("Unknown FTN.CurveStyle: " + curveStyle);
            }
        }

        public static UnitMultiplier GetDMSUnitMultiplier(FTN.UnitMultiplier unitMultiplier)
        {
            switch (unitMultiplier)
            {
                case FTN.UnitMultiplier.none:
                    return UnitMultiplier.none;
                case FTN.UnitMultiplier.m:
                    return UnitMultiplier.m;
                case FTN.UnitMultiplier.G:
                    return UnitMultiplier.G;
                case FTN.UnitMultiplier.n:
                    return UnitMultiplier.n;
                case FTN.UnitMultiplier.d:
                    return UnitMultiplier.d;
                case FTN.UnitMultiplier.k:
                    return UnitMultiplier.k;
                case FTN.UnitMultiplier.c:
                    return UnitMultiplier.c;
                case FTN.UnitMultiplier.T:
                    return UnitMultiplier.T;
                case FTN.UnitMultiplier.M:
                    return UnitMultiplier.M;
                case FTN.UnitMultiplier.micro:
                    return UnitMultiplier.micro;
                case FTN.UnitMultiplier.p:
                    return UnitMultiplier.p;
                default:
                    throw new ArgumentException("Unknown FTN.UnitMultiplier: " + unitMultiplier);
            }
        }


        public static UnitSymbol GetDMSUnitSymbol(FTN.UnitSymbol unitSymbol)
        {
            switch (unitSymbol)
            {
                case FTN.UnitSymbol.A:
                    return UnitSymbol.A;
                case FTN.UnitSymbol.J:
                    return UnitSymbol.J;
                case FTN.UnitSymbol.Hz:
                    return UnitSymbol.Hz;
                case FTN.UnitSymbol.ohm:
                    return UnitSymbol.ohm;
                case FTN.UnitSymbol.deg:
                    return UnitSymbol.deg;
                case FTN.UnitSymbol.Wh:
                    return UnitSymbol.Wh;
                case FTN.UnitSymbol.S:
                    return UnitSymbol.S;
                case FTN.UnitSymbol.VAr:
                    return UnitSymbol.VAr;
                case FTN.UnitSymbol.m:
                    return UnitSymbol.m;
                case FTN.UnitSymbol.VA:
                    return UnitSymbol.VA;
                case FTN.UnitSymbol.VAh:
                    return UnitSymbol.VAh;
                case FTN.UnitSymbol.H:
                    return UnitSymbol.H;
                case FTN.UnitSymbol.N:
                    return UnitSymbol.N;
                case FTN.UnitSymbol.Pa:
                    return UnitSymbol.Pa;
                case FTN.UnitSymbol.h:
                    return UnitSymbol.h;
                case FTN.UnitSymbol.V:
                    return UnitSymbol.V;
                case FTN.UnitSymbol.g:
                    return UnitSymbol.g;
                case FTN.UnitSymbol.none:
                    return UnitSymbol.none;
                case FTN.UnitSymbol.W:
                    return UnitSymbol.W;
                case FTN.UnitSymbol.rad:
                    return UnitSymbol.rad;
                case FTN.UnitSymbol.VArh:
                    return UnitSymbol.VArh;
                case FTN.UnitSymbol.m3:
                    return UnitSymbol.m3;
                case FTN.UnitSymbol.degC:
                    return UnitSymbol.degC;
                case FTN.UnitSymbol.F:
                    return UnitSymbol.F;
                case FTN.UnitSymbol.s:
                    return UnitSymbol.s;
                case FTN.UnitSymbol.min:
                    return UnitSymbol.min;
                case FTN.UnitSymbol.m2:
                    return UnitSymbol.m2;
                default:
                    throw new ArgumentException("Unknown FTN.UnitSymbol: " + unitSymbol);
            }
        }

        #endregion Enums convert
    }
}
