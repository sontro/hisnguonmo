using Inventec.Desktop.Common.LanguageManager;
using System;
using System.Resources;
using System.Windows.Forms;
namespace HIS.Desktop.Plugins.HisMachineServMaty
{
    public partial class UCHisMachineServMaty : HIS.Desktop.Utility.UserControlBase
    {
        private void SetCaptionByLanguageKey()
        {
            try
            {
                ////Khoi tao doi tuong resource
                Resources.ResourceLanguageManager.LanguageResource = new ResourceManager("HIS.Desktop.Plugins.HisMachineServMaty.Resources.Lang", typeof(HIS.Desktop.Plugins.HisMachineServMaty.UCHisMachineServMaty).Assembly);

                ////Gan gia tri cho cac control editor co Text/Caption/ToolTip/NullText/NullValuePrompt/FindNullPrompt
                this.layoutControl1.Text = Inventec.Common.Resource.Get.Value("UCHisMachineServMaty.layoutControl1.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.layoutControl3.Text = Inventec.Common.Resource.Get.Value("UCHisMachineServMaty.layoutControl3.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.btnSave.Text = Inventec.Common.Resource.Get.Value("UCHisMachineServMaty.btnSave.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridColumnMaterialType_STT.Caption = Inventec.Common.Resource.Get.Value("UCHisMachineServMaty.gridColumnMaterialType_STT.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridColumnMaterialType_MaterialTypeCode.Caption = Inventec.Common.Resource.Get.Value("UCHisMachineServMaty.gridColumnMaterialType_MaterialTypeCode.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridColumnMaterialType_MaterialTypeName.Caption = Inventec.Common.Resource.Get.Value("UCHisMachineServMaty.gridColumnMaterialType_MaterialTypeName.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridColumnMaterialType_ServiceUnitName.Caption = Inventec.Common.Resource.Get.Value("UCHisMachineServMaty.gridColumnMaterialType_ServiceUnitName.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridColumnMaterialType_Amount.Caption = Inventec.Common.Resource.Get.Value("UCHisMachineServMaty.gridColumnMaterialType_Amount.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridColumnMaterialType_TotalPrice.Caption = Inventec.Common.Resource.Get.Value("UCHisMachineServMaty.gridColumnMaterialType_TotalPrice.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridServiceColumn_STT.Caption = Inventec.Common.Resource.Get.Value("UCHisMachineServMaty.gridServiceColumn_STT.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridServiceColumn_ServiceCode.Caption = Inventec.Common.Resource.Get.Value("UCHisMachineServMaty.gridServiceColumn_ServiceCode.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridServiceColumn_ServiceName.Caption = Inventec.Common.Resource.Get.Value("UCHisMachineServMaty.gridServiceColumn_ServiceName.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridHisMachineColumn_STT.Caption = Inventec.Common.Resource.Get.Value("UCHisMachineServMaty.gridHisMachineColumn_STT.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridHisMachineColumn_RoomCode.Caption = Inventec.Common.Resource.Get.Value("UCHisMachineServMaty.gridHisMachineColumn_RoomCode.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridHisMachineColumn_RoomName.Caption = Inventec.Common.Resource.Get.Value("UCHisMachineServMaty.gridHisMachineColumn_RoomName.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.repositoryItemPictureEdit2.NullText = Inventec.Common.Resource.Get.Value("UCHisMachineServMaty.repositoryItemPictureEdit2.NullText", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
              
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}