using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisWelfareType
{
    partial class HisWelfareTypeUpdate : BusinessBase
    {
		private List<HIS_WELFARE_TYPE> beforeUpdateHisWelfareTypes = new List<HIS_WELFARE_TYPE>();
		
        internal HisWelfareTypeUpdate()
            : base()
        {

        }

        internal HisWelfareTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_WELFARE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisWelfareTypeCheck checker = new HisWelfareTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_WELFARE_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.WELFARE_TYPE_CODE, data.ID);
                if (valid)
                {                    
					if (!DAOWorker.HisWelfareTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisWelfareType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisWelfareType that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisWelfareTypes.Add(raw);
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

        internal bool UpdateList(List<HIS_WELFARE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisWelfareTypeCheck checker = new HisWelfareTypeCheck(param);
                List<HIS_WELFARE_TYPE> listRaw = new List<HIS_WELFARE_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.WELFARE_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisWelfareTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisWelfareType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisWelfareType that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisWelfareTypes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisWelfareTypes))
            {
                if (!DAOWorker.HisWelfareTypeDAO.UpdateList(this.beforeUpdateHisWelfareTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisWelfareType that bai, can kiem tra lai." + LogUtil.TraceData("HisWelfareTypes", this.beforeUpdateHisWelfareTypes));
                }
				this.beforeUpdateHisWelfareTypes = null;
            }
        }
    }
}
