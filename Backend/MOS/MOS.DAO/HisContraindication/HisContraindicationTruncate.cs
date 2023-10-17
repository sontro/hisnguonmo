using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisContraindication
{
    partial class HisContraindicationTruncate : EntityBase
    {
        public HisContraindicationTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CONTRAINDICATION>();
        }

        private BridgeDAO<HIS_CONTRAINDICATION> bridgeDAO;

        public bool Truncate(HIS_CONTRAINDICATION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_CONTRAINDICATION> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
