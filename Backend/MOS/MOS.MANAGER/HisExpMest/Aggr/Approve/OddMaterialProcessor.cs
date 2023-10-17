using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisMaterialBean.Handle;
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
    class OddMaterialProcessor : BusinessBase
    {
        private HisExpMestCreate hisExpMestCreate;
        private HisExpMestUpdate hisExpMestUpdate;
        private HisExpMestMaterialMaker hisExpMestMaterialMaker;
        private List<HisMaterialBeanSplit> beanSpliters = new List<HisMaterialBeanSplit>();
        private HisExpMestMaterialCreate hisExpMestMaterialCreate;

        internal OddMaterialProcessor()
            : base()
        {
            this.Init();
        }

        internal OddMaterialProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
            this.hisExpMestMaterialMaker = new HisExpMestMaterialMaker(param);
            this.hisExpMestMaterialCreate = new HisExpMestMaterialCreate(param);
        }

        public bool Run(HIS_EXP_MEST aggrExpMest, List<HIS_EXP_MEST> childs, long requestRoomId, string loginname, string username, long? approvalTime, List<HIS_EXP_MEST_REASON> reasons, ref List<HIS_EXP_MEST_MATERIAL> resultMaterials, ref List<string> sqls)
        {
            try
            {
                //Lay toan du lieu lenh cua cac don noi tru
                List<HIS_EXP_MEST_MATERIAL> expMestMaterials = new HisExpMestMaterialGet().GetByAggrExpMestId(aggrExpMest.ID);

                //neu ko co thong tin thi ko xu ly gi
                if (!IsNotNullOrEmpty(expMestMaterials))
                {
                    return true;
                }

                if (HisMediStockCFG.ODD_MATERIAL_MANAGEMENT_OPTION == HisMediStockCFG.OddManagementOption.MANAGEMENT_ALLOCATE)
                {
                    this.ProcessOddAllocate(expMestMaterials, childs, requestRoomId, loginname, username, approvalTime, ref resultMaterials, ref sqls);
                }
                else if (HisMediStockCFG.ODD_MATERIAL_MANAGEMENT_OPTION == HisMediStockCFG.OddManagementOption.MANAGEMENT_PATIENT)
                {
                    this.ProcessOddPatient(expMestMaterials, childs, requestRoomId, loginname, username, approvalTime, ref resultMaterials, ref sqls);
                }
                else
                {
                    this.ProcessOdd(aggrExpMest, expMestMaterials, requestRoomId, loginname, username, approvalTime, reasons, ref sqls);
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

        private void ProcessOdd(HIS_EXP_MEST aggrExpMest, List<HIS_EXP_MEST_MATERIAL> expMestMaterials, long requestRoomId, string loginname, string username, long? approvalTime, List<HIS_EXP_MEST_REASON> reasons, ref List<string> sqls)
        {
            //Nguoc lai, gom nhom theo loai thuoc de xu ly:
            //Voi cac thuoc co le ==> tao them du lieu duyet phan bu
            var groups = expMestMaterials.GroupBy(o => o.TDL_MATERIAL_TYPE_ID);

            //Danh sach phan bu
            List<ExpMaterialTypeSDO> complements = new List<ExpMaterialTypeSDO>();

            //Duyet danh sach y/c thuoc
            foreach (var group in groups)
            {
                //Tong so luong thuoc y/c
                decimal totalAmount = group.Sum(o => o.AMOUNT);
                //Kiem tra xem co le ko
                decimal complementAmount = Math.Ceiling(totalAmount) - totalAmount;

                //Neu vat tu co le va he thong cau hinh khong cho phep xuat le thi tao phan bu
                HIS_MATERIAL_TYPE materialType = HisMaterialTypeCFG.DATA.Where(o => o.ID == group.Key).FirstOrDefault();
                if (complementAmount > 0 && materialType != null && materialType.IS_ALLOW_EXPORT_ODD != MOS.UTILITY.Constant.IS_TRUE)
                {
                    ExpMaterialTypeSDO complement = new ExpMaterialTypeSDO();
                    complement.Amount = complementAmount;
                    complement.MaterialTypeId = group.Key.Value;
                    complements.Add(complement);
                }
            }

            //Neu ton tai du lieu phan bu
            if (IsNotNullOrEmpty(complements))
            {
                HIS_EXP_MEST complementExp = null;

                //Neu cau hinh co quan ly thuoc le thi tao ra phieu bu thuoc le
                //xuat vao kho le cua khoa yeu cau
                if (HisMediStockCFG.ODD_MATERIAL_MANAGEMENT_OPTION == HisMediStockCFG.OddManagementOption.MANAGEMENT)
                {
                    V_HIS_MEDI_STOCK oddMediStock = HisMediStockCFG.DATA
                        .Where(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE && o.DEPARTMENT_ID == aggrExpMest.REQ_DEPARTMENT_ID && o.IS_ODD == MOS.UTILITY.Constant.IS_TRUE)
                        .FirstOrDefault();

                    if (oddMediStock == null)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_KhoaYeuCauKhongCoKhoLe);
                        throw new Exception("Kho yeu cau khong co kho le");
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

                List<HIS_EXP_MEST_MATERIAL> complementMaterials = null;
                long? expiredDate = HisMediStockCFG.DONT_PRES_EXPIRED_ITEM ? approvalTime : null;
                if (!this.hisExpMestMaterialMaker.Run(complements, complementExp, expiredDate, loginname, username, approvalTime, false, ref complementMaterials, ref sqls))
                {
                    throw new Exception("Tao phan bu that bai. Rollback du lieu");
                }
            }
        }

        private void ProcessOddAllocate(List<HIS_EXP_MEST_MATERIAL> expMestMaterials, List<HIS_EXP_MEST> childs, long requestRoomId, string loginname, string username, long? approvalTime, ref List<HIS_EXP_MEST_MATERIAL> resultMaterials, ref List<string> sqls)
        {
            Dictionary<HIS_EXP_MEST, List<ExpMaterialTypeSDO>> dicExpMaterial = new Dictionary<HIS_EXP_MEST, List<ExpMaterialTypeSDO>>();

            //Nguoc lai, gom nhom theo loai thuoc de xu ly:
            //Voi cac thuoc co le ==> tao them du lieu duyet phan bu
            var groups = expMestMaterials.GroupBy(o => o.TDL_MATERIAL_TYPE_ID);

            //Duyet danh sach y/c thuoc
            foreach (var group in groups)
            {
                //Tong so luong thuoc y/c
                decimal totalAmount = group.Sum(o => o.AMOUNT);
                //Kiem tra xem co le ko
                decimal complementAmount = Math.Ceiling(totalAmount) - totalAmount;

                //Neu vat tu co le va he thong cau hinh khong cho phep xuat le thi tao phan bu
                HIS_MATERIAL_TYPE materialType = HisMaterialTypeCFG.DATA.Where(o => o.ID == group.Key).FirstOrDefault();
                if (complementAmount > 0 && materialType != null && materialType.IS_ALLOW_EXPORT_ODD != MOS.UTILITY.Constant.IS_TRUE)
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
                    ExpMaterialTypeSDO fSdo = new ExpMaterialTypeSDO();
                    fSdo.Amount = complementAmount - (unitAmount * (decimal)(count - 1));
                    fSdo.MaterialTypeId = group.Key ?? 0;
                    fSdo.IsNotPres = Constant.IS_TRUE;
                    fSdo.PatientTypeId = HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE;
                    if (!dicExpMaterial.ContainsKey(first)) dicExpMaterial[first] = new List<ExpMaterialTypeSDO>();
                    dicExpMaterial[first].Add(fSdo);

                    foreach (var exp in expMestOdds)
                    {
                        if (exp.ID == first.ID || unitAmount <= 0) continue;
                        ExpMaterialTypeSDO sdo = new ExpMaterialTypeSDO();
                        sdo.Amount = unitAmount;
                        sdo.MaterialTypeId = group.Key ?? 0;
                        sdo.IsNotPres = Constant.IS_TRUE;
                        sdo.PatientTypeId = HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE;
                        if (!dicExpMaterial.ContainsKey(exp)) dicExpMaterial[exp] = new List<ExpMaterialTypeSDO>();
                        dicExpMaterial[exp].Add(sdo);
                    }
                }
            }

            List<HIS_EXP_MEST_MATERIAL> creates = new List<HIS_EXP_MEST_MATERIAL>();
            Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();
            if (dicExpMaterial.Count > 0)
            {
                foreach (var dic in dicExpMaterial)
                {
                    if (!IsNotNullOrEmpty(dic.Value)) continue;
                    List<HIS_EXP_MEST_MATERIAL> rsDatas = this.SplitBeanAndMakeData(dic.Value, dic.Key, loginname, username, approvalTime, ref materialDic);
                    creates.AddRange(rsDatas);
                }

                if (IsNotNullOrEmpty(creates) && !this.hisExpMestMaterialCreate.CreateList(creates))
                {
                    throw new Exception("Tao exp_mest_material that bai. Rollback du lieu");
                }

                this.SqlUpdateBean(materialDic, ref sqls);

                resultMaterials = creates;
            }
        }

        /// <summary>
        /// Phan le se gan cho 1 BN
        /// </summary>
        /// <param name="expMestMaterials"></param>
        /// <param name="childs"></param>
        /// <param name="requestRoomId"></param>
        /// <param name="loginname"></param>
        /// <param name="username"></param>
        /// <param name="approvalTime"></param>
        /// <param name="resultMaterials"></param>
        /// <param name="sqls"></param>
        private void ProcessOddPatient(List<HIS_EXP_MEST_MATERIAL> expMestMaterials, List<HIS_EXP_MEST> childs, long requestRoomId, string loginname, string username, long? approvalTime, ref List<HIS_EXP_MEST_MATERIAL> resultMaterials, ref List<string> sqls)
        {
            Dictionary<HIS_EXP_MEST, List<ExpMaterialTypeSDO>> dicExpMaterial = new Dictionary<HIS_EXP_MEST, List<ExpMaterialTypeSDO>>();

            //Nguoc lai, gom nhom theo loai thuoc de xu ly:
            //Voi cac thuoc co le ==> tao them du lieu duyet phan bu
            var groups = expMestMaterials.GroupBy(o => o.TDL_MATERIAL_TYPE_ID);

            //Duyet danh sach y/c thuoc
            foreach (var group in groups)
            {
                //Tong so luong thuoc y/c
                decimal totalAmount = group.Sum(o => o.AMOUNT);
                //Kiem tra xem co le ko
                decimal complementAmount = Math.Ceiling(totalAmount) - totalAmount;

                //Neu vat tu co le va he thong cau hinh khong cho phep xuat le thi tao phan bu
                HIS_MATERIAL_TYPE materialType = HisMaterialTypeCFG.DATA.Where(o => o.ID == group.Key).FirstOrDefault();
                if (complementAmount > 0 && materialType != null && materialType.IS_ALLOW_EXPORT_ODD != MOS.UTILITY.Constant.IS_TRUE)
                {
                    List<HIS_EXP_MEST> expMestOdds = new List<HIS_EXP_MEST>();

                    this.CheckOddExpMest(childs, group.ToList(), ref expMestOdds);

                    if (!IsNotNullOrEmpty(expMestOdds))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        throw new Exception("CheckOddExpMest cho ra expMestOdds empty");
                    }

                    HIS_EXP_MEST first = expMestOdds.OrderByDescending(o => o.ID).FirstOrDefault();
                    HIS_EXP_MEST_MATERIAL emm = expMestMaterials.Where(o => o.EXP_MEST_ID == first.ID && o.TDL_MATERIAL_TYPE_ID == group.Key).FirstOrDefault();
                    ExpMaterialTypeSDO fSdo = new ExpMaterialTypeSDO();
                    fSdo.Amount = complementAmount;
                    fSdo.MaterialTypeId = group.Key ?? 0;
                    fSdo.IsNotPres = Constant.IS_TRUE;
                    fSdo.PatientTypeId = emm.PATIENT_TYPE_ID;

                    if (!dicExpMaterial.ContainsKey(first))
                    {
                        dicExpMaterial[first] = new List<ExpMaterialTypeSDO>();
                    }
                    dicExpMaterial[first].Add(fSdo);
                }
            }

            List<HIS_EXP_MEST_MATERIAL> creates = new List<HIS_EXP_MEST_MATERIAL>();
            Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();
            if (dicExpMaterial.Count > 0)
            {
                foreach (var dic in dicExpMaterial)
                {
                    if (!IsNotNullOrEmpty(dic.Value)) continue;
                    List<HIS_EXP_MEST_MATERIAL> rsDatas = this.SplitBeanAndMakeData(dic.Value, dic.Key, loginname, username, approvalTime, ref materialDic);
                    creates.AddRange(rsDatas);
                }

                if (IsNotNullOrEmpty(creates) && !this.hisExpMestMaterialCreate.CreateList(creates))
                {
                    throw new Exception("Tao exp_mest_material that bai. Rollback du lieu");
                }

                this.SqlUpdateBean(materialDic, ref sqls);

                resultMaterials = creates;
            }
        }

        private void CheckOddExpMest(List<HIS_EXP_MEST> childs, List<HIS_EXP_MEST_MATERIAL> listMaterials, ref List<HIS_EXP_MEST> expMestOdds)
        {
            var groups = listMaterials.GroupBy(g => g.TDL_TREATMENT_ID).ToList();
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
        /// Tach bean theo ReqMaterialData va tao ra exp_mest_material tuong ung
        /// </summary>
        /// <param name="toSplits">Danh sach ReqMaterialData dam bao ko co material_type_id nao trung nhau va cung thuoc 1 medi_stock_id</param>
        /// <param name="expMest"></param>
        /// <returns></returns>
        private List<HIS_EXP_MEST_MATERIAL> SplitBeanAndMakeData(List<ExpMaterialTypeSDO> toSplits, HIS_EXP_MEST expMest, string loginname, string usernname, long? approvalTime, ref Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic)
        {
            List<HIS_MATERIAL_BEAN> materialBeans = null;
            List<HIS_MATERIAL_PATY> materialPaties = null;

            HisMaterialBeanSplit spliter = new HisMaterialBeanSplit(param);
            this.beanSpliters.Add(spliter);

            long? expiredDate = HisMediStockCFG.DONT_PRES_EXPIRED_ITEM ? approvalTime : null;

            if (!spliter.SplitByMaterialType(toSplits, expMest.MEDI_STOCK_ID, expiredDate, null, null, ref materialBeans, ref materialPaties))
            {
                throw new Exception("Tach bean that bai. Rollback du lieu");
            }

            List<HIS_EXP_MEST_MATERIAL> data = new List<HIS_EXP_MEST_MATERIAL>();

            //Duyet theo tung yeu cau de tao ra du lieu duyet (exp_mest_material) tuong ung
            foreach (ExpMaterialTypeSDO req in toSplits)
            {
                //Do danh sach mety_req dam bao ko co material_type_id nao trung nhau ==> dung material_type_id de lay ra cac bean tuong ung
                List<HIS_MATERIAL_BEAN> reqBeans = materialBeans
                    .Where(o => o.TDL_MATERIAL_TYPE_ID == req.MaterialTypeId)
                    .ToList();

                if (!IsNotNullOrEmpty(reqBeans))
                {
                    throw new Exception("Ko tach duoc bean tuong ung voi MaterialTypeId:" + req.MaterialTypeId);
                }

                List<HIS_EXP_MEST_MATERIAL> materials = new List<HIS_EXP_MEST_MATERIAL>();
                var group = reqBeans.GroupBy(o => new { o.MATERIAL_ID, o.TDL_MATERIAL_IMP_PRICE, o.TDL_MATERIAL_IMP_VAT_RATIO });
                foreach (var tmp in group)
                {
                    List<HIS_MATERIAL_BEAN> beans = tmp.ToList();
                    HIS_EXP_MEST_MATERIAL exp = new HIS_EXP_MEST_MATERIAL();
                    exp.EXP_MEST_ID = expMest.ID;
                    exp.TDL_SERVICE_REQ_ID = expMest.SERVICE_REQ_ID;
                    exp.TDL_TREATMENT_ID = expMest.TDL_TREATMENT_ID;
                    exp.TDL_AGGR_EXP_MEST_ID = expMest.AGGR_EXP_MEST_ID;
                    exp.AMOUNT = beans.Sum(o => o.AMOUNT);
                    exp.TDL_MATERIAL_TYPE_ID = req.MaterialTypeId;
                    exp.MATERIAL_ID = tmp.Key.MATERIAL_ID;
                    exp.IS_NOT_PRES = req.IsNotPres;
                    exp.APPROVAL_LOGINNAME = loginname;
                    exp.APPROVAL_USERNAME = usernname;
                    exp.APPROVAL_TIME = approvalTime;

                    //Neu ban bang gia nhap
                    if (beans[0].TDL_IS_SALE_EQUAL_IMP_PRICE == MOS.UTILITY.Constant.IS_TRUE)
                    {
                        exp.PRICE = tmp.Key.TDL_MATERIAL_IMP_PRICE;
                        exp.VAT_RATIO = tmp.Key.TDL_MATERIAL_IMP_VAT_RATIO;
                    }
                    else
                    {
                        HIS_MATERIAL_PATY paty = IsNotNullOrEmpty(materialPaties) ? materialPaties.Where(o => o.PATIENT_TYPE_ID == req.PatientTypeId && o.MATERIAL_ID == tmp.Key.MATERIAL_ID).FirstOrDefault() : null;
                        if (paty == null)
                        {
                            throw new Exception("Khong ton tai chinh sach gia tuong ung voi material_id: " + tmp.Key.MATERIAL_ID + "va patient_type_id: " + req.PatientTypeId);
                        }
                        exp.PRICE = paty.EXP_PRICE;
                        exp.VAT_RATIO = paty.EXP_VAT_RATIO;
                    }

                    exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                    exp.NUM_ORDER = req.NumOrder;
                    exp.PATIENT_TYPE_ID = req.PatientTypeId;
                    exp.IS_CREATED_BY_APPROVAL = Constant.IS_TRUE;

                    materials.Add(exp);

                    materialDic.Add(exp, beans.Select(o => o.ID).ToList());
                }
                data.AddRange(materials);
            }
            return data;
        }


        private void SqlUpdateBean(Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> useBeandIdDic, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(useBeandIdDic))
            {
                foreach (HIS_EXP_MEST_MATERIAL expMestMaterial in useBeandIdDic.Keys)
                {
                    List<long> beanIds = useBeandIdDic[expMestMaterial];
                    //cap nhat danh sach cac bean da dung
                    string query = DAOWorker.SqlDAO.AddInClause(beanIds, "UPDATE HIS_MATERIAL_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 0, EXP_MEST_MATERIAL_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                    query = string.Format(query, expMestMaterial.ID);
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
            this.hisExpMestMaterialCreate.RollbackData();
            this.hisExpMestMaterialMaker.Rollback();
            this.hisExpMestCreate.RollbackData();
        }
    }
}
