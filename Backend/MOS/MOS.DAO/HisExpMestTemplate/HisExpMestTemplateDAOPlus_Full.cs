using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestTemplate
{
    public partial class HisExpMestTemplateDAO : EntityBase
    {
        public List<V_HIS_EXP_MEST_TEMPLATE> GetView(HisExpMestTemplateSO search, CommonParam param)
        {
            List<V_HIS_EXP_MEST_TEMPLATE> result = new List<V_HIS_EXP_MEST_TEMPLATE>();

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

        public HIS_EXP_MEST_TEMPLATE GetByCode(string code, HisExpMestTemplateSO search)
        {
            HIS_EXP_MEST_TEMPLATE result = null;

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
        
        public V_HIS_EXP_MEST_TEMPLATE GetViewById(long id, HisExpMestTemplateSO search)
        {
            V_HIS_EXP_MEST_TEMPLATE result = null;

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

        public V_HIS_EXP_MEST_TEMPLATE GetViewByCode(string code, HisExpMestTemplateSO search)
        {
            V_HIS_EXP_MEST_TEMPLATE result = null;

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

        public Dictionary<string, HIS_EXP_MEST_TEMPLATE> GetDicByCode(HisExpMestTemplateSO search, CommonParam param)
        {
            Dictionary<string, HIS_EXP_MEST_TEMPLATE> result = new Dictionary<string, HIS_EXP_MEST_TEMPLATE>();
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
