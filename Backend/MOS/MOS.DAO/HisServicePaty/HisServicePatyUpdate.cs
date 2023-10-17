using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServicePaty
{
    partial class HisServicePatyUpdate : EntityBase
    {
        public HisServicePatyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_PATY>();
        }

        private BridgeDAO<HIS_SERVICE_PATY> bridgeDAO;

        public bool Update(HIS_SERVICE_PATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERVICE_PATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
