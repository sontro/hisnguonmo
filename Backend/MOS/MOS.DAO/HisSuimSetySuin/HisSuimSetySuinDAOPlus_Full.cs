using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSuimSetySuin
{
    public partial class HisSuimSetySuinDAO : EntityBase
    {
        public List<V_HIS_SUIM_SETY_SUIN> GetView(HisSuimSetySuinSO search, CommonParam param)
        {
            List<V_HIS_SUIM_SETY_SUIN> result = new List<V_HIS_SUIM_SETY_SUIN>();

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

        public HIS_SUIM_SETY_SUIN GetByCode(string code, HisSuimSetySuinSO search)
        {
            HIS_SUIM_SETY_SUIN result = null;

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
        
        public V_HIS_SUIM_SETY_SUIN GetViewById(long id, HisSuimSetySuinSO search)
        {
            V_HIS_SUIM_SETY_SUIN result = null;

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

        public V_HIS_SUIM_SETY_SUIN GetViewByCode(string code, HisSuimSetySuinSO search)
        {
            V_HIS_SUIM_SETY_SUIN result = null;

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

        public Dictionary<string, HIS_SUIM_SETY_SUIN> GetDicByCode(HisSuimSetySuinSO search, CommonParam param)
        {
            Dictionary<string, HIS_SUIM_SETY_SUIN> result = new Dictionary<string, HIS_SUIM_SETY_SUIN>();
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
