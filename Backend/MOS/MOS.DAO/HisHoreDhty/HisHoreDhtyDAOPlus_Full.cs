using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHoreDhty
{
    public partial class HisHoreDhtyDAO : EntityBase
    {
        public List<V_HIS_HORE_DHTY> GetView(HisHoreDhtySO search, CommonParam param)
        {
            List<V_HIS_HORE_DHTY> result = new List<V_HIS_HORE_DHTY>();

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

        public HIS_HORE_DHTY GetByCode(string code, HisHoreDhtySO search)
        {
            HIS_HORE_DHTY result = null;

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
        
        public V_HIS_HORE_DHTY GetViewById(long id, HisHoreDhtySO search)
        {
            V_HIS_HORE_DHTY result = null;

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

        public V_HIS_HORE_DHTY GetViewByCode(string code, HisHoreDhtySO search)
        {
            V_HIS_HORE_DHTY result = null;

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

        public Dictionary<string, HIS_HORE_DHTY> GetDicByCode(HisHoreDhtySO search, CommonParam param)
        {
            Dictionary<string, HIS_HORE_DHTY> result = new Dictionary<string, HIS_HORE_DHTY>();
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
