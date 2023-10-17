using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMestMetyDepa
{
    partial class HisMestMetyDepaUpdate : BusinessBase
    {
        private List<HIS_MEST_METY_DEPA> beforeUpdateHisMestMetyDepas = new List<HIS_MEST_METY_DEPA>();

        internal HisMestMetyDepaUpdate()
            : base()
        {

        }

        internal HisMestMetyDepaUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEST_METY_DEPA data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestMetyDepaCheck checker = new HisMestMetyDepaCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEST_METY_DEPA raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsNotExists(data, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisMestMetyDepas.Add(raw);
                    if (!DAOWorker.HisMestMetyDepaDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestMetyDepa_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMestMetyDepa that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_MEST_METY_DEPA> listData, List<HIS_MEST_METY_DEPA> listBefore, bool notCheckExists)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestMetyDepaCheck checker = new HisMestMetyDepaCheck(param);
                valid = valid && checker.IsUnLock(listBefore);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && (notCheckExists || checker.IsNotExists(data, data.ID));
                }
                if (valid)
                {
                    if (!DAOWorker.HisMestMetyDepaDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestMetyDepa_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMestMetyDepa that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisMestMetyDepas.AddRange(listBefore);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMestMetyDepas))
            {
                if (!DAOWorker.HisMestMetyDepaDAO.UpdateList(this.beforeUpdateHisMestMetyDepas))
                {
                    LogSystem.Warn("Rollback du lieu HisMestMetyDepa that bai, can kiem tra lai." + LogUtil.TraceData("HisMestMetyDepas", this.beforeUpdateHisMestMetyDepas));
                }
            }
        }
    }
}
