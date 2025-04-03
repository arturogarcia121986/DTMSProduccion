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

    Private Sub asignarStringDesdeLinea()
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
    End Sub
    Private Sub frmDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Text = " DETAILS FOR " & _linea
            asignarStringDesdeLinea()

            Dim query As String = "SELECT TOP (1000) do.id," &
                    "LEFT(process, 1) AS linea," & _ 'Nueva columna: línea
                    "CASE" &
                        " WHEN process LIKE '[1-5]100' THEN '100'" &
                        " WHEN process LIKE '[1-5]400' THEN '400'" &
                        " WHEN process LIKE '[1-5]700' THEN '700'" &
                    " END AS process," &
                    "[status]," &
                    "[startTime]," &
                    "[endTime]," &
                    "[AcumTime]," &
                    "CONVERT(DATE, do.[insert_date]) AS insert_date, " &
                    "alarmedDepto AS depto, remarks, EC1, ec2, ec3, ec4, ec5, defects" &
                " FROM [db_kyungshin].[dbo].[t_bma_downtime] AS do" &
                " WHERE (" &
                        " (status='DOWN' AND " & procesos & " AND acumtime>='00:05:00' AND CONVERT(DATE, do.insert_date)='" & ConvierteAdateMySQL(Date.Now.ToShortDateString) & "')" &
                        " OR " &
                        " (status='DOWN' AND " & procesos & " AND acumtime='' AND CONVERT(DATE, do.insert_date)='" & ConvierteAdateMySQL(Date.Now.ToShortDateString) & "' AND step=1)" &
                    ")" &
                " ORDER BY process, id"
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

                    ' Centrar el contenido de la columna "line"
                    dgv.Columns("line").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    dgv.Columns("process").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                    ' Asignar valores a las celdas normales
                    newRow.Cells("id").Value = item("id").ToString.Trim
                    newRow.Cells("line").Value = item("linea").ToString.Trim
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

                    newRow.Cells("defects").Value = item("defects").ToString.Trim

                    ' Habilitar/Deshabilitar columna "defects" según el valor de "depto"
                    If item("depto").ToString.Trim.ToUpper() = "QUALITY" Then  'Usar ToUpper() para evitar problemas de mayúsculas/minúsculas
                        dgv.Columns("defects").ReadOnly = True 'O .ReadOnly = False si quieres que sea editable
                    Else
                        dgv.Columns("defects").ReadOnly = False 'O .ReadOnly = True si quieres que no sea editable
                    End If

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
            dtpDesde.Value = Date.Now.ToShortDateString
            dtpHasta.Value = Date.Now.ToShortDateString
            'LoadCombos()
            'dgv.DataSource = EjecutaSelects(query, "buscahistorial")
            Call UpdateDefectsReadOnly()
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

    Private Sub bntFiltrar_Click(sender As Object, e As EventArgs) Handles bntFiltrar.Click
        Try
            dgv.Rows.Clear()
            Dim desde As String = Convert.ToDateTime(dtpDesde.Value).ToString("yyyy-MM-dd")
            Dim hasta As String = Convert.ToDateTime(dtpHasta.Value).ToString("yyyy-MM-dd")
            Dim query, querycompl As String

            If rbIncomplete.Checked = True Then
                querycompl &= " AND (depto='' or depto is null or alarmedDepto='' or alarmedDepto is null or ec1='') "
            Else
                querycompl = ""
            End If

            If cbDepto.Text <> "" Then
                querycompl &= " AND (depto='" & cbDepto.Text & "' or alarmedDepto='" & cbDepto.Text & "')"
            End If

            query = "SELECT id,
    LEFT([process], 1) AS linea,  -- Extrae el primer dígito para la línea
    RIGHT([process], 3) AS proceso -- Extrae los últimos 3 dígitos para el proceso
                  ,[status]
                  ,[startTime]
                  ,[endTime]
                  ,[AcumTime]
                  ,[insert_date]
                  ,[remarks]
                  ,[depto]
                  ,[alarmedDepto]
                  ,[ec1]
                  ,[ec2]
                  ,[ec3]
                  ,[ec4]
                  ,[ec5],defects
              FROM [db_kyungshin].[dbo].[t_bma_downtime] 
              WHERE 1=1 AND AcumTime>='00:05:00' and STATUS='DOWN' 
                AND  CONVERT(DATE ,insert_date) BETWEEN '" & desde & "' AND '" & hasta & "' AND  " & procesos & querycompl & " ORDER BY process"


            Dim arre2 As DataRowCollection = GetRows(query)

            If Not IsNothing(arre2) Then


                For Each item As DataRow In arre2
                    Dim newRow As DataGridViewRow = dgv.Rows(dgv.Rows.Add())
                    dgv.Columns("line").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    dgv.Columns("process").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    ' Asignar valores a las celdas normales
                    newRow.Cells("id").Value = item("id").ToString.Trim
                    newRow.Cells("line").Value = item("linea").ToString.Trim
                    newRow.Cells("depto").Value = item("depto").ToString.Trim
                    newRow.Cells("fecha").Value = ConvierteFechaMySQL(item("insert_date").ToString.Trim)
                    newRow.Cells("process").Value = item("proceso").ToString.Trim
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

                    newRow.Cells("defects").Value = item("defects").ToString.Trim

                    ' Habilitar/Deshabilitar columna "defects" según el valor de "depto"
                    If item("depto").ToString.Trim.ToUpper() = "QUALITY" Then  'Usar ToUpper() para evitar problemas de mayúsculas/minúsculas
                        dgv.Columns("defects").ReadOnly = True 'O .ReadOnly = False si quieres que sea editable
                    Else
                        dgv.Columns("defects").ReadOnly = False 'O .ReadOnly = True si quieres que no sea editable
                    End If

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


                ' colores()

            End If
            Call UpdateDefectsReadOnly()
            ResizeCols(dgv)
        Catch ex As Exception

        End Try
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

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        btnExcel.Enabled = False
        GridAExcelGeneral(dgv, "")
        btnExcel.Enabled = True
    End Sub
    ' Evento para cuando cambia el valor del ComboBox:
    Private Sub dgv_DataBindingComplete(sender As Object, e As EventArgs) Handles dgv.DataBindingComplete
        Call UpdateDefectsReadOnly() ' Llama a la función para actualizar ReadOnly
    End Sub

    Private Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click
        Dim frm As New frmScreenshots
        frm.ShowDialog()
    End Sub

    Private Sub dgv_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellValueChanged
        If e.ColumnIndex = dgv.Columns("depto")?.Index Then 'Manejo de nulos con ?
            Call UpdateDefectsReadOnly() ' Llama a la función para actualizar ReadOnly
        End If
    End Sub

    ' Función para actualizar ReadOnly (se llama desde Load y CellValueChanged)
    Private Sub UpdateDefectsReadOnly()
        If dgv.Columns.Contains("depto") AndAlso dgv.Columns.Contains("defects") Then
            For Each row As DataGridViewRow In dgv.Rows
                Dim deptoValue As String = row.Cells("depto")?.Value?.ToString()?.Trim()?.ToUpper()
                row.Cells("defects").ReadOnly = deptoValue <> "QUALITY"
            Next
        End If
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
                      "',defects='" & rw.Cells("defects").Value.ToString &
                "' WHERE id='" & rw.Cells("id").Value.ToString & "'"

                Ejecuta(query, "updBMA DT")

            Next

            colores()
            MessageBox.Show("The data was saved succesfully.", "Details", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Function GridAExcelGeneral(ByVal ElGrid As DataGridView, Optional frmSender As String = "") As Boolean

        'Creamos las variables
        Dim exApp As New Microsoft.Office.Interop.Excel.Application
        Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
        Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet

        Try
            'Añadimos el Libro al programa, y la hoja al libro
            exLibro = exApp.Workbooks.Add
            exHoja = exLibro.Worksheets.Add()

            ' ¿Cuantas columnas y cuantas filas?
            Dim NCol As Integer = ElGrid.ColumnCount
            Dim NRow As Integer = ElGrid.RowCount
            'Dim i As Integer = 0

            'For Each column As DataGridViewColumn In dgv.Columns
            '    If column.Visible = True Then
            '        exHoja.Cells.Item(1, i + 1) = ElGrid.Columns(i).Name.ToString
            '        i += 1
            '    End If
            'Next

            'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
            For i As Integer = 1 To NCol

                exHoja.Cells.Item(1, i) = ElGrid.Columns(i - 1).Name.ToString.Trim
                'exHoja.Cells.Item(1, i).HorizontalAlignment = 3

            Next

            Dim colDate As String = ""
            For Fila As Integer = 0 To NRow - 1
                For Col As Integer = 0 To NCol - 1
                    Try

                        exHoja.Cells.Item(Fila + 2, Col + 1) = ElGrid.Rows(Fila).Cells(Col).Value.ToString.Trim

                        ' exHoja.Cells.Item(Fila + 2, Col + 1) = ElGrid.Rows(Fila).Cells(Col).Value.ToString.Trim
                    Catch ex As Exception

                    End Try
                Next
            Next

            'Titulo en negrita, Alineado al centro y que el tamaño de la columna se
            'ajuste al texto
            exHoja.Rows.Item(1).Font.Bold = 1
            exHoja.Rows.Item(1).HorizontalAlignment = 3
            exHoja.Columns.AutoFit()

            'Aplicación visible
            exApp.Application.Visible = True

            exHoja = Nothing
            exLibro = Nothing
            exApp = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al exportar a Excel")
            'http://programaciontotal.blogspot.com
            Return False
        End Try

        Return True
    End Function
End Class