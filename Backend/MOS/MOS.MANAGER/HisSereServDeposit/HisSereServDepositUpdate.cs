using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServDeposit
{
    partial class HisSereServDepositUpdate : BusinessBase
    {
        private List<HIS_SERE_SERV_DEPOSIT> beforeUpdateHisSereServDeposits = new List<HIS_SERE_SERV_DEPOSIT>();

        internal HisSereServDepositUpdate()
            : base()
        {

        }

        internal HisSereServDepositUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERE_SERV_DEPOSIT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServDepositCheck checker = new HisSereServDepositCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SERE_SERV_DEPOSIT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (!DAOWorker.HisSereServDepositDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServDeposit_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServDeposit that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisSereServDeposits.Add(raw);

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

        internal bool UpdateList(List<HIS_SERE_SERV_DEPOSIT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServDepositCheck checker = new HisSereServDepositCheck(param);
                List<HIS_SERE_SERV_DEPOSIT> listRaw = new List<HIS_SERE_SERV_DEPOSIT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisSereServDepositDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServDeposit_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServDeposit that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisSereServDeposits.AddRange(listRaw);
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

        internal bool UpdateList(List<HIS_SERE_SERV_DEPOSIT> listData,List<HIS_SERE_SERV_DEPOSIT> listBefore, bool checkLock)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServDepositCheck checker = new HisSereServDepositCheck(param);
                if (checkLock)
                {
                    valid = valid && checker.IsUnLock(listBefore);
                }
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisSereServDepositDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServDeposit_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServDeposit that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisSereServDeposits.AddRange(listBefore);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisSereServDeposits))
            {
                if (!DAOWorker.HisSereServDepositDAO.UpdateList(this.beforeUpdateHisSereServDeposits))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServDeposit that bai, can kiem tra lai." + LogUtil.TraceData("HisSereServDeposits", this.beforeUpdateHisSereServDeposits));
                }
            }
        }
    }
}
