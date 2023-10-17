using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAnticipate
{
    partial class HisAnticipateCreate : EntityBase
    {
        public HisAnticipateCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTICIPATE>();
        }

        private BridgeDAO<HIS_ANTICIPATE> bridgeDAO;

        public bool Create(HIS_ANTICIPATE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ANTICIPATE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
