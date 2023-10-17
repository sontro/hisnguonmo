using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPatySub
{
    partial class HisMestPatySubCreate : EntityBase
    {
        public HisMestPatySubCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PATY_SUB>();
        }

        private BridgeDAO<HIS_MEST_PATY_SUB> bridgeDAO;

        public bool Create(HIS_MEST_PATY_SUB data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEST_PATY_SUB> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
