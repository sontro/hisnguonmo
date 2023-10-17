using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisHoreHandover
{
    partial class HisHoreHandoverCreate : EntityBase
    {
        public HisHoreHandoverCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HORE_HANDOVER>();
        }

        private BridgeDAO<HIS_HORE_HANDOVER> bridgeDAO;

        public bool Create(HIS_HORE_HANDOVER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_HORE_HANDOVER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
