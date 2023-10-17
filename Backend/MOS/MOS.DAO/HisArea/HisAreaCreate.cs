using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisArea
{
    partial class HisAreaCreate : EntityBase
    {
        public HisAreaCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_AREA>();
        }

        private BridgeDAO<HIS_AREA> bridgeDAO;

        public bool Create(HIS_AREA data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_AREA> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
