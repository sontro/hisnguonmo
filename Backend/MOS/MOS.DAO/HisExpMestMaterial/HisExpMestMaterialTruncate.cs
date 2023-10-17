using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExpMestMaterial
{
    partial class HisExpMestMaterialTruncate : EntityBase
    {
        public HisExpMestMaterialTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_MATERIAL>();
        }

        private BridgeDAO<HIS_EXP_MEST_MATERIAL> bridgeDAO;

        public bool Truncate(HIS_EXP_MEST_MATERIAL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EXP_MEST_MATERIAL> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
