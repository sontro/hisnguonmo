using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisHeinApproval
{
    partial class HisHeinApprovalCreate : EntityBase
    {
        public HisHeinApprovalCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HEIN_APPROVAL>();
        }

        private BridgeDAO<HIS_HEIN_APPROVAL> bridgeDAO;

        public bool Create(HIS_HEIN_APPROVAL data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_HEIN_APPROVAL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
