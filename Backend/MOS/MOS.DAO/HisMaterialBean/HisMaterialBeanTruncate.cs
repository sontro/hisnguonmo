using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMaterialBean
{
    partial class HisMaterialBeanTruncate : EntityBase
    {
        public HisMaterialBeanTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATERIAL_BEAN>();
        }

        private BridgeDAO<HIS_MATERIAL_BEAN> bridgeDAO;

        public bool Truncate(HIS_MATERIAL_BEAN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MATERIAL_BEAN> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
