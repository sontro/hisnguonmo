using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMediStock;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMest.Common.Update
{
    partial class HisExpMestUpdate : BusinessBase
    {
        internal bool UpdateTransactionId(HIS_EXP_MEST data, HIS_EXP_MEST befores)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestCheck checker = new HisExpMestCheck(param);
                HisMediStockCheck mediStockChecker = new HisMediStockCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisExpMestDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMest_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpMest that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisExpMests.Add(befores);
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

        internal bool UpdateTransactionId(List<HIS_EXP_MEST> listData, List<HIS_EXP_MEST> befores)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestCheck checker = new HisExpMestCheck(param);
                valid = valid && IsNotNullOrEmpty(listData);
                valid = valid && checker.IsUnlock(befores);
                foreach (HIS_EXP_MEST data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisExpMestDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMest_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpMest that bai.");
                    }
                    this.beforeUpdateHisExpMests.AddRange(befores);
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
    }
}
