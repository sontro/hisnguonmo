using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisFilmSize
{
    partial class HisFilmSizeDelete : BusinessBase
    {
        internal HisFilmSizeDelete()
            : base()
        {

        }

        internal HisFilmSizeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_FILM_SIZE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisFilmSizeCheck checker = new HisFilmSizeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_FILM_SIZE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisFilmSizeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_FILM_SIZE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisFilmSizeCheck checker = new HisFilmSizeCheck(param);
                List<HIS_FILM_SIZE> listRaw = new List<HIS_FILM_SIZE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisFilmSizeDAO.DeleteList(listData);
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
