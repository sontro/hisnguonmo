using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCarerCard
{
    partial class HisCarerCardUpdate : BusinessBase
    {
		private List<HIS_CARER_CARD> beforeUpdateHisCarerCards = new List<HIS_CARER_CARD>();
		
        internal HisCarerCardUpdate()
            : base()
        {

        }

        internal HisCarerCardUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_CARER_CARD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCarerCardCheck checker = new HisCarerCardCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_CARER_CARD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.CARER_CARD_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisCarerCardDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCarerCard_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCarerCard that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisCarerCards.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_CARER_CARD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCarerCardCheck checker = new HisCarerCardCheck(param);
                List<HIS_CARER_CARD> listRaw = new List<HIS_CARER_CARD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.CARER_CARD_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisCarerCardDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCarerCard_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCarerCard that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisCarerCards.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisCarerCards))
            {
                if (!DAOWorker.HisCarerCardDAO.UpdateList(this.beforeUpdateHisCarerCards))
                {
                    LogSystem.Warn("Rollback du lieu HisCarerCard that bai, can kiem tra lai." + LogUtil.TraceData("HisCarerCards", this.beforeUpdateHisCarerCards));
                }
				this.beforeUpdateHisCarerCards = null;
            }
        }
    }
}
