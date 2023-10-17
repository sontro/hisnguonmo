using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMediRecord
{
    partial class HisMediRecordUpdate : BusinessBase
    {
		private List<HIS_MEDI_RECORD> beforeUpdateHisMediRecords = new List<HIS_MEDI_RECORD>();
		
        internal HisMediRecordUpdate()
            : base()
        {

        }

        internal HisMediRecordUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDI_RECORD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediRecordCheck checker = new HisMediRecordCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEDI_RECORD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisMediRecordDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediRecord_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediRecord that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisMediRecords.Add(raw);
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

        internal bool UpdateList(List<HIS_MEDI_RECORD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediRecordCheck checker = new HisMediRecordCheck(param);
                List<HIS_MEDI_RECORD> listRaw = new List<HIS_MEDI_RECORD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisMediRecordDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediRecord_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediRecord that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisMediRecords.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMediRecords))
            {
                if (!DAOWorker.HisMediRecordDAO.UpdateList(this.beforeUpdateHisMediRecords))
                {
                    LogSystem.Warn("Rollback du lieu HisMediRecord that bai, can kiem tra lai." + LogUtil.TraceData("HisMediRecords", this.beforeUpdateHisMediRecords));
                }
				this.beforeUpdateHisMediRecords = null;
            }
        }
    }
}
