using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceMaty
{
    partial class HisServiceMatyCreate : EntityBase
    {
        public HisServiceMatyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_MATY>();
        }

        private BridgeDAO<HIS_SERVICE_MATY> bridgeDAO;

        public bool Create(HIS_SERVICE_MATY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
