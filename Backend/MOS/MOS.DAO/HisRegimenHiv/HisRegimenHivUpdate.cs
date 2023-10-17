using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRegimenHiv
{
    partial class HisRegimenHivUpdate : EntityBase
    {
        public HisRegimenHivUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REGIMEN_HIV>();
        }

        private BridgeDAO<HIS_REGIMEN_HIV> bridgeDAO;

        public bool Update(HIS_REGIMEN_HIV data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_REGIMEN_HIV> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
