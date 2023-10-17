using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAnticipateMety
{
    public partial class HisAnticipateMetyDAO : EntityBase
    {
        public List<V_HIS_ANTICIPATE_METY> GetView(HisAnticipateMetySO search, CommonParam param)
        {
            List<V_HIS_ANTICIPATE_METY> result = new List<V_HIS_ANTICIPATE_METY>();

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

        public HIS_ANTICIPATE_METY GetByCode(string code, HisAnticipateMetySO search)
        {
            HIS_ANTICIPATE_METY result = null;

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
        
        public V_HIS_ANTICIPATE_METY GetViewById(long id, HisAnticipateMetySO search)
        {
            V_HIS_ANTICIPATE_METY result = null;

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

        public V_HIS_ANTICIPATE_METY GetViewByCode(string code, HisAnticipateMetySO search)
        {
            V_HIS_ANTICIPATE_METY result = null;

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

        public Dictionary<string, HIS_ANTICIPATE_METY> GetDicByCode(HisAnticipateMetySO search, CommonParam param)
        {
            Dictionary<string, HIS_ANTICIPATE_METY> result = new Dictionary<string, HIS_ANTICIPATE_METY>();
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
