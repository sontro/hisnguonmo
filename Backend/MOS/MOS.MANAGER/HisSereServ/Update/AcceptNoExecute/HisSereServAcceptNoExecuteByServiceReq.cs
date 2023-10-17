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
    internal partial class HisSereServAcceptNoExecuteByServiceReq : BusinessBase
    {
        public HisSereServAcceptNoExecuteByServiceReq()
            : base()
        {
        }

        public HisSereServAcceptNoExecuteByServiceReq(CommonParam param)
            : base(param)
        {
        }

        /// <summary>
        /// Xu ly nghiep vu "Dong y cho phep khong thuc hien dich vu" toan bo phieu chi dinh
        /// </summary>
        /// <param name="toUpdates"></param>
        /// <param name="resultData"></param>
        public void Accept(HisServiceReqAcceptNoExecuteSDO data, ref HIS_SERVICE_REQ resultData)
        {
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HIS_SERVICE_REQ serviceReq = null;

                HisSereServAcceptNoExecuteCheck checker = new HisSereServAcceptNoExecuteCheck(param);
                HisServiceReqCheck serviceReqChecker = new HisServiceReqCheck(param);

                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && checker.IsValidData(data, workPlace, ref serviceReq);
                valid = valid && serviceReqChecker.IsNotStarted(serviceReq);
                if (valid)
                {
                    serviceReq.IS_ACCEPTING_NO_EXECUTE = Constant.IS_TRUE;

                    string sqlUpdateServiceReq = string.Format("UPDATE HIS_SERVICE_REQ SET IS_ACCEPTING_NO_EXECUTE = 1 WHERE ID = {0}", data.ServiceReqId);

                    string sqlUpdateSereServ = string.Format("UPDATE HIS_SERE_SERV SET IS_ACCEPTING_NO_EXECUTE = 1 ");
                    if (string.IsNullOrWhiteSpace(data.NoExecuteReason))
                    {
                        sqlUpdateSereServ += string.Format(", NO_EXECUTE_REASON = NULL ");
                    }
                    else
                    {
                        sqlUpdateSereServ += string.Format(", NO_EXECUTE_REASON = '{0}' ", data.NoExecuteReason);
                    }
                    sqlUpdateSereServ += string.Format("WHERE SERVICE_REQ_ID = {0}", data.ServiceReqId);

                    List<string> sqls = new List<string>(){
                        sqlUpdateServiceReq,
                        sqlUpdateSereServ
                    };
                    if (!DAOWorker.SqlDAO.Execute(sqls))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServ_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServ, his_service_req that bai.");
                    }

                    resultData = serviceReq;
                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisServiceReq_DongYChoPhepKhongThucHien)
                        .TreatmentCode(serviceReq.TDL_TREATMENT_CODE)
                        .ServiceReqCode(serviceReq.SERVICE_REQ_CODE)
                        .Run();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// Xu ly nghiep vu "Huy dong y cho phep khong thuc hien dich vu" toan bo phieu chi dinh
        /// </summary>
        /// <param name="toUpdates"></param>
        /// <param name="resultData"></param>
        public void Unaccept(HisServiceReqAcceptNoExecuteSDO data, ref HIS_SERVICE_REQ resultData)
        {
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HIS_SERVICE_REQ serviceReq = null;
                HisSereServAcceptNoExecuteCheck checker = new HisSereServAcceptNoExecuteCheck(param);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && checker.IsValidData(data, workPlace, ref serviceReq);

                if (valid)
                {
                    serviceReq.IS_ACCEPTING_NO_EXECUTE = null;

                    string sqlUpdateServiceReq = string.Format("UPDATE HIS_SERVICE_REQ SET IS_ACCEPTING_NO_EXECUTE = NULL WHERE ID = {0}", data.ServiceReqId);
                    string sqlUpdateSereServ = string.Format("UPDATE HIS_SERE_SERV SET IS_ACCEPTING_NO_EXECUTE = NULL, NO_EXECUTE_REASON = NULL WHERE SERVICE_REQ_ID = {0}", data.ServiceReqId);

                    List<string> sqls = new List<string>(){
                        sqlUpdateServiceReq,
                        sqlUpdateSereServ
                    };

                    if (!DAOWorker.SqlDAO.Execute(sqls))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServ_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServ, his_service_req that bai.");
                    }

                    resultData = serviceReq;

                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisServiceReq_HuyDongYChoPhepKhongThucHien)
                        .TreatmentCode(serviceReq.TDL_TREATMENT_CODE)
                        .ServiceReqCode(serviceReq.SERVICE_REQ_CODE)
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
