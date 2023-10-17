using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisFormTypeCfgData
{
    partial class HisFormTypeCfgDataUpdate : EntityBase
    {
        public HisFormTypeCfgDataUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FORM_TYPE_CFG_DATA>();
        }

        private BridgeDAO<HIS_FORM_TYPE_CFG_DATA> bridgeDAO;

        public bool Update(HIS_FORM_TYPE_CFG_DATA data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_FORM_TYPE_CFG_DATA> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
