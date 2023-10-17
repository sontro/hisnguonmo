using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Create;
using MOS.MANAGER.HisServiceReq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.InPres.Export
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

        internal bool Run(HIS_TREATMENT treatment, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, List<HIS_EXP_MEST_MATERIAL> expMestMaterials)
        {
            try
            {
                if (!IsNotNull(expMest) || !expMest.SERVICE_REQ_ID.HasValue)
                {
                    return true;
                }
                HIS_SERVICE_REQ serviceReq = new HisServiceReqGet().GetById(expMest.SERVICE_REQ_ID.Value);
                List<HIS_SERE_SERV> existBhytSereServs = this.GetExistSereServsOfBhytTreatment(expMest, expMestMedicines, expMestMaterials);
                List<HIS_SERE_SERV> newSereServs = new List<HIS_SERE_SERV>();
                this.MakeSereServByExpMestMedicine(expMestMedicines, treatment, expMest, serviceReq, ref newSereServs);

                this.MakeSereServByExpMestMaterial(expMestMaterials, treatment, expMest, serviceReq, ref newSereServs);

                this.creator.Run(new List<HIS_TREATMENT>() { treatment }, new List<HIS_SERVICE_REQ>() { serviceReq }, existBhytSereServs, newSereServs);
                return true;
            }
            catch (Exception ex)
            {
                this.Rollback();
                Inventec.Common.Logging.LogSystem.Error(ex);
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
        private List<HIS_SERE_SERV> GetExistSereServsOfBhytTreatment(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> presMedicines, List<HIS_EXP_MEST_MATERIAL> presMaterials)
        {
            List<HIS_SERE_SERV> result = new List<HIS_SERE_SERV>();

            bool isBhyt = false;
            if (IsNotNullOrEmpty(presMedicines) && presMedicines.Exists(o => o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT))
            {
                isBhyt = true;
            }
            if (IsNotNullOrEmpty(presMaterials) && presMaterials.Exists(o => o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT))
            {
                isBhyt = true;
            }

            if (isBhyt)
            {
                HisSereServFilterQuery filter = new HisSereServFilterQuery();
                filter.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT;
                filter.HAS_EXECUTE = true;
                filter.TREATMENT_ID = expMest.TDL_TREATMENT_ID.HasValue ? expMest.TDL_TREATMENT_ID.Value : -1;
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
        private void MakeSereServByExpMestMedicine(List<HIS_EXP_MEST_MEDICINE> presMedicines, HIS_TREATMENT treatment, HIS_EXP_MEST expMest, HIS_SERVICE_REQ serviceReq, ref List<HIS_SERE_SERV> sereServs)
        {
            if (IsNotNullOrEmpty(presMedicines))
            {
                List<long> medicineIds = presMedicines.Select(o => o.MEDICINE_ID).Distinct().ToList();
                List<V_HIS_MEDICINE_2> medicines = new HisMedicineGet().GetView2ByIds(medicineIds);

                foreach (HIS_EXP_MEST_MEDICINE exp in presMedicines)
                {
                    //chi xu ly voi cac exp_mest_medicine thuoc don thuoc (tranh truong hop phieu xuat bu le thi ko tao ra sere_serv)
                    if (expMest != null && expMest.SERVICE_REQ_ID.HasValue)
                    {
                        V_HIS_MEDICINE_2 medicine2 = medicines.Where(o => o.ID == exp.MEDICINE_ID).FirstOrDefault();
                        if (medicine2 == null)
                        {
                            throw new Exception("Ko tim thay medicine tuong ung voi exp.MEDICINE_ID " + exp.MEDICINE_ID);
                        }
                        HIS_SERE_SERV ss = new HIS_SERE_SERV();
                        ss.AMOUNT = exp.AMOUNT;
                        ss.EXECUTE_TIME = exp.EXP_TIME;
                        ss.IS_EXPEND = exp.IS_EXPEND;
                        ss.IS_OUT_PARENT_FEE = exp.IS_OUT_PARENT_FEE;
                        ss.MEDICINE_ID = exp.MEDICINE_ID;
                        ss.ORIGINAL_PRICE = exp.PRICE.Value;
                        ss.PARENT_ID = exp.SERE_SERV_PARENT_ID;
                        ss.PATIENT_TYPE_ID = exp.PATIENT_TYPE_ID.Value;
                        ss.PRICE = exp.PRICE.Value;
                        ss.VAT_RATIO = (exp.VAT_RATIO ?? 0);
                        ss.SERVICE_ID = medicine2.SERVICE_ID;
                        ss.SERVICE_REQ_ID = expMest.SERVICE_REQ_ID.Value;

                        HisSereServUtil.SetTdl(ss, serviceReq, medicine2);
                        new HisSereServSetPrice(param, treatment).AddPriceForNonService(ss, serviceReq.INTRUCTION_TIME);
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
        private void MakeSereServByExpMestMaterial(List<HIS_EXP_MEST_MATERIAL> presMaterials, HIS_TREATMENT treatment, HIS_EXP_MEST expMest, HIS_SERVICE_REQ serviceReq, ref List<HIS_SERE_SERV> sereServs)
        {
            if (IsNotNullOrEmpty(presMaterials))
            {
                List<long> materialIds = presMaterials.Select(o => o.MATERIAL_ID).Distinct().ToList();
                List<V_HIS_MATERIAL_2> materials = new HisMaterialGet().GetView2ByIds(materialIds);

                foreach (HIS_EXP_MEST_MATERIAL exp in presMaterials)
                {
                    //chi xu ly voi cac exp_mest_material thuoc don thuoc (tranh truong hop phieu xuat bu le thi ko tao ra sere_serv)
                    if (expMest != null && expMest.SERVICE_REQ_ID.HasValue)
                    {
                        V_HIS_MATERIAL_2 material2 = materials.Where(o => o.ID == exp.MATERIAL_ID).FirstOrDefault();
                        if (material2 == null)
                        {
                            throw new Exception("Ko tim thay material tuong ung voi exp.MATERIAL_ID " + exp.MATERIAL_ID);
                        }
                        HIS_SERE_SERV ss = new HIS_SERE_SERV();
                        ss.AMOUNT = exp.AMOUNT;
                        ss.EXECUTE_TIME = exp.EXP_TIME;
                        ss.IS_EXPEND = exp.IS_EXPEND;
                        ss.IS_OUT_PARENT_FEE = exp.IS_OUT_PARENT_FEE;
                        ss.MATERIAL_ID = exp.MATERIAL_ID;
                        ss.ORIGINAL_PRICE = exp.PRICE.Value;
                        ss.PARENT_ID = exp.SERE_SERV_PARENT_ID;
                        ss.PATIENT_TYPE_ID = exp.PATIENT_TYPE_ID.Value;
                        ss.PRICE = exp.PRICE.Value;
                        ss.VAT_RATIO = (exp.VAT_RATIO ?? 0);
                        ss.STENT_ORDER = exp.STENT_ORDER;
                        ss.SERVICE_ID = material2.SERVICE_ID;
                        ss.SERVICE_REQ_ID = expMest.SERVICE_REQ_ID.Value;

                        //can set thua du lieu o day luon do phan xu ly ti le thanh toan
                        //co su dung cac truong thua du lieu
                        HisSereServUtil.SetTdl(ss, serviceReq, material2);
                        new HisSereServSetPrice(param, treatment).AddPriceForNonService(ss, serviceReq.INTRUCTION_TIME);
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
