using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMediContractMety
{
    public partial class HisMediContractMetyDAO : EntityBase
    {
        public List<V_HIS_MEDI_CONTRACT_METY> GetView(HisMediContractMetySO search, CommonParam param)
        {
            List<V_HIS_MEDI_CONTRACT_METY> result = new List<V_HIS_MEDI_CONTRACT_METY>();
            try
            {
                result = GetWorker.GetView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public V_HIS_MEDI_CONTRACT_METY GetViewById(long id, HisMediContractMetySO search)
        {
            V_HIS_MEDI_CONTRACT_METY result = null;

            try
            {
                result = GetWorker.GetViewById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public List<V_HIS_MEDI_CONTRACT_METY_1> GetView1(HisMediContractMetySO search, CommonParam param)
        {
            List<V_HIS_MEDI_CONTRACT_METY_1> result = new List<V_HIS_MEDI_CONTRACT_METY_1>();
            try
            {
                result = GetWorker.GetView1(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public V_HIS_MEDI_CONTRACT_METY_1 GetView1ById(long id, HisMediContractMetySO search)
        {
            V_HIS_MEDI_CONTRACT_METY_1 result = null;

            try
            {
                result = GetWorker.GetView1ById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }
    }
}
