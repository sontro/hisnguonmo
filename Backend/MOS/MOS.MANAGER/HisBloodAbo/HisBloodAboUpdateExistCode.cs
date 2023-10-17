using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBloodAbo
{
    partial class HisBloodAboUpdate : BusinessBase
    {
		private List<HIS_BLOOD_ABO> beforeUpdateHisBloodAbos = new List<HIS_BLOOD_ABO>();
		
        internal HisBloodAboUpdate()
            : base()
        {

        }

        internal HisBloodAboUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BLOOD_ABO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBloodAboCheck checker = new HisBloodAboCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BLOOD_ABO raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.BLOOD_ABO_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisBloodAbos.Add(raw);
					if (!DAOWorker.HisBloodAboDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBloodAbo_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBloodAbo that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_BLOOD_ABO> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBloodAboCheck checker = new HisBloodAboCheck(param);
                List<HIS_BLOOD_ABO> listRaw = new List<HIS_BLOOD_ABO>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.BLOOD_ABO_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisBloodAbos.AddRange(listRaw);
					if (!DAOWorker.HisBloodAboDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBloodAbo_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBloodAbo that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBloodAbos))
            {
                if (!new HisBloodAboUpdate(param).UpdateList(this.beforeUpdateHisBloodAbos))
                {
                    LogSystem.Warn("Rollback du lieu HisBloodAbo that bai, can kiem tra lai." + LogUtil.TraceData("HisBloodAbos", this.beforeUpdateHisBloodAbos));
                }
            }
        }
    }
}
