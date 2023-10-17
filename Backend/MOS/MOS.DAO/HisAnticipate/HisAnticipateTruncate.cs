using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAnticipate
{
    partial class HisAnticipateTruncate : EntityBase
    {
        public HisAnticipateTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTICIPATE>();
        }

        private BridgeDAO<HIS_ANTICIPATE> bridgeDAO;

        public bool Truncate(HIS_ANTICIPATE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ANTICIPATE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
