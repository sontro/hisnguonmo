using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Common.Logging;
using MOS.UTILITY;
using System.Data;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.EFMODEL.DataModels;
using Inventec.Core;

namespace MOS.MANAGER.CodeGenerator.HisTreatment
{
    public class EndCodeGenerator
    {
        internal static bool SetEndCode(HIS_TREATMENT treatment, CommonParam param)
        {
            try
            {
                string seedCode = null;
                string endCode = null;
                HIS_TREATMENT_TYPE treatmentType = HisTreatmentTypeCFG.DATA.Where(o => o.ID == treatment.TDL_TREATMENT_TYPE_ID).FirstOrDefault();
                if (HisTreatmentCFG.END_CODE_OPTION == HisTreatmentCFG.EndCodeOption.OPTION1)
                {
                    endCode = EndCodeGenerator.GetEndCode(treatment.OUT_TIME.Value, seedCode, (long)HisTreatmentCFG.END_CODE_OPTION);
                }
                else if (HisTreatmentCFG.END_CODE_OPTION == HisTreatmentCFG.EndCodeOption.OPTION2)
                {
                    // Truong hop key = 3 thi seedCode la ma dien dieu tri
                    seedCode = treatmentType != null ? treatmentType.TREATMENT_TYPE_CODE : "";
                    endCode = EndCodeGenerator.GetEndCode(treatment.OUT_TIME.Value, seedCode, (long)HisTreatmentCFG.END_CODE_OPTION);
                }
                else if (HisTreatmentCFG.END_CODE_OPTION == HisTreatmentCFG.EndCodeOption.OPTION3 && treatment.TDL_TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                {
                    var department = HisDepartmentCFG.DATA.FirstOrDefault(o => treatment.END_DEPARTMENT_ID.HasValue && o.ID == treatment.END_DEPARTMENT_ID.Value);
                    var endTypeCode = HisTreatmentEndTypeCFG.DATA.FirstOrDefault(o => o.ID == treatment.TREATMENT_END_TYPE_ID);

                    string departStr = department != null ? department.DEPARTMENT_CODE : "";
                    string typePrefixCode = treatmentType != null ? treatmentType.END_CODE_PREFIX : "";
                    string endTypePrefixCode = endTypeCode != null ? endTypeCode.END_CODE_PREFIX : "";

                    seedCode = departStr + typePrefixCode + endTypePrefixCode;
                    endCode = EndCodeGenerator.GetEndCode(treatment.OUT_TIME.Value, seedCode, (long)HisTreatmentCFG.END_CODE_OPTION);
                }

                if (string.IsNullOrWhiteSpace(endCode)
                    && (HisTreatmentCFG.END_CODE_OPTION != HisTreatmentCFG.EndCodeOption.OPTION3
                        || (HisTreatmentCFG.END_CODE_OPTION == HisTreatmentCFG.EndCodeOption.OPTION3 && treatment.TDL_TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                       )
                   )
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_TuDongSinhSoRaVienThatBai);
                    return false;
                }
                treatment.END_CODE = endCode;
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal static string GetEndCode(long outTime, string seedCode, long formatOption)
        {
            string result = null;
            try
            {
                string storedSql = "PKG_TREATMENT_END_CODE.PRO_GET";
                OracleParameter inPar = new OracleParameter("P_TIME", OracleDbType.Long);
                inPar.Direction = ParameterDirection.Input;
                inPar.Value = outTime;
                OracleParameter seedCodePar = new OracleParameter("P_SEED_CODE", OracleDbType.Varchar2, 300);
                seedCodePar.Direction = ParameterDirection.Input;
                seedCodePar.Value = seedCode;
                OracleParameter formatOptionPar = new OracleParameter("P_FORMAT_OPTION", OracleDbType.Long);
                formatOptionPar.Direction = ParameterDirection.Input;
                formatOptionPar.Value = formatOption;

                OracleParameter resultPar = new OracleParameter("P_END_CODE", OracleDbType.Varchar2, 1000);
                resultPar.Direction = ParameterDirection.Output;

                object resultHolder = null;

                if (DAOWorker.SqlDAO.ExecuteStored(OutputHandler, ref resultHolder, storedSql, resultPar, inPar, seedCodePar, formatOptionPar))
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
