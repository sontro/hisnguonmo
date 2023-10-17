using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServSuin
{
    partial class HisSereServSuinUpdate : BusinessBase
    {
        internal HisSereServSuinUpdate()
            : base()
        {

        }

        internal HisSereServSuinUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERE_SERV_SUIN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServSuinCheck checker = new HisSereServSuinCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SERE_SERV_SUIN raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisSereServSuinDAO.Update(data);
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

        internal bool UpdateList(List<HIS_SERE_SERV_SUIN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServSuinCheck checker = new HisSereServSuinCheck(param);
                List<HIS_SERE_SERV_SUIN> listRaw = new List<HIS_SERE_SERV_SUIN>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisSereServSuinDAO.UpdateList(listData);
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
