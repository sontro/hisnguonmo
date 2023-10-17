using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAccidentPoison
{
    partial class HisAccidentPoisonUpdate : BusinessBase
    {
		private List<HIS_ACCIDENT_POISON> beforeUpdateHisAccidentPoisons = new List<HIS_ACCIDENT_POISON>();
		
        internal HisAccidentPoisonUpdate()
            : base()
        {

        }

        internal HisAccidentPoisonUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ACCIDENT_POISON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentPoisonCheck checker = new HisAccidentPoisonCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ACCIDENT_POISON raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.ACCIDENT_POISON_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisAccidentPoisons.Add(raw);
					if (!DAOWorker.HisAccidentPoisonDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentPoison_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAccidentPoison that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_ACCIDENT_POISON> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAccidentPoisonCheck checker = new HisAccidentPoisonCheck(param);
                List<HIS_ACCIDENT_POISON> listRaw = new List<HIS_ACCIDENT_POISON>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.ACCIDENT_POISON_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisAccidentPoisons.AddRange(listRaw);
					if (!DAOWorker.HisAccidentPoisonDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentPoison_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAccidentPoison that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAccidentPoisons))
            {
                if (!new HisAccidentPoisonUpdate(param).UpdateList(this.beforeUpdateHisAccidentPoisons))
                {
                    LogSystem.Warn("Rollback du lieu HisAccidentPoison that bai, can kiem tra lai." + LogUtil.TraceData("HisAccidentPoisons", this.beforeUpdateHisAccidentPoisons));
                }
            }
        }
    }
}
