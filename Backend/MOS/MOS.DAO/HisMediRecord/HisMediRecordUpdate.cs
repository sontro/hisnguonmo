using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediRecord
{
    partial class HisMediRecordUpdate : EntityBase
    {
        public HisMediRecordUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_RECORD>();
        }

        private BridgeDAO<HIS_MEDI_RECORD> bridgeDAO;

        public bool Update(HIS_MEDI_RECORD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDI_RECORD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
