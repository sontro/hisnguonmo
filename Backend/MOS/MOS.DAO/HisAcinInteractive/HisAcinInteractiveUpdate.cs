using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAcinInteractive
{
    partial class HisAcinInteractiveUpdate : EntityBase
    {
        public HisAcinInteractiveUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACIN_INTERACTIVE>();
        }

        private BridgeDAO<HIS_ACIN_INTERACTIVE> bridgeDAO;

        public bool Update(HIS_ACIN_INTERACTIVE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ACIN_INTERACTIVE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
