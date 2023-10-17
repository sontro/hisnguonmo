using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRegisterGate
{
    partial class HisRegisterGateUpdate : BusinessBase
    {
		private List<HIS_REGISTER_GATE> beforeUpdateHisRegisterGates = new List<HIS_REGISTER_GATE>();
		
        internal HisRegisterGateUpdate()
            : base()
        {

        }

        internal HisRegisterGateUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_REGISTER_GATE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRegisterGateCheck checker = new HisRegisterGateCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_REGISTER_GATE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.REGISTER_GATE_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisRegisterGates.Add(raw);
					if (!DAOWorker.HisRegisterGateDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRegisterGate_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRegisterGate that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_REGISTER_GATE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRegisterGateCheck checker = new HisRegisterGateCheck(param);
                List<HIS_REGISTER_GATE> listRaw = new List<HIS_REGISTER_GATE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.REGISTER_GATE_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisRegisterGates.AddRange(listRaw);
					if (!DAOWorker.HisRegisterGateDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRegisterGate_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRegisterGate that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisRegisterGates))
            {
                if (!new HisRegisterGateUpdate(param).UpdateList(this.beforeUpdateHisRegisterGates))
                {
                    LogSystem.Warn("Rollback du lieu HisRegisterGate that bai, can kiem tra lai." + LogUtil.TraceData("HisRegisterGates", this.beforeUpdateHisRegisterGates));
                }
            }
        }
    }
}
