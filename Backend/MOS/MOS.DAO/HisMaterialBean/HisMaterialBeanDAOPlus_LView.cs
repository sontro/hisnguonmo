using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMaterialBean
{
    public partial class HisMaterialBeanDAO : EntityBase
    {
        public List<L_HIS_MATERIAL_BEAN> GetLView(HisMaterialBeanSO search, CommonParam param)
        {
            List<L_HIS_MATERIAL_BEAN> result = new List<L_HIS_MATERIAL_BEAN>();
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

        public L_HIS_MATERIAL_BEAN GetLViewById(long id, HisMaterialBeanSO search)
        {
            L_HIS_MATERIAL_BEAN result = null;

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
