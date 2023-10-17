using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAntibioticRequest
{
    partial class HisAntibioticRequestUpdate : EntityBase
    {
        public HisAntibioticRequestUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIBIOTIC_REQUEST>();
        }

        private BridgeDAO<HIS_ANTIBIOTIC_REQUEST> bridgeDAO;

        public bool Update(HIS_ANTIBIOTIC_REQUEST data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ANTIBIOTIC_REQUEST> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
