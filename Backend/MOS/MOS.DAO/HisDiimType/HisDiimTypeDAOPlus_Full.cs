using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDiimType
{
    public partial class HisDiimTypeDAO : EntityBase
    {
        public List<V_HIS_DIIM_TYPE> GetView(HisDiimTypeSO search, CommonParam param)
        {
            List<V_HIS_DIIM_TYPE> result = new List<V_HIS_DIIM_TYPE>();

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

        public HIS_DIIM_TYPE GetByCode(string code, HisDiimTypeSO search)
        {
            HIS_DIIM_TYPE result = null;

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
        
        public V_HIS_DIIM_TYPE GetViewById(long id, HisDiimTypeSO search)
        {
            V_HIS_DIIM_TYPE result = null;

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

        public V_HIS_DIIM_TYPE GetViewByCode(string code, HisDiimTypeSO search)
        {
            V_HIS_DIIM_TYPE result = null;

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

        public Dictionary<string, HIS_DIIM_TYPE> GetDicByCode(HisDiimTypeSO search, CommonParam param)
        {
            Dictionary<string, HIS_DIIM_TYPE> result = new Dictionary<string, HIS_DIIM_TYPE>();
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
