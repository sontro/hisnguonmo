using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSuimIndexUnit
{
    public partial class HisSuimIndexUnitDAO : EntityBase
    {
        public List<V_HIS_SUIM_INDEX_UNIT> GetView(HisSuimIndexUnitSO search, CommonParam param)
        {
            List<V_HIS_SUIM_INDEX_UNIT> result = new List<V_HIS_SUIM_INDEX_UNIT>();

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

        public HIS_SUIM_INDEX_UNIT GetByCode(string code, HisSuimIndexUnitSO search)
        {
            HIS_SUIM_INDEX_UNIT result = null;

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
        
        public V_HIS_SUIM_INDEX_UNIT GetViewById(long id, HisSuimIndexUnitSO search)
        {
            V_HIS_SUIM_INDEX_UNIT result = null;

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

        public V_HIS_SUIM_INDEX_UNIT GetViewByCode(string code, HisSuimIndexUnitSO search)
        {
            V_HIS_SUIM_INDEX_UNIT result = null;

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

        public Dictionary<string, HIS_SUIM_INDEX_UNIT> GetDicByCode(HisSuimIndexUnitSO search, CommonParam param)
        {
            Dictionary<string, HIS_SUIM_INDEX_UNIT> result = new Dictionary<string, HIS_SUIM_INDEX_UNIT>();
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
