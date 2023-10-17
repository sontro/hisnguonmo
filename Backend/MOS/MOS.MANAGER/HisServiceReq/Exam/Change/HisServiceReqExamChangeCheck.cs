using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.UTILITY;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisTreatment;

namespace MOS.MANAGER.HisServiceReq.Exam.Change
{
    partial class HisServiceReqExamChangeCheck : BusinessBase
    {
        internal HisServiceReqExamChangeCheck()
            : base()
        {
        }

        internal HisServiceReqExamChangeCheck(CommonParam param)
            : base(param)
        {
        }

        public bool IsValidData(HisServiceReqExamChangeSDO data, long serviceReqId, WorkPlaceSDO workPlaceSDO, ref HIS_SERVICE_REQ currentServiceReq)
        {
            try
            {
                //Kiem tra du lieu hien tai co hop le khong
                HIS_SERVICE_REQ serviceReq = new HisServiceReqGet().GetById(serviceReqId);

                if (currentServiceReq.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Chi cho phep thuc hien nghiep vu nay voi phieu y/c kham." + LogUtil.TraceData("currentServiceReq", currentServiceReq));
                }

                V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.ID == data.ServiceId).FirstOrDefault();
                if (service == null || service.SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Chi cho phep thuc hien nghiep vu nay voi dich vu kham." + LogUtil.TraceData("currentServiceReq", currentServiceReq));
                }

                if (workPlaceSDO == null || (currentServiceReq.EXECUTE_ROOM_ID != workPlaceSDO.RoomId && currentServiceReq.REQUEST_ROOM_ID != workPlaceSDO.RoomId))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChiChoPhepSuaPhieuChiDinhDoPhongMinhTao);
                    return false;
                }

                currentServiceReq = serviceReq;
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }
    }
}
