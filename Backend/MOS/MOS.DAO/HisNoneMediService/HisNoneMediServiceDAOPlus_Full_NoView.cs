using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisNoneMediService
{
    public partial class HisNoneMediServiceDAO : EntityBase
    {
        public HIS_NONE_MEDI_SERVICE GetByCode(string code, HisNoneMediServiceSO search)
        {
            HIS_NONE_MEDI_SERVICE result = null;

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

        public Dictionary<string, HIS_NONE_MEDI_SERVICE> GetDicByCode(HisNoneMediServiceSO search, CommonParam param)
        {
            Dictionary<string, HIS_NONE_MEDI_SERVICE> result = new Dictionary<string, HIS_NONE_MEDI_SERVICE>();
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
