using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServReha
{
    partial class HisSereServRehaCreate : EntityBase
    {
        public HisSereServRehaCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_REHA>();
        }

        private BridgeDAO<HIS_SERE_SERV_REHA> bridgeDAO;

        public bool Create(HIS_SERE_SERV_REHA data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERE_SERV_REHA> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
