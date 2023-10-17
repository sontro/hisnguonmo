using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisKskContract;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisKskAccess
{
    class HisKskAccessAssignEmployee : BusinessBase
    {
		private HisKskAccessCreate createProcessor;
        private HisKskAccessTruncate truncateProcessor;

        private List<HIS_KSK_ACCESS> beforeAccesses;
		
        internal HisKskAccessAssignEmployee()
            : base()
        {
            this.Init();
        }

        internal HisKskAccessAssignEmployee(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.createProcessor = new HisKskAccessCreate(param);
            this.truncateProcessor = new HisKskAccessTruncate(param);

            this.beforeAccesses = new List<HIS_KSK_ACCESS>();
        }

        internal bool Run(KskAccessSDO data, ref KskAccessResultSDO resultData)
        {
            bool result = false;
            try
            {
                resultData = new KskAccessResultSDO();
                HIS_KSK_CONTRACT contract = null;
                HisKskAccessAssignEmployeeCheck checker = new HisKskAccessAssignEmployeeCheck(param);
                HisKskContractCheck contractChecker = new HisKskContractCheck(param);
                bool valid = true;
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && contractChecker.VerifyId(data.KskContractId, ref contract);
                valid = valid && contractChecker.IsLimitedAccess(contract);
                if (valid)
                {
                    List<HIS_KSK_ACCESS> listOldAccess = new HisKskAccessGet().GetByKskContract(data.KskContractId);
                    if (IsNotNullOrEmpty(listOldAccess))
                    {
                        this.beforeAccesses.AddRange(listOldAccess);
                        if (!truncateProcessor.TruncateList(listOldAccess))
                        {
                            throw new Exception("Xoa du lieu HIS_KSK_ACCESS cu theo KskContractId that bai." + LogUtil.TraceData("data", listOldAccess));
                        }
                    }

                    List<HIS_KSK_ACCESS> listNewAccess = new List<HIS_KSK_ACCESS>();
                    if (IsNotNullOrEmpty(data.EmployeeIds))
                    {
                        foreach (var employeeId in data.EmployeeIds)
                        {
                            HIS_KSK_ACCESS newAccess = new HIS_KSK_ACCESS();
                            newAccess.EMPLOYEE_ID = employeeId;
                            newAccess.KSK_CONTRACT_ID = contract.ID;
                            listNewAccess.Add(newAccess);
                        }
                    }

                    if (IsNotNullOrEmpty(listNewAccess))
                    {
                        if (!createProcessor.CreateList(listNewAccess))
                        {
                            throw new Exception("Them du lieu HIS_KSK_ACCESS moi theo KskContractId va employeeId that bai," + LogUtil.TraceData("data", listNewAccess));
                        }
                    }
                    resultData.KskContract = contract;
                    resultData.KskAccesses = listNewAccess;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
		
		internal void RollbackData()
        {
            this.createProcessor.RollbackData();
            if (IsNotNullOrEmpty(this.beforeAccesses))
            {
                if (!DAOWorker.HisKskAccessDAO.CreateList(this.beforeAccesses))
                {
                    LogSystem.Warn("Rollback Tao lai du lieu HisKskAccess that bai, can kiem tra lai." + LogUtil.TraceData("recentHisKskAccesss", this.beforeAccesses));
                }
                this.beforeAccesses = null;
            }
        }
    }
}
