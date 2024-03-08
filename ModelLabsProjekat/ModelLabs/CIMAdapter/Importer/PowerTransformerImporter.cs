using System;
using System.Collections.Generic;
using System.Data;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	/// <summary>
	/// PowerTransformerImporter
	/// </summary>
	public class PowerTransformerImporter
	{
		/// <summary> Singleton </summary>
		private static PowerTransformerImporter ptImporter = null;
		private static object singletoneLock = new object();

		private ConcreteModel concreteModel;
		private Delta delta;
		private ImportHelper importHelper;
		private TransformAndLoadReport report;


		#region Properties
		public static PowerTransformerImporter Instance
		{
			get
			{
				if (ptImporter == null)
				{
					lock (singletoneLock)
					{
						if (ptImporter == null)
						{
							ptImporter = new PowerTransformerImporter();
							ptImporter.Reset();
						}
					}
				}
				return ptImporter;
			}
		}

		public Delta NMSDelta
		{
			get 
			{
				return delta;
			}
		}
		#endregion Properties


		public void Reset()
		{
			concreteModel = null;
			delta = new Delta();
			importHelper = new ImportHelper();
			report = null;
		}

		public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
		{
			LogManager.Log("Importing PowerTransformer Elements...", LogLevel.Info);
			report = new TransformAndLoadReport();
			concreteModel = cimConcreteModel;
			delta.ClearDeltaOperations();

			if ((concreteModel != null) && (concreteModel.ModelMap != null))
			{
				try
				{
					// convert into DMS elements
					ConvertModelAndPopulateDelta();
				}
				catch (Exception ex)
				{
					string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
					LogManager.Log(message);
					report.Report.AppendLine(ex.Message);
					report.Success = false;
				}
			}
			LogManager.Log("Importing PowerTransformer Elements - END.", LogLevel.Info);
			return report;
		}

		/// <summary>
		/// Method performs conversion of network elements from CIM based concrete model into DMS model.
		/// </summary>
        /// 
        
		private void ConvertModelAndPopulateDelta()
		{
			LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

            //// import all concrete model types (DMSType enum)
            ///
            /*
            typeIdsInInsertOrder.Add(ModelCode.REGULATINGCONTROL);
            typeIdsInInsertOrder.Add(ModelCode.FREQUENCYCONVERTER);
            typeIdsInInsertOrder.Add(ModelCode.SHUNTCOMPENSATOR);
            typeIdsInInsertOrder.Add(ModelCode.STATICVARCOMPENSATOR);
            typeIdsInInsertOrder.Add(ModelCode.TERMINAL);
            typeIdsInInsertOrder.Add(ModelCode.CONTROL);
            typeIdsInInsertOrder.Add(ModelCode.REACTIVECAPABILITYCURVE);
            typeIdsInInsertOrder.Add(ModelCode.SYNCHRONOUSMACHINE);
            typeIdsInInsertOrder.Add(ModelCode.CURVEDATA);
            */
            ImportRegulatingControl();
            ImportFrequencyConvertert();
            ImportShuntCompensator();
            ImportStaticVarCompensator();
            ImportTerminal();
            ImportControl();
            ImportReactiveCapabilityCurve();
            ImportSynchronousMachine();
            ImportCurveData();


            LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
		}

        #region Import 
        private void ImportFrequencyConvertert()
        {
            SortedDictionary<string, object> cimACLineSegments = concreteModel.GetAllObjectsOfType("FTN.FrequencyConverter");
            if (cimACLineSegments != null)
            {
                foreach (KeyValuePair<string, object> cimACLineSegmentPair in cimACLineSegments)
                {
                    FTN.FrequencyConverter cimACLineSegment = cimACLineSegmentPair.Value as FTN.FrequencyConverter;

                    ResourceDescription rd = CreateFrequencyConvertertResourceDescription(cimACLineSegment);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("FrequencyConverter ID = ").Append(cimACLineSegment.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("FrequencyConverter ID = ").Append(cimACLineSegment.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private void ImportShuntCompensator()
        {
            SortedDictionary<string, object> cimACLineSegments = concreteModel.GetAllObjectsOfType("FTN.ShuntCompensator");
            if (cimACLineSegments != null)
            {
                foreach (KeyValuePair<string, object> cimACLineSegmentPair in cimACLineSegments)
                {
                    FTN.ShuntCompensator cimACLineSegment = cimACLineSegmentPair.Value as FTN.ShuntCompensator;

                    ResourceDescription rd = CreateShuntCompensatorResourceDescription(cimACLineSegment);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("ShuntCompensator ID = ").Append(cimACLineSegment.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("ShuntCompensator ID = ").Append(cimACLineSegment.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private void ImportStaticVarCompensator()
        {
            SortedDictionary<string, object> cimACLineSegments = concreteModel.GetAllObjectsOfType("FTN.StaticVarCompensator");
            if (cimACLineSegments != null)
            {
                foreach (KeyValuePair<string, object> cimACLineSegmentPair in cimACLineSegments)
                {
                    FTN.StaticVarCompensator cimACLineSegment = cimACLineSegmentPair.Value as FTN.StaticVarCompensator;

                    ResourceDescription rd = CreateStaticVarCompenasatorResourceDescription(cimACLineSegment);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("StaticVarCompensator ID = ").Append(cimACLineSegment.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("StaticVarCompensator ID = ").Append(cimACLineSegment.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private void ImportControl()
        {
            SortedDictionary<string, object> cimACLineSegments = concreteModel.GetAllObjectsOfType("FTN.Control");
            if (cimACLineSegments != null)
            {
                foreach (KeyValuePair<string, object> cimACLineSegmentPair in cimACLineSegments)
                {
                    FTN.Control cimACLineSegment = cimACLineSegmentPair.Value as FTN.Control;

                    ResourceDescription rd = CreateControlResourceDescription(cimACLineSegment);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Control ID = ").Append(cimACLineSegment.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Control ID = ").Append(cimACLineSegment.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private void ImportTerminal()
        {
            SortedDictionary<string, object> cimACLineSegments = concreteModel.GetAllObjectsOfType("FTN.Terminal");
            if (cimACLineSegments != null)
            {
                foreach (KeyValuePair<string, object> cimACLineSegmentPair in cimACLineSegments)
                {
                    FTN.Terminal cimACLineSegment = cimACLineSegmentPair.Value as FTN.Terminal;

                    ResourceDescription rd = CreateTerminalResourceDescription(cimACLineSegment);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Terminal ID = ").Append(cimACLineSegment.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Terminal ID = ").Append(cimACLineSegment.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private void ImportSynchronousMachine()
        {
            SortedDictionary<string, object> cimACLineSegments = concreteModel.GetAllObjectsOfType("FTN.SynchronousMachine");
            if (cimACLineSegments != null)
            {
                foreach (KeyValuePair<string, object> cimACLineSegmentPair in cimACLineSegments)
                {
                    FTN.SynchronousMachine cimACLineSegment = cimACLineSegmentPair.Value as FTN.SynchronousMachine;

                    ResourceDescription rd = CreateSynchronousMachineResourceDescription(cimACLineSegment);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("SynchronousMachine ID = ").Append(cimACLineSegment.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("SynchronousMachine ID = ").Append(cimACLineSegment.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private void ImportReactiveCapabilityCurve()
        {
            SortedDictionary<string, object> cimACLineSegments = concreteModel.GetAllObjectsOfType("FTN.ReactiveCapabilityCurve");
            if (cimACLineSegments != null)
            {
                foreach (KeyValuePair<string, object> cimACLineSegmentPair in cimACLineSegments)
                {
                    FTN.ReactiveCapabilityCurve cimACLineSegment = cimACLineSegmentPair.Value as FTN.ReactiveCapabilityCurve;

                    ResourceDescription rd = CreateReactiveCapabilityCurveResourceDescription(cimACLineSegment);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("ReactiveCapabilityCurve ID = ").Append(cimACLineSegment.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("ReactiveCapabilityCurve ID = ").Append(cimACLineSegment.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private void ImportCurveData()
        {
            SortedDictionary<string, object> cimACLineSegments = concreteModel.GetAllObjectsOfType("FTN.CurveData");
            if (cimACLineSegments != null)
            {
                foreach (KeyValuePair<string, object> cimACLineSegmentPair in cimACLineSegments)
                {
                    FTN.CurveData cimACLineSegment = cimACLineSegmentPair.Value as FTN.CurveData;

                    ResourceDescription rd = CreateCurveDataResourceDescription(cimACLineSegment);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("CurveData ID = ").Append(cimACLineSegment.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("CurveData ID = ").Append(cimACLineSegment.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private void ImportRegulatingControl()
        {
            SortedDictionary<string, object> cimACLineSegments = concreteModel.GetAllObjectsOfType("FTN.RegulatingControl");
            if (cimACLineSegments != null)
            {
                foreach (KeyValuePair<string, object> cimACLineSegmentPair in cimACLineSegments)
                {
                    FTN.RegulatingControl cimACLineSegment = cimACLineSegmentPair.Value as FTN.RegulatingControl;

                    ResourceDescription rd = CreateRegulatingControlDescription(cimACLineSegment);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("RegulatingControl ID = ").Append(cimACLineSegment.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("RegulatingControl ID = ").Append(cimACLineSegment.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        #endregion

        #region Create 
        private ResourceDescription CreateFrequencyConvertertResourceDescription(FTN.FrequencyConverter frequencyConverter)
		{
			ResourceDescription resourceDescription = null;
			if(frequencyConverter != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0,(short)DMSType.FREQUENCYCONVERTER,importHelper.CheckOutIndexForDMSType(DMSType.FREQUENCYCONVERTER));	
				resourceDescription = new ResourceDescription(gid);
				importHelper.DefineIDMapping(frequencyConverter.ID, gid);
				PowerTransformerConverter.PopulateFrequencyConverterProperties(frequencyConverter, resourceDescription,importHelper,report);
			}
			return resourceDescription;
		}

        private ResourceDescription CreateShuntCompensatorResourceDescription(FTN.ShuntCompensator frequencyConverter)
        {
            ResourceDescription resourceDescription = null;
            if (frequencyConverter != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SHUNTCOMPENSATOR, importHelper.CheckOutIndexForDMSType(DMSType.SHUNTCOMPENSATOR));
                resourceDescription = new ResourceDescription(gid);
                importHelper.DefineIDMapping(frequencyConverter.ID, gid);
                PowerTransformerConverter.PopulateShuntCompensatorProperties(frequencyConverter, resourceDescription, importHelper, report);
            }
            return resourceDescription;
        }

        private ResourceDescription CreateStaticVarCompenasatorResourceDescription(FTN.StaticVarCompensator frequencyConverter)
        {
            ResourceDescription resourceDescription = null;
            if (frequencyConverter != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.STATICVARCOMPENSATOR, importHelper.CheckOutIndexForDMSType(DMSType.STATICVARCOMPENSATOR));
                resourceDescription = new ResourceDescription(gid);
                importHelper.DefineIDMapping(frequencyConverter.ID, gid);
                PowerTransformerConverter.PopulateStaticVarCompensatorProperties(frequencyConverter, resourceDescription, importHelper, report);
            }
            return resourceDescription;
        }

        private ResourceDescription CreateControlResourceDescription(FTN.Control frequencyConverter)
        {
            ResourceDescription resourceDescription = null;
            if (frequencyConverter != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.CONTROL, importHelper.CheckOutIndexForDMSType(DMSType.CONTROL));
                resourceDescription = new ResourceDescription(gid);
                importHelper.DefineIDMapping(frequencyConverter.ID, gid);
                PowerTransformerConverter.PopulateControlProperties(frequencyConverter, resourceDescription, importHelper, report);
            }
            return resourceDescription;
        }

        private ResourceDescription CreateTerminalResourceDescription(FTN.Terminal frequencyConverter)
        {
            ResourceDescription resourceDescription = null;
            if (frequencyConverter != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.TERMINAL, importHelper.CheckOutIndexForDMSType(DMSType.TERMINAL));
                resourceDescription = new ResourceDescription(gid);
                importHelper.DefineIDMapping(frequencyConverter.ID, gid);
                PowerTransformerConverter.PopulateTerminalProperties(frequencyConverter, resourceDescription, importHelper, report);
            }
            return resourceDescription;
        }

        private ResourceDescription CreateRegulatingControlDescription(FTN.RegulatingControl frequencyConverter)
        {
            ResourceDescription resourceDescription = null;
            if (frequencyConverter != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGULATINGCONTROL, importHelper.CheckOutIndexForDMSType(DMSType.REGULATINGCONTROL));
                resourceDescription = new ResourceDescription(gid);
                importHelper.DefineIDMapping(frequencyConverter.ID, gid);
                PowerTransformerConverter.PopulateRegulatingControlProperties(frequencyConverter, resourceDescription, importHelper, report);
            }
            return resourceDescription;
        }

        private ResourceDescription CreateSynchronousMachineResourceDescription(FTN.SynchronousMachine frequencyConverter)
        {
            ResourceDescription resourceDescription = null;
            if (frequencyConverter != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SYNCHRONOUSMACHINE, importHelper.CheckOutIndexForDMSType(DMSType.SYNCHRONOUSMACHINE));
                resourceDescription = new ResourceDescription(gid);
                importHelper.DefineIDMapping(frequencyConverter.ID, gid);
                PowerTransformerConverter.PopulateSynchronousMachineProperties(frequencyConverter, resourceDescription, importHelper, report);
            }
            return resourceDescription;
        }

        private ResourceDescription CreateReactiveCapabilityCurveResourceDescription(FTN.ReactiveCapabilityCurve frequencyConverter)
        {
            ResourceDescription resourceDescription = null;
            if (frequencyConverter != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REACTIVECAPABILITYCURVE, importHelper.CheckOutIndexForDMSType(DMSType.REACTIVECAPABILITYCURVE));
                resourceDescription = new ResourceDescription(gid);
                importHelper.DefineIDMapping(frequencyConverter.ID, gid);
                PowerTransformerConverter.PopulateReactiveCapabilityCurve(frequencyConverter, resourceDescription, importHelper, report);
            }
            return resourceDescription;
        }

        private ResourceDescription CreateCurveDataResourceDescription(FTN.CurveData frequencyConverter)
        {
            ResourceDescription resourceDescription = null;
            if (frequencyConverter != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.CURVEDATA, importHelper.CheckOutIndexForDMSType(DMSType.CURVEDATA));
                resourceDescription = new ResourceDescription(gid);
                importHelper.DefineIDMapping(frequencyConverter.ID, gid);
                PowerTransformerConverter.PopulateCurveDataProperties(frequencyConverter, resourceDescription, importHelper, report);
            }
            return resourceDescription;
        }


        #endregion 
       
    }
}

