using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHeinServiceType
{
    partial class HisHeinServiceTypeUpdate : EntityBase
    {
        public HisHeinServiceTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HEIN_SERVICE_TYPE>();
        }

        private BridgeDAO<HIS_HEIN_SERVICE_TYPE> bridgeDAO;

        public bool Update(HIS_HEIN_SERVICE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_HEIN_SERVICE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
