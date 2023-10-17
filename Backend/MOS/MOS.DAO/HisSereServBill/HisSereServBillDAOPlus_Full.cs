using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServBill
{
    public partial class HisSereServBillDAO : EntityBase
    {
        public List<V_HIS_SERE_SERV_BILL> GetView(HisSereServBillSO search, CommonParam param)
        {
            List<V_HIS_SERE_SERV_BILL> result = new List<V_HIS_SERE_SERV_BILL>();

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

        public HIS_SERE_SERV_BILL GetByCode(string code, HisSereServBillSO search)
        {
            HIS_SERE_SERV_BILL result = null;

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
        
        public V_HIS_SERE_SERV_BILL GetViewById(long id, HisSereServBillSO search)
        {
            V_HIS_SERE_SERV_BILL result = null;

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

        public V_HIS_SERE_SERV_BILL GetViewByCode(string code, HisSereServBillSO search)
        {
            V_HIS_SERE_SERV_BILL result = null;

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

        public Dictionary<string, HIS_SERE_SERV_BILL> GetDicByCode(HisSereServBillSO search, CommonParam param)
        {
            Dictionary<string, HIS_SERE_SERV_BILL> result = new Dictionary<string, HIS_SERE_SERV_BILL>();
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
