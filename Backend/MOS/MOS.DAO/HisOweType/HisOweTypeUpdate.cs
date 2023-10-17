using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisOweType
{
    partial class HisOweTypeUpdate : EntityBase
    {
        public HisOweTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_OWE_TYPE>();
        }

        private BridgeDAO<HIS_OWE_TYPE> bridgeDAO;

        public bool Update(HIS_OWE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_OWE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
