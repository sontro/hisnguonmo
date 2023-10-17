using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestBlood
{
    partial class HisExpMestBloodUpdate : BusinessBase
    {
        private List<HIS_EXP_MEST_BLOOD> beforeUpdateHisExpMestBloods = new List<HIS_EXP_MEST_BLOOD>();

        internal HisExpMestBloodUpdate()
            : base()
        {

        }

        internal HisExpMestBloodUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EXP_MEST_BLOOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestBloodCheck checker = new HisExpMestBloodCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EXP_MEST_BLOOD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisExpMestBloods.Add(raw);
                    if (!DAOWorker.HisExpMestBloodDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestBlood_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpMestBlood that bai." + LogUtil.TraceData("data", data));
                    }

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

        internal bool UpdateList(List<HIS_EXP_MEST_BLOOD> listData, List<HIS_EXP_MEST_BLOOD> befores)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestBloodCheck checker = new HisExpMestBloodCheck(param);
                valid = valid && checker.IsUnLock(befores);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisExpMestBloods.AddRange(befores);
                    if (!DAOWorker.HisExpMestBloodDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestBlood_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpMestBlood that bai." + LogUtil.TraceData("listData", listData));
                    }
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisExpMestBloods))
            {
                if (!DAOWorker.HisExpMestBloodDAO.UpdateList(this.beforeUpdateHisExpMestBloods))
                {
                    LogSystem.Warn("Rollback du lieu HisExpMestBlood that bai, can kiem tra lai." + LogUtil.TraceData("HisExpMestBloods", this.beforeUpdateHisExpMestBloods));
                }
            }
        }
    }
}
