using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTranPatiTech
{
    partial class HisTranPatiTechTruncate : EntityBase
    {
        public HisTranPatiTechTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRAN_PATI_TECH>();
        }

        private BridgeDAO<HIS_TRAN_PATI_TECH> bridgeDAO;

        public bool Truncate(HIS_TRAN_PATI_TECH data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TRAN_PATI_TECH> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
