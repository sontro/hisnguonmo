using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRegimenHiv
{
    public partial class HisRegimenHivDAO : EntityBase
    {
        public List<V_HIS_REGIMEN_HIV> GetView(HisRegimenHivSO search, CommonParam param)
        {
            List<V_HIS_REGIMEN_HIV> result = new List<V_HIS_REGIMEN_HIV>();

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

        public HIS_REGIMEN_HIV GetByCode(string code, HisRegimenHivSO search)
        {
            HIS_REGIMEN_HIV result = null;

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
        
        public V_HIS_REGIMEN_HIV GetViewById(long id, HisRegimenHivSO search)
        {
            V_HIS_REGIMEN_HIV result = null;

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

        public V_HIS_REGIMEN_HIV GetViewByCode(string code, HisRegimenHivSO search)
        {
            V_HIS_REGIMEN_HIV result = null;

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

        public Dictionary<string, HIS_REGIMEN_HIV> GetDicByCode(HisRegimenHivSO search, CommonParam param)
        {
            Dictionary<string, HIS_REGIMEN_HIV> result = new Dictionary<string, HIS_REGIMEN_HIV>();
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
