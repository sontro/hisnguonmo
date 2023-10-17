using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEkipTemp
{
    public partial class HisEkipTempDAO : EntityBase
    {
        public List<V_HIS_EKIP_TEMP> GetView(HisEkipTempSO search, CommonParam param)
        {
            List<V_HIS_EKIP_TEMP> result = new List<V_HIS_EKIP_TEMP>();

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

        public HIS_EKIP_TEMP GetByCode(string code, HisEkipTempSO search)
        {
            HIS_EKIP_TEMP result = null;

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
        
        public V_HIS_EKIP_TEMP GetViewById(long id, HisEkipTempSO search)
        {
            V_HIS_EKIP_TEMP result = null;

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

        public V_HIS_EKIP_TEMP GetViewByCode(string code, HisEkipTempSO search)
        {
            V_HIS_EKIP_TEMP result = null;

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

        public Dictionary<string, HIS_EKIP_TEMP> GetDicByCode(HisEkipTempSO search, CommonParam param)
        {
            Dictionary<string, HIS_EKIP_TEMP> result = new Dictionary<string, HIS_EKIP_TEMP>();
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
