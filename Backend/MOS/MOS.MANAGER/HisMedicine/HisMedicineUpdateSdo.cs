using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMedicine
{
    class HisMedicineUpdateSdo : BusinessBase
    {
        private HisMedicineUpdate hisMedicineUpdate;
        internal HisMedicineUpdateSdo()
            : base()
        {
            this.hisMedicineUpdate = new HisMedicineUpdate(param);
        }

        internal HisMedicineUpdateSdo(CommonParam param)
            : base(param)
        {
            this.hisMedicineUpdate = new HisMedicineUpdate(param);
        }

        internal bool Run(HisMedicineSDO data, ref HIS_MEDICINE resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MEDICINE raw = null;
                HisMedicineCheck checker = new HisMedicineCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && checker.VerifyRequireField(data.HisMedicine);
                valid = valid && checker.VerifyId(data.HisMedicine.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckChangePrice(data.HisMedicine, raw);
                if (valid)
                {
                    if (!hisMedicineUpdate.Update(data.HisMedicine, raw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicine that bai." + LogUtil.TraceData("data", data));
                    }

                    this.ProcessUpdateSereServ(data, raw);

                    resultData = data.HisMedicine;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
            }
            return result;
        }

        private void ProcessUpdateSereServ(HisMedicineSDO data, HIS_MEDICINE raw)
        {
            if ((data.IsUpdateAll || data.IsUpdateUnlock) )
            {
                string storedSql = "PKG_UPDATE_SERE_SERV__MEDICINE.PRO_UPDATE_SERE_SERV__MEDICINE";

                OracleParameter isUpdateSereServPar = null;
                if (data.IsUpdateAll)
                {
                    isUpdateSereServPar = new OracleParameter("P_IS_UPDATE_SERE_SERV", OracleDbType.Int64, 1, ParameterDirection.Input);
                }
                else
                {
                    isUpdateSereServPar = new OracleParameter("P_IS_UPDATE_SERE_SERV", OracleDbType.Int64, 0, ParameterDirection.Input);
                }

                string modifier = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                OracleParameter medicineIdPar = new OracleParameter("P_MEDICINE_ID", OracleDbType.Int64, raw.ID, ParameterDirection.Input);
                OracleParameter activeIngrBhytCodePar = new OracleParameter("P_ACTIVE_INGR_BHYT_CODE", OracleDbType.Varchar2, data.HisMedicine.ACTIVE_INGR_BHYT_CODE, ParameterDirection.Input);
                OracleParameter activeIngrBhytNamePar = new OracleParameter("P_ACTIVE_INGR_BHYT_NAME", OracleDbType.Varchar2, data.HisMedicine.ACTIVE_INGR_BHYT_NAME, ParameterDirection.Input);
                OracleParameter tdlBidNumOderPar = new OracleParameter("P_TDL_BID_NUM_ORDER", OracleDbType.Varchar2, data.HisMedicine.TDL_BID_NUM_ORDER, ParameterDirection.Input);
                OracleParameter medicineRegisterNumberPar = new OracleParameter("P_MEDICINE_REGISTER_NUMBER", OracleDbType.Varchar2, data.HisMedicine.MEDICINE_REGISTER_NUMBER, ParameterDirection.Input);
                OracleParameter packageNumberPar = new OracleParameter("P_PACKAGE_NUMBER", OracleDbType.Varchar2, data.HisMedicine.PACKAGE_NUMBER, ParameterDirection.Input);
                OracleParameter resultPar = new OracleParameter("P_RESULT", OracleDbType.Int64, ParameterDirection.Output);

                object resultHolder = null;
                if (!DAOWorker.SqlDAO.ExecuteStored(OutputHandler, ref resultHolder, storedSql, isUpdateSereServPar, medicineIdPar, activeIngrBhytCodePar, activeIngrBhytNamePar, tdlBidNumOderPar, medicineRegisterNumberPar, packageNumberPar, resultPar))
                {
                    throw new Exception("Execute Store PRO_UPDATE_SERE_SERV__MEDICINE Faild");
                }

                if (resultHolder == null)
                {
                    throw new Exception("Execute Store PRO_UPDATE_SERE_SERV__MEDICINE Faild. ResultHolder is null.");
                }

                var result = (long)resultHolder;
                if (result != 1)
                {
                    throw new Exception("Cap nhat sere_serv that bai:" + LogUtil.TraceData("Medicine", data.HisMedicine));
                }
            }
        }

        private void OutputHandler(ref object resultHolder, params OracleParameter[] parameters)
        {
            try
            {
                //Tham so thu 8 chua output
                if (parameters[7] != null && parameters[7].Value != null)
                {
                    long id = long.Parse(parameters[7].Value.ToString());
                    resultHolder = id;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// Khong dung tranh truong hop sua xong muon update tat ca
        /// </summary>
        /// <param name="data"></param>
        /// <param name="raw"></param>
        /// <returns></returns>
        private bool CheckDiffForUddateSereServ(HisMedicineSDO data, HIS_MEDICINE raw)
        {
            return ((data.HisMedicine.ACTIVE_INGR_BHYT_CODE ?? "") != (raw.ACTIVE_INGR_BHYT_CODE ?? "")
                || (data.HisMedicine.ACTIVE_INGR_BHYT_NAME ?? "") != (raw.ACTIVE_INGR_BHYT_NAME ?? "")
                || (data.HisMedicine.TDL_BID_NUM_ORDER ?? "") != (raw.TDL_BID_NUM_ORDER ?? "")
                || (data.HisMedicine.MEDICINE_REGISTER_NUMBER ?? "") != (raw.MEDICINE_REGISTER_NUMBER ?? "")
                || (data.HisMedicine.PACKAGE_NUMBER ?? "") != (raw.PACKAGE_NUMBER ?? ""));
        }

        private void RollbackData()
        {
            try
            {
                this.hisMedicineUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
