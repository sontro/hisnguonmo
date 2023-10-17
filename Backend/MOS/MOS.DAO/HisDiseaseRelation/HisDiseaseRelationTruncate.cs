using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDiseaseRelation
{
    partial class HisDiseaseRelationTruncate : EntityBase
    {
        public HisDiseaseRelationTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DISEASE_RELATION>();
        }

        private BridgeDAO<HIS_DISEASE_RELATION> bridgeDAO;

        public bool Truncate(HIS_DISEASE_RELATION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DISEASE_RELATION> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
