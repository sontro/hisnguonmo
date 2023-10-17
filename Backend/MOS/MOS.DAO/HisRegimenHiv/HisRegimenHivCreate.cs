using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRegimenHiv
{
    partial class HisRegimenHivCreate : EntityBase
    {
        public HisRegimenHivCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REGIMEN_HIV>();
        }

        private BridgeDAO<HIS_REGIMEN_HIV> bridgeDAO;

        public bool Create(HIS_REGIMEN_HIV data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_REGIMEN_HIV> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
