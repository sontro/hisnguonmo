using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSurgRemuDetail
{
    partial class HisSurgRemuDetailCreate : EntityBase
    {
        public HisSurgRemuDetailCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SURG_REMU_DETAIL>();
        }

        private BridgeDAO<HIS_SURG_REMU_DETAIL> bridgeDAO;

        public bool Create(HIS_SURG_REMU_DETAIL data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SURG_REMU_DETAIL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
