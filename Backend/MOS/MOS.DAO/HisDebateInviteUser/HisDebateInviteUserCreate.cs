using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDebateInviteUser
{
    partial class HisDebateInviteUserCreate : EntityBase
    {
        public HisDebateInviteUserCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE_INVITE_USER>();
        }

        private BridgeDAO<HIS_DEBATE_INVITE_USER> bridgeDAO;

        public bool Create(HIS_DEBATE_INVITE_USER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DEBATE_INVITE_USER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
