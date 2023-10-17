using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMaterialType
{
    public partial class HisMaterialTypeDAO : EntityBase
    {
        public List<V_HIS_MATERIAL_TYPE> GetView(HisMaterialTypeSO search, CommonParam param)
        {
            List<V_HIS_MATERIAL_TYPE> result = new List<V_HIS_MATERIAL_TYPE>();
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

        public V_HIS_MATERIAL_TYPE GetViewById(long id, HisMaterialTypeSO search)
        {
            V_HIS_MATERIAL_TYPE result = null;

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
