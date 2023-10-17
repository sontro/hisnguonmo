using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTransfusion
{
    partial class HisTransfusionUpdate : EntityBase
    {
        public HisTransfusionUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANSFUSION>();
        }

        private BridgeDAO<HIS_TRANSFUSION> bridgeDAO;

        public bool Update(HIS_TRANSFUSION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TRANSFUSION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
