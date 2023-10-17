using Inventec.Common.Logging;
using Inventec.Core;
using MOS.DAO.Sql;
using MOS.MANAGER.Base;
using MOS.OracleUDT;
using MOS.UTILITY;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.ChangePatient
{
    class ResultHolder
    {
        public bool IsSuccess { get; set; }
    }

    class ChangePatientProcessor : BusinessBase
    {
        internal ChangePatientProcessor()
            : base()
        {
        }

        internal ChangePatientProcessor(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(List<long> treatmentIds, long newPatientId)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(treatmentIds))
                {
                    string storedSql = "PKG_TREATMENT_CHANGE_PATIENT.PRO_EXECUTE";

                    TInt64 tTreatmentIds = new TInt64();
                    tTreatmentIds.Int64Array = treatmentIds.ToArray();

                    OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TInt64>("P_TREATMENT_IDS", "HIS_RS.T_NUMBER", tTreatmentIds, ParameterDirection.InputOutput);
                    OracleParameter param2 = new OracleParameter("P_NEW_PATIENT_ID", OracleDbType.Int64, newPatientId, ParameterDirection.Input);
                    OracleParameter param3 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);
                    object resultHolder = null;

                    if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2, param3))
                    {
                        throw new Exception("Thay doi benh nhan that bai. " + LogUtil.TraceData("tTreatmentIds", tTreatmentIds) + LogUtil.TraceData("newPatientId", newPatientId));
                    }

                    if (resultHolder != null)
                    {
                        ResultHolder rs = (ResultHolder)resultHolder;
                        result = rs.IsSuccess;
                    }
                    if (!result)
                    {
                        LogSystem.Warn("EXECUTE PKG_TREATMENT_CHANGE_PATIENT FAILD");
                    }
                }
                else
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }


        //Xu ly ket qua tra ve khi goi procedure
        private void OutputHandler(ref object resultHolder, OracleDataReader dataReader, params OracleParameter[] parameters)
        {
            try
            {
                ResultHolder rs = new ResultHolder();
                if (parameters[2] != null && parameters[2].Value != null && parameters[2].Value != DBNull.Value)
                {
                    rs.IsSuccess = Int32.Parse(parameters[2].Value.ToString()) == Constant.IS_TRUE;
                }
                resultHolder = rs;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
