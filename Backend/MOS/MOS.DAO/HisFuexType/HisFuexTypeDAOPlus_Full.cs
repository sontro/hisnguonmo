using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisFuexType
{
    public partial class HisFuexTypeDAO : EntityBase
    {
        public List<V_HIS_FUEX_TYPE> GetView(HisFuexTypeSO search, CommonParam param)
        {
            List<V_HIS_FUEX_TYPE> result = new List<V_HIS_FUEX_TYPE>();

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

        public HIS_FUEX_TYPE GetByCode(string code, HisFuexTypeSO search)
        {
            HIS_FUEX_TYPE result = null;

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
        
        public V_HIS_FUEX_TYPE GetViewById(long id, HisFuexTypeSO search)
        {
            V_HIS_FUEX_TYPE result = null;

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

        public V_HIS_FUEX_TYPE GetViewByCode(string code, HisFuexTypeSO search)
        {
            V_HIS_FUEX_TYPE result = null;

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

        public Dictionary<string, HIS_FUEX_TYPE> GetDicByCode(HisFuexTypeSO search, CommonParam param)
        {
            Dictionary<string, HIS_FUEX_TYPE> result = new Dictionary<string, HIS_FUEX_TYPE>();
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
