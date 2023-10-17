using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServMaty
{
    public partial class HisSereServMatyDAO : EntityBase
    {
        public List<V_HIS_SERE_SERV_MATY> GetView(HisSereServMatySO search, CommonParam param)
        {
            List<V_HIS_SERE_SERV_MATY> result = new List<V_HIS_SERE_SERV_MATY>();

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

        public HIS_SERE_SERV_MATY GetByCode(string code, HisSereServMatySO search)
        {
            HIS_SERE_SERV_MATY result = null;

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
        
        public V_HIS_SERE_SERV_MATY GetViewById(long id, HisSereServMatySO search)
        {
            V_HIS_SERE_SERV_MATY result = null;

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

        public V_HIS_SERE_SERV_MATY GetViewByCode(string code, HisSereServMatySO search)
        {
            V_HIS_SERE_SERV_MATY result = null;

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

        public Dictionary<string, HIS_SERE_SERV_MATY> GetDicByCode(HisSereServMatySO search, CommonParam param)
        {
            Dictionary<string, HIS_SERE_SERV_MATY> result = new Dictionary<string, HIS_SERE_SERV_MATY>();
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
