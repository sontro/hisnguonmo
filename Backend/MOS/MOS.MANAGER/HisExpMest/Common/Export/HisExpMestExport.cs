using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt.HeinTreatmentType;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisTreatment;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisMediStock;
using MOS.SDO;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.Token;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.UTILITY;
using MOS.MANAGER.HisServiceReq.Update.Finish;
using MOS.MANAGER.HisEmployee;

namespace MOS.MANAGER.HisExpMest.Common.Export
{
    /// <summary>
    /// Thuc hien nghiep vu thuc xuat phieu. Khong bao gom phieu linh noi tru
    /// </summary>
    partial class HisExpMestExport : BusinessBase
    {
        private HisServiceReqUpdateFinish hisServiceReqUpdateFinish;
        private BloodProcessor bloodProcessor;
        private MedicineProcessor medicineProcessor;
        private MaterialProcessor materialProcessor;
        private ImportAutoProcessor importAutoProcessor;
        private ExpBltyServiceProcessor expBltyServiceProcessor;
        private ImportAutoMaterialProcessor importAutoMaterialProcessor;

        private HIS_EXP_MEST beforeUpdateHisExpMest;
        private HIS_EXP_MEST recentHisExpMest;

        internal HisExpMestExport()
            : base()
        {
            this.Init();
        }

