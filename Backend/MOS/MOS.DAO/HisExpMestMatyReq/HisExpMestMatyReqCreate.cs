using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestMatyReq
{
    partial class HisExpMestMatyReqCreate : EntityBase
    {
        public HisExpMestMatyReqCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_MATY_REQ>();
        }

        private BridgeDAO<HIS_EXP_MEST_MATY_REQ> bridgeDAO;

        public bool Create(HIS_EXP_MEST_MATY_REQ data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EXP_MEST_MATY_REQ> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
