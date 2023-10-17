using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPrepareMaty;
using MOS.MANAGER.HisPrepareMety;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPrepare.Check
{
    class HisPrepareCheckAmount : BusinessBase
    {
        internal HisPrepareCheckAmount()
            : base()
        {

        }

        internal HisPrepareCheckAmount(CommonParam param)
            : base(param)
        {

        }

        /// <summary>
        /// Check so luong duyet du tru, so luong da ke va so luong muon ke
        /// </summary>
        /// <param name="treatmentId"></param>
        /// <param name="medicineTypeId"></param>
        /// <param name="amount">so luong muon ke</param>
        /// <returns></returns>
        internal bool CheckAmountMedicine(long treatmentId, long medicineTypeId, decimal amount, ref decimal approvalAmount, ref decimal prescriptionAmount)
        {
            bool valid = true;
            try
            {
                string sqlExpMest = new StringBuilder().Append("SELECT EMME.* FROM HIS_EXP_MEST_MEDICINE EMME ")
                    .Append("JOIN HIS_EXP_MEST EXME ON EMME.EXP_MEST_ID = EXME.ID ")
                    .Append("WHERE EXME.TDL_TREATMENT_ID = ")
                    .Append(treatmentId)
                    .Append(" AND EMME.IS_DELETE = 0 ")
                    .Append(" AND (EXME.EXP_MEST_TYPE_ID = ").Append(IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT)
                    .Append(" OR EXME.EXP_MEST_TYPE_ID = ").Append(IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT)
                    .Append(") AND EXME.EXP_MEST_STT_ID <> ").Append(IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REJECT)
                    .Append(" AND EMME.TDL_MEDICINE_TYPE_ID = ").Append(medicineTypeId).ToString();

                HisPrepareMetyFilterQuery preFilter = new HisPrepareMetyFilterQuery();
                preFilter.MEDICINE_TYPE_ID = medicineTypeId;
                preFilter.TDL_TREATMENT_ID = treatmentId;
                preFilter.HAS_APPROVAL_AMOUNT = true;

                List<HIS_EXP_MEST_MEDICINE> expMestMedicines = DAOWorker.SqlDAO.GetSql<HIS_EXP_MEST_MEDICINE>(sqlExpMest);
                List<HIS_PREPARE_METY> preMetys = new HisPrepareMetyGet().Get(preFilter);
                decimal expAmount = expMestMedicines != null ? expMestMedicines.Sum(s => s.AMOUNT) : 0;
                decimal preAmount = preMetys != null ? preMetys.Sum(s => (s.APPROVAL_AMOUNT ?? 0)) : 0;
                if (preAmount - expAmount < amount)
                {
                    approvalAmount = preAmount;
                    prescriptionAmount = expAmount;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        /// <summary>
        /// Check so luong duyet du tru, so luong da ke va so luong muon ke
        /// Phuc vu nghiep vu huy duyet du tru
        /// Khong bao gom phieu du tru muon huy
        /// </summary>
        /// <param name="treatmentId"></param>
        /// <param name="medicineTypeId"></param>
        /// <param name="notInPrepareId">phieu du tru khong tinh</param>
        /// <param name="amount"> so luong muon ke (0)</param>
        /// <returns></returns>
        internal bool CheckAmountMedicineNotInPrepare(long treatmentId, long medicineTypeId, long notInPrepareId, decimal amount)
        {
            bool valid = true;
            try
            {
                string sqlExpMest = new StringBuilder().Append("SELECT EMME.* FROM HIS_EXP_MEST_MEDICINE EMME ")
                    .Append("JOIN HIS_EXP_MEST EXME ON EMME.EXP_MEST_ID = EXME.ID ")
                    .Append("WHERE EXME.TDL_TREATMENT_ID = ")
                    .Append(treatmentId)
                    .Append(" AND EMME.IS_DELETE = 0 ")
                    .Append(" AND (EXME.EXP_MEST_TYPE_ID = ").Append(IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT)
                    .Append(" OR EXME.EXP_MEST_TYPE_ID = ").Append(IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT)
                    .Append(") AND EXME.EXP_MEST_STT_ID <> ").Append(IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REJECT)
                    .Append(" AND EMME.TDL_MEDICINE_TYPE_ID = ").Append(medicineTypeId).ToString();

                HisPrepareMetyFilterQuery preFilter = new HisPrepareMetyFilterQuery();
                preFilter.MEDICINE_TYPE_ID = medicineTypeId;
                preFilter.TDL_TREATMENT_ID = treatmentId;
                preFilter.PREPARE_ID__NOT_EQUAL = notInPrepareId;
                preFilter.HAS_APPROVAL_AMOUNT = true;

                List<HIS_EXP_MEST_MEDICINE> expMestMedicines = DAOWorker.SqlDAO.GetSql<HIS_EXP_MEST_MEDICINE>(sqlExpMest);
                List<HIS_PREPARE_METY> preMetys = new HisPrepareMetyGet().Get(preFilter);
                decimal expAmount = expMestMedicines != null ? expMestMedicines.Sum(s => s.AMOUNT) : 0;
                decimal preAmount = preMetys != null ? preMetys.Sum(s => (s.APPROVAL_AMOUNT ?? 0)) : 0;
                if (preAmount - expAmount < amount)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        /// <summary>
        /// Check so luong duyet du tru, so luong da ke va so luong muon ke
        /// Phuc vu nghiep vu sua don
        /// Khong bao gom don dang duoc sua
        /// </summary>
        /// <param name="treatmentId"></param>
        /// <param name="medicineTypeId"></param>
        /// <param name="notInServiceReqId">don dang sua</param>
        /// <param name="amount">so luong muon sua</param>
        /// <returns></returns>
        internal bool CheckAmountMedicineNotInServiceReq(long treatmentId, long medicineTypeId, long notInServiceReqId, decimal amount, ref decimal approvalAmount, ref decimal prescriptionAmount)
        {
            bool valid = true;
            try
            {
                string sqlExpMest = new StringBuilder().Append("SELECT EMME.* FROM HIS_EXP_MEST_MEDICINE EMME ")
                    .Append("JOIN HIS_EXP_MEST EXME ON EMME.EXP_MEST_ID = EXME.ID ")
                    .Append("WHERE EXME.TDL_TREATMENT_ID = ")
                    .Append(treatmentId)
                    .Append(" AND EMME.IS_DELETE = 0 ")
                    .Append(" AND (EXME.EXP_MEST_TYPE_ID = ").Append(IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT)
                    .Append(" OR EXME.EXP_MEST_TYPE_ID = ").Append(IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT)
                    .Append(") AND EXME.EXP_MEST_STT_ID <> ").Append(IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REJECT)
                    .Append(" AND EMME.TDL_MEDICINE_TYPE_ID = ").Append(medicineTypeId)
                    .Append("AND EXME.SERVICE_REQ_ID <> ").Append(notInServiceReqId).ToString();

                HisPrepareMetyFilterQuery preFilter = new HisPrepareMetyFilterQuery();
                preFilter.MEDICINE_TYPE_ID = medicineTypeId;
                preFilter.TDL_TREATMENT_ID = treatmentId;
                preFilter.HAS_APPROVAL_AMOUNT = true;

                List<HIS_EXP_MEST_MEDICINE> expMestMedicines = DAOWorker.SqlDAO.GetSql<HIS_EXP_MEST_MEDICINE>(sqlExpMest);
                List<HIS_PREPARE_METY> preMetys = new HisPrepareMetyGet().Get(preFilter);
                decimal expAmount = expMestMedicines != null ? expMestMedicines.Sum(s => s.AMOUNT) : 0;
                decimal preAmount = preMetys != null ? preMetys.Sum(s => (s.APPROVAL_AMOUNT ?? 0)) : 0;

                if (preAmount - expAmount < amount)
                {
                    approvalAmount = preAmount;
                    prescriptionAmount = expAmount;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        /// <summary>
        ///  Check so luong duyet du tru, so luong da ke va so luong muon ke
        /// </summary>
        /// <param name="treatmentId"></param>
        /// <param name="materialTypeId"></param>
        /// <param name="amount">so luong muon ke</param>
        /// <returns></returns>
        internal bool CheckAmountMaterial(long treatmentId, long materialTypeId, decimal amount, ref decimal approvalAmount, ref decimal prescriptionAmount)
        {
            bool valid = true;
            try
            {
                string sqlExpMest = new StringBuilder().Append("SELECT EMMA.* FROM HIS_EXP_MEST_MATERIAL EMMA ")
                    .Append("JOIN HIS_EXP_MEST EXME ON EMMA.EXP_MEST_ID = EXME.ID ")
                    .Append("WHERE EXME.TDL_TREATMENT_ID = ")
                    .Append(treatmentId)
                    .Append(" AND EMMA.IS_DELETE = 0 ")
                    .Append(" AND (EXME.EXP_MEST_TYPE_ID = ").Append(IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT)
                    .Append(" OR EXME.EXP_MEST_TYPE_ID = ").Append(IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT)
                    .Append(") AND EXME.EXP_MEST_STT_ID <> ").Append(IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REJECT)
                    .Append(" AND EMMA.TDL_MATERIAL_TYPE_ID = ").Append(materialTypeId).ToString();

                HisPrepareMatyFilterQuery preFilter = new HisPrepareMatyFilterQuery();
                preFilter.MATERIAL_TYPE_ID = materialTypeId;
                preFilter.TDL_TREATMENT_ID = treatmentId;
                preFilter.HAS_APPROVAL_AMOUNT = true;

                List<HIS_EXP_MEST_MATERIAL> expMestMaterials = DAOWorker.SqlDAO.GetSql<HIS_EXP_MEST_MATERIAL>(sqlExpMest);
                List<HIS_PREPARE_MATY> preMatys = new HisPrepareMatyGet().Get(preFilter);
                decimal expAmount = expMestMaterials != null ? expMestMaterials.Sum(s => s.AMOUNT) : 0;
                decimal preAmount = preMatys != null ? preMatys.Sum(s => (s.APPROVAL_AMOUNT ?? 0)) : 0;
                if (preAmount - expAmount < amount)
                {
                    approvalAmount = preAmount;
                    prescriptionAmount = expAmount;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        /// <summary>
        /// Check so luong duyet du tru, so luong da ke va so luong muon ke
        /// Phuc vu nghiep vu huy duyet du tru
        /// Khong bao gom phieu du tru muon huy
        /// </summary>
        /// <param name="treatmentId"></param>
        /// <param name="materialTypeId"></param>
        /// <param name="notInPrepareId">phieu du tru khong tinh</param>
        /// <param name="amount">so luong muon ke (0)</param>
        /// <returns></returns>
        internal bool CheckAmountMaterialNotInPrepare(long treatmentId, long materialTypeId, long notInPrepareId, decimal amount)
        {
            bool valid = true;
            try
            {
                string sqlExpMest = new StringBuilder().Append("SELECT EMMA.* FROM HIS_EXP_MEST_MATERIAL EMMA ")
                    .Append("JOIN HIS_EXP_MEST EXME ON EMMA.EXP_MEST_ID = EXME.ID ")
                    .Append("WHERE EXME.TDL_TREATMENT_ID = ")
                    .Append(treatmentId)
                    .Append(" AND EMMA.IS_DELETE = 0 ")
                    .Append(" AND (EXME.EXP_MEST_TYPE_ID = ").Append(IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT)
                    .Append(" OR EXME.EXP_MEST_TYPE_ID = ").Append(IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT)
                    .Append(") AND EXME.EXP_MEST_STT_ID <> ").Append(IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REJECT)
                    .Append(" AND EMMA.TDL_MATERIAL_TYPE_ID = ").Append(materialTypeId).ToString();

                HisPrepareMatyFilterQuery preFilter = new HisPrepareMatyFilterQuery();
                preFilter.MATERIAL_TYPE_ID = materialTypeId;
                preFilter.TDL_TREATMENT_ID = treatmentId;
                preFilter.PREPARE_ID__NOT_EQUAL = notInPrepareId;
                preFilter.HAS_APPROVAL_AMOUNT = true;

                List<HIS_EXP_MEST_MATERIAL> expMestMaterials = DAOWorker.SqlDAO.GetSql<HIS_EXP_MEST_MATERIAL>(sqlExpMest);
                List<HIS_PREPARE_MATY> preMatys = new HisPrepareMatyGet().Get(preFilter);
                decimal expAmount = expMestMaterials != null ? expMestMaterials.Sum(s => s.AMOUNT) : 0;
                decimal preAmount = preMatys != null ? preMatys.Sum(s => (s.APPROVAL_AMOUNT ?? 0)) : 0;
                if (preAmount - expAmount < amount)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        /// <summary>
        /// Check so luong duyet du tru, so luong da ke va so luong muon ke
        /// Phuc vu nghiep vu sua don
        /// Khong bao gom don dang duoc sua
        /// </summary>
        /// <param name="treatmentId"></param>
        /// <param name="materialTypeId"></param>
        /// <param name="notInServiceReqId">don dang sua</param>
        /// <param name="amount">so luong muon sua</param>
        /// <returns></returns>
        internal bool CheckAmountMaterialNotInServiceReq(long treatmentId, long materialTypeId, long notInServiceReqId, decimal amount, ref decimal approvalAmount, ref decimal prescriptionAmount)
        {
            bool valid = true;
            try
            {
                string sqlExpMest = new StringBuilder().Append("SELECT EMMA.* FROM HIS_EXP_MEST_MATERIAL EMMA ")
                    .Append("JOIN HIS_EXP_MEST EXME ON EMMA.EXP_MEST_ID = EXME.ID ")
                    .Append("WHERE EXME.TDL_TREATMENT_ID = ")
                    .Append(treatmentId)
                    .Append(" AND EMMA.IS_DELETE = 0 ")
                    .Append(" AND (EXME.EXP_MEST_TYPE_ID = ").Append(IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT)
                    .Append(" OR EXME.EXP_MEST_TYPE_ID = ").Append(IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT)
                    .Append(") AND EXME.EXP_MEST_STT_ID <> ").Append(IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REJECT)
                    .Append(" AND EMMA.TDL_MATERIAL_TYPE_ID = ").Append(materialTypeId)
                    .Append("AND EXME.SERVICE_REQ_ID <> ").Append(notInServiceReqId).ToString();

                HisPrepareMatyFilterQuery preFilter = new HisPrepareMatyFilterQuery();
                preFilter.MATERIAL_TYPE_ID = materialTypeId;
                preFilter.TDL_TREATMENT_ID = treatmentId;
                preFilter.HAS_APPROVAL_AMOUNT = true;

                List<HIS_EXP_MEST_MATERIAL> expMestMaterials = DAOWorker.SqlDAO.GetSql<HIS_EXP_MEST_MATERIAL>(sqlExpMest);
                List<HIS_PREPARE_MATY> preMatys = new HisPrepareMatyGet().Get(preFilter);
                decimal expAmount = expMestMaterials != null ? expMestMaterials.Sum(s => s.AMOUNT) : 0;
                decimal preAmount = preMatys != null ? preMatys.Sum(s => (s.APPROVAL_AMOUNT ?? 0)) : 0;

                if (preAmount - expAmount < amount)
                {
                    approvalAmount = preAmount;
                    prescriptionAmount = expAmount;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

    }
}
