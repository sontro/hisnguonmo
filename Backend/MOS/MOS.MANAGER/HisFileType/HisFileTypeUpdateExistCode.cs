using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisFileType
{
    partial class HisFileTypeUpdate : BusinessBase
    {
		private List<HIS_FILE_TYPE> beforeUpdateHisFileTypes = new List<HIS_FILE_TYPE>();
		
        internal HisFileTypeUpdate()
            : base()
        {

        }

        internal HisFileTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_FILE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisFileTypeCheck checker = new HisFileTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_FILE_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.FILE_TYPE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisFileTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFileType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisFileType that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisFileTypes.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_FILE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisFileTypeCheck checker = new HisFileTypeCheck(param);
                List<HIS_FILE_TYPE> listRaw = new List<HIS_FILE_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.FILE_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisFileTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFileType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisFileType that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisFileTypes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisFileTypes))
            {
                if (!DAOWorker.HisFileTypeDAO.UpdateList(this.beforeUpdateHisFileTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisFileType that bai, can kiem tra lai." + LogUtil.TraceData("HisFileTypes", this.beforeUpdateHisFileTypes));
                }
				this.beforeUpdateHisFileTypes = null;
            }
        }
    }
}
