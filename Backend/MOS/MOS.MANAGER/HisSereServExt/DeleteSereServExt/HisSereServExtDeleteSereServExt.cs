using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServExt.DeleteSereServExt
{
    class HisSereServExtDeleteSereServExt: BusinessBase
    {
        internal HisSereServExtDeleteSereServExt()
            : base()
        {
            this.Init();
        }

        internal HisSereServExtDeleteSereServExt(CommonParam paramDelete)
            : base(paramDelete)
        {
            this.Init();
        }

        private void Init()
        {
        }

        /// <summary>
        /// Hủy xử lý dịch vụ
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Run(HisSereServExtDeleteSereServExtSDO data, ref HIS_SERE_SERV_EXT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServCheck sschecker = new HisSereServCheck(param);
                HisSereServExtDeleteSereServExtCheck excuteChecker = new HisSereServExtDeleteSereServExtCheck(param);
                WorkPlaceSDO workPlace = null;
                HIS_SERE_SERV sereServ = null;
                HIS_SERE_SERV_EXT sereServExt = new HisSereServExtGet().GetBySereServId(data.SereServId);
                valid = valid && sschecker.VerifyId(data.SereServId, ref sereServ);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && excuteChecker.VerifyRequireField(data);
                valid = valid && excuteChecker.IsAllow(data, sereServExt, workPlace);
                
                if (valid)
                {
                    StringBuilder query = new StringBuilder("UPDATE HIS_SERE_SERV_EXT SET ");
                    query.Append(" BEGIN_TIME = NULL");
                    query.Append(", END_TIME = NULL");
                    query.Append(", NOTE = NULL");
                    query.Append(", CONCLUDE = NULL");

                    if (!DAOWorker.SqlDAO.Execute(query.ToString()))
                    {
                        throw new Exception("Update HisSereServExtDeleteSereServExt that bai. Ket thuc nghiep vu");
                    }

                    resultData = sereServExt;
                    result = true;

                    string eventLog = "";
                    ProcessEventLog(sereServ, ref eventLog, data);
                    new EventLogGenerator(EventLog.Enum.HisSereServExt_HuyXuLyDichVu, eventLog)
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

        private void ProcessEventLog(HIS_SERE_SERV data, ref string eventLog, HisSereServExtDeleteSereServExtSDO sdo)
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
