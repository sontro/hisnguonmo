using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMestMetyUnit
{
    partial class HisMestMetyUnitCreate : EntityBase
    {
        public HisMestMetyUnitCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_METY_UNIT>();
        }

        private BridgeDAO<HIS_MEST_METY_UNIT> bridgeDAO;

        public bool Create(HIS_MEST_METY_UNIT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEST_METY_UNIT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
