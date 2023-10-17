using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisEinvoiceType
{
    partial class HisEinvoiceTypeCheck : EntityBase
    {
        public HisEinvoiceTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EINVOICE_TYPE>();
        }

        private BridgeDAO<HIS_EINVOICE_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
