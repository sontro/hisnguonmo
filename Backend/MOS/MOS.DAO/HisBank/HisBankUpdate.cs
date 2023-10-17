using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBank
{
    partial class HisBankUpdate : EntityBase
    {
        public HisBankUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BANK>();
        }

        private BridgeDAO<HIS_BANK> bridgeDAO;

        public bool Update(HIS_BANK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BANK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
