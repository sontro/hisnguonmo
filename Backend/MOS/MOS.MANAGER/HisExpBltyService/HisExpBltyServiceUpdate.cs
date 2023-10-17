using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpBltyService
{
    partial class HisExpBltyServiceUpdate : BusinessBase
    {
		private List<HIS_EXP_BLTY_SERVICE> beforeUpdateHisExpBltyServices = new List<HIS_EXP_BLTY_SERVICE>();
		
        internal HisExpBltyServiceUpdate()
            : base()
        {

        }

        internal HisExpBltyServiceUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EXP_BLTY_SERVICE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpBltyServiceCheck checker = new HisExpBltyServiceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EXP_BLTY_SERVICE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisExpBltyServiceDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpBltyService_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpBltyService that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisExpBltyServices.Add(raw);
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

        internal bool UpdateList(List<HIS_EXP_BLTY_SERVICE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpBltyServiceCheck checker = new HisExpBltyServiceCheck(param);
                List<HIS_EXP_BLTY_SERVICE> listRaw = new List<HIS_EXP_BLTY_SERVICE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisExpBltyServiceDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpBltyService_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpBltyService that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisExpBltyServices.AddRange(listRaw);
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

        internal bool UpdateList(List<HIS_EXP_BLTY_SERVICE> listData, List<HIS_EXP_BLTY_SERVICE> listBefore)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpBltyServiceCheck checker = new HisExpBltyServiceCheck(param);
                valid = valid && checker.IsUnLock(listBefore);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisExpBltyServiceDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpBltyService_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpBltyService that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisExpBltyServices.AddRange(listBefore);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisExpBltyServices))
            {
                if (!DAOWorker.HisExpBltyServiceDAO.UpdateList(this.beforeUpdateHisExpBltyServices))
                {
                    LogSystem.Warn("Rollback du lieu HisExpBltyService that bai, can kiem tra lai." + LogUtil.TraceData("HisExpBltyServices", this.beforeUpdateHisExpBltyServices));
                }
				this.beforeUpdateHisExpBltyServices = null;
            }
        }
    }
}
