using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using MOS.MANAGER.HisServiceFollow;
using MOS.MANAGER.HisServiceReq.AssignService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Blood
{
    class HisServiceReqBloodPresUltil : BusinessBase
    {
        internal static bool AssignServiceForTestBlood(HisServiceReqAssignServiceCreate processor, List<HIS_EXP_MEST_BLTY_REQ> hisExpMestBltyReqs, HIS_SERVICE_REQ serviceReq)
        {
            bool valid = true;
            try
            {
                if (hisExpMestBltyReqs != null && hisExpMestBltyReqs.Count > 0 && processor != null && serviceReq != null)
                {
                    List<long> unValidServiceTypeIds = new List<long>()
                {
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__MAU,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__G,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__AN
                };

                    List<ServiceReqDetailSDO> serviceReqDetails = new List<ServiceReqDetailSDO>();
                    foreach (HIS_EXP_MEST_BLTY_REQ blty in hisExpMestBltyReqs)
                    {
                        HIS_BLOOD_TYPE bloodType = HisBloodTypeCFG.DATA.FirstOrDefault(o => o.ID == blty.BLOOD_TYPE_ID);
                        if (bloodType != null)
                        {
                            HisServiceFollowViewFilterQuery filter = new HisServiceFollowViewFilterQuery();
                            filter.SERVICE_ID = bloodType.SERVICE_ID;

                            List<V_HIS_SERVICE_FOLLOW> flows = new HisServiceFollowGet().GetView(filter);
                            flows = flows != null && flows.Count > 0 ? flows.Where(o => !unValidServiceTypeIds.Contains(o.FOLLOW_TYPE_ID) 
                                                                                    && (!o.CONDITIONED_AMOUNT.HasValue || o.CONDITIONED_AMOUNT.Value == blty.AMOUNT)
                                                                                    && (o.TREATMENT_TYPE_IDS == null || MOS.UTILITY.CommonUtil.IsListIdsContainsId(o.TREATMENT_TYPE_IDS, serviceReq.TDL_TREATMENT_TYPE_ID ?? 0))).ToList() : null;
                            if (flows != null && flows.Count > 0)
                            {
                                foreach (var f in flows)
                                {
                                    ServiceReqDetailSDO serviceReqDetail = new ServiceReqDetailSDO();
                                    serviceReqDetail.ServiceId = f.FOLLOW_ID;
                                    if (f.CONDITIONED_AMOUNT.HasValue)
                                    {
                                        serviceReqDetail.Amount = f.AMOUNT;
                                    }
                                    else
                                    {
                                        serviceReqDetail.Amount = blty.AMOUNT * f.AMOUNT;
                                    }
                                    if (blty.PATIENT_TYPE_ID.HasValue)
                                    {
                                        serviceReqDetail.PatientTypeId = blty.PATIENT_TYPE_ID.Value;
                                    }
                                    serviceReqDetail.IsExpend = f.IS_EXPEND;
                                    serviceReqDetails.Add(serviceReqDetail);
                                }
                            }
                        }
                    }

                    AssignServiceSDO assignSDO = null;
                    if (serviceReqDetails != null && serviceReqDetails.Count > 0)
                    {
                        assignSDO = new AssignServiceSDO();
                        assignSDO.IcdCode = serviceReq.ICD_CODE;
                        assignSDO.IcdName = serviceReq.ICD_NAME;
                        assignSDO.IcdSubCode = serviceReq.ICD_SUB_CODE;
                        assignSDO.IcdText = serviceReq.ICD_TEXT;
                        assignSDO.InstructionTimes = new List<long>() { serviceReq.INTRUCTION_TIME };
                        assignSDO.ParentServiceReqId = serviceReq.ID;
                        assignSDO.RequestLoginName = serviceReq.REQUEST_LOGINNAME;
                        assignSDO.RequestRoomId = serviceReq.REQUEST_ROOM_ID;
                        assignSDO.RequestUserName = serviceReq.REQUEST_USERNAME;
                        assignSDO.ServiceReqDetails = serviceReqDetails;
                        assignSDO.TreatmentId = serviceReq.TREATMENT_ID;
                    }
                    if (assignSDO != null)
                    {
                        HisServiceReqListResultSDO resultData = null;
                        if (!processor.Create(assignSDO, false, ref resultData))
                        {
                            throw new Exception("Chi dinh dich vu ki thuat cho don mau that bai. Rollback");
                        }
                        valid = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
            return valid;
        }
    }
}
