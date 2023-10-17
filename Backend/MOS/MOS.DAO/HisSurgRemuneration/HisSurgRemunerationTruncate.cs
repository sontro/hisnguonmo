using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSurgRemuneration
{
    partial class HisSurgRemunerationTruncate : EntityBase
    {
        public HisSurgRemunerationTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SURG_REMUNERATION>();
        }

        private BridgeDAO<HIS_SURG_REMUNERATION> bridgeDAO;

        public bool Truncate(HIS_SURG_REMUNERATION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SURG_REMUNERATION> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
