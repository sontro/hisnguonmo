using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentPoison
{
    partial class HisAccidentPoisonCreate : EntityBase
    {
        public HisAccidentPoisonCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_POISON>();
        }

        private BridgeDAO<HIS_ACCIDENT_POISON> bridgeDAO;

        public bool Create(HIS_ACCIDENT_POISON data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ACCIDENT_POISON> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
