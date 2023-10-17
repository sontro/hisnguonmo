using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmrForm
{
    partial class HisEmrFormTruncate : EntityBase
    {
        public HisEmrFormTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMR_FORM>();
        }

        private BridgeDAO<HIS_EMR_FORM> bridgeDAO;

        public bool Truncate(HIS_EMR_FORM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EMR_FORM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
