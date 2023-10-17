using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTransReq
{
    partial class HisTransReqCreate : EntityBase
    {
        public HisTransReqCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANS_REQ>();
        }

        private BridgeDAO<HIS_TRANS_REQ> bridgeDAO;

        public bool Create(HIS_TRANS_REQ data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TRANS_REQ> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
