using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServDeposit
{
    partial class HisSereServDepositCreate : BusinessBase
    {
        private List<HIS_SERE_SERV_DEPOSIT> recentHisSereServDeposits = new List<HIS_SERE_SERV_DEPOSIT>();

        internal HisSereServDepositCreate()
            : base()
        {

        }

        internal HisSereServDepositCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERE_SERV_DEPOSIT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServDepositCheck checker = new HisSereServDepositCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisSereServDepositDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServDeposit_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSereServDeposit that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSereServDeposits.Add(data);
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

        internal bool CreateList(List<HIS_SERE_SERV_DEPOSIT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServDepositCheck checker = new HisSereServDepositCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisSereServDepositDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServDeposit_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSereServDeposit that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisSereServDeposits.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisSereServDeposits))
            {
                if (!new HisSereServDepositTruncate(param).TruncateList(this.recentHisSereServDeposits))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServDeposit that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSereServDeposits", this.recentHisSereServDeposits));
                }
            }
        }
    }
}
