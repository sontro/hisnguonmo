using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDiseaseType
{
    partial class HisDiseaseTypeDelete : BusinessBase
    {
        internal HisDiseaseTypeDelete()
            : base()
        {

        }

        internal HisDiseaseTypeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_DISEASE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDiseaseTypeCheck checker = new HisDiseaseTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_DISEASE_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisDiseaseTypeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_DISEASE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDiseaseTypeCheck checker = new HisDiseaseTypeCheck(param);
                List<HIS_DISEASE_TYPE> listRaw = new List<HIS_DISEASE_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisDiseaseTypeDAO.DeleteList(listData);
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
