using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTreatment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.LisSenderV1
{
    class LisBatchSenderCheck : BusinessBase
    {
        internal bool IsAllowedForStart(HIS_SERVICE_REQ data, List<HIS_PATIENT_TYPE_ALTER> patientTypeAlters, List<V_HIS_TREATMENT_FEE_1> treatmentFees)
        {
            bool valid = true;
            try
            {
                if (this.CheckStartByServiceReq(data))
                {
                    return true;
                }
                V_HIS_TREATMENT_FEE_1 treatmentFee = IsNotNullOrEmpty(treatmentFees) ? treatmentFees.Where(o => o.ID == data.TREATMENT_ID).FirstOrDefault() : null;
                //Neu ho so thuoc dien cho no vien phi thi ko can kiem tra tiep
                if (treatmentFee.OWE_TYPE_ID.HasValue)
                {
                    return true;
                }

                HIS_PATIENT_TYPE_ALTER patientTypeAlter = null;
                if (patientTypeAlters == null)
                {
                    patientTypeAlter = new HisPatientTypeAlterGet().GetApplied(data.TREATMENT_ID, data.INTRUCTION_TIME, patientTypeAlters);
                }
                else
                {
                    patientTypeAlter = new HisPatientTypeAlterGet().GetApplied(data.TREATMENT_ID, data.INTRUCTION_TIME);
                }

                if (this.CheckStartByPatientAlter(patientTypeAlter))
                {
                    return true;
                }

                decimal? unpaidAmount = new HisTreatmentGet(param).GetUnpaid(treatmentFee);
                if (this.CheckStartByUnpaidAmount(unpaidAmount))
                {
                    return true;
                }

                //Luu y: ham nay luon check sau cung (vi nguyen tac: 
                //neu cac dieu kien truoc do "true" thi ko check tiep cac dieu kien sau do)
                valid = this.CheckStartByExecuteRoom(data.EXECUTE_ROOM_ID);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        /// <summary>
        /// Kiem tra cho phep bat dau doi voi y/c nay khong, can cu vao trang thai va thuoc tinh IS_NOT_REQUIRE_FEE
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckStartByServiceReq(HIS_SERVICE_REQ data)
        {
            //Da bat dau roi ko cho thuc hien bat dau tiep
            if (data.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChiChoPhepThucHienKhiChuaBatDau);
                return false;
            }

            //Neu dich vu duoc danh dau la ko phai dong vien phi thi ko tiep tuc check
            if (data.IS_NOT_REQUIRE_FEE.HasValue && data.IS_NOT_REQUIRE_FEE.Value == MOS.UTILITY.Constant.IS_TRUE)
            {
                return true;
            }

            //Kiem tra xem tuong ung voi phieu chi dinh do co dich vu nao chua duoc thanh toan hoac tam ung khong.
            //Neu da thanh toan hoac tam ung het thi van cho bat dau
            if (this.IsPaid(data.ID))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Kiem tra xem co cho phep bat dau xu ly tuong ung voi doi tuong benh nhan nay khong
        /// </summary>
        /// <param name="patientTypeAlter"></param>
        /// <returns></returns>
        private bool CheckStartByPatientAlter(HIS_PATIENT_TYPE_ALTER patientTypeAlter)
        {
            //Neu cau hinh cho phep ko can dong vien phi doi voi cac benh nhan thuoc doi tuong la BHYT
            if (patientTypeAlter == null)
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_KhongTonTaiDuLieuTruocNgayYLenh);
                return false;
            }

            if (patientTypeAlter.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && HisServiceReqCFG.NOT_REQUIRE_FEE_FOR_BHYT == HisServiceReqCFG.NotRequireFeeOption.BY_PATIENT_TYPE)
            {
                return true;
            }

            //Neu benh nhan noi tru thi luon cho phep bat dau
            if (patientTypeAlter.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU)
            {
                return true;
            }

            //Neu BN dieu tri ngoai tru va co option ko chan dieu tri ngoai tru
            if (patientTypeAlter.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU && (HisServiceReqCFG.NOT_REQUIRE_FEE_FOR_BHYT == HisServiceReqCFG.NotRequireFeeOption.BY_PAYMENT_PATIENT_TYPE_AND_NOT_OUT_PATIENT || HisServiceReqCFG.NOT_REQUIRE_FEE_FOR_BHYT == HisServiceReqCFG.NotRequireFeeOption.BY_IN_PATIENT_AND_OUT_PATIENT))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Neu da dong du tien thi cho phep thuc hien
        /// </summary>
        /// <param name="unpaidAmount"></param>
        /// <returns></returns>
        private bool CheckStartByUnpaidAmount(decimal? unpaidAmount)
        {
            if (unpaidAmount == null)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                throw new Exception("Khong xac dinh duoc du lieu unpaid_amount tuong ung voi TREATMENT_ID cua service_req");
            }

            //Neu da dong du tien
            if (unpaidAmount <= 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Neu phong xu ly la phong cap cuu thi cho phep bat dau ma ko can dong vien phi
        /// </summary>
        /// <param name="hisExecuteRoom"></param>
        /// <returns></returns>
        private bool CheckStartByExecuteRoom(long roomId)
        {
            V_HIS_EXECUTE_ROOM hisExecuteRoom = HisExecuteRoomCFG.DATA.Where(o => o.ROOM_ID == roomId).FirstOrDefault();
            if (hisExecuteRoom == null)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                throw new Exception("Khong xac dinh duoc du lieu execute_room tuong ung voi service_req");
            }

            V_HIS_EXECUTE_ROOM hisRequestRoom = HisExecuteRoomCFG.DATA.Where(o => o.ROOM_ID == roomId).FirstOrDefault();
            if (hisRequestRoom == null)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                throw new Exception("Khong xac dinh duoc du lieu hisRequestRoom tuong ung voi service_req");
            }

            if (hisExecuteRoom.IS_EMERGENCY != MOS.UTILITY.Constant.IS_TRUE
                && hisRequestRoom.IS_EMERGENCY != MOS.UTILITY.Constant.IS_TRUE)
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongChoPhepBatDauKhiThieuVienPhi);
                return false;
            }

            return true;
        }

        private bool IsPaid(long serviceReqId)
        {
            bool valid = true;
            try
            {
                List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByServiceReqId(serviceReqId);
                if (IsNotNullOrEmpty(sereServs))
                {
                    //Kiem tra xem so tien phai tra lon hon 0 hay khong
                    decimal mustPay = sereServs.Sum(o => o.VIR_TOTAL_PATIENT_PRICE.Value);
                    if (mustPay > 0)
                    {
                        List<long> sereServIds = sereServs.Select(o => o.ID).ToList();
                        List<HIS_SERE_SERV_DEPOSIT> sereServDeposits = new HisSereServDepositGet().GetNoCancelBySereServIds(sereServIds);
                        if (IsNotNullOrEmpty(sereServDeposits))
                        {
                            List<HIS_SESE_DEPO_REPAY> seseDepoRepays = new HisSeseDepoRepayGet().GetNoCancelBySereServDepositIds(sereServDeposits.Select(s => s.ID).ToList());
                            if (IsNotNullOrEmpty(seseDepoRepays))
                            {
                                sereServDeposits = sereServDeposits.Where(o => !seseDepoRepays.Exists(e => e.SERE_SERV_DEPOSIT_ID == o.ID)).ToList();
                            }
                        }
                        List<HIS_SERE_SERV_BILL> sereServBills = new HisSereServBillGet().GetNoCancelBySereServIds(sereServIds);

                        //neu ton tai dich vu chua thanh toan va chua tam ung
                        if (sereServs.Exists(o => (!IsNotNullOrEmpty(sereServDeposits) || !sereServDeposits.Exists(t => t.SERE_SERV_ID == o.ID))
                            && (!IsNotNullOrEmpty(sereServBills) || !sereServBills.Exists(t => t.SERE_SERV_ID == o.ID))))
                        {
                            valid = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

    }
}
