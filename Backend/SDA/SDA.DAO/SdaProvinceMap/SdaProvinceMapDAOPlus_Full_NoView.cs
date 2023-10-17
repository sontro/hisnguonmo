using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaProvinceMap
{
    public partial class SdaProvinceMapDAO : EntityBase
    {
        public SDA_PROVINCE_MAP GetByCode(string code, SdaProvinceMapSO search)
        {
            SDA_PROVINCE_MAP result = null;

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

        public Dictionary<string, SDA_PROVINCE_MAP> GetDicByCode(SdaProvinceMapSO search, CommonParam param)
        {
            Dictionary<string, SDA_PROVINCE_MAP> result = new Dictionary<string, SDA_PROVINCE_MAP>();
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
