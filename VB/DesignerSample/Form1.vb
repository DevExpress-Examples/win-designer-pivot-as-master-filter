Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardCommon.ViewerData
Imports DevExpress.DashboardWin
Imports DevExpress.XtraPivotGrid
Imports DevExpress.XtraEditors

Namespace DesignerSample
	Partial Public Class Form1
		Inherits XtraForm
		''' <summary>
		''' Determines whether filtering should be skipped (the Expand/Collapse button is pressed)
		''' </summary>
		Private skipFiltering As Boolean = False

		Public Sub New()
			InitializeComponent()
			dashboardDesigner1.CreateRibbon()
			dashboardDesigner1.LoadDashboard("PivotMasterFilter.xml")
		End Sub

		''' <summary>
		''' Used to handle the MouseDown event of the underlying PivotGridControl and determine if the Expand/Collapse button is pressed
		''' </summary>
		Private Sub OnDashboardItemControlCreated(ByVal sender As Object, ByVal e As DashboardItemControlEventArgs) Handles dashboardDesigner1.DashboardItemControlCreated
			If e.DashboardItemName = "pivotDashboardItem1" Then
				AddHandler e.PivotGridControl.MouseDown, AddressOf OnPivotGridControlMouseDown
			End If
		End Sub

		''' <summary>
		''' Used to skip DashboardItemClick processing if the Expand/Collapse button is pressed
		''' </summary>
		Private Sub OnPivotGridControlMouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
			Dim pivot As PivotGridControl = TryCast(sender, PivotGridControl)
			Dim hi As PivotGridHitInfo = pivot.CalcHitInfo(e.Location)
			skipFiltering = (hi.ValueInfo IsNot Nothing AndAlso hi.ValueInfo.ValueHitTest = PivotGridValueHitTest.ExpandButton)
		End Sub

		''' <summary>
		''' Used to get dimensions' values related to a clicked area and set parameters used for filtering
		''' </summary>
		Private Sub OnDashboardItemClick(ByVal sender As Object, ByVal e As DashboardItemMouseActionEventArgs) Handles dashboardDesigner1.DashboardItemClick
			If e.DashboardItemName = "pivotDashboardItem1" AndAlso (Not skipFiltering) Then
				dashboardDesigner1.BeginUpdateParameters()
				'clear all parameters
				ClearPivotFilter()
				'set selected columns and rows to parameters
				SetParameterValue(e.GetAxisPoint("Column"))
				SetParameterValue(e.GetAxisPoint("Row"))
				dashboardDesigner1.EndUpdateParameters()
			End If
		End Sub

		''' <summary>
		''' Sets Parameters' values for dimensions from a specific axis (Column or Row)
		''' </summary>
		Private Sub SetParameterValue(ByVal axisPoint As AxisPoint)
			Do While axisPoint IsNot Nothing AndAlso axisPoint.Dimension IsNot Nothing
				Dim parameterName As String = GetParameterDataMember(axisPoint.Dimension.DataMember)
				dashboardDesigner1.Parameters(parameterName).SelectedValue = axisPoint.Value
				axisPoint = axisPoint.Parent
			Loop
		End Sub

		''' <summary>
		''' Used to add the Clear Filter button to the PivotGrid Item's caption
		''' </summary>
		Private Sub OnCustomizeDashboardItemCaption(ByVal sender As Object, ByVal e As CustomizeDashboardItemCaptionEventArgs) Handles dashboardDesigner1.CustomizeDashboardItemCaption
			If e.DashboardItemName = "pivotDashboardItem1" Then
				Dim showDataItem As New DashboardToolbarItem("Clear Master Filter", New Action(Of DashboardToolbarItemClickEventArgs)(Function(args) AnonymousMethod1(args)))
				showDataItem.Enabled = IsAnyFilterSet()
				showDataItem.SvgImage = svgImageCollection1(0)
				e.Items.Insert(0, showDataItem)
			End If
		End Sub
		
		Private Function AnonymousMethod1(ByVal args As Object) As Boolean
			dashboardDesigner1.BeginUpdateParameters()
			ClearPivotFilter()
			dashboardDesigner1.EndUpdateParameters()
			Return True
		End Function

		''' <summary>
		''' Clears all parameters related to the PivotGrid Item
		''' </summary>
		Private Sub ClearPivotFilter()
			Dim pivotItem As PivotDashboardItem = TryCast(dashboardDesigner1.Dashboard.Items("pivotDashboardItem1"), PivotDashboardItem)
			ClearParameters(pivotItem.Columns)
			ClearParameters(pivotItem.Rows)
		End Sub

		''' <summary>
		''' Clears Prarameters' values for all dimensions form a specific collection
		''' </summary>
		Private Sub ClearParameters(ByVal dimensions As DimensionCollection)
			For Each dimension In dimensions
				dashboardDesigner1.Parameters(GetParameterDataMember(dimension.DataMember)).SelectedValue = Nothing
			Next dimension
		End Sub

		''' <summary>
		''' Gets a value indicating if any parameter related to the PivotGrid Item has value
		''' </summary>
		Private Function IsAnyFilterSet() As Boolean
			Dim pivotItem As PivotDashboardItem = TryCast(dashboardDesigner1.Dashboard.Items("pivotDashboardItem1"), PivotDashboardItem)
			If pivotItem.Columns.Any(Function(c) dashboardDesigner1.Parameters(GetParameterDataMember(c.DataMember)).SelectedValue IsNot Nothing) Then
				Return True
			End If
			If pivotItem.Rows.Any(Function(c) dashboardDesigner1.Parameters(GetParameterDataMember(c.DataMember)).SelectedValue IsNot Nothing) Then
				Return True
			End If
			Return False
		End Function

		''' <summary>
		''' Generates a parameter's name from a data member
		''' </summary>
		Private Function GetParameterDataMember(ByVal dataMember As String) As String
			Return dataMember & "Param"
		End Function
	End Class
End Namespace
