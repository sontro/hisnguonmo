using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServ.Update.AcceptNoExecute
{
    internal partial class HisSereServAcceptNoExecute : BusinessBase
    {
        public HisSereServAcceptNoExecute()
            : base()
        {
        }

        public HisSereServAcceptNoExecute(CommonParam param)
            : base(param)
        {
        }

        /// <summary>
        /// Xu ly nghiep vu "Dong y cho phep khong thuc hien dich vu"
        /// </summary>
        /// <param name="toUpdates"></param>
        /// <param name="resultData"></param>
        public void Accept(HisSereServAcceptNoExecuteSDO data, ref HIS_SERE_SERV resultData)
        {
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HIS_SERE_SERV sereServ = null;
                HIS_SERVICE_REQ serviceReq = null;

                HisSereServAcceptNoExecuteCheck checker = new HisSereServAcceptNoExecuteCheck(param);
                HisServiceReqCheck serviceReqChecker = new HisServiceReqCheck(param);

                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && checker.IsValidData(data, workPlace, ref sereServ);
                valid = valid && checker.IsNotAccepted(sereServ);
                valid = valid && sereServ != null && sereServ.SERVICE_REQ_ID.HasValue;
                valid = valid && serviceReqChecker.VerifyId(sereServ.SERVICE_REQ_ID.Value, ref serviceReq);
                valid = valid && checker.IsNotConfirmNoExcute(serviceReq,sereServ);
                if (valid)
                {
                    sereServ.IS_ACCEPTING_NO_EXECUTE = Constant.IS_TRUE;
                    sereServ.NO_EXECUTE_REASON = data.NoExecuteReason;

                    if (!DAOWorker.HisSereServDAO.Update(sereServ))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServ_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServ that bai.");
                    }
                    resultData = sereServ;

                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisSereServ_DongYChoPhepKhongThucHien, sereServ.TDL_SERVICE_CODE, sereServ.TDL_SERVICE_NAME)
                        .TreatmentCode(sereServ.TDL_TREATMENT_CODE)
                        .ServiceReqCode(sereServ.TDL_SERVICE_REQ_CODE)
                        .Run();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// Xu ly nghiep vu "Dong y cho phep khong thuc hien dich vu"
        /// </summary>
        /// <param name="toUpdates"></param>
        /// <param name="resultData"></param>
        public void Unaccept(HisSereServAcceptNoExecuteSDO data, ref HIS_SERE_SERV resultData)
        {
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HIS_SERE_SERV sereServ = null;
                HisSereServAcceptNoExecuteCheck checker = new HisSereServAcceptNoExecuteCheck(param);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && checker.IsValidData(data, workPlace, ref sereServ);
                valid = valid && checker.IsAccepted(sereServ);

                if (valid)
                {
                    sereServ.IS_ACCEPTING_NO_EXECUTE = null;

                    if (!DAOWorker.HisSereServDAO.Update(sereServ))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServ_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServ that bai.");
                    }
                    resultData = sereServ;

                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisSereServ_HuyDongYChoPhepKhongThucHien, sereServ.TDL_SERVICE_CODE, sereServ.TDL_SERVICE_NAME)
                        .TreatmentCode(sereServ.TDL_TREATMENT_CODE)
                        .ServiceReqCode(sereServ.TDL_SERVICE_REQ_CODE)
                        .Run();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
