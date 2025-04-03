<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScreenshots
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmScreenshots))
        Me.dtpDesde = New Guna.UI.WinForms.GunaDateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.bntFiltrar = New Guna.UI.WinForms.GunaAdvenceButton()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.dtpDesde.Location = New System.Drawing.Point(66, 12)
        Me.dtpDesde.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpDesde.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDesde.Name = "dtpDesde"
        Me.dtpDesde.OnHoverBaseColor = System.Drawing.Color.White
        Me.dtpDesde.OnHoverBorderColor = System.Drawing.Color.DodgerBlue
        Me.dtpDesde.OnHoverForeColor = System.Drawing.Color.Black
        Me.dtpDesde.OnPressedColor = System.Drawing.Color.Black
        Me.dtpDesde.Radius = 5
        Me.dtpDesde.Size = New System.Drawing.Size(113, 30)
        Me.dtpDesde.TabIndex = 250
        Me.dtpDesde.Text = "1/31/2023"
        Me.dtpDesde.Value = New Date(2023, 1, 31, 15, 57, 46, 926)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 17)
        Me.Label3.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 20)
        Me.Label3.TabIndex = 249
        Me.Label3.Text = "From:"
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
        Me.bntFiltrar.Location = New System.Drawing.Point(197, 9)
        Me.bntFiltrar.Name = "bntFiltrar"
        Me.bntFiltrar.OnHoverBaseColor = System.Drawing.Color.SteelBlue
        Me.bntFiltrar.OnHoverBorderColor = System.Drawing.Color.Black
        Me.bntFiltrar.OnHoverForeColor = System.Drawing.Color.White
        Me.bntFiltrar.OnHoverImage = Nothing
        Me.bntFiltrar.OnHoverLineColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.bntFiltrar.OnPressedColor = System.Drawing.Color.Black
        Me.bntFiltrar.Radius = 10
        Me.bntFiltrar.Size = New System.Drawing.Size(42, 36)
        Me.bntFiltrar.TabIndex = 251
        Me.bntFiltrar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'PictureBox5
        '
        Me.PictureBox5.Location = New System.Drawing.Point(17, 48)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(1102, 650)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox5.TabIndex = 252
        Me.PictureBox5.TabStop = False
        '
        'frmScreenshots
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1142, 725)
        Me.Controls.Add(Me.PictureBox5)
        Me.Controls.Add(Me.bntFiltrar)
        Me.Controls.Add(Me.dtpDesde)
        Me.Controls.Add(Me.Label3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmScreenshots"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Images"
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents bntFiltrar As Guna.UI.WinForms.GunaAdvenceButton
    Friend WithEvents dtpDesde As Guna.UI.WinForms.GunaDateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents PictureBox5 As PictureBox
End Class
