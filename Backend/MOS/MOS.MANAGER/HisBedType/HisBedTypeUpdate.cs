using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBedType
{
    partial class HisBedTypeUpdate : BusinessBase
    {
		private List<HIS_BED_TYPE> beforeUpdateHisBedTypes = new List<HIS_BED_TYPE>();
		
        internal HisBedTypeUpdate()
            : base()
        {

        }

        internal HisBedTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BED_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBedTypeCheck checker = new HisBedTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BED_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisBedTypes.Add(raw);
					if (!DAOWorker.HisBedTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBedType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBedType that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_BED_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBedTypeCheck checker = new HisBedTypeCheck(param);
                List<HIS_BED_TYPE> listRaw = new List<HIS_BED_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisBedTypes.AddRange(listRaw);
					if (!DAOWorker.HisBedTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBedType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBedType that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBedTypes))
            {
                if (!new HisBedTypeUpdate(param).UpdateList(this.beforeUpdateHisBedTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisBedType that bai, can kiem tra lai." + LogUtil.TraceData("HisBedTypes", this.beforeUpdateHisBedTypes));
                }
            }
        }
    }
}
