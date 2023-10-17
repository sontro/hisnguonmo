using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisGender
{
    partial class HisGenderTruncate : EntityBase
    {
        public HisGenderTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_GENDER>();
        }

        private BridgeDAO<HIS_GENDER> bridgeDAO;

        public bool Truncate(HIS_GENDER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_GENDER> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
