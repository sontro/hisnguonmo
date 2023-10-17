using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRegisterReq;
using MOS.SDO;
using MOS.DAO.Sql;
using MOS.OracleUDT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace MOS.MANAGER.HisRegisterGate
{
    class HisRegisterGateCallOrReCall : BusinessBase
    {
        internal HisRegisterGateCallOrReCall()
            : base()
        {
        }

        internal HisRegisterGateCallOrReCall(CommonParam param)
            : base(param)
        {
        }

        internal bool CallOrReCall(RegisterGateCallSDO data, bool isReCall,ref List<HIS_REGISTER_REQ> resultData)
        {
            bool result = false;
            try
            {
                HIS_REGISTER_GATE registerGate = null;
                
                HisRegisterGateCheck commonChecker = new HisRegisterGateCheck(param);
                HisRegisterGateCallOrReCallCheck checker = new HisRegisterGateCallOrReCallCheck(param);

                bool valid = true;
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && commonChecker.VerifyId(data.RegisterGateId, ref registerGate);
                if (valid)
                {
                    TInt64 TRegisterReqIds = new TInt64();
                    TRegisterReqIds.Int64Array = new List<long>().ToArray();
                    string storedSql = isReCall ? "PKG_CALL_REGISTER_REQ.PRO_RECALL" : "PKG_CALL_REGISTER_REQ.PRO_CALL"; // Goi hoac Goi lai
                    OracleParameter registerGateIdPar = new OracleParameter("P_REGISTER_GATE_ID", OracleDbType.Int64, data.RegisterGateId, ParameterDirection.Input);
                    OracleParameter registerDatePar = new OracleParameter("P_REGISTER_DATE", OracleDbType.Int64, Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd") + "000000"), ParameterDirection.Input);
                    OracleParameter callPlacePar = new OracleParameter("P_CALL_PLACE", OracleDbType.Varchar2, data.CallPlace, ParameterDirection.Input);
                    OracleParameter callStepPar = new OracleParameter("P_CALL_STEP", OracleDbType.Int64, data.CallStep.HasValue ? data.CallStep.Value : 1, ParameterDirection.Input);
                    OracleParameter resultPar = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TInt64>("P_IDs", "HIS_RS.T_NUMBER", TRegisterReqIds, ParameterDirection.InputOutput);

                    object resultHolder = null;

                    if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, registerGateIdPar, registerDatePar, callPlacePar, callStepPar, resultPar))
                    {
                        if (isReCall)
                        {
                            throw new Exception("Goi lai that bai!");
                        }
                        else
                        {
                            throw new Exception("Goi that bai!");
                        }
                    }

                    if (resultHolder != null)
                    {
                        TInt64 ids = (TInt64)resultHolder;
                        resultData = new HisRegisterReqGet().GetByIds(ids.Int64Array.ToList());
                        result = true;
                    }
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

        //Xu ly ket qua tra ve khi goi procedure
        private void OutputHandler(ref object resultHolder, OracleDataReader dataReader, params OracleParameter[] parameters)
        {
            try
            {
                TInt64 rs = new TInt64();
                if (parameters[4] != null && parameters[4].Value != null && parameters[4].Value != DBNull.Value)
                {
                    rs = (TInt64)parameters[4].Value;
                }
                resultHolder = rs;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
