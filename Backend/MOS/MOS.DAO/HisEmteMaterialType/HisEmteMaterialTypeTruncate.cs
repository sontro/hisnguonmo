using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmteMaterialType
{
    partial class HisEmteMaterialTypeTruncate : EntityBase
    {
        public HisEmteMaterialTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMTE_MATERIAL_TYPE>();
        }

        private BridgeDAO<HIS_EMTE_MATERIAL_TYPE> bridgeDAO;

        public bool Truncate(HIS_EMTE_MATERIAL_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EMTE_MATERIAL_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
