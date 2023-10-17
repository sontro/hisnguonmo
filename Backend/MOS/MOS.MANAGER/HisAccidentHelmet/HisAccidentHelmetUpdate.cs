using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAccidentHelmet
{
    partial class HisAccidentHelmetUpdate : BusinessBase
    {
		private List<HIS_ACCIDENT_HELMET> beforeUpdateHisAccidentHelmets = new List<HIS_ACCIDENT_HELMET>();
		
        internal HisAccidentHelmetUpdate()
            : base()
        {

        }

        internal HisAccidentHelmetUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ACCIDENT_HELMET data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentHelmetCheck checker = new HisAccidentHelmetCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ACCIDENT_HELMET raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisAccidentHelmets.Add(raw);
					if (!DAOWorker.HisAccidentHelmetDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentHelmet_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAccidentHelmet that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_ACCIDENT_HELMET> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAccidentHelmetCheck checker = new HisAccidentHelmetCheck(param);
                List<HIS_ACCIDENT_HELMET> listRaw = new List<HIS_ACCIDENT_HELMET>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisAccidentHelmets.AddRange(listRaw);
					if (!DAOWorker.HisAccidentHelmetDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentHelmet_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAccidentHelmet that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAccidentHelmets))
            {
                if (!new HisAccidentHelmetUpdate(param).UpdateList(this.beforeUpdateHisAccidentHelmets))
                {
                    LogSystem.Warn("Rollback du lieu HisAccidentHelmet that bai, can kiem tra lai." + LogUtil.TraceData("HisAccidentHelmets", this.beforeUpdateHisAccidentHelmets));
                }
            }
        }
    }
}
