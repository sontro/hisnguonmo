using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisUnlimitType
{
    partial class HisUnlimitTypeUpdate : BusinessBase
    {
		private List<HIS_UNLIMIT_TYPE> beforeUpdateHisUnlimitTypes = new List<HIS_UNLIMIT_TYPE>();
		
        internal HisUnlimitTypeUpdate()
            : base()
        {

        }

        internal HisUnlimitTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_UNLIMIT_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisUnlimitTypeCheck checker = new HisUnlimitTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_UNLIMIT_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.UNLIMIT_TYPE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisUnlimitTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisUnlimitType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisUnlimitType that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisUnlimitTypes.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_UNLIMIT_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisUnlimitTypeCheck checker = new HisUnlimitTypeCheck(param);
                List<HIS_UNLIMIT_TYPE> listRaw = new List<HIS_UNLIMIT_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.UNLIMIT_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisUnlimitTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisUnlimitType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisUnlimitType that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisUnlimitTypes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisUnlimitTypes))
            {
                if (!DAOWorker.HisUnlimitTypeDAO.UpdateList(this.beforeUpdateHisUnlimitTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisUnlimitType that bai, can kiem tra lai." + LogUtil.TraceData("HisUnlimitTypes", this.beforeUpdateHisUnlimitTypes));
                }
				this.beforeUpdateHisUnlimitTypes = null;
            }
        }
    }
}
