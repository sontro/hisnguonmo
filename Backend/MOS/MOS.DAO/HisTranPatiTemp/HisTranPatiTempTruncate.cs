using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTranPatiTemp
{
    partial class HisTranPatiTempTruncate : EntityBase
    {
        public HisTranPatiTempTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRAN_PATI_TEMP>();
        }

        private BridgeDAO<HIS_TRAN_PATI_TEMP> bridgeDAO;

        public bool Truncate(HIS_TRAN_PATI_TEMP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TRAN_PATI_TEMP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
