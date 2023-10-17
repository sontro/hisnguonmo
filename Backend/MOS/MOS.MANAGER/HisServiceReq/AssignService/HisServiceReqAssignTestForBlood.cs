using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MOS.LibraryEventLog;

namespace MOS.MANAGER.HisServiceReq.AssignService
{
    partial class HisServiceReqAssignTestForBlood : BusinessBase
    {
        private HisServiceReqAssignServiceCreate hisServiceReqAssignServiceCreate;

        internal HisServiceReqAssignTestForBlood()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqAssignTestForBlood(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqAssignServiceCreate = new HisServiceReqAssignServiceCreate(param);
        }

        internal bool Create(AssignTestForBloodSDO data, ref HisServiceReqListResultSDO resultData)
        {
            bool result = false;
            try
            {
                HIS_SERVICE_REQ serviceReq = null;
                if (this.Validate(data, ref serviceReq))
                {
                    AssignServiceSDO sdo = this.MakeData(data, serviceReq);
                    if (this.hisServiceReqAssignServiceCreate.Create(sdo, false, ref resultData))
                    {
                        HisServiceReqLog.Run(resultData.ServiceReqs, resultData.SereServs, LibraryEventLog.EventLog.Enum.HisServiceReq_ChiDinhXetNghiemTheoTuiMau);
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                resultData = null;
                result = false;
            }
            return result;
        }

        private AssignServiceSDO MakeData(AssignTestForBloodSDO data, HIS_SERVICE_REQ serviceReq)
        {
            AssignServiceSDO result = new AssignServiceSDO();
            result.IcdCode = serviceReq.ICD_CODE;
            result.IcdName = serviceReq.ICD_NAME;
            result.IcdSubCode = serviceReq.ICD_SUB_CODE;
            result.IcdText = serviceReq.ICD_TEXT;
            result.InstructionTimes = new List<long>() {data.InstructionTime};
            result.ParentServiceReqId = serviceReq.ID;
            result.RequestLoginName = serviceReq.REQUEST_LOGINNAME;
            result.RequestRoomId = serviceReq.REQUEST_ROOM_ID;
            result.RequestUserName = serviceReq.REQUEST_USERNAME;
            result.ServiceReqDetails = data.ServiceReqDetails;
            result.TreatmentId = serviceReq.TREATMENT_ID;
            return result;
        }

        private bool Validate(AssignTestForBloodSDO data, ref HIS_SERVICE_REQ serviceReq)
        {
            try
            {
                var tmp = new HisServiceReqGet().GetById(data.ServiceReqId);
                if (tmp == null || tmp.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONM)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("service_req null hoac ko phai don mau");
                    return false;
                }

                if (!IsNotNullOrEmpty(data.ServiceReqDetails))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("ServiceReqDetails null");
                    return false;
                }

                List<V_HIS_SERVICE> notTests = HisServiceCFG.DATA_VIEW.Where(o => data.ServiceReqDetails.Exists(t => t.ServiceId == o.ID) && o.SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN).ToList();

                if (IsNotNullOrEmpty(notTests))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Ton tai dich vu ko phai la xet nghiem." + LogUtil.TraceData("notTests", notTests));
                    return false;
                }

                serviceReq = tmp;
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal void RollbackData()
        {
            this.hisServiceReqAssignServiceCreate.RollbackData();
        }
    }
}
