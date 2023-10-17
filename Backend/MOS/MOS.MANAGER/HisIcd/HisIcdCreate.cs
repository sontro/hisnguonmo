using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisIcd
{
    partial class HisIcdCreate : BusinessBase
    {
        private List<HIS_ICD> recentHisIcds = new List<HIS_ICD>();

        internal HisIcdCreate()
            : base()
        {

        }

        internal HisIcdCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ICD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisIcdCheck checker = new HisIcdCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ICD_CODE, null);
                if (valid)
                {
                    if (!DAOWorker.HisIcdDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisIcd_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisIcd that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisIcds.Add(data);
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

        internal bool CreateList(List<HIS_ICD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisIcdCheck checker = new HisIcdCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.ICD_CODE, null);
                }
                if (valid)
                {
                    if (!DAOWorker.HisIcdDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisIcd_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisIcd that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisIcds.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisIcds))
            {
                if (!DAOWorker.HisIcdDAO.TruncateList(this.recentHisIcds))
                {
                    LogSystem.Warn("Rollback du lieu HisIcd that bai, can kiem tra lai." + LogUtil.TraceData("recentHisIcds", this.recentHisIcds));
                }
                this.recentHisIcds = null;
            }
        }
    }
}
