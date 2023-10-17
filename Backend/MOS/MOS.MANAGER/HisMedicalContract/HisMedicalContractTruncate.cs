using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMediContractMaty;
using MOS.MANAGER.HisMediContractMety;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicalContract
{
    partial class HisMedicalContractTruncate : BusinessBase
    {
        internal HisMedicalContractTruncate()
            : base()
        {

        }

        internal HisMedicalContractTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicalContractCheck checker = new HisMedicalContractCheck(param);
                HIS_MEDICAL_CONTRACT raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    List<HIS_MEDI_CONTRACT_MATY> matys = new HisMediContractMatyGet().GetByMedicalContractId(id);
                    List<HIS_MEDI_CONTRACT_METY> metys = new HisMediContractMetyGet().GetByMedicalContractId(id);
                    valid = valid && new HisMediContractMatyCheck(param).IsUnLock(matys);
                    valid = valid && new HisMediContractMetyCheck(param).IsUnLock(metys);
                    if (valid)
                    {
                        List<string> sqls = new List<string>();
                        if (raw.BID_ID.HasValue)
                        {
                            if (IsNotNullOrEmpty(matys))
                            {
                                foreach (HIS_MEDI_CONTRACT_MATY item in matys)
                                {
                                    if (item.BID_MATERIAL_TYPE_ID.HasValue)
                                        sqls.Add(String.Format("UPDATE HIS_BID_MATERIAL_TYPE SET TDL_CONTRACT_AMOUNT = NVL(TDL_CONTRACT_AMOUNT, 0) - {0} WHERE ID = {1}", item.AMOUNT, item.BID_MATERIAL_TYPE_ID.Value));
                                }
                            }

                            if (IsNotNullOrEmpty(metys))
                            {
                                foreach (HIS_MEDI_CONTRACT_METY item in metys)
                                {
                                    if (item.BID_MEDICINE_TYPE_ID.HasValue)
                                        sqls.Add(String.Format("UPDATE HIS_BID_MEDICINE_TYPE SET TDL_CONTRACT_AMOUNT = NVL(TDL_CONTRACT_AMOUNT, 0) - {0} WHERE ID = {1}", item.AMOUNT, item.BID_MEDICINE_TYPE_ID.Value));
                                }
                            }
                        }

                        sqls.Add(String.Format("DELETE HIS_MEDI_CONTRACT_MATY WHERE MEDICAL_CONTRACT_ID = {0}", raw.ID));
                        sqls.Add(String.Format("DELETE HIS_MEDI_CONTRACT_METY WHERE MEDICAL_CONTRACT_ID = {0}", raw.ID));
                        sqls.Add(String.Format("DELETE HIS_MEDICAL_CONTRACT WHERE ID = {0}", raw.ID));

                        if (!DAOWorker.SqlDAO.Execute(sqls))
                        {
                            throw new Exception("Xoa hop dong duoc that bai. Sqls: " + sqls.ToString());
                        }

                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool TruncateList(List<HIS_MEDICAL_CONTRACT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicalContractCheck checker = new HisMedicalContractCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisMedicalContractDAO.TruncateList(listData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
