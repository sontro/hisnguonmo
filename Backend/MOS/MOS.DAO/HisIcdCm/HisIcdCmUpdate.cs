using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisIcdCm
{
    partial class HisIcdCmUpdate : EntityBase
    {
        public HisIcdCmUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ICD_CM>();
        }

        private BridgeDAO<HIS_ICD_CM> bridgeDAO;

        public bool Update(HIS_ICD_CM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ICD_CM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
