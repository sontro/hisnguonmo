using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRegimenHiv
{
    partial class HisRegimenHivTruncate : EntityBase
    {
        public HisRegimenHivTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REGIMEN_HIV>();
        }

        private BridgeDAO<HIS_REGIMEN_HIV> bridgeDAO;

        public bool Truncate(HIS_REGIMEN_HIV data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_REGIMEN_HIV> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
