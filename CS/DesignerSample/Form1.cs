using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.DashboardCommon;
using DevExpress.DashboardCommon.ViewerData;
using DevExpress.DashboardWin;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraEditors;

namespace DesignerSample {
    public partial class Form1 : XtraForm {
        /// <summary>
        /// Determines whether filtering should be skipped (the Expand/Collapse button is pressed)
        /// </summary>
        bool skipFiltering = false;

        public Form1() {
            InitializeComponent();
            dashboardDesigner1.CreateRibbon();
            dashboardDesigner1.LoadDashboard("PivotMasterFilter.xml");
        }

        /// <summary>
        /// Used to handle the MouseDown event of the underlying PivotGridControl and determine if the Expand/Collapse button is pressed
        /// </summary>
        void OnDashboardItemControlCreated(object sender, DashboardItemControlEventArgs e) {
            if(e.DashboardItemName == "pivotDashboardItem1") {
                e.PivotGridControl.MouseDown += OnPivotGridControlMouseDown;
            }
        }

        /// <summary>
        /// Used to skip DashboardItemClick processing if the Expand/Collapse button is pressed
        /// </summary>
        void OnPivotGridControlMouseDown(object sender, MouseEventArgs e) {
            PivotGridControl pivot = sender as PivotGridControl;
            PivotGridHitInfo hi = pivot.CalcHitInfo(e.Location);
            skipFiltering = (hi.ValueInfo != null && hi.ValueInfo.ValueHitTest == PivotGridValueHitTest.ExpandButton);
        }

        /// <summary>
        /// Used to get dimensions' values related to a clicked area and set parameters used for filtering
        /// </summary>
        void OnDashboardItemClick(object sender, DashboardItemMouseActionEventArgs e) {
            if(e.DashboardItemName == "pivotDashboardItem1" && !skipFiltering) {
                dashboardDesigner1.BeginUpdateParameters();
                //clear all parameters
                ClearPivotFilter();
                //set selected columns and rows to parameters
                SetParameterValue(e.GetAxisPoint("Column"));
                SetParameterValue(e.GetAxisPoint("Row"));
                dashboardDesigner1.EndUpdateParameters();
            }
        }

        /// <summary>
        /// Sets Parameters' values for dimensions from a specific axis (Column or Row)
        /// </summary>
        void SetParameterValue(AxisPoint axisPoint) {
            while(axisPoint != null && axisPoint.Dimension != null) {
                string parameterName = GetParameterDataMember(axisPoint.Dimension.DataMember);
                dashboardDesigner1.Parameters[parameterName].SelectedValue = axisPoint.Value;
                axisPoint = axisPoint.Parent;
            }
        }

        /// <summary>
        /// Used to add the Clear Filter button to the PivotGrid Item's caption
        /// </summary>
        void OnCustomizeDashboardItemCaption(object sender, CustomizeDashboardItemCaptionEventArgs e) {
            if(e.DashboardItemName == "pivotDashboardItem1") {
                DashboardToolbarItem showDataItem = new DashboardToolbarItem("Clear Master Filter",
                        new Action<DashboardToolbarItemClickEventArgs>((args) => {
                            dashboardDesigner1.BeginUpdateParameters();
                            ClearPivotFilter();
                            dashboardDesigner1.EndUpdateParameters();
                        }));
                showDataItem.Enabled = IsAnyFilterSet();
                showDataItem.SvgImage = svgImageCollection1[0];
                e.Items.Insert(0, showDataItem);
            }
        }

        /// <summary>
        /// Clears all parameters related to the PivotGrid Item
        /// </summary>
        void ClearPivotFilter() {
            PivotDashboardItem pivotItem = dashboardDesigner1.Dashboard.Items["pivotDashboardItem1"] as PivotDashboardItem;
            ClearParameters(pivotItem.Columns);
            ClearParameters(pivotItem.Rows);
        }

        /// <summary>
        /// Clears Prarameters' values for all dimensions form a specific collection
        /// </summary>
        void ClearParameters(DimensionCollection dimensions) {
            foreach(var dimension in dimensions)
                dashboardDesigner1.Parameters[GetParameterDataMember(dimension.DataMember)].SelectedValue = null;
        }

        /// <summary>
        /// Gets a value indicating if any parameter related to the PivotGrid Item has value
        /// </summary>
        bool IsAnyFilterSet() {
            PivotDashboardItem pivotItem = dashboardDesigner1.Dashboard.Items["pivotDashboardItem1"] as PivotDashboardItem;
            if(pivotItem.Columns.Any(c => dashboardDesigner1.Parameters[GetParameterDataMember(c.DataMember)].SelectedValue != null))
                return true;
            if(pivotItem.Rows.Any(c => dashboardDesigner1.Parameters[GetParameterDataMember(c.DataMember)].SelectedValue != null))
                return true;
            return false;
        }

        /// <summary>
        /// Generates a parameter's name from a data member
        /// </summary>
        string GetParameterDataMember(string dataMember) {
            return dataMember + "Param";
        }
    }
}
