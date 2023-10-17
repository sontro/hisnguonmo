using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPackage
{
    partial class HisPackageUpdate : BusinessBase
    {
		private List<HIS_PACKAGE> beforeUpdateHisPackages = new List<HIS_PACKAGE>();
		
        internal HisPackageUpdate()
            : base()
        {

        }

        internal HisPackageUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PACKAGE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPackageCheck checker = new HisPackageCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PACKAGE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.PACKAGE_CODE, data.ID);
                valid = valid && checker.IsValidPackage(data);
                if (valid)
                {
                    this.beforeUpdateHisPackages.Add(raw);
					if (!DAOWorker.HisPackageDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPackage_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPackage that bai." + LogUtil.TraceData("data", data));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisPackages))
            {
                if (!DAOWorker.HisPackageDAO.UpdateList(this.beforeUpdateHisPackages))
                {
                    LogSystem.Warn("Rollback du lieu HisPackage that bai, can kiem tra lai." + LogUtil.TraceData("HisPackages", this.beforeUpdateHisPackages));
                }
            }
        }
    }
}
