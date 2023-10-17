using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServTein
{
    public partial class HisSereServTeinDAO : EntityBase
    {
        public List<V_HIS_SERE_SERV_TEIN> GetView(HisSereServTeinSO search, CommonParam param)
        {
            List<V_HIS_SERE_SERV_TEIN> result = new List<V_HIS_SERE_SERV_TEIN>();

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

        public HIS_SERE_SERV_TEIN GetByCode(string code, HisSereServTeinSO search)
        {
            HIS_SERE_SERV_TEIN result = null;

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
        
        public V_HIS_SERE_SERV_TEIN GetViewById(long id, HisSereServTeinSO search)
        {
            V_HIS_SERE_SERV_TEIN result = null;

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

        public V_HIS_SERE_SERV_TEIN GetViewByCode(string code, HisSereServTeinSO search)
        {
            V_HIS_SERE_SERV_TEIN result = null;

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

        public Dictionary<string, HIS_SERE_SERV_TEIN> GetDicByCode(HisSereServTeinSO search, CommonParam param)
        {
            Dictionary<string, HIS_SERE_SERV_TEIN> result = new Dictionary<string, HIS_SERE_SERV_TEIN>();
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
