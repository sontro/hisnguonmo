using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAcinInteractive
{
    partial class HisAcinInteractiveCreate : EntityBase
    {
        public HisAcinInteractiveCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACIN_INTERACTIVE>();
        }

        private BridgeDAO<HIS_ACIN_INTERACTIVE> bridgeDAO;

        public bool Create(HIS_ACIN_INTERACTIVE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ACIN_INTERACTIVE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
