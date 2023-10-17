using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSpeciality
{
    partial class HisSpecialityDelete : BusinessBase
    {
        internal HisSpecialityDelete()
            : base()
        {

        }

        internal HisSpecialityDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SPECIALITY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSpecialityCheck checker = new HisSpecialityCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SPECIALITY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisSpecialityDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_SPECIALITY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSpecialityCheck checker = new HisSpecialityCheck(param);
                List<HIS_SPECIALITY> listRaw = new List<HIS_SPECIALITY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisSpecialityDAO.DeleteList(listData);
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
