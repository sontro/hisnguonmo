using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisImpMestBlood
{
    partial class HisImpMestBloodUpdate : BusinessBase
    {
        private List<HIS_IMP_MEST_BLOOD> beforeUpdateHisImpMestBloods = new List<HIS_IMP_MEST_BLOOD>();

        internal HisImpMestBloodUpdate()
            : base()
        {

        }

        internal HisImpMestBloodUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_IMP_MEST_BLOOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestBloodCheck checker = new HisImpMestBloodCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_IMP_MEST_BLOOD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisImpMestBloods.Add(raw);
                    if (!DAOWorker.HisImpMestBloodDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestBlood_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMestBlood that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_IMP_MEST_BLOOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpMestBloodCheck checker = new HisImpMestBloodCheck(param);
                List<HIS_IMP_MEST_BLOOD> listRaw = new List<HIS_IMP_MEST_BLOOD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisImpMestBloodDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestBlood_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMestBlood that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisImpMestBloods.AddRange(listRaw);
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

        internal bool UpdateList(List<HIS_IMP_MEST_BLOOD> listData, List<HIS_IMP_MEST_BLOOD> beforeUpdates)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpMestBloodCheck checker = new HisImpMestBloodCheck(param);
                valid = valid && checker.IsUnLock(beforeUpdates);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisImpMestBloodDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestBlood_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMestBlood that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisImpMestBloods.AddRange(beforeUpdates);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisImpMestBloods))
            {
                if (!DAOWorker.HisImpMestBloodDAO.UpdateList(this.beforeUpdateHisImpMestBloods))
                {
                    LogSystem.Warn("Rollback du lieu HisImpMestBlood that bai, can kiem tra lai." + LogUtil.TraceData("HisImpMestBloods", this.beforeUpdateHisImpMestBloods));
                }
            }
        }
    }
}
