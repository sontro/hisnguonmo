using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDhst
{
    partial class HisDhstUpdate : BusinessBase
    {
        private List<HIS_DHST> beforeUpdateHisDhsts = new List<HIS_DHST>();

        internal HisDhstUpdate()
            : base()
        {

        }

        internal HisDhstUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_DHST data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDhstCheck checker = new HisDhstCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_DHST raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (!DAOWorker.HisDhstDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDhst_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDhst that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisDhsts.Add(raw);
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

        internal bool UpdateList(List<HIS_DHST> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDhstCheck checker = new HisDhstCheck(param);
                List<HIS_DHST> listRaw = new List<HIS_DHST>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisDhstDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDhst_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDhst that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisDhsts.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisDhsts))
            {
                if (!DAOWorker.HisDhstDAO.UpdateList(this.beforeUpdateHisDhsts))
                {
                    LogSystem.Warn("Rollback du lieu HisDhst that bai, can kiem tra lai." + LogUtil.TraceData("HisDhsts", this.beforeUpdateHisDhsts));
                }
                this.beforeUpdateHisDhsts = null;
            }
        }
    }
}
