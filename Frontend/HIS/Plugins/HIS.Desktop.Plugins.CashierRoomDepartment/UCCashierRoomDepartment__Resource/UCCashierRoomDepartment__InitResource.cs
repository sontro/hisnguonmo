        private void SetCaptionByLanguageKey()
        {
            try
            {
                ////Khoi tao doi tuong resource
                Resources.ResourceLanguageManager.LanguageResource = new ResourceManager("HIS.Desktop.Plugins.CashierRoomDepartment.Resources.Lang", typeof(HIS.Desktop.Plugins.CashierRoomDepartment.UCCashierRoomDepartment__Resource).Assembly);

                ////Gan gia tri cho cac control editor co Text/Caption/ToolTip/NullText/NullValuePrompt/FindNullPrompt
                this.layoutControl1.Text = Inventec.Common.Resource.Get.Value("UCCashierRoomDepartment.layoutControl1.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.btnSave.Text = Inventec.Common.Resource.Get.Value("UCCashierRoomDepartment.btnSave.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.comboType.Properties.NullText = Inventec.Common.Resource.Get.Value("UCCashierRoomDepartment.comboType.Properties.NullText", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.btnSearchCashierRoom.Text = Inventec.Common.Resource.Get.Value("UCCashierRoomDepartment.btnSearchCashierRoom.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.txtCashierRoom.Properties.NullValuePrompt = Inventec.Common.Resource.Get.Value("UCCashierRoomDepartment.txtCashierRoom.Properties.NullValuePrompt", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.btnSearchDepartment.Text = Inventec.Common.Resource.Get.Value("UCCashierRoomDepartment.btnSearchDepartment.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.txtDepartment.Properties.NullValuePrompt = Inventec.Common.Resource.Get.Value("UCCashierRoomDepartment.txtDepartment.Properties.NullValuePrompt", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.layoutControlItem10.Text = Inventec.Common.Resource.Get.Value("UCCashierRoomDepartment.layoutControlItem10.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
