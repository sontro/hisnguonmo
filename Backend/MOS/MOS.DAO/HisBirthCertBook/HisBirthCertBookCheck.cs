using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBirthCertBook
{
    partial class HisBirthCertBookCheck : EntityBase
    {
        public HisBirthCertBookCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BIRTH_CERT_BOOK>();
        }

        private BridgeDAO<HIS_BIRTH_CERT_BOOK> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
