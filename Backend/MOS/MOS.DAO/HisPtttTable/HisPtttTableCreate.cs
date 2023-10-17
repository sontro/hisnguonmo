using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttTable
{
    partial class HisPtttTableCreate : EntityBase
    {
        public HisPtttTableCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_TABLE>();
        }

        private BridgeDAO<HIS_PTTT_TABLE> bridgeDAO;

        public bool Create(HIS_PTTT_TABLE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PTTT_TABLE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
