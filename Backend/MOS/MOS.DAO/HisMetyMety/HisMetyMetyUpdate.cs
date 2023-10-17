using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMetyMety
{
    partial class HisMetyMetyUpdate : EntityBase
    {
        public HisMetyMetyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_METY_METY>();
        }

        private BridgeDAO<HIS_METY_METY> bridgeDAO;

        public bool Update(HIS_METY_METY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_METY_METY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
