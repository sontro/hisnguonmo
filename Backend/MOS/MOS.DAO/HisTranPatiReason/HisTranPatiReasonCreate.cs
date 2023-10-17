using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTranPatiReason
{
    partial class HisTranPatiReasonCreate : EntityBase
    {
        public HisTranPatiReasonCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRAN_PATI_REASON>();
        }

        private BridgeDAO<HIS_TRAN_PATI_REASON> bridgeDAO;

        public bool Create(HIS_TRAN_PATI_REASON data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TRAN_PATI_REASON> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
