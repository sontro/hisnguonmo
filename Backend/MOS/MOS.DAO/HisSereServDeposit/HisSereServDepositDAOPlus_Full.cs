using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServDeposit
{
    public partial class HisSereServDepositDAO : EntityBase
    {
        public List<V_HIS_SERE_SERV_DEPOSIT> GetView(HisSereServDepositSO search, CommonParam param)
        {
            List<V_HIS_SERE_SERV_DEPOSIT> result = new List<V_HIS_SERE_SERV_DEPOSIT>();

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

        public HIS_SERE_SERV_DEPOSIT GetByCode(string code, HisSereServDepositSO search)
        {
            HIS_SERE_SERV_DEPOSIT result = null;

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
        
        public V_HIS_SERE_SERV_DEPOSIT GetViewById(long id, HisSereServDepositSO search)
        {
            V_HIS_SERE_SERV_DEPOSIT result = null;

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

        public V_HIS_SERE_SERV_DEPOSIT GetViewByCode(string code, HisSereServDepositSO search)
        {
            V_HIS_SERE_SERV_DEPOSIT result = null;

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

        public Dictionary<string, HIS_SERE_SERV_DEPOSIT> GetDicByCode(HisSereServDepositSO search, CommonParam param)
        {
            Dictionary<string, HIS_SERE_SERV_DEPOSIT> result = new Dictionary<string, HIS_SERE_SERV_DEPOSIT>();
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
