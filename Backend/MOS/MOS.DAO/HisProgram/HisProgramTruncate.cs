using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisProgram
{
    partial class HisProgramTruncate : EntityBase
    {
        public HisProgramTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PROGRAM>();
        }

        private BridgeDAO<HIS_PROGRAM> bridgeDAO;

        public bool Truncate(HIS_PROGRAM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PROGRAM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
