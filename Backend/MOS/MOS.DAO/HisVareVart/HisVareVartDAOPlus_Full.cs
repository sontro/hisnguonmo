using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVareVart
{
    public partial class HisVareVartDAO : EntityBase
    {
        public List<V_HIS_VARE_VART> GetView(HisVareVartSO search, CommonParam param)
        {
            List<V_HIS_VARE_VART> result = new List<V_HIS_VARE_VART>();

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

        public HIS_VARE_VART GetByCode(string code, HisVareVartSO search)
        {
            HIS_VARE_VART result = null;

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
        
        public V_HIS_VARE_VART GetViewById(long id, HisVareVartSO search)
        {
            V_HIS_VARE_VART result = null;

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

        public V_HIS_VARE_VART GetViewByCode(string code, HisVareVartSO search)
        {
            V_HIS_VARE_VART result = null;

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

        public Dictionary<string, HIS_VARE_VART> GetDicByCode(HisVareVartSO search, CommonParam param)
        {
            Dictionary<string, HIS_VARE_VART> result = new Dictionary<string, HIS_VARE_VART>();
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
