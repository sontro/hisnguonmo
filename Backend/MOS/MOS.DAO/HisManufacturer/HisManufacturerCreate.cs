using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisManufacturer
{
    partial class HisManufacturerCreate : EntityBase
    {
        public HisManufacturerCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MANUFACTURER>();
        }

        private BridgeDAO<HIS_MANUFACTURER> bridgeDAO;

        public bool Create(HIS_MANUFACTURER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MANUFACTURER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
