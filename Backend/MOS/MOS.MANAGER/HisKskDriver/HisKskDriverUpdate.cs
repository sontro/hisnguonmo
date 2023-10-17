using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskDriver
{
    partial class HisKskDriverUpdate : BusinessBase
    {
        private List<HIS_KSK_DRIVER> beforeUpdateHisKskDrivers = new List<HIS_KSK_DRIVER>();

        internal HisKskDriverUpdate()
            : base()
        {

        }

        internal HisKskDriverUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_KSK_DRIVER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskDriverCheck checker = new HisKskDriverCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_KSK_DRIVER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (!DAOWorker.HisKskDriverDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskDriver_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskDriver that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisKskDrivers.Add(raw);
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

        internal bool Update(HIS_KSK_DRIVER data, HIS_KSK_DRIVER before)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskDriverCheck checker = new HisKskDriverCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_KSK_DRIVER raw = null;
                valid = valid && checker.IsUnLock(before);
                if (valid)
                {
                    if (!DAOWorker.HisKskDriverDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskDriver_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskDriver that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisKskDrivers.Add(before);
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

        internal bool UpdateList(List<HIS_KSK_DRIVER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskDriverCheck checker = new HisKskDriverCheck(param);
                List<HIS_KSK_DRIVER> listRaw = new List<HIS_KSK_DRIVER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisKskDriverDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskDriver_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskDriver that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisKskDrivers.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisKskDrivers))
            {
                if (!DAOWorker.HisKskDriverDAO.UpdateList(this.beforeUpdateHisKskDrivers))
                {
                    LogSystem.Warn("Rollback du lieu HisKskDriver that bai, can kiem tra lai." + LogUtil.TraceData("HisKskDrivers", this.beforeUpdateHisKskDrivers));
                }
                this.beforeUpdateHisKskDrivers = null;
            }
        }
    }
}
