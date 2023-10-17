using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisBedRoom;
using MOS.MANAGER.HisCoTreatment;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentBedRoom
{
    partial class HisTreatmentBedRoomCreate : BusinessBase
    {
        private HisPatientTypeAlterCreate hisPatientTypeAlterCreate;
        private HisTreatmentBedRoomRemove hisTreatmentBedRoomRemove;

        internal bool CreateSdo(HisTreatmentBedRoomSDO data, ref HisTreatmentBedRoomSDO resultData)
        {
            bool result = false;
            try
            {
                this.hisPatientTypeAlterCreate = new HisPatientTypeAlterCreate(param);
                this.hisTreatmentBedRoomRemove = new HisTreatmentBedRoomRemove(param);
                HIS_CO_TREATMENT coTreatment = null;
                V_HIS_BED_ROOM bedRoom = HisBedRoomCFG.DATA.FirstOrDefault(o => o.ID == data.BED_ROOM_ID);
                if (bedRoom == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("data.BED_ROOM_ID invalid");
                }

                HIS_TREATMENT treatment = null;
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                bool valid = true;
                valid = valid && treatmentChecker.VerifyId(data.TREATMENT_ID, ref treatment);
                valid = valid && treatmentChecker.IsUnpause(treatment);

                if (valid)
                {
                    this.CheckCoTreatment(data, bedRoom, ref coTreatment);
                    this.ProcessRemoveCurrentBedRoom(data, bedRoom);
                    this.ProcessPatientTypeAlter(data);
                    Mapper.CreateMap<HisTreatmentBedRoomSDO, HIS_TREATMENT_BED_ROOM>();
                    HIS_TREATMENT_BED_ROOM treatmentBedRoom = Mapper.Map<HIS_TREATMENT_BED_ROOM>(data);
                    if (coTreatment != null)
                    {
                        treatmentBedRoom.CO_TREATMENT_ID = coTreatment.ID;
                    }
                    if (!this.Create(treatmentBedRoom, data.BedServiceTypeId, data.ShareCount, data.PatientTypeId, data.PrimaryPatientTypeId))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                    Mapper.CreateMap<HIS_TREATMENT_BED_ROOM, HisTreatmentBedRoomSDO>();
                    resultData = Mapper.Map<HisTreatmentBedRoomSDO>(treatmentBedRoom);
                    resultData.TreatmentTypeId = data.TreatmentTypeId;

                    new EventLogGenerator(EventLog.Enum.HisTreatment_VaoBuong, bedRoom.BED_ROOM_NAME).TreatmentCode(treatment.TREATMENT_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                this.hisPatientTypeAlterCreate.RollbackData();
                this.hisTreatmentBedRoomRemove.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessRemoveCurrentBedRoom(HisTreatmentBedRoomSDO data, V_HIS_BED_ROOM bedRoom)
        {
            if (data.IsAutoRemove.HasValue && data.IsAutoRemove.Value)
            {
                List<long> listIds = HisBedRoomCFG.DATA.Where(o => o.DEPARTMENT_ID == bedRoom.DEPARTMENT_ID).Select(s => s.ID).ToList();
                List<V_HIS_TREATMENT_BED_ROOM> treatmentBedRooms = new HisTreatmentBedRoomGet().GetViewCurrentInByTreatmentId(data.TREATMENT_ID);
                treatmentBedRooms = treatmentBedRooms != null ? treatmentBedRooms.Where(o => listIds != null && listIds.Contains(o.BED_ROOM_ID)).ToList() : null;
                if (IsNotNullOrEmpty(treatmentBedRooms))
                {
                    if (treatmentBedRooms.Exists(e => e.BED_ROOM_ID == data.BED_ROOM_ID))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatmentBedRoom_BenhNhanDangTrongBuongBenhKhongChoPhepTao);
                        throw new Exception("Benh nhan dang trong buong benh, khong cho phep tao" + LogUtil.TraceData("treatmentBedRooms", treatmentBedRooms));
                    }
                    Mapper.CreateMap<V_HIS_TREATMENT_BED_ROOM, HIS_TREATMENT_BED_ROOM>();
                    List<HIS_TREATMENT_BED_ROOM> listUpdate = Mapper.Map<List<HIS_TREATMENT_BED_ROOM>>(treatmentBedRooms);
                    List<HIS_TREATMENT_BED_ROOM> resultUpdates = null;
                    if (!this.hisTreatmentBedRoomRemove.Remove(listUpdate, data.ADD_TIME, false, ref resultUpdates))
                    {
                        throw new Exception("hisTreatmentBedRoomRemove. Ket thuc nghiep vu");
                    }
                }
            }
        }

        private void ProcessPatientTypeAlter(HisTreatmentBedRoomSDO data)
        {
            if (data.TreatmentTypeId.HasValue)
            {
                HIS_PATIENT_TYPE_ALTER resultData = new HIS_PATIENT_TYPE_ALTER();
                if (!this.hisPatientTypeAlterCreate.CreateByChangeTreatmentType(data.TREATMENT_ID, data.TreatmentTypeId.Value, data.ADD_TIME, data.RequestRoomId, ref resultData))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
            }
        }

        private void CheckCoTreatment(HisTreatmentBedRoomSDO data, V_HIS_BED_ROOM bedRoom, ref HIS_CO_TREATMENT coTreatment)
        {
            HisCoTreatmentFilterQuery filterQuery = new HisCoTreatmentFilterQuery();
            filterQuery.TDL_TREATMENT_ID = data.TREATMENT_ID;
            filterQuery.DEPARTMENT_ID = bedRoom.DEPARTMENT_ID;
            filterQuery.HAS_START_TIME = true;
            filterQuery.HAS_FINISH_TIME = false;
            List<HIS_CO_TREATMENT> coTreatments = new HisCoTreatmentGet().Get(filterQuery);
            if (IsNotNullOrEmpty(coTreatments))
            {
                coTreatment = coTreatments.FirstOrDefault();
            }

        }
    }
}
