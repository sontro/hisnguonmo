using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBaby
{
    partial class HisBabyUpdate : BusinessBase
    {
		private List<HIS_BABY> beforeUpdateHisBabys = new List<HIS_BABY>();
		
        internal HisBabyUpdate()
            : base()
        {

        }

        internal HisBabyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BABY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBabyCheck checker = new HisBabyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BABY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisBabys.Add(raw);
					if (!DAOWorker.HisBabyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBaby_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBaby that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_BABY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBabyCheck checker = new HisBabyCheck(param);
                List<HIS_BABY> listRaw = new List<HIS_BABY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisBabys.AddRange(listRaw);
					if (!DAOWorker.HisBabyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBaby_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBaby that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBabys))
            {
                if (!new HisBabyUpdate(param).UpdateList(this.beforeUpdateHisBabys))
                {
                    LogSystem.Warn("Rollback du lieu HisBaby that bai, can kiem tra lai." + LogUtil.TraceData("HisBabys", this.beforeUpdateHisBabys));
                }
            }
        }
    }
}
