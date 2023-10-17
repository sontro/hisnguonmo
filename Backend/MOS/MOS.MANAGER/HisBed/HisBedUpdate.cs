using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBed
{
    partial class HisBedUpdate : BusinessBase
    {
        private List<HIS_BED> beforeUpdateHisBeds = new List<HIS_BED>();

        internal HisBedUpdate()
            : base()
        {

        }

        internal HisBedUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BED data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBedCheck checker = new HisBedCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BED raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckExists(data);
                if (valid)
                {
                    if (!DAOWorker.HisBedDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBed_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBed that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisBeds.Add(raw);

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

        internal bool UpdateList(List<HIS_BED> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBedCheck checker = new HisBedCheck(param);
                List<HIS_BED> listRaw = new List<HIS_BED>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                valid = valid && checker.CheckExists(listData);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisBeds.AddRange(listRaw);
                    if (!DAOWorker.HisBedDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBed_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBed that bai." + LogUtil.TraceData("listData", listData));
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

        internal bool UpdateList(List<HIS_BED> listData, List<HIS_BED> listBefore, bool notCheckExists)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBedCheck checker = new HisBedCheck(param);
                valid = valid && checker.IsUnLock(listBefore);
                valid = valid && (notCheckExists || checker.CheckExists(listData));
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisBedDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBed_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBed that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisBeds.AddRange(listBefore);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBeds))
            {
                if (!new HisBedUpdate(param).UpdateList(this.beforeUpdateHisBeds))
                {
                    LogSystem.Warn("Rollback du lieu HisBed that bai, can kiem tra lai." + LogUtil.TraceData("HisBeds", this.beforeUpdateHisBeds));
                }
            }
        }
    }
}
