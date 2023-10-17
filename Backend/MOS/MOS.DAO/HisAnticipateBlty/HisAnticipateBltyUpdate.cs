using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAnticipateBlty
{
    partial class HisAnticipateBltyUpdate : EntityBase
    {
        public HisAnticipateBltyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTICIPATE_BLTY>();
        }

        private BridgeDAO<HIS_ANTICIPATE_BLTY> bridgeDAO;

        public bool Update(HIS_ANTICIPATE_BLTY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ANTICIPATE_BLTY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
