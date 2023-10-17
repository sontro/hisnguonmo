using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPrepareMaty
{
    public partial class HisPrepareMatyDAO : EntityBase
    {
        public List<V_HIS_PREPARE_MATY> GetView(HisPrepareMatySO search, CommonParam param)
        {
            List<V_HIS_PREPARE_MATY> result = new List<V_HIS_PREPARE_MATY>();

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

        public HIS_PREPARE_MATY GetByCode(string code, HisPrepareMatySO search)
        {
            HIS_PREPARE_MATY result = null;

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
        
        public V_HIS_PREPARE_MATY GetViewById(long id, HisPrepareMatySO search)
        {
            V_HIS_PREPARE_MATY result = null;

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

        public V_HIS_PREPARE_MATY GetViewByCode(string code, HisPrepareMatySO search)
        {
            V_HIS_PREPARE_MATY result = null;

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

        public Dictionary<string, HIS_PREPARE_MATY> GetDicByCode(HisPrepareMatySO search, CommonParam param)
        {
            Dictionary<string, HIS_PREPARE_MATY> result = new Dictionary<string, HIS_PREPARE_MATY>();
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
