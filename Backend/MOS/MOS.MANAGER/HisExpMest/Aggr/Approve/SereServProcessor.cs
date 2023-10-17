using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Aggr.Approve
{
    class SereServProcessor : BusinessBase
    {
        private HisSereServCreateSql hisSereServCreateSql;

        internal SereServProcessor()
            : base()
        {
            this.hisSereServCreateSql = new HisSereServCreateSql(param);
        }

        internal SereServProcessor(CommonParam param)
            : base(param)
        {
            this.hisSereServCreateSql = new HisSereServCreateSql(param);
        }

        internal bool Run(List<HIS_EXP_MEST> children, List<HIS_EXP_MEST_MEDICINE> presMedicines, List<HIS_EXP_MEST_MATERIAL> presMaterials)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(presMaterials) || IsNotNullOrEmpty(presMedicines))
                {
                    List<long> serviceReqIs = new List<long>();
                    if (IsNotNullOrEmpty(presMaterials))
                    {
                        serviceReqIs.AddRange(presMaterials.Select(s => (s.TDL_SERVICE_REQ_ID ?? 0)).ToList());
                    }
                    if (IsNotNullOrEmpty(presMedicines))
                    {
                        serviceReqIs.AddRange(presMedicines.Select(s => (s.TDL_SERVICE_REQ_ID ?? 0)).ToList());
                    }

                    serviceReqIs = serviceReqIs.Distinct().ToList();
                    List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().GetByIds(serviceReqIs);

                    List<HIS_SERE_SERV> sereServs = new List<HIS_SERE_SERV>();

                    this.MakeSereServByExpMestMaterial(presMaterials, children, serviceReqs, ref sereServs);
                    this.MakeSereServByExpMestMedicine(presMedicines, children, serviceReqs, ref sereServs);

                    if (!this.hisSereServCreateSql.Run(sereServs, serviceReqs))
                    {
                        throw new Exception("hisSereServCreateSql. Ket thuc nghiep vuu");
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void MakeSereServByExpMestMedicine(List<HIS_EXP_MEST_MEDICINE> presMedicines, List<HIS_EXP_MEST> children, List<HIS_SERVICE_REQ> serviceReqs, ref List<HIS_SERE_SERV> sereServs)
        {
            if (IsNotNullOrEmpty(presMedicines))
            {
                List<long> medicineIds = presMedicines.Select(o => o.MEDICINE_ID.Value).ToList();
                List<V_HIS_MEDICINE_2> medicines = new HisMedicineGet().GetView2ByIds(medicineIds);

                foreach (HIS_EXP_MEST_MEDICINE exp in presMedicines)
                {
                    HIS_EXP_MEST child = children.Where(o => o.ID == exp.EXP_MEST_ID).FirstOrDefault();

                    //chi xu ly voi cac exp_mest_medicine thuoc don thuoc (tranh truong hop phieu xuat bu le thi ko tao ra sere_serv)
                    if (child != null && child.SERVICE_REQ_ID.HasValue)
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
                        ss.PRIMARY_PRICE = exp.PRICE.Value;
                        ss.VAT_RATIO = exp.VAT_RATIO ?? 0;
                        ss.SERVICE_ID = medicine2.SERVICE_ID;
                        ss.SERVICE_REQ_ID = child.SERVICE_REQ_ID.Value;
                        ss.EXP_MEST_MEDICINE_ID = exp.ID;
                        ss.EXPEND_TYPE_ID = exp.EXPEND_TYPE_ID;
                        ss.USE_ORIGINAL_UNIT_FOR_PRES = exp.USE_ORIGINAL_UNIT_FOR_PRES;
                        ss.IS_NOT_PRES = exp.IS_NOT_PRES;

                        //can set thua du lieu o day luon do phan xu ly ti le thanh toan
                        //co su dung cac truong thua du lieu
                        HIS_SERVICE_REQ serviceReq = serviceReqs.Where(o => o.ID == child.SERVICE_REQ_ID.Value).FirstOrDefault();
                        HisSereServUtil.SetTdl(ss, serviceReq, medicine2);
                        sereServs.Add(ss);
                    }
                }
            }
        }

        // <summary>
        /// Tao du lieu sere_serv dua vao du lieu exp_mest_material
        /// </summary>
        /// <param name="expMestMaterials"></param>
        /// <param name="treatments"></param>
        /// <param name="children"></param>
        /// <param name="serviceReqs"></param>
        /// <param name="sereServs"></param>
        private void MakeSereServByExpMestMaterial(List<HIS_EXP_MEST_MATERIAL> presMaterials, List<HIS_EXP_MEST> children, List<HIS_SERVICE_REQ> serviceReqs, ref List<HIS_SERE_SERV> sereServs)
        {
            if (IsNotNullOrEmpty(presMaterials))
            {
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
                        HisSereServUtil.SetTdl(ss, serviceReq, materialType);
                        sereServs.Add(ss);
                    }
                }
            }
        }

        internal void Rollback()
        {
            try
            {
                this.hisSereServCreateSql.Rollback();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
