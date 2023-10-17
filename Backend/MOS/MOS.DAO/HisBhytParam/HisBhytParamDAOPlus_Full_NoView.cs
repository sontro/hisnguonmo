using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBhytParam
{
    public partial class HisBhytParamDAO : EntityBase
    {
        public HIS_BHYT_PARAM GetByCode(string code, HisBhytParamSO search)
        {
            HIS_BHYT_PARAM result = null;

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

        public Dictionary<string, HIS_BHYT_PARAM> GetDicByCode(HisBhytParamSO search, CommonParam param)
        {
            Dictionary<string, HIS_BHYT_PARAM> result = new Dictionary<string, HIS_BHYT_PARAM>();
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
