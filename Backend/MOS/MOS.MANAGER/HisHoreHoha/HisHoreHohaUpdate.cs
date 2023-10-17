using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisHoreHoha
{
    partial class HisHoreHohaUpdate : BusinessBase
    {
		private List<HIS_HORE_HOHA> beforeUpdateHisHoreHohas = new List<HIS_HORE_HOHA>();
		
        internal HisHoreHohaUpdate()
            : base()
        {

        }

        internal HisHoreHohaUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_HORE_HOHA data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHoreHohaCheck checker = new HisHoreHohaCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_HORE_HOHA raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisHoreHohaDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHoreHoha_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHoreHoha that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisHoreHohas.Add(raw);
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

        internal bool UpdateList(List<HIS_HORE_HOHA> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHoreHohaCheck checker = new HisHoreHohaCheck(param);
                List<HIS_HORE_HOHA> listRaw = new List<HIS_HORE_HOHA>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisHoreHohaDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHoreHoha_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHoreHoha that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisHoreHohas.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisHoreHohas))
            {
                if (!DAOWorker.HisHoreHohaDAO.UpdateList(this.beforeUpdateHisHoreHohas))
                {
                    LogSystem.Warn("Rollback du lieu HisHoreHoha that bai, can kiem tra lai." + LogUtil.TraceData("HisHoreHohas", this.beforeUpdateHisHoreHohas));
                }
				this.beforeUpdateHisHoreHohas = null;
            }
        }
    }
}
