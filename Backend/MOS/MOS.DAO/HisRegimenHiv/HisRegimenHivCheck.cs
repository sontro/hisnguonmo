using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRegimenHiv
{
    partial class HisRegimenHivCheck : EntityBase
    {
        public HisRegimenHivCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REGIMEN_HIV>();
        }

        private BridgeDAO<HIS_REGIMEN_HIV> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
