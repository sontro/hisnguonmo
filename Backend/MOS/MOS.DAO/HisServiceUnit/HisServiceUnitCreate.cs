using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceUnit
{
    partial class HisServiceUnitCreate : EntityBase
    {
        public HisServiceUnitCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_UNIT>();
        }

        private BridgeDAO<HIS_SERVICE_UNIT> bridgeDAO;

        public bool Create(HIS_SERVICE_UNIT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE_UNIT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
