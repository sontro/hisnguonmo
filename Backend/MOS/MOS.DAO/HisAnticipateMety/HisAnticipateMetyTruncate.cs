using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAnticipateMety
{
    partial class HisAnticipateMetyTruncate : EntityBase
    {
        public HisAnticipateMetyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTICIPATE_METY>();
        }

        private BridgeDAO<HIS_ANTICIPATE_METY> bridgeDAO;

        public bool Truncate(HIS_ANTICIPATE_METY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ANTICIPATE_METY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
