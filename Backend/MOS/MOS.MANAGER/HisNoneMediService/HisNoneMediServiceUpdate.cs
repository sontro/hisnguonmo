using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisNoneMediService
{
    partial class HisNoneMediServiceUpdate : BusinessBase
    {
		private List<HIS_NONE_MEDI_SERVICE> beforeUpdateHisNoneMediServices = new List<HIS_NONE_MEDI_SERVICE>();
		
        internal HisNoneMediServiceUpdate()
            : base()
        {

        }

        internal HisNoneMediServiceUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_NONE_MEDI_SERVICE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisNoneMediServiceCheck checker = new HisNoneMediServiceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_NONE_MEDI_SERVICE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisNoneMediServiceDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisNoneMediService_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisNoneMediService that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisNoneMediServices.Add(raw);
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

        internal bool UpdateList(List<HIS_NONE_MEDI_SERVICE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisNoneMediServiceCheck checker = new HisNoneMediServiceCheck(param);
                List<HIS_NONE_MEDI_SERVICE> listRaw = new List<HIS_NONE_MEDI_SERVICE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisNoneMediServiceDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisNoneMediService_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisNoneMediService that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisNoneMediServices.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisNoneMediServices))
            {
                if (!DAOWorker.HisNoneMediServiceDAO.UpdateList(this.beforeUpdateHisNoneMediServices))
                {
                    LogSystem.Warn("Rollback du lieu HisNoneMediService that bai, can kiem tra lai." + LogUtil.TraceData("HisNoneMediServices", this.beforeUpdateHisNoneMediServices));
                }
				this.beforeUpdateHisNoneMediServices = null;
            }
        }
    }
}
