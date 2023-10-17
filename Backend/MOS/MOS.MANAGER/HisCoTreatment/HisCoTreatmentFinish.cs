using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatmentBedRoom;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisCoTreatment
{
    class HisCoTreatmentFinish : BusinessBase
    {
        private HIS_CO_TREATMENT recentCoTreatment;

        private HisCoTreatmentUpdate hisCoTreatmentUpdate;
        private HisTreatmentBedRoomRemove hisTreatmentBedRoomRemove;
        private HisTreatmentUpdate treatUpdateProcessor;

        internal HisCoTreatmentFinish()
            : base()
        {
            this.Init();
        }

        internal HisCoTreatmentFinish(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisCoTreatmentUpdate = new HisCoTreatmentUpdate(param);
            this.hisTreatmentBedRoomRemove = new HisTreatmentBedRoomRemove(param);
            this.treatUpdateProcessor = new HisTreatmentUpdate(param);
        }

        internal bool Finish(HisCoTreatmentFinishSDO data, ref HIS_CO_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                HisTreatmentCheckPrescription checkPres = new HisTreatmentCheckPrescription(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                bool valid = true;
                WorkPlaceSDO workPlaceSDO = null;
                HIS_CO_TREATMENT coTreatment = null;
                HIS_TREATMENT treatment = null;
                
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlaceSDO);
                valid = valid && this.IsValidCoTreatment(workPlaceSDO, data, ref coTreatment);
                valid = valid && checkPres.HasNoPostponePrescriptionByDepartmentTran(coTreatment.DEPARTMENT_ID, coTreatment.TDL_TREATMENT_ID);
                valid = valid && treatmentChecker.VerifyId(coTreatment.TDL_TREATMENT_ID, ref treatment);

                if (valid)
                {
                    this.ProcessCoTreatment(data, coTreatment);
                    this.ProcessTreatmentBedRoom(data);
                    this.ProcessTreatment(data, treatment);
                    resultData = this.recentCoTreatment;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
            }
            return result;
        }

        private void ProcessTreatment(HisCoTreatmentFinishSDO data, HIS_TREATMENT treatment)
        {
            if (!string.IsNullOrWhiteSpace(data.IcdSubCode) && !string.IsNullOrWhiteSpace(data.IcdText))
            {
                HisTreatmentUpdate.AddIcd(treatment, data.IcdSubCode, data.IcdText);
                if (!this.treatUpdateProcessor.Update(treatment))
                {
                    throw new Exception("Cap nhat lai thong tin benh phu dieu tri ket hop cho ho so dieu tri that bai");
                }
            }
        }

        private bool IsValidCoTreatment(WorkPlaceSDO sdo, HisCoTreatmentFinishSDO data, ref HIS_CO_TREATMENT coTreatment)
        {
            HIS_CO_TREATMENT coTreat = new HisCoTreatmentGet().GetById(data.Id);
            if (coTreat == null)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                throw new Exception("HisCoTreatmentId Invalid: " + data.Id);
            }

            if (!coTreat.START_TIME.HasValue)
            {
                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisCoTreatment_BenhNhanChuaDuocTiepNhanDieuTriKetHop);
                return false;
            }

            if (coTreat.START_TIME.Value > data.FinishTime)
            {
                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_ThoiGianKetThucKhongDuocBeHonThoiGianBatDau, Inventec.Common.DateTime.Convert.TimeNumberToTimeString(coTreat.START_TIME.Value));
                return false;
            }

            if (sdo == null || sdo.DepartmentId != coTreat.DEPARTMENT_ID)
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisCoTreatment_PhongLamViecKhongThuocKhoaDieuTriKetHop, sdo != null ? sdo.DepartmentName : "");
                return false;
            }
            coTreatment = coTreat;
            return true;
        }

        private void ProcessCoTreatment(HisCoTreatmentFinishSDO data, HIS_CO_TREATMENT coTreat)
        {

            Mapper.CreateMap<HIS_CO_TREATMENT, HIS_CO_TREATMENT>();
            HIS_CO_TREATMENT update = Mapper.Map<HIS_CO_TREATMENT>(coTreat);
            update.FINISH_TIME = data.FinishTime;
            update.ICD_SUB_CODE = data.IcdSubCode;
            update.ICD_TEXT = data.IcdText;

            if (!this.hisCoTreatmentUpdate.Update(update, coTreat))
            {
                throw new Exception("Cap nhat HisCoTreatment that bai. ket thuc nghiep vu");
            }
            this.recentCoTreatment = update;
        }

        private void ProcessTreatmentBedRoom(HisCoTreatmentFinishSDO data)
        {
            List<HIS_TREATMENT_BED_ROOM> treatmentBedRooms = new HisTreatmentBedRoomGet().GetCurrentInByCoTreatmentId(data.Id);
            if (IsNotNullOrEmpty(treatmentBedRooms))
            {
                List<HIS_TREATMENT_BED_ROOM> tmp = null;
                if (!this.hisTreatmentBedRoomRemove.Remove(treatmentBedRooms, data.FinishTime, false, ref tmp))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
            }
        }

        internal void RollbackData()
        {
            try
            {
                this.treatUpdateProcessor.RollbackData();
                this.hisTreatmentBedRoomRemove.RollbackData();
                this.hisCoTreatmentUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
