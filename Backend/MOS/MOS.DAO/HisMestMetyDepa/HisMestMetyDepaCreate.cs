using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMestMetyDepa
{
    partial class HisMestMetyDepaCreate : EntityBase
    {
        public HisMestMetyDepaCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_METY_DEPA>();
        }

        private BridgeDAO<HIS_MEST_METY_DEPA> bridgeDAO;

        public bool Create(HIS_MEST_METY_DEPA data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEST_METY_DEPA> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
