using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBloodRh
{
    partial class HisBloodRhUpdate : BusinessBase
    {
		private HIS_BLOOD_RH beforeUpdateHisBloodRhDTO;
		private List<HIS_BLOOD_RH> beforeUpdateHisBloodRhDTOs;
		
        internal HisBloodRhUpdate()
            : base()
        {

        }

        internal HisBloodRhUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BLOOD_RH data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBloodRhCheck checker = new HisBloodRhCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BLOOD_RH raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.BLOOD_RH_CODE, data.ID);
                if (valid)
                {
					this.beforeUpdateHisBloodRhDTO = raw;
					if (!DAOWorker.HisBloodRhDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBloodRh_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBloodRh that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_BLOOD_RH> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBloodRhCheck checker = new HisBloodRhCheck(param);
                List<HIS_BLOOD_RH> listRaw = new List<HIS_BLOOD_RH>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.BLOOD_RH_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisBloodRhDTOs = listRaw;
					if (!DAOWorker.HisBloodRhDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBloodRh_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBloodRh that bai." + LogUtil.TraceData("listData", listData));
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
            if (this.beforeUpdateHisBloodRhDTO != null)
            {
                if (!new HisBloodRhUpdate(param).Update(this.beforeUpdateHisBloodRhDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisBloodRh that bai, can kiem tra lai." + LogUtil.TraceData("HisBloodRhDTO", this.beforeUpdateHisBloodRhDTO));
                }
            }
			
			if (this.beforeUpdateHisBloodRhDTOs != null)
            {
                if (!new HisBloodRhUpdate(param).UpdateList(this.beforeUpdateHisBloodRhDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisBloodRh that bai, can kiem tra lai." + LogUtil.TraceData("HisBloodRhDTOs", this.beforeUpdateHisBloodRhDTOs));
                }
            }
        }
    }
}
