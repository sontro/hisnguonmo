using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServReha
{
    public partial class HisSereServRehaDAO : EntityBase
    {
        public List<V_HIS_SERE_SERV_REHA> GetView(HisSereServRehaSO search, CommonParam param)
        {
            List<V_HIS_SERE_SERV_REHA> result = new List<V_HIS_SERE_SERV_REHA>();

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

        public HIS_SERE_SERV_REHA GetByCode(string code, HisSereServRehaSO search)
        {
            HIS_SERE_SERV_REHA result = null;

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
        
        public V_HIS_SERE_SERV_REHA GetViewById(long id, HisSereServRehaSO search)
        {
            V_HIS_SERE_SERV_REHA result = null;

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

        public V_HIS_SERE_SERV_REHA GetViewByCode(string code, HisSereServRehaSO search)
        {
            V_HIS_SERE_SERV_REHA result = null;

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

        public Dictionary<string, HIS_SERE_SERV_REHA> GetDicByCode(HisSereServRehaSO search, CommonParam param)
        {
            Dictionary<string, HIS_SERE_SERV_REHA> result = new Dictionary<string, HIS_SERE_SERV_REHA>();
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
