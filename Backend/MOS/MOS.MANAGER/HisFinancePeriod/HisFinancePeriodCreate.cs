using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace MOS.MANAGER.HisFinancePeriod
{
    partial class HisFinancePeriodCreate : BusinessBase
    {
		private List<HIS_FINANCE_PERIOD> recentHisFinancePeriods = new List<HIS_FINANCE_PERIOD>();
		
        internal HisFinancePeriodCreate()
            : base()
        {

        }

        internal HisFinancePeriodCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_FINANCE_PERIOD data, ref HIS_FINANCE_PERIOD resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisFinancePeriodCheck checker = new HisFinancePeriodCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.CheckPeriodTime(data);
                if (valid)
                {
                    string storedSql = "PKG_CREATE_FINANCE_PERIOD.PRO_CREATE_FINANCE_PERIOD";
                    string creator = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    OracleParameter periodPar = new OracleParameter("P_PERIOD_TIME", OracleDbType.Int64, data.PERIOD_TIME, ParameterDirection.Input);
                    OracleParameter branchIdPar = new OracleParameter("P_BRANCH_ID", OracleDbType.Int64, data.BRANCH_ID, ParameterDirection.Input);
                    OracleParameter creatorPar = new OracleParameter("P_CREATOR", OracleDbType.Varchar2, creator, ParameterDirection.Input);
                    OracleParameter appCreatorPar = new OracleParameter("P_APP_CREATOR", OracleDbType.Varchar2, MOS.UTILITY.Constant.APPLICATION_CODE, ParameterDirection.Input);
                    OracleParameter resultPar = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                    object resultHolder = null;
                    if (DAOWorker.SqlDAO.ExecuteStored(OutputHandler, ref resultHolder, storedSql, periodPar, branchIdPar, creatorPar, appCreatorPar, resultPar))
                    {
                        if (resultHolder != null)
                        {
                            resultData = (HIS_FINANCE_PERIOD)resultHolder;
                            result = resultData != null;
                        }
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

        private void OutputHandler(ref object resultHolder, params OracleParameter[] parameters)
        {
            try
            {
                //Tham so thu 5 chua output
                if (parameters[4] != null && parameters[4].Value != null)
                {
                    long id = long.Parse(parameters[4].Value.ToString());
                    resultHolder = new HisFinancePeriodGet().GetById(id);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
		
		internal bool CreateList(List<HIS_FINANCE_PERIOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisFinancePeriodCheck checker = new HisFinancePeriodCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisFinancePeriodDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFinancePeriod_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisFinancePeriod that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisFinancePeriods.AddRange(listData);
                    result = true;
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
		
		internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.recentHisFinancePeriods))
            {
                if (!DAOWorker.HisFinancePeriodDAO.TruncateList(this.recentHisFinancePeriods))
                {
                    LogSystem.Warn("Rollback du lieu HisFinancePeriod that bai, can kiem tra lai." + LogUtil.TraceData("recentHisFinancePeriods", this.recentHisFinancePeriods));
                }
				this.recentHisFinancePeriods = null;
            }
        }
    }
}
