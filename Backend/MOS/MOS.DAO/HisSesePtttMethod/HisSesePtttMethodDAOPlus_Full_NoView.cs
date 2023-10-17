using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSesePtttMethod
{
    public partial class HisSesePtttMethodDAO : EntityBase
    {
        public HIS_SESE_PTTT_METHOD GetByCode(string code, HisSesePtttMethodSO search)
        {
            HIS_SESE_PTTT_METHOD result = null;

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

        public Dictionary<string, HIS_SESE_PTTT_METHOD> GetDicByCode(HisSesePtttMethodSO search, CommonParam param)
        {
            Dictionary<string, HIS_SESE_PTTT_METHOD> result = new Dictionary<string, HIS_SESE_PTTT_METHOD>();
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
