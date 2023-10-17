using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisIcdGroup
{
    partial class HisIcdGroupCreate : BusinessBase
    {
        private List<HIS_ICD_GROUP> recentHisIcdGroups = new List<HIS_ICD_GROUP>();

        internal HisIcdGroupCreate()
            : base()
        {

        }

        internal HisIcdGroupCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ICD_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisIcdGroupCheck checker = new HisIcdGroupCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisIcdGroupDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisIcdGroup_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisIcdGroup that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisIcdGroups.Add(data);
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

        internal bool CreateList(List<HIS_ICD_GROUP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisIcdGroupCheck checker = new HisIcdGroupCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisIcdGroupDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisIcdGroup_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisIcdGroup that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisIcdGroups.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisIcdGroups))
            {
                if (!new HisIcdGroupTruncate(param).TruncateList(this.recentHisIcdGroups))
                {
                    LogSystem.Warn("Rollback du lieu HisIcdGroup that bai, can kiem tra lai." + LogUtil.TraceData("recentHisIcdGroups", this.recentHisIcdGroups));
                }
            }
        }
    }
}
