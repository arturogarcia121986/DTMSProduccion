<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDetails))
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.btnSave = New Guna.UI.WinForms.GunaAdvenceButton()
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.line = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.process = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.machineID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.processName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.machineStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.start = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.endTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lead = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.depto = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.EC1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EC2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EC3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EC4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EC5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.detail = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.AllowUserToOrderColumns = True
        Me.dgv.AllowUserToResizeRows = False
        Me.dgv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv.BackgroundColor = System.Drawing.Color.White
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id, Me.fecha, Me.line, Me.process, Me.machineID, Me.processName, Me.machineStatus, Me.start, Me.endTime, Me.lead, Me.depto, Me.EC1, Me.EC2, Me.EC3, Me.EC4, Me.EC5, Me.detail})
        Me.dgv.Location = New System.Drawing.Point(2, 2)
        Me.dgv.Name = "dgv"
        Me.dgv.RowHeadersVisible = False
        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgv.Size = New System.Drawing.Size(1221, 465)
        Me.dgv.TabIndex = 1
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.AnimationHoverSpeed = 0.07!
        Me.btnSave.AnimationSpeed = 0.03!
        Me.btnSave.BackColor = System.Drawing.Color.Transparent
        Me.btnSave.BaseColor = System.Drawing.Color.LightBlue
        Me.btnSave.BorderColor = System.Drawing.Color.Black
        Me.btnSave.CheckedBaseColor = System.Drawing.Color.Gray
        Me.btnSave.CheckedBorderColor = System.Drawing.Color.Black
        Me.btnSave.CheckedForeColor = System.Drawing.Color.White
        Me.btnSave.CheckedImage = CType(resources.GetObject("btnSave.CheckedImage"), System.Drawing.Image)
        Me.btnSave.CheckedLineColor = System.Drawing.Color.DimGray
        Me.btnSave.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnSave.FocusedColor = System.Drawing.Color.Empty
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Image = Global.DTMS.My.Resources.Resources.disk
        Me.btnSave.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnSave.LineColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.btnSave.Location = New System.Drawing.Point(1156, 473)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.OnHoverBaseColor = System.Drawing.Color.SteelBlue
        Me.btnSave.OnHoverBorderColor = System.Drawing.Color.Black
        Me.btnSave.OnHoverForeColor = System.Drawing.Color.White
        Me.btnSave.OnHoverImage = Nothing
        Me.btnSave.OnHoverLineColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.btnSave.OnPressedColor = System.Drawing.Color.Black
        Me.btnSave.Radius = 10
        Me.btnSave.Size = New System.Drawing.Size(75, 33)
        Me.btnSave.TabIndex = 59
        Me.btnSave.Text = "Save"
        Me.btnSave.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'id
        '
        Me.id.HeaderText = "ID"
        Me.id.Name = "id"
        Me.id.Visible = False
        '
        'fecha
        '
        Me.fecha.HeaderText = "Date"
        Me.fecha.Name = "fecha"
        '
        'line
        '
        Me.line.HeaderText = "Line"
        Me.line.Name = "line"
        '
        'process
        '
        Me.process.HeaderText = "# Process"
        Me.process.Name = "process"
        '
        'machineID
        '
        Me.machineID.HeaderText = "Machine ID"
        Me.machineID.Name = "machineID"
        Me.machineID.ReadOnly = True
        '
        'processName
        '
        Me.processName.HeaderText = "Process Name"
        Me.processName.Name = "processName"
        Me.processName.ReadOnly = True
        '
        'machineStatus
        '
        Me.machineStatus.HeaderText = "Machine Status"
        Me.machineStatus.Name = "machineStatus"
        Me.machineStatus.ReadOnly = True
        '
        'start
        '
        Me.start.HeaderText = "Start Time"
        Me.start.Name = "start"
        Me.start.ReadOnly = True
        '
        'endTime
        '
        Me.endTime.HeaderText = "End Time"
        Me.endTime.Name = "endTime"
        Me.endTime.ReadOnly = True
        '
        'lead
        '
        Me.lead.HeaderText = "Lead Time"
        Me.lead.Name = "lead"
        Me.lead.ReadOnly = True
        '
        'depto
        '
        Me.depto.HeaderText = "Depto"
        Me.depto.Name = "depto"
        Me.depto.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.depto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'EC1
        '
        Me.EC1.HeaderText = "EC1"
        Me.EC1.Name = "EC1"
        Me.EC1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'EC2
        '
        Me.EC2.HeaderText = "EC2"
        Me.EC2.Name = "EC2"
        Me.EC2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.EC2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'EC3
        '
        Me.EC3.HeaderText = "EC3"
        Me.EC3.Name = "EC3"
        Me.EC3.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.EC3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'EC4
        '
        Me.EC4.HeaderText = "EC4"
        Me.EC4.Name = "EC4"
        Me.EC4.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.EC4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'EC5
        '
        Me.EC5.HeaderText = "EC5"
        Me.EC5.Name = "EC5"
        Me.EC5.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.EC5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'detail
        '
        Me.detail.HeaderText = "Detail"
        Me.detail.Name = "detail"
        '
        'frmDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1235, 529)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.dgv)
        Me.Name = "frmDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Downtime Details"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgv As DataGridView
    Friend WithEvents btnSave As Guna.UI.WinForms.GunaAdvenceButton
    Friend WithEvents id As DataGridViewTextBoxColumn
    Friend WithEvents fecha As DataGridViewTextBoxColumn
    Friend WithEvents line As DataGridViewTextBoxColumn
    Friend WithEvents process As DataGridViewTextBoxColumn
    Friend WithEvents machineID As DataGridViewTextBoxColumn
    Friend WithEvents processName As DataGridViewTextBoxColumn
    Friend WithEvents machineStatus As DataGridViewTextBoxColumn
    Friend WithEvents start As DataGridViewTextBoxColumn
    Friend WithEvents endTime As DataGridViewTextBoxColumn
    Friend WithEvents lead As DataGridViewTextBoxColumn
    Friend WithEvents depto As DataGridViewComboBoxColumn
    Friend WithEvents EC1 As DataGridViewTextBoxColumn
    Friend WithEvents EC2 As DataGridViewTextBoxColumn
    Friend WithEvents EC3 As DataGridViewTextBoxColumn
    Friend WithEvents EC4 As DataGridViewTextBoxColumn
    Friend WithEvents EC5 As DataGridViewTextBoxColumn
    Friend WithEvents detail As DataGridViewTextBoxColumn
End Class
