using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMediOrg
{
    partial class HisMediOrgUpdate : BusinessBase
    {
        private List<HIS_MEDI_ORG> beforeUpdateHisMediOrgs = new List<HIS_MEDI_ORG>();

        internal HisMediOrgUpdate()
            : base()
        {

        }

        internal HisMediOrgUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDI_ORG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediOrgCheck checker = new HisMediOrgCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEDI_ORG raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.MEDI_ORG_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisMediOrgs.Add(raw);
                    if (!DAOWorker.HisMediOrgDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediOrg_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediOrg that bai." + LogUtil.TraceData("data", data));
                    }

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

        internal bool UpdateList(List<HIS_MEDI_ORG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediOrgCheck checker = new HisMediOrgCheck(param);
                List<HIS_MEDI_ORG> listRaw = new List<HIS_MEDI_ORG>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MEDI_ORG_CODE, data.ID);
                }
                if (valid)
                {
                    this.beforeUpdateHisMediOrgs.AddRange(listRaw);
                    if (!DAOWorker.HisMediOrgDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediOrg_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediOrg that bai." + LogUtil.TraceData("listData", listData));
                    }
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMediOrgs))
            {
                if (!DAOWorker.HisMediOrgDAO.UpdateList(this.beforeUpdateHisMediOrgs))
                {
                    LogSystem.Warn("Rollback du lieu HisMediOrg that bai, can kiem tra lai." + LogUtil.TraceData("HisMediOrgs", this.beforeUpdateHisMediOrgs));
                }
            }
        }
    }
}
