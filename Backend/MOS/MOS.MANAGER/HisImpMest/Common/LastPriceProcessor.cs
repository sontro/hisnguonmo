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

namespace MOS.MANAGER.HisImpMest.Common
{
    class ResultHolder
    {
        public bool IsSuccess { get; set; }
    }
    class LastPriceProcessor : BusinessBase
    {
        internal LastPriceProcessor()
            : base()
        {

        }

        internal LastPriceProcessor(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<long> medicineTypeIds, List<long> materialTypeIds)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(medicineTypeIds) || IsNotNullOrEmpty(materialTypeIds))
                {
                    if (medicineTypeIds == null) medicineTypeIds = new List<long>();
                    if (materialTypeIds == null) materialTypeIds = new List<long>();

                    string storedSql = "PKG_UPDATE_LAST_PRICE_METYMATY.PRO_EXECUTE";

                    TInt64 tMedicineTypeIds = new TInt64();
                    tMedicineTypeIds.Int64Array = medicineTypeIds.ToArray();
                    TInt64 tMaterialTypeIds = new TInt64();
                    tMaterialTypeIds.Int64Array = materialTypeIds.ToArray();

                    OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TInt64>("P_MEDICINE_TYPE_IDS", "HIS_RS.T_NUMBER", tMedicineTypeIds, ParameterDirection.InputOutput);
                    OracleParameter param2 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TInt64>("P_MATERIAL_TYPE_IDS", "HIS_RS.T_NUMBER", tMaterialTypeIds, ParameterDirection.InputOutput);
                    OracleParameter param3 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);
                    object resultHolder = null;

                    if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2, param3))
                    {
                        throw new Exception("Update Last_price that bai. " + LogUtil.TraceData("medicineTypeIds", medicineTypeIds) + LogUtil.TraceData("materialTypeIds", materialTypeIds));
                    }

                    if (resultHolder != null)
                    {
                        ResultHolder rs = (ResultHolder)resultHolder;
                        result = rs.IsSuccess;
                    }
                    if (!result)
                    {
                        LogSystem.Warn("EXECUTE PKG_UPDATE_LAST_PRICE_METYMATY FAILD");
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
