using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPermission
{
    partial class HisPermissionCreate : BusinessBase
    {
		private List<HIS_PERMISSION> recentHisPermissions = new List<HIS_PERMISSION>();
		
        internal HisPermissionCreate()
            : base()
        {

        }

        internal HisPermissionCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PERMISSION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPermissionCheck checker = new HisPermissionCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisPermissionDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPermission_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPermission that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPermissions.Add(data);
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
		
		internal bool CreateList(List<HIS_PERMISSION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPermissionCheck checker = new HisPermissionCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisPermissionDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPermission_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPermission that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisPermissions.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisPermissions))
            {
                if (!DAOWorker.HisPermissionDAO.TruncateList(this.recentHisPermissions))
                {
                    LogSystem.Warn("Rollback du lieu HisPermission that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPermissions", this.recentHisPermissions));
                }
				this.recentHisPermissions = null;
            }
        }
    }
}
