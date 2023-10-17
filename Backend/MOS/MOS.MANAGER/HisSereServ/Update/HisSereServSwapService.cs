using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisSereServ.Update.Package;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServ
{
    internal partial class HisSereServSwapService : BusinessBase
    {
        private HisSereServUpdateHein hisSereServUpdateHein;
        private HisSereServCreate hisSereServCreate;
        private HisSereServUpdate hisSereServUpdate;
        private HisExpMestMedicineUpdate hisExpMestMedicineUpdate;
        private HisExpMestMaterialUpdate hisExpMestMaterialUpdate;

        internal HisSereServSwapService()
            : base()
        {
            this.Init();
        }

        internal HisSereServSwapService(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServCreate = new HisSereServCreate(param);
            this.hisSereServUpdate = new HisSereServUpdate(param);
            this.hisExpMestMedicineUpdate = new HisExpMestMedicineUpdate(param);
            this.hisExpMestMaterialUpdate = new HisExpMestMaterialUpdate(param);
        }

        /// <summary>
        /// Doi dich vu cua sere_serv
        /// </summary>
        /// <param name="hisSereServs"></param>
        /// <returns></returns>
        internal bool SwapService(SwapServiceSDO sdo, ref HIS_SERE_SERV resultData)
        {
            bool result = false;
            try
            {
                HIS_SERE_SERV old = null;
                HIS_TREATMENT treatment = null;
                HIS_SERVICE_REQ serviceReq = null;

                if (this.Validate(sdo, ref old, ref serviceReq, ref treatment))
                {
                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();

                    //Cap nhat du lieu cu thanh "khong thuc hien"
                    HIS_SERE_SERV oldSereServ = Mapper.Map<HIS_SERE_SERV>(old);
                    HIS_SERE_SERV beforeUpdate = Mapper.Map<HIS_SERE_SERV>(old); //clone phuc vu rollback
                    oldSereServ.IS_NO_EXECUTE = MOS.UTILITY.Constant.IS_TRUE;

                    //Tao du lieu moi voi service_id tuong ung voi service_id moi
                    HIS_SERE_SERV newSereServ = Mapper.Map<HIS_SERE_SERV>(old);
                    newSereServ.SERVICE_ID = sdo.NewService.ServiceId;
                    newSereServ.IS_EXPEND = sdo.NewService.IsExpend;
                    newSereServ.IS_OUT_PARENT_FEE = sdo.NewService.IsOutParentFee;
                    newSereServ.PATIENT_TYPE_ID = sdo.NewService.PatientTypeId;
                    newSereServ.AMOUNT = sdo.NewService.Amount;

                    HisSereServPackageUtil.AutoAssign(newSereServ);

                    //SereServUpdateHein ko can check treatment vi da check o ham validate o phia tren
                    this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, false);

                    //Cap nhat thong tin gia theo dich vu moi
                    HIS_DEPARTMENT department = HisDepartmentCFG.DATA != null ? HisDepartmentCFG.DATA.Where(o => o.ID == serviceReq.EXECUTE_DEPARTMENT_ID).FirstOrDefault() : null;
                    HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment, null, null);
                    if (!priceAdder.AddPrice(newSereServ, serviceReq.INTRUCTION_TIME, department.BRANCH_ID, old.TDL_REQUEST_ROOM_ID, old.TDL_REQUEST_DEPARTMENT_ID, old.TDL_EXECUTE_ROOM_ID))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    if (this.hisSereServUpdate.Update(oldSereServ, beforeUpdate)
                        && this.hisSereServCreate.Create(newSereServ, serviceReq, false))
                    {
                        //cap nhat cac dv dinh kem sang sere_serv moi
                        this.ProcessChildren(oldSereServ.ID, newSereServ.ID);

                        this.AutoUpdate3day7day(newSereServ, serviceReq);

                        //Cap nhat ti le BHYT cho sere_serv
                        if (!this.hisSereServUpdateHein.UpdateDb())
                        {
                            throw new Exception("Rollback du lieu");
                        }
                        result = true;
                        resultData = newSereServ;

                        new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisSereServ_DoiDichVu, oldSereServ.TDL_SERVICE_CODE, oldSereServ.TDL_SERVICE_NAME, newSereServ.TDL_SERVICE_CODE, newSereServ.TDL_SERVICE_NAME)
                            .TreatmentCode(treatment.TREATMENT_CODE)
                            .ServiceReqCode(newSereServ.TDL_SERVICE_REQ_CODE)
                            .Run();
                    }
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        //Tu dong cap nhat du lieu goi 3/7 (nghiep vu dac thu cua vien Tim)
        private void AutoUpdate3day7day(HIS_SERE_SERV raw, HIS_SERVICE_REQ serviceReq)
        {
            List<HIS_SERE_SERV> changeRecords = null;
            List<HIS_SERE_SERV> oldOfChangeRecords = null;
            List<HIS_EXP_MEST_MEDICINE> changeRecordMedicines = null;
            List<HIS_EXP_MEST_MEDICINE> oldOfChangeRecordMedicines = null;
            List<HIS_EXP_MEST_MATERIAL> changeRecordMaterials = null;
            List<HIS_EXP_MEST_MATERIAL> oldOfChangeRecordMaterials = null;

            new HisSereServPackage37(param).Update3Day7Day(serviceReq, raw,
                ref changeRecords, ref oldOfChangeRecords,
                ref changeRecordMedicines, ref oldOfChangeRecordMedicines,
                ref changeRecordMaterials, ref oldOfChangeRecordMaterials);

            //Neu co su thay doi du lieu thi thuc hien cap nhat
            if (IsNotNullOrEmpty(changeRecords))
            {
                if (!this.hisSereServUpdate.UpdateList(changeRecords, oldOfChangeRecords, false))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }

            //Neu co su thay doi du lieu thi thuc hien cap nhat
            if (IsNotNullOrEmpty(changeRecordMedicines))
            {
                if (!this.hisExpMestMedicineUpdate.UpdateList(changeRecordMedicines, oldOfChangeRecordMedicines))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }

            //Neu co su thay doi du lieu thi thuc hien cap nhat
            if (IsNotNullOrEmpty(changeRecordMaterials))
            {
                if (!this.hisExpMestMaterialUpdate.UpdateList(changeRecordMaterials, oldOfChangeRecordMaterials))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessChildren(long oldSereServId, long newSereServId)
        {
            string updateSereServ = string.Format("UPDATE HIS_SERE_SERV SET PARENT_ID = {0} WHERE PARENT_ID = {1}", newSereServId, oldSereServId);
            string updateExpMestMedicine = string.Format("UPDATE HIS_EXP_MEST_MEDICINE SET SERE_SERV_PARENT_ID = {0} WHERE SERE_SERV_PARENT_ID = {1}", newSereServId, oldSereServId);
            string updateExpMestMaterial = string.Format("UPDATE HIS_EXP_MEST_MATERIAL SET SERE_SERV_PARENT_ID = {0} WHERE SERE_SERV_PARENT_ID = {1}", newSereServId, oldSereServId);
            string updateExpMestBlood = string.Format("UPDATE HIS_EXP_MEST_BLOOD SET SERE_SERV_PARENT_ID = {0} WHERE SERE_SERV_PARENT_ID = {1}", newSereServId, oldSereServId);
            string updateExpMestBltyReq = string.Format("UPDATE HIS_EXP_MEST_BLTY_REQ SET SERE_SERV_PARENT_ID = {0} WHERE SERE_SERV_PARENT_ID = {1}", newSereServId, oldSereServId);
            List<string> sqls = new List<string>() { updateSereServ, updateExpMestMedicine, updateExpMestMaterial, updateExpMestBltyReq };

            //review: trong truong hop can rollback thi xu ly the nao???
            if (!DAOWorker.SqlDAO.Execute(sqls))
            {
                throw new Exception("Cap nhat cac dich vu dinh kem that bai");
            }
        }

        private void RollbackData()
        {
            if (this.hisSereServUpdateHein != null)
            {
                this.hisSereServUpdateHein.RollbackData();
            }
            this.hisSereServCreate.RollbackData();
            this.hisSereServUpdate.RollbackData();
            this.hisExpMestMedicineUpdate.RollbackData();
            this.hisExpMestMaterialUpdate.RollbackData();
        }

        private bool Validate(SwapServiceSDO sdo, ref HIS_SERE_SERV old, ref HIS_SERVICE_REQ serviceReq, ref HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                if (sdo == null || sdo.NewService == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                }

                //Kiem tra phong nguoi dung truy cap duoc phep sua dich vu hay khong
                WorkPlaceSDO workPlace = null;
                if (!this.HasWorkPlaceInfo(sdo.ExecuteRoomId, ref workPlace))
                {
                    return false;
                }

                old = new HisSereServGet().GetById(sdo.SereServId);
                serviceReq = old != null && old.SERVICE_REQ_ID.HasValue ? new HisServiceReqGet().GetById(old.SERVICE_REQ_ID.Value) : null;

                if (old == null || serviceReq == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Id sere_serv khong hop le hoac ko co thong tin service_req (co the do da bi xoa)");
                    return false;
                }

                if (serviceReq.EXECUTE_ROOM_ID != workPlace.RoomId)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_BanKhongTruyCapPhongXuLyKhongCoQuyenSuaDichVu);
                    return false;
                }

                //Kiem tra xem dich vu nguoi dung can doi sang co the xu ly o phong nay hay khong
                List<V_HIS_SERVICE_ROOM> serviceRooms = HisServiceRoomCFG.DATA_VIEW != null ?
                    HisServiceRoomCFG.DATA_VIEW.Where(o => o.ROOM_ID == workPlace.RoomId && o.SERVICE_ID == sdo.NewService.ServiceId && o.IS_ACTIVE == Constant.IS_TRUE).ToList() : null;

                if (!IsNotNullOrEmpty(serviceRooms))
                {
                    V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW != null ? HisServiceCFG.DATA_VIEW.Where(o => o.ID == sdo.NewService.ServiceId).FirstOrDefault() : null;
                    string serviceName = service != null ? service.SERVICE_NAME : "";
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_PhongDuocChiDinhKhongTheXuLyDichVu, serviceName);
                    return false;
                }

                HisSereServCheck checker = new HisSereServCheck(param);
                //Check cac dieu kien lien quan den ho so dieu tri
                HisTreatmentCheck treatmentCheck = new HisTreatmentCheck(param);
                valid = treatmentCheck.VerifyId(old.TDL_TREATMENT_ID.Value, ref treatment);
                valid = valid && treatmentCheck.IsUnLock(treatment);
                valid = valid && treatmentCheck.IsUnTemporaryLock(treatment);
                valid = valid && treatmentCheck.IsUnLockHein(treatment);
                valid = valid && checker.HasNoBill(old);
                valid = valid && checker.HasNoHeinApproval(old);
                valid = valid && checker.HasNoInvoice(old);
                valid = valid && checker.HasExecute(old);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }
    }
}
