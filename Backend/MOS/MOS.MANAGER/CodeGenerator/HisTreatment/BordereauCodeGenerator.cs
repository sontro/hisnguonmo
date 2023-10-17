using Inventec.Core;
using Inventec.Common.Logging;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.EFMODEL.DataModels;
using MOS.UTILITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.CodeGenerator.HisTreatment
{
    public class BordereauCodeGenerator
    {
        internal static bool SetEndCode(HIS_TREATMENT treatment, CommonParam param)
        {
            bool valid = false;
            try
            {
                if (treatment.HEIN_LOCK_TIME.HasValue && treatment.TDL_TREATMENT_TYPE_ID.HasValue)
                {
                    string bordereauCode = BordereauCodeGenerator.GetBordereauCode(treatment.HEIN_LOCK_TIME.Value, treatment.TDL_TREATMENT_TYPE_ID.Value);

                    if (string.IsNullOrWhiteSpace(bordereauCode))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_TuDongSinhSoRaVienThatBai);
                        return false;
                    }
                    treatment.STORE_BORDEREAU_CODE = bordereauCode;
                    return true;
                }
                else
                {
                    LogSystem.Error("Loi khi tao ma luu tru bang ke. Khong co thong tin thoi gian khoa bao hiem va dien dieu tri");
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
            return valid;
        }

        internal static string GetBordereauCode(long lockTime, long treatmentTypeId)
        {
            string result = null;
            try
            {
                string storedSql = "PKG_TREATMENT_BORDEREAU_CODE.PRO_GET";
                OracleParameter lockTimePar = new OracleParameter("P_TIME", OracleDbType.Long);
                lockTimePar.Direction = ParameterDirection.Input;
                lockTimePar.Value = lockTime;
                OracleParameter treatTypePar = new OracleParameter("P_TREATMENT_TYPE", OracleDbType.Long);
                treatTypePar.Direction = ParameterDirection.Input;
                treatTypePar.Value = treatmentTypeId;

                OracleParameter resultPar = new OracleParameter("P_BORDEREAU_CODE", OracleDbType.Varchar2, 1000);
                resultPar.Direction = ParameterDirection.Output;

                object resultHolder = null;

                if (DAOWorker.SqlDAO.ExecuteStored(OutputHandler, ref resultHolder, storedSql, resultPar, lockTimePar, treatTypePar))
                {
                    result = (string)resultHolder;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        //Xu ly ket qua tra ve khi goi procedure
        private static void OutputHandler(ref object resultHolder, params OracleParameter[] parameters)
        {
            try
            {
                if (parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    resultHolder = !Constant.DB_NULL_STR.Equals(parameters[0].Value.ToString()) ? parameters[0].Value.ToString() : null;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
