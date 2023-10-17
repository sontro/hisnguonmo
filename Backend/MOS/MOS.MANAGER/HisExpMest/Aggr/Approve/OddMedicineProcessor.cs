using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Aggr.Approve
{
    /// <summary>
    /// Xu ly nghiep vu thuoc le khi duyet phieu linh. Khi duyet phieu linh, neu so luong thuoc bi le
    /// </summary>
    class OddMedicineProcessor : BusinessBase
    {
        private HisExpMestMedicineMaker hisExpMestMedicineMaker;
        private HisExpMestCreate hisExpMestCreate;
        private HisExpMestUpdate hisExpMestUpdate;

        private List<HisMedicineBeanSplit> beanSpliters = new List<HisMedicineBeanSplit>();
        private HisExpMestMedicineCreate hisExpMestMedicineCreate;

        internal OddMedicineProcessor()
            : base()
        {
            this.Init();
        }

        internal OddMedicineProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestMedicineMaker = new HisExpMestMedicineMaker(param);
            this.hisExpMestCreate = new HisExpMestCreate(param);
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
            this.hisExpMestMedicineCreate = new HisExpMestMedicineCreate(param);
        }

        public bool Run(HIS_EXP_MEST aggrExpMest, List<HIS_EXP_MEST> childs, long requestRoomId, string loginname, string username, long? approvalTime, List<HIS_EXP_MEST_REASON> reasons, ref List<HIS_EXP_MEST_MEDICINE> resultMedicines, ref List<string> sqls)
        {
            try
            {
                //Lay toan du lieu lenh cua cac don noi tru
                List<HIS_EXP_MEST_MEDICINE> expMestMedicines = new HisExpMestMedicineGet().GetByAggrExpMestId(aggrExpMest.ID);

                //neu ko co thong tin thi ko xu ly gi
                if (!IsNotNullOrEmpty(expMestMedicines))
                {
                    return true;
                }

                if (HisMediStockCFG.ODD_MEDICINE_MANAGEMENT_OPTION == HisMediStockCFG.OddManagementOption.MANAGEMENT_ALLOCATE)
                {
                    this.ProcessOddAllocate(expMestMedicines, childs, requestRoomId, loginname, username, approvalTime, ref resultMedicines, ref sqls);
                }
                else if (HisMediStockCFG.ODD_MEDICINE_MANAGEMENT_OPTION == HisMediStockCFG.OddManagementOption.MANAGEMENT_PATIENT)
                {
                    this.ProcessOddPatient(expMestMedicines, childs, requestRoomId, loginname, username, approvalTime, ref resultMedicines, ref sqls);
                }
                else
                {
                    this.ProcessOdd(expMestMedicines, aggrExpMest, requestRoomId, loginname, username, approvalTime, reasons,ref sqls);
                }

                return true;
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                return false;
            }
        }

        private void ProcessOdd(List<HIS_EXP_MEST_MEDICINE> expMestMedicines, HIS_EXP_MEST aggrExpMest, long requestRoomId, string loginname, string username, long? approvalTime, List<HIS_EXP_MEST_REASON> reasons, ref List<string> sqls)
        {

            //Nguoc lai, gom nhom theo loai thuoc de xu ly:
            //Voi cac thuoc co le ==> tao them du lieu duyet phan bu
            var groups = expMestMedicines.GroupBy(o => o.TDL_MEDICINE_TYPE_ID);

            //Danh sach phan bu
            List<ExpMedicineTypeSDO> complements = new List<ExpMedicineTypeSDO>();

            //Duyet danh sach y/c thuoc
            foreach (var group in groups)
            {
                //Tong so luong thuoc y/c
                decimal totalAmount = group.Sum(o => o.AMOUNT);
                //Kiem tra xem co le ko
                decimal complementAmount = Math.Ceiling(totalAmount) - totalAmount;

                //Neu thuoc co le va he thong cau hinh khong cho phep xuat le thi tao phan bu
                HIS_MEDICINE_TYPE medicineType = HisMedicineTypeCFG.DATA.Where(o => o.ID == group.Key).FirstOrDefault();
                if (complementAmount > 0 && medicineType != null && medicineType.IS_ALLOW_EXPORT_ODD != MOS.UTILITY.Constant.IS_TRUE)
                {
                    ExpMedicineTypeSDO complement = new ExpMedicineTypeSDO();
                    complement.Amount = complementAmount;
                    complement.MedicineTypeId = group.Key.Value;
                    complements.Add(complement);
                }
            }

            //Neu ton tai du lieu phan bu
            if (IsNotNullOrEmpty(complements))
            {
                HIS_EXP_MEST complementExp = null;

                //Neu cau hinh co quan ly thuoc le thi tao ra phieu bu thuoc le
                //xuat vao kho le cua khoa yeu cau
                if (HisMediStockCFG.ODD_MEDICINE_MANAGEMENT_OPTION == HisMediStockCFG.OddManagementOption.MANAGEMENT)
                {
                    V_HIS_MEDI_STOCK oddMediStock = HisMediStockCFG.DATA
                        .Where(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE && o.DEPARTMENT_ID == aggrExpMest.REQ_DEPARTMENT_ID && o.IS_ODD == MOS.UTILITY.Constant.IS_TRUE)
                        .FirstOrDefault();

                    if (oddMediStock == null)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_KhoaYeuCauKhongCoKhoLe);
                        throw new Exception("Khoa yeu cau khong co kho le");
                    }
                    complementExp = new HIS_EXP_MEST();
                    complementExp.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BL;
                    complementExp.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE;
                    complementExp.MEDI_STOCK_ID = aggrExpMest.MEDI_STOCK_ID;
                    complementExp.IMP_MEDI_STOCK_ID = oddMediStock.ID;
                    complementExp.REQ_ROOM_ID = requestRoomId;
                    complementExp.AGGR_EXP_MEST_ID = aggrExpMest.ID;
                    complementExp.HAS_NOT_PRES = Constant.IS_TRUE; //Danh dau phieu co thuoc lam tron (ko phai do bs ke)
                    if (HisExpMestCFG.IS_REASON_REQUIRED && IsNotNullOrEmpty(reasons))
                    {
                        complementExp.EXP_MEST_REASON_ID = reasons.OrderBy(o => o.ID).FirstOrDefault().ID;
                    }

                    if (!this.hisExpMestCreate.Create(complementExp))
                    {
                        throw new Exception("Tao phieu bu le that bai. Rollback du lieu");
                    }
                }
                //Neu cau hinh ko quan ly thuoc le thi du lieu bu gan truc tiep vao phieu linh
                else
                {
                    complementExp = aggrExpMest;
                    if (HisExpMestCFG.IS_REASON_REQUIRED && IsNotNullOrEmpty(reasons))
                    {
                        Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                        HIS_EXP_MEST beforeUpdate = Mapper.Map<HIS_EXP_MEST>(complementExp);

                        complementExp.EXP_MEST_REASON_ID = reasons.OrderBy(o => o.ID).FirstOrDefault().ID;
                        if (!this.hisExpMestUpdate.Update(complementExp, beforeUpdate))
                        {
                            throw new Exception("Cap nhat ly do that bai. Rollback du lieu");
                        }
                    }
                }

                List<HIS_EXP_MEST_MEDICINE> complementMedicines = null;

                long? expiredDate = HisMediStockCFG.DONT_PRES_EXPIRED_ITEM ? approvalTime : null;

                if (!this.hisExpMestMedicineMaker.Run(complements, complementExp, expiredDate, loginname, username, approvalTime, false, ref complementMedicines, ref sqls))
                {
                    throw new Exception("Tao phan bu that bai. Rollback du lieu");
                }
            }
        }

        public void ProcessOddAllocate(List<HIS_EXP_MEST_MEDICINE> expMestMedicines, List<HIS_EXP_MEST> childs, long requestRoomId, string loginname, string username, long? approvalTime, ref List<HIS_EXP_MEST_MEDICINE> resultMedicines, ref List<string> sqls)
        {
            Dictionary<HIS_EXP_MEST, List<ExpMedicineTypeSDO>> dicExpMedicine = new Dictionary<HIS_EXP_MEST, List<ExpMedicineTypeSDO>>();
            //Nguoc lai, gom nhom theo loai thuoc de xu ly:
            //Voi cac thuoc co le ==> tao them du lieu duyet phan bu
            var groups = expMestMedicines.GroupBy(o => o.TDL_MEDICINE_TYPE_ID);

            //Danh sach phan bu
            List<ExpMedicineTypeSDO> complements = new List<ExpMedicineTypeSDO>();

            //Duyet danh sach y/c thuoc
            foreach (var group in groups)
            {
                //Tong so luong thuoc y/c
                decimal totalAmount = group.Sum(o => o.AMOUNT);
                //Kiem tra xem co le ko
                decimal complementAmount = Math.Ceiling(totalAmount) - totalAmount;

                //Neu thuoc co le va he thong cau hinh khong cho phep xuat le thi tao phan bu
                HIS_MEDICINE_TYPE medicineType = HisMedicineTypeCFG.DATA.Where(o => o.ID == group.Key).FirstOrDefault();
                if (complementAmount > 0 && medicineType != null && medicineType.IS_ALLOW_EXPORT_ODD != MOS.UTILITY.Constant.IS_TRUE)
                {
                    List<HIS_EXP_MEST> expMestOdds = new List<HIS_EXP_MEST>();
                    this.CheckOddExpMest(childs, group.ToList(), ref expMestOdds);
                    if (!IsNotNullOrEmpty(expMestOdds))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        throw new Exception("CheckOddExpMest cho ra expMestOdds empty");
                    }
                    decimal count = (decimal)expMestOdds.Count;
                    decimal unitAmount = (Math.Floor((complementAmount / (decimal)count) * Constant.DECIMAL_PRECISION_AMOUNT) / Constant.DECIMAL_PRECISION_AMOUNT);

                    HIS_EXP_MEST first = expMestOdds.FirstOrDefault();
                    ExpMedicineTypeSDO fSdo = new ExpMedicineTypeSDO();
                    fSdo.Amount = complementAmount - (unitAmount * (decimal)(count - 1));                    
                    fSdo.MedicineTypeId = group.Key ?? 0;
                    fSdo.IsNotPres = Constant.IS_TRUE;
                    fSdo.PatientTypeId = HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE;
                    if (!dicExpMedicine.ContainsKey(first)) dicExpMedicine[first] = new List<ExpMedicineTypeSDO>();
                    dicExpMedicine[first].Add(fSdo);

                    foreach (var exp in expMestOdds)
                    {
                        if (exp.ID == first.ID || unitAmount <= 0) continue;
                        ExpMedicineTypeSDO sdo = new ExpMedicineTypeSDO();
                        sdo.Amount = unitAmount;
                        sdo.MedicineTypeId = group.Key ?? 0;
                        sdo.IsNotPres = Constant.IS_TRUE;
                        sdo.PatientTypeId = HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE;
                        if (!dicExpMedicine.ContainsKey(exp)) dicExpMedicine[exp] = new List<ExpMedicineTypeSDO>();
                        dicExpMedicine[exp].Add(sdo);
                    }
                }
            }

            List<HIS_EXP_MEST_MEDICINE> creates = new List<HIS_EXP_MEST_MEDICINE>();
            Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();

            if (dicExpMedicine.Count > 0)
            {
                foreach (var dic in dicExpMedicine)
                {

                    if (!IsNotNullOrEmpty(dic.Value)) continue;
                    List<HIS_EXP_MEST_MEDICINE> rsDatas = this.SplitBeanAndMakeData(dic.Value, dic.Key, loginname, username, approvalTime, ref medicineDic);
                    creates.AddRange(rsDatas);
                }
                if (IsNotNullOrEmpty(creates) && !this.hisExpMestMedicineCreate.CreateList(creates))
                {
                    throw new Exception("Tao exp_mest_medicine that bai. Rollback du lieu");
                }

                this.SqlUpdateBean(medicineDic, ref sqls);

                resultMedicines = creates;
            }
        }

        /// <summary>
        /// Phan le se gan cho 1 BN
        /// </summary>
        /// <param name="expMestMedicines"></param>
        /// <param name="childs"></param>
        /// <param name="requestRoomId"></param>
        /// <param name="loginname"></param>
        /// <param name="username"></param>
        /// <param name="approvalTime"></param>
        /// <param name="resultMedicines"></param>
        /// <param name="sqls"></param>
        public void ProcessOddPatient(List<HIS_EXP_MEST_MEDICINE> expMestMedicines, List<HIS_EXP_MEST> childs, long requestRoomId, string loginname, string username, long? approvalTime, ref List<HIS_EXP_MEST_MEDICINE> resultMedicines, ref List<string> sqls)
        {
            Dictionary<HIS_EXP_MEST, List<ExpMedicineTypeSDO>> dicExpMedicine = new Dictionary<HIS_EXP_MEST, List<ExpMedicineTypeSDO>>();
            //Nguoc lai, gom nhom theo loai thuoc de xu ly:
            //Voi cac thuoc co le ==> tao them du lieu duyet phan bu
            var groups = expMestMedicines.GroupBy(o => o.TDL_MEDICINE_TYPE_ID);

            //Danh sach phan bu
            List<ExpMedicineTypeSDO> complements = new List<ExpMedicineTypeSDO>();

            //Duyet danh sach y/c thuoc
            foreach (var group in groups)
            {
                //Tong so luong thuoc y/c
                decimal totalAmount = group.Sum(o => o.AMOUNT);
                //Kiem tra xem co le ko
                decimal complementAmount = Math.Ceiling(totalAmount) - totalAmount;

                //Neu thuoc co le va he thong cau hinh khong cho phep xuat le thi tao phan bu
                HIS_MEDICINE_TYPE medicineType = HisMedicineTypeCFG.DATA.Where(o => o.ID == group.Key).FirstOrDefault();
                if (complementAmount > 0 && medicineType != null && medicineType.IS_ALLOW_EXPORT_ODD != MOS.UTILITY.Constant.IS_TRUE)
                {
                    List<HIS_EXP_MEST> expMestOdds = new List<HIS_EXP_MEST>();
                    
                    this.CheckOddExpMest(childs, group.ToList(), ref expMestOdds);

                    if (!IsNotNullOrEmpty(expMestOdds))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        throw new Exception("CheckOddExpMest cho ra expMestOdds empty");
                    }

                    HIS_EXP_MEST first = expMestOdds.OrderByDescending(o => o.ID).FirstOrDefault();
                    HIS_EXP_MEST_MEDICINE emm = expMestMedicines.Where(o => o.EXP_MEST_ID == first.ID && o.TDL_MEDICINE_TYPE_ID == group.Key).FirstOrDefault();
                    ExpMedicineTypeSDO fSdo = new ExpMedicineTypeSDO();
                    fSdo.Amount = complementAmount;
                    fSdo.MedicineTypeId = group.Key ?? 0;
                    fSdo.IsNotPres = Constant.IS_TRUE;
                    fSdo.PatientTypeId = emm.PATIENT_TYPE_ID;

                    if (!dicExpMedicine.ContainsKey(first))
                    {
                        dicExpMedicine[first] = new List<ExpMedicineTypeSDO>();
                    }
                    dicExpMedicine[first].Add(fSdo);
                }
            }

            List<HIS_EXP_MEST_MEDICINE> creates = new List<HIS_EXP_MEST_MEDICINE>();
            Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();

            if (dicExpMedicine.Count > 0)
            {
                foreach (var dic in dicExpMedicine)
                {

                    if (!IsNotNullOrEmpty(dic.Value)) continue;
                    List<HIS_EXP_MEST_MEDICINE> rsDatas = this.SplitBeanAndMakeData(dic.Value, dic.Key, loginname, username, approvalTime, ref medicineDic);
                    creates.AddRange(rsDatas);
                }
                if (IsNotNullOrEmpty(creates) && !this.hisExpMestMedicineCreate.CreateList(creates))
                {
                    throw new Exception("Tao exp_mest_medicine that bai. Rollback du lieu");
                }

                this.SqlUpdateBean(medicineDic, ref sqls);

                resultMedicines = creates;
            }
        }

        private void CheckOddExpMest(List<HIS_EXP_MEST> childs, List<HIS_EXP_MEST_MEDICINE> listMedicines, ref List<HIS_EXP_MEST> expMestOdds)
        {
            var groups = listMedicines.GroupBy(g => g.TDL_TREATMENT_ID).ToList();
            foreach (var group in groups)
            {
                //Tong so luong thuoc y/c
                decimal totalAmount = group.Sum(o => o.AMOUNT);
                //Kiem tra xem co le ko
                decimal complementAmount = Math.Ceiling(totalAmount) - totalAmount;
                if (complementAmount <= 0) continue;

                var groupByExpMests = group.ToList().GroupBy(g => g.EXP_MEST_ID).ToList();
                foreach (var g in groupByExpMests)
                {
                    //Tong so luong thuoc y/c
                    decimal amount = g.Sum(o => o.AMOUNT);
                    //Kiem tra xem co le ko
                    decimal complement = Math.Ceiling(totalAmount) - totalAmount;
                    if (complement <= 0) continue;

                    //Chi lay EXP_MEST dau tien va thoat vong lap chuyen sang benh nhan khac
                    expMestOdds.Add(childs.FirstOrDefault(o => o.ID == g.Key));
                    break;
                }
            }
        }


        /// <summary>
        /// Tach bean theo ReqMedicineData va tao ra exp_mest_medicine tuong ung
        /// </summary>
        /// <param name="toSplits">Danh sach ReqMedicineData dam bao ko co medicine_type_id nao trung nhau va cung thuoc 1 medi_stock_id</param>
        /// <param name="expMest"></param>
        /// <returns></returns>
        private List<HIS_EXP_MEST_MEDICINE> SplitBeanAndMakeData(List<ExpMedicineTypeSDO> toSplits, HIS_EXP_MEST expMest, string loginname, string usernname, long? approvalTime, ref Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic)
        {
            List<HIS_MEDICINE_BEAN> medicineBeans = null;
            List<HIS_MEDICINE_PATY> medicinePaties = null;

            HisMedicineBeanSplit spliter = new HisMedicineBeanSplit(param);
            this.beanSpliters.Add(spliter);

            long? expiredDate = HisMediStockCFG.DONT_PRES_EXPIRED_ITEM ? approvalTime : null;

            if (!spliter.SplitByMedicineType(toSplits, expMest.MEDI_STOCK_ID, expiredDate, null, null, ref medicineBeans, ref medicinePaties))
            {
                throw new Exception("Tach bean that bai. Rollback du lieu");
            }

            List<HIS_EXP_MEST_MEDICINE> data = new List<HIS_EXP_MEST_MEDICINE>();

            //Duyet theo tung yeu cau de tao ra du lieu duyet (exp_mest_medicine) tuong ung
            foreach (ExpMedicineTypeSDO req in toSplits)
            {
                //Do danh sach mety_req dam bao ko co medicine_type_id nao trung nhau ==> dung medicine_type_id de lay ra cac bean tuong ung
                //Neu ke theo lo thi can cu vao medicine_id
                List<HIS_MEDICINE_BEAN> reqBeans = medicineBeans
                    .Where(o => o.TDL_MEDICINE_TYPE_ID == req.MedicineTypeId)
                    .ToList();

                List<HIS_EXP_MEST_MEDICINE> medicines = new List<HIS_EXP_MEST_MEDICINE>();
                if (!IsNotNullOrEmpty(reqBeans))
                {
                    throw new Exception("Ko tach duoc bean tuong ung voi MedicineTypeId:" + req.MedicineTypeId);
                }

                var group = reqBeans.GroupBy(o => new { o.MEDICINE_ID, o.TDL_MEDICINE_IMP_PRICE, o.TDL_MEDICINE_IMP_VAT_RATIO });
                foreach (var tmp in group)
                {
                    List<HIS_MEDICINE_BEAN> beans = tmp.ToList();
                    HIS_EXP_MEST_MEDICINE exp = new HIS_EXP_MEST_MEDICINE();
                    exp.EXP_MEST_ID = expMest.ID;
                    exp.TDL_SERVICE_REQ_ID = expMest.SERVICE_REQ_ID;
                    exp.TDL_TREATMENT_ID = expMest.TDL_TREATMENT_ID;
                    exp.TDL_AGGR_EXP_MEST_ID = expMest.AGGR_EXP_MEST_ID;
                    exp.AMOUNT = beans.Sum(o => o.AMOUNT);
                    exp.TDL_MEDICINE_TYPE_ID = req.MedicineTypeId;
                    exp.MEDICINE_ID = tmp.Key.MEDICINE_ID;
                    exp.IS_NOT_PRES = req.IsNotPres;
                    exp.APPROVAL_LOGINNAME = loginname;
                    exp.APPROVAL_USERNAME = usernname;
                    exp.APPROVAL_TIME = approvalTime;
                    //Neu ban bang gia nhap
                    if (beans[0].TDL_IS_SALE_EQUAL_IMP_PRICE == MOS.UTILITY.Constant.IS_TRUE)
                    {
                        exp.PRICE = tmp.Key.TDL_MEDICINE_IMP_PRICE;
                        exp.VAT_RATIO = tmp.Key.TDL_MEDICINE_IMP_VAT_RATIO;
                    }
                    else
                    {
                        HIS_MEDICINE_PATY paty = IsNotNullOrEmpty(medicinePaties) ? medicinePaties.Where(o => o.PATIENT_TYPE_ID == req.PatientTypeId && o.MEDICINE_ID == tmp.Key.MEDICINE_ID).FirstOrDefault() : null;
                        if (paty == null)
                        {
                            throw new Exception("Khong ton tai chinh sach gia tuong ung voi medicine_id: " + tmp.Key.MEDICINE_ID + "va patient_type_id: " + req.PatientTypeId);
                        }
                        exp.PRICE = paty.EXP_PRICE;
                        exp.VAT_RATIO = paty.EXP_VAT_RATIO;
                    }

                    exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                    exp.NUM_ORDER = req.NumOrder;
                    exp.PATIENT_TYPE_ID = req.PatientTypeId;
                    exp.TUTORIAL = req.Tutorial;
                    exp.IS_CREATED_BY_APPROVAL = Constant.IS_TRUE;
                    medicines.Add(exp);
                    medicineDic.Add(exp, beans.Select(o => o.ID).ToList());
                }
                data.AddRange(medicines);
            }
            return data;
        }

        private void SqlUpdateBean(Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> useBeandIdDic, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(useBeandIdDic))
            {
                foreach (HIS_EXP_MEST_MEDICINE expMestMedicine in useBeandIdDic.Keys)
                {
                    List<long> beanIds = useBeandIdDic[expMestMedicine];
                    //cap nhat danh sach cac bean da dung
                    string query = DAOWorker.SqlDAO.AddInClause(beanIds, "UPDATE HIS_MEDICINE_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 0, EXP_MEST_MEDICINE_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                    query = string.Format(query, expMestMedicine.ID);
                    sqls.Add(query);
                }
            }
        }


        internal void Rollback()
        {
            if (IsNotNullOrEmpty(this.beanSpliters))
            {
                foreach (var spliter in this.beanSpliters)
                {
                    spliter.RollBack();
                }
            }
            this.hisExpMestMedicineCreate.RollbackData();
            this.hisExpMestMedicineMaker.Rollback();
            this.hisExpMestUpdate.RollbackData();
            this.hisExpMestCreate.RollbackData();
        }
    }
}
