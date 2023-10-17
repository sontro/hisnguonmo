using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBidBloodType
{
    partial class HisBidBloodTypeUpdate : BusinessBase
    {
		private List<HIS_BID_BLOOD_TYPE> beforeUpdateHisBidBloodTypes = new List<HIS_BID_BLOOD_TYPE>();
		
        internal HisBidBloodTypeUpdate()
            : base()
        {

        }

        internal HisBidBloodTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BID_BLOOD_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBidBloodTypeCheck checker = new HisBidBloodTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BID_BLOOD_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.BID_BLOOD_TYPE_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisBidBloodTypes.Add(raw);
					if (!DAOWorker.HisBidBloodTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBidBloodType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBidBloodType that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_BID_BLOOD_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBidBloodTypeCheck checker = new HisBidBloodTypeCheck(param);
                List<HIS_BID_BLOOD_TYPE> listRaw = new List<HIS_BID_BLOOD_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.BID_BLOOD_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisBidBloodTypes.AddRange(listRaw);
					if (!DAOWorker.HisBidBloodTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBidBloodType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBidBloodType that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBidBloodTypes))
            {
                if (!new HisBidBloodTypeUpdate(param).UpdateList(this.beforeUpdateHisBidBloodTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisBidBloodType that bai, can kiem tra lai." + LogUtil.TraceData("HisBidBloodTypes", this.beforeUpdateHisBidBloodTypes));
                }
            }
        }
    }
}
