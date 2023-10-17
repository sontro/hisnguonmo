using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestMaterial
{
    public partial class HisExpMestMaterialDAO : EntityBase
    {
        public List<V_HIS_EXP_MEST_MATERIAL> GetView(HisExpMestMaterialSO search, CommonParam param)
        {
            List<V_HIS_EXP_MEST_MATERIAL> result = new List<V_HIS_EXP_MEST_MATERIAL>();

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

        public HIS_EXP_MEST_MATERIAL GetByCode(string code, HisExpMestMaterialSO search)
        {
            HIS_EXP_MEST_MATERIAL result = null;

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
        
        public V_HIS_EXP_MEST_MATERIAL GetViewById(long id, HisExpMestMaterialSO search)
        {
            V_HIS_EXP_MEST_MATERIAL result = null;

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

        public V_HIS_EXP_MEST_MATERIAL GetViewByCode(string code, HisExpMestMaterialSO search)
        {
            V_HIS_EXP_MEST_MATERIAL result = null;

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

        public Dictionary<string, HIS_EXP_MEST_MATERIAL> GetDicByCode(HisExpMestMaterialSO search, CommonParam param)
        {
            Dictionary<string, HIS_EXP_MEST_MATERIAL> result = new Dictionary<string, HIS_EXP_MEST_MATERIAL>();
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
