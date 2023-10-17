using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTransfusionSum
{
    partial class HisTransfusionSumUpdate : EntityBase
    {
        public HisTransfusionSumUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANSFUSION_SUM>();
        }

        private BridgeDAO<HIS_TRANSFUSION_SUM> bridgeDAO;

        public bool Update(HIS_TRANSFUSION_SUM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TRANSFUSION_SUM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
