using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMaterial
{
    partial class HisMaterialTruncate : EntityBase
    {
        public HisMaterialTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATERIAL>();
        }

        private BridgeDAO<HIS_MATERIAL> bridgeDAO;

        public bool Truncate(HIS_MATERIAL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MATERIAL> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
