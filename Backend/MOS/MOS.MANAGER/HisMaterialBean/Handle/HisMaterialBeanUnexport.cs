using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMaterial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMaterialBean.Handle
{
    /// <summary>
    /// Huy thuc xuat
    /// - Tao cac bean moi theo thong tin trong exp_mest_material va gan vao kho
    /// </summary>
    class HisMaterialBeanUnexport : BusinessBase
    {
        private HisMaterialBeanCreateSql hisMaterialBeanCreate;

        internal HisMaterialBeanUnexport()
            : base()
        {
            this.hisMaterialBeanCreate = new HisMaterialBeanCreateSql(param);
        }

        internal HisMaterialBeanUnexport(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.hisMaterialBeanCreate = new HisMaterialBeanCreateSql(param);
        }

        internal bool Run(List<HIS_EXP_MEST_MATERIAL> expMestMaterials, long mediStockId)
        {
            try
            {
                if (IsNotNullOrEmpty(expMestMaterials))
                {
                    List<long> materialIds = expMestMaterials.Select(o => o.MATERIAL_ID.Value).ToList();
                    List<HIS_MATERIAL> hisMaterials = new HisMaterialGet().GetByIds(materialIds);

                    List<HIS_MATERIAL_BEAN> toInserts = new List<HIS_MATERIAL_BEAN>();

                    foreach (HIS_EXP_MEST_MATERIAL exp in expMestMaterials)
                    {
                        HIS_MATERIAL_BEAN bean = new HIS_MATERIAL_BEAN();
                        bean.AMOUNT = exp.AMOUNT;
                        bean.MATERIAL_ID = exp.MATERIAL_ID.Value;
                        bean.MEDI_STOCK_ID = mediStockId;
                        bean.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE;
                        bean.EXP_MEST_MATERIAL_ID = exp.ID;
                        //gan lai thong tin tai su dung
                        bean.SERIAL_NUMBER = exp.SERIAL_NUMBER;
                        bean.REMAIN_REUSE_COUNT = exp.REMAIN_REUSE_COUNT;
                        HisMaterialBeanUtil.SetTdl(bean, hisMaterials.FirstOrDefault(o => o.ID == exp.MATERIAL_ID));
                        toInserts.Add(bean);
                    }

                    if (!this.hisMaterialBeanCreate.Run(toInserts))
                    {
                        throw new Exception("Tao bean de huy thuc xuat that bai");
                    }
                }
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
            this.hisMaterialBeanCreate.Rollback();
        }
    }
}
