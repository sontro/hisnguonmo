using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentHelmet
{
    partial class HisAccidentHelmetTruncate : EntityBase
    {
        public HisAccidentHelmetTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_HELMET>();
        }

        private BridgeDAO<HIS_ACCIDENT_HELMET> bridgeDAO;

        public bool Truncate(HIS_ACCIDENT_HELMET data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ACCIDENT_HELMET> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
