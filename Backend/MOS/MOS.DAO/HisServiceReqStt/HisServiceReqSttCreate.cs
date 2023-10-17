using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceReqStt
{
    partial class HisServiceReqSttCreate : EntityBase
    {
        public HisServiceReqSttCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_REQ_STT>();
        }

        private BridgeDAO<HIS_SERVICE_REQ_STT> bridgeDAO;

        public bool Create(HIS_SERVICE_REQ_STT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE_REQ_STT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
