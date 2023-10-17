using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPayForm
{
    public partial class HisPayFormDAO : EntityBase
    {
        public List<V_HIS_PAY_FORM> GetView(HisPayFormSO search, CommonParam param)
        {
            List<V_HIS_PAY_FORM> result = new List<V_HIS_PAY_FORM>();

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

        public HIS_PAY_FORM GetByCode(string code, HisPayFormSO search)
        {
            HIS_PAY_FORM result = null;

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
        
        public V_HIS_PAY_FORM GetViewById(long id, HisPayFormSO search)
        {
            V_HIS_PAY_FORM result = null;

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

        public V_HIS_PAY_FORM GetViewByCode(string code, HisPayFormSO search)
        {
            V_HIS_PAY_FORM result = null;

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

        public Dictionary<string, HIS_PAY_FORM> GetDicByCode(HisPayFormSO search, CommonParam param)
        {
            Dictionary<string, HIS_PAY_FORM> result = new Dictionary<string, HIS_PAY_FORM>();
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
