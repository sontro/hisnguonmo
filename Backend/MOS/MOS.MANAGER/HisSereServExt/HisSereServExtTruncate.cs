using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisRestRetrType;
using MOS.MANAGER.HisSereServ;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOS.MANAGER.HisSereServExt
{
    partial class HisSereServExtTruncate : BusinessBase
    {
        internal HisSereServExtTruncate()
            : base()
        {

        }

        internal HisSereServExtTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServExtCheck checker = new HisSereServExtCheck(param);
                HIS_SERE_SERV_EXT raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    result = DAOWorker.HisSereServExtDAO.Truncate(raw);
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


        /// <summary>
        /// Hủy xử lý dịch vụ
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Truncate(HisSereServDeleteConfirmNoExcuteSDO data, ref HIS_SERE_SERV_EXT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HIS_SERE_SERV sereServ = null;
                HisSereServExtCheck checker = new HisSereServExtCheck(param);
                HisSereServCheck sschecker = new HisSereServCheck(param);
                HisSereServExtTruncateCheck truncateChecker = new HisSereServExtTruncateCheck(param);

                valid = valid && truncateChecker.VerifyRequireField(data);
                valid = valid && sschecker.VerifyId(data.SereServId, ref sereServ);
                HIS_SERE_SERV_EXT sereServExt = new HisSereServExtGet().GetBySereServId(sereServ.ID);

                valid = valid && checker.IsUnLock(sereServExt);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && truncateChecker.IsAllow(data, sereServExt, workPlace);

                if (valid)
                {
                    StringBuilder query = new StringBuilder("UPDATE HIS_SERE_SERV_EXT SET ");
                    List<object> listParam = new List<object>();
                    query.Append(" BEGIN_TIME = NULL");
                    query.Append(", END_TIME = NULL");
                    query.Append(", NOTE = NULL");
                    query.Append(", CONCLUDE = NULL");
                    query.Append(" WHERE ID = :param6");
                    listParam.Add(sereServExt.ID);

                    if (!DAOWorker.SqlDAO.Execute(query.ToString(), listParam.ToArray()))
                    {
                        throw new Exception("Update HisSereServExtDeleteSereServExt that bai. Ket thuc nghiep vu");
                    }

                    resultData = new HisSereServExtGet().GetById(sereServExt.ID);
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

        private void ProcessEventLog(HIS_SERE_SERV data, ref string eventLog, HisSereServDeleteConfirmNoExcuteSDO sdo)
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

        internal bool TruncateList(List<HIS_SERE_SERV_EXT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServExtCheck checker = new HisSereServExtCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisSereServExtDAO.TruncateList(listData);
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
    }
}
