using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceReqMaty
{
    partial class HisServiceReqMatyCreate : EntityBase
    {
        public HisServiceReqMatyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_REQ_MATY>();
        }

        private BridgeDAO<HIS_SERVICE_REQ_MATY> bridgeDAO;

        public bool Create(HIS_SERVICE_REQ_MATY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE_REQ_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
