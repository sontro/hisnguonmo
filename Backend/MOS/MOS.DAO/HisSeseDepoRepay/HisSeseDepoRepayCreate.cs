using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSeseDepoRepay
{
    partial class HisSeseDepoRepayCreate : EntityBase
    {
        public HisSeseDepoRepayCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SESE_DEPO_REPAY>();
        }

        private BridgeDAO<HIS_SESE_DEPO_REPAY> bridgeDAO;

        public bool Create(HIS_SESE_DEPO_REPAY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SESE_DEPO_REPAY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
