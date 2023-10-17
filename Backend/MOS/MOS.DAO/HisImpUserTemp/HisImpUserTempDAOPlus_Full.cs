using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisImpUserTemp
{
    public partial class HisImpUserTempDAO : EntityBase
    {
        public List<V_HIS_IMP_USER_TEMP> GetView(HisImpUserTempSO search, CommonParam param)
        {
            List<V_HIS_IMP_USER_TEMP> result = new List<V_HIS_IMP_USER_TEMP>();

            try
            {
                result = GetWorker.GetView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public HIS_IMP_USER_TEMP GetByCode(string code, HisImpUserTempSO search)
        {
            HIS_IMP_USER_TEMP result = null;

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
        
        public V_HIS_IMP_USER_TEMP GetViewById(long id, HisImpUserTempSO search)
        {
            V_HIS_IMP_USER_TEMP result = null;

            try
            {
                result = GetWorker.GetViewById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public V_HIS_IMP_USER_TEMP GetViewByCode(string code, HisImpUserTempSO search)
        {
            V_HIS_IMP_USER_TEMP result = null;

            try
            {
                result = GetWorker.GetViewByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public Dictionary<string, HIS_IMP_USER_TEMP> GetDicByCode(HisImpUserTempSO search, CommonParam param)
        {
            Dictionary<string, HIS_IMP_USER_TEMP> result = new Dictionary<string, HIS_IMP_USER_TEMP>();
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
