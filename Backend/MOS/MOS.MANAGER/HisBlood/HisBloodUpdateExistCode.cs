using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBlood
{
    partial class HisBloodUpdate : BusinessBase
    {
        private List<HIS_BLOOD> beforeUpdateHisBloods = new List<HIS_BLOOD>();

        internal HisBloodUpdate()
            : base()
        {

        }

        internal HisBloodUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BLOOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBloodCheck checker = new HisBloodCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BLOOD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.BLOOD_CODE, data.ID);
                if (valid)
                {
                    if (!DAOWorker.HisBloodDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBlood_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBlood that bai." + LogUtil.TraceData("data", data));
                    }

                    this.beforeUpdateHisBloods.Add(raw);
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

        internal bool UpdateList(List<HIS_BLOOD> listData)
        {
            bool result = false;
            try
            {
                result = this.UpdateList(listData, true);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool UpdateList(List<HIS_BLOOD> listData, bool isCheckUnlock)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBloodCheck checker = new HisBloodCheck(param);
                List<HIS_BLOOD> listRaw = new List<HIS_BLOOD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                if (isCheckUnlock)
                {
                    valid = valid && checker.IsUnLock(listRaw);
                }
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                valid = valid && checker.ExistsCode(listData);

                if (valid)
                {
                    if (!DAOWorker.HisBloodDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBlood_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBlood that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisBloods.AddRange(listRaw);
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

        internal bool UpdateList(List<HIS_BLOOD> listData, List<HIS_BLOOD> listBefore)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBloodCheck checker = new HisBloodCheck(param);
                valid = valid && checker.IsUnLock(listBefore);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                valid = valid && checker.ExistsCode(listData);
                if (valid)
                {
                    if (!DAOWorker.HisBloodDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBlood_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBlood that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisBloods.AddRange(listBefore);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBloods))
            {
                if (!DAOWorker.HisBloodDAO.UpdateList(this.beforeUpdateHisBloods))
                {
                    LogSystem.Warn("Rollback du lieu HisBlood that bai, can kiem tra lai." + LogUtil.TraceData("HisBloods", this.beforeUpdateHisBloods));
                }
            }
        }
    }
}
