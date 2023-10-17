using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMetyMaty
{
    partial class HisMetyMatyCreate : EntityBase
    {
        public HisMetyMatyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_METY_MATY>();
        }

        private BridgeDAO<HIS_METY_MATY> bridgeDAO;

        public bool Create(HIS_METY_MATY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_METY_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
