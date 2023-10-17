using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAwareness
{
    partial class HisAwarenessTruncate : EntityBase
    {
        public HisAwarenessTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_AWARENESS>();
        }

        private BridgeDAO<HIS_AWARENESS> bridgeDAO;

        public bool Truncate(HIS_AWARENESS data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_AWARENESS> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
