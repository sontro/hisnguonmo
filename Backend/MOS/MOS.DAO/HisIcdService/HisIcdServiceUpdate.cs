using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisIcdService
{
    partial class HisIcdServiceUpdate : EntityBase
    {
        public HisIcdServiceUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ICD_SERVICE>();
        }

        private BridgeDAO<HIS_ICD_SERVICE> bridgeDAO;

        public bool Update(HIS_ICD_SERVICE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ICD_SERVICE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
