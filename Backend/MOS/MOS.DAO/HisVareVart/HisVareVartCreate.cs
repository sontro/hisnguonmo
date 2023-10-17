using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisVareVart
{
    partial class HisVareVartCreate : EntityBase
    {
        public HisVareVartCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VARE_VART>();
        }

        private BridgeDAO<HIS_VARE_VART> bridgeDAO;

        public bool Create(HIS_VARE_VART data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_VARE_VART> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
