using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVitaminA
{
    partial class HisVitaminAUpdate : BusinessBase
    {
		private List<HIS_VITAMIN_A> beforeUpdateHisVitaminAs = new List<HIS_VITAMIN_A>();
		
        internal HisVitaminAUpdate()
            : base()
        {

        }

        internal HisVitaminAUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_VITAMIN_A data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVitaminACheck checker = new HisVitaminACheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_VITAMIN_A raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.VITAMIN_A_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisVitaminADAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVitaminA_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVitaminA that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisVitaminAs.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_VITAMIN_A> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVitaminACheck checker = new HisVitaminACheck(param);
                List<HIS_VITAMIN_A> listRaw = new List<HIS_VITAMIN_A>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.VITAMIN_A_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisVitaminADAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVitaminA_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVitaminA that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisVitaminAs.AddRange(listRaw);
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

        internal bool UpdateList(List<HIS_VITAMIN_A> listData, List<HIS_VITAMIN_A> listBefore)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVitaminACheck checker = new HisVitaminACheck(param);
                valid = valid && checker.IsUnLock(listBefore);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.VITAMIN_A_CODE, data.ID);
                }
                if (valid)
                {
                    if (!DAOWorker.HisVitaminADAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVitaminA_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVitaminA that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisVitaminAs.AddRange(listBefore);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisVitaminAs))
            {
                if (!DAOWorker.HisVitaminADAO.UpdateList(this.beforeUpdateHisVitaminAs))
                {
                    LogSystem.Warn("Rollback du lieu HisVitaminA that bai, can kiem tra lai." + LogUtil.TraceData("HisVitaminAs", this.beforeUpdateHisVitaminAs));
                }
				this.beforeUpdateHisVitaminAs = null;
            }
        }
    }
}
