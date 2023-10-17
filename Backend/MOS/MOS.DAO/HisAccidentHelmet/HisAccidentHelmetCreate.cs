using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentHelmet
{
    partial class HisAccidentHelmetCreate : EntityBase
    {
        public HisAccidentHelmetCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_HELMET>();
        }

        private BridgeDAO<HIS_ACCIDENT_HELMET> bridgeDAO;

        public bool Create(HIS_ACCIDENT_HELMET data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ACCIDENT_HELMET> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
