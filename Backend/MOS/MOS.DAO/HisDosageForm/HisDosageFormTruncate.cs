using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDosageForm
{
    partial class HisDosageFormTruncate : EntityBase
    {
        public HisDosageFormTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DOSAGE_FORM>();
        }

        private BridgeDAO<HIS_DOSAGE_FORM> bridgeDAO;

        public bool Truncate(HIS_DOSAGE_FORM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DOSAGE_FORM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
