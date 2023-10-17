using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisIcdGroup
{
    partial class HisIcdGroupUpdate : EntityBase
    {
        public HisIcdGroupUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ICD_GROUP>();
        }

        private BridgeDAO<HIS_ICD_GROUP> bridgeDAO;

        public bool Update(HIS_ICD_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ICD_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
