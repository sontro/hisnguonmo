using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTextLib
{
    partial class HisTextLibCreate : EntityBase
    {
        public HisTextLibCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEXT_LIB>();
        }

        private BridgeDAO<HIS_TEXT_LIB> bridgeDAO;

        public bool Create(HIS_TEXT_LIB data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TEXT_LIB> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