        internal HisExpMestExport(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqUpdateFinish = new HisServiceReqUpdateFinish(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.bloodProcessor = new BloodProcessor(param);
            this.importAutoProcessor = new ImportAutoProcessor(param);
            this.expBltyServiceProcessor = new ExpBltyServiceProcessor(param);
            this.importAutoMaterialProcessor = new ImportAutoMaterialProcessor(param);
        }

        /// <summary>
        /// Thuc hien thuc nhap
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Export(HisExpMestExportSDO sdo, bool isAuto, ref HIS_EXP_MEST resultData)
        {
            return this.Export(sdo, isAuto, null, null, ref resultData);
        }

        internal bool Export(HisExpMestExportSDO sdo, bool isAuto, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, List<HIS_EXP_MEST_MATERIAL> expMestMaterials, ref HIS_EXP_MEST resultData)
        {
            bool result = false;
            try
            {
                HIS_EXP_MEST expMest = null;

                List<HIS_EXP_MEST_MEDICINE> medicines = expMestMedicines;
                List<HIS_EXP_MEST_MATERIAL> materials = expMestMaterials;
                List<HIS_EXP_MEST_BLOOD> bloods = null;
                HIS_TREATMENT treatment = null;
                HisExpMestExportCheck checker = new HisExpMestExportCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                bool valid = true;

                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                valid = valid && commonChecker.VerifyRequireField(sdo);
                valid = valid && checker.IsAllowed(sdo, isAuto, ref expMest);
                valid = valid && checker.IsValidAntibioticRequest(new List<HIS_EXP_MEST>() { expMest });
                valid = valid && commonChecker.HasNotInExpMestAggr(expMest);
                valid = valid && commonChecker.IsUnNotTaken(expMest);
                valid = valid && commonChecker.IsUnNoExecute(expMest);
                valid = valid && (!expMest.TDL_TREATMENT_ID.HasValue || treatmentChecker.VerifyId(expMest.TDL_TREATMENT_ID.Value, ref treatment));
                valid = valid && (treatment == null || checker.IsUnlockTreatment(expMest, treatment));
                valid = valid && checker.HasUnexport(expMest, ref medicines, ref materials, ref bloods);
                valid = valid && checker.CheckUnpaidOutPatientPrescription(treatment, expMest, medicines, materials);
                valid = valid && checker.CheckBillForSale(expMest);

                if (isAuto == false)
                {
                    valid = valid && checker.CheckValidData(expMest, medicines, materials);
                }

                if (valid)
                {
                    List<string> sqls = new List<string>();
                    string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    string userName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    long expTime = Inventec.Common.DateTime.Get.Now() ?? 0;

                    this.ProcessExpMest(sdo, expMest, medicines, materials, loginName, userName, expTime);

                    this.ProcessServiceReq(expMest);


                    if (!this.medicineProcessor.Run(medicines, expMest.MEDI_STOCK_ID, loginName, userName, expTime, ref sqls))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }

                    if (!this.materialProcessor.Run(materials, expMest.MEDI_STOCK_ID, loginName, userName, expTime, ref sqls))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }

                    if (!this.expBltyServiceProcessor.Run(bloods, expMest))
                    {
                        throw new Exception("expBltyServiceProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.bloodProcessor.Run(bloods, expMest, treatment, loginName, userName, expTime))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }

                    //Xu ly sql de duoi cung de tranh rollback du lieu
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    resultData = this.recentHisExpMest;
                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisExpMest_ThucXuatPhieuXuat).ExpMestCode(this.recentHisExpMest.EXP_MEST_CODE).Run();
                    
                    //Thực hiện tự động nhập tái sử dụng
                    this.importAutoMaterialProcessor.Run(materials, expMest, sdo.ReqRoomId);

                    //Thu hien tu dong tao phieu nhap chuyen kho
                    this.importAutoProcessor.Run(this.recentHisExpMest, sdo.ReqRoomId);

                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                this.Rollback();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessServiceReq(HIS_EXP_MEST raw)
        {
            if (raw != null)
            {
                HIS_SERVICE_REQ serviceReq = null;

                if (raw.SERVICE_REQ_ID.HasValue)
                {
                    serviceReq = new HisServiceReqGet().GetById(raw.SERVICE_REQ_ID.Value);
                }
                //Neu phieu xuat ban va co cau hinh tu dong tao phieu xuat ban hoac co cau hinh luon cap nhat trang thai 
                //don thuoc ngoai kho--> khi duyet phieu xuat ban, tu dong cap nhat trang thai y lenh ke don ngoai kho
                else if (raw.PRESCRIPTION_ID.HasValue && (HisServiceReqCFG.IS_AUTO_CREATE_SALE_EXP_MEST || HisServiceReqCFG.UPDATE_STATUS_ALONG_WITH_SALE_EXP_MEST))
                {
                    serviceReq = new HisServiceReqGet().GetById(raw.PRESCRIPTION_ID.Value);
                }

                if (serviceReq != null)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    HIS_SERVICE_REQ beforeServiceReq = Mapper.Map<HIS_SERVICE_REQ>(serviceReq); //phuc vu rollback

                    serviceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT;
                    serviceReq.FINISH_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(DateTime.Now);
                    serviceReq.EXECUTE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    serviceReq.EXECUTE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    serviceReq.EXECUTE_USER_TITLE = HisEmployeeUtil.GetTitle(serviceReq.EXECUTE_LOGINNAME);

                    if (!new HisServiceReqUpdate(param).Update(serviceReq, beforeServiceReq, false))
                    {
                        throw new Exception("Cap nhat trang thai service_Req sang hoan thanh that bai");
                    }
                }
            }
        }

        private void ProcessExpMest(HisExpMestExportSDO sdo, HIS_EXP_MEST raw, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, List<HIS_EXP_MEST_MATERIAL> expMestMaterials, string loginname, string username, long expTime)
        {
            if (raw != null)
            {

                Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                HIS_EXP_MEST before = Mapper.Map<HIS_EXP_MEST>(raw);
                //Trong truong hop can thay doi trang thai (chuyen sang trang thai hoan thanh)
                //thi cap nhat thong tin exp_mest                
                if (this.IsDone(sdo, raw, expMestMedicines, expMestMaterials))
                {
                    raw.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE;
                    raw.FINISH_TIME = expTime;
                }
                raw.IS_EXPORT_EQUAL_APPROVE = MOS.UTILITY.Constant.IS_TRUE;
                raw.LAST_EXP_LOGINNAME = loginname;
                raw.LAST_EXP_TIME = expTime;
                raw.LAST_EXP_USERNAME = username;
                if (!DAOWorker.HisExpMestDAO.Update(raw))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.CapNhatThatBai);
                    throw new Exception("Cap nhat thong tin hisExpMest that bai." + LogUtil.TraceData("raw", raw));
                }
                this.recentHisExpMest = raw;
                this.beforeUpdateHisExpMest = before;
            }
        }

