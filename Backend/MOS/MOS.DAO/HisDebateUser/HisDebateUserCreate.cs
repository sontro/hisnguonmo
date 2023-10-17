using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDebateUser
{
    partial class HisDebateUserCreate : EntityBase
    {
        public HisDebateUserCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE_USER>();
        }

        private BridgeDAO<HIS_DEBATE_USER> bridgeDAO;

        public bool Create(HIS_DEBATE_USER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DEBATE_USER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
