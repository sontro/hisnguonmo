using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDispense
{
    partial class HisDispenseCreate : EntityBase
    {
        public HisDispenseCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DISPENSE>();
        }

        private BridgeDAO<HIS_DISPENSE> bridgeDAO;

        public bool Create(HIS_DISPENSE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DISPENSE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
