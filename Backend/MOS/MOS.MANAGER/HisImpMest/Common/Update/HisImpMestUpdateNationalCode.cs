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
    class HisImpMestUpdateNationalCode : BusinessBase
    {
        internal HisImpMestUpdateNationalCode()
            : base()
        {

        }

        internal HisImpMestUpdateNationalCode(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<HIS_IMP_MEST> listData, ref List<HIS_IMP_MEST> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_IMP_MEST> listRaw = new List<HIS_IMP_MEST>();
                HisImpMestCheck checker = new HisImpMestCheck(param);
                valid = valid && IsNotNullOrEmpty(listData);
                valid = valid && checker.VerifyIds(listData.Select(s => s.ID).ToList(), listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                valid = valid && checker.IsImported(listRaw);
                valid = valid && checker.HasNoNationalCode(listRaw);
                if (valid)
                {
                    foreach (var raw in listRaw)
                    {
                        HIS_IMP_MEST data = listData.FirstOrDefault(o => o.ID == raw.ID);
                        raw.NATIONAL_IMP_MEST_CODE = data.NATIONAL_IMP_MEST_CODE;
                    }
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
