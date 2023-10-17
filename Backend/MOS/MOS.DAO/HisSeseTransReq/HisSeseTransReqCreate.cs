using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSeseTransReq
{
    partial class HisSeseTransReqCreate : EntityBase
    {
        public HisSeseTransReqCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SESE_TRANS_REQ>();
        }

        private BridgeDAO<HIS_SESE_TRANS_REQ> bridgeDAO;

        public bool Create(HIS_SESE_TRANS_REQ data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SESE_TRANS_REQ> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
