using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRehaTrain
{
    partial class HisRehaTrainCreate : EntityBase
    {
        public HisRehaTrainCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REHA_TRAIN>();
        }

        private BridgeDAO<HIS_REHA_TRAIN> bridgeDAO;

        public bool Create(HIS_REHA_TRAIN data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_REHA_TRAIN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
