using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ.DeleteConfirmNoExcute
{
    class HisSereServDeleteConfirmNoExcute: BusinessBase
    {
        internal HisSereServDeleteConfirmNoExcute()
            : base()
        {
            this.Init();
        }

        internal HisSereServDeleteConfirmNoExcute(CommonParam paramDelete)
            : base(paramDelete)
        {
            this.Init();
        }

        private void Init()
        {
        }

        /// <summary>
        /// Xác nhận không thực hiện
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Run(HisSereServDeleteConfirmNoExcuteSDO data, ref HIS_SERE_SERV resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_SERE_SERV sereServ = null;
                HisSereServCheck checker = new HisSereServCheck(param);
                HisSereServDeleteConfirmNoExcuteCheck excuteChecker = new HisSereServDeleteConfirmNoExcuteCheck(param);
                WorkPlaceSDO workPlace = null;

                valid = valid && excuteChecker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.SereServId, ref sereServ);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && excuteChecker.IsAllow(data, sereServ, workPlace);
                
                if (valid)
                {
                    StringBuilder query = new StringBuilder("UPDATE HIS_SERE_SERV SET ");
                    List<object> listParam = new List<object>();
                    query.Append(" IS_CONFIRM_NO_EXCUTE = NULL");
                    query.Append(", CONFIRM_NO_EXCUTE_REASON  = NULL");
                    query.Append(" WHERE ID = :param6");
                    listParam.Add(data.SereServId);

                    if (!DAOWorker.SqlDAO.Execute(query.ToString(), listParam.ToArray()))
                    {
                        throw new Exception("Update HisSereServDeleteConfirmNoExcute that bai. Ket thuc nghiep vu");
                    }

                    resultData = new HisSereServGet().GetById(data.SereServId);
                    result = true;

                    string eventLog = "";
                    ProcessEventLog(resultData, ref eventLog);
                    new EventLogGenerator(EventLog.Enum.HisSereServ_HuyXacNhanKhongThucHienDichVu, eventLog)
                            .TreatmentCode(sereServ.TDL_TREATMENT_CODE)
                            .ServiceReqCode(sereServ.TDL_SERVICE_REQ_CODE)
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

        private void ProcessEventLog(HIS_SERE_SERV data, ref string eventLog)
        {
            try
            {
                List<string> logs = new List<string>();
                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DichVu), data.TDL_SERVICE_NAME));
                eventLog = String.Join(". ", logs);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
