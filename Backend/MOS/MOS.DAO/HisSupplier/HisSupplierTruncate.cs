using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSupplier
{
    partial class HisSupplierTruncate : EntityBase
    {
        public HisSupplierTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SUPPLIER>();
        }

        private BridgeDAO<HIS_SUPPLIER> bridgeDAO;

        public bool Truncate(HIS_SUPPLIER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SUPPLIER> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
