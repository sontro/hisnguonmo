using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors;
using System.ComponentModel;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.Data.Filtering;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using HIS.Desktop.Utilities.Extensions;

namespace MultiColumnFilterTest
{
    [UserRepositoryItem("RegisterCustomGridLookUpEditNew")]
    public class RepositoryItemCustomGridLookUpEditNew : RepositoryItemGridLookUpEdit
    {
        static RepositoryItemCustomGridLookUpEditNew() { RegisterCustomGridLookUpEditNew(); }

        public RepositoryItemCustomGridLookUpEditNew()
        {
            TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            AutoComplete = false;
        }
        [Browsable(false)]
        public override DevExpress.XtraEditors.Controls.TextEditStyles TextEditStyle
        {
            get
            {
                return
                    base.TextEditStyle;
            }
            set { base.TextEditStyle = value; }
        }
        public const string CustomGridLookUpEditNewName = "CustomGridLookUpEditNew";

        public override string EditorTypeName { get { return CustomGridLookUpEditNewName; } }

        public static void RegisterCustomGridLookUpEditNew()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomGridLookUpEditNewName,
              typeof(CustomGridLookUpEditNew), typeof(RepositoryItemCustomGridLookUpEditNew),
              typeof(GridLookUpEditBaseViewInfo), new ButtonEditPainter(), true));
        }

        protected override GridView CreateViewInstance() { return new CustomGridView(); }
        protected override GridControl CreateGrid() { return new CustomGridControl(); }
    }


    public class CustomGridLookUpEditNew : GridLookUpEdit
    {
        static CustomGridLookUpEditNew()
        {
            RepositoryItemCustomGridLookUpEditNew.RegisterCustomGridLookUpEditNew();
        }

        public CustomGridLookUpEditNew() : base() { }

        public override string EditorTypeName
        {
            get
            {
                return
                    RepositoryItemCustomGridLookUpEditNew.CustomGridLookUpEditNewName;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemCustomGridLookUpEditNew Properties
        {
            get
            {
                return base.Properties as
                    RepositoryItemCustomGridLookUpEditNew;
            }
        }
    }
}