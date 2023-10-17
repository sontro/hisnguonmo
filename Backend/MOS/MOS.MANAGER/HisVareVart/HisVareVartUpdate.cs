using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVareVart
{
    partial class HisVareVartUpdate : BusinessBase
    {
		private List<HIS_VARE_VART> beforeUpdateHisVareVarts = new List<HIS_VARE_VART>();
		
        internal HisVareVartUpdate()
            : base()
        {

        }

        internal HisVareVartUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_VARE_VART data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVareVartCheck checker = new HisVareVartCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_VARE_VART raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisVareVartDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVareVart_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVareVart that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisVareVarts.Add(raw);
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

        internal bool UpdateList(List<HIS_VARE_VART> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVareVartCheck checker = new HisVareVartCheck(param);
                List<HIS_VARE_VART> listRaw = new List<HIS_VARE_VART>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisVareVartDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVareVart_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVareVart that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisVareVarts.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisVareVarts))
            {
                if (!DAOWorker.HisVareVartDAO.UpdateList(this.beforeUpdateHisVareVarts))
                {
                    LogSystem.Warn("Rollback du lieu HisVareVart that bai, can kiem tra lai." + LogUtil.TraceData("HisVareVarts", this.beforeUpdateHisVareVarts));
                }
				this.beforeUpdateHisVareVarts = null;
            }
        }
    }
}
