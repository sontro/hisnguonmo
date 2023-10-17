using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialType;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicineType;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Create;
using MOS.MANAGER.HisServiceReq;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.InPatient.Create
{
    class SereServProcessor : BusinessBase
    {
        private HisSereServCreateBatchUsingThread creator;

        internal SereServProcessor()
            : base()
        {
            this.creator = new HisSereServCreateBatchUsingThread(param);
        }

        internal SereServProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.creator = new HisSereServCreateBatchUsingThread(param);
        }

        internal bool Run(HIS_TREATMENT treatment, List<HIS_SERE_SERV> existSereServs, List<HIS_EXP_MEST> expMests, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, List<HIS_EXP_MEST_MATERIAL> expMestMaterials, List<V_HIS_MEDICINE_2> choosenMedicines, ref List<HIS_SERE_SERV> nSereServs)
        {
            try
            {
                if (!IsNotNullOrEmpty(expMests))
                {
                    return true;
                }

                List<long> serviceReqIds = expMests
                .Where(o => o.SERVICE_REQ_ID.HasValue)
                .Select(o => o.SERVICE_REQ_ID.Value).ToList();

                List<long> prescriptionIds = IsNotNullOrEmpty(expMests) ? expMests
                    .Where(o => o.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT)
                    .Select(o => o.ID).ToList() : null;

                if (IsNotNullOrEmpty(prescriptionIds))
                {
                    List<HIS_EXP_MEST_MEDICINE> presMedicines = IsNotNullOrEmpty(expMestMedicines) ? expMestMedicines.Where(o => prescriptionIds.Contains(o.EXP_MEST_ID.Value)).ToList() : null;
                    List<HIS_EXP_MEST_MATERIAL> presMaterials = IsNotNullOrEmpty(expMestMaterials) ? expMestMaterials.Where(o => prescriptionIds.Contains(o.EXP_MEST_ID.Value)).ToList() : null;

                    List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().GetByIds(serviceReqIds);

                    List<HIS_SERE_SERV> newSereServs = new List<HIS_SERE_SERV>();

                    this.MakeSereServByExpMestMedicine(presMedicines, treatment, expMests, serviceReqs, choosenMedicines, ref newSereServs);

                    this.MakeSereServByExpMestMaterial(presMaterials, treatment, expMests, serviceReqs, ref newSereServs);

                    this.creator.Run(new List<HIS_TREATMENT>() { treatment }, serviceReqs, existSereServs, newSereServs);

                    nSereServs = newSereServs;
                }
                return true;
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Lay ra cac sere_serv BHYT da co doi voi cac treatment ma co phieu xuat co su dung Doi tuong thanh toan la BHYT
        /// </summary>
        /// <param name="children"></param>
        /// <param name="expMestMedicines"></param>
        /// <param name="expMestMaterials"></param>
        /// <returns></returns>
        private List<HIS_SERE_SERV> GetExistSereServsOfBhytTreatment(long treatmentId, List<HIS_EXP_MEST_MEDICINE> presMedicines, List<HIS_EXP_MEST_MATERIAL> presMaterials)
        {
            List<HIS_SERE_SERV> result = new List<HIS_SERE_SERV>();
            bool check = (presMedicines != null && presMedicines.Exists(o => o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT))
                || (presMaterials != null && presMaterials.Exists(o => o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT));

            if (check)
            {
                HisSereServFilterQuery filter = new HisSereServFilterQuery();
                filter.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT;
                filter.HAS_EXECUTE = true;
                filter.TREATMENT_ID = treatmentId;
                return new HisSereServGet().Get(filter);
            }
            return null;
        }

        /// <summary>
        /// Tao du lieu sere_serv dua vao du lieu exp_mest_medicine
        /// </summary>
        /// <param name="expMestMedicines"></param>
        /// <param name="treatments"></param>
        /// <param name="children"></param>
        /// <param name="serviceReqs"></param>
        /// <param name="sereServs"></param>
        private void MakeSereServByExpMestMedicine(List<HIS_EXP_MEST_MEDICINE> presMedicines, HIS_TREATMENT treatment, List<HIS_EXP_MEST> children, List<HIS_SERVICE_REQ> serviceReqs, List<V_HIS_MEDICINE_2> choosenMedicines, ref List<HIS_SERE_SERV> sereServs)
        {
            if (IsNotNullOrEmpty(presMedicines) && IsNotNullOrEmpty(choosenMedicines))
            {
                //Gia lo thuoc/vat tu da duoc xu ly khi tao thong tin phieu xuat
                //nen ko can truyen vao trong priceAdder
                HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment, null, null);

                foreach (HIS_EXP_MEST_MEDICINE exp in presMedicines)
                {
                    HIS_EXP_MEST child = children.Where(o => o.ID == exp.EXP_MEST_ID).FirstOrDefault();

                    //chi xu ly voi cac exp_mest_medicine thuoc don thuoc (tranh truong hop phieu xuat bu le thi ko tao ra sere_serv)
                    if (child != null && child.SERVICE_REQ_ID.HasValue)
                    {
                        V_HIS_MEDICINE_2 medicine2 = choosenMedicines.Where(o => o.ID == exp.MEDICINE_ID).FirstOrDefault();
                        if (medicine2 == null)
                        {
                            throw new Exception("Ko tim thay medicine tuong ung voi exp.MEDICINE_ID " + exp.MEDICINE_ID);
                        }

                        HIS_MEDICINE_TYPE medicineType = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == exp.TDL_MEDICINE_TYPE_ID);
                        if (medicineType == null)
                        {
                            HisMedicineTypeCFG.Reload();
                            medicineType = HisMedicineTypeCFG.DATA.Where(o => o.ID == exp.TDL_MEDICINE_TYPE_ID).FirstOrDefault();
                        }

                        HIS_SERE_SERV ss = new HIS_SERE_SERV();
                        ss.AMOUNT = exp.AMOUNT;
                        ss.EXECUTE_TIME = exp.EXP_TIME;
                        ss.IS_EXPEND = exp.IS_EXPEND;
                        ss.SERVICE_CONDITION_ID = exp.SERVICE_CONDITION_ID;
                        ss.OTHER_PAY_SOURCE_ID = exp.OTHER_PAY_SOURCE_ID;
                        ss.IS_OUT_PARENT_FEE = exp.IS_OUT_PARENT_FEE;
                        ss.MEDICINE_ID = exp.MEDICINE_ID;
                        ss.ORIGINAL_PRICE = exp.PRICE.Value;
                        ss.PARENT_ID = exp.SERE_SERV_PARENT_ID;
                        ss.PATIENT_TYPE_ID = exp.PATIENT_TYPE_ID.Value;
                        ss.PRICE = exp.PRICE.Value;
                        ss.PRIMARY_PRICE = exp.PRICE.Value;
                        ss.VAT_RATIO = exp.VAT_RATIO ?? 0;
                        ss.SERVICE_ID = medicine2.SERVICE_ID;
                        ss.SERVICE_REQ_ID = child.SERVICE_REQ_ID.Value;
                        ss.EXP_MEST_MEDICINE_ID = exp.ID;
                        ss.EXPEND_TYPE_ID = exp.EXPEND_TYPE_ID;
                        ss.USE_ORIGINAL_UNIT_FOR_PRES = exp.USE_ORIGINAL_UNIT_FOR_PRES;
                        ss.IS_NOT_PRES = exp.IS_NOT_PRES;
                        ss.TDL_IS_VACCINE = medicineType.IS_VACCINE;
                        //can set thua du lieu o day luon do phan xu ly ti le thanh toan
                        //co su dung cac truong thua du lieu
                        HIS_SERVICE_REQ serviceReq = serviceReqs.Where(o => o.ID == child.SERVICE_REQ_ID.Value).FirstOrDefault();

                        //Can set vao o day de phuc vu xu ly tinh toan ti le BHYT o ham AddPriceForNonService
                        ss.HEIN_CARD_NUMBER = ss.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT ? serviceReq.TDL_HEIN_CARD_NUMBER : null;

                        HisSereServUtil.SetTdl(ss, serviceReq, medicine2);
                        priceAdder.AddPriceForNonService(ss, serviceReq.INTRUCTION_TIME, serviceReq.ICD_CODE, serviceReq.ICD_SUB_CODE);
                        sereServs.Add(ss);
                    }
                }
            }
        }

        /// <summary>
        /// Tao du lieu sere_serv dua vao du lieu exp_mest_material
        /// </summary>
        /// <param name="expMestMaterials"></param>
        /// <param name="treatments"></param>
        /// <param name="children"></param>
        /// <param name="serviceReqs"></param>
        /// <param name="sereServs"></param>
        private void MakeSereServByExpMestMaterial(List<HIS_EXP_MEST_MATERIAL> presMaterials, HIS_TREATMENT treatment, List<HIS_EXP_MEST> children, List<HIS_SERVICE_REQ> serviceReqs, ref List<HIS_SERE_SERV> sereServs)
        {
            if (IsNotNullOrEmpty(presMaterials))
            {
                //Gia lo thuoc/vat tu da duoc xu ly khi tao thong tin phieu xuat
                //nen ko can truyen vao trong priceAdder
                HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment, null, null);

                foreach (HIS_EXP_MEST_MATERIAL exp in presMaterials)
                {
                    HIS_EXP_MEST child = children.Where(o => o.ID == exp.EXP_MEST_ID).FirstOrDefault();

                    //chi xu ly voi cac exp_mest_material thuoc don thuoc (tranh truong hop phieu xuat bu le thi ko tao ra sere_serv)
                    if (child != null && child.SERVICE_REQ_ID.HasValue)
                    {
                        HIS_MATERIAL_TYPE materialType = HisMaterialTypeCFG.DATA.Where(o => o.ID == exp.TDL_MATERIAL_TYPE_ID).FirstOrDefault();
                        if (materialType == null)
                        {
                            HisMaterialTypeCFG.Reload();
                            materialType = HisMaterialTypeCFG.DATA.Where(o => o.ID == exp.TDL_MATERIAL_TYPE_ID).FirstOrDefault();
                        }
                        HIS_SERE_SERV ss = new HIS_SERE_SERV();
                        ss.AMOUNT = exp.AMOUNT;
                        ss.EXECUTE_TIME = exp.EXP_TIME;
                        ss.IS_EXPEND = exp.IS_EXPEND;
                        ss.SERVICE_CONDITION_ID = exp.SERVICE_CONDITION_ID;
                        ss.OTHER_PAY_SOURCE_ID = exp.OTHER_PAY_SOURCE_ID;
                        ss.IS_OUT_PARENT_FEE = exp.IS_OUT_PARENT_FEE;
                        ss.MATERIAL_ID = exp.MATERIAL_ID;
                        ss.ORIGINAL_PRICE = exp.PRICE.Value;
                        ss.PARENT_ID = exp.SERE_SERV_PARENT_ID;
                        ss.PATIENT_TYPE_ID = exp.PATIENT_TYPE_ID.Value;
                        ss.PRICE = exp.PRICE.Value;
                        ss.PRIMARY_PRICE = exp.PRICE.Value;
                        ss.VAT_RATIO = (exp.VAT_RATIO ?? 0);
                        ss.STENT_ORDER = exp.STENT_ORDER;
                        ss.SERVICE_REQ_ID = child.SERVICE_REQ_ID.Value;
                        ss.EXP_MEST_MATERIAL_ID = exp.ID;
                        ss.EQUIPMENT_SET_ID = exp.EQUIPMENT_SET_ID;
                        ss.EQUIPMENT_SET_ORDER = exp.EQUIPMENT_SET_ORDER;
                        ss.EXPEND_TYPE_ID = exp.EXPEND_TYPE_ID;
                        ss.IS_NOT_PRES = exp.IS_NOT_PRES;

                        //can set thua du lieu o day luon do phan xu ly ti le thanh toan
                        //co su dung cac truong thua du lieu
                        HIS_SERVICE_REQ serviceReq = serviceReqs.Where(o => o.ID == child.SERVICE_REQ_ID.Value).FirstOrDefault();

                        //Can set vao o day de phuc vu xu ly tinh toan ti le BHYT o ham AddPriceForNonService
                        ss.HEIN_CARD_NUMBER = ss.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT ? serviceReq.TDL_HEIN_CARD_NUMBER : null;

                        HisSereServUtil.SetTdl(ss, serviceReq, materialType);
                        priceAdder.AddPriceForNonService(ss, serviceReq.INTRUCTION_TIME, serviceReq.ICD_CODE, serviceReq.ICD_SUB_CODE);
                        sereServs.Add(ss);
                    }
                }
            }
        }

        internal void Rollback()
        {
            this.creator.Rollback();
        }
    }
}
