using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestMaterial
{
    public partial class HisExpMestMaterialDAO : EntityBase
    {
        public List<L_HIS_EXP_MEST_MATERIAL> GetLView(HisExpMestMaterialSO search, CommonParam param)
        {
            List<L_HIS_EXP_MEST_MATERIAL> result = new List<L_HIS_EXP_MEST_MATERIAL>();
            try
            {
                result = GetWorker.GetLView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public L_HIS_EXP_MEST_MATERIAL GetLViewById(long id, HisExpMestMaterialSO search)
        {
            L_HIS_EXP_MEST_MATERIAL result = null;

            try
            {
                result = GetWorker.GetLViewById(id, search);
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
