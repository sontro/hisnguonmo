using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSurgRemuneration
{
    public partial class HisSurgRemunerationDAO : EntityBase
    {
        public List<V_HIS_SURG_REMUNERATION> GetView(HisSurgRemunerationSO search, CommonParam param)
        {
            List<V_HIS_SURG_REMUNERATION> result = new List<V_HIS_SURG_REMUNERATION>();

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

        public HIS_SURG_REMUNERATION GetByCode(string code, HisSurgRemunerationSO search)
        {
            HIS_SURG_REMUNERATION result = null;

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
        
        public V_HIS_SURG_REMUNERATION GetViewById(long id, HisSurgRemunerationSO search)
        {
            V_HIS_SURG_REMUNERATION result = null;

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

        public V_HIS_SURG_REMUNERATION GetViewByCode(string code, HisSurgRemunerationSO search)
        {
            V_HIS_SURG_REMUNERATION result = null;

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

        public Dictionary<string, HIS_SURG_REMUNERATION> GetDicByCode(HisSurgRemunerationSO search, CommonParam param)
        {
            Dictionary<string, HIS_SURG_REMUNERATION> result = new Dictionary<string, HIS_SURG_REMUNERATION>();
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
