using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMatyMaty
{
    partial class HisMatyMatyUpdate : EntityBase
    {
        public HisMatyMatyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATY_MATY>();
        }

        private BridgeDAO<HIS_MATY_MATY> bridgeDAO;

        public bool Update(HIS_MATY_MATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MATY_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
