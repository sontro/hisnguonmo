using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSevereIllnessInfo
{
    public partial class HisSevereIllnessInfoDAO : EntityBase
    {
        public HIS_SEVERE_ILLNESS_INFO GetByCode(string code, HisSevereIllnessInfoSO search)
        {
            HIS_SEVERE_ILLNESS_INFO result = null;

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

        public Dictionary<string, HIS_SEVERE_ILLNESS_INFO> GetDicByCode(HisSevereIllnessInfoSO search, CommonParam param)
        {
            Dictionary<string, HIS_SEVERE_ILLNESS_INFO> result = new Dictionary<string, HIS_SEVERE_ILLNESS_INFO>();
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
