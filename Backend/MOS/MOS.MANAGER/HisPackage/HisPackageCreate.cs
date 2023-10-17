using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPackage
{
    partial class HisPackageCreate : BusinessBase
    {
		private List<HIS_PACKAGE> recentHisPackages = new List<HIS_PACKAGE>();
		
        internal HisPackageCreate()
            : base()
        {

        }

        internal HisPackageCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PACKAGE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPackageCheck checker = new HisPackageCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisPackageDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPackage_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPackage that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPackages.Add(data);
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
		
		internal bool CreateList(List<HIS_PACKAGE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPackageCheck checker = new HisPackageCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisPackageDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPackage_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPackage that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisPackages.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisPackages))
            {
                if (!new HisPackageTruncate(param).TruncateList(this.recentHisPackages))
                {
                    LogSystem.Warn("Rollback du lieu HisPackage that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPackages", this.recentHisPackages));
                }
            }
        }
    }
}
