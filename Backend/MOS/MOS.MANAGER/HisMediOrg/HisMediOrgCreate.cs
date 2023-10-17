using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediOrg
{
    partial class HisMediOrgCreate : BusinessBase
    {
        private List<HIS_MEDI_ORG> recentHisMediOrgs = new List<HIS_MEDI_ORG>();

        internal HisMediOrgCreate()
            : base()
        {

        }

        internal HisMediOrgCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDI_ORG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediOrgCheck checker = new HisMediOrgCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.MEDI_ORG_CODE, null);
                if (valid)
                {
                    if (!DAOWorker.HisMediOrgDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediOrg_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMediOrg that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMediOrgs.Add(data);
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

        internal bool CreateList(List<HIS_MEDI_ORG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediOrgCheck checker = new HisMediOrgCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MEDI_ORG_CODE, null);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMediOrgDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediOrg_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMediOrg that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMediOrgs.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMediOrgs))
            {
                if (!new HisMediOrgTruncate(param).TruncateList(this.recentHisMediOrgs))
                {
                    LogSystem.Warn("Rollback du lieu HisMediOrg that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMediOrgs", this.recentHisMediOrgs));
                }
            }
        }
    }
}
