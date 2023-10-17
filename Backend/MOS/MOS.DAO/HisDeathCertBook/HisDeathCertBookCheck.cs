using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDeathCertBook
{
    partial class HisDeathCertBookCheck : EntityBase
    {
        public HisDeathCertBookCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEATH_CERT_BOOK>();
        }

        private BridgeDAO<HIS_DEATH_CERT_BOOK> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
