using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTracking
{
    class HisTrackingGetSql : BusinessBase
    {
        internal HisTrackingGetSql()
            : base()
        {

        }

        internal HisTrackingGetSql(CommonParam paramGet)
            : base(paramGet)
        {

        }


        internal List<HisTrackingTDO> GetForEmr(HisTrackingForEmrFilter filter)
        {
            List<HisTrackingTDO> result = null;
            try
            {
                if (filter.TREATMENT_ID.HasValue || !String.IsNullOrWhiteSpace(filter.TREATMENT_CODE__EXACT))
                {
                    long treatmentId = 0;

                    if (!String.IsNullOrWhiteSpace(filter.TREATMENT_CODE__EXACT))
                    {
                        treatmentId = DAOWorker.SqlDAO.GetSqlSingle<long>("SELECT ID FROM HIS_TREATMENT WHERE TREATMENT_CODE = :param1", filter.TREATMENT_CODE__EXACT);
                    }
                    else
                    {
                        treatmentId = filter.TREATMENT_ID.Value;
                    }

                    if (treatmentId > 0)
                    {
                        result = new List<HisTrackingTDO>();

                        List<HIS_TRACKING> trackings = new HisTrackingGet().GetByTreatmentId(treatmentId);
                        List<HIS_SERVICE_REQ> serviceReqs = null;
                        List<HIS_SERE_SERV> sereServs = null;
                        List<HIS_EXP_MEST_MEDICINE> medicines = null;
                        List<HIS_EXP_MEST_MATERIAL> materials = null;

                        if (IsNotNullOrEmpty(trackings))
                        {
                            HisServiceReqFilterQuery serviceReqFilter = new HisServiceReqFilterQuery();
                            serviceReqFilter.TREATMENT_ID = treatmentId;
                            serviceReqFilter.HAS_TRACKING_ID = true;
                            serviceReqFilter.IS_NO_EXECUTE = false;
                            serviceReqs = new HisServiceReqGet().Get(serviceReqFilter);

                            if (IsNotNullOrEmpty(serviceReqs))
                            {
                                HisSereServFilterQuery ssFilter = new HisSereServFilterQuery();
                                ssFilter.TREATMENT_ID = treatmentId;
                                ssFilter.HAS_EXECUTE = true;
                                ssFilter.SERVICE_REQ_IDs = serviceReqs.Select(s => s.ID).ToList();
                                sereServs = new HisSereServGet().Get(ssFilter);

                                HisExpMestMedicineFilterQuery mediFilter = new HisExpMestMedicineFilterQuery();
                                mediFilter.TDL_TREATMENT_ID = treatmentId;
                                mediFilter.TDL_SERVICE_REQ_IDs = serviceReqs.Select(s => s.ID).ToList();
                                medicines = new HisExpMestMedicineGet().Get(mediFilter);

                                HisExpMestMaterialFilterQuery mateFilter = new HisExpMestMaterialFilterQuery();
                                mateFilter.TDL_TREATMENT_ID = treatmentId;
                                mateFilter.TDL_SERVICE_REQ_IDs = serviceReqs.Select(s => s.ID).ToList();
                                materials = new HisExpMestMaterialGet().Get(mateFilter);
                            }
                        }

                        if (IsNotNullOrEmpty(trackings))
                        {
                            foreach (HIS_TRACKING track in trackings)
                            {
                                HisTrackingTDO trackTDO = new HisTrackingTDO();
                                trackTDO.CareInstruction = track.CARE_INSTRUCTION;
                                trackTDO.Content = track.CONTENT;
                                trackTDO.CreateLoginname = track.CREATOR;
                                trackTDO.GeneralExpression = track.GENERAL_EXPRESSION;
                                trackTDO.IcdCode = track.ICD_CODE;
                                trackTDO.IcdName = track.ICD_NAME;
                                trackTDO.IcdSubCode = track.ICD_SUB_CODE;
                                trackTDO.IcdText = track.ICD_TEXT;
                                trackTDO.MedicalInstruction = track.MEDICAL_INSTRUCTION;
                                trackTDO.SheetOrder = track.SHEET_ORDER;
                                trackTDO.SubclinicalProcesses = track.SUBCLINICAL_PROCESSES;
                                trackTDO.TrackingId = track.ID;
                                trackTDO.TrackingTime = track.TRACKING_TIME;
                                trackTDO.RehabilitationContent = track.REHABILITATION_CONTENT;
                                ProcessServiceReq(serviceReqs, sereServs, medicines, materials, track, ref trackTDO);
                                result.Add(trackTDO);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }
            return result;
        }

        private void ProcessServiceReq(List<HIS_SERVICE_REQ> serviceReqs, List<HIS_SERE_SERV> sereServs, List<HIS_EXP_MEST_MEDICINE> medicines, List<HIS_EXP_MEST_MATERIAL> materials, HIS_TRACKING track, ref HisTrackingTDO trackTDO)
        {
            List<HIS_SERVICE_REQ> reqs = serviceReqs != null ? serviceReqs.Where(o => o.TRACKING_ID == track.ID).ToList() : null;
            if (IsNotNullOrEmpty(reqs))
            {
                trackTDO.ServiceReqs = new List<HisServiceReqTDO>();
                foreach (HIS_SERVICE_REQ req in reqs)
                {
                    HisServiceReqTDO serviceReqTDO = new HisServiceReqTDO();
                    serviceReqTDO.Description = req.DESCRIPTION;
                    serviceReqTDO.IcdCode = req.ICD_CODE;
                    serviceReqTDO.IcdName = req.ICD_NAME;
                    serviceReqTDO.IcdSubCode = req.ICD_SUB_CODE;
                    serviceReqTDO.IcdText = req.ICD_TEXT;
                    serviceReqTDO.InstructionTime = req.INTRUCTION_TIME;
                    serviceReqTDO.Note = req.NOTE;
                    serviceReqTDO.ServiceReqCode = req.SERVICE_REQ_CODE;
                    serviceReqTDO.ServiceReqTypeId = req.SERVICE_REQ_TYPE_ID;
                    serviceReqTDO.TreatmentCode = req.TDL_TREATMENT_CODE;
                    trackTDO.ServiceReqs.Add(serviceReqTDO);


                    ProcessSereServ(sereServs, medicines, materials, req, ref serviceReqTDO);
                }
            }
        }

        private void ProcessSereServ(List<HIS_SERE_SERV> sereServs, List<HIS_EXP_MEST_MEDICINE> medicines, List<HIS_EXP_MEST_MATERIAL> materials, HIS_SERVICE_REQ req, ref HisServiceReqTDO serviceReqTDO)
        {
            List<HIS_SERE_SERV> services = sereServs != null ? sereServs.Where(o => o.SERVICE_REQ_ID == req.ID).ToList() : null;

            if (IsNotNullOrEmpty(services))
            {
                serviceReqTDO.Services = new List<HisSereServTDO>();
                foreach (HIS_SERE_SERV ss in services)
                {
                    HisSereServTDO ssTDO = new HisSereServTDO();
                    ssTDO.SereServId = ss.ID;
                    ssTDO.ServiceCode = ss.TDL_SERVICE_CODE;
                    ssTDO.ServiceName = ss.TDL_SERVICE_NAME;
                    ssTDO.ServiceReqCode = ss.TDL_SERVICE_REQ_CODE;
                    ssTDO.ServiceTypeId = ss.TDL_SERVICE_TYPE_ID;
                    if (ss.MEDICINE_ID.HasValue)
                    {
                        HIS_EXP_MEST_MEDICINE m = medicines != null ? medicines.FirstOrDefault(o => o.TDL_SERVICE_REQ_ID == req.ID && o.MEDICINE_ID == ss.MEDICINE_ID.Value) : null;
                        ssTDO.Tutorial = m != null ? m.TUTORIAL : "";
                    }
                    else if (ss.MATERIAL_ID.HasValue)
                    {
                        HIS_EXP_MEST_MATERIAL m = materials != null ? materials.FirstOrDefault(o => o.TDL_SERVICE_REQ_ID == req.ID && o.MATERIAL_ID == ss.MATERIAL_ID.Value) : null;
                        ssTDO.Tutorial = m != null ? m.TUTORIAL : "";
                    }
                    serviceReqTDO.Services.Add(ssTDO);
                }
            }
        }
    }
}
