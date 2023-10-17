using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineInteractive
{
    public partial class HisMedicineInteractiveDAO : EntityBase
    {
        public List<V_HIS_MEDICINE_INTERACTIVE> GetView(HisMedicineInteractiveSO search, CommonParam param)
        {
            List<V_HIS_MEDICINE_INTERACTIVE> result = new List<V_HIS_MEDICINE_INTERACTIVE>();

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

        public HIS_MEDICINE_INTERACTIVE GetByCode(string code, HisMedicineInteractiveSO search)
        {
            HIS_MEDICINE_INTERACTIVE result = null;

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
        
        public V_HIS_MEDICINE_INTERACTIVE GetViewById(long id, HisMedicineInteractiveSO search)
        {
            V_HIS_MEDICINE_INTERACTIVE result = null;

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

        public V_HIS_MEDICINE_INTERACTIVE GetViewByCode(string code, HisMedicineInteractiveSO search)
        {
            V_HIS_MEDICINE_INTERACTIVE result = null;

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

        public Dictionary<string, HIS_MEDICINE_INTERACTIVE> GetDicByCode(HisMedicineInteractiveSO search, CommonParam param)
        {
            Dictionary<string, HIS_MEDICINE_INTERACTIVE> result = new Dictionary<string, HIS_MEDICINE_INTERACTIVE>();
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
