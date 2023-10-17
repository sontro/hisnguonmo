using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskGeneral
{
    partial class HisKskGeneralTruncate : EntityBase
    {
        public HisKskGeneralTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_GENERAL>();
        }

        private BridgeDAO<HIS_KSK_GENERAL> bridgeDAO;

        public bool Truncate(HIS_KSK_GENERAL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_KSK_GENERAL> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
