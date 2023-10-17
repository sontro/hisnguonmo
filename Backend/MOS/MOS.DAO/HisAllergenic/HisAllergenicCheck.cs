using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisAllergenic
{
    partial class HisAllergenicCheck : EntityBase
    {
        public HisAllergenicCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ALLERGENIC>();
        }

        private BridgeDAO<HIS_ALLERGENIC> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
