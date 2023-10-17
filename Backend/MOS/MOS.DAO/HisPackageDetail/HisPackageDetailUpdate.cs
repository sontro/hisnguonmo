using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPackageDetail
{
    partial class HisPackageDetailUpdate : EntityBase
    {
        public HisPackageDetailUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PACKAGE_DETAIL>();
        }

        private BridgeDAO<HIS_PACKAGE_DETAIL> bridgeDAO;

        public bool Update(HIS_PACKAGE_DETAIL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PACKAGE_DETAIL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
