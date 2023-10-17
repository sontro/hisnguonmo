using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpMestMedicine
{
    partial class HisImpMestMedicineTruncate : EntityBase
    {
        public HisImpMestMedicineTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_MEDICINE>();
        }

        private BridgeDAO<HIS_IMP_MEST_MEDICINE> bridgeDAO;

        public bool Truncate(HIS_IMP_MEST_MEDICINE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_IMP_MEST_MEDICINE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
