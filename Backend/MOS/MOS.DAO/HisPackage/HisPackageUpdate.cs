using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPackage
{
    partial class HisPackageUpdate : EntityBase
    {
        public HisPackageUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PACKAGE>();
        }

        private BridgeDAO<HIS_PACKAGE> bridgeDAO;

        public bool Update(HIS_PACKAGE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PACKAGE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
