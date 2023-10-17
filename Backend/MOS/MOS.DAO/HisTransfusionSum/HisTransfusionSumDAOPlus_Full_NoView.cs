using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTransfusionSum
{
    public partial class HisTransfusionSumDAO : EntityBase
    {
        public HIS_TRANSFUSION_SUM GetByCode(string code, HisTransfusionSumSO search)
        {
            HIS_TRANSFUSION_SUM result = null;

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

        public Dictionary<string, HIS_TRANSFUSION_SUM> GetDicByCode(HisTransfusionSumSO search, CommonParam param)
        {
            Dictionary<string, HIS_TRANSFUSION_SUM> result = new Dictionary<string, HIS_TRANSFUSION_SUM>();
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
