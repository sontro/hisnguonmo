using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExamSereDire
{
    partial class HisExamSereDireUpdate : BusinessBase
    {
		private HIS_EXAM_SERE_DIRE beforeUpdateHisExamSereDireDTO;
		private List<HIS_EXAM_SERE_DIRE> beforeUpdateHisExamSereDireDTOs;
		
        internal HisExamSereDireUpdate()
            : base()
        {

        }

        internal HisExamSereDireUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EXAM_SERE_DIRE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExamSereDireCheck checker = new HisExamSereDireCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EXAM_SERE_DIRE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
					this.beforeUpdateHisExamSereDireDTO = raw;
                    if (!DAOWorker.HisExamSereDireDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExamSereDire_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExamSereDire that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_EXAM_SERE_DIRE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExamSereDireCheck checker = new HisExamSereDireCheck(param);
                List<HIS_EXAM_SERE_DIRE> listRaw = new List<HIS_EXAM_SERE_DIRE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisExamSereDireDTOs = listRaw;
                    if (!DAOWorker.HisExamSereDireDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExamSereDire_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExamSereDire that bai." + LogUtil.TraceData("listData", listData));
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
            if (this.beforeUpdateHisExamSereDireDTO != null)
            {
                if (!new HisExamSereDireUpdate(param).Update(this.beforeUpdateHisExamSereDireDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisExamSereDire that bai, can kiem tra lai." + LogUtil.TraceData("HisExamSereDireDTO", this.beforeUpdateHisExamSereDireDTO));
                }
            }
			
			if (this.beforeUpdateHisExamSereDireDTOs != null)
            {
                if (!new HisExamSereDireUpdate(param).UpdateList(this.beforeUpdateHisExamSereDireDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisExamSereDire that bai, can kiem tra lai." + LogUtil.TraceData("HisExamSereDireDTOs", this.beforeUpdateHisExamSereDireDTOs));
                }
            }
        }
    }
}
