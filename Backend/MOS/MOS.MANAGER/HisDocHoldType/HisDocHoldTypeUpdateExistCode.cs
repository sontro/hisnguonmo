using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDocHoldType
{
    partial class HisDocHoldTypeUpdate : BusinessBase
    {
		private List<HIS_DOC_HOLD_TYPE> beforeUpdateHisDocHoldTypes = new List<HIS_DOC_HOLD_TYPE>();
		
        internal HisDocHoldTypeUpdate()
            : base()
        {

        }

        internal HisDocHoldTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_DOC_HOLD_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDocHoldTypeCheck checker = new HisDocHoldTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_DOC_HOLD_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.DOC_HOLD_TYPE_CODE, data.ID);
                valid = valid && checker.IsExistedHeinCard(data.IS_HEIN_CARD);
                if (valid)
                {
					if (!DAOWorker.HisDocHoldTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDocHoldType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDocHoldType that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisDocHoldTypes.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_DOC_HOLD_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDocHoldTypeCheck checker = new HisDocHoldTypeCheck(param);
                List<HIS_DOC_HOLD_TYPE> listRaw = new List<HIS_DOC_HOLD_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.DOC_HOLD_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisDocHoldTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDocHoldType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDocHoldType that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisDocHoldTypes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisDocHoldTypes))
            {
                if (!DAOWorker.HisDocHoldTypeDAO.UpdateList(this.beforeUpdateHisDocHoldTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisDocHoldType that bai, can kiem tra lai." + LogUtil.TraceData("HisDocHoldTypes", this.beforeUpdateHisDocHoldTypes));
                }
				this.beforeUpdateHisDocHoldTypes = null;
            }
        }
    }
}
