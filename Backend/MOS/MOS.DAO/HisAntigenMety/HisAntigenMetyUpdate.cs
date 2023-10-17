using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAntigenMety
{
    partial class HisAntigenMetyUpdate : EntityBase
    {
        public HisAntigenMetyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIGEN_METY>();
        }

        private BridgeDAO<HIS_ANTIGEN_METY> bridgeDAO;

        public bool Update(HIS_ANTIGEN_METY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ANTIGEN_METY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
