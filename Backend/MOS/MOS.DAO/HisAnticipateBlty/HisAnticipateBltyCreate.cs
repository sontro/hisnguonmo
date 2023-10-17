using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisAnticipateBlty
{
    partial class HisAnticipateBltyCreate : EntityBase
    {
        public HisAnticipateBltyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTICIPATE_BLTY>();
        }

        private BridgeDAO<HIS_ANTICIPATE_BLTY> bridgeDAO;

        public bool Create(HIS_ANTICIPATE_BLTY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ANTICIPATE_BLTY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
