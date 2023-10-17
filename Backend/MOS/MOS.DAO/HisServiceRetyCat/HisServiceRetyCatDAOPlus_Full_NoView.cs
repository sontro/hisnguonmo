using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceRetyCat
{
    public partial class HisServiceRetyCatDAO : EntityBase
    {
        public HIS_SERVICE_RETY_CAT GetByCode(string code, HisServiceRetyCatSO search)
        {
            HIS_SERVICE_RETY_CAT result = null;

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

        public Dictionary<string, HIS_SERVICE_RETY_CAT> GetDicByCode(HisServiceRetyCatSO search, CommonParam param)
        {
            Dictionary<string, HIS_SERVICE_RETY_CAT> result = new Dictionary<string, HIS_SERVICE_RETY_CAT>();
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
