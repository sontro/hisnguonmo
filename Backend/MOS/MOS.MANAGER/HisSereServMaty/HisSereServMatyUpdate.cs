using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServMaty
{
    partial class HisSereServMatyUpdate : BusinessBase
    {
        private List<HIS_SERE_SERV_MATY> beforeUpdateHisSereServMatys = new List<HIS_SERE_SERV_MATY>();

        internal HisSereServMatyUpdate()
            : base()
        {

        }

        internal HisSereServMatyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERE_SERV_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServMatyCheck checker = new HisSereServMatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SERE_SERV_MATY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisSereServMatys.Add(raw);
                    if (!DAOWorker.HisSereServMatyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServMaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServMaty that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_SERE_SERV_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServMatyCheck checker = new HisSereServMatyCheck(param);
                List<HIS_SERE_SERV_MATY> listRaw = new List<HIS_SERE_SERV_MATY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisSereServMatys.AddRange(listRaw);
                    if (!DAOWorker.HisSereServMatyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServMaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServMaty that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisSereServMatys))
            {
                if (!DAOWorker.HisSereServMatyDAO.UpdateList(this.beforeUpdateHisSereServMatys))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServMaty that bai, can kiem tra lai." + LogUtil.TraceData("HisSereServMatys", this.beforeUpdateHisSereServMatys));
                }
            }
        }
    }
}
