using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceReqMety
{
    partial class HisServiceReqMetyCreate : EntityBase
    {
        public HisServiceReqMetyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_REQ_METY>();
        }

        private BridgeDAO<HIS_SERVICE_REQ_METY> bridgeDAO;

        public bool Create(HIS_SERVICE_REQ_METY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE_REQ_METY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
