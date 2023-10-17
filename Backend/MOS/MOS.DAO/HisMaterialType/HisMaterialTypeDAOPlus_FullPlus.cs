using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMaterialType
{
    public partial class HisMaterialTypeDAO : EntityBase
    {
        public List<V_HIS_MATERIAL_TYPE_STOCK> GetStockView(HisMaterialTypeSO search, CommonParam param)
        {
            List<V_HIS_MATERIAL_TYPE_STOCK> result = new List<V_HIS_MATERIAL_TYPE_STOCK>();

            #region Logging Input Data
            try
            {
                Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            #endregion

            try
            {
                result = GetWorker.GetStockView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            #region Logging
            if (param.HasException)
            {
                LogInOut();
            }
            #endregion

            return result;
        }

    }
}
