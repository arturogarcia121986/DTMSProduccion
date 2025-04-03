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
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.line = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.process = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        Me.defects = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pStatus = New System.Windows.Forms.Panel()
        Me.rbIncomplete = New System.Windows.Forms.RadioButton()
        Me.rbAll = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbDepto = New Guna.UI.WinForms.GunaComboBox()
        Me.pFecha = New System.Windows.Forms.Panel()
        Me.dtpHasta = New Guna.UI.WinForms.GunaDateTimePicker()
        Me.dtpDesde = New Guna.UI.WinForms.GunaDateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GunaAdvenceButton1 = New Guna.UI.WinForms.GunaAdvenceButton()
        Me.btnExcel = New Guna.UI.WinForms.GunaAdvenceButton()
        Me.bntFiltrar = New Guna.UI.WinForms.GunaAdvenceButton()
        Me.btnSave = New Guna.UI.WinForms.GunaAdvenceButton()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pStatus.SuspendLayout()
        Me.pFecha.SuspendLayout()
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
        Me.dgv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id, Me.fecha, Me.line, Me.process, Me.machineStatus, Me.start, Me.endTime, Me.lead, Me.depto, Me.EC1, Me.EC2, Me.EC3, Me.EC4, Me.EC5, Me.detail, Me.defects})
        Me.dgv.Location = New System.Drawing.Point(2, 56)
        Me.dgv.Name = "dgv"
        Me.dgv.RowHeadersVisible = False
        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgv.Size = New System.Drawing.Size(1221, 505)
        Me.dgv.TabIndex = 1
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
        'defects
        '
        Me.defects.HeaderText = "Defects"
        Me.defects.Name = "defects"
        '
        'pStatus
        '
        Me.pStatus.Controls.Add(Me.rbIncomplete)
        Me.pStatus.Controls.Add(Me.rbAll)
        Me.pStatus.Controls.Add(Me.Label2)
        Me.pStatus.Controls.Add(Me.cbDepto)
        Me.pStatus.Location = New System.Drawing.Point(368, 15)
        Me.pStatus.Name = "pStatus"
        Me.pStatus.Size = New System.Drawing.Size(447, 37)
        Me.pStatus.TabIndex = 250
        '
        'rbIncomplete
        '
        Me.rbIncomplete.AutoSize = True
        Me.rbIncomplete.Checked = True
        Me.rbIncomplete.Location = New System.Drawing.Point(303, 10)
        Me.rbIncomplete.Name = "rbIncomplete"
        Me.rbIncomplete.Size = New System.Drawing.Size(128, 17)
        Me.rbIncomplete.TabIndex = 232
        Me.rbIncomplete.TabStop = True
        Me.rbIncomplete.Text = "Show only incomplete"
        Me.rbIncomplete.UseVisualStyleBackColor = True
        '
        'rbAll
        '
        Me.rbAll.AutoSize = True
        Me.rbAll.Location = New System.Drawing.Point(218, 11)
        Me.rbAll.Name = "rbAll"
        Me.rbAll.Size = New System.Drawing.Size(66, 17)
        Me.rbAll.TabIndex = 231
        Me.rbAll.Text = "Show All"
        Me.rbAll.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 230
        Me.Label2.Text = "Depto:"
        '
        'cbDepto
        '
        Me.cbDepto.BackColor = System.Drawing.Color.Transparent
        Me.cbDepto.BaseColor = System.Drawing.Color.White
        Me.cbDepto.BorderColor = System.Drawing.Color.Silver
        Me.cbDepto.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cbDepto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDepto.FocusedColor = System.Drawing.Color.DodgerBlue
        Me.cbDepto.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cbDepto.ForeColor = System.Drawing.Color.Black
        Me.cbDepto.FormattingEnabled = True
        Me.cbDepto.Items.AddRange(New Object() {"", "MATERIAL", "MAINTENANCE", "PRODUCTION", "QUALITY", "OTHERS"})
        Me.cbDepto.Location = New System.Drawing.Point(55, 6)
        Me.cbDepto.Margin = New System.Windows.Forms.Padding(4)
        Me.cbDepto.Name = "cbDepto"
        Me.cbDepto.OnHoverItemBaseColor = System.Drawing.Color.DodgerBlue
        Me.cbDepto.OnHoverItemForeColor = System.Drawing.Color.White
        Me.cbDepto.Radius = 5
        Me.cbDepto.Size = New System.Drawing.Size(134, 26)
        Me.cbDepto.TabIndex = 228
        '
        'pFecha
        '
        Me.pFecha.Controls.Add(Me.dtpHasta)
        Me.pFecha.Controls.Add(Me.dtpDesde)
        Me.pFecha.Controls.Add(Me.Label3)
        Me.pFecha.Controls.Add(Me.Label4)
        Me.pFecha.Location = New System.Drawing.Point(2, 13)
        Me.pFecha.Margin = New System.Windows.Forms.Padding(4)
        Me.pFecha.Name = "pFecha"
        Me.pFecha.Size = New System.Drawing.Size(359, 39)
        Me.pFecha.TabIndex = 249
        '
        'dtpHasta
        '
        Me.dtpHasta.BackColor = System.Drawing.Color.Transparent
        Me.dtpHasta.BaseColor = System.Drawing.Color.White
        Me.dtpHasta.BorderColor = System.Drawing.Color.Silver
        Me.dtpHasta.CustomFormat = Nothing
        Me.dtpHasta.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right
        Me.dtpHasta.FocusedColor = System.Drawing.Color.DodgerBlue
        Me.dtpHasta.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.dtpHasta.ForeColor = System.Drawing.Color.Black
        Me.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpHasta.Location = New System.Drawing.Point(231, 5)
        Me.dtpHasta.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpHasta.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpHasta.Name = "dtpHasta"
        Me.dtpHasta.OnHoverBaseColor = System.Drawing.Color.White
        Me.dtpHasta.OnHoverBorderColor = System.Drawing.Color.DodgerBlue
        Me.dtpHasta.OnHoverForeColor = System.Drawing.Color.Black
        Me.dtpHasta.OnPressedColor = System.Drawing.Color.Black
        Me.dtpHasta.Radius = 5
        Me.dtpHasta.Size = New System.Drawing.Size(113, 30)
        Me.dtpHasta.TabIndex = 229
        Me.dtpHasta.Text = "1/31/2023"
        Me.dtpHasta.Value = New Date(2023, 1, 31, 15, 57, 46, 926)
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
        Me.dtpDesde.Location = New System.Drawing.Point(56, 5)
        Me.dtpDesde.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpDesde.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDesde.Name = "dtpDesde"
        Me.dtpDesde.OnHoverBaseColor = System.Drawing.Color.White
        Me.dtpDesde.OnHoverBorderColor = System.Drawing.Color.DodgerBlue
        Me.dtpDesde.OnHoverForeColor = System.Drawing.Color.Black
        Me.dtpDesde.OnPressedColor = System.Drawing.Color.Black
        Me.dtpDesde.Radius = 5
        Me.dtpDesde.Size = New System.Drawing.Size(113, 30)
        Me.dtpDesde.TabIndex = 228
        Me.dtpDesde.Text = "1/31/2023"
        Me.dtpDesde.Value = New Date(2023, 1, 31, 15, 57, 46, 926)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 10)
        Me.Label3.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 20)
        Me.Label3.TabIndex = 177
        Me.Label3.Text = "From:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(198, 11)
        Me.Label4.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 20)
        Me.Label4.TabIndex = 178
        Me.Label4.Text = "To:"
        '
        'GunaAdvenceButton1
        '
        Me.GunaAdvenceButton1.AnimationHoverSpeed = 0.07!
        Me.GunaAdvenceButton1.AnimationSpeed = 0.03!
        Me.GunaAdvenceButton1.BackColor = System.Drawing.Color.Transparent
        Me.GunaAdvenceButton1.BaseColor = System.Drawing.Color.LightBlue
        Me.GunaAdvenceButton1.BorderColor = System.Drawing.Color.Black
        Me.GunaAdvenceButton1.CheckedBaseColor = System.Drawing.Color.Gray
        Me.GunaAdvenceButton1.CheckedBorderColor = System.Drawing.Color.Black
        Me.GunaAdvenceButton1.CheckedForeColor = System.Drawing.Color.White
        Me.GunaAdvenceButton1.CheckedImage = CType(resources.GetObject("GunaAdvenceButton1.CheckedImage"), System.Drawing.Image)
        Me.GunaAdvenceButton1.CheckedLineColor = System.Drawing.Color.DimGray
        Me.GunaAdvenceButton1.DialogResult = System.Windows.Forms.DialogResult.None
        Me.GunaAdvenceButton1.FocusedColor = System.Drawing.Color.Empty
        Me.GunaAdvenceButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GunaAdvenceButton1.ForeColor = System.Drawing.Color.Black
        Me.GunaAdvenceButton1.Image = Global.DTMS.My.Resources.Resources.Text
        Me.GunaAdvenceButton1.ImageSize = New System.Drawing.Size(20, 20)
        Me.GunaAdvenceButton1.LineColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.GunaAdvenceButton1.Location = New System.Drawing.Point(995, 15)
        Me.GunaAdvenceButton1.Name = "GunaAdvenceButton1"
        Me.GunaAdvenceButton1.OnHoverBaseColor = System.Drawing.Color.SteelBlue
        Me.GunaAdvenceButton1.OnHoverBorderColor = System.Drawing.Color.Black
        Me.GunaAdvenceButton1.OnHoverForeColor = System.Drawing.Color.White
        Me.GunaAdvenceButton1.OnHoverImage = Nothing
        Me.GunaAdvenceButton1.OnHoverLineColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.GunaAdvenceButton1.OnPressedColor = System.Drawing.Color.Black
        Me.GunaAdvenceButton1.Radius = 10
        Me.GunaAdvenceButton1.Size = New System.Drawing.Size(85, 33)
        Me.GunaAdvenceButton1.TabIndex = 252
        Me.GunaAdvenceButton1.Text = "History"
        Me.GunaAdvenceButton1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnExcel
        '
        Me.btnExcel.AnimationHoverSpeed = 0.07!
        Me.btnExcel.AnimationSpeed = 0.03!
        Me.btnExcel.BackColor = System.Drawing.Color.Transparent
        Me.btnExcel.BaseColor = System.Drawing.Color.LightGreen
        Me.btnExcel.BorderColor = System.Drawing.Color.Black
        Me.btnExcel.CheckedBaseColor = System.Drawing.Color.Gray
        Me.btnExcel.CheckedBorderColor = System.Drawing.Color.Black
        Me.btnExcel.CheckedForeColor = System.Drawing.Color.White
        Me.btnExcel.CheckedImage = CType(resources.GetObject("btnExcel.CheckedImage"), System.Drawing.Image)
        Me.btnExcel.CheckedLineColor = System.Drawing.Color.DimGray
        Me.btnExcel.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnExcel.FocusedColor = System.Drawing.Color.Empty
        Me.btnExcel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExcel.ForeColor = System.Drawing.Color.Black
        Me.btnExcel.Image = Global.DTMS.My.Resources.Resources.xs2
        Me.btnExcel.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnExcel.LineColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.btnExcel.Location = New System.Drawing.Point(1086, 12)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.OnHoverBaseColor = System.Drawing.Color.SeaGreen
        Me.btnExcel.OnHoverBorderColor = System.Drawing.Color.Black
        Me.btnExcel.OnHoverForeColor = System.Drawing.Color.White
        Me.btnExcel.OnHoverImage = Nothing
        Me.btnExcel.OnHoverLineColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.btnExcel.OnPressedColor = System.Drawing.Color.Black
        Me.btnExcel.Radius = 10
        Me.btnExcel.Size = New System.Drawing.Size(137, 36)
        Me.btnExcel.TabIndex = 251
        Me.btnExcel.Text = "Download Excel"
        Me.btnExcel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.bntFiltrar.Location = New System.Drawing.Point(860, 13)
        Me.bntFiltrar.Name = "bntFiltrar"
        Me.bntFiltrar.OnHoverBaseColor = System.Drawing.Color.SteelBlue
        Me.bntFiltrar.OnHoverBorderColor = System.Drawing.Color.Black
        Me.bntFiltrar.OnHoverForeColor = System.Drawing.Color.White
        Me.bntFiltrar.OnHoverImage = Nothing
        Me.bntFiltrar.OnHoverLineColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.bntFiltrar.OnPressedColor = System.Drawing.Color.Black
        Me.bntFiltrar.Radius = 10
        Me.bntFiltrar.Size = New System.Drawing.Size(42, 36)
        Me.bntFiltrar.TabIndex = 248
        Me.bntFiltrar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.btnSave.Location = New System.Drawing.Point(1156, 567)
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
        'frmDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1235, 623)
        Me.Controls.Add(Me.GunaAdvenceButton1)
        Me.Controls.Add(Me.btnExcel)
        Me.Controls.Add(Me.pStatus)
        Me.Controls.Add(Me.pFecha)
        Me.Controls.Add(Me.bntFiltrar)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.dgv)
        Me.Name = "frmDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Downtime Details"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pStatus.ResumeLayout(False)
        Me.pStatus.PerformLayout()
        Me.pFecha.ResumeLayout(False)
        Me.pFecha.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgv As DataGridView
    Friend WithEvents btnSave As Guna.UI.WinForms.GunaAdvenceButton
    Friend WithEvents pStatus As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents cbDepto As Guna.UI.WinForms.GunaComboBox
    Friend WithEvents pFecha As Panel
    Friend WithEvents dtpHasta As Guna.UI.WinForms.GunaDateTimePicker
    Friend WithEvents dtpDesde As Guna.UI.WinForms.GunaDateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents bntFiltrar As Guna.UI.WinForms.GunaAdvenceButton
    Friend WithEvents rbIncomplete As RadioButton
    Friend WithEvents rbAll As RadioButton
    Friend WithEvents id As DataGridViewTextBoxColumn
    Friend WithEvents fecha As DataGridViewTextBoxColumn
    Friend WithEvents line As DataGridViewTextBoxColumn
    Friend WithEvents process As DataGridViewTextBoxColumn
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
    Friend WithEvents defects As DataGridViewTextBoxColumn
    Friend WithEvents btnExcel As Guna.UI.WinForms.GunaAdvenceButton
    Friend WithEvents GunaAdvenceButton1 As Guna.UI.WinForms.GunaAdvenceButton
End Class
