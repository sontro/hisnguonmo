using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServRation
{
    public partial class HisSereServRationDAO : EntityBase
    {
        public HIS_SERE_SERV_RATION GetByCode(string code, HisSereServRationSO search)
        {
            HIS_SERE_SERV_RATION result = null;

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

        public Dictionary<string, HIS_SERE_SERV_RATION> GetDicByCode(HisSereServRationSO search, CommonParam param)
        {
            Dictionary<string, HIS_SERE_SERV_RATION> result = new Dictionary<string, HIS_SERE_SERV_RATION>();
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
