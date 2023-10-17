using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMetyMaty
{
    partial class HisMetyMatyUpdate : BusinessBase
    {
		private List<HIS_METY_MATY> beforeUpdateHisMetyMatys = new List<HIS_METY_MATY>();
		
        internal HisMetyMatyUpdate()
            : base()
        {

        }

        internal HisMetyMatyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_METY_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMetyMatyCheck checker = new HisMetyMatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_METY_MATY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisMetyMatyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMetyMaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMetyMaty that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisMetyMatys.Add(raw);
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

        internal bool UpdateList(List<HIS_METY_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMetyMatyCheck checker = new HisMetyMatyCheck(param);
                List<HIS_METY_MATY> listRaw = new List<HIS_METY_MATY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisMetyMatyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMetyMaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMetyMaty that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisMetyMatys.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMetyMatys))
            {
                if (!DAOWorker.HisMetyMatyDAO.UpdateList(this.beforeUpdateHisMetyMatys))
                {
                    LogSystem.Warn("Rollback du lieu HisMetyMaty that bai, can kiem tra lai." + LogUtil.TraceData("HisMetyMatys", this.beforeUpdateHisMetyMatys));
                }
				this.beforeUpdateHisMetyMatys = null;
            }
        }
    }
}
