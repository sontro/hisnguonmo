using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.DAO.SarRetyFofi
{
    public partial class SarRetyFofiDAO : EntityBase
    {
        public SAR_RETY_FOFI GetByCode(string code, SarRetyFofiSO search)
        {
            SAR_RETY_FOFI result = null;

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

        public Dictionary<string, SAR_RETY_FOFI> GetDicByCode(SarRetyFofiSO search, CommonParam param)
        {
            Dictionary<string, SAR_RETY_FOFI> result = new Dictionary<string, SAR_RETY_FOFI>();
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
