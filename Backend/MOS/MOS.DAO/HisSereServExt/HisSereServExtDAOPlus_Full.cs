using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServExt
{
    public partial class HisSereServExtDAO : EntityBase
    {
        public List<V_HIS_SERE_SERV_EXT> GetView(HisSereServExtSO search, CommonParam param)
        {
            List<V_HIS_SERE_SERV_EXT> result = new List<V_HIS_SERE_SERV_EXT>();

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

        public HIS_SERE_SERV_EXT GetByCode(string code, HisSereServExtSO search)
        {
            HIS_SERE_SERV_EXT result = null;

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
        
        public V_HIS_SERE_SERV_EXT GetViewById(long id, HisSereServExtSO search)
        {
            V_HIS_SERE_SERV_EXT result = null;

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

        public V_HIS_SERE_SERV_EXT GetViewByCode(string code, HisSereServExtSO search)
        {
            V_HIS_SERE_SERV_EXT result = null;

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

        public Dictionary<string, HIS_SERE_SERV_EXT> GetDicByCode(HisSereServExtSO search, CommonParam param)
        {
            Dictionary<string, HIS_SERE_SERV_EXT> result = new Dictionary<string, HIS_SERE_SERV_EXT>();
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
