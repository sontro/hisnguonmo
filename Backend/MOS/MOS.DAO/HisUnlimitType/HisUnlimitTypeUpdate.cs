using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisUnlimitType
{
    partial class HisUnlimitTypeUpdate : EntityBase
    {
        public HisUnlimitTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_UNLIMIT_TYPE>();
        }

        private BridgeDAO<HIS_UNLIMIT_TYPE> bridgeDAO;

        public bool Update(HIS_UNLIMIT_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_UNLIMIT_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
