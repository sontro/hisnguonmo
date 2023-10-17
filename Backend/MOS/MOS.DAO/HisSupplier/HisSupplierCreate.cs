using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSupplier
{
    partial class HisSupplierCreate : EntityBase
    {
        public HisSupplierCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SUPPLIER>();
        }

        private BridgeDAO<HIS_SUPPLIER> bridgeDAO;

        public bool Create(HIS_SUPPLIER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SUPPLIER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
