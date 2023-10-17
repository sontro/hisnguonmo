using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMaterialPaty
{
    partial class HisMaterialPatyCreate : EntityBase
    {
        public HisMaterialPatyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATERIAL_PATY>();
        }

        private BridgeDAO<HIS_MATERIAL_PATY> bridgeDAO;

        public bool Create(HIS_MATERIAL_PATY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MATERIAL_PATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
