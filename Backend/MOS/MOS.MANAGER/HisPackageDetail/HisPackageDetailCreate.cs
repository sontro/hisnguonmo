using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPackageDetail
{
    partial class HisPackageDetailCreate : BusinessBase
    {
		private List<HIS_PACKAGE_DETAIL> recentHisPackageDetails = new List<HIS_PACKAGE_DETAIL>();
		
        internal HisPackageDetailCreate()
            : base()
        {

        }

        internal HisPackageDetailCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PACKAGE_DETAIL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPackageDetailCheck checker = new HisPackageDetailCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisPackageDetailDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPackageDetail_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPackageDetail that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPackageDetails.Add(data);
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
		
		internal bool CreateList(List<HIS_PACKAGE_DETAIL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPackageDetailCheck checker = new HisPackageDetailCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisPackageDetailDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPackageDetail_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPackageDetail that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisPackageDetails.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisPackageDetails))
            {
                if (!DAOWorker.HisPackageDetailDAO.TruncateList(this.recentHisPackageDetails))
                {
                    LogSystem.Warn("Rollback du lieu HisPackageDetail that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPackageDetails", this.recentHisPackageDetails));
                }
				this.recentHisPackageDetails = null;
            }
        }
    }
}
