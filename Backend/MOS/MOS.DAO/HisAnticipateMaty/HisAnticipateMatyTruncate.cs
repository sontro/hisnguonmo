using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAnticipateMaty
{
    partial class HisAnticipateMatyTruncate : EntityBase
    {
        public HisAnticipateMatyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTICIPATE_MATY>();
        }

        private BridgeDAO<HIS_ANTICIPATE_MATY> bridgeDAO;

        public bool Truncate(HIS_ANTICIPATE_MATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ANTICIPATE_MATY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
