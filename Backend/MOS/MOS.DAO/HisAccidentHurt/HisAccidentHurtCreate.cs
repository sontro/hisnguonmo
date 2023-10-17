using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentHurt
{
    partial class HisAccidentHurtCreate : EntityBase
    {
        public HisAccidentHurtCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_HURT>();
        }

        private BridgeDAO<HIS_ACCIDENT_HURT> bridgeDAO;

        public bool Create(HIS_ACCIDENT_HURT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ACCIDENT_HURT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
