﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOEE
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOEE))
        Me.dtpDesde = New Guna.UI.WinForms.GunaDateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.bntFiltrar = New Guna.UI.WinForms.GunaAdvenceButton()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.resultDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.site = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.program = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.model = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.line = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.shift = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.process = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.productionQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.defectQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.startTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.endTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.workTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.available = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.planned = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.running = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.breakdown = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.qualityDT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.maintenanceDT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.materialDT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.productionDT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.othersDT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.dgvOEE = New System.Windows.Forms.DataGridView()
        Me.result2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.site2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.program2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.model2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.line2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.shift2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.process2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.qtyDaily = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.totalTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.plannedDT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.available2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.breakdowns = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tooling = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.running2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.runninge = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.productive = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.times = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.defects = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mttr = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mtbf = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ratio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.durability = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.oee = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgvOEE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtpDesde
        '
        Me.dtpDesde.BackColor = System.Drawing.Color.Transparent
        Me.dtpDesde.BaseColor = System.Drawing.Color.White
        Me.dtpDesde.BorderColor = System.Drawing.Color.Silver
        Me.dtpDesde.CustomFormat = Nothing
        Me.dtpDesde.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right
        Me.dtpDesde.FocusedColor = System.Drawing.Color.DodgerBlue
        Me.dtpDesde.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.dtpDesde.ForeColor = System.Drawing.Color.Black
        Me.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDesde.Location = New System.Drawing.Point(77, 15)
        Me.dtpDesde.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpDesde.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDesde.Name = "dtpDesde"
        Me.dtpDesde.OnHoverBaseColor = System.Drawing.Color.White
        Me.dtpDesde.OnHoverBorderColor = System.Drawing.Color.DodgerBlue
        Me.dtpDesde.OnHoverForeColor = System.Drawing.Color.Black
        Me.dtpDesde.OnPressedColor = System.Drawing.Color.Black
        Me.dtpDesde.Radius = 5
        Me.dtpDesde.Size = New System.Drawing.Size(113, 30)
        Me.dtpDesde.TabIndex = 253
        Me.dtpDesde.Text = "1/31/2023"
        Me.dtpDesde.Value = New Date(2023, 1, 31, 15, 57, 46, 926)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(24, 20)
        Me.Label3.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 20)
        Me.Label3.TabIndex = 252
        Me.Label3.Text = "Date:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'bntFiltrar
        '
        Me.bntFiltrar.AnimationHoverSpeed = 0.07!
        Me.bntFiltrar.AnimationSpeed = 0.03!
        Me.bntFiltrar.BackColor = System.Drawing.Color.Transparent
        Me.bntFiltrar.BaseColor = System.Drawing.Color.LightBlue
        Me.bntFiltrar.BorderColor = System.Drawing.Color.Black
        Me.bntFiltrar.CheckedBaseColor = System.Drawing.Color.Gray
        Me.bntFiltrar.CheckedBorderColor = System.Drawing.Color.Black
        Me.bntFiltrar.CheckedForeColor = System.Drawing.Color.White
        Me.bntFiltrar.CheckedImage = Nothing
        Me.bntFiltrar.CheckedLineColor = System.Drawing.Color.DimGray
        Me.bntFiltrar.DialogResult = System.Windows.Forms.DialogResult.None
        Me.bntFiltrar.FocusedColor = System.Drawing.Color.Empty
        Me.bntFiltrar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bntFiltrar.ForeColor = System.Drawing.Color.Black
        Me.bntFiltrar.Image = CType(resources.GetObject("bntFiltrar.Image"), System.Drawing.Image)
        Me.bntFiltrar.ImageSize = New System.Drawing.Size(20, 20)
        Me.bntFiltrar.LineColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.bntFiltrar.Location = New System.Drawing.Point(208, 12)
        Me.bntFiltrar.Name = "bntFiltrar"
        Me.bntFiltrar.OnHoverBaseColor = System.Drawing.Color.SteelBlue
        Me.bntFiltrar.OnHoverBorderColor = System.Drawing.Color.Black
        Me.bntFiltrar.OnHoverForeColor = System.Drawing.Color.White
        Me.bntFiltrar.OnHoverImage = Nothing
        Me.bntFiltrar.OnHoverLineColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.bntFiltrar.OnPressedColor = System.Drawing.Color.Black
        Me.bntFiltrar.Radius = 10
        Me.bntFiltrar.Size = New System.Drawing.Size(42, 36)
        Me.bntFiltrar.TabIndex = 254
        Me.bntFiltrar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.AllowUserToResizeRows = False
        Me.dgv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.resultDate, Me.site, Me.program, Me.model, Me.line, Me.shift, Me.process, Me.status, Me.productionQty, Me.defectQty, Me.startTime, Me.endTime, Me.workTime, Me.available, Me.planned, Me.running, Me.breakdown, Me.qualityDT, Me.maintenanceDT, Me.materialDT, Me.productionDT, Me.othersDT})
        Me.dgv.Location = New System.Drawing.Point(12, 6)
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersVisible = False
        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv.Size = New System.Drawing.Size(1172, 684)
        Me.dgv.TabIndex = 255
        '
        'resultDate
        '
        Me.resultDate.HeaderText = "Result Date"
        Me.resultDate.Name = "resultDate"
        Me.resultDate.ReadOnly = True
        '
        'site
        '
        Me.site.HeaderText = "Site"
        Me.site.Name = "site"
        Me.site.ReadOnly = True
        '
        'program
        '
        Me.program.HeaderText = "Program"
        Me.program.Name = "program"
        Me.program.ReadOnly = True
        '
        'model
        '
        Me.model.HeaderText = "Model"
        Me.model.Name = "model"
        Me.model.ReadOnly = True
        '
        'line
        '
        Me.line.HeaderText = "Line"
        Me.line.Name = "line"
        Me.line.ReadOnly = True
        '
        'shift
        '
        Me.shift.HeaderText = "Shift"
        Me.shift.Name = "shift"
        Me.shift.ReadOnly = True
        '
        'process
        '
        Me.process.HeaderText = "Process"
        Me.process.Name = "process"
        Me.process.ReadOnly = True
        '
        'status
        '
        Me.status.HeaderText = "Status"
        Me.status.Name = "status"
        Me.status.ReadOnly = True
        '
        'productionQty
        '
        Me.productionQty.HeaderText = "Production Qty"
        Me.productionQty.Name = "productionQty"
        Me.productionQty.ReadOnly = True
        '
        'defectQty
        '
        Me.defectQty.HeaderText = "Defect QTY"
        Me.defectQty.Name = "defectQty"
        Me.defectQty.ReadOnly = True
        '
        'startTime
        '
        Me.startTime.HeaderText = "Start Time"
        Me.startTime.Name = "startTime"
        Me.startTime.ReadOnly = True
        '
        'endTime
        '
        Me.endTime.HeaderText = "End Time"
        Me.endTime.Name = "endTime"
        Me.endTime.ReadOnly = True
        '
        'workTime
        '
        Me.workTime.HeaderText = "Total Work Time"
        Me.workTime.Name = "workTime"
        Me.workTime.ReadOnly = True
        '
        'available
        '
        Me.available.HeaderText = "Available Working time"
        Me.available.Name = "available"
        Me.available.ReadOnly = True
        '
        'planned
        '
        Me.planned.HeaderText = "Planned DownTime"
        Me.planned.Name = "planned"
        Me.planned.ReadOnly = True
        '
        'running
        '
        Me.running.HeaderText = "Running Time"
        Me.running.Name = "running"
        Me.running.ReadOnly = True
        '
        'breakdown
        '
        Me.breakdown.HeaderText = "Breakdowns"
        Me.breakdown.Name = "breakdown"
        Me.breakdown.ReadOnly = True
        '
        'qualityDT
        '
        Me.qualityDT.HeaderText = "Quality Downtime"
        Me.qualityDT.Name = "qualityDT"
        Me.qualityDT.ReadOnly = True
        '
        'maintenanceDT
        '
        Me.maintenanceDT.HeaderText = "Maintenance Downtime"
        Me.maintenanceDT.Name = "maintenanceDT"
        Me.maintenanceDT.ReadOnly = True
        '
        'materialDT
        '
        Me.materialDT.HeaderText = "Material Downtime"
        Me.materialDT.Name = "materialDT"
        Me.materialDT.ReadOnly = True
        '
        'productionDT
        '
        Me.productionDT.HeaderText = "Production Downtime"
        Me.productionDT.Name = "productionDT"
        Me.productionDT.ReadOnly = True
        '
        'othersDT
        '
        Me.othersDT.HeaderText = "Others Downtime"
        Me.othersDT.Name = "othersDT"
        Me.othersDT.ReadOnly = True
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 54)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1198, 719)
        Me.TabControl1.TabIndex = 256
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dgv)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1190, 693)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Downtime Results"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.dgvOEE)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1190, 693)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Daily Data"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'dgvOEE
        '
        Me.dgvOEE.AllowUserToAddRows = False
        Me.dgvOEE.AllowUserToDeleteRows = False
        Me.dgvOEE.AllowUserToResizeRows = False
        Me.dgvOEE.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvOEE.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOEE.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.result2, Me.site2, Me.program2, Me.model2, Me.line2, Me.shift2, Me.process2, Me.qtyDaily, Me.totalTime, Me.plannedDT, Me.available2, Me.breakdowns, Me.tooling, Me.running2, Me.runninge, Me.productive, Me.cost, Me.times, Me.defects, Me.mttr, Me.mtbf, Me.ratio, Me.durability, Me.oee})
        Me.dgvOEE.Location = New System.Drawing.Point(9, 4)
        Me.dgvOEE.Name = "dgvOEE"
        Me.dgvOEE.ReadOnly = True
        Me.dgvOEE.RowHeadersVisible = False
        Me.dgvOEE.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvOEE.Size = New System.Drawing.Size(1172, 684)
        Me.dgvOEE.TabIndex = 256
        '
        'result2
        '
        Me.result2.HeaderText = "Result Date"
        Me.result2.Name = "result2"
        Me.result2.ReadOnly = True
        '
        'site2
        '
        Me.site2.HeaderText = "Site"
        Me.site2.Name = "site2"
        Me.site2.ReadOnly = True
        '
        'program2
        '
        Me.program2.HeaderText = "Program"
        Me.program2.Name = "program2"
        Me.program2.ReadOnly = True
        '
        'model2
        '
        Me.model2.HeaderText = "Model"
        Me.model2.Name = "model2"
        Me.model2.ReadOnly = True
        '
        'line2
        '
        Me.line2.HeaderText = "Line"
        Me.line2.Name = "line2"
        Me.line2.ReadOnly = True
        '
        'shift2
        '
        Me.shift2.HeaderText = "Shift"
        Me.shift2.Name = "shift2"
        Me.shift2.ReadOnly = True
        '
        'process2
        '
        Me.process2.HeaderText = "Process"
        Me.process2.Name = "process2"
        Me.process2.ReadOnly = True
        '
        'qtyDaily
        '
        Me.qtyDaily.HeaderText = "Qty of Product"
        Me.qtyDaily.Name = "qtyDaily"
        Me.qtyDaily.ReadOnly = True
        '
        'totalTime
        '
        Me.totalTime.HeaderText = "Total Time"
        Me.totalTime.Name = "totalTime"
        Me.totalTime.ReadOnly = True
        '
        'plannedDT
        '
        Me.plannedDT.HeaderText = "Planned Dowtime"
        Me.plannedDT.Name = "plannedDT"
        Me.plannedDT.ReadOnly = True
        '
        'available2
        '
        Me.available2.HeaderText = "Available Time"
        Me.available2.Name = "available2"
        Me.available2.ReadOnly = True
        '
        'breakdowns
        '
        Me.breakdowns.HeaderText = "Breakdowns"
        Me.breakdowns.Name = "breakdowns"
        Me.breakdowns.ReadOnly = True
        '
        'tooling
        '
        Me.tooling.HeaderText = "Tooling Changes"
        Me.tooling.Name = "tooling"
        Me.tooling.ReadOnly = True
        '
        'running2
        '
        Me.running2.HeaderText = "Running Time"
        Me.running2.Name = "running2"
        Me.running2.ReadOnly = True
        '
        'runninge
        '
        Me.runninge.HeaderText = "Running Efficiency"
        Me.runninge.Name = "runninge"
        Me.runninge.ReadOnly = True
        '
        'productive
        '
        Me.productive.HeaderText = "Productive Performance"
        Me.productive.Name = "productive"
        Me.productive.ReadOnly = True
        '
        'cost
        '
        Me.cost.HeaderText = "Maintenance Cost"
        Me.cost.Name = "cost"
        Me.cost.ReadOnly = True
        '
        'times
        '
        Me.times.HeaderText = "Times of Breakdowns"
        Me.times.Name = "times"
        Me.times.ReadOnly = True
        '
        'defects
        '
        Me.defects.HeaderText = "Qty of Defects"
        Me.defects.Name = "defects"
        Me.defects.ReadOnly = True
        '
        'mttr
        '
        Me.mttr.HeaderText = "MTTR"
        Me.mttr.Name = "mttr"
        Me.mttr.ReadOnly = True
        '
        'mtbf
        '
        Me.mtbf.HeaderText = "MTBF"
        Me.mtbf.Name = "mtbf"
        Me.mtbf.ReadOnly = True
        '
        'ratio
        '
        Me.ratio.HeaderText = "Fair quality Ratio"
        Me.ratio.Name = "ratio"
        Me.ratio.ReadOnly = True
        '
        'durability
        '
        Me.durability.HeaderText = "Breakdowns durability Rate"
        Me.durability.Name = "durability"
        Me.durability.ReadOnly = True
        '
        'oee
        '
        Me.oee.HeaderText = "OEE"
        Me.oee.Name = "oee"
        Me.oee.ReadOnly = True
        '
        'frmOEE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1212, 777)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.bntFiltrar)
        Me.Controls.Add(Me.dtpDesde)
        Me.Controls.Add(Me.Label3)
        Me.Name = "frmOEE"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "OEE Calculation"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.dgvOEE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents bntFiltrar As Guna.UI.WinForms.GunaAdvenceButton
    Friend WithEvents dtpDesde As Guna.UI.WinForms.GunaDateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents dgv As DataGridView
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents resultDate As DataGridViewTextBoxColumn
    Friend WithEvents site As DataGridViewTextBoxColumn
    Friend WithEvents program As DataGridViewTextBoxColumn
    Friend WithEvents model As DataGridViewTextBoxColumn
    Friend WithEvents line As DataGridViewTextBoxColumn
    Friend WithEvents shift As DataGridViewTextBoxColumn
    Friend WithEvents process As DataGridViewTextBoxColumn
    Friend WithEvents status As DataGridViewTextBoxColumn
    Friend WithEvents productionQty As DataGridViewTextBoxColumn
    Friend WithEvents defectQty As DataGridViewTextBoxColumn
    Friend WithEvents startTime As DataGridViewTextBoxColumn
    Friend WithEvents endTime As DataGridViewTextBoxColumn
    Friend WithEvents workTime As DataGridViewTextBoxColumn
    Friend WithEvents available As DataGridViewTextBoxColumn
    Friend WithEvents planned As DataGridViewTextBoxColumn
    Friend WithEvents running As DataGridViewTextBoxColumn
    Friend WithEvents breakdown As DataGridViewTextBoxColumn
    Friend WithEvents qualityDT As DataGridViewTextBoxColumn
    Friend WithEvents maintenanceDT As DataGridViewTextBoxColumn
    Friend WithEvents materialDT As DataGridViewTextBoxColumn
    Friend WithEvents productionDT As DataGridViewTextBoxColumn
    Friend WithEvents othersDT As DataGridViewTextBoxColumn
    Friend WithEvents dgvOEE As DataGridView
    Friend WithEvents result2 As DataGridViewTextBoxColumn
    Friend WithEvents site2 As DataGridViewTextBoxColumn
    Friend WithEvents program2 As DataGridViewTextBoxColumn
    Friend WithEvents model2 As DataGridViewTextBoxColumn
    Friend WithEvents line2 As DataGridViewTextBoxColumn
    Friend WithEvents shift2 As DataGridViewTextBoxColumn
    Friend WithEvents process2 As DataGridViewTextBoxColumn
    Friend WithEvents qtyDaily As DataGridViewTextBoxColumn
    Friend WithEvents totalTime As DataGridViewTextBoxColumn
    Friend WithEvents plannedDT As DataGridViewTextBoxColumn
    Friend WithEvents available2 As DataGridViewTextBoxColumn
    Friend WithEvents breakdowns As DataGridViewTextBoxColumn
    Friend WithEvents tooling As DataGridViewTextBoxColumn
    Friend WithEvents running2 As DataGridViewTextBoxColumn
    Friend WithEvents runninge As DataGridViewTextBoxColumn
    Friend WithEvents productive As DataGridViewTextBoxColumn
    Friend WithEvents cost As DataGridViewTextBoxColumn
    Friend WithEvents times As DataGridViewTextBoxColumn
    Friend WithEvents defects As DataGridViewTextBoxColumn
    Friend WithEvents mttr As DataGridViewTextBoxColumn
    Friend WithEvents mtbf As DataGridViewTextBoxColumn
    Friend WithEvents ratio As DataGridViewTextBoxColumn
    Friend WithEvents durability As DataGridViewTextBoxColumn
    Friend WithEvents oee As DataGridViewTextBoxColumn
End Class
