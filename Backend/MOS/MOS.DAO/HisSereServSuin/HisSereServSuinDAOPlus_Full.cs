using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServSuin
{
    public partial class HisSereServSuinDAO : EntityBase
    {
        public List<V_HIS_SERE_SERV_SUIN> GetView(HisSereServSuinSO search, CommonParam param)
        {
            List<V_HIS_SERE_SERV_SUIN> result = new List<V_HIS_SERE_SERV_SUIN>();

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

        public HIS_SERE_SERV_SUIN GetByCode(string code, HisSereServSuinSO search)
        {
            HIS_SERE_SERV_SUIN result = null;

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
        
        public V_HIS_SERE_SERV_SUIN GetViewById(long id, HisSereServSuinSO search)
        {
            V_HIS_SERE_SERV_SUIN result = null;

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

        public V_HIS_SERE_SERV_SUIN GetViewByCode(string code, HisSereServSuinSO search)
        {
            V_HIS_SERE_SERV_SUIN result = null;

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

        public Dictionary<string, HIS_SERE_SERV_SUIN> GetDicByCode(HisSereServSuinSO search, CommonParam param)
        {
            Dictionary<string, HIS_SERE_SERV_SUIN> result = new Dictionary<string, HIS_SERE_SERV_SUIN>();
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
