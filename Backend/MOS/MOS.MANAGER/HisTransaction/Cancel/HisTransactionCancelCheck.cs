using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisUserRoom;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Cancel
{
    class HisTransactionCancelCheck : BusinessBase
    {
        internal HisTransactionCancelCheck()
            : base()
        {

        }

        internal HisTransactionCancelCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisTransactionCancelSDO data)
        {
            bool result = true;
            try
            {
                V_HIS_CASHIER_ROOM cashierRoom = null;
                HIS_TRANSACTION raw = null;
                HIS_TREATMENT hisTreatment = null;
                V_HIS_ACCOUNT_BOOK hisAccountBook = null;
                List<HIS_SERE_SERV_DEPOSIT> hisSereServDeposits = null;
                HisTransactionCheck checker = new HisTransactionCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                result = result && this.VerifyRequireField(data);
                result = result && this.HasPermission(data.RequestRoomId, ref cashierRoom);
                result = result && checker.VerifyId(data.TransactionId, ref raw);
                result = result && checker.IsNotCollect(raw);
                result = result && checker.IsUnLock(raw);
                result = result && checker.HasNotFinancePeriod(raw);
                result = result && this.IsValidTime(data.CancelTime, raw.TRANSACTION_TIME);

                //check ho so dieu tri voi cac giao dich gan lien voi ho so dieu tri
                if (raw.TREATMENT_ID.HasValue)
                {
                    result = result && treatmentChecker.IsUnLock(raw.TREATMENT_ID.Value, ref hisTreatment);//chi cho cap nhat khi chua duyet khoa tai chinh
                    result = result && treatmentChecker.IsUnTemporaryLock(hisTreatment);
                    result = result && treatmentChecker.HasNoHeinApproval(raw.TREATMENT_ID.Value);//chi cho cap nhat khi chua duyet bhyt
                    result = result && treatmentChecker.IsUnLockHein(hisTreatment);//chi cho cap nhat khi chua duyet khoa BH
                    result = result && checker.IsUnlockAccountBook(raw.ACCOUNT_BOOK_ID, ref hisAccountBook);
                    result = result && checker.IsAlowCancelDeposit(raw, ref hisSereServDeposits);
                }

                if (result)
                {
                    result = result && this.CheckSereServBill(raw);
                    result = result && this.CheckSeseDepoRepay(raw);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool IsValidTime(long cancelTime, long transactionTime)
        {
            bool valid = true;
            try
            {
                if (cancelTime < transactionTime)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_ThoiGianHuyGiaoDichNhoHonThoiGianGiaoDich);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool VerifyRequireField(HisTransactionCancelSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.TransactionId <= 0) throw new ArgumentNullException("data.TransactionId");
                if (data.RequestRoomId <= 0) throw new ArgumentNullException("data.RequestRoomId");
                if (data.CancelTime <= 0) throw new ArgumentNullException("data.CancelTime");
                if (string.IsNullOrWhiteSpace(data.CancelReason) && !data.CancelReasonId.HasValue) throw new ArgumentNullException("data.CancelReason, data.CancelReasonId");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        private bool CheckSereServBill(HIS_TRANSACTION data)
        {
            if (data.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT)
            {
                return true;
            }
            List<V_HIS_SERE_SERV_BILL> hisSereServBills = new HisSereServBillGet().GetViewByBillId(data.ID);

            if (IsNotNullOrEmpty(hisSereServBills))
            {
                //Kiem tra xem BN co don nao da linh thuoc va chua thu hoi het hay ko
                List<long> serviceReqIds = hisSereServBills
                        .Where(o => o.SERVICE_REQ_ID.HasValue && o.TDL_SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONK && o.AMOUNT > 0)
                        .Select(o => o.SERVICE_REQ_ID.Value)
                        .ToList();

                //Kiem tra neu don phong kham da thuc xuat thi khong cho phep huy thanh toan
                if (IsNotNullOrEmpty(serviceReqIds))
                {
                    HisExpMestFilterQuery filter = new HisExpMestFilterQuery();
                    filter.SERVICE_REQ_IDs = serviceReqIds;
                    filter.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE;
                    List<HIS_EXP_MEST> expMests = new HisExpMestGet().Get(filter);

                    if (IsNotNullOrEmpty(expMests))
                    {
                        List<string> expMestCodes = expMests.Select(o => o.EXP_MEST_CODE).ToList();
                        string expMestCodeStr = string.Join(",", expMestCodes);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBill_BenhNhanDaLinhThuoc, expMestCodeStr);
                        return false;
                    }
                }

                if (!new HisSereServCheck(param).HasNoInvoice(hisSereServBills.Select(s => s.SERE_SERV_ID).ToList()))
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckSeseDepoRepay(HIS_TRANSACTION data)
        {
            if (data.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__HU)
                return true;
            List<HIS_SESE_DEPO_REPAY> hisSeseDepoRepays = new HisSeseDepoRepayGet().GetByRepayId(data.ID);
            if (IsNotNullOrEmpty(hisSeseDepoRepays))
            {
                //get view de lay sereServId check trang thang dang thuc hien
                List<V_HIS_SESE_DEPO_REPAY> views = new HisSeseDepoRepayGet().GetViewByRepayId(data.ID);
                List<long> sereServIds = views.Select(s => s.SERE_SERV_ID).Distinct().ToList();
                List<HIS_SERE_SERV> hisSereServs = new HisSereServGet(param).GetByIds(sereServIds);

                //Neu dich vu o trang thai khong thuc hien thi khong cho huy hoan ung, nguoi dung phai tich thuc hien thi moi cho phep huy
                if (!new HisSereServCheck().HasExecute(hisSereServs))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisSereServ_DichVuKhongThucHienKhongChoPhepHuyHoanUng);
                    return false;
                }

            }
            return true;
        }

        internal bool HasPermission(long requestRoomId, ref V_HIS_CASHIER_ROOM cashierRoom)
        {
            cashierRoom = HisCashierRoomCFG.DATA.Where(o => o.ROOM_ID == requestRoomId).FirstOrDefault();
            if (cashierRoom == null)
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BanKhongLamViecTaiPhongThuNgan);
                return false;
            }

            HisUserRoomFilterQuery filter = new HisUserRoomFilterQuery();
            filter.LOGINNAME__EXACT = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            filter.ROOM_ID = requestRoomId;
            filter.IS_ACTIVE = Constant.IS_TRUE;

            List<HIS_USER_ROOM> userRooms = new HisUserRoomGet().Get(filter);
            if (!IsNotNullOrEmpty(userRooms))
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_BanKhongCoQuyenTruyCapVaoPhongThuNgan, cashierRoom.CASHIER_ROOM_NAME);
                return false;
            }

            return true;
        }

        internal bool HasNoProcessedServiceReq(HIS_TRANSACTION data, HIS_TREATMENT treatment)
        {
            if (data.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT)
            {
                V_HIS_MEDI_STOCK t;
                return true;
            }

            if (HisTransactionCFG.DO_NOT_ALLOW_CANCEL_BILL_IF_SERVICE_REQ_IS_PROCESSED)
            {
                bool valid = true;

                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(treatment);

                if (valid && treatment.TDL_PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                {
                    string query = "SELECT * FROM HIS_SERE_SERV SS JOIN HIS_SERVICE_REQ REQ ON SS.SERVICE_REQ_ID = REQ.ID WHERE EXISTS (SELECT 1 FROM HIS_SERE_SERV_BILL WHERE SS.ID = SERE_SERV_ID AND BILL_ID = :param1) AND REQ.SERVICE_REQ_STT_ID <> :param2";
                    List<HIS_SERE_SERV> sereServ = DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV>(query, data.ID, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL);
                    if (IsNotNullOrEmpty(sereServ))
                    {
                        List<string> mess = new List<string>();
                        var groupss = sereServ.GroupBy(o => o.TDL_SERVICE_REQ_CODE).ToList();
                        int count = 1;
                        foreach (var gr in groupss)
                        {
                            V_HIS_ROOM room = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == gr.First().TDL_EXECUTE_ROOM_ID);
                            string room_name = room != null ? room.ROOM_NAME : "";
                            string room_address = (room != null && !String.IsNullOrWhiteSpace(room.ADDRESS)) ? String.Format("({0})", room.ADDRESS) : "";

                            mess.Add(string.Format("{0}. {1} - {2}{3}: {4}", count, gr.Key, room_name, room_address, string.Join(",", gr.Select(s => s.TDL_SERVICE_NAME).Distinct()), gr.Key));
                            count++;
                        }

                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_TonTaiDichVuDaThucHien, String.Format("\n{0}", string.Join(".\n", mess)));
                        valid = false;
                    }
                }

                return valid;
            }

            return true;
        }
    }
}
