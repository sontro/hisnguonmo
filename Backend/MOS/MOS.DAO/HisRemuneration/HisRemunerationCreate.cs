using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRemuneration
{
    partial class HisRemunerationCreate : EntityBase
    {
        public HisRemunerationCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REMUNERATION>();
        }

        private BridgeDAO<HIS_REMUNERATION> bridgeDAO;

        public bool Create(HIS_REMUNERATION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_REMUNERATION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
