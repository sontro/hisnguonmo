using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAntigen
{
    partial class HisAntigenUpdate : EntityBase
    {
        public HisAntigenUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIGEN>();
        }

        private BridgeDAO<HIS_ANTIGEN> bridgeDAO;

        public bool Update(HIS_ANTIGEN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ANTIGEN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
