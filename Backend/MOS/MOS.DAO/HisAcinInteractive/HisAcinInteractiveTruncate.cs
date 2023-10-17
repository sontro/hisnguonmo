using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAcinInteractive
{
    partial class HisAcinInteractiveTruncate : EntityBase
    {
        public HisAcinInteractiveTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACIN_INTERACTIVE>();
        }

        private BridgeDAO<HIS_ACIN_INTERACTIVE> bridgeDAO;

        public bool Truncate(HIS_ACIN_INTERACTIVE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ACIN_INTERACTIVE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
