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
        private List<HIS_EXP_MEST> beforeUpdateHisExpMests = new List<HIS_EXP_MEST>();

        internal HisExpMestUpdate()
            : base()
        {

        }

        internal HisExpMestUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EXP_MEST data, HIS_EXP_MEST beforeUpdate)
        {
            return this.UpdateList(new List<HIS_EXP_MEST>() { data }, new List<HIS_EXP_MEST>() { beforeUpdate });
        }

        internal bool UpdateList(List<HIS_EXP_MEST> listData, List<HIS_EXP_MEST> beforeUpdates)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestCheck checker = new HisExpMestCheck(param);
                HisMediStockCheck mediStockChecker = new HisMediStockCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                foreach (var beforeUpdate in beforeUpdates)
                {
                    valid = valid && checker.IsUnlock(beforeUpdate);
                    valid = valid && checker.HasNoNationalCode(beforeUpdate);
                    valid = valid && mediStockChecker.IsUnLockCache(beforeUpdate.MEDI_STOCK_ID);
                }
                if (valid)
                {
                    if (!DAOWorker.HisExpMestDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMest_ThemMoiThatBai);
                        throw new Exception("Sua thong tin HisExpMest that bai." + LogUtil.TraceData("listData", listData));
                    }
                    result = true;
                    this.beforeUpdateHisExpMests.AddRange(beforeUpdates);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisExpMests))
            {
                if (!DAOWorker.HisExpMestDAO.UpdateList(this.beforeUpdateHisExpMests))
                {
                    LogSystem.Warn("Rollback du lieu HisExpMest that bai, can kiem tra lai." + LogUtil.TraceData("beforeUpdateHisExpMests", this.beforeUpdateHisExpMests));
                }
            }
        }
    }
}
