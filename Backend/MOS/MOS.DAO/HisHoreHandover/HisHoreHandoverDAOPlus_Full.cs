using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHoreHandover
{
    public partial class HisHoreHandoverDAO : EntityBase
    {
        public List<V_HIS_HORE_HANDOVER> GetView(HisHoreHandoverSO search, CommonParam param)
        {
            List<V_HIS_HORE_HANDOVER> result = new List<V_HIS_HORE_HANDOVER>();

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

        public HIS_HORE_HANDOVER GetByCode(string code, HisHoreHandoverSO search)
        {
            HIS_HORE_HANDOVER result = null;

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
        
        public V_HIS_HORE_HANDOVER GetViewById(long id, HisHoreHandoverSO search)
        {
            V_HIS_HORE_HANDOVER result = null;

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

        public V_HIS_HORE_HANDOVER GetViewByCode(string code, HisHoreHandoverSO search)
        {
            V_HIS_HORE_HANDOVER result = null;

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

        public Dictionary<string, HIS_HORE_HANDOVER> GetDicByCode(HisHoreHandoverSO search, CommonParam param)
        {
            Dictionary<string, HIS_HORE_HANDOVER> result = new Dictionary<string, HIS_HORE_HANDOVER>();
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
