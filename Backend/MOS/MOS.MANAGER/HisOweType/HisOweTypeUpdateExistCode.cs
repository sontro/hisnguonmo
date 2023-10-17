using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisOweType
{
    partial class HisOweTypeUpdate : BusinessBase
    {
		private List<HIS_OWE_TYPE> beforeUpdateHisOweTypes = new List<HIS_OWE_TYPE>();
		
        internal HisOweTypeUpdate()
            : base()
        {

        }

        internal HisOweTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_OWE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisOweTypeCheck checker = new HisOweTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_OWE_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.OWE_TYPE_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisOweTypes.Add(raw);
					if (!DAOWorker.HisOweTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisOweType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisOweType that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_OWE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisOweTypeCheck checker = new HisOweTypeCheck(param);
                List<HIS_OWE_TYPE> listRaw = new List<HIS_OWE_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.OWE_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisOweTypes.AddRange(listRaw);
					if (!DAOWorker.HisOweTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisOweType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisOweType that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisOweTypes))
            {
                if (!new HisOweTypeUpdate(param).UpdateList(this.beforeUpdateHisOweTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisOweType that bai, can kiem tra lai." + LogUtil.TraceData("HisOweTypes", this.beforeUpdateHisOweTypes));
                }
            }
        }
    }
}
