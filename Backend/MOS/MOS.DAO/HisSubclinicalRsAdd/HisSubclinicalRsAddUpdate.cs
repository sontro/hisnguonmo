using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSubclinicalRsAdd
{
    partial class HisSubclinicalRsAddUpdate : EntityBase
    {
        public HisSubclinicalRsAddUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SUBCLINICAL_RS_ADD>();
        }

        private BridgeDAO<HIS_SUBCLINICAL_RS_ADD> bridgeDAO;

        public bool Update(HIS_SUBCLINICAL_RS_ADD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SUBCLINICAL_RS_ADD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
