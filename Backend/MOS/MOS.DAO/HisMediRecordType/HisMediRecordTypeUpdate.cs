using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediRecordType
{
    partial class HisMediRecordTypeUpdate : EntityBase
    {
        public HisMediRecordTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_RECORD_TYPE>();
        }

        private BridgeDAO<HIS_MEDI_RECORD_TYPE> bridgeDAO;

        public bool Update(HIS_MEDI_RECORD_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDI_RECORD_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
