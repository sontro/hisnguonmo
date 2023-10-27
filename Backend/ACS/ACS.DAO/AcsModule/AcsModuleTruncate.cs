using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsModule
{
    partial class AcsModuleTruncate : EntityBase
    {
        public AcsModuleTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_MODULE>();
        }

        private BridgeDAO<ACS_MODULE> bridgeDAO;

        public bool Truncate(ACS_MODULE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<ACS_MODULE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
