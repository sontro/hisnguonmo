using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceRati
{
    partial class HisServiceRatiUpdate : BusinessBase
    {
		private List<HIS_SERVICE_RATI> beforeUpdateHisServiceRatis = new List<HIS_SERVICE_RATI>();
		
        internal HisServiceRatiUpdate()
            : base()
        {

        }

        internal HisServiceRatiUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERVICE_RATI data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceRatiCheck checker = new HisServiceRatiCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SERVICE_RATI raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisServiceRatiDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceRati_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceRati that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisServiceRatis.Add(raw);
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

        internal bool UpdateList(List<HIS_SERVICE_RATI> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceRatiCheck checker = new HisServiceRatiCheck(param);
                List<HIS_SERVICE_RATI> listRaw = new List<HIS_SERVICE_RATI>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisServiceRatiDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceRati_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceRati that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisServiceRatis.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisServiceRatis))
            {
                if (!DAOWorker.HisServiceRatiDAO.UpdateList(this.beforeUpdateHisServiceRatis))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceRati that bai, can kiem tra lai." + LogUtil.TraceData("HisServiceRatis", this.beforeUpdateHisServiceRatis));
                }
				this.beforeUpdateHisServiceRatis = null;
            }
        }
    }
}
