using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBloodGiver
{
    partial class HisBloodGiverUpdate : BusinessBase
    {
		private List<HIS_BLOOD_GIVER> beforeUpdateHisBloodGivers = new List<HIS_BLOOD_GIVER>();
		
        internal HisBloodGiverUpdate()
            : base()
        {

        }

        internal HisBloodGiverUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BLOOD_GIVER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBloodGiverCheck checker = new HisBloodGiverCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BLOOD_GIVER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.GIVE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisBloodGiverDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBloodGiver_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBloodGiver that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisBloodGivers.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_BLOOD_GIVER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBloodGiverCheck checker = new HisBloodGiverCheck(param);
                List<HIS_BLOOD_GIVER> listRaw = new List<HIS_BLOOD_GIVER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.GIVE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisBloodGiverDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBloodGiver_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBloodGiver that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisBloodGivers.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBloodGivers))
            {
                if (!DAOWorker.HisBloodGiverDAO.UpdateList(this.beforeUpdateHisBloodGivers))
                {
                    LogSystem.Warn("Rollback du lieu HisBloodGiver that bai, can kiem tra lai." + LogUtil.TraceData("HisBloodGivers", this.beforeUpdateHisBloodGivers));
                }
				this.beforeUpdateHisBloodGivers = null;
            }
        }
    }
}
