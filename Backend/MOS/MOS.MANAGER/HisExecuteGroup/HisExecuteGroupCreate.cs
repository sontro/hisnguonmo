using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExecuteGroup
{
    partial class HisExecuteGroupCreate : BusinessBase
    {
        private List<HIS_EXECUTE_GROUP> recentHisExecuteGroups = new List<HIS_EXECUTE_GROUP>();

        internal HisExecuteGroupCreate()
            : base()
        {

        }

        internal HisExecuteGroupCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EXECUTE_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExecuteGroupCheck checker = new HisExecuteGroupCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.EXECUTE_GROUP_CODE, null);
                if (valid)
                {
                    if (!DAOWorker.HisExecuteGroupDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExecuteGroup_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExecuteGroup that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisExecuteGroups.Add(data);
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

        internal bool CreateList(List<HIS_EXECUTE_GROUP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExecuteGroupCheck checker = new HisExecuteGroupCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisExecuteGroupDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExecuteGroup_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExecuteGroup that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisExecuteGroups.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisExecuteGroups))
            {
                if (!new HisExecuteGroupTruncate(param).TruncateList(this.recentHisExecuteGroups))
                {
                    LogSystem.Warn("Rollback du lieu HisExecuteGroup that bai, can kiem tra lai." + LogUtil.TraceData("recentHisExecuteGroups", this.recentHisExecuteGroups));
                }
            }
        }
    }
}
