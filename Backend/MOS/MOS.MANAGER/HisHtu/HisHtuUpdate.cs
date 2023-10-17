using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisHtu
{
    partial class HisHtuUpdate : BusinessBase
    {
		private List<HIS_HTU> beforeUpdateHisHtus = new List<HIS_HTU>();
		
        internal HisHtuUpdate()
            : base()
        {

        }

        internal HisHtuUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_HTU data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHtuCheck checker = new HisHtuCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_HTU raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisHtus.Add(raw);
					if (!DAOWorker.HisHtuDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHtu_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHtu that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_HTU> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHtuCheck checker = new HisHtuCheck(param);
                List<HIS_HTU> listRaw = new List<HIS_HTU>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisHtus.AddRange(listRaw);
					if (!DAOWorker.HisHtuDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHtu_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHtu that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisHtus))
            {
                if (!new HisHtuUpdate(param).UpdateList(this.beforeUpdateHisHtus))
                {
                    LogSystem.Warn("Rollback du lieu HisHtu that bai, can kiem tra lai." + LogUtil.TraceData("HisHtus", this.beforeUpdateHisHtus));
                }
            }
        }
    }
}
