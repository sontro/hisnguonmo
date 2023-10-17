using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskGeneral
{
    partial class HisKskGeneralUpdate : BusinessBase
    {
		private List<HIS_KSK_GENERAL> beforeUpdateHisKskGenerals = new List<HIS_KSK_GENERAL>();
		
        internal HisKskGeneralUpdate()
            : base()
        {

        }

        internal HisKskGeneralUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_KSK_GENERAL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskGeneralCheck checker = new HisKskGeneralCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_KSK_GENERAL raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisKskGeneralDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskGeneral_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskGeneral that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisKskGenerals.Add(raw);
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

        internal bool UpdateList(List<HIS_KSK_GENERAL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskGeneralCheck checker = new HisKskGeneralCheck(param);
                List<HIS_KSK_GENERAL> listRaw = new List<HIS_KSK_GENERAL>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisKskGeneralDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskGeneral_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskGeneral that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisKskGenerals.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisKskGenerals))
            {
                if (!DAOWorker.HisKskGeneralDAO.UpdateList(this.beforeUpdateHisKskGenerals))
                {
                    LogSystem.Warn("Rollback du lieu HisKskGeneral that bai, can kiem tra lai." + LogUtil.TraceData("HisKskGenerals", this.beforeUpdateHisKskGenerals));
                }
				this.beforeUpdateHisKskGenerals = null;
            }
        }
    }
}
