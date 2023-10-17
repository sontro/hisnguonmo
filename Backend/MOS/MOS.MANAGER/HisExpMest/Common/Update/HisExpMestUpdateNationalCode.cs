using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Update
{
    class HisExpMestUpdateNationalCode : BusinessBase
    {
        internal HisExpMestUpdateNationalCode()
            : base()
        {

        }

        internal HisExpMestUpdateNationalCode(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<HIS_EXP_MEST> listData, ref List<HIS_EXP_MEST> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_EXP_MEST> listRaw = new List<HIS_EXP_MEST>();
                HisExpMestCheck checker = new HisExpMestCheck(param);
                valid = valid && IsNotNullOrEmpty(listData);
                valid = valid && checker.VerifyIds(listData.Select(s => s.ID).ToList(), listRaw);
                valid = valid && checker.IsUnlock(listRaw);
                valid = valid && checker.IsFinished(listRaw);
                valid = valid && checker.HasNoNationalCode(listRaw);
                if (valid)
                {
                    foreach (var raw in listRaw)
                    {
                        HIS_EXP_MEST data = listData.FirstOrDefault(o => o.ID == raw.ID);
                        raw.NATIONAL_EXP_MEST_CODE = data.NATIONAL_EXP_MEST_CODE;
                    }
                    if (!DAOWorker.HisExpMestDAO.UpdateList(listRaw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMest_ThemMoiThatBai);
                        throw new Exception("update NATIONAL_EXP_MEST_CODE HisExpMest that bai." + LogUtil.TraceData("listRaw", listRaw));
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
