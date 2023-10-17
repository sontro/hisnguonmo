using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTransfusion
{
    public partial class HisTransfusionDAO : EntityBase
    {
        public List<V_HIS_TRANSFUSION> GetView(HisTransfusionSO search, CommonParam param)
        {
            List<V_HIS_TRANSFUSION> result = new List<V_HIS_TRANSFUSION>();

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

        public HIS_TRANSFUSION GetByCode(string code, HisTransfusionSO search)
        {
            HIS_TRANSFUSION result = null;

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
        
        public V_HIS_TRANSFUSION GetViewById(long id, HisTransfusionSO search)
        {
            V_HIS_TRANSFUSION result = null;

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

        public V_HIS_TRANSFUSION GetViewByCode(string code, HisTransfusionSO search)
        {
            V_HIS_TRANSFUSION result = null;

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

        public Dictionary<string, HIS_TRANSFUSION> GetDicByCode(HisTransfusionSO search, CommonParam param)
        {
            Dictionary<string, HIS_TRANSFUSION> result = new Dictionary<string, HIS_TRANSFUSION>();
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
