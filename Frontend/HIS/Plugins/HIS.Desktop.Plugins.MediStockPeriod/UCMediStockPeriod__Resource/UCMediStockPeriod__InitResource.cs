        private void SetCaptionByLanguageKey()
        {
            try
            {
                ////Khoi tao doi tuong resource
                Resources.ResourceLanguageManager.LanguageResource = new ResourceManager("HIS.Desktop.Plugins.MediStockPeriod.Resources.Lang", typeof(HIS.Desktop.Plugins.MediStockPeriod.UCMediStockPeriod__Resource).Assembly);

                ////Gan gia tri cho cac control editor co Text/Caption/ToolTip/NullText/NullValuePrompt/FindNullPrompt
                this.layoutControl1.Text = Inventec.Common.Resource.Get.Value("UCMediStockPeriod.layoutControl1.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.layoutControl3.Text = Inventec.Common.Resource.Get.Value("UCMediStockPeriod.layoutControl3.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.btnPrint.Text = Inventec.Common.Resource.Get.Value("UCMediStockPeriod.btnPrint.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.btnRefesh.Text = Inventec.Common.Resource.Get.Value("UCMediStockPeriod.btnRefesh.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.btnCreatePeriod.Text = Inventec.Common.Resource.Get.Value("UCMediStockPeriod.btnCreatePeriod.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.chkVatTu.Properties.Caption = Inventec.Common.Resource.Get.Value("UCMediStockPeriod.chkVatTu.Properties.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.chkThuoc.Properties.Caption = Inventec.Common.Resource.Get.Value("UCMediStockPeriod.chkThuoc.Properties.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.chkKhongHienDongTangGiam.Properties.Caption = Inventec.Common.Resource.Get.Value("UCMediStockPeriod.chkKhongHienDongTangGiam.Properties.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.chkKhongHienDongHet.Properties.Caption = Inventec.Common.Resource.Get.Value("UCMediStockPeriod.chkKhongHienDongHet.Properties.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.layoutControl2.Text = Inventec.Common.Resource.Get.Value("UCMediStockPeriod.layoutControl2.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.cboMediStock.Properties.NullText = Inventec.Common.Resource.Get.Value("UCMediStockPeriod.cboMediStock.Properties.NullText", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridColSTT.Caption = Inventec.Common.Resource.Get.Value("UCMediStockPeriod.gridColSTT.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridColMediStockPeriodCode.Caption = Inventec.Common.Resource.Get.Value("UCMediStockPeriod.gridColMediStockPeriodCode.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridColMediStockPeriodName.Caption = Inventec.Common.Resource.Get.Value("UCMediStockPeriod.gridColMediStockPeriodName.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridColCountImpMest.Caption = Inventec.Common.Resource.Get.Value("UCMediStockPeriod.gridColCountImpMest.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridColCountExpMest.Caption = Inventec.Common.Resource.Get.Value("UCMediStockPeriod.gridColCountExpMest.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridColCoutMediType.Caption = Inventec.Common.Resource.Get.Value("UCMediStockPeriod.gridColCoutMediType.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.gridColCountMateType.Caption = Inventec.Common.Resource.Get.Value("UCMediStockPeriod.gridColCountMateType.Caption", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
                this.lciChooStock.Text = Inventec.Common.Resource.Get.Value("UCMediStockPeriod.lciChooStock.Text", Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
