using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceMety
{
    partial class HisServiceMetyUpdate : EntityBase
    {
        public HisServiceMetyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_METY>();
        }

        private BridgeDAO<HIS_SERVICE_METY> bridgeDAO;

        public bool Update(HIS_SERVICE_METY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERVICE_METY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
