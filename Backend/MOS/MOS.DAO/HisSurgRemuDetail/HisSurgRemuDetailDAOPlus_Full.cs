using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSurgRemuDetail
{
    public partial class HisSurgRemuDetailDAO : EntityBase
    {
        public List<V_HIS_SURG_REMU_DETAIL> GetView(HisSurgRemuDetailSO search, CommonParam param)
        {
            List<V_HIS_SURG_REMU_DETAIL> result = new List<V_HIS_SURG_REMU_DETAIL>();

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

        public HIS_SURG_REMU_DETAIL GetByCode(string code, HisSurgRemuDetailSO search)
        {
            HIS_SURG_REMU_DETAIL result = null;

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
        
        public V_HIS_SURG_REMU_DETAIL GetViewById(long id, HisSurgRemuDetailSO search)
        {
            V_HIS_SURG_REMU_DETAIL result = null;

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

        public V_HIS_SURG_REMU_DETAIL GetViewByCode(string code, HisSurgRemuDetailSO search)
        {
            V_HIS_SURG_REMU_DETAIL result = null;

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

        public Dictionary<string, HIS_SURG_REMU_DETAIL> GetDicByCode(HisSurgRemuDetailSO search, CommonParam param)
        {
            Dictionary<string, HIS_SURG_REMU_DETAIL> result = new Dictionary<string, HIS_SURG_REMU_DETAIL>();
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
