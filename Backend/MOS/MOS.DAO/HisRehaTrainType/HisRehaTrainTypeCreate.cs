using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRehaTrainType
{
    partial class HisRehaTrainTypeCreate : EntityBase
    {
        public HisRehaTrainTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REHA_TRAIN_TYPE>();
        }

        private BridgeDAO<HIS_REHA_TRAIN_TYPE> bridgeDAO;

        public bool Create(HIS_REHA_TRAIN_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_REHA_TRAIN_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
