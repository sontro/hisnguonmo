using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMediRecord
{
    partial class HisMediRecordCreate : EntityBase
    {
        public HisMediRecordCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_RECORD>();
        }

        private BridgeDAO<HIS_MEDI_RECORD> bridgeDAO;

        public bool Create(HIS_MEDI_RECORD data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDI_RECORD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
