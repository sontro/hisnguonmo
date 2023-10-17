using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMatyMaty
{
    partial class HisMatyMatyUpdate : BusinessBase
    {
		private List<HIS_MATY_MATY> beforeUpdateHisMatyMatys = new List<HIS_MATY_MATY>();
		
        internal HisMatyMatyUpdate()
            : base()
        {

        }

        internal HisMatyMatyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MATY_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMatyMatyCheck checker = new HisMatyMatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MATY_MATY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.MATY_MATY_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisMatyMatyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMatyMaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMatyMaty that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisMatyMatys.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_MATY_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMatyMatyCheck checker = new HisMatyMatyCheck(param);
                List<HIS_MATY_MATY> listRaw = new List<HIS_MATY_MATY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MATY_MATY_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisMatyMatyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMatyMaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMatyMaty that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisMatyMatys.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMatyMatys))
            {
                if (!DAOWorker.HisMatyMatyDAO.UpdateList(this.beforeUpdateHisMatyMatys))
                {
                    LogSystem.Warn("Rollback du lieu HisMatyMaty that bai, can kiem tra lai." + LogUtil.TraceData("HisMatyMatys", this.beforeUpdateHisMatyMatys));
                }
				this.beforeUpdateHisMatyMatys = null;
            }
        }
    }
}
