using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.Token;
using MOS.SDO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.OutPatient.Create
{
    class HisExpMestProcessor : BusinessBase
    {
        private HisExpMestCreate hisExpMestCreate;

        internal HisExpMestProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
        }

        internal bool Run(OutPatientPresSDO data, List<HIS_SERVICE_REQ> serviceReqs, ref List<HIS_EXP_MEST> resultData)
        {
            try
            {
                //Co ke thuoc/vat tu trong kho hoac ke vat tu tai su dung thi moi tao ra exp_mest
                if (IsNotNullOrEmpty(serviceReqs) && 
                    ((data.Medicines != null && data.Medicines.Exists(t => IsNotNullOrEmpty(t.MedicineBeanIds)))
                        || (data.Materials != null && data.Materials.Exists(t => IsNotNullOrEmpty(t.MaterialBeanIds)))
                        || IsNotNullOrEmpty(data.SerialNumbers)))
                {
                    var tmp = new List<HIS_EXP_MEST>();
                    long createYear = DateTime.Now.Year;

                    long? expMestReasonId = null;
                    if (IsNotNullOrEmpty(data.Medicines)) expMestReasonId = data.Medicines.FirstOrDefault().ExpMestReasonId;
                    else if (IsNotNullOrEmpty(data.Materials)) expMestReasonId = data.Materials.FirstOrDefault().ExpMestReasonId;

                    //Dic de luu so da cap
                    Dictionary<string, long> specialMedicineNumOrder = new Dictionary<string, long>();

                    //Tao thong tin exp_mest tu thong tin service_req
                    //Do don phong kham se truc tiep tao lenh chua khong tao y/c
                    //==> lay luon thong tin duyet phieu xuat theo thong tin tao y/c
                    foreach(HIS_SERVICE_REQ sr in serviceReqs)
                    {
                        //lay thong tin kho xuat dua vao execute_room_id cua service_req
                        V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ROOM_ID == sr.EXECUTE_ROOM_ID).FirstOrDefault();
                        HIS_EXP_MEST expMest = new HIS_EXP_MEST();
                        expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;

                        //neu ke vao tu truc thi loai don la don tu truc, nguoc lai, la don phong kham
                        expMest.EXP_MEST_TYPE_ID = mediStock.IS_CABINET == MOS.UTILITY.Constant.IS_TRUE ? 
                            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT : IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK;
                        expMest.SERVICE_REQ_ID = sr.ID;
                        expMest.MEDI_STOCK_ID = mediStock.ID;
                        expMest.PRES_GROUP = sr.PRES_GROUP;
                        expMest.IS_HOME_PRES = sr.IS_HOME_PRES;
                        expMest.IS_KIDNEY = sr.IS_KIDNEY;
                        expMest.SPECIAL_MEDICINE_TYPE = sr.SPECIAL_MEDICINE_TYPE;
                        expMest.REQ_DEPARTMENT_ID = sr.REQUEST_DEPARTMENT_ID;
                        expMest.ICD_CODE = sr.ICD_CODE;
                        expMest.ICD_NAME = sr.ICD_NAME;
                        expMest.ICD_SUB_CODE = sr.ICD_SUB_CODE;
                        expMest.ICD_TEXT = sr.ICD_TEXT;
                        expMest.EXP_MEST_REASON_ID = expMestReasonId;
                        expMest.REMEDY_COUNT = data.RemedyCount;

                        //Neu la don phong kham va co cau hinh cap STT cho loai thuoc dac biet (gay nghien, huong than, thuoc doc)
                        //thi can xu ly de cap STT
                        if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK
                            && HisExpMestCFG.SPECIAL_MEDICINE_NUM_ORDER_OPTION == HisExpMestCFG.SpecialMedicineNumOrderOption.BY_YEAR__TYPE__REQ_DEPARTMENT__MEDI_STOCK
                            && expMest.SPECIAL_MEDICINE_TYPE.HasValue)
                        {
                            string key = string.Format("{0}-{1}-{2}-{3}", expMest.SPECIAL_MEDICINE_TYPE, expMest.REQ_DEPARTMENT_ID, createYear, expMest.MEDI_STOCK_ID);

                            //Kiem tra xem tuong ung voi loai thuoc dac biet, khoa yeu cau va kho do da co so nao duoc cap chua. 
                            //Neu co thi lay ra va tang len 1, neu chua thi truy van DB de cap
                            //De tranh truong hop trong 1 lan ke don co 2 phieu (exp_mest) co cung loai thuoc, khoa yeu cau, kho xuat (vd: do co key: MOS.HIS_SERVICE_REQ.SPLIT_PRES_BY_GROUP_OPTION)
                            if (specialMedicineNumOrder.ContainsKey(key))
                            {
                                long numOrder = specialMedicineNumOrder[key];
                                expMest.SPECIAL_MEDICINE_NUM_ORDER = numOrder + 1;

                                specialMedicineNumOrder[key] = expMest.SPECIAL_MEDICINE_NUM_ORDER.Value; //update lai STT da cap
                            }
                            else
                            {
                                expMest.SPECIAL_MEDICINE_NUM_ORDER = HisExpMestUtil.GetNextSpeciaMedicineTypeNumOrder(expMest.SPECIAL_MEDICINE_TYPE, expMest.REQ_DEPARTMENT_ID, createYear, expMest.MEDI_STOCK_ID);
                                specialMedicineNumOrder.Add(key, expMest.SPECIAL_MEDICINE_NUM_ORDER.Value); //Luu stt da cap tuong ung
                            }
                        }
                        
                        tmp.Add(expMest);
                    }

                    if (!this.hisExpMestCreate.CreateList(tmp, serviceReqs))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                    resultData = tmp;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        internal void Rollback()
        {
            this.hisExpMestCreate.RollbackData();
        }
    }
}
