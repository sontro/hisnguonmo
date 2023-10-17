using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTranPatiForm
{
    partial class HisTranPatiFormTruncate : EntityBase
    {
        public HisTranPatiFormTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRAN_PATI_FORM>();
        }

        private BridgeDAO<HIS_TRAN_PATI_FORM> bridgeDAO;

        public bool Truncate(HIS_TRAN_PATI_FORM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TRAN_PATI_FORM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
