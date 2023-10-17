using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskOccupational
{
    partial class HisKskOccupationalUpdate : BusinessBase
    {
		private List<HIS_KSK_OCCUPATIONAL> beforeUpdateHisKskOccupationals = new List<HIS_KSK_OCCUPATIONAL>();
		
        internal HisKskOccupationalUpdate()
            : base()
        {

        }

        internal HisKskOccupationalUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_KSK_OCCUPATIONAL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskOccupationalCheck checker = new HisKskOccupationalCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_KSK_OCCUPATIONAL raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisKskOccupationalDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskOccupational_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskOccupational that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisKskOccupationals.Add(raw);
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

        internal bool UpdateList(List<HIS_KSK_OCCUPATIONAL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskOccupationalCheck checker = new HisKskOccupationalCheck(param);
                List<HIS_KSK_OCCUPATIONAL> listRaw = new List<HIS_KSK_OCCUPATIONAL>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisKskOccupationalDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskOccupational_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskOccupational that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisKskOccupationals.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisKskOccupationals))
            {
                if (!DAOWorker.HisKskOccupationalDAO.UpdateList(this.beforeUpdateHisKskOccupationals))
                {
                    LogSystem.Warn("Rollback du lieu HisKskOccupational that bai, can kiem tra lai." + LogUtil.TraceData("HisKskOccupationals", this.beforeUpdateHisKskOccupationals));
                }
				this.beforeUpdateHisKskOccupationals = null;
            }
        }
    }
}
