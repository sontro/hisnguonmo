using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMaterialPaty
{
    public partial class HisMaterialPatyDAO : EntityBase
    {
        public List<V_HIS_MATERIAL_PATY> GetView(HisMaterialPatySO search, CommonParam param)
        {
            List<V_HIS_MATERIAL_PATY> result = new List<V_HIS_MATERIAL_PATY>();

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

        public HIS_MATERIAL_PATY GetByCode(string code, HisMaterialPatySO search)
        {
            HIS_MATERIAL_PATY result = null;

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
        
        public V_HIS_MATERIAL_PATY GetViewById(long id, HisMaterialPatySO search)
        {
            V_HIS_MATERIAL_PATY result = null;

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

        public V_HIS_MATERIAL_PATY GetViewByCode(string code, HisMaterialPatySO search)
        {
            V_HIS_MATERIAL_PATY result = null;

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

        public Dictionary<string, HIS_MATERIAL_PATY> GetDicByCode(HisMaterialPatySO search, CommonParam param)
        {
            Dictionary<string, HIS_MATERIAL_PATY> result = new Dictionary<string, HIS_MATERIAL_PATY>();
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
