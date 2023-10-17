using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Sale.Update
{
    class HisExpMestProcessor : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;

        internal HisExpMestProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
        }

        internal bool Run(HisExpMestSaleSDO data, HIS_EXP_MEST hisExpMest, ref HIS_EXP_MEST resultData, AutoEnum en = AutoEnum.NONE, long? time = null, string loginname = null, string username = null)
        {
            try
            {
                Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                HIS_EXP_MEST before = Mapper.Map<HIS_EXP_MEST>(hisExpMest);

                hisExpMest.SALE_PATIENT_TYPE_ID = data.PatientTypeId;
                hisExpMest.MEDI_STOCK_ID = data.MediStockId;
                hisExpMest.DESCRIPTION = String.IsNullOrWhiteSpace(data.Description) ? null : data.Description;
                hisExpMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BAN; //xuat ban
                hisExpMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST; //xuat ban khi tao ra o trang thai duyet (Thay doi van de o trang thai yeu cau)
                hisExpMest.PRESCRIPTION_ID = data.PrescriptionId;
                hisExpMest.DISCOUNT = data.Discount;
                hisExpMest.IS_NOT_IN_WORKING_TIME = data.IsNotInWorkingTime ? (short?)Constant.IS_TRUE : null;
                hisExpMest.CASHIER_ROOM_ID = data.CashierRoomId;

                hisExpMest.ICD_CODE = data.IcdCode;
                hisExpMest.ICD_NAME = data.IcdName;
                hisExpMest.ICD_SUB_CODE = data.IcdSubCode;
                hisExpMest.ICD_TEXT = data.IcdText;

                if (data.PrescriptionId.HasValue)
                {
                    HIS_SERVICE_REQ prescription = new HisServiceReqGet().GetById(data.PrescriptionId.Value);
                    if (prescription == null)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("prescription_id ko ton tai");
                    }
                    HisExpMestUtil.SetTdl(hisExpMest, prescription, true);
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(data.PatientName))
                    {
                        data.PatientName = data.PatientName.Trim();

                        int idx = data.PatientName.LastIndexOf(" ");
                        hisExpMest.TDL_PATIENT_NAME = data.PatientName;
                        hisExpMest.TDL_PATIENT_FIRST_NAME = (idx > -1 ? data.PatientName.Substring(idx).Trim() : data.PatientName);
                        hisExpMest.TDL_PATIENT_LAST_NAME = (idx > -1 ? data.PatientName.Substring(0, idx).Trim() : "");
                    }
                    hisExpMest.TDL_PATIENT_ADDRESS = data.PatientAddress;
                    hisExpMest.TDL_PATIENT_GENDER_ID = data.PatientGenderId;
                    if (data.PatientGenderId.HasValue)
                    {
                        HIS_GENDER gender = HisGenderCFG.DATA.Where(o => o.ID == data.PatientGenderId).FirstOrDefault();
                        hisExpMest.TDL_PATIENT_GENDER_NAME = gender != null ? gender.GENDER_NAME : null;
                    }
                    hisExpMest.TDL_PATIENT_DOB = data.PatientDob;
                    hisExpMest.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = data.IsHasNotDayDob ? (short?)Constant.IS_TRUE : null;
                    hisExpMest.TDL_INTRUCTION_TIME = data.InstructionTime;
                    hisExpMest.TDL_INTRUCTION_DATE = data.InstructionTime.HasValue ? data.InstructionTime - data.InstructionTime % 1000000 : null;
                }

                hisExpMest.TDL_PATIENT_TAX_CODE = data.PatientTaxCode;
                hisExpMest.TDL_PATIENT_ACCOUNT_NUMBER = data.PatientAccountNumber;
                hisExpMest.TDL_PATIENT_WORK_PLACE = data.PatientWorkPlace;

                if (en == AutoEnum.APPROVE)
                {
                    hisExpMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE;
                    hisExpMest.IS_EXPORT_EQUAL_APPROVE = null;
                    hisExpMest.LAST_APPROVAL_TIME = time;
                    hisExpMest.LAST_APPROVAL_LOGINNAME = loginname;
                    hisExpMest.LAST_APPROVAL_USERNAME = username;
                    hisExpMest.LAST_APPROVAL_DATE = hisExpMest.LAST_APPROVAL_TIME - hisExpMest.LAST_APPROVAL_TIME % 1000000;
                }
                else if (en == AutoEnum.APPROVE_EXPORT && (hisExpMest.DEBT_ID.HasValue || hisExpMest.BILL_ID.HasValue || !HisExpMestCFG.EXPORT_SALE_MUST_BILL))
                {
                    hisExpMest.IS_EXPORT_EQUAL_APPROVE = null;
                    hisExpMest.LAST_APPROVAL_TIME = time;
                    hisExpMest.LAST_APPROVAL_LOGINNAME = loginname;
                    hisExpMest.LAST_APPROVAL_USERNAME = username;
                    hisExpMest.LAST_APPROVAL_DATE = hisExpMest.LAST_APPROVAL_TIME - hisExpMest.LAST_APPROVAL_TIME % 1000000;

                    hisExpMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE;
                    hisExpMest.FINISH_TIME = time;
                    hisExpMest.IS_EXPORT_EQUAL_APPROVE = MOS.UTILITY.Constant.IS_TRUE;
                    hisExpMest.LAST_EXP_LOGINNAME = loginname;
                    hisExpMest.LAST_EXP_TIME = time;
                    hisExpMest.LAST_EXP_USERNAME = username;
                }
                hisExpMest.TDL_PRESCRIPTION_REQ_USERNAME = data.PrescriptionReqUsername;
                hisExpMest.TDL_PRESCRIPTION_REQ_LOGINNAME = data.PrescriptionReqLoginname;

                if (ValueChecker.IsPrimitiveDiff<HIS_EXP_MEST>(before, hisExpMest))
                {
                    if (!this.hisExpMestUpdate.Update(hisExpMest, before))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                }
                resultData = hisExpMest;
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal void Rollback()
        {
            this.hisExpMestUpdate.RollbackData();
        }
    }
}
