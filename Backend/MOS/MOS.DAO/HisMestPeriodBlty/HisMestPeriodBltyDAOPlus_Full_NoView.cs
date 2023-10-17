using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPeriodBlty
{
    public partial class HisMestPeriodBltyDAO : EntityBase
    {
        public HIS_MEST_PERIOD_BLTY GetByCode(string code, HisMestPeriodBltySO search)
        {
            HIS_MEST_PERIOD_BLTY result = null;

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

        public Dictionary<string, HIS_MEST_PERIOD_BLTY> GetDicByCode(HisMestPeriodBltySO search, CommonParam param)
        {
            Dictionary<string, HIS_MEST_PERIOD_BLTY> result = new Dictionary<string, HIS_MEST_PERIOD_BLTY>();
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
