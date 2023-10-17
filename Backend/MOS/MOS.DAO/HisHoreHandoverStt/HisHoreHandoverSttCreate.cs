using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisHoreHandoverStt
{
    partial class HisHoreHandoverSttCreate : EntityBase
    {
        public HisHoreHandoverSttCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HORE_HANDOVER_STT>();
        }

        private BridgeDAO<HIS_HORE_HANDOVER_STT> bridgeDAO;

        public bool Create(HIS_HORE_HANDOVER_STT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_HORE_HANDOVER_STT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