        /// <summary>
        /// Kiem tra phieu xuat da hoan thanh xu ly hay chua
        /// </summary>
        /// <param name="sdo"></param>
        /// <param name="raw"></param>
        /// <param name="expMestMedicines"></param>
        /// <param name="expMestMaterials"></param>
        /// <returns></returns>
        private bool IsDone(HisExpMestExportSDO sdo, HIS_EXP_MEST raw, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, List<HIS_EXP_MEST_MATERIAL> expMestMaterials)
        {
            bool rs = true;
            //Kiem tra xem phieu xuat da hoan thanh chua
            //Neu la phieu linh thi kiem tra xem du lieu xuat da du so voi so luong y/c chua
            //Neu du thi la da hoan thanh, nguoc lai, dang thuc hien
            //Cac loai xuat khac thi can cu vao truong is_finish do nguoi dung gui len
            if (!(raw.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__PL
                || raw.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS))
            {
                return true;
            }
            List<HIS_EXP_MEST_MATY_REQ> expMestMatyReqs = new HisExpMestMatyReqGet().GetByExpMestId(raw.ID);
            List<HIS_EXP_MEST_METY_REQ> expMestMetyReqs = new HisExpMestMetyReqGet().GetByExpMestId(raw.ID);

            //Kiem tra voi vat tu
            if (IsNotNullOrEmpty(expMestMatyReqs))
            {
                var matyReqs = expMestMatyReqs.GroupBy(o => o.MATERIAL_TYPE_ID);
                foreach (var t in matyReqs)
                {
                    decimal totalExecuteAmount = IsNotNullOrEmpty(expMestMaterials) ?
                        expMestMaterials.Where(o => o.TDL_MATERIAL_TYPE_ID == t.Key).Sum(o => o.AMOUNT) : 0;
                    decimal totalReqAmount = t.Sum(o => o.AMOUNT);
                    if (totalExecuteAmount < totalReqAmount)
                    {
                        rs = false;
                        break;
                    }
                }
            }

            //Kiem tra voi thuoc
            if (IsNotNullOrEmpty(expMestMetyReqs))
            {
                var metyReqs = expMestMetyReqs.GroupBy(o => o.MEDICINE_TYPE_ID);
                foreach (var t in metyReqs)
                {
                    decimal totalExecuteAmount = IsNotNullOrEmpty(expMestMedicines) ?
                        expMestMedicines.Where(o => o.TDL_MEDICINE_TYPE_ID == t.Key).Sum(o => o.AMOUNT) : 0;
                    decimal totalReqAmount = t.Sum(o => o.AMOUNT);
                    if (totalExecuteAmount < totalReqAmount)
                    {
                        rs = false;
                        break;
                    }
                }
            }

            if (rs)
            {
                raw.IS_EXPORT_EQUAL_REQUEST = Constant.IS_TRUE;
            }
            else
            {
                raw.IS_EXPORT_EQUAL_REQUEST = null;
            }
            if (raw.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS)
            {
                return true;
            }
            return rs;
        }

        /// <summary>
        /// Luu y: Ham rollback nay can dat "private".
        /// Neu ben ngoai sau khi "thuc xuat" thanh cong 
        /// va muon rollback thi can thuc hien nghiep vu "huy thuc xuat"
        /// </summary>
        private void Rollback()
        {
            this.bloodProcessor.Rollback();
            this.expBltyServiceProcessor.Rollback();
            this.hisServiceReqUpdateFinish.Rollback();

            if (this.beforeUpdateHisExpMest != null)
            {
                if (!DAOWorker.HisExpMestDAO.Update(this.beforeUpdateHisExpMest))
                {
                    LogSystem.Warn("Rollback exp_mest that bai");
                }
            }
        }
    }
}
