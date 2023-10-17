using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMestPatySub
{
    partial class HisMestPatySubUpdate : BusinessBase
    {
		private List<HIS_MEST_PATY_SUB> beforeUpdateHisMestPatySubs = new List<HIS_MEST_PATY_SUB>();
		
        internal HisMestPatySubUpdate()
            : base()
        {

        }

        internal HisMestPatySubUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEST_PATY_SUB data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestPatySubCheck checker = new HisMestPatySubCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEST_PATY_SUB raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisMestPatySubs.Add(raw);
					if (!DAOWorker.HisMestPatySubDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestPatySub_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMestPatySub that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_MEST_PATY_SUB> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestPatySubCheck checker = new HisMestPatySubCheck(param);
                List<HIS_MEST_PATY_SUB> listRaw = new List<HIS_MEST_PATY_SUB>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisMestPatySubs.AddRange(listRaw);
					if (!DAOWorker.HisMestPatySubDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestPatySub_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMestPatySub that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMestPatySubs))
            {
                if (!new HisMestPatySubUpdate(param).UpdateList(this.beforeUpdateHisMestPatySubs))
                {
                    LogSystem.Warn("Rollback du lieu HisMestPatySub that bai, can kiem tra lai." + LogUtil.TraceData("HisMestPatySubs", this.beforeUpdateHisMestPatySubs));
                }
            }
        }
    }
}
