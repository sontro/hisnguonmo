using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTranPatiForm
{
    partial class HisTranPatiFormCheck : EntityBase
    {
        public HisTranPatiFormCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRAN_PATI_FORM>();
        }

        private BridgeDAO<HIS_TRAN_PATI_FORM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
