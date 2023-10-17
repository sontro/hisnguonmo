using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSpeciality
{
    partial class HisSpecialityTruncate : EntityBase
    {
        public HisSpecialityTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SPECIALITY>();
        }

        private BridgeDAO<HIS_SPECIALITY> bridgeDAO;

        public bool Truncate(HIS_SPECIALITY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SPECIALITY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
