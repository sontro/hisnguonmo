using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAdr
{
    partial class HisAdrUpdate : EntityBase
    {
        public HisAdrUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ADR>();
        }

        private BridgeDAO<HIS_ADR> bridgeDAO;

        public bool Update(HIS_ADR data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ADR> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
