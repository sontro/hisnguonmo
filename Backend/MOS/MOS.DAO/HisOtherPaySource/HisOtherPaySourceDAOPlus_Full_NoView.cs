using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisOtherPaySource
{
    public partial class HisOtherPaySourceDAO : EntityBase
    {
        public HIS_OTHER_PAY_SOURCE GetByCode(string code, HisOtherPaySourceSO search)
        {
            HIS_OTHER_PAY_SOURCE result = null;

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

        public Dictionary<string, HIS_OTHER_PAY_SOURCE> GetDicByCode(HisOtherPaySourceSO search, CommonParam param)
        {
            Dictionary<string, HIS_OTHER_PAY_SOURCE> result = new Dictionary<string, HIS_OTHER_PAY_SOURCE>();
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
