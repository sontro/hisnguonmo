using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSupplier
{
    partial class HisSupplierUpdate : EntityBase
    {
        public HisSupplierUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SUPPLIER>();
        }

        private BridgeDAO<HIS_SUPPLIER> bridgeDAO;

        public bool Update(HIS_SUPPLIER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SUPPLIER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
