using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServPttt
{
    partial class HisSereServPtttCreate : EntityBase
    {
        public HisSereServPtttCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_PTTT>();
        }

        private BridgeDAO<HIS_SERE_SERV_PTTT> bridgeDAO;

        public bool Create(HIS_SERE_SERV_PTTT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERE_SERV_PTTT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
