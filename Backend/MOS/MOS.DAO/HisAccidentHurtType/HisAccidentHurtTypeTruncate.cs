using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentHurtType
{
    partial class HisAccidentHurtTypeTruncate : EntityBase
    {
        public HisAccidentHurtTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_HURT_TYPE>();
        }

        private BridgeDAO<HIS_ACCIDENT_HURT_TYPE> bridgeDAO;

        public bool Truncate(HIS_ACCIDENT_HURT_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ACCIDENT_HURT_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
