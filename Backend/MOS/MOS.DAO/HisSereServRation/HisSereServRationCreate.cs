using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServRation
{
    partial class HisSereServRationCreate : EntityBase
    {
        public HisSereServRationCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_RATION>();
        }

        private BridgeDAO<HIS_SERE_SERV_RATION> bridgeDAO;

        public bool Create(HIS_SERE_SERV_RATION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERE_SERV_RATION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
