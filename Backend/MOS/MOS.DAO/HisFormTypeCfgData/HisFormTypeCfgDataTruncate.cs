using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisFormTypeCfgData
{
    partial class HisFormTypeCfgDataTruncate : EntityBase
    {
        public HisFormTypeCfgDataTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FORM_TYPE_CFG_DATA>();
        }

        private BridgeDAO<HIS_FORM_TYPE_CFG_DATA> bridgeDAO;

        public bool Truncate(HIS_FORM_TYPE_CFG_DATA data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_FORM_TYPE_CFG_DATA> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
