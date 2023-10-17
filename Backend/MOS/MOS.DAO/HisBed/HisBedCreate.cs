using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBed
{
    partial class HisBedCreate : EntityBase
    {
        public HisBedCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BED>();
        }

        private BridgeDAO<HIS_BED> bridgeDAO;

        public bool Create(HIS_BED data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BED> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
