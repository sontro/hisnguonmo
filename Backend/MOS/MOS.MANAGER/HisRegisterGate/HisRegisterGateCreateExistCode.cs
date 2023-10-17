using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRegisterGate
{
    partial class HisRegisterGateCreate : BusinessBase
    {
		private List<HIS_REGISTER_GATE> recentHisRegisterGates = new List<HIS_REGISTER_GATE>();
		
        internal HisRegisterGateCreate()
            : base()
        {

        }

        internal HisRegisterGateCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_REGISTER_GATE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRegisterGateCheck checker = new HisRegisterGateCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.REGISTER_GATE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisRegisterGateDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRegisterGate_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRegisterGate that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisRegisterGates.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisRegisterGates))
            {
                if (!new HisRegisterGateTruncate(param).TruncateList(this.recentHisRegisterGates))
                {
                    LogSystem.Warn("Rollback du lieu HisRegisterGate that bai, can kiem tra lai." + LogUtil.TraceData("recentHisRegisterGates", this.recentHisRegisterGates));
                }
            }
        }
    }
}
