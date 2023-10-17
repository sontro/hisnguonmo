using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskDriverCar
{
    partial class HisKskDriverCarDelete : BusinessBase
    {
        internal HisKskDriverCarDelete()
            : base()
        {

        }

        internal HisKskDriverCarDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_KSK_DRIVER_CAR data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskDriverCarCheck checker = new HisKskDriverCarCheck(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_DRIVER_CAR raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisKskDriverCarDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_KSK_DRIVER_CAR> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskDriverCarCheck checker = new HisKskDriverCarCheck(param);
                List<HIS_KSK_DRIVER_CAR> listRaw = new List<HIS_KSK_DRIVER_CAR>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisKskDriverCarDAO.DeleteList(listData);
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
