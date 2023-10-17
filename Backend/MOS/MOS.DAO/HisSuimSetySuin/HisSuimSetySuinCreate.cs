using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSuimSetySuin
{
    partial class HisSuimSetySuinCreate : EntityBase
    {
        public HisSuimSetySuinCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SUIM_SETY_SUIN>();
        }

        private BridgeDAO<HIS_SUIM_SETY_SUIN> bridgeDAO;

        public bool Create(HIS_SUIM_SETY_SUIN data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SUIM_SETY_SUIN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
