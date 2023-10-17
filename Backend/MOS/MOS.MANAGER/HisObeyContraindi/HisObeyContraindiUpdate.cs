using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisObeyContraindi
{
    partial class HisObeyContraindiUpdate : BusinessBase
    {
		private List<HIS_OBEY_CONTRAINDI> beforeUpdateHisObeyContraindis = new List<HIS_OBEY_CONTRAINDI>();
		
        internal HisObeyContraindiUpdate()
            : base()
        {

        }

        internal HisObeyContraindiUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_OBEY_CONTRAINDI data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisObeyContraindiCheck checker = new HisObeyContraindiCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_OBEY_CONTRAINDI raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisObeyContraindiDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisObeyContraindi_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisObeyContraindi that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisObeyContraindis.Add(raw);
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

        internal bool UpdateList(List<HIS_OBEY_CONTRAINDI> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisObeyContraindiCheck checker = new HisObeyContraindiCheck(param);
                List<HIS_OBEY_CONTRAINDI> listRaw = new List<HIS_OBEY_CONTRAINDI>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisObeyContraindiDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisObeyContraindi_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisObeyContraindi that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisObeyContraindis.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisObeyContraindis))
            {
                if (!DAOWorker.HisObeyContraindiDAO.UpdateList(this.beforeUpdateHisObeyContraindis))
                {
                    LogSystem.Warn("Rollback du lieu HisObeyContraindi that bai, can kiem tra lai." + LogUtil.TraceData("HisObeyContraindis", this.beforeUpdateHisObeyContraindis));
                }
				this.beforeUpdateHisObeyContraindis = null;
            }
        }
    }
}
