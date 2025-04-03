
Imports System.Data.SqlClient

Module Module1
    Public contra As String
    Public operador As String
    'Public conexion As MySqlConnection
    Public oCn_WMS As SqlConnection = Nothing
    Public oCn_lines As SqlConnection = Nothing
    Public conexion As SqlConnection
    Public folioDetenido As String
    Public iphCheck As Boolean = False
    Public nombre As String = Nothing
    Public ap As String = Nothing
    Public am As String = Nothing
    Public TemplateIndice As String = Nothing
    Public templatePulgar As String = Nothing
    Public urlFoto As String = Nothing
    Public remision As String = Nothing
    Public Nomina As String = ""
    Public nombreColumna As String = Nothing
    Public userAtencion As Boolean = False
    Public datosConciliacion As Boolean = False, notificacionAbierta As Boolean = False, notificacionConciliacionAbierta As Boolean = False
    Public gParametros As E_PARAMETROS = Nothing 'Parametros Globales
    Public datosEncontrados As DataTable
    Public criterio, PlacaLib, Parte_lib, fecha_Liberacion, folio_liberacion, vehiculox, parteRepetido, tipoLib, folioPago As String
    Public clicBuscarEnAccidentes As Boolean = False
    Public placaRepetida As Boolean = False
    Public userConci As Boolean = False
    Public ArrayPermisos() As String
    Public TemplateMaximo As Boolean = False
    'EXCEL
    'Public m_Excel As New Excel.Application
    'Public objLibroExcel As Excel.Workbook = m_Excel.Workbooks.Add
    'Public objHojaExcel As Microsoft.Office.Interop.Excel.Worksheet = objLibroExcel.Worksheets(1)
    '_________
    Public latitud As String = ""
    Public longitud As String = ""
    Public BuscaHuella As Boolean = False
    Public datosPersonales(20) As String
    Public dataLoaded As Boolean = False
    Public ultimafila As Integer
    Public narracion As String = "", seguimiento As String = ""

    Private cmd As SqlCommand = Nothing
    Private Adapter As SqlDataAdapter = Nothing
    Private ds As DataSet = Nothing

    Public Current_Session As t_usuarios.datos 'cuenta padre
    Public Current_Session_Usuario As t_usuarios.datos 'usuarios sucursales

    Public progressIndex As Integer = 0

    Public remisionMarcador As String = ""
    Public marcadorClic As Boolean = False

    Public Sub ResizeCols(ByVal dgv As DataGridView)

        With dgv
            .AutoResizeColumns()
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        End With
    End Sub

    'Public Sub generaExcel()
    '    m_Excel = New Excel.Application
    '    objLibroExcel = m_Excel.Workbooks.Add
    '    objHojaExcel = objLibroExcel.Worksheets(1)
    'End Sub


    Public Function SeparaEnLineasParaNota(ByVal cadena As String) As ArrayList

        Dim arre As New ArrayList
        Dim nLineas() As String = Nothing
        Dim size As Long = cadena.Trim.Length

        Try
            Dim sOutput As String = Nothing

            Dim k As Byte = 0
            Dim j As Integer = 0, pos As Long = 0
            For i = 0 To size
                If (j = 70) Then
                    k = k + 1
                    If (k = 1) Then pos = 0
                    If (k = 2) Then pos = 70
                    If (k = 3) Then pos = 140
                    If (k = 4) Then pos = 210
                    If (k = 5) Then pos = 280
                    If (k = 6) Then pos = 350
                    If (k = 7) Then pos = 420
                    If (k = 8) Then pos = 490
                    If (k = 9) Then pos = 560
                    If (k = 10) Then pos = 630
                    If (k = 11) Then pos = 700
                    If (k = 12) Then pos = 770
                    If (k = 13) Then pos = 840
                    If (k = 14) Then pos = 910
                    If (k = 15) Then pos = 980
                    If (k = 16) Then pos = 1050
                    If (k = 17) Then pos = 1120
                    If (k = 18) Then pos = 1190
                    If (k = 19) Then pos = 1260
                    If (k = 20) Then pos = 1330
                    If (k = 21) Then pos = 1400
                    If (k = 22) Then pos = 1470
                    If (k = 23) Then pos = 1540


                    If (k > 23) Then
                        'exit sub
                    Else
                        arre.Add(cadena.Substring(pos, 70))
                    End If
                    j = 0
                End If
                j = j + 1
            Next

            Try
                If (j < 70) Then 'para agregar la cadena restante de la linea 3 
                    j -= 1
                    arre.Add(cadena.Substring(cadena.Length - j, j))
                Else
                    arre.Add(cadena.Substring(0, j - 1))
                End If
            Catch ex As Exception
                arre.Add(cadena.Substring(45, j - 1))
            End Try

            'If (size > 30) Then
            '    arre.Add(cadena.Substring(0, 30)) 'Linea 1
            '    arre.Add(cadena.Substring(30, 30))
            'Else
            '    arre.Add(cadena.Trim)
            'End If

            Return arre
        Catch ex As Exception
            Return arre
        End Try
    End Function

    Public Function hayInternet() As Boolean
        ' Returns True if connection is available 
        ' Replace www.yoursite.com with a site that 
        ' is guaranteed to be online - perhaps your 
        ' corporate site, or microsoft.com 
        Dim objUrl As New System.Uri("http://www.google.com/")
        ' Setup WebRequest 
        Dim objWebReq As System.Net.WebRequest
        objWebReq = System.Net.WebRequest.Create(objUrl)
        Dim objResp As System.Net.WebResponse
        Try
            ' Attempt to get response and return True 
            objResp = objWebReq.GetResponse
            objResp.Close()
            objWebReq = Nothing
            Return True
        Catch ex As Exception
            ' Error, exit and return False 
            objWebReq = Nothing
            Return False
        End Try
    End Function

    Public Function EjecutaSelectsDS(ByVal fecha1 As String, ByVal hora1 As String, ByVal hora2 As String)
        Dim status As New DataSet
        Dim seleccampo As New SqlCommand("Select * From t_capturainfracciones WHERE fecha= '" & fecha1 & "' AND hora BETWEEN '" & hora1 & "' AND '" & hora2 & "'", conexion)
        Dim adaptaer As New SqlDataAdapter
        adaptaer.SelectCommand = seleccampo
        adaptaer.Fill(status)
        Return status
    End Function

    Public Function CheckIfRowExists(ByVal sql As String, ByVal sender As String) As Boolean

        Try
            cmd = New SqlCommand(sql, conexion)
            ds = New DataSet
            Adapter = New SqlDataAdapter
            Adapter.SelectCommand = cmd
            Adapter.Fill(ds)

            If (ds.Tables(0).Rows.Count > 0) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message & vbCrLf & "Source :CheckIfRowExists(.." & vbCrLf & "Sender :" & sender,
            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
            Exit Function
        Finally
            ds = Nothing
            Adapter = Nothing
            cmd = Nothing
        End Try
    End Function

    Public Function GetRows(ByVal SQL As String) As DataRowCollection

        Try
            cmd = New SqlCommand(SQL, conexion)
            ds = New DataSet
            Adapter = New SqlDataAdapter
            Adapter.SelectCommand = cmd
            Adapter.Fill(ds)

            If (ds.Tables(0).Rows.Count > 0) Then
                Return ds.Tables(0).Rows
            Else
                'MessageBox.Show("No se encontraron registros.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Return ds.Tables(0).Rows 'devolvera 0
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GetRows(..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
            Exit Function
        Finally
            ds = Nothing
            Adapter = Nothing
            cmd = Nothing
        End Try
    End Function

    Public Function Get_Parametros_Lector() As LectorDigital

        Dim _parametros As LectorDigital = Nothing

        Try
            Dim sq As String = "SELECT * FROM t_devices AS d INNER JOIN t_terminales AS t ON d.alias=t.alias WHERE t.machineName='" & GetComputerName() & "'"
            cmd = New SqlCommand(sq, conexion)
            ds = New DataSet
            Adapter = New SqlDataAdapter
            Adapter.SelectCommand = cmd
            Adapter.Fill(ds)

            If (ds.Tables(0).Rows.Count > 0) Then
                With _parametros
                    .Activation_code = ds.Tables(0).Rows(0)("activationCode").ToString.Trim
                    .Registration_number = ds.Tables(0).Rows(0)("registrationNumber").ToString.Trim
                    .Serial_number = ds.Tables(0).Rows(0)("serialNumber").ToString.Trim
                    .Verification_code = ds.Tables(0).Rows(0)("verificationCode").ToString.Trim
                    .Verification_key = ds.Tables(0).Rows(0)("verificationKey").ToString.Trim
                End With

                Return _parametros
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message & "sender:322", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Adapter.Dispose()
            ds.Dispose()
            cmd.Dispose()
        End Try
    End Function


    Public Function GetComputerName() As String
        Dim ComputerName As String
        ComputerName = System.Net.Dns.GetHostName
        Return ComputerName
    End Function
    Public Function DevuelveMaximoNChars(ByVal cadena As String, ByVal NChars As Integer) As String

        Try
            If (cadena.Trim.Length > NChars) Then
                Return cadena.Substring(0, NChars)
            Else
                Return cadena.Trim
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message & "Sender:DevuelveMaximoNChars", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return cadena.Trim
        End Try
    End Function


    Public Function EjecutaSelects(ByVal sSQL As String, ByVal sender As String)
        Dim cmd As SqlCommand = Nothing
        Try
            cmd = New SqlCommand(sSQL, Module1.conexion)
            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            Return dt
        Catch ex As Exception
            ' MsgBox(ex.ToString)
        End Try
        Return sender
    End Function

    Public Function EjecutaSelectsHISS(ByVal sSQL As String, ByVal sender As String)
        Dim cmd As SqlCommand = Nothing
        Try
            cmd = New SqlCommand(sSQL, Module1.oCn_WMS)
            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            Return dt
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Return sender
    End Function




    Public Function QueryRow(ByVal sSQL As String, ByVal CellName As String, ByVal sender As Object) As String


        Try
            cmd = New SqlCommand(sSQL, conexion)
            ds = New DataSet
            Adapter = New SqlDataAdapter
            Adapter.SelectCommand = cmd
            Adapter.Fill(ds)

            If (ds.Tables(0).Rows.Count > 0) Then

                Return ds.Tables(0).Rows(0)(CellName).ToString()
            Else
                'MessageBox.Show("No se encontraron registros.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return ""
            End If

        Catch ex As Exception
            ' MessageBox.Show(ex.Message & vbCrLf & "Sender : QueryRow(..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        Finally
            ds = Nothing
            Adapter = Nothing
            cmd = Nothing
        End Try
    End Function

    'Function GridAExcel(ByVal ElGrid As DataGridView, Optional frmSender As String = "") As Boolean

    '    'Creamos las variables
    '    Dim exApp As New Microsoft.Office.Interop.Excel.Application
    '    Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
    '    Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet

    '    Try
    '        'Añadimos el Libro al programa, y la hoja al libro
    '        exLibro = exApp.Workbooks.Add
    '        exHoja = exLibro.Worksheets.Add()

    '        ' ¿Cuantas columnas y cuantas filas?
    '        Dim NCol As Integer = ElGrid.ColumnCount
    '        Dim NRow As Integer = ElGrid.RowCount
    '        'Dim i As Integer = 0

    '        'For Each column As DataGridViewColumn In dgv.Columns
    '        '    If column.Visible = True Then
    '        '        exHoja.Cells.Item(1, i + 1) = ElGrid.Columns(i).Name.ToString
    '        '        i += 1
    '        '    End If
    '        'Next

    '        'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
    '        For i As Integer = 1 To NCol

    '            exHoja.Cells.Item(1, i) = ElGrid.Columns(i - 1).Name.ToString.Trim
    '            'exHoja.Cells.Item(1, i).HorizontalAlignment = 3

    '        Next

    '        Dim colDate As String = ""
    '        Dim arre() As String
    '        For Fila As Integer = 0 To NRow - 1
    '            For Col As Integer = 0 To NCol - 1
    '                Try
    '                    If Col = 0 Or Col = 6 Then
    '                        'colDate = Convert.ToDateTime(ElGrid.Rows(Fila).Cells(Col).Value.ToString.Trim).ToString("dd/MM/yyyy")
    '                        arre = ElGrid.Rows(Fila).Cells(Col).Value.ToString().Split(" ")
    '                        colDate = ConvierteFechaMySQL(arre(0))
    '                        exHoja.Cells.Item(Fila + 2, Col + 1) = colDate

    '                        If Col = 6 And
    '                        (ElGrid.Rows(Fila).Cells(Col).Value.ToString.Trim) <= ElGrid.Rows(Fila).Cells(Col + 3).Value.ToString.Trim Then
    '                            If frmSender = "reorden" Then
    '                                'rellenar en amarillo los productos que necesitan reorden de material
    '                                exHoja.Cells(Fila + 2, Col + 1).interior.color = Color.Yellow
    '                                exHoja.Cells(Fila + 2, Col).interior.color = Color.Yellow
    '                                exHoja.Cells(Fila + 2, Col - 3).interior.color = Color.Yellow
    '                            End If

    '                        Else
    '                            ' exHoja.Cells(Fila + 2, Col + 1).interior.color = Color.White
    '                        End If

    '                    Else
    '                        exHoja.Cells.Item(Fila + 2, Col + 1) = ElGrid.Rows(Fila).Cells(Col).Value.ToString.Trim
    '                    End If
    '                    ' exHoja.Cells.Item(Fila + 2, Col + 1) = ElGrid.Rows(Fila).Cells(Col).Value.ToString.Trim
    '                Catch ex As Exception

    '                End Try
    '            Next
    '        Next

    '        'Titulo en negrita, Alineado al centro y que el tamaño de la columna se
    '        'ajuste al texto
    '        exHoja.Rows.Item(1).Font.Bold = 1
    '        exHoja.Rows.Item(1).HorizontalAlignment = 3
    '        exHoja.Columns.AutoFit()

    '        'Aplicación visible
    '        exApp.Application.Visible = True

    '        exHoja = Nothing
    '        exLibro = Nothing
    '        exApp = Nothing
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al exportar a Excel")
    '        'http://programaciontotal.blogspot.com
    '        Return False
    '    End Try

    '    Return True
    'End Function

End Module
