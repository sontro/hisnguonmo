using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSuimIndex
{
    partial class HisSuimIndexCreate : EntityBase
    {
        public HisSuimIndexCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SUIM_INDEX>();
        }

        private BridgeDAO<HIS_SUIM_INDEX> bridgeDAO;

        public bool Create(HIS_SUIM_INDEX data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SUIM_INDEX> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
