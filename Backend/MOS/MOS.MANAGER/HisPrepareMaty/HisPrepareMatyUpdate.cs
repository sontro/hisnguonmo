using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPrepareMaty
{
    partial class HisPrepareMatyUpdate : BusinessBase
    {
		private List<HIS_PREPARE_MATY> beforeUpdateHisPrepareMatys = new List<HIS_PREPARE_MATY>();
		
        internal HisPrepareMatyUpdate()
            : base()
        {

        }

        internal HisPrepareMatyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PREPARE_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPrepareMatyCheck checker = new HisPrepareMatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PREPARE_MATY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisPrepareMatyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPrepareMaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPrepareMaty that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisPrepareMatys.Add(raw);
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

        internal bool UpdateList(List<HIS_PREPARE_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPrepareMatyCheck checker = new HisPrepareMatyCheck(param);
                List<HIS_PREPARE_MATY> listRaw = new List<HIS_PREPARE_MATY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisPrepareMatyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPrepareMaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPrepareMaty that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisPrepareMatys.AddRange(listRaw);
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

        internal bool UpdateList(List<HIS_PREPARE_MATY> listData, List<HIS_PREPARE_MATY> listBefore)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPrepareMatyCheck checker = new HisPrepareMatyCheck(param);
                valid = valid && checker.IsUnLock(listBefore);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisPrepareMatyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPrepareMaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPrepareMaty that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisPrepareMatys.AddRange(listBefore);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisPrepareMatys))
            {
                if (!DAOWorker.HisPrepareMatyDAO.UpdateList(this.beforeUpdateHisPrepareMatys))
                {
                    LogSystem.Warn("Rollback du lieu HisPrepareMaty that bai, can kiem tra lai." + LogUtil.TraceData("HisPrepareMatys", this.beforeUpdateHisPrepareMatys));
                }
				this.beforeUpdateHisPrepareMatys = null;
            }
        }
    }
}
