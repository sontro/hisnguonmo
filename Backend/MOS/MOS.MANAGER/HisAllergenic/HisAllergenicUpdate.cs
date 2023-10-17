using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAllergenic
{
    partial class HisAllergenicUpdate : BusinessBase
    {
		private List<HIS_ALLERGENIC> beforeUpdateHisAllergenics = new List<HIS_ALLERGENIC>();
		
        internal HisAllergenicUpdate()
            : base()
        {

        }

        internal HisAllergenicUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ALLERGENIC data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAllergenicCheck checker = new HisAllergenicCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ALLERGENIC raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisAllergenicDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAllergenic_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAllergenic that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisAllergenics.Add(raw);
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

        internal bool UpdateList(List<HIS_ALLERGENIC> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAllergenicCheck checker = new HisAllergenicCheck(param);
                List<HIS_ALLERGENIC> listRaw = new List<HIS_ALLERGENIC>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisAllergenicDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAllergenic_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAllergenic that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisAllergenics.AddRange(listRaw);
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

        internal bool UpdateList(List<HIS_ALLERGENIC> listData, List<HIS_ALLERGENIC> befores)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAllergenicCheck checker = new HisAllergenicCheck(param);
                valid = valid && checker.IsUnLock(befores);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisAllergenicDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAllergenic_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAllergenic that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisAllergenics.AddRange(befores);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAllergenics))
            {
                if (!DAOWorker.HisAllergenicDAO.UpdateList(this.beforeUpdateHisAllergenics))
                {
                    LogSystem.Warn("Rollback du lieu HisAllergenic that bai, can kiem tra lai." + LogUtil.TraceData("HisAllergenics", this.beforeUpdateHisAllergenics));
                }
				this.beforeUpdateHisAllergenics = null;
            }
        }
    }
}
