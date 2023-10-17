using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttCatastrophe
{
    partial class HisPtttCatastropheCreate : EntityBase
    {
        public HisPtttCatastropheCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_CATASTROPHE>();
        }

        private BridgeDAO<HIS_PTTT_CATASTROPHE> bridgeDAO;

        public bool Create(HIS_PTTT_CATASTROPHE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PTTT_CATASTROPHE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
