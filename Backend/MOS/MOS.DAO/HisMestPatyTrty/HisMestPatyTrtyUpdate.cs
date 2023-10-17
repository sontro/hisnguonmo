using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestPatyTrty
{
    partial class HisMestPatyTrtyUpdate : EntityBase
    {
        public HisMestPatyTrtyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PATY_TRTY>();
        }

        private BridgeDAO<HIS_MEST_PATY_TRTY> bridgeDAO;

        public bool Update(HIS_MEST_PATY_TRTY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEST_PATY_TRTY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
