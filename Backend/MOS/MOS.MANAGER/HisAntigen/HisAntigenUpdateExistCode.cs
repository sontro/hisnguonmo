using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAntigen
{
    partial class HisAntigenUpdate : BusinessBase
    {
		private List<HIS_ANTIGEN> beforeUpdateHisAntigens = new List<HIS_ANTIGEN>();
		
        internal HisAntigenUpdate()
            : base()
        {

        }

        internal HisAntigenUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ANTIGEN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAntigenCheck checker = new HisAntigenCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ANTIGEN raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.ANTIGEN_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisAntigenDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAntigen_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAntigen that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisAntigens.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_ANTIGEN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAntigenCheck checker = new HisAntigenCheck(param);
                List<HIS_ANTIGEN> listRaw = new List<HIS_ANTIGEN>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.ANTIGEN_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisAntigenDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAntigen_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAntigen that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisAntigens.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAntigens))
            {
                if (!DAOWorker.HisAntigenDAO.UpdateList(this.beforeUpdateHisAntigens))
                {
                    LogSystem.Warn("Rollback du lieu HisAntigen that bai, can kiem tra lai." + LogUtil.TraceData("HisAntigens", this.beforeUpdateHisAntigens));
                }
				this.beforeUpdateHisAntigens = null;
            }
        }
    }
}
