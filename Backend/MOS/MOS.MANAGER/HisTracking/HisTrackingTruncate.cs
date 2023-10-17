using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisCare;
using MOS.MANAGER.HisDhst;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTracking
{
    partial class HisTrackingTruncate : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisDhstUpdate hisDhstUpdate;
        private HisCareUpdate hisCareUpdate;

        internal HisTrackingTruncate()
            : base()
        {
            this.Init();
        }

        internal HisTrackingTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisDhstUpdate = new HisDhstUpdate(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisCareUpdate = new HisCareUpdate(param);
        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTrackingCheck checker = new HisTrackingCheck(param);
                HisServiceReqCheck serviceReqCheck = new HisServiceReqCheck(param);
                HIS_TRACKING raw = null;
                List<HIS_SERVICE_REQ> serviceReqs = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsAllowCreateOrEdit(raw);
                if (valid)
                {
                    this.ProcessCare(raw.ID);
                    this.ProcessDhst(raw.ID);
                    this.ProcessServiceReq(raw.ID, ref serviceReqs);
                    result = DAOWorker.HisTrackingDAO.Truncate(raw);

                    HIS_TREATMENT treat = new HisTreatmentGet().GetById(raw.TREATMENT_ID);
                    string eventLog = "";
                    ProcessEventLog(raw, ref eventLog, serviceReqs);
                    new EventLogGenerator(EventLog.Enum.HisTracking_XoaToDieuTri, eventLog)
                        .TreatmentCode(treat.TREATMENT_CODE)
                        .Run();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessCare(long trackingId)
        {
            List<HIS_CARE> cares = new HisCareGet().GetByTrackingId(trackingId);
            if (IsNotNullOrEmpty(cares))
            {
                cares.ForEach(o => o.TRACKING_ID = null);
                if (!this.hisCareUpdate.UpdateList(cares))
                {
                    throw new Exception("Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessDhst(long trackingId)
        {
            List<HIS_DHST> dhsts = new HisDhstGet().GetByTrackingId(trackingId);
            if (IsNotNullOrEmpty(dhsts))
            {
                dhsts.ForEach(o => o.TRACKING_ID = null);
                if (!this.hisDhstUpdate.UpdateList(dhsts))
                {
                    throw new Exception("Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessServiceReq(long trackingId, ref List<HIS_SERVICE_REQ> serviceReqs)
        {
            serviceReqs = new HisServiceReqGet().GetByTrackingIdOrUsedForTrackingId(trackingId);
            if (IsNotNullOrEmpty(serviceReqs))
            {
                // Xoa thong tin tracking and usedfortracking trong y lenh
                foreach (var req in serviceReqs)
                {
                    if (req.TRACKING_ID == trackingId)
                    {
                        req.TRACKING_ID = null;
                    }
                    if (req.USED_FOR_TRACKING_ID == trackingId)
                    {
                        req.USED_FOR_TRACKING_ID = null;
                    }
                }

                if (!this.hisServiceReqUpdate.UpdateList(serviceReqs))
                {
                    throw new Exception("Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessEventLog(HIS_TRACKING data, ref string eventLog, List<HIS_SERVICE_REQ> serviceReqs)
        {
            try
            {
                List<string> editFields = new List<string>();

                string newValue = Inventec.Common.DateTime.Convert.TimeNumberToTimeStringWithoutSecond(data.TRACKING_TIME);
                string fieldNameTime = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThoiGian);
                editFields.Add(String.Format("{0}: {1}", fieldNameTime, newValue));

                editFields.Add(String.Format("ICD: ({0}){1} - ({2}){3}", data.ICD_CODE, data.ICD_NAME, data.ICD_SUB_CODE, data.ICD_TEXT));

                if (IsNotNullOrEmpty(serviceReqs))
                {
                    editFields.Add(String.Format("Mã y lệnh: {0}", String.Join(",", serviceReqs.Select(o => o.SERVICE_REQ_CODE))));
                }

                eventLog = String.Join(". ", editFields);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
