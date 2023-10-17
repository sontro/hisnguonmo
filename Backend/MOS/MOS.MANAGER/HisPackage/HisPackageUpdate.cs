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

        internal bool UpdateList(List<HIS_PACKAGE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPackageCheck checker = new HisPackageCheck(param);
                List<HIS_PACKAGE> listRaw = new List<HIS_PACKAGE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisPackages.AddRange(listRaw);
					if (!DAOWorker.HisPackageDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPackage_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPackage that bai." + LogUtil.TraceData("listData", listData));
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
