using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBcsMatyReqDt
{
    partial class HisBcsMatyReqDtCreate : EntityBase
    {
        public HisBcsMatyReqDtCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BCS_MATY_REQ_DT>();
        }

        private BridgeDAO<HIS_BCS_MATY_REQ_DT> bridgeDAO;

        public bool Create(HIS_BCS_MATY_REQ_DT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BCS_MATY_REQ_DT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
