using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDiseaseType
{
    partial class HisDiseaseTypeTruncate : EntityBase
    {
        public HisDiseaseTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DISEASE_TYPE>();
        }

        private BridgeDAO<HIS_DISEASE_TYPE> bridgeDAO;

        public bool Truncate(HIS_DISEASE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DISEASE_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
