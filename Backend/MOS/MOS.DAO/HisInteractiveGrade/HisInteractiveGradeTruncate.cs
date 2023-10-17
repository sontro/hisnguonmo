using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisInteractiveGrade
{
    partial class HisInteractiveGradeTruncate : EntityBase
    {
        public HisInteractiveGradeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INTERACTIVE_GRADE>();
        }

        private BridgeDAO<HIS_INTERACTIVE_GRADE> bridgeDAO;

        public bool Truncate(HIS_INTERACTIVE_GRADE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_INTERACTIVE_GRADE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
