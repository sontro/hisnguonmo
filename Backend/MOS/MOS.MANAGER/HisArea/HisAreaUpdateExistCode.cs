using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisArea
{
    partial class HisAreaUpdate : BusinessBase
    {
		private List<HIS_AREA> beforeUpdateHisAreas = new List<HIS_AREA>();
		
        internal HisAreaUpdate()
            : base()
        {

        }

        internal HisAreaUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_AREA data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAreaCheck checker = new HisAreaCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_AREA raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.AREA_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisAreaDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisArea_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisArea that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisAreas.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_AREA> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAreaCheck checker = new HisAreaCheck(param);
                List<HIS_AREA> listRaw = new List<HIS_AREA>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.AREA_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisAreaDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisArea_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisArea that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisAreas.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAreas))
            {
                if (!DAOWorker.HisAreaDAO.UpdateList(this.beforeUpdateHisAreas))
                {
                    LogSystem.Warn("Rollback du lieu HisArea that bai, can kiem tra lai." + LogUtil.TraceData("HisAreas", this.beforeUpdateHisAreas));
                }
				this.beforeUpdateHisAreas = null;
            }
        }
    }
}
