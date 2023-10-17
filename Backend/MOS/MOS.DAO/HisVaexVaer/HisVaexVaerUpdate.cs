using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaexVaer
{
    partial class HisVaexVaerUpdate : EntityBase
    {
        public HisVaexVaerUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VAEX_VAER>();
        }

        private BridgeDAO<HIS_VAEX_VAER> bridgeDAO;

        public bool Update(HIS_VAEX_VAER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_VAEX_VAER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
