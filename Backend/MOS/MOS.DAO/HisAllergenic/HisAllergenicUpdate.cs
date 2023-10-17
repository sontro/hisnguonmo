using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAllergenic
{
    partial class HisAllergenicUpdate : EntityBase
    {
        public HisAllergenicUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ALLERGENIC>();
        }

        private BridgeDAO<HIS_ALLERGENIC> bridgeDAO;

        public bool Update(HIS_ALLERGENIC data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ALLERGENIC> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
