using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMediRecordType
{
    partial class HisMediRecordTypeCreate : EntityBase
    {
        public HisMediRecordTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_RECORD_TYPE>();
        }

        private BridgeDAO<HIS_MEDI_RECORD_TYPE> bridgeDAO;

        public bool Create(HIS_MEDI_RECORD_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDI_RECORD_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
