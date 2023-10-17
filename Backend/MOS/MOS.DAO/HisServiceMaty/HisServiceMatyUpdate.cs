using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceMaty
{
    partial class HisServiceMatyUpdate : EntityBase
    {
        public HisServiceMatyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_MATY>();
        }

        private BridgeDAO<HIS_SERVICE_MATY> bridgeDAO;

        public bool Update(HIS_SERVICE_MATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERVICE_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
