using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRehaTrainUnit
{
    partial class HisRehaTrainUnitCreate : EntityBase
    {
        public HisRehaTrainUnitCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REHA_TRAIN_UNIT>();
        }

        private BridgeDAO<HIS_REHA_TRAIN_UNIT> bridgeDAO;

        public bool Create(HIS_REHA_TRAIN_UNIT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_REHA_TRAIN_UNIT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
