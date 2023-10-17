using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRegisterGate
{
    public partial class HisRegisterGateDAO : EntityBase
    {
        public List<V_HIS_REGISTER_GATE> GetView(HisRegisterGateSO search, CommonParam param)
        {
            List<V_HIS_REGISTER_GATE> result = new List<V_HIS_REGISTER_GATE>();

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

        public HIS_REGISTER_GATE GetByCode(string code, HisRegisterGateSO search)
        {
            HIS_REGISTER_GATE result = null;

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
        
        public V_HIS_REGISTER_GATE GetViewById(long id, HisRegisterGateSO search)
        {
            V_HIS_REGISTER_GATE result = null;

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

        public V_HIS_REGISTER_GATE GetViewByCode(string code, HisRegisterGateSO search)
        {
            V_HIS_REGISTER_GATE result = null;

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

        public Dictionary<string, HIS_REGISTER_GATE> GetDicByCode(HisRegisterGateSO search, CommonParam param)
        {
            Dictionary<string, HIS_REGISTER_GATE> result = new Dictionary<string, HIS_REGISTER_GATE>();
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
