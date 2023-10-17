using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDiseaseType
{
    partial class HisDiseaseTypeCreate : EntityBase
    {
        public HisDiseaseTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DISEASE_TYPE>();
        }

        private BridgeDAO<HIS_DISEASE_TYPE> bridgeDAO;

        public bool Create(HIS_DISEASE_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DISEASE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
