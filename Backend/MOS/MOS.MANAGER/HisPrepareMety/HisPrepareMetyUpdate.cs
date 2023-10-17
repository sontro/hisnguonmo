using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPrepareMety
{
    partial class HisPrepareMetyUpdate : BusinessBase
    {
		private List<HIS_PREPARE_METY> beforeUpdateHisPrepareMetys = new List<HIS_PREPARE_METY>();
		
        internal HisPrepareMetyUpdate()
            : base()
        {

        }

        internal HisPrepareMetyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PREPARE_METY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPrepareMetyCheck checker = new HisPrepareMetyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PREPARE_METY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisPrepareMetyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPrepareMety_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPrepareMety that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisPrepareMetys.Add(raw);
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

        internal bool UpdateList(List<HIS_PREPARE_METY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPrepareMetyCheck checker = new HisPrepareMetyCheck(param);
                List<HIS_PREPARE_METY> listRaw = new List<HIS_PREPARE_METY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisPrepareMetyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPrepareMety_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPrepareMety that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisPrepareMetys.AddRange(listRaw);
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

        internal bool UpdateList(List<HIS_PREPARE_METY> listData, List<HIS_PREPARE_METY> listBefore)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPrepareMetyCheck checker = new HisPrepareMetyCheck(param);
                valid = valid && checker.IsUnLock(listBefore);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisPrepareMetyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPrepareMety_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPrepareMety that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisPrepareMetys.AddRange(listBefore);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisPrepareMetys))
            {
                if (!DAOWorker.HisPrepareMetyDAO.UpdateList(this.beforeUpdateHisPrepareMetys))
                {
                    LogSystem.Warn("Rollback du lieu HisPrepareMety that bai, can kiem tra lai." + LogUtil.TraceData("HisPrepareMetys", this.beforeUpdateHisPrepareMetys));
                }
				this.beforeUpdateHisPrepareMetys = null;
            }
        }
    }
}
