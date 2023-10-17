using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Other.Create
{
    class MaterialReuseProcessor : BusinessBase
    {
        private HisMaterialBeanTakeBySerial hisMaterialBeanTakeBySerial;
        private HisExpMestMaterialCreate hisExpMestMaterialCreate;

        internal MaterialReuseProcessor()
            : base()
        {
            this.Init();
        }

        internal MaterialReuseProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        internal void Init()
        {
            this.hisMaterialBeanTakeBySerial = new HisMaterialBeanTakeBySerial(param);
            this.hisExpMestMaterialCreate = new HisExpMestMaterialCreate(param);
        }

        internal bool Run(List<PresMaterialBySerialNumberSDO> materialReuses, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MATERIAL> resultData, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(materialReuses) && expMest != null)
                {
                    Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> dicBean = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();
                    List<HIS_EXP_MEST_MATERIAL> expMestMaterials = this.TakeBeanAndMakeData(materialReuses, expMest, ref dicBean);

                    if (!this.hisExpMestMaterialCreate.CreateList(expMestMaterials))
                    {
                        throw new Exception("Tao exp_mest_material that bai. Rollback du lieu");
                    }

                    this.SqlUpdateBean(dicBean, ref sqls);

                    if (resultData == null) resultData = new List<HIS_EXP_MEST_MATERIAL>();
                    resultData.AddRange(expMestMaterials);
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }


        private List<HIS_EXP_MEST_MATERIAL> TakeBeanAndMakeData(List<PresMaterialBySerialNumberSDO> reqMaterials, HIS_EXP_MEST expMest, ref Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic)
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

        internal void Rollback()
        {
            try
            {
                this.hisMaterialBeanTakeBySerial.Rollback();
                this.hisExpMestMaterialCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
