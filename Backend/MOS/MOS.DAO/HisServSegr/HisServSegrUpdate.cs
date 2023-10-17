using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServSegr
{
    partial class HisServSegrUpdate : EntityBase
    {
        public HisServSegrUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERV_SEGR>();
        }

        private BridgeDAO<HIS_SERV_SEGR> bridgeDAO;

        public bool Update(HIS_SERV_SEGR data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERV_SEGR> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
