using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisRegisterGate
{
    class HisRegisterGateIssueOrderNumber : BusinessBase
    {

        internal HisRegisterGateIssueOrderNumber()
            : base()
        {

        }

        internal HisRegisterGateIssueOrderNumber(CommonParam param)
            : base(param)
        {

        }

        internal IssueOrderNumberSDO Run(long departmentId)
        {
            IssueOrderNumberSDO result = null;
            try
            {

                string sqlQuery = "SELECT GATE.* FROM HIS_REGISTER_GATE GATE WHERE GATE.IS_ACTIVE = 1 AND GATE.DEPARTMENT_ID = :param1 AND (GATE.WAITING_LIMIT IS NULL OR (GATE.WAITING_LIMIT IS NOT NULL AND (NVL(GATE.CURRENT_ISSUED_NUMBER, 0) - NVL(GATE.CURRENT_CALLED_NUMBER, 0) < GATE.WAITING_LIMIT)))";
                List<HIS_REGISTER_GATE> avails = DAOWorker.SqlDAO.GetSql<HIS_REGISTER_GATE>(sqlQuery, departmentId);
                DateTime dt = DateTime.Now;

                avails = avails != null ? avails.Where(o => this.IsInTime(dt, o.HOUR_FROM, o.HOUR_TO, o.MINUTE_FROM, o.MINUTE_TO)).ToList() : null;

                if (!IsNotNullOrEmpty(avails))
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisRegisterGate_KhongCoQuayNaoKhaDung);
                    return null;
                }


                avails = avails.OrderByDescending(o => o.WAITING_LIMIT.HasValue).ThenBy(t => (t.CURRENT_ISSUED_NUMBER ?? 0) - (t.CURRENT_CALLED_NUMBER ?? 0)).ThenBy(tb => tb.PRIORITY ?? 9999999999).ToList();
                HIS_REGISTER_GATE gate = avails.FirstOrDefault();

                string storedSql = "PKG_UPDATE_REGISTER_NUMBER.PRO_EXECUTE";

                OracleParameter P_REGISTER_GATE_ID = new OracleParameter("P_REGISTER_GATE_ID", OracleDbType.Int64, gate.ID, ParameterDirection.Input);
                OracleParameter P_RESULT = new OracleParameter("P_RESULT", OracleDbType.Int64, ParameterDirection.Output);

                object resultHolder = null;

                if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, P_REGISTER_GATE_ID, P_RESULT))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisRegisterGate_SinhSTTTiepDonThatBai);
                    throw new Exception("Sinh stt tiep don that bai");
                }

                if (resultHolder != null)
                {

                    RegisterGateResultHolder rs = (RegisterGateResultHolder)resultHolder;
                    if (rs.OrderNumber.HasValue && rs.OrderNumber.Value > 0)
                    {
                        result = new IssueOrderNumberSDO();
                        HIS_DEPARTMENT depart = HisDepartmentCFG.DATA.FirstOrDefault(o => o.ID == departmentId);
                        result.DepartmentName = depart != null ? depart.DEPARTMENT_NAME : "";
                        result.OrderNumber = rs.OrderNumber.Value;
                        result.RegisterGateAddress = gate.ADDRESS;
                        result.RegisterGateName = gate.REGISTER_GATE_NAME;
                    }
                    else
                    {
                        LogSystem.Error("Execute Store failed: " + LogUtil.TraceData("ResultHolder", rs));
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }
            return result;
        }

        //Xu ly ket qua tra ve khi goi procedure
        private void OutputHandler(ref object resultHolder, OracleDataReader dataReader, params OracleParameter[] parameters)
        {
            try
            {
                RegisterGateResultHolder rs = new RegisterGateResultHolder();
                if (parameters[1] != null && parameters[1].Value != null && parameters[1].Value != DBNull.Value)
                {
                    rs.OrderNumber = Int64.Parse(parameters[1].Value.ToString());
                }

                resultHolder = rs;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        bool IsInTime(DateTime dt, long? from_hour, long? to_hour, long? from_minute, long? to_minute)
        {
            try
            {
                if (from_hour.HasValue && dt.Hour < from_hour.Value)
                {
                    return false;
                }

                if (to_hour.HasValue && dt.Hour > to_hour.Value)
                {
                    return false;
                }

                if (from_minute.HasValue && dt.Minute < from_minute.Value)
                {
                    return false;
                }

                if (to_minute.HasValue && dt.Minute > to_minute.Value)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }
    }

    class RegisterGateResultHolder
    {
        public long? OrderNumber { get; set; }
    }
}
