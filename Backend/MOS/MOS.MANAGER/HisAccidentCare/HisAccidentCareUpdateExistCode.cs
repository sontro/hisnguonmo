using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAccidentCare
{
    partial class HisAccidentCareUpdate : BusinessBase
    {
		private List<HIS_ACCIDENT_CARE> beforeUpdateHisAccidentCares = new List<HIS_ACCIDENT_CARE>();
		
        internal HisAccidentCareUpdate()
            : base()
        {

        }

        internal HisAccidentCareUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ACCIDENT_CARE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentCareCheck checker = new HisAccidentCareCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ACCIDENT_CARE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.ACCIDENT_CARE_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisAccidentCares.Add(raw);
					if (!DAOWorker.HisAccidentCareDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentCare_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAccidentCare that bai." + LogUtil.TraceData("data", data));
                    }
                    
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

        internal bool UpdateList(List<HIS_ACCIDENT_CARE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAccidentCareCheck checker = new HisAccidentCareCheck(param);
                List<HIS_ACCIDENT_CARE> listRaw = new List<HIS_ACCIDENT_CARE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.ACCIDENT_CARE_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisAccidentCares.AddRange(listRaw);
					if (!DAOWorker.HisAccidentCareDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentCare_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAccidentCare that bai." + LogUtil.TraceData("listData", listData));
                    }
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAccidentCares))
            {
                if (!new HisAccidentCareUpdate(param).UpdateList(this.beforeUpdateHisAccidentCares))
                {
                    LogSystem.Warn("Rollback du lieu HisAccidentCare that bai, can kiem tra lai." + LogUtil.TraceData("HisAccidentCares", this.beforeUpdateHisAccidentCares));
                }
            }
        }
    }
}
