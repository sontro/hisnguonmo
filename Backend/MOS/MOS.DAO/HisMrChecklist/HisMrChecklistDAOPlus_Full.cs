using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMrChecklist
{
    public partial class HisMrChecklistDAO : EntityBase
    {
        public List<V_HIS_MR_CHECKLIST> GetView(HisMrChecklistSO search, CommonParam param)
        {
            List<V_HIS_MR_CHECKLIST> result = new List<V_HIS_MR_CHECKLIST>();

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

        public HIS_MR_CHECKLIST GetByCode(string code, HisMrChecklistSO search)
        {
            HIS_MR_CHECKLIST result = null;

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
        
        public V_HIS_MR_CHECKLIST GetViewById(long id, HisMrChecklistSO search)
        {
            V_HIS_MR_CHECKLIST result = null;

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

        public V_HIS_MR_CHECKLIST GetViewByCode(string code, HisMrChecklistSO search)
        {
            V_HIS_MR_CHECKLIST result = null;

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

        public Dictionary<string, HIS_MR_CHECKLIST> GetDicByCode(HisMrChecklistSO search, CommonParam param)
        {
            Dictionary<string, HIS_MR_CHECKLIST> result = new Dictionary<string, HIS_MR_CHECKLIST>();
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
