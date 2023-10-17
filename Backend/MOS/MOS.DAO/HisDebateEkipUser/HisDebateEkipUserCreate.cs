using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDebateEkipUser
{
    partial class HisDebateEkipUserCreate : EntityBase
    {
        public HisDebateEkipUserCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE_EKIP_USER>();
        }

        private BridgeDAO<HIS_DEBATE_EKIP_USER> bridgeDAO;

        public bool Create(HIS_DEBATE_EKIP_USER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DEBATE_EKIP_USER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
