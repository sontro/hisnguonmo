using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBlood;
using MOS.MANAGER.HisBloodType;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.ServicePaty;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMestBlood
{
    class HisExpMestBloodMaker : BusinessBase
    {
        private HisBloodLock hisBloodLock;
        private HisExpMestBloodCreate hisExpMestBloodCreate;
        private List<long> bloodIds;

        internal HisExpMestBloodMaker(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisBloodLock = new HisBloodLock(param);
            this.hisExpMestBloodCreate = new HisExpMestBloodCreate(param);
        }

        internal bool Run(List<ExpBloodSDO> bloods, HIS_EXP_MEST expMest, string loginname, string username, long? approvalTime, ref List<V_HIS_BLOOD> bls, ref string exBloodCodes, ref List<HIS_EXP_MEST_BLOOD> resultData, ref List<string> sqls)
        {
            try
            {
                if (IsNotNullOrEmpty(bloods) && expMest != null)
                {

                    List<HIS_EXP_MEST_BLTY_REQ> hisExpMestBltyReqs = new HisExpMestBltyReqGet().GetByExpMestId(expMest.ID);
                    this.bloodIds = bloods.Select(o => o.BloodId).ToList();

                    HisBloodViewFilterQuery filter = new HisBloodViewFilterQuery();
                    filter.IDs = bloodIds;
                    bls = new HisBloodGet().GetView(filter);

                    if (!IsNotNullOrEmpty(bls))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("blood_id ko hop le");
                        return false;
                    }

                    List<string> notInMediStocks = bls.Where(o => o.MEDI_STOCK_ID != expMest.MEDI_STOCK_ID).Select(o => o.BLOOD_CODE).ToList();
                    if (IsNotNullOrEmpty(notInMediStocks))
                    {
                        string bloodCodes = string.Join(",", notInMediStocks);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBlood_KhongThuocKho, bloodCodes);
                        return false;
                    }

                    List<string> lockBloods = bls.Where(o => o.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).Select(s => s.BLOOD_CODE).ToList();
                    if (IsNotNullOrEmpty(lockBloods))
                    {
                        string bloodCodes = string.Join(",", lockBloods);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBlood_CacTuiMauSauDangTamKhoa, bloodCodes);
                        return false;
                    }

                    if (!this.hisBloodLock.LockList(bloodIds))
                    {
                        return false;
                    }
                    bls.ForEach(o => o.IS_ACTIVE = Constant.IS_FALSE); // Neu khoa thanh cong thi cap nhat lai danh sach thuoc
                    List<HIS_EXP_MEST_BLOOD> data = new List<HIS_EXP_MEST_BLOOD>();

                    HIS_TREATMENT treatment = null;
                    
                    //Trong truong hop bat cau hinh lay theo chinh sach gia thi lay them thông tin ho so dieu tri de phuc vu lay chinh sach gia
                    //Chi xu ly trong truong hop bat cau hinh de khong anh huong hieu nang
                    if (HisExpMestCFG.IS_BLOOD_EXP_PRICE_OPTION && expMest.TDL_TREATMENT_ID.HasValue)
                    {
                        treatment = new HisTreatmentGet().GetById(expMest.TDL_TREATMENT_ID.Value);
                    }

                    //Duyet theo y/c cua client de tao ra exp_mest_blood tuong ung
                    foreach (ExpBloodSDO sdo in bloods)
                    {
                        V_HIS_BLOOD blood = bls.Where(o => o.ID == sdo.BloodId).FirstOrDefault();
                        if (blood == null)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_TuiMauDuocChonKhongKhaDung);
                            return false;
                        }
                        HIS_EXP_MEST_BLOOD exp = new HIS_EXP_MEST_BLOOD();
                        exp.EXP_MEST_ID = expMest.ID;
                        exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        exp.TDL_BLOOD_TYPE_ID = blood.BLOOD_TYPE_ID;
                        exp.TDL_SERVICE_REQ_ID = expMest.SERVICE_REQ_ID;
                        exp.TDL_TREATMENT_ID = expMest.TDL_TREATMENT_ID;
                        exp.BLOOD_ID = sdo.BloodId;
                        exp.NUM_ORDER = sdo.NumOrder;
                        
                        exp.PRICE = blood.IMP_PRICE; //mau thi gia ban luon bang gia nhap
                        exp.VAT_RATIO = blood.IMP_VAT_RATIO; //mau thi gia ban luon bang gia nhap
                        exp.DESCRIPTION = sdo.Description;
                        exp.APPROVAL_LOGINNAME = loginname;
                        exp.APPROVAL_TIME = approvalTime ?? 0;
                        exp.APPROVAL_USERNAME = username;
                        exp.ANTI_GLOBULIN_ENVI = sdo.AntiGlobulinEnvi;
                        exp.SALT_ENVI = sdo.SaltEnvi;
                        exp.AC_SELF_ENVIDENCE = sdo.AcSelfEnvidence;
                        exp.AC_SELF_ENVIDENCE_SECOND = sdo.AcSelfEnvidenceSecond;
                        exp.SALT_ENVI_TWO = sdo.SaltEnviTwo;
                        exp.ANTI_GLOBULIN_ENVI_TWO = sdo.AntiGlobulinEnviTwo;
                        exp.PATIENT_BLOOD_ABO_CODE = sdo.PatientBloodAboCode;
                        exp.PATIENT_BLOOD_RH_CODE = sdo.PatientBloodRhCode;

                        if (sdo.ExpMestBltyReqId.HasValue)
                        {
                            HIS_EXP_MEST_BLTY_REQ bltyReq = hisExpMestBltyReqs != null ? hisExpMestBltyReqs.FirstOrDefault(o => o.ID == sdo.ExpMestBltyReqId.Value) : null;
                            if (bltyReq == null)
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                                throw new Exception("Khong lay duoc HIS_EXP_MEST_BLTY_REQ theo ExpMestBltyReqId: " + sdo.ExpMestBltyReqId);
                            }
                            exp.EXP_MEST_BLTY_REQ_ID = bltyReq.ID;
                            exp.PATIENT_TYPE_ID = bltyReq.PATIENT_TYPE_ID;
                            exp.IS_OUT_PARENT_FEE = bltyReq.IS_OUT_PARENT_FEE;
                            exp.SERE_SERV_PARENT_ID = bltyReq.SERE_SERV_PARENT_ID;
                        }

                        //Neu co thiet lap gia mau lay theo chinh sach gia thi xu ly de cap nhat lai gia ban theo chinh sach gia
                        if (HisExpMestCFG.IS_BLOOD_EXP_PRICE_OPTION)
                        {
                            //Lay thong tin chinh sach gia duoc ap dung cho dich vu tuong ung voi loai mau
                            V_HIS_SERVICE_PATY appliedServicePaty = ServicePatyUtil.GetApplied(HisServicePatyCFG.DATA, treatment.BRANCH_ID, null, null, null, expMest.TDL_INTRUCTION_TIME.Value, treatment.IN_TIME, blood.SERVICE_ID, exp.PATIENT_TYPE_ID.Value, null);

                            //Neu khong ton tai gia thi hien thi thong bao loi, neu co thi gan lai gia theo chinh sach gia vua lay duoc
                            if (appliedServicePaty == null)
                            {
                                HIS_PATIENT_TYPE hisPatientType = HisPatientTypeCFG.DATA.Where(o => o.ID == exp.PATIENT_TYPE_ID.Value).FirstOrDefault();
                                V_HIS_SERVICE hisService = HisServiceCFG.DATA_VIEW.Where(o => o.ID == blood.SERVICE_ID).FirstOrDefault();
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_KhongTonTaiDuLieuPhuHop, hisService.SERVICE_NAME, hisService.SERVICE_CODE, hisPatientType.PATIENT_TYPE_NAME);
                                throw new Exception();
                            }
                            else
                            {
                                exp.PRICE = appliedServicePaty.PRICE;
                                exp.VAT_RATIO = appliedServicePaty.VAT_RATIO;
                            }
                        }
                        data.Add(exp);
                    }

                    if (!this.hisExpMestBloodCreate.CreateList(data))
                    {
                        throw new Exception("Tao exp_mest_blood that bai. Rollback du lieu");
                    }
                    resultData = data;
                    List<long> blIds = data.Select(o => o.BLOOD_ID).ToList();
                    List<string> blCodes = bls.Where(o => blIds.Contains(o.ID)).Select(s => s.BLOOD_CODE).ToList();
                    string codes = string.Join(",", blCodes);
                    exBloodCodes = codes;
                    string sql = !string.IsNullOrWhiteSpace(codes) ? string.Format("UPDATE HIS_EXP_MEST SET TDL_BLOOD_CODE = '{0}' WHERE ID = {1}", codes, expMest.ID) : string.Format("UPDATE HIS_EXP_MEST SET TDL_BLOOD_CODE = NULL WHERE ID = {0}", expMest.ID);
                    sqls.Add(sql);
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        internal void Rollback()
        {
            this.hisBloodLock.UnlockList(bloodIds);
            this.hisExpMestBloodCreate.RollbackData();
        }
    }
}
