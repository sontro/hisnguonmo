using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMaterialTypeMap
{
    partial class HisMaterialTypeMapTruncate : EntityBase
    {
        public HisMaterialTypeMapTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATERIAL_TYPE_MAP>();
        }

        private BridgeDAO<HIS_MATERIAL_TYPE_MAP> bridgeDAO;

        public bool Truncate(HIS_MATERIAL_TYPE_MAP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MATERIAL_TYPE_MAP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
