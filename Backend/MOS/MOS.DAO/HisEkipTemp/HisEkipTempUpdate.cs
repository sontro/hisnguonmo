using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEkipTemp
{
    partial class HisEkipTempUpdate : EntityBase
    {
        public HisEkipTempUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EKIP_TEMP>();
        }

        private BridgeDAO<HIS_EKIP_TEMP> bridgeDAO;

        public bool Update(HIS_EKIP_TEMP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EKIP_TEMP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
