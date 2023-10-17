using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServicePackage
{
    partial class HisServicePackageUpdate : EntityBase
    {
        public HisServicePackageUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_PACKAGE>();
        }

        private BridgeDAO<HIS_SERVICE_PACKAGE> bridgeDAO;

        public bool Update(HIS_SERVICE_PACKAGE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERVICE_PACKAGE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
