using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMaterialPaty
{
    partial class HisMaterialPatyTruncate : EntityBase
    {
        public HisMaterialPatyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATERIAL_PATY>();
        }

        private BridgeDAO<HIS_MATERIAL_PATY> bridgeDAO;

        public bool Truncate(HIS_MATERIAL_PATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MATERIAL_PATY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
