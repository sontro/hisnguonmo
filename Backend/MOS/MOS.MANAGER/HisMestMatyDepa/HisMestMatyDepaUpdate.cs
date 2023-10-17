using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMestMatyDepa
{
    partial class HisMestMatyDepaUpdate : BusinessBase
    {
        private List<HIS_MEST_MATY_DEPA> beforeUpdateHisMestMatyDepas = new List<HIS_MEST_MATY_DEPA>();

        internal HisMestMatyDepaUpdate()
            : base()
        {

        }

        internal HisMestMatyDepaUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEST_MATY_DEPA data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestMatyDepaCheck checker = new HisMestMatyDepaCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEST_MATY_DEPA raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsNotExists(data, data.ID);
                if (valid)
                {
                    if (!DAOWorker.HisMestMatyDepaDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestMatyDepa_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMestMatyDepa that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisMestMatyDepas.Add(raw);
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

        internal bool UpdateList(List<HIS_MEST_MATY_DEPA> listData, List<HIS_MEST_MATY_DEPA> listBefore, bool notCheckExists)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestMatyDepaCheck checker = new HisMestMatyDepaCheck(param);
                valid = valid && checker.IsUnLock(listBefore);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && (notCheckExists || checker.IsNotExists(data, data.ID));
                }
                if (valid)
                {
                    if (!DAOWorker.HisMestMatyDepaDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestMatyDepa_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMestMatyDepa that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisMestMatyDepas.AddRange(listBefore);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMestMatyDepas))
            {
                if (!DAOWorker.HisMestMatyDepaDAO.UpdateList(this.beforeUpdateHisMestMatyDepas))
                {
                    LogSystem.Warn("Rollback du lieu HisMestMatyDepa that bai, can kiem tra lai." + LogUtil.TraceData("HisMestMatyDepas", this.beforeUpdateHisMestMatyDepas));
                }
                this.beforeUpdateHisMestMatyDepas = null;
            }
        }
    }
}
