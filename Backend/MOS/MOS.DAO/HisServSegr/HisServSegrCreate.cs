using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServSegr
{
    partial class HisServSegrCreate : EntityBase
    {
        public HisServSegrCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERV_SEGR>();
        }

        private BridgeDAO<HIS_SERV_SEGR> bridgeDAO;

        public bool Create(HIS_SERV_SEGR data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERV_SEGR> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
