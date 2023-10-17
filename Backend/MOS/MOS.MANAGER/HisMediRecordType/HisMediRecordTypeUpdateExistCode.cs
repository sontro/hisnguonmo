using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMediRecordType
{
    partial class HisMediRecordTypeUpdate : BusinessBase
    {
		private List<HIS_MEDI_RECORD_TYPE> beforeUpdateHisMediRecordTypes = new List<HIS_MEDI_RECORD_TYPE>();
		
        internal HisMediRecordTypeUpdate()
            : base()
        {

        }

        internal HisMediRecordTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDI_RECORD_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediRecordTypeCheck checker = new HisMediRecordTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEDI_RECORD_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.MEDI_RECORD_TYPE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisMediRecordTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediRecordType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediRecordType that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisMediRecordTypes.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_MEDI_RECORD_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediRecordTypeCheck checker = new HisMediRecordTypeCheck(param);
                List<HIS_MEDI_RECORD_TYPE> listRaw = new List<HIS_MEDI_RECORD_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MEDI_RECORD_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisMediRecordTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediRecordType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediRecordType that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisMediRecordTypes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMediRecordTypes))
            {
                if (!DAOWorker.HisMediRecordTypeDAO.UpdateList(this.beforeUpdateHisMediRecordTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisMediRecordType that bai, can kiem tra lai." + LogUtil.TraceData("HisMediRecordTypes", this.beforeUpdateHisMediRecordTypes));
                }
				this.beforeUpdateHisMediRecordTypes = null;
            }
        }
    }
}
