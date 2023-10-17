using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSevereIllnessInfo
{
    partial class HisSevereIllnessInfoUpdate : BusinessBase
    {
		private List<HIS_SEVERE_ILLNESS_INFO> beforeUpdateHisSevereIllnessInfos = new List<HIS_SEVERE_ILLNESS_INFO>();
		
        internal HisSevereIllnessInfoUpdate()
            : base()
        {

        }

        internal HisSevereIllnessInfoUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SEVERE_ILLNESS_INFO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSevereIllnessInfoCheck checker = new HisSevereIllnessInfoCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SEVERE_ILLNESS_INFO raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisSevereIllnessInfoDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSevereIllnessInfo_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSevereIllnessInfo that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisSevereIllnessInfos.Add(raw);
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

        internal bool UpdateList(List<HIS_SEVERE_ILLNESS_INFO> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSevereIllnessInfoCheck checker = new HisSevereIllnessInfoCheck(param);
                List<HIS_SEVERE_ILLNESS_INFO> listRaw = new List<HIS_SEVERE_ILLNESS_INFO>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisSevereIllnessInfoDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSevereIllnessInfo_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSevereIllnessInfo that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisSevereIllnessInfos.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisSevereIllnessInfos))
            {
                if (!DAOWorker.HisSevereIllnessInfoDAO.UpdateList(this.beforeUpdateHisSevereIllnessInfos))
                {
                    LogSystem.Warn("Rollback du lieu HisSevereIllnessInfo that bai, can kiem tra lai." + LogUtil.TraceData("HisSevereIllnessInfos", this.beforeUpdateHisSevereIllnessInfos));
                }
				this.beforeUpdateHisSevereIllnessInfos = null;
            }
        }
    }
}
