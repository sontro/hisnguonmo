using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentPoison
{
    partial class HisAccidentPoisonTruncate : EntityBase
    {
        public HisAccidentPoisonTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_POISON>();
        }

        private BridgeDAO<HIS_ACCIDENT_POISON> bridgeDAO;

        public bool Truncate(HIS_ACCIDENT_POISON data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ACCIDENT_POISON> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
