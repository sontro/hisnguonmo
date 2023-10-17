using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Ration
{
    public class HisServiceReqProcessor : BusinessBase
    {
        private HisServiceReqCreate hisServiceReqCreate;

        internal HisServiceReqProcessor()
            : base()
        {
            this.hisServiceReqCreate = new HisServiceReqCreate(param);
        }

        internal HisServiceReqProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisServiceReqCreate = new HisServiceReqCreate(param);
        }

        internal bool Run(HisRationServiceReqSDO data, List<HIS_TREATMENT> treatments, long reqDepartmentId, List<RationRequest> rationRequests, Dictionary<HIS_SERVICE_REQ, List<RationRequest>> SR_RATIONREQ_MAP, ref List<HIS_SERVICE_REQ> serviceReqs)
        {
            try
            {
                List<HIS_SERVICE_REQ> srs = this.MakeData(treatments, data, reqDepartmentId, rationRequests, SR_RATIONREQ_MAP);
                if (this.hisServiceReqCreate.CreateList(srs))
                {
                    serviceReqs = srs;
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        /// <summary>
        /// Tao du lieu HIS_SERVICE_REQ
        /// </summary>
        /// <param name="treatment"></param>
        /// <param name="data"></param>
        /// <param name="workPlace"></param>
        /// <param name="rationRequests"></param>
        /// <returns></returns>
        private List<HIS_SERVICE_REQ> MakeData(List<HIS_TREATMENT> treatments, HisRationServiceReqSDO data, long reqDepartmentId, List<RationRequest> rationRequests, Dictionary<HIS_SERVICE_REQ, List<RationRequest>> SR_RATIONREQ_MAP)
        {
            List<HIS_SERVICE_REQ> srs = new List<HIS_SERVICE_REQ>();
            List<V_HIS_SERVICE> splitServices = HisServiceCFG.DATA_VIEW
                .Where(o => rationRequests.Select(s => s.ServiceId).Contains(o.ID) && o.IS_SPLIT_SERVICE_REQ == Constant.IS_TRUE).ToList();

            foreach (HIS_TREATMENT treatment in treatments)
            {
                var groups = rationRequests.GroupBy(o => new { o.RationTimeId, o.RoomId, o.IntructionTime }).ToList();
                string sessionCode = Guid.NewGuid().ToString();

                foreach (var g in groups)
                {
                    List<RationRequest> splitReqs = g.Where(o => splitServices.Exists(e => e.ID == o.ServiceId)).ToList();
                    List<RationRequest> notSplitReqs = g.Where(o => splitReqs == null || !splitReqs.Exists(e => e.ServiceId == o.ServiceId)).ToList();
                    //if (g.Any(a => splitServices.Exists(o => o.ID == a.ServiceId)))
                    if (IsNotNullOrEmpty(splitReqs))
                    {
                        foreach (var rationReq in splitReqs)
                        {
                            if (rationReq.Amount > 1 && (rationReq.Amount % 1) == 0)
                            {
                                int count = (int)rationReq.Amount;

                                rationReq.Amount = 1;

                                //tach doi voi so luong 1
                                for (int i = 0; i < count; i++)
                                {
                                    this.MakeServiceReqData(data, treatment, sessionCode, reqDepartmentId, rationReq.RationTimeId, rationReq.RoomId, rationReq.IntructionTime, new List<RationRequest>() { rationReq }, srs, SR_RATIONREQ_MAP);
                                }
                            }
                            else if (rationReq.Amount == 1 && g.Count() > 1)
                            {
                                this.MakeServiceReqData(data, treatment, sessionCode, reqDepartmentId, rationReq.RationTimeId, rationReq.RoomId, rationReq.IntructionTime, new List<RationRequest>() { rationReq }, srs, SR_RATIONREQ_MAP);
                            }
                            else
                            {
                                this.MakeServiceReqData(data, treatment, sessionCode, reqDepartmentId, rationReq.RationTimeId, rationReq.RoomId, rationReq.IntructionTime, new List<RationRequest>() { rationReq }, srs, SR_RATIONREQ_MAP);
                            }
                        }
                    }
                    if (IsNotNullOrEmpty(notSplitReqs))
                    {
                        this.MakeServiceReqData(data, treatment, sessionCode, reqDepartmentId, g.Key.RationTimeId, g.Key.RoomId, g.Key.IntructionTime, notSplitReqs, srs, SR_RATIONREQ_MAP);
                    }
                }
            }
            return srs;
        }

        private void MakeServiceReqData(HisRationServiceReqSDO data, HIS_TREATMENT treatment, string sessionCode, long reqDepartmentId, long rationTimeId, long roomId, long intructionTime, List<RationRequest> rationReqs, List<HIS_SERVICE_REQ> srs, Dictionary<HIS_SERVICE_REQ, List<RationRequest>> SR_RATIONREQ_MAP)
        {
            HIS_SERVICE_REQ toInsert = new HIS_SERVICE_REQ();
            V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == roomId).FirstOrDefault();
            toInsert.SESSION_CODE = sessionCode;
            toInsert.EXECUTE_DEPARTMENT_ID = room.DEPARTMENT_ID;
            toInsert.EXECUTE_ROOM_ID = roomId;
            toInsert.RATION_TIME_ID = rationTimeId;
            toInsert.INTRUCTION_TIME = intructionTime;

            if (IsNotNull(data.TreatmentIds))
            {
                if (data.TreatmentIds.Count == 1)
                {
                    toInsert.ICD_NAME = data.IcdName;
                    toInsert.ICD_SUB_CODE = data.IcdSubCode;
                    toInsert.ICD_CODE = data.IcdCode;
                    toInsert.ICD_TEXT = data.IcdText;
                }
                else if (data.TreatmentIds.Count > 1)
                {
                    toInsert.ICD_NAME = treatment.ICD_NAME;
                    toInsert.ICD_SUB_CODE = treatment.ICD_SUB_CODE;
                    toInsert.ICD_CODE = treatment.ICD_CODE;
                    toInsert.ICD_TEXT = treatment.ICD_TEXT;
                }
            }
           
            toInsert.PARENT_ID = data.ParentServiceReqId;
            toInsert.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
            toInsert.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__AN;
            toInsert.TDL_PATIENT_ID = treatment.PATIENT_ID;
            toInsert.TREATMENT_ID = treatment.ID;
            toInsert.REQUEST_ROOM_ID = data.RequestRoomId;
            toInsert.REQUEST_DEPARTMENT_ID = reqDepartmentId;
            toInsert.REQUEST_LOGINNAME = data.RequestLoginName;
            toInsert.REQUEST_USERNAME = data.RequestUserName;
            toInsert.REQUEST_USER_TITLE = HisEmployeeUtil.GetTitle(data.RequestLoginName);
            toInsert.DESCRIPTION = data.Description;
            toInsert.TRACKING_ID = data.TrackingId;
            toInsert.TREATMENT_TYPE_ID = treatment.TDL_TREATMENT_TYPE_ID;
            toInsert.IS_FOR_HOMIE = data.IsForHomie ? (short?)Constant.IS_TRUE : null;
            if (data.IsForAutoCreateRation)
            {
                toInsert.IS_FOR_AUTO_CREATE_RATION = Constant.IS_TRUE;
            }
            HisServiceReqUtil.SetTdl(toInsert, treatment);
            srs.Add(toInsert);
            SR_RATIONREQ_MAP.Add(toInsert, rationReqs);
        }

        internal void Rollback()
        {
            this.hisServiceReqCreate.RollbackData();
        }
    }
}
