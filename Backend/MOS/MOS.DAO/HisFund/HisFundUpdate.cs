using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisFund
{
    partial class HisFundUpdate : EntityBase
    {
        public HisFundUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FUND>();
        }

        private BridgeDAO<HIS_FUND> bridgeDAO;

        public bool Update(HIS_FUND data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_FUND> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
