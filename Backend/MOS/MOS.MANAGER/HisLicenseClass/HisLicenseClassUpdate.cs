using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisLicenseClass
{
    partial class HisLicenseClassUpdate : BusinessBase
    {
		private List<HIS_LICENSE_CLASS> beforeUpdateHisLicenseClasss = new List<HIS_LICENSE_CLASS>();
		
        internal HisLicenseClassUpdate()
            : base()
        {

        }

        internal HisLicenseClassUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_LICENSE_CLASS data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisLicenseClassCheck checker = new HisLicenseClassCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_LICENSE_CLASS raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisLicenseClassDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisLicenseClass_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisLicenseClass that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisLicenseClasss.Add(raw);
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

        internal bool UpdateList(List<HIS_LICENSE_CLASS> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisLicenseClassCheck checker = new HisLicenseClassCheck(param);
                List<HIS_LICENSE_CLASS> listRaw = new List<HIS_LICENSE_CLASS>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisLicenseClassDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisLicenseClass_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisLicenseClass that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisLicenseClasss.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisLicenseClasss))
            {
                if (!DAOWorker.HisLicenseClassDAO.UpdateList(this.beforeUpdateHisLicenseClasss))
                {
                    LogSystem.Warn("Rollback du lieu HisLicenseClass that bai, can kiem tra lai." + LogUtil.TraceData("HisLicenseClasss", this.beforeUpdateHisLicenseClasss));
                }
				this.beforeUpdateHisLicenseClasss = null;
            }
        }
    }
}
