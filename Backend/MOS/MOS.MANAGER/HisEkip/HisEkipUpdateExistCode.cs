using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEkip
{
    partial class HisEkipUpdate : BusinessBase
    {
		private List<HIS_EKIP> beforeUpdateHisEkips = new List<HIS_EKIP>();
		
        internal HisEkipUpdate()
            : base()
        {

        }

        internal HisEkipUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EKIP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEkipCheck checker = new HisEkipCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EKIP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.EKIP_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisEkips.Add(raw);
					if (!DAOWorker.HisEkipDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEkip_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEkip that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_EKIP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEkipCheck checker = new HisEkipCheck(param);
                List<HIS_EKIP> listRaw = new List<HIS_EKIP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.EKIP_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisEkips.AddRange(listRaw);
					if (!DAOWorker.HisEkipDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEkip_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEkip that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisEkips))
            {
                if (!new HisEkipUpdate(param).UpdateList(this.beforeUpdateHisEkips))
                {
                    LogSystem.Warn("Rollback du lieu HisEkip that bai, can kiem tra lai." + LogUtil.TraceData("HisEkips", this.beforeUpdateHisEkips));
                }
            }
        }
    }
}
