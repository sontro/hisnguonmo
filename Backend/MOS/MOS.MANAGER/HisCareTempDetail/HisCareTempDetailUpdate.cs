using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCareTempDetail
{
    partial class HisCareTempDetailUpdate : BusinessBase
    {
		private List<HIS_CARE_TEMP_DETAIL> beforeUpdateHisCareTempDetails = new List<HIS_CARE_TEMP_DETAIL>();
		
        internal HisCareTempDetailUpdate()
            : base()
        {

        }

        internal HisCareTempDetailUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_CARE_TEMP_DETAIL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCareTempDetailCheck checker = new HisCareTempDetailCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_CARE_TEMP_DETAIL raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisCareTempDetailDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCareTempDetail_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCareTempDetail that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisCareTempDetails.Add(raw);
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

        internal bool UpdateList(List<HIS_CARE_TEMP_DETAIL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCareTempDetailCheck checker = new HisCareTempDetailCheck(param);
                List<HIS_CARE_TEMP_DETAIL> listRaw = new List<HIS_CARE_TEMP_DETAIL>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisCareTempDetailDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCareTempDetail_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCareTempDetail that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisCareTempDetails.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisCareTempDetails))
            {
                if (!DAOWorker.HisCareTempDetailDAO.UpdateList(this.beforeUpdateHisCareTempDetails))
                {
                    LogSystem.Warn("Rollback du lieu HisCareTempDetail that bai, can kiem tra lai." + LogUtil.TraceData("HisCareTempDetails", this.beforeUpdateHisCareTempDetails));
                }
				this.beforeUpdateHisCareTempDetails = null;
            }
        }
    }
}
