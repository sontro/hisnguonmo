using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentHurt
{
    partial class HisAccidentHurtTruncate : EntityBase
    {
        public HisAccidentHurtTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_HURT>();
        }

        private BridgeDAO<HIS_ACCIDENT_HURT> bridgeDAO;

        public bool Truncate(HIS_ACCIDENT_HURT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ACCIDENT_HURT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
