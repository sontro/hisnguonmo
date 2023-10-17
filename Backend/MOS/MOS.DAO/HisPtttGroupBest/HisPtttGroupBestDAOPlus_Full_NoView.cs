using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttGroupBest
{
    public partial class HisPtttGroupBestDAO : EntityBase
    {
        public HIS_PTTT_GROUP_BEST GetByCode(string code, HisPtttGroupBestSO search)
        {
            HIS_PTTT_GROUP_BEST result = null;

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

        public Dictionary<string, HIS_PTTT_GROUP_BEST> GetDicByCode(HisPtttGroupBestSO search, CommonParam param)
        {
            Dictionary<string, HIS_PTTT_GROUP_BEST> result = new Dictionary<string, HIS_PTTT_GROUP_BEST>();
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
