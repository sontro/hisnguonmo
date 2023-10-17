using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisProgram
{
    partial class HisProgramUpdate : BusinessBase
    {
        private List<HIS_PROGRAM> beforeUpdateHisPrograms = new List<HIS_PROGRAM>();

        internal HisProgramUpdate()
            : base()
        {

        }

        internal HisProgramUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PROGRAM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisProgramCheck checker = new HisProgramCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PROGRAM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.PROGRAM_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisPrograms.Add(raw);
                    if (!DAOWorker.HisProgramDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisProgram_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisProgram that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_PROGRAM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisProgramCheck checker = new HisProgramCheck(param);
                List<HIS_PROGRAM> listRaw = new List<HIS_PROGRAM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.PROGRAM_CODE, data.ID);
                }
                if (valid)
                {
                    this.beforeUpdateHisPrograms.AddRange(listRaw);
                    if (!DAOWorker.HisProgramDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisProgram_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisProgram that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisPrograms))
            {
                if (!new HisProgramUpdate(param).UpdateList(this.beforeUpdateHisPrograms))
                {
                    LogSystem.Warn("Rollback du lieu HisProgram that bai, can kiem tra lai." + LogUtil.TraceData("HisPrograms", this.beforeUpdateHisPrograms));
                }
            }
        }
    }
}
