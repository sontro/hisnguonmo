using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediOrg
{
    partial class HisMediOrgUpdate : EntityBase
    {
        public HisMediOrgUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_ORG>();
        }

        private BridgeDAO<HIS_MEDI_ORG> bridgeDAO;

        public bool Update(HIS_MEDI_ORG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDI_ORG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
