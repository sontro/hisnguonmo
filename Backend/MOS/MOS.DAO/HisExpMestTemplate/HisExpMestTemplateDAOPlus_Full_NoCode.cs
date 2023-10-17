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
    }
}
