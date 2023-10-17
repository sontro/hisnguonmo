using AutoMapper;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBedLog;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatment.Util;
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
    class HisCoTreatmentReceive : BusinessBase
    {
        private HIS_CO_TREATMENT recentCoTreatment = null;
        private HIS_TREATMENT_BED_ROOM recentTreatmentBedRoom = null;

        private HisCoTreatmentUpdate hisCoTreatmentUpdate;
        private HisTreatmentBedRoomCreate hisTreatmentBedRoomCreate;
        private HisBedLogCreate hisBedLogCreate;
        private HisTreatmentUpdate hisTreatmentUpdate;

        internal HisCoTreatmentReceive()
            : base()
        {
            this.Init();
        }

        internal HisCoTreatmentReceive(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisCoTreatmentUpdate = new HisCoTreatmentUpdate(param);
            this.hisTreatmentBedRoomCreate = new HisTreatmentBedRoomCreate(param);
            this.hisBedLogCreate = new HisBedLogCreate(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal bool Receive(HisCoTreatmentReceiveSDO data, ref HIS_CO_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                this.ProcessCoTreatment(data);
                this.ProcessTreatmentBedRoom(data);
                this.ProcessBedLog(data);
                this.ProcessTreatment();
                resultData = this.recentCoTreatment;
                result = true;
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

        private void ProcessCoTreatment(HisCoTreatmentReceiveSDO data)
        {

            HIS_CO_TREATMENT coTreat = new HisCoTreatmentGet().GetById(data.Id);
            if (coTreat == null)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                throw new Exception("Khong lay duoc HIS_CO_TREATMENT theo id: " + data.Id);
            }
            HIS_DEPARTMENT_TRAN dt = new HisDepartmentTranGet().GetById(coTreat.DEPARTMENT_TRAN_ID);

            if (coTreat.START_TIME.HasValue)
            {
                HIS_DEPARTMENT department = HisDepartmentCFG.DATA != null ? HisDepartmentCFG.DATA.Where(o => o.ID == coTreat.DEPARTMENT_ID).FirstOrDefault() : null;
                string departmentName = department != null ? department.DEPARTMENT_NAME : "";
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisCoTreatment_BenhNhanDaDuocTiepNhanVaoKhoa, departmentName);
                throw new Exception("Benh nhan da duoc tiep (dieu tri ket hop) nhan vao khoa " + departmentName);
            }

            if (dt.DEPARTMENT_IN_TIME.HasValue && data.StartTime < dt.DEPARTMENT_IN_TIME.Value)
            {
                string inTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(dt.DEPARTMENT_IN_TIME.Value);
                string startTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.StartTime);
                HIS_DEPARTMENT department = HisDepartmentCFG.DATA.Where(o => o.ID == dt.DEPARTMENT_ID).FirstOrDefault();
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisCoTreatment_ThoiGianKhongDuocNhoHonThoiGianVaoKhoaChinh, department.DEPARTMENT_NAME, inTime);
                throw new Exception("Thoi gian khong duoc nho hon thoi gian vao khoa chinh startTime: " + startTime + " - DepartmentInTime: " + inTime + " - Department: " + department.DEPARTMENT_NAME);
            }

            WorkPlaceSDO sdo = TokenManager.GetWorkPlace(data.RequestRoomId);
            if (sdo == null || sdo.DepartmentId != coTreat.DEPARTMENT_ID)
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisCoTreatment_PhongLamViecKhongThuocKhoaTiepNhan, sdo != null ? sdo.DepartmentName : "");
                throw new Exception("Phong lam vien khong thuoc khoa tiep nhan RequestRoomId:" + data.RequestRoomId);
            }

            Mapper.CreateMap<HIS_CO_TREATMENT, HIS_CO_TREATMENT>();
            HIS_CO_TREATMENT update = Mapper.Map<HIS_CO_TREATMENT>(coTreat);
            update.START_TIME = data.StartTime;
            if (!this.hisCoTreatmentUpdate.Update(update, coTreat))
            {
                throw new Exception("Cap nhat HisCoTreatment that bai. ket thuc nghiep vu");
            }
            this.recentCoTreatment = update;
        }

        private void ProcessTreatmentBedRoom(HisCoTreatmentReceiveSDO data)
        {
            HIS_TREATMENT_BED_ROOM treatmentBedRoom = new HIS_TREATMENT_BED_ROOM();
            treatmentBedRoom.BED_ROOM_ID = data.BedRoomId;
            treatmentBedRoom.TREATMENT_ID = this.recentCoTreatment.TDL_TREATMENT_ID;
            treatmentBedRoom.CO_TREATMENT_ID = this.recentCoTreatment.ID;
            treatmentBedRoom.ADD_TIME = data.StartTime;
            treatmentBedRoom.BED_ID = data.BedId;
            if (!hisTreatmentBedRoomCreate.Create(treatmentBedRoom))
            {
                throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
            }
            this.recentTreatmentBedRoom = treatmentBedRoom;
        }

        private void ProcessBedLog(HisCoTreatmentReceiveSDO data)
        {
            if (data.BedId.HasValue && this.recentTreatmentBedRoom != null)
            {
                HIS_BED_LOG bedLog = new HIS_BED_LOG();
                bedLog.BED_ID = data.BedId.Value;
                bedLog.BED_SERVICE_TYPE_ID = data.BedServiceId;
                bedLog.TREATMENT_BED_ROOM_ID = this.recentTreatmentBedRoom.ID;
                bedLog.START_TIME = data.StartTime;
                if (!this.hisBedLogCreate.Create(bedLog))
                {
                    throw new Exception("Tao thong tin giuong that bai. Ket thuc nghiep vu Rollback du lieu");
                }
            }
        }

        private void ProcessTreatment()
        {
            HIS_TREATMENT treatment = new HisTreatmentGet().GetById(this.recentCoTreatment.TDL_TREATMENT_ID);
            if (treatment != null)
            {
                Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                HIS_TREATMENT beforeUpdate = Mapper.Map<HIS_TREATMENT>(treatment);

                List<HIS_CO_TREATMENT> allCoTreats = new HisCoTreatmentGet().GetByTreatmentId(treatment.ID);
                HisTreatmentUtil.SetCoDepartmentInfo(treatment, allCoTreats);

                if (ValueChecker.IsPrimitiveDiff<HIS_TREATMENT>(beforeUpdate, treatment)
                    && !this.hisTreatmentUpdate.Update(treatment, beforeUpdate))
                {
                    throw new Exception("Cap nhat thong tin CO_DEPARTMENT_IDS cho bang Treatment that bai. Rollback du lieu");
                }
            }
        }

        internal void RollbackData()
        {
            try
            {
                this.hisTreatmentUpdate.RollbackData();
                this.hisBedLogCreate.RollbackData();
                this.hisTreatmentBedRoomCreate.RollbackData();
                this.hisCoTreatmentUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
