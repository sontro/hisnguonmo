using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSurgRemuDetail
{
    partial class HisSurgRemuDetailTruncate : EntityBase
    {
        public HisSurgRemuDetailTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SURG_REMU_DETAIL>();
        }

        private BridgeDAO<HIS_SURG_REMU_DETAIL> bridgeDAO;

        public bool Truncate(HIS_SURG_REMU_DETAIL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SURG_REMU_DETAIL> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
