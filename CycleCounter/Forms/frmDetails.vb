Public Class frmDetails
    Dim procesos As String = ""
    Dim _linea As String = ""
    Dim nomLinea As String = ""
    Dim firstLoad As Boolean = False
    Private selectedIndex As Integer
    Dim folioSelected As String = ""

    Public Sub New(linea As String)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        _linea = linea
    End Sub

    Public iResult As Boolean = False
    Private Sub frmDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Text = " DETAILS FOR " & _linea
            Select Case _linea
                Case "M1"
                    procesos = " (process='1100' OR process='1400' or process='1700') "
                Case "M2"
                    procesos = " (process='2100' OR process='2400' or process='2700') "
                Case "M3"
                    procesos = " (process='3100' OR process='3400' or process='3700') "
                Case "M4"
                    procesos = " (process='4100' OR process='4400' or process='4700') "
                Case "M5"
                    procesos = " (process='5100' OR process='5400' or process='5700') "
                Case "1100"
                    procesos = " (process='1100') "
                    _linea = "M1"
                Case "1400"
                    procesos = " (process='1400') "
                    _linea = "M1"
                Case "1700"
                    procesos = " (process='1700') "
                    _linea = "M1"
                Case "2100"
                    procesos = " (process='2100') "
                    _linea = "M2"
                Case "2400"
                    procesos = " (process='2400') "
                    _linea = "M2"
                Case "2700"
                    procesos = " (process='2700') "
                    _linea = "M2"
                Case "3100"
                    procesos = " (process='3100') "
                    _linea = "M3"
                Case "3400"
                    procesos = " (process='3400') "
                    _linea = "M3"
                Case "3700"
                    procesos = " (process='3700') "
                    _linea = "M3"
                Case "4100"
                    procesos = " (process='4100') "
                    _linea = "M4"
                Case "4400"
                    procesos = " (process='4400') "
                    _linea = "M4"
                Case "4700"
                    procesos = " (process='4700') "
                    _linea = "M4"
                Case "5100"
                    procesos = " (process='5100') "
                    _linea = "M5"
                Case "5400"
                    procesos = " (process='5400') "
                    _linea = "M5"
                Case "5700"
                    procesos = " (process='5700') "
                    _linea = "M5"
            End Select

            Dim query As String = "SELECT TOP (1000) do.id,
                                CASE
                                    WHEN process ='1100' or process='2100' or process='3100'  or process='4100' or process='5100' THEN '100'
	                                 WHEN process ='1400' or process='2400' or process='3400'  or process='4400' or process='5400' THEN '400'
	                                 WHEN process ='1700' or process='2700' or process='3700'  or process='4700' or process='5700' THEN '700'

	                                 end as process
                                  ,[status]
                                  ,[startTime]
                                  ,[endTime]
                                  ,[AcumTime]
                                  ,convert(date,do.[insert_date]) as insert_date,alarmedDepto as depto,remarks,EC1,ec2,ec3,ec4,ec5

                            FROM [db_kyungshin].[dbo].[t_bma_downtime] as do
                           
                              WHERE (status='DOWN' and " & procesos & " AND acumtime>'00:05:00' AND convert(date, do.insert_date)='" &
                              ConvierteAdateMySQL(Date.Now.ToShortDateString) & "') 
                              OR (status='DOWN' and " & procesos & " AND acumtime='' AND convert(date, do.insert_date)='" &
                              ConvierteAdateMySQL(Date.Now.ToShortDateString) & "' AND step=1)
                              ORDER BY process,id"

            'CODE
            '                ,(select distinct STRING_AGG(codigo,',') from t_bma_alarmCodes a
            'where  ((proceso = '1100e' ) and (process='1100')and (convert(time,horaDeInsert) between startTime and endTime) and convert(date,fecha) = convert(date,do.insert_date)) 
            'or ((proceso = '1400e') and (process='1400')and (convert(time,horaDeInsert) between startTime and endTime) and convert(date,fecha) =      convert(date,do.insert_date)
            'or (proceso = '1700e') and (process='1700')and (convert(time,horaDeInsert) between startTime and endTime) and convert(date,fecha) =       convert(date,do.insert_date))) as code

            'inner join t_bma_breaktime br on br.line='" & _linea & "' 
            '               AND  (br.lunch1fin not between do.startTime and do.endTime 
            '               and br.lunch1 not between do.startTime and do.endTime 
            '               and br.break1 not between do.startTime and do.endTime 
            '               and br.break1fin not between do.startTime and do.endTime 
            '               and br.break2 not between do.startTime and do.endTime 
            '               and br.break2fin not between do.startTime and do.endTime 
            '               and br.startup2 not between do.startTime and do.endTime 
            '               and br.startup2fin not between do.startTime and do.endTime 
            '               and br.startup1 not between do.startTime and do.endTime 
            '               and br.startup1Fin not between do.startTime and do.endTime 
            '               and br.lunch2 not between do.startTime and do.endTime 
            '               and br.lunch2fin not between do.startTime and do.endTime 
            '               and br.break3 not between do.startTime and do.endTime 
            '               and br.break3fin not between do.startTime and do.endTime 
            '               and br.break4 not between do.startTime and do.endTime 
            '               and br.break4fin not between do.startTime and do.endTime 
            '               )

            '(select distinct STRING_AGG(codigo,',') from t_bma_alarmCodes a
            'where  ((proceso = '2100e' ) and (process='2100')and (convert(time,horaDeInsert) between startTime and endTime) and convert(date,fecha) = convert(date,insert_date)) 
            'or ((proceso = '2400e') and (process='2400')and (convert(time,horaDeInsert) between startTime and endTime) and convert(date,fecha) = convert(date,insert_date)
            'or (proceso = '2700e') and (process='2700')and (convert(time,horaDeInsert) between startTime and endTime) and convert(date,fecha) = convert(date,insert_date))) as code

            '(select distinct STRING_AGG(codigo,',') from t_bma_alarmCodes a
            'where  ((proceso = '3100e' ) and (process='3100')and (convert(time,horaDeInsert) between startTime and endTime) and convert(date,fecha) = convert(date,insert_date)) 
            'or ((proceso = '3400e') and (process='3400')and (convert(time,horaDeInsert) between startTime and endTime) and convert(date,fecha) = convert(date,insert_date)
            'or (proceso = '3700e') and (process='3700')and (convert(time,horaDeInsert) between startTime and endTime) and convert(date,fecha) = convert(date,insert_date))) as code

            Dim arre2 As DataRowCollection = GetRows(query)

            If Not IsNothing(arre2) Then
                'For Each item As DataRow In arre2
                '    dgv.Rows.Add(item("id").ToString.Trim, ConvierteFechaMySQL(item("insert_date").ToString.Trim), _linea,
                '                 item("process").ToString.Trim, "", "", item("status").ToString.Trim, item("startTime").ToString.Trim,
                '                    item("endTime").ToString.Trim, item("AcumTime").ToString.Trim, item("depto").ToString.Trim,
                '                     item("remarks").ToString.Trim,
                '                     item("EC1").ToString.Trim, item("EC2").ToString.Trim, item("EC3").ToString.Trim, item("EC4").ToString.Trim, item("EC5").ToString.Trim)

                'Next

                For Each item As DataRow In arre2
                    Dim newRow As DataGridViewRow = dgv.Rows(dgv.Rows.Add())

                    ' Asignar valores a las celdas normales
                    newRow.Cells("id").Value = item("id").ToString.Trim
                    newRow.Cells("depto").Value = item("depto").ToString.Trim
                    newRow.Cells("fecha").Value = ConvierteFechaMySQL(item("insert_date").ToString.Trim)
                    newRow.Cells("process").Value = item("process").ToString.Trim
                    newRow.Cells("machinestatus").Value = item("status").ToString.Trim
                    newRow.Cells("start").Value = item("startTime").ToString.Trim
                    newRow.Cells("endtime").Value = item("endtime").ToString.Trim
                    newRow.Cells("lead").Value = item("AcumTime").ToString.Trim
                    newRow.Cells("detail").Value = item("remarks").ToString.Trim

                    newRow.Cells("EC1").Value = item("EC1").ToString.Trim
                    newRow.Cells("EC2").Value = item("EC2").ToString.Trim
                    newRow.Cells("EC3").Value = item("EC3").ToString.Trim
                    newRow.Cells("EC4").Value = item("EC4").ToString.Trim
                    newRow.Cells("EC5").Value = item("EC5").ToString.Trim

                    ' ... (otros valores)
                    Dim cellDepto As DataGridViewComboBoxCell = CType(newRow.Cells("depto"), DataGridViewComboBoxCell)

                    If Not cellDepto.Items.Contains(item("depto").ToString.Trim) Then
                        cellDepto.Items.Add(item("depto").ToString.Trim)
                    End If

                    cellDepto.Value = item("depto").ToString.Trim


                    If Not String.IsNullOrEmpty(newRow.Cells("EC1").Value) Then
                        newRow.Cells("EC1").ReadOnly = True
                    End If

                    If Not String.IsNullOrEmpty(cellDepto.Value) Then
                        ' cellDepto.ReadOnly = True
                        IIf(cellDepto.Value <> "MATERIAL", cellDepto.Items.Add("MATERIAL"), Nothing)
                        IIf(cellDepto.Value <> "MAINTENANCE", cellDepto.Items.Add("MAINTENANCE"), Nothing)
                        IIf(cellDepto.Value <> "PRODUCTION", cellDepto.Items.Add("PRODUCTION"), Nothing)
                        IIf(cellDepto.Value <> "QUALITY", cellDepto.Items.Add("QUALITY"), Nothing)
                        IIf(cellDepto.Value <> "OTHER", cellDepto.Items.Add("OTHER"), Nothing)

                    Else
                        cellDepto.ReadOnly = False
                        cellDepto.Items.Add("MATERIAL")
                        cellDepto.Items.Add("MAINTENANCE")
                        cellDepto.Items.Add("PRODUCTION")
                        cellDepto.Items.Add("QUALITY")
                        cellDepto.Items.Add("OTHER")
                    End If
                Next


                colores()

            End If
            'LoadCombos()
            'dgv.DataSource = EjecutaSelects(query, "buscahistorial")
            ResizeCols(dgv)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub colores()
        For Each rw As DataGridViewRow In dgv.Rows
            If rw.Cells("depto").Value.ToString = "PRODUCTION" And rw.Cells("EC1").Value.ToString <> "" Then
                rw.Visible = False
            End If

            If rw.Cells("depto").Value.ToString = "MAINTENANCE" And
                rw.Cells("EC1").Value.ToString <> "" And
                 rw.Cells("EC2").Value.ToString <> "" And
                  rw.Cells("EC3").Value.ToString <> "" And
                   rw.Cells("EC4").Value.ToString <> "" And
                    rw.Cells("EC5").Value.ToString <> "" Then
                rw.Visible = False
            End If

            If rw.Cells("depto").Value.ToString = "QUALITY" And
                rw.Cells("EC1").Value.ToString <> "" And
                 rw.Cells("EC2").Value.ToString <> "" And
                  rw.Cells("EC3").Value.ToString <> "" And
                   rw.Cells("EC4").Value.ToString <> "" Then
                rw.Visible = False
            End If

            If rw.Cells("depto").Value.ToString = "MATERIAL" And
              rw.Cells("EC1").Value.ToString <> "" And
               rw.Cells("EC2").Value.ToString <> "" Then
                rw.Visible = False
            End If
        Next
    End Sub

    Dim value As String = ""
    Dim cbec1, cbec2, cbec3, cbec4, cbec5 As New DataGridViewComboBoxCell

    Private Sub dgv_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgv.DataError
        e.Cancel = True
    End Sub

    Dim arre2 As DataRowCollection

    Private Sub dgv_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellClick
        If dgv.Columns(e.ColumnIndex).Name = "EC1" OrElse dgv.Columns(e.ColumnIndex).Name = "EC2" OrElse dgv.Columns(e.ColumnIndex).Name = "EC3" OrElse
            dgv.Columns(e.ColumnIndex).Name = "EC4" OrElse dgv.Columns(e.ColumnIndex).Name = "EC5" Then

            selectedIndex = dgv.CurrentRow.Index
            folioSelected = dgv.Rows(selectedIndex).Cells("depto").Value

            Dim frm As New frmListaCodes(dgv.Columns(e.ColumnIndex).Name, folioSelected)
            frm.ShowDialog()
            ' Mostrar el formulario como un diálogo modal
            If frm.iResult.code <> "" Then
                dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = frm.iResult.code ' Suponiendo que el formulario tiene un ComboBox llamado ComboBoxValue
            End If
        End If
    End Sub

    Dim query As String

    Private Sub LoadCombos()
        value = ""
        query = ""
        For Each row As DataGridViewRow In dgv.Rows
            If Not row.IsNewRow Then
                Try

                    value = row.Cells("depto").Value.ToString() ' Reemplaza "NombreColumna" por el nombre de la columna que quieres verificar
                    arre2 = GetRows("SELECT [code],NIVEL
                                FROM [db_kyungshin].[dbo].[t_bma_dtms_codes] 
                                WHERE depto='" & value & "'")

                    cbec1 = CType(row.Cells("EC1"), DataGridViewComboBoxCell) ' Reemplaza "ColumnaComboBox" por el nombre de la columna del ComboBox
                    cbec2 = CType(row.Cells("EC2"), DataGridViewComboBoxCell)
                    cbec3 = CType(row.Cells("EC3"), DataGridViewComboBoxCell)
                    cbec4 = CType(row.Cells("EC4"), DataGridViewComboBoxCell)
                    cbec5 = CType(row.Cells("EC5"), DataGridViewComboBoxCell)

                    If Not IsNothing(arre2) Then
                        For Each item As DataRow In arre2
                            Select Case item("nivel").ToString.Trim
                                Case "EC1"
                                    cbec1.Items.Add(item("code").ToString.Trim)
                                Case "EC2"
                                    cbec2.Items.Add(item("code").ToString.Trim)
                                Case "EC3"
                                    cbec3.Items.Add(item("code").ToString.Trim)
                                Case "EC4"
                                    cbec4.Items.Add(item("code").ToString.Trim)
                                Case "EC5"
                                    cbec5.Items.Add(item("code").ToString.Trim)
                            End Select
                        Next
                    End If
                Catch ex As Exception

                End Try



                '' Verifica si el valor ya existe en el ComboBox
                'If Not comboBoxCell.Items.Contains(value) Then

                'End If
            End If
        Next
    End Sub
    Private Sub DataGridView1_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgv.RowsAdded

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            iResult = True
            Dim query As String = ""
            For Each rw As DataGridViewRow In dgv.Rows
                query = "UPDATE t_bma_downtime Set depto='" & rw.Cells("depto").Value.ToString &
                    "',remarks='" & rw.Cells("detail").Value.ToString &
                     "',ec1='" & rw.Cells("EC1").Value.ToString &
                     "',ec2='" & rw.Cells("EC2").Value.ToString &
                     "',ec3='" & rw.Cells("EC3").Value.ToString &
                     "',ec4='" & rw.Cells("EC4").Value.ToString &
                     "',ec5='" & rw.Cells("EC5").Value.ToString &
                     "',alarmedDepto='" & rw.Cells("depto").Value.ToString &
                "' WHERE id='" & rw.Cells("id").Value.ToString & "'"

                Ejecuta(query, "updBMA DT")

            Next

            colores()
            MessageBox.Show("The data was saved succesfully.", "Details", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class