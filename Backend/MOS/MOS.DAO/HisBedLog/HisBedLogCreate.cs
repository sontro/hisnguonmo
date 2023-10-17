using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBedLog
{
    partial class HisBedLogCreate : EntityBase
    {
        public HisBedLogCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BED_LOG>();
        }

        private BridgeDAO<HIS_BED_LOG> bridgeDAO;

        public bool Create(HIS_BED_LOG data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BED_LOG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
