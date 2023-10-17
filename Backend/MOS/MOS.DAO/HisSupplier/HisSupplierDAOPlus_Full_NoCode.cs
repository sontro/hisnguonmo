using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSupplier
{
    public partial class HisSupplierDAO : EntityBase
    {
        public List<V_HIS_SUPPLIER> GetView(HisSupplierSO search, CommonParam param)
        {
            List<V_HIS_SUPPLIER> result = new List<V_HIS_SUPPLIER>();
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

        public V_HIS_SUPPLIER GetViewById(long id, HisSupplierSO search)
        {
            V_HIS_SUPPLIER result = null;

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
