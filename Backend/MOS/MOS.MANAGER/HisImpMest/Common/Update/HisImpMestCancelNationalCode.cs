using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Common.Update
{
    class HisImpMestCancelNationalCode : BusinessBase
    {
        internal HisImpMestCancelNationalCode()
            : base()
        {

        }

        internal HisImpMestCancelNationalCode(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<long> listData, ref List<HIS_IMP_MEST> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_IMP_MEST> listRaw = new List<HIS_IMP_MEST>();
                HisImpMestCheck checker = new HisImpMestCheck(param);
                valid = valid && IsNotNullOrEmpty(listData);
                valid = valid && checker.VerifyIds(listData, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                valid = valid && checker.HasNationalCode(listRaw);
                if (valid)
                {
                    listRaw.ForEach(o => o.NATIONAL_IMP_MEST_CODE = null);
                    if (!DAOWorker.HisImpMestDAO.UpdateList(listRaw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMest_ThemMoiThatBai);
                        throw new Exception("update NATIONAL_IMP_MEST_CODE HisImpMest that bai." + LogUtil.TraceData("listRaw", listRaw));
                    }
                    result = true;
                    resultData = listRaw;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
