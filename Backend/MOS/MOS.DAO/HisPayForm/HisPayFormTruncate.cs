using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPayForm
{
    partial class HisPayFormTruncate : EntityBase
    {
        public HisPayFormTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PAY_FORM>();
        }

        private BridgeDAO<HIS_PAY_FORM> bridgeDAO;

        public bool Truncate(HIS_PAY_FORM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PAY_FORM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
