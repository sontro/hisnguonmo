using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserGroupTempDt
{
    partial class HisUserGroupTempDtCreate : BusinessBase
    {
		private List<HIS_USER_GROUP_TEMP_DT> recentHisUserGroupTempDts = new List<HIS_USER_GROUP_TEMP_DT>();
		
        internal HisUserGroupTempDtCreate()
            : base()
        {

        }

        internal HisUserGroupTempDtCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_USER_GROUP_TEMP_DT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisUserGroupTempDtCheck checker = new HisUserGroupTempDtCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisUserGroupTempDtDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisUserGroupTempDt_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisUserGroupTempDt that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisUserGroupTempDts.Add(data);
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
		
		internal bool CreateList(List<HIS_USER_GROUP_TEMP_DT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisUserGroupTempDtCheck checker = new HisUserGroupTempDtCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisUserGroupTempDtDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisUserGroupTempDt_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisUserGroupTempDt that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisUserGroupTempDts.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisUserGroupTempDts))
            {
                if (!DAOWorker.HisUserGroupTempDtDAO.TruncateList(this.recentHisUserGroupTempDts))
                {
                    LogSystem.Warn("Rollback du lieu HisUserGroupTempDt that bai, can kiem tra lai." + LogUtil.TraceData("recentHisUserGroupTempDts", this.recentHisUserGroupTempDts));
                }
				this.recentHisUserGroupTempDts = null;
            }
        }
    }
}
