Imports Microsoft.VisualBasic
Imports System
Namespace DesignerSample
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container()
			Me.dashboardDesigner1 = New DevExpress.DashboardWin.DashboardDesigner()
			Me.svgImageCollection1 = New DevExpress.Utils.SvgImageCollection(Me.components)
			CType(Me.dashboardDesigner1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.svgImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' dashboardDesigner1
			' 
			Me.dashboardDesigner1.AllowInspectAggregatedData = True
			Me.dashboardDesigner1.AllowInspectRawData = True
			Me.dashboardDesigner1.AllowPrintDashboard = False
			Me.dashboardDesigner1.AllowPrintDashboardItems = False
			Me.dashboardDesigner1.Appearance.BackColor = System.Drawing.Color.FromArgb((CInt(Fix((CByte(240))))), (CInt(Fix((CByte(240))))), (CInt(Fix((CByte(240))))))
			Me.dashboardDesigner1.Appearance.Options.UseBackColor = True
			Me.dashboardDesigner1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.dashboardDesigner1.Location = New System.Drawing.Point(0, 0)
			Me.dashboardDesigner1.Name = "dashboardDesigner1"
			Me.dashboardDesigner1.Size = New System.Drawing.Size(1096, 473)
			Me.dashboardDesigner1.TabIndex = 0
            '			Me.dashboardDesigner1.DashboardItemClick += New DevExpress.DashboardWin.DashboardItemMouseActionEventHandler(Me.OnDashboardItemClick);
            '			Me.dashboardDesigner1.DashboardItemControlCreated += New DevExpress.DashboardWin.DashboardItemControlCreatedEventHandler(Me.OnDashboardItemControlCreated);
            '			Me.dashboardDesigner1.CustomizeDashboardItemCaption += New DevExpress.DashboardWin.CustomizeDashboardItemCaptionEventHandler(Me.OnCustomizeDashboardItemCaption);
            ' 
            ' svgImageCollection1
            ' 
            Me.svgImageCollection1.Add("ClearMasterFilter", "ClearMasterFilter", GetType(My.Resources))
            ' 
            ' Form1
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(1096, 473)
			Me.Controls.Add(Me.dashboardDesigner1)
			Me.Name = "Form1"
			Me.Text = "Form1"
			CType(Me.dashboardDesigner1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.svgImageCollection1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private WithEvents dashboardDesigner1 As DevExpress.DashboardWin.DashboardDesigner
		Private svgImageCollection1 As DevExpress.Utils.SvgImageCollection
	End Class
End Namespace