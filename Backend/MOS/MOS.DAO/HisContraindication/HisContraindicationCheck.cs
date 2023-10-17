using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisContraindication
{
    partial class HisContraindicationCheck : EntityBase
    {
        public HisContraindicationCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CONTRAINDICATION>();
        }

        private BridgeDAO<HIS_CONTRAINDICATION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
