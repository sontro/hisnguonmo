using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAnticipateMaty
{
    public partial class HisAnticipateMatyDAO : EntityBase
    {
        public List<V_HIS_ANTICIPATE_MATY> GetView(HisAnticipateMatySO search, CommonParam param)
        {
            List<V_HIS_ANTICIPATE_MATY> result = new List<V_HIS_ANTICIPATE_MATY>();

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

        public HIS_ANTICIPATE_MATY GetByCode(string code, HisAnticipateMatySO search)
        {
            HIS_ANTICIPATE_MATY result = null;

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
        
        public V_HIS_ANTICIPATE_MATY GetViewById(long id, HisAnticipateMatySO search)
        {
            V_HIS_ANTICIPATE_MATY result = null;

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

        public V_HIS_ANTICIPATE_MATY GetViewByCode(string code, HisAnticipateMatySO search)
        {
            V_HIS_ANTICIPATE_MATY result = null;

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

        public Dictionary<string, HIS_ANTICIPATE_MATY> GetDicByCode(HisAnticipateMatySO search, CommonParam param)
        {
            Dictionary<string, HIS_ANTICIPATE_MATY> result = new Dictionary<string, HIS_ANTICIPATE_MATY>();
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
