using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPackingType
{
    partial class HisPackingTypeCreate : EntityBase
    {
        public HisPackingTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PACKING_TYPE>();
        }

        private BridgeDAO<HIS_PACKING_TYPE> bridgeDAO;

        public bool Create(HIS_PACKING_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PACKING_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
