using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEkipTempUser
{
    public partial class HisEkipTempUserDAO : EntityBase
    {
        public HIS_EKIP_TEMP_USER GetByCode(string code, HisEkipTempUserSO search)
        {
            HIS_EKIP_TEMP_USER result = null;

            try
            {
                result = GetWorker.GetByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public Dictionary<string, HIS_EKIP_TEMP_USER> GetDicByCode(HisEkipTempUserSO search, CommonParam param)
        {
            Dictionary<string, HIS_EKIP_TEMP_USER> result = new Dictionary<string, HIS_EKIP_TEMP_USER>();
            try
            {
                result = GetWorker.GetDicByCode(search, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public bool ExistsCode(string code, long? id)
        {

            try
            {
                return CheckWorker.ExistsCode(code, id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
