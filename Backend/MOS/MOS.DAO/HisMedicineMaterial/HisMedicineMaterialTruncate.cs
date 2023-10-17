using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineMaterial
{
    partial class HisMedicineMaterialTruncate : EntityBase
    {
        public HisMedicineMaterialTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_MATERIAL>();
        }

        private BridgeDAO<HIS_MEDICINE_MATERIAL> bridgeDAO;

        public bool Truncate(HIS_MEDICINE_MATERIAL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDICINE_MATERIAL> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
