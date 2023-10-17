using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPackageDetail
{
    partial class HisPackageDetailTruncate : EntityBase
    {
        public HisPackageDetailTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PACKAGE_DETAIL>();
        }

        private BridgeDAO<HIS_PACKAGE_DETAIL> bridgeDAO;

        public bool Truncate(HIS_PACKAGE_DETAIL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PACKAGE_DETAIL> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
