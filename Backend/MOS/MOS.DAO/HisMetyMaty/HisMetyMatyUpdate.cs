using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMetyMaty
{
    partial class HisMetyMatyUpdate : EntityBase
    {
        public HisMetyMatyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_METY_MATY>();
        }

        private BridgeDAO<HIS_METY_MATY> bridgeDAO;

        public bool Update(HIS_METY_MATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_METY_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
