        private void SetCaptionByLanguageKey()
        {
            try
            {
                ////Khoi tao doi tuong resource
                Resources.ResourceLanguageManager.LanguageResource = new ResourceManager("HIS.Desktop.Plugins. AccidentHurt.Resources.Lang", typeof(HIS.Desktop.Plugins. AccidentHurt.frmAccidentHurt).Assembly);

                ////Gan gia tri cho cac control editor co Text/Caption/ToolTip/NullText/NullValuePrompt/FindNullPrompt
                this.layoutControl1.Text = Inventec.Common.Resource.Get.Value("frmAccidentHurt.layoutControl1.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.bar1.Text = Inventec.Common.Resource.Get.Value("frmAccidentHurt.bar1.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.barButtonItem__Save.Caption = Inventec.Common.Resource.Get.Value("frmAccidentHurt.barButtonItem__Save.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.cboAccidentHurtType.Properties.NullText = Inventec.Common.Resource.Get.Value("frmAccidentHurt.cboAccidentHurtType.Properties.NullText", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.layoutControl6.Text = Inventec.Common.Resource.Get.Value("frmAccidentHurt.layoutControl6.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.btnSave.Text = Inventec.Common.Resource.Get.Value("frmAccidentHurt.btnSave.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.cboAccidentLocaltion.Properties.NullText = Inventec.Common.Resource.Get.Value("frmAccidentHurt.cboAccidentLocaltion.Properties.NullText", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.cboAccidentCare.Properties.NullText = Inventec.Common.Resource.Get.Value("frmAccidentHurt.cboAccidentCare.Properties.NullText", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.cboAccidentResult.Properties.NullText = Inventec.Common.Resource.Get.Value("frmAccidentHurt.cboAccidentResult.Properties.NullText", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.layoutControl2.Text = Inventec.Common.Resource.Get.Value("frmAccidentHurt.layoutControl2.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.cboAccidentBodyPart.Properties.NullText = Inventec.Common.Resource.Get.Value("frmAccidentHurt.cboAccidentBodyPart.Properties.NullText", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.groupHurtType.Text = Inventec.Common.Resource.Get.Value("frmAccidentHurt.groupHurtType.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.layoutControl3.Text = Inventec.Common.Resource.Get.Value("frmAccidentHurt.layoutControl3.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.cboAccidentPoison.Properties.NullText = Inventec.Common.Resource.Get.Value("frmAccidentHurt.cboAccidentPoison.Properties.NullText", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.cboHelmet.Properties.NullText = Inventec.Common.Resource.Get.Value("frmAccidentHurt.cboHelmet.Properties.NullText", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.cboAccidentVehicle.Properties.NullText = Inventec.Common.Resource.Get.Value("frmAccidentHurt.cboAccidentVehicle.Properties.NullText", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.chkUseAlcohol.Text = Inventec.Common.Resource.Get.Value("frmAccidentHurt.chkUseAlcohol.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.lciTxtAccidentVehicle.Text = Inventec.Common.Resource.Get.Value("frmAccidentHurt.lciTxtAccidentVehicle.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.lciTXTAccidentPoison.Text = Inventec.Common.Resource.Get.Value("frmAccidentHurt.lciTXTAccidentPoison.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.lciTxtHelmet.Text = Inventec.Common.Resource.Get.Value("frmAccidentHurt.lciTxtHelmet.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.lciAlcohol.Text = Inventec.Common.Resource.Get.Value("frmAccidentHurt.lciAlcohol.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.lciContent.Text = Inventec.Common.Resource.Get.Value("frmAccidentHurt.lciContent.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.lciAccidentLocaltion.Text = Inventec.Common.Resource.Get.Value("frmAccidentHurt.lciAccidentLocaltion.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.lciAccidentBodyPart.Text = Inventec.Common.Resource.Get.Value("frmAccidentHurt.lciAccidentBodyPart.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.lciAccidentResult.Text = Inventec.Common.Resource.Get.Value("frmAccidentHurt.lciAccidentResult.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.lciAccidentCare.Text = Inventec.Common.Resource.Get.Value("frmAccidentHurt.lciAccidentCare.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.lciAccidentHurtType.Text = Inventec.Common.Resource.Get.Value("frmAccidentHurt.lciAccidentHurtType.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.lciAccidentTime.Text = Inventec.Common.Resource.Get.Value("frmAccidentHurt.lciAccidentTime.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.layoutControl4.Text = Inventec.Common.Resource.Get.Value("frmAccidentHurt.layoutControl4.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.Text = Inventec.Common.Resource.Get.Value("frmAccidentHurt.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
