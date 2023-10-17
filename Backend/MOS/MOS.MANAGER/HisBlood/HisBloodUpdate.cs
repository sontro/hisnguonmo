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
                if (valid)
                {
                    this.beforeUpdateHisBloods.Add(raw);
					if (!DAOWorker.HisBloodDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBlood_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBlood that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_BLOOD> listData)
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
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisBloods.AddRange(listRaw);
					if (!DAOWorker.HisBloodDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBlood_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBlood that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBloods))
            {
                if (!new HisBloodUpdate(param).UpdateList(this.beforeUpdateHisBloods))
                {
                    LogSystem.Warn("Rollback du lieu HisBlood that bai, can kiem tra lai." + LogUtil.TraceData("HisBloods", this.beforeUpdateHisBloods));
                }
            }
        }
    }
}
