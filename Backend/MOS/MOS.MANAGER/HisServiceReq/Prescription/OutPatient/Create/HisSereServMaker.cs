using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisSereServ.Update.Package;
using MOS.SDO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.OutPatient.Create
{
    class HisSereServMaker : BusinessBase
    {
        private List<HIS_EXP_MEST_MEDICINE> expMestMedicines;
        private List<HIS_EXP_MEST_MATERIAL> expMestMaterials;
        private List<HIS_EXP_MEST> expMests;
        private List<HIS_SERVICE_REQ> serviceReqs;
        private List<V_HIS_MEDICINE_2> choosenMedicines;
        private OutPatientPresSDO prescription;
        private HIS_TREATMENT treatment;

        internal HisSereServMaker(CommonParam paramCreate, HIS_TREATMENT t, OutPatientPresSDO pres, List<HIS_SERVICE_REQ> serviceReqs, List<HIS_EXP_MEST> exps, List<HIS_EXP_MEST_MEDICINE> medicines, List<HIS_EXP_MEST_MATERIAL> materials, List<V_HIS_MEDICINE_2> choosenMedicines)
            : base(paramCreate)
        {
            this.expMests = exps;
            this.prescription = pres;
            this.expMestMaterials = materials;
            this.expMestMedicines = medicines;
            this.treatment = t;
            this.serviceReqs = serviceReqs;
            this.choosenMedicines = choosenMedicines;
        }

        internal bool Run(ref List<HIS_SERE_SERV> resultData)
        {
            try
            {
                if ((IsNotNullOrEmpty(this.expMestMedicines) || IsNotNullOrEmpty(this.expMestMaterials)) && IsNotNullOrEmpty(this.expMests))
                {
                    //Gia lo thuoc/vat tu da duoc xu ly khi tao thong tin phieu xuat nen ko can truyen vao
                    //trong priceAdder
                    HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment, null, null);

                    resultData = new List<HIS_SERE_SERV>();
                    this.MakeDataByMaterial(priceAdder, ref resultData);
                    this.MakeDataByMedicine(priceAdder, ref resultData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        private void MakeDataByMedicine(HisSereServSetPrice priceAdder, ref List<HIS_SERE_SERV> sereServs)
        {
            if (IsNotNullOrEmpty(this.expMestMedicines))
            {
                List<HIS_SERE_SERV> medicineSereServs = new List<HIS_SERE_SERV>();

                foreach (HIS_EXP_MEST_MEDICINE m in this.expMestMedicines)
                {
                    V_HIS_MEDICINE_2 medicine2 = choosenMedicines.Where(o => o.ID == m.MEDICINE_ID).FirstOrDefault();

                    HIS_EXP_MEST expMest = this.expMests.Where(o => o.ID == m.EXP_MEST_ID).FirstOrDefault();
                    HIS_SERVICE_REQ serviceReq = this.serviceReqs.Where(o => o.ID == expMest.SERVICE_REQ_ID).FirstOrDefault();
                    HIS_MEDICINE_TYPE medicineType = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == m.TDL_MEDICINE_TYPE_ID);
                    if (medicineType == null)
                    {
                        HisMedicineTypeCFG.Reload();
                        medicineType = HisMedicineTypeCFG.DATA.Where(o => o.ID == m.TDL_MEDICINE_TYPE_ID).FirstOrDefault();
                    }

                    HIS_SERE_SERV sereServ = new HIS_SERE_SERV();
                    sereServ.SERVICE_REQ_ID = expMest.SERVICE_REQ_ID;
                    sereServ.AMOUNT = m.AMOUNT;
                    sereServ.PATIENT_TYPE_ID = m.PATIENT_TYPE_ID.Value;
                    sereServ.IS_EXPEND = m.IS_EXPEND;
                    sereServ.SERVICE_CONDITION_ID = m.SERVICE_CONDITION_ID;
                    sereServ.OTHER_PAY_SOURCE_ID = m.OTHER_PAY_SOURCE_ID;
                    sereServ.PRICE = m.PRICE.HasValue ? m.PRICE.Value : 0;
                    sereServ.ORIGINAL_PRICE = sereServ.PRICE;
                    sereServ.PRIMARY_PRICE = sereServ.PRICE;
                    sereServ.VAT_RATIO = m.VAT_RATIO.HasValue ? m.VAT_RATIO.Value : 0;
                    sereServ.MEDICINE_ID = m.MEDICINE_ID;
                    sereServ.PARENT_ID = m.SERE_SERV_PARENT_ID;
                    sereServ.IS_OUT_PARENT_FEE = m.IS_OUT_PARENT_FEE;
                    sereServ.SERVICE_ID = medicine2.SERVICE_ID;
                    sereServ.EXP_MEST_MEDICINE_ID = m.ID;
                    sereServ.EXPEND_TYPE_ID = m.EXPEND_TYPE_ID;
                    sereServ.USE_ORIGINAL_UNIT_FOR_PRES = m.USE_ORIGINAL_UNIT_FOR_PRES;
                    sereServ.TDL_IS_VACCINE = medicineType.IS_VACCINE;
                    //Can set vao o day de phuc vu xu ly tinh toan ti le BHYT o ham AddPriceForNonService
                    sereServ.HEIN_CARD_NUMBER = sereServ.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT ? serviceReq.TDL_HEIN_CARD_NUMBER : null;

                    //can set thua du lieu o day luon do phan xu ly ti le thanh toan
                    //co su dung cac truong thua du lieu
                    HisSereServUtil.SetTdl(sereServ, serviceReq, medicine2);
                    priceAdder.AddPriceForNonService(sereServ, this.prescription.InstructionTime, serviceReq.ICD_CODE, serviceReq.ICD_SUB_CODE);
                    medicineSereServs.Add(sereServ);
                }

                if (!IsNotNullOrEmpty(medicineSereServs))
                {
                    throw new Exception("Loi du lieu. Ko tao duoc sere_serv tuong ung voi exp_mest_medicine");
                }
                else
                {
                    sereServs.AddRange(medicineSereServs);
                }
            }
        }

        private void MakeDataByMaterial(HisSereServSetPrice priceAdder, ref List<HIS_SERE_SERV> sereServs)
        {
            if (IsNotNullOrEmpty(this.expMestMaterials))
            {
                List<HIS_SERE_SERV> materialSereServs = new List<HIS_SERE_SERV>();

                foreach (HIS_EXP_MEST_MATERIAL m in this.expMestMaterials)
                {
                    PresMaterialSDO presMaterial = this.prescription.Materials
                    .Where(o => o.MaterialTypeId == m.TDL_MATERIAL_TYPE_ID)
                    .FirstOrDefault();

                    HIS_EXP_MEST expMest = this.expMests.Where(o => o.ID == m.EXP_MEST_ID).FirstOrDefault();
                    HIS_SERVICE_REQ serviceReq = this.serviceReqs.Where(o => o.ID == expMest.SERVICE_REQ_ID).FirstOrDefault();

                    HIS_MATERIAL_TYPE materialType = HisMaterialTypeCFG.DATA.Where(o => o.ID == presMaterial.MaterialTypeId).FirstOrDefault();
                    
                    if (materialType == null)  //co the do chua thuc hien reload cau hinh MOS
                    {
                        HisMaterialTypeCFG.Reload();
                        materialType = HisMaterialTypeCFG.DATA.Where(o => o.ID == presMaterial.MaterialTypeId).FirstOrDefault();
                    }

                    HIS_SERE_SERV sereServ = new HIS_SERE_SERV();
                    sereServ.SERVICE_REQ_ID = expMest.SERVICE_REQ_ID;
                    sereServ.AMOUNT = m.AMOUNT;
                    sereServ.PATIENT_TYPE_ID = m.PATIENT_TYPE_ID.Value;
                    sereServ.PRICE = m.PRICE.HasValue ? m.PRICE.Value : 0;
                    sereServ.ORIGINAL_PRICE = sereServ.PRICE;
                    sereServ.PRIMARY_PRICE = sereServ.PRICE;
                    sereServ.VAT_RATIO = m.VAT_RATIO.HasValue ? m.VAT_RATIO.Value : 0;
                    sereServ.MATERIAL_ID = m.MATERIAL_ID;
                    sereServ.PARENT_ID = m.SERE_SERV_PARENT_ID;
                    sereServ.IS_EXPEND = m.IS_EXPEND;
                    sereServ.SERVICE_CONDITION_ID = m.SERVICE_CONDITION_ID;
                    sereServ.OTHER_PAY_SOURCE_ID = m.OTHER_PAY_SOURCE_ID;
                    sereServ.IS_OUT_PARENT_FEE = m.IS_OUT_PARENT_FEE;
                    sereServ.STENT_ORDER = m.STENT_ORDER;
                    sereServ.EXP_MEST_MATERIAL_ID = m.ID;
                    sereServ.EXPEND_TYPE_ID = m.EXPEND_TYPE_ID;
                    sereServ.EQUIPMENT_SET_ID = m.EQUIPMENT_SET_ID;
                    sereServ.EQUIPMENT_SET_ORDER = m.EQUIPMENT_SET_ORDER;
                    //Can set vao o day de phuc vu xu ly tinh toan ti le BHYT o ham AddPriceForNonService
                    sereServ.HEIN_CARD_NUMBER = sereServ.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT ? serviceReq.TDL_HEIN_CARD_NUMBER : null;

                    //can set thua du lieu o day luon do phan xu ly ti le thanh toan co su dung cac truong thua du lieu
                    HisSereServUtil.SetTdl(sereServ, serviceReq, materialType);
                    priceAdder.AddPriceForNonService(sereServ, this.prescription.InstructionTime, serviceReq.ICD_CODE, serviceReq.ICD_SUB_CODE);
                    materialSereServs.Add(sereServ);
                }

                if (!IsNotNullOrEmpty(materialSereServs))
                {
                    throw new Exception("Loi du lieu. Ko tao duoc sere_serv tuong ung voi exp_mest_material");
                }
                else
                {
                    sereServs.AddRange(materialSereServs);
                }
            }
        }
    }
}
