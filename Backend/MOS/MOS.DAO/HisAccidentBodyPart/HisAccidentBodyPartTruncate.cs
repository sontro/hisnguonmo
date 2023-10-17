using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentBodyPart
{
    partial class HisAccidentBodyPartTruncate : EntityBase
    {
        public HisAccidentBodyPartTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_BODY_PART>();
        }

        private BridgeDAO<HIS_ACCIDENT_BODY_PART> bridgeDAO;

        public bool Truncate(HIS_ACCIDENT_BODY_PART data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ACCIDENT_BODY_PART> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
