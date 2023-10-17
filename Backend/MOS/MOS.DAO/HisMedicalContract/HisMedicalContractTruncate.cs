using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicalContract
{
    partial class HisMedicalContractTruncate : EntityBase
    {
        public HisMedicalContractTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICAL_CONTRACT>();
        }

        private BridgeDAO<HIS_MEDICAL_CONTRACT> bridgeDAO;

        public bool Truncate(HIS_MEDICAL_CONTRACT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDICAL_CONTRACT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
