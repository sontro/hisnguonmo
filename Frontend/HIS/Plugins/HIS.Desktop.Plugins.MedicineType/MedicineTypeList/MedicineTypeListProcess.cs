using DevExpress.XtraTreeList.Nodes;
using HIS.Desktop.Plugins.MedicineType.ADO;
using HIS.Desktop.Plugins.MedicineType.MedicineTypeList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineTypeList
{
    public class MedicineTypeListProcess
    {
        internal static void FillDataToControl(List<MedicineTypeADO> datas, UCMedcineTypeList control)
        {
            try
            {
                //if (control != null && datas != null)
                //{
                //    control.treeMedicineType.ClearNodes();
                //    TreeListNode parentForRootNodes = null;
                //    var listRoot = datas.Where(o => o.PARENT_ID == null);
                //    //var aml20 = datas.FirstOrDefault(o => o.MEDICINE_TYPE_CODE == "AML20");
                //    //var _1985 = datas.FirstOrDefault(o => o.ID == 03645);
                //    //var _am0017 = datas.FirstOrDefault(o => o.MEDICINE_TYPE_CODE == "AM001717");
                //    if (listRoot != null)
                //    {
                //        foreach (var medicineType in listRoot)
                //        {
                //            TreeListNode rootNode = control.treeMedicineType.AppendNode(
                //new object[] { medicineType.MEDICINE_TYPE_CODE, medicineType.MEDICINE_TYPE_NAME, medicineType.SERVICE_UNIT_NAME, medicineType.ACTIVE_INGR_BHYT_NAME, medicineType.CONCENTRA, medicineType.MANUFACTURER_NAME, medicineType.NATIONAL_NAME, Inventec.Common.Number.Convert.NumberToString(medicineType.IMP_PRICE ?? 0), medicineType.IMP_VAT_RATIO * 100, medicineType.HEIN_SERVICE_BHYT_NAME, medicineType.REGISTER_NUMBER, medicineType.MEDICINE_LINE_NAME, medicineType.MEDICINE_USE_FORM_NAME, medicineType.PACKING_TYPE_NAME, medicineType.IsAntibiotic, medicineType.IsAddictive, medicineType.IsNeurological, medicineType.ALERT_EXPIRED_DATE, medicineType.ALERT_MIN_IN_STOCK, medicineType.NUM_ORDER, medicineType.IsStopImp, medicineType, medicineType },
                //parentForRootNodes, medicineType);
                //            CreateChildNodeMedicineType(rootNode, medicineType, datas, control);
                //        }

                //        control.treeMedicineType.ExpandAll();
                //    }
                //}
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        internal static void CreateChildNodeMedicineType(TreeListNode rootNode, MedicineTypeADO medicineTypeAdo, List<MedicineTypeADO> medicineTypeAdos, UCMedcineTypeList control)
        {
            try
            {
                //if (medicineTypeAdo.MEDICINE_TYPE_CODE == "AML20")
                //{
                //    int o = 1;
                //}
                //var listChildMedicineType = medicineTypeAdos.Where(o => o.PARENT_ID == medicineTypeAdo.ID).ToList();
                //if (listChildMedicineType != null && listChildMedicineType.Count > 0)
                //{
                //    foreach (var medicineType in listChildMedicineType)
                //    {

                //        TreeListNode childNode = control.treeMedicineType.AppendNode(
                //         new object[] { medicineType.MEDICINE_TYPE_CODE, medicineType.MEDICINE_TYPE_NAME, medicineType.SERVICE_UNIT_NAME, medicineType.ACTIVE_INGR_BHYT_NAME, medicineType.CONCENTRA, medicineType.MANUFACTURER_NAME, medicineType.NATIONAL_NAME, Inventec.Common.Number.Convert.NumberToString(medicineType.IMP_PRICE ?? 0), medicineType.IMP_VAT_RATIO * 100, medicineType.HEIN_SERVICE_BHYT_NAME, medicineType.REGISTER_NUMBER, medicineType.MEDICINE_LINE_NAME, medicineType.MEDICINE_USE_FORM_NAME, medicineType.PACKING_TYPE_NAME, medicineType.IsAntibiotic, medicineType.IsAddictive, medicineType.IsNeurological, medicineType.ALERT_EXPIRED_DATE, medicineType.ALERT_MIN_IN_STOCK, medicineType.NUM_ORDER, medicineType.IsStopImp, medicineType }, rootNode, medicineType);
                //        CreateChildNodeMedicineType(childNode, medicineType, medicineTypeAdos, control);
                //    }
                //}
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
