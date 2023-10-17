using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCareer
{
    partial class HisCareerTruncate : EntityBase
    {
        public HisCareerTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CAREER>();
        }

        private BridgeDAO<HIS_CAREER> bridgeDAO;

        public bool Truncate(HIS_CAREER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_CAREER> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
