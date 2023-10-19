using Inventec.Common.Logging;
using ACS.EFMODEL.DataModels;
using ACS.LibraryEventLog;
using ACS.LogManager;
using ACS.MANAGER.Base;
//using ACS.MANAGER.Config;
using ACS.SDO;
using ACS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.MANAGER.EventLogUtil
{
    public class HisServiceReqLog
    {
        public static void Run(List<V_HIS_SERVICE_REQ> serviceReqs, List<V_HIS_SERE_SERV> sereServs, EventLog.Enum logEnum)
        {
            try
            {
                string treatmentCode = serviceReqs != null && serviceReqs.Count > 0 ? serviceReqs[0].TREATMENT_CODE : null;
                new EventLogGenerator(logEnum).TreatmentCode(treatmentCode).ServiceReqList(GetServiceReqData(serviceReqs, sereServs)).Run();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        public static void Run(V_HIS_SERVICE_REQ serviceReq, List<V_HIS_SERE_SERV> sereServs, EventLog.Enum logEnum)
        {
            Run(new List<V_HIS_SERVICE_REQ>() { serviceReq }, sereServs, logEnum);
        }

        public static void Run(HIS_SERVICE_REQ oldServiceReq, List<HIS_SERE_SERV> oldSereServs, V_HIS_SERVICE_REQ newServiceReq, List<V_HIS_SERE_SERV> newSereServs, EventLog.Enum logEnum)
        {
            try
            {
                string treatmentCode = newServiceReq != null ? newServiceReq.TREATMENT_CODE : null;
                List<string> serviceNames = newSereServs != null && newSereServs.Count > 0 ? newSereServs.Select(o => o.TDL_SERVICE_NAME).ToList() : null;
                List<string> oldServiceNames = oldSereServs != null && oldSereServs.Count > 0 ? oldSereServs.Select(o => o.TDL_SERVICE_NAME).ToList() : null;

                new EventLogGenerator(logEnum)
                    .TreatmentCode(treatmentCode)
                    .ServiceReqData(new ApplicationRoleData(oldServiceReq.SERVICE_REQ_CODE, oldServiceNames))
                    .ServiceReqData1(new ApplicationRoleData(newServiceReq.SERVICE_REQ_CODE, serviceNames))
                    .Run();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        internal static void Run(List<HIS_SERVICE_REQ> serviceReqs, List<HIS_EXP_MEST> expMests,
            List<HIS_SERVICE_REQ_MATY> serviceReqMaties, List<HIS_SERVICE_REQ_METY> serviceReqMeties,
            List<HIS_EXP_MEST_MATERIAL> materials, List<HIS_EXP_MEST_MEDICINE> medicines, EventLog.Enum logEnum)
        {
            try
            {
                string treatmentCode = serviceReqs != null ? serviceReqs[0].TDL_TREATMENT_CODE : null;

                List<ApplicationRoleData> serviceReqList = new List<ApplicationRoleData>();

                foreach (HIS_SERVICE_REQ req in serviceReqs)
                {
                    List<string> reqData = new List<string>();
                    List<string> expMestCodes = expMests != null ? expMests.Where(o => o.SERVICE_REQ_ID == req.ID).Select(o => o.EXP_MEST_CODE).ToList() : null;
                    if (expMestCodes != null)
                    {
                        string expMest = LogCommonUtil.GetEventLogContent(EventLog.Enum.MaXuat);
                        expMestCodes[0] = expMest + " " + expMestCodes[0];
                        reqData.AddRange(expMestCodes);
                    }

                    List<string> outItems = new List<string>();
                    List<string> inItems = new List<string>();

                    if (serviceReqMaties != null && serviceReqMaties.Count > 0)
                    {
                        List<string> tmp = serviceReqMaties.Where(o => o.SERVICE_REQ_ID == req.ID).Select(o => o.MATERIAL_TYPE_NAME).ToList();
                        if (tmp != null && tmp.Count > 0)
                        {
                            outItems.AddRange(tmp);
                        }
                    }
                    if (serviceReqMeties != null && serviceReqMeties.Count > 0)
                    {
                        List<string> tmp = serviceReqMeties.Where(o => o.SERVICE_REQ_ID == req.ID).Select(o => o.MEDICINE_TYPE_NAME).ToList();
                        if (tmp != null && tmp.Count > 0)
                        {
                            outItems.AddRange(tmp);
                        }
                    }
                    if (materials != null && materials.Count > 0)
                    {
                        List<long> materialTypeIds = materials.Where(o => o.TDL_SERVICE_REQ_ID == req.ID).Select(o => o.TDL_MATERIAL_TYPE_ID).ToList();
                        if (materialTypeIds != null && materialTypeIds.Count > 0)
                        {
                            List<string> tmp = HisMaterialTypeCFG.DATA.Where(o => materialTypeIds.Contains(o.ID)).Select(o => o.MATERIAL_TYPE_NAME).ToList();
                            inItems.AddRange(tmp);
                        }
                    }

                    if (medicines != null && medicines.Count > 0)
                    {
                        List<long> medicineTypeIds = medicines.Where(o => o.TDL_SERVICE_REQ_ID == req.ID).Select(o => o.TDL_MEDICINE_TYPE_ID).ToList();
                        if (medicineTypeIds != null && medicineTypeIds.Count > 0)
                        {
                            List<string> tmp = HisMedicineTypeCFG.DATA.Where(o => medicineTypeIds.Contains(o.ID)).Select(o => o.MEDICINE_TYPE_NAME).ToList();
                            inItems.AddRange(tmp);
                        }
                    }

                    if (outItems != null && outItems.Count > 0)
                    {
                        string outItemContent = LogCommonUtil.GetEventLogContent(EventLog.Enum.MuaNgoai);
                        outItems[0] = outItemContent + " " + outItems[0];
                        reqData.AddRange(outItems);
                    }
                    if (inItems != null && inItems.Count > 0)
                    {
                        string inItemContent = LogCommonUtil.GetEventLogContent(EventLog.Enum.TrongKho);
                        inItems[0] = inItemContent + " " + inItems[0];
                        reqData.AddRange(inItems);
                    }
                    serviceReqList.Add(new ApplicationRoleData(req.SERVICE_REQ_CODE, reqData));
                }

                new EventLogGenerator(logEnum)
                    .TreatmentCode(treatmentCode)
                    .ServiceReqList(serviceReqList)
                    .Run();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        internal static List<ApplicationRoleData> GetServiceReqData(List<V_HIS_SERVICE_REQ> serviceReqs, List<V_HIS_SERE_SERV> sereServs)
        {
            try
            {
                List<ApplicationRoleData> serviceReqData = new List<ApplicationRoleData>();
                foreach (V_HIS_SERVICE_REQ req in serviceReqs)
                {
                    List<string> serviceNames = sereServs != null && sereServs.Count > 0 ?
                        sereServs.Where(o => o.SERVICE_REQ_ID == req.ID).Select(o => o.TDL_SERVICE_NAME).ToList() : null;
                    if (serviceNames != null)
                    {
                        serviceReqData.Add(new ApplicationRoleData(req.SERVICE_REQ_CODE, serviceNames));
                    }
                }
                return serviceReqData;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

    }
}
