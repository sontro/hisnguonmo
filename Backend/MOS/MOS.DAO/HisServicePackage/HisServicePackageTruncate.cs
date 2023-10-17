using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServicePackage
{
    partial class HisServicePackageTruncate : EntityBase
    {
        public HisServicePackageTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_PACKAGE>();
        }

        private BridgeDAO<HIS_SERVICE_PACKAGE> bridgeDAO;

        public bool Truncate(HIS_SERVICE_PACKAGE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERVICE_PACKAGE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
