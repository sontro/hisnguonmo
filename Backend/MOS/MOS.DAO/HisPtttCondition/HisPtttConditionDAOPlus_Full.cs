using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttCondition
{
    public partial class HisPtttConditionDAO : EntityBase
    {
        public List<V_HIS_PTTT_CONDITION> GetView(HisPtttConditionSO search, CommonParam param)
        {
            List<V_HIS_PTTT_CONDITION> result = new List<V_HIS_PTTT_CONDITION>();

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

        public HIS_PTTT_CONDITION GetByCode(string code, HisPtttConditionSO search)
        {
            HIS_PTTT_CONDITION result = null;

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
        
        public V_HIS_PTTT_CONDITION GetViewById(long id, HisPtttConditionSO search)
        {
            V_HIS_PTTT_CONDITION result = null;

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

        public V_HIS_PTTT_CONDITION GetViewByCode(string code, HisPtttConditionSO search)
        {
            V_HIS_PTTT_CONDITION result = null;

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

        public Dictionary<string, HIS_PTTT_CONDITION> GetDicByCode(HisPtttConditionSO search, CommonParam param)
        {
            Dictionary<string, HIS_PTTT_CONDITION> result = new Dictionary<string, HIS_PTTT_CONDITION>();
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
