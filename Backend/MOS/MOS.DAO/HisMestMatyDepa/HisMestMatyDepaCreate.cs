using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMestMatyDepa
{
    partial class HisMestMatyDepaCreate : EntityBase
    {
        public HisMestMatyDepaCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_MATY_DEPA>();
        }

        private BridgeDAO<HIS_MEST_MATY_DEPA> bridgeDAO;

        public bool Create(HIS_MEST_MATY_DEPA data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEST_MATY_DEPA> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
