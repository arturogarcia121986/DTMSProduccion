<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBreaktime
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBreaktime))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbLinea = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lbb4 = New System.Windows.Forms.Label()
        Me.lbb3 = New System.Windows.Forms.Label()
        Me.lbl2 = New System.Windows.Forms.Label()
        Me.lbs2 = New System.Windows.Forms.Label()
        Me.lbb2 = New System.Windows.Forms.Label()
        Me.lbl1 = New System.Windows.Forms.Label()
        Me.lbb1 = New System.Windows.Forms.Label()
        Me.lbs1 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cbBr4 = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cbBr3 = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cbL2 = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cbSt2 = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cbBr2 = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbL1 = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbBr1 = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbSt1 = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnSave = New Guna.UI.WinForms.GunaAdvenceButton()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Line:"
        '
        'cbLinea
        '
        Me.cbLinea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLinea.FormattingEnabled = True
        Me.cbLinea.Items.AddRange(New Object() {"M1", "M2", "M3", "M4", "M5"})
        Me.cbLinea.Location = New System.Drawing.Point(75, 10)
        Me.cbLinea.Name = "cbLinea"
        Me.cbLinea.Size = New System.Drawing.Size(72, 24)
        Me.cbLinea.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.Label27)
        Me.GroupBox1.Controls.Add(Me.Label28)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.lbb4)
        Me.GroupBox1.Controls.Add(Me.lbb3)
        Me.GroupBox1.Controls.Add(Me.lbl2)
        Me.GroupBox1.Controls.Add(Me.lbs2)
        Me.GroupBox1.Controls.Add(Me.lbb2)
        Me.GroupBox1.Controls.Add(Me.lbl1)
        Me.GroupBox1.Controls.Add(Me.lbb1)
        Me.GroupBox1.Controls.Add(Me.lbs1)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.cbBr4)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cbBr3)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.cbL2)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.cbSt2)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.cbBr2)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cbL1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cbBr1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cbSt1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(36, 52)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(313, 334)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Planned downtime"
        '
        'lbb4
        '
        Me.lbb4.AutoSize = True
        Me.lbb4.Location = New System.Drawing.Point(205, 294)
        Me.lbb4.Name = "lbb4"
        Me.lbb4.Size = New System.Drawing.Size(22, 16)
        Me.lbb4.TabIndex = 27
        Me.lbb4.Text = "15"
        '
        'lbb3
        '
        Me.lbb3.AutoSize = True
        Me.lbb3.Location = New System.Drawing.Point(205, 266)
        Me.lbb3.Name = "lbb3"
        Me.lbb3.Size = New System.Drawing.Size(22, 16)
        Me.lbb3.TabIndex = 26
        Me.lbb3.Text = "15"
        '
        'lbl2
        '
        Me.lbl2.AutoSize = True
        Me.lbl2.Location = New System.Drawing.Point(205, 235)
        Me.lbl2.Name = "lbl2"
        Me.lbl2.Size = New System.Drawing.Size(22, 16)
        Me.lbl2.TabIndex = 25
        Me.lbl2.Text = "30"
        '
        'lbs2
        '
        Me.lbs2.AutoSize = True
        Me.lbs2.Location = New System.Drawing.Point(205, 206)
        Me.lbs2.Name = "lbs2"
        Me.lbs2.Size = New System.Drawing.Size(22, 16)
        Me.lbs2.TabIndex = 24
        Me.lbs2.Text = "15"
        '
        'lbb2
        '
        Me.lbb2.AutoSize = True
        Me.lbb2.Location = New System.Drawing.Point(205, 131)
        Me.lbb2.Name = "lbb2"
        Me.lbb2.Size = New System.Drawing.Size(22, 16)
        Me.lbb2.TabIndex = 23
        Me.lbb2.Text = "15"
        '
        'lbl1
        '
        Me.lbl1.AutoSize = True
        Me.lbl1.Location = New System.Drawing.Point(205, 103)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(22, 16)
        Me.lbl1.TabIndex = 22
        Me.lbl1.Text = "30"
        '
        'lbb1
        '
        Me.lbb1.AutoSize = True
        Me.lbb1.Location = New System.Drawing.Point(205, 73)
        Me.lbb1.Name = "lbb1"
        Me.lbb1.Size = New System.Drawing.Size(22, 16)
        Me.lbb1.TabIndex = 21
        Me.lbb1.Text = "15"
        '
        'lbs1
        '
        Me.lbs1.AutoSize = True
        Me.lbs1.Location = New System.Drawing.Point(205, 43)
        Me.lbs1.Name = "lbs1"
        Me.lbs1.Size = New System.Drawing.Size(22, 16)
        Me.lbs1.TabIndex = 20
        Me.lbs1.Text = "15"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(204, 17)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(58, 16)
        Me.Label12.TabIndex = 19
        Me.Label12.Text = "Duration"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(15, 173)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(67, 16)
        Me.Label11.TabIndex = 18
        Me.Label11.Text = "Night Shift"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(15, 18)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(61, 16)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "Day Shift"
        '
        'cbBr4
        '
        Me.cbBr4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBr4.FormattingEnabled = True
        Me.cbBr4.Items.AddRange(New Object() {"00:00", "00:15", "00:30", "00:45", "01:00", "01:15", "01:30", "01:45", "02:00", "02:15", "02:30", "02:45", "03:00", "03:15", "03:30", "03:45", "04:00", "04:15", "04:30", "04:45", "05:00", "05:15", "05:30", "05:45", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.cbBr4.Location = New System.Drawing.Point(121, 290)
        Me.cbBr4.Name = "cbBr4"
        Me.cbBr4.Size = New System.Drawing.Size(72, 24)
        Me.cbBr4.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(36, 294)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 16)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Break time 2"
        '
        'cbBr3
        '
        Me.cbBr3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBr3.FormattingEnabled = True
        Me.cbBr3.Items.AddRange(New Object() {"00:00", "00:15", "00:30", "00:45", "01:00", "01:15", "01:30", "01:45", "02:00", "02:15", "02:30", "02:45", "03:00", "03:15", "03:30", "03:45", "04:00", "04:15", "04:30", "04:45", "05:00", "05:15", "05:30", "05:45", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.cbBr3.Location = New System.Drawing.Point(121, 260)
        Me.cbBr3.Name = "cbBr3"
        Me.cbBr3.Size = New System.Drawing.Size(72, 24)
        Me.cbBr3.TabIndex = 14
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(36, 264)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 16)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Break time 1"
        '
        'cbL2
        '
        Me.cbL2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbL2.FormattingEnabled = True
        Me.cbL2.Items.AddRange(New Object() {"00:00", "00:15", "00:30", "00:45", "01:00", "01:15", "01:30", "01:45", "02:00", "02:15", "02:30", "02:45", "03:00", "03:15", "03:30", "03:45", "04:00", "04:15", "04:30", "04:45", "05:00", "05:15", "05:30", "05:45", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.cbL2.Location = New System.Drawing.Point(121, 230)
        Me.cbL2.Name = "cbL2"
        Me.cbL2.Size = New System.Drawing.Size(72, 24)
        Me.cbL2.TabIndex = 12
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(75, 234)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 16)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Lunch"
        '
        'cbSt2
        '
        Me.cbSt2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSt2.FormattingEnabled = True
        Me.cbSt2.Items.AddRange(New Object() {"00:00", "00:15", "00:30", "00:45", "01:00", "01:15", "01:30", "01:45", "02:00", "02:15", "02:30", "02:45", "03:00", "03:15", "03:30", "03:45", "04:00", "04:15", "04:30", "04:45", "05:00", "05:15", "05:30", "05:45", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.cbSt2.Location = New System.Drawing.Point(121, 200)
        Me.cbSt2.Name = "cbSt2"
        Me.cbSt2.Size = New System.Drawing.Size(72, 24)
        Me.cbSt2.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(36, 203)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(83, 16)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "Startup (2nd)"
        '
        'cbBr2
        '
        Me.cbBr2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBr2.FormattingEnabled = True
        Me.cbBr2.Items.AddRange(New Object() {"00:00", "00:15", "00:30", "00:45", "01:00", "01:15", "01:30", "01:45", "02:00", "02:15", "02:30", "02:45", "03:00", "03:15", "03:30", "03:45", "04:00", "04:15", "04:30", "04:45", "05:00", "05:15", "05:30", "05:45", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.cbBr2.Location = New System.Drawing.Point(121, 128)
        Me.cbBr2.Name = "cbBr2"
        Me.cbBr2.Size = New System.Drawing.Size(72, 24)
        Me.cbBr2.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(36, 132)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 16)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Break time 2"
        '
        'cbL1
        '
        Me.cbL1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbL1.FormattingEnabled = True
        Me.cbL1.Items.AddRange(New Object() {"00:00", "00:15", "00:30", "00:45", "01:00", "01:15", "01:30", "01:45", "02:00", "02:15", "02:30", "02:45", "03:00", "03:15", "03:30", "03:45", "04:00", "04:15", "04:30", "04:45", "05:00", "05:15", "05:30", "05:45", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.cbL1.Location = New System.Drawing.Point(121, 98)
        Me.cbL1.Name = "cbL1"
        Me.cbL1.Size = New System.Drawing.Size(72, 24)
        Me.cbL1.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(75, 102)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 16)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Lunch"
        '
        'cbBr1
        '
        Me.cbBr1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBr1.FormattingEnabled = True
        Me.cbBr1.Items.AddRange(New Object() {"00:00", "00:15", "00:30", "00:45", "01:00", "01:15", "01:30", "01:45", "02:00", "02:15", "02:30", "02:45", "03:00", "03:15", "03:30", "03:45", "04:00", "04:15", "04:30", "04:45", "05:00", "05:15", "05:30", "05:45", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.cbBr1.Location = New System.Drawing.Point(121, 68)
        Me.cbBr1.Name = "cbBr1"
        Me.cbBr1.Size = New System.Drawing.Size(72, 24)
        Me.cbBr1.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(36, 72)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 16)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Break time 1"
        '
        'cbSt1
        '
        Me.cbSt1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSt1.FormattingEnabled = True
        Me.cbSt1.Items.AddRange(New Object() {"00:00", "00:15", "00:30", "00:45", "01:00", "01:15", "01:30", "01:45", "02:00", "02:15", "02:30", "02:45", "03:00", "03:15", "03:30", "03:45", "04:00", "04:15", "04:30", "04:45", "05:00", "05:15", "05:30", "05:45", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.cbSt1.Location = New System.Drawing.Point(121, 38)
        Me.cbSt1.Name = "cbSt1"
        Me.cbSt1.Size = New System.Drawing.Size(72, 24)
        Me.cbSt1.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(40, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Startup (1st)"
        '
        'btnSave
        '
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
        Me.btnSave.Image = Global.BMAmonitoring.My.Resources.Resources.disk
        Me.btnSave.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnSave.LineColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.btnSave.Location = New System.Drawing.Point(124, 408)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.OnHoverBaseColor = System.Drawing.Color.SteelBlue
        Me.btnSave.OnHoverBorderColor = System.Drawing.Color.Black
        Me.btnSave.OnHoverForeColor = System.Drawing.Color.White
        Me.btnSave.OnHoverImage = Nothing
        Me.btnSave.OnHoverLineColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.btnSave.OnPressedColor = System.Drawing.Color.Black
        Me.btnSave.Radius = 10
        Me.btnSave.Size = New System.Drawing.Size(75, 33)
        Me.btnSave.TabIndex = 60
        Me.btnSave.Text = "Save"
        Me.btnSave.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(233, 43)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(29, 16)
        Me.Label21.TabIndex = 28
        Me.Label21.Text = "min"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(233, 72)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(29, 16)
        Me.Label22.TabIndex = 29
        Me.Label22.Text = "min"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(233, 103)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(29, 16)
        Me.Label23.TabIndex = 30
        Me.Label23.Text = "min"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(233, 132)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(29, 16)
        Me.Label24.TabIndex = 31
        Me.Label24.Text = "min"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(233, 295)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(29, 16)
        Me.Label25.TabIndex = 35
        Me.Label25.Text = "min"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(233, 266)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(29, 16)
        Me.Label26.TabIndex = 34
        Me.Label26.Text = "min"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(233, 235)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(29, 16)
        Me.Label27.TabIndex = 33
        Me.Label27.Text = "min"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(233, 206)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(29, 16)
        Me.Label28.TabIndex = 32
        Me.Label28.Text = "min"
        '
        'frmBreaktime
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 453)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cbLinea)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "frmBreaktime"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Edit Breaktime"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents cbLinea As ComboBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cbSt1 As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents cbBr4 As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cbBr3 As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents cbL2 As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents cbSt2 As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents cbBr2 As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cbL1 As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cbBr1 As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btnSave As Guna.UI.WinForms.GunaAdvenceButton
    Friend WithEvents lbb4 As Label
    Friend WithEvents lbb3 As Label
    Friend WithEvents lbl2 As Label
    Friend WithEvents lbs2 As Label
    Friend WithEvents lbb2 As Label
    Friend WithEvents lbl1 As Label
    Friend WithEvents lbb1 As Label
    Friend WithEvents lbs1 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents Label28 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents Label21 As Label
End Class
