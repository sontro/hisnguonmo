using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisInfusion
{
    partial class HisInfusionUpdate : BusinessBase
    {
        internal bool Unfinish(HIS_INFUSION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisInfusionCheck checker = new HisInfusionCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_INFUSION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisInfusions.Add(raw);

                    data.EXECUTE_LOGINNAME = null;
                    data.EXECUTE_USERNAME = null;
                    
                    if (!DAOWorker.HisInfusionDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisInfusion_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisInfusion that bai." + LogUtil.TraceData("data", data));
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
    }
}
