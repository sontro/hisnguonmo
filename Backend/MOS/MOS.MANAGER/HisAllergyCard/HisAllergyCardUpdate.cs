using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAllergyCard
{
    partial class HisAllergyCardUpdate : BusinessBase
    {
		private List<HIS_ALLERGY_CARD> beforeUpdateHisAllergyCards = new List<HIS_ALLERGY_CARD>();
		
        internal HisAllergyCardUpdate()
            : base()
        {

        }

        internal HisAllergyCardUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ALLERGY_CARD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAllergyCardCheck checker = new HisAllergyCardCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ALLERGY_CARD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisAllergyCardDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAllergyCard_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAllergyCard that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisAllergyCards.Add(raw);
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

        internal bool Update(HIS_ALLERGY_CARD data, HIS_ALLERGY_CARD before)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAllergyCardCheck checker = new HisAllergyCardCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsUnLock(before);
                if (valid)
                {
                    if (!DAOWorker.HisAllergyCardDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAllergyCard_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAllergyCard that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisAllergyCards.Add(before);
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

        internal bool UpdateList(List<HIS_ALLERGY_CARD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAllergyCardCheck checker = new HisAllergyCardCheck(param);
                List<HIS_ALLERGY_CARD> listRaw = new List<HIS_ALLERGY_CARD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisAllergyCardDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAllergyCard_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAllergyCard that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisAllergyCards.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAllergyCards))
            {
                if (!DAOWorker.HisAllergyCardDAO.UpdateList(this.beforeUpdateHisAllergyCards))
                {
                    LogSystem.Warn("Rollback du lieu HisAllergyCard that bai, can kiem tra lai." + LogUtil.TraceData("HisAllergyCards", this.beforeUpdateHisAllergyCards));
                }
				this.beforeUpdateHisAllergyCards = null;
            }
        }
    }
}
