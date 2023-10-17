using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBlood;
using MOS.MANAGER.HisBloodType;
using MOS.MANAGER.HisImpMestBlood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Manu.Create
{
    class BloodProcessor : BusinessBase
    {
        private HisBloodCreate hisBloodCreate;
        private HisImpMestBloodCreate hisImpMestBloodCreate;

        internal BloodProcessor(CommonParam param)
            : base(param)
        {
            this.hisBloodCreate = new HisBloodCreate(param);
            this.hisImpMestBloodCreate = new HisImpMestBloodCreate(param);
        }

        internal bool Run(List<HIS_BLOOD> manuBloods, HIS_IMP_MEST impMest, ref List<HIS_BLOOD> hisBloods)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(manuBloods))
                {
                    List<HIS_IMP_MEST_BLOOD> hisImpMestBloods = new List<HIS_IMP_MEST_BLOOD>();

                    HisImpMestCheck checker = new HisImpMestCheck(param);
                    bool valid = true;
                    valid = valid && checker.IsValidBloodExpiredDates(manuBloods);
                    if (!valid)
                    {
                        throw new Exception("Du lieu ko hop le.");
                    }

                    //Lay danh sach blood_type de lay thong tin gia noi bo (internal_price)
                    List<long> bloodTypeIds = manuBloods.Select(o => o.BLOOD_TYPE_ID).ToList();
                    List<HIS_BLOOD_TYPE> hisBloodTypes = new HisBloodTypeGet().GetByIds(bloodTypeIds);

                    List<HIS_BLOOD> output = new List<HIS_BLOOD>();
                    foreach (HIS_BLOOD blood in manuBloods)
                    {
                        HIS_BLOOD_TYPE bloodType = hisBloodTypes
                            .Where(o => o.ID == blood.BLOOD_TYPE_ID).SingleOrDefault();

                        //tu dong insert cac truong sau dua vao thong tin co trong danh muc
                        blood.INTERNAL_PRICE = bloodType.INTERNAL_PRICE;
                        blood.SUPPLIER_ID = impMest.SUPPLIER_ID;
                        blood.IS_PREGNANT = MOS.UTILITY.Constant.IS_TRUE;
                        output.Add(blood);
                    }

                    if (!this.hisBloodCreate.CreateList(output))
                    {
                        throw new Exception("Tao HIS_BLOOD that bai");
                    }

                    foreach (HIS_BLOOD blood in output)
                    {
                        HIS_IMP_MEST_BLOOD impMestBlood = new HIS_IMP_MEST_BLOOD();
                        impMestBlood.IMP_MEST_ID = impMest.ID;
                        impMestBlood.BLOOD_ID = blood.ID;
                        impMestBlood.PRICE = blood.IMP_PRICE;
                        impMestBlood.VAT_RATIO = blood.IMP_VAT_RATIO;
                        hisImpMestBloods.Add(impMestBlood);
                    }
                    if (!this.hisImpMestBloodCreate.CreateList(hisImpMestBloods))
                    {
                        throw new Exception("Tao HIS_IMP_MEST_BLOOD that bai");
                    }
                    hisBloods = output;
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            try
            {
                this.hisImpMestBloodCreate.RollbackData();
                this.hisBloodCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
