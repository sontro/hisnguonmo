using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDispenseType
{
    partial class HisDispenseTypeCreate : EntityBase
    {
        public HisDispenseTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DISPENSE_TYPE>();
        }

        private BridgeDAO<HIS_DISPENSE_TYPE> bridgeDAO;

        public bool Create(HIS_DISPENSE_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DISPENSE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
