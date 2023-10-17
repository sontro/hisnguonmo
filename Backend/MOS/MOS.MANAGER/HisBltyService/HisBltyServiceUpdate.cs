using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBltyService
{
    partial class HisBltyServiceUpdate : BusinessBase
    {
		private List<HIS_BLTY_SERVICE> beforeUpdateHisBltyServices = new List<HIS_BLTY_SERVICE>();
		
        internal HisBltyServiceUpdate()
            : base()
        {

        }

        internal HisBltyServiceUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BLTY_SERVICE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBltyServiceCheck checker = new HisBltyServiceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BLTY_SERVICE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsNotExists(data);
                if (valid)
                {                    
					if (!DAOWorker.HisBltyServiceDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBltyService_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBltyService that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisBltyServices.Add(raw);
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

        internal bool UpdateList(List<HIS_BLTY_SERVICE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBltyServiceCheck checker = new HisBltyServiceCheck(param);
                List<HIS_BLTY_SERVICE> listRaw = new List<HIS_BLTY_SERVICE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsNotExists(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisBltyServiceDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBltyService_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBltyService that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisBltyServices.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBltyServices))
            {
                if (!DAOWorker.HisBltyServiceDAO.UpdateList(this.beforeUpdateHisBltyServices))
                {
                    LogSystem.Warn("Rollback du lieu HisBltyService that bai, can kiem tra lai." + LogUtil.TraceData("HisBltyServices", this.beforeUpdateHisBltyServices));
                }
				this.beforeUpdateHisBltyServices = null;
            }
        }
    }
}
