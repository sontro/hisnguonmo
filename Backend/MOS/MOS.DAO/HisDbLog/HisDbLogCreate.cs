using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDbLog
{
    partial class HisDbLogCreate : EntityBase
    {
        public HisDbLogCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DB_LOG>();
        }

        private BridgeDAO<HIS_DB_LOG> bridgeDAO;

        public bool Create(HIS_DB_LOG data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DB_LOG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
