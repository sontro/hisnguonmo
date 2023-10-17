using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisLicenseClass
{
    partial class HisLicenseClassCreate : BusinessBase
    {
		private List<HIS_LICENSE_CLASS> recentHisLicenseClasss = new List<HIS_LICENSE_CLASS>();
		
        internal HisLicenseClassCreate()
            : base()
        {

        }

        internal HisLicenseClassCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_LICENSE_CLASS data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisLicenseClassCheck checker = new HisLicenseClassCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisLicenseClassDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisLicenseClass_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisLicenseClass that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisLicenseClasss.Add(data);
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
		
		internal bool CreateList(List<HIS_LICENSE_CLASS> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisLicenseClassCheck checker = new HisLicenseClassCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisLicenseClassDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisLicenseClass_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisLicenseClass that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisLicenseClasss.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisLicenseClasss))
            {
                if (!DAOWorker.HisLicenseClassDAO.TruncateList(this.recentHisLicenseClasss))
                {
                    LogSystem.Warn("Rollback du lieu HisLicenseClass that bai, can kiem tra lai." + LogUtil.TraceData("recentHisLicenseClasss", this.recentHisLicenseClasss));
                }
				this.recentHisLicenseClasss = null;
            }
        }
    }
}
