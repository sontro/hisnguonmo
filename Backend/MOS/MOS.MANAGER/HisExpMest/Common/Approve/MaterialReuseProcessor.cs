using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.MANAGER.HisMaterialPaty;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Approve
{
    class MaterialReuseProcessor : BusinessBase
    {
        private HisMaterialBeanTakeBySerial hisMaterialBeanTakeBySerial;
        private HisExpMestMaterialCreate hisExpMestMaterialCreate;
        private HisExpMestMatyReqIncreaseDdAmount hisExpMestMatyReqIncreaseDdAmount;

        internal MaterialReuseProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisMaterialBeanTakeBySerial = new HisMaterialBeanTakeBySerial(param);
            this.hisExpMestMatyReqIncreaseDdAmount = new HisExpMestMatyReqIncreaseDdAmount(param);
            this.hisExpMestMaterialCreate = new HisExpMestMaterialCreate(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATY_REQ> matyReqs, List<PresMaterialBySerialNumberSDO> materialSDOs, string loginname, string username, long approvalTime, bool isAuto, ref List<HIS_EXP_MEST_MATERIAL> expMestMaterials, ref List<string> sqls)
        {
            try
            {
                //Voi cac loai xuat co tao y/c thi luc duyet se tao ra thong tin lenh
                if (HisExpMestConstant.HAS_REQ_EXP_MEST_TYPE_IDs.Contains(expMest.EXP_MEST_TYPE_ID))
                {
                    this.CreateExpMestMaterial(expMest, matyReqs, materialSDOs, loginname, username, approvalTime, isAuto, ref expMestMaterials, ref sqls);
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

        private void CreateExpMestMaterial(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATY_REQ> matyReqs, List<PresMaterialBySerialNumberSDO> materials, string loginname, string username, long approvalTime, bool isAuto, ref List<HIS_EXP_MEST_MATERIAL> expMestMaterials, ref List<string> sqls)
        {

            if (IsNotNullOrEmpty(matyReqs) && IsNotNullOrEmpty(materials))
            {
                Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> dicBean = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();
                List<HIS_EXP_MEST_MATERIAL> expMestMates = this.TakeBeanAndMakeData(materials, expMest, loginname, username, approvalTime, ref dicBean);

                if (IsNotNullOrEmpty(expMestMates) && !this.hisExpMestMaterialCreate.CreateList(expMestMates))
                {
                    throw new Exception("Tao exp_mest_material that bai. Rollback du lieu");
                }

                this.SqlUpdateBean(dicBean, ref sqls);
                //neu duyet thanh cong thi cap nhat so luong da duyet vao maty_req
                this.ProcessUpdateDAmountRequest(matyReqs, expMestMates);
                if (expMestMaterials == null)
                {
                    expMestMaterials = new List<HIS_EXP_MEST_MATERIAL>();
                }
                expMestMaterials.AddRange(expMestMates);
            }
        }

        private List<HIS_EXP_MEST_MATERIAL> TakeBeanAndMakeData(List<PresMaterialBySerialNumberSDO> reqMaterials, HIS_EXP_MEST expMest, string loginname, string username, long approvalTime, ref Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic)
        {
            List<HIS_MATERIAL_BEAN> materialBeans = null;
            List<string> serialNumbers = reqMaterials != null ? reqMaterials.Select(o => o.SerialNumber).ToList() : null;
            if (!IsNotNullOrEmpty(serialNumbers))
            {
                return null;
            }
            if (!this.hisMaterialBeanTakeBySerial.Run(serialNumbers, expMest.MEDI_STOCK_ID, ref materialBeans))
            {
                throw new Exception("Tach bean that bai. Rollback du lieu");
            }

            List<HIS_EXP_MEST_MATERIAL> data = new List<HIS_EXP_MEST_MATERIAL>();

            //Duyet theo tung yeu cau de tao ra du lieu duyet (exp_mest_material) tuong ung
            foreach (PresMaterialBySerialNumberSDO req in reqMaterials)
            {
                List<HIS_MATERIAL_BEAN> beans = materialBeans.Where(o => o.SERIAL_NUMBER == req.SerialNumber).ToList();

                if (!IsNotNullOrEmpty(beans))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterialBean_VatTuKhongCoSan, req.SerialNumber);
                    throw new Exception("Ko lay duoc vat tu tuong ung voi so seri: " + req.SerialNumber);
                }

                if (beans.Count > 1)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterialBean_CoNhieuHon1BeanVoiSoSeri, req.SerialNumber);
                    throw new Exception("Co nhieu hon 1 ban ghi bean voi so seri: " + req.SerialNumber);
                }

                HIS_EXP_MEST_MATERIAL exp = new HIS_EXP_MEST_MATERIAL();
                exp.EXP_MEST_ID = expMest.ID;
                exp.AMOUNT = beans.Sum(o => o.AMOUNT);
                exp.TDL_MATERIAL_TYPE_ID = beans[0].TDL_MATERIAL_TYPE_ID;
                exp.MATERIAL_ID = beans[0].MATERIAL_ID;
                exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                exp.NUM_ORDER = req.NumOrder;
                exp.SERIAL_NUMBER = beans[0].SERIAL_NUMBER;
                exp.REMAIN_REUSE_COUNT = beans[0].REMAIN_REUSE_COUNT;
                exp.APPROVAL_LOGINNAME = loginname;
                exp.APPROVAL_TIME = approvalTime;
                exp.APPROVAL_USERNAME = username;
                exp.EXP_MEST_MATY_REQ_ID = req.ExpMestMatyReqId;
                exp.NUM_ORDER = req.NumOrder;

                materialDic.Add(exp, beans.Select(o => o.ID).ToList());
                data.Add(exp);
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
                    string query = DAOWorker.SqlDAO.AddInClause(beanIds, "UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 0, EXP_MEST_MATERIAL_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                    query = string.Format(query, expMestMaterial.ID);
                    sqls.Add(query);
                }
            }
        }

        private void ProcessUpdateDAmountRequest(List<HIS_EXP_MEST_MATY_REQ> matyReqs, List<HIS_EXP_MEST_MATERIAL> expMestMates)
        {
            if (IsNotNullOrEmpty(expMestMates))
            {
                Dictionary<long, decimal> increaseDic = new Dictionary<long, decimal>();

                //cap nhat so luong da duyet
                foreach (HIS_EXP_MEST_MATY_REQ req in matyReqs)
                {
                    decimal approvalAmount = expMestMates.Where(o => o.EXP_MEST_MATY_REQ_ID == req.ID).Sum(o => o.AMOUNT);
                    if (approvalAmount > 0)
                    {
                        increaseDic.Add(req.ID, approvalAmount);
                    }
                }
                if (IsNotNullOrEmpty(increaseDic))
                {
                    if (!this.hisExpMestMatyReqIncreaseDdAmount.Run(increaseDic))
                    {
                        throw new Exception("Cap nhat dd_amount that bai. Rollback");
                    }
                }
            }
        }

        internal void Rollback()
        {
            this.hisExpMestMatyReqIncreaseDdAmount.Rollback();
            this.hisExpMestMaterialCreate.RollbackData();
            this.hisMaterialBeanTakeBySerial.Rollback();
        }
    }
}
