using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPackageDetail
{
    partial class HisPackageDetailUpdate : BusinessBase
    {
		private List<HIS_PACKAGE_DETAIL> beforeUpdateHisPackageDetails = new List<HIS_PACKAGE_DETAIL>();
		
        internal HisPackageDetailUpdate()
            : base()
        {

        }

        internal HisPackageDetailUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PACKAGE_DETAIL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPackageDetailCheck checker = new HisPackageDetailCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PACKAGE_DETAIL raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisPackageDetailDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPackageDetail_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPackageDetail that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisPackageDetails.Add(raw);
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

        internal bool UpdateList(List<HIS_PACKAGE_DETAIL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPackageDetailCheck checker = new HisPackageDetailCheck(param);
                List<HIS_PACKAGE_DETAIL> listRaw = new List<HIS_PACKAGE_DETAIL>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisPackageDetailDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPackageDetail_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPackageDetail that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisPackageDetails.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisPackageDetails))
            {
                if (!DAOWorker.HisPackageDetailDAO.UpdateList(this.beforeUpdateHisPackageDetails))
                {
                    LogSystem.Warn("Rollback du lieu HisPackageDetail that bai, can kiem tra lai." + LogUtil.TraceData("HisPackageDetails", this.beforeUpdateHisPackageDetails));
                }
				this.beforeUpdateHisPackageDetails = null;
            }
        }
    }
}
