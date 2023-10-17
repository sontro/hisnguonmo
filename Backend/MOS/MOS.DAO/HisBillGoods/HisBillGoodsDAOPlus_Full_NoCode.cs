using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBillGoods
{
    public partial class HisBillGoodsDAO : EntityBase
    {
        public List<V_HIS_BILL_GOODS> GetView(HisBillGoodsSO search, CommonParam param)
        {
            List<V_HIS_BILL_GOODS> result = new List<V_HIS_BILL_GOODS>();
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

        public V_HIS_BILL_GOODS GetViewById(long id, HisBillGoodsSO search)
        {
            V_HIS_BILL_GOODS result = null;

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
