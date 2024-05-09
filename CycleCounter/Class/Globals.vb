
Imports System.IO
Imports System.Text
Imports System.Data.SqlClient

Module Globals
    Private cmd As SqlCommand = Nothing
    Private Adapter As SqlDataAdapter = Nothing
    Private ds As DataSet = Nothing
    Public noHuella As String = ""
    Public registroSinHuella As Boolean = False
    Public SecondLoad As Boolean = False
    Public DBserver As String = ""
    Public ArchivoDat As Archivo_CFG_DAT
    Public private_key As String = "_mega"
    Public Configuración_SMTP As Email_Cfg = Nothing
    Public debugMode As Boolean = False 'FALSE SE CONECTA A BD PRODUCCION   TRUE SE CONECTA A BD LOCAL LAPTOP

    Public Function ConvierteDecimalMoneda(ByVal m_valor As Double) As String
        Try
            Return (m_valor).ToString("C2")

        Catch ex As Exception
            Return m_valor
        End Try
    End Function


    Public Function LlenaCboWithQuery(ByVal cbo As ComboBox, ByVal sSQL As String, ByVal CellName As String, ByVal sender As Object, Optional clear As Boolean = True) As Boolean

        Try
            cmd = New SqlCommand(sSQL, conexion)
            ds = New DataSet
            Adapter = New SqlDataAdapter
            Adapter.SelectCommand = cmd
            Adapter.Fill(ds)

            If (ds.Tables(0).Rows.Count > 0) Then

                If clear = True Then
                    cbo.Items.Clear()
                End If

                For Each row As DataRow In ds.Tables(0).Rows
                    If row.Item(CellName).ToString() <> "" Then
                        cbo.Items.Add(row.Item(CellName).ToString())
                    End If

                Next
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            ds = Nothing
            Adapter = Nothing
            cmd = Nothing
        End Try
    End Function

    ''' <summary>
    ''' devuelve un string de una fecha en formato mysql yyyy-MM-dd al formato dd/MM/yyyy
    ''' </summary>
    ''' <param name="mysqlDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConvierteFechaMySQL(ByVal mysqlDate As String) As String
        Try
            Dim fecha As Date = Convert.ToDateTime(mysqlDate)
            Return fecha.ToString("dd/MM/yyyy")
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return mysqlDate
        End Try
    End Function

    Public Function ConvierteAdateMySQL(ByVal mysqlDate As String) As String
        Try
            Dim fecha As Date = Convert.ToDateTime(mysqlDate)
            Return fecha.ToString("yyyy-MM-dd")
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return mysqlDate
        End Try
    End Function

    ''' <summary>
    ''' Checa si la conexión se encuentra Activa
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CheckForConnection()

        If (conexion.State = ConnectionState.Closed Or IsNothing(conexion)) Then
            Try
                conexion = New SqlConnection("server=17;user=root;database=barandilla;port=3306;password=1209;")
                conexion.Open()
            Catch ex As Exception
                MessageBox.Show("No ha podido establecerse la conexión a la base de datos, Consulte al administrador del sistema", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Application.Exit()
            End Try
        End If
    End Sub

    Public Sub CheckForRPBSConnection()

        If (conexion.State = ConnectionState.Closed Or IsNothing(conexion)) Then
            Try
                conexion = New SqlConnection()
                If debugMode = True Then
                    If GetComputerName() = "arturo-garcia" Then
                        conexion.ConnectionString = "Initial Catalog=db_kyungshin;Data Source=ARTURO-GARCIA\SQLEXPRESS; Integrated Security=True"

                    Else
                        conexion.ConnectionString = "Initial Catalog=db_kyungshin;Data Source=LAPTOP-ROUA60BG\SQLEXPRESS; Integrated Security=True"

                    End If
                Else
                    conexion.ConnectionString = "Initial Catalog=db_kyungshin;Data Source=172.30.61.20; User Id=sistemas;pwd=kyungshin2!"
                End If
                conexion.Open()

            Catch ex As Exception
                MessageBox.Show("No ha podido establecerse la conexión a la base de datos, Consulte al administrador del sistema", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Application.Exit()
            End Try
        End If
    End Sub


    Public OutputStream As System.IO.StreamWriter
    Sub OnDataReceived1(ByVal Sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs)

        If e.Data IsNot Nothing Then
            Dim text As String = e.Data
            Dim bytes As Byte() = Encoding.Default.GetBytes(text)
            text = Encoding.UTF8.GetString(bytes)
            OutputStream.WriteLine(text)
        End If
    End Sub
    Sub CreateBackup(ByVal datos As E_DATOS_CORREO_BACKUP)

        'Try
        '    If (File.Exists("db_megacarnes.sql") = True) Then
        '        Try
        '            Kill("db_megacarnes.sql") 'Clear Previous Backup

        '        Catch ex As Exception
        '            Console.WriteLine("Error al borrar el archivo" & vbCrLf & "Descripción : " & ex.Message)
        '        End Try
        '    End If

        '    If (File.Exists("backup.bat") = True) Then
        '        Try
        '            Kill("backup.bat") 'Clear Previous Backup

        '        Catch ex As Exception
        '            Console.WriteLine("Error al borrar el archivo" & vbCrLf & "Descripción : " & ex.Message)
        '        End Try
        '    End If

        '    Dim mysqldumpPath As String = Class_MySQL.GetMySQLFolder() & "\MySQL\MySQL Server 5.1\bin\mysqldump.exe"
        '    Dim host As String = "localhost"
        '    Dim user As String = "root"
        '    Dim pswd As String = "AES256"
        '    Dim dbnm As String = "db_megacarnes"
        '    Dim cmd As String = String.Format("-h{0} -u{1} -p{2} {3}", host, user, pswd, dbnm)
        '    Dim filePath As String = Environment.CurrentDirectory & "\db_megacarnes.sql"
        '    OutputStream = New System.IO.StreamWriter(filePath, False, System.Text.Encoding.UTF8)

        '    Dim startInfo As System.Diagnostics.ProcessStartInfo = New System.Diagnostics.ProcessStartInfo()
        '    startInfo.FileName = mysqldumpPath
        '    startInfo.Arguments = cmd

        '    startInfo.RedirectStandardError = True
        '    startInfo.RedirectStandardInput = False
        '    startInfo.RedirectStandardOutput = True
        '    startInfo.UseShellExecute = False
        '    startInfo.CreateNoWindow = True
        '    startInfo.ErrorDialog = False

        '    Dim proc As System.Diagnostics.Process = New System.Diagnostics.Process()
        '    proc.StartInfo = startInfo
        '    AddHandler proc.OutputDataReceived, AddressOf OnDataReceived1
        '    proc.Start()
        '    proc.BeginOutputReadLine()
        '    proc.WaitForExit()

        '    OutputStream.Flush()
        '    OutputStream.Close()
        '    proc.Close()


        '    'Si se escribio el archivo correctamente.
        '    If (File.Exists("db_megacarnes.sql") = True) Then
        '        'System.Diagnostics.Process.Start("db_megacarnes.sql")

        '        'Tiempo Aproximado en el que se hace el respaldo NOTA : (Con el tiempo este valor tendra que aumentar)
        '        Threading.Thread.Sleep(6000)
        '        'Envia Correo con Archivo Adjunto.
        '        '
        '        Dim m_body As String = "<table>" & vbCrLf
        '        m_body = m_body & "<tr>" & vbCrLf
        '        m_body = m_body & "<th>Version</th>" & vbCrLf
        '        m_body = m_body & "<td>" & m_version & "</td>" & vbCrLf
        '        m_body = m_body & "</tr>" & vbCrLf
        '        m_body = m_body & "<tr>" & vbCrLf

        '        m_body = m_body & "<th>Usurio PV</th>" & vbCrLf
        '        m_body = m_body & "<td>" & datos.usuario_PV & "</td>" & vbCrLf      'Current_Session_Usuario_PV.usuario
        '        m_body = m_body & "<tr>" & vbCrLf
        '        m_body = m_body & "<tr>" & vbCrLf
        '        m_body = m_body & "<th>Usuario Corte</th>" & vbCrLf
        '        m_body = m_body & "<td>" & datos.usuario_Corte & "</td>" & vbCrLf  ' gUsuario_Corte.Trim()
        '        m_body = m_body & "<tr>" & vbCrLf

        '        m_body = m_body & "<tr>" & vbCrLf
        '        m_body = m_body & "<th>Maquina</th>" & vbCrLf
        '        m_body = m_body & "<td>" & Class_System.GetComputerName & "</td>" & vbCrLf
        '        m_body = m_body & "<tr>" & vbCrLf
        '        m_body = m_body & "<th>S.O</th>" & vbCrLf
        '        m_body = m_body & "<td>" & My.Computer.Info.OSFullName & "</td>" & vbCrLf
        '        m_body = m_body & "</tr>" & vbCrLf

        '        Dim Bval1 As Boolean = SendEmail("Respaldo Sucursal " & Current_Session_Usuario_Logeado.sucursal, m_body, gParametros.email_respaldos, "db_megacarnes.sql")

        '        If (Bval1 = True) Then
        '            'Actualizar FECHA del ultimo Respaldo
        '            Call Ejecuta("UPDATE t_parametros SET valor='" & GetCurrentDate() & "',lastUpdate='" & GetCurrentDateTime() & "' WHERE parametro='respaldo'", "432")

        '            Try
        '                Kill("db_megacarnes.sql")
        '            Catch ex As Exception
        '                'The process cannot access the file 'C:\Users\Rochade\Documents\git\MEGACARNES\bin\Debug\db_megacarnes.sql' because it is being used by another process.
        '                Console.WriteLine("Error al borrar el archivo" & vbCrLf & "Descripción : " & ex.Message)
        '            End Try
        '        End If

        '        Application.Exit()
        '    End If

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub
    Public Sub crearDUMP_Mysql(ByVal datos As E_DATOS_CORREO_BACKUP) ' +2  Referencias menu (Manual), y al terminar de hacer el corte de forma automatica.

        ''Este proceso será de forma temporal , la propuesta de mejora es respaldar todo en la NUBE.

        'Try
        '    If (File.Exists("db_megacarnes.sql") = True) Then
        '        Try
        '            Kill("db_megacarnes.sql") 'Clear Previous Backup

        '        Catch ex As Exception
        '            Console.WriteLine("Error al borrar el archivo" & vbCrLf & "Descripción : " & ex.Message)
        '        End Try
        '    End If

        '    If (File.Exists("backup.bat") = True) Then
        '        Try
        '            Kill("backup.bat") 'Clear Previous Backup

        '        Catch ex As Exception
        '            Console.WriteLine("Error al borrar el archivo" & vbCrLf & "Descripción : " & ex.Message)
        '        End Try
        '    End If

        '    Dim sPath As String = Nothing

        '    '================================================
        '    '   Verifica en que Carpeta esta instalado MySQL
        '    '================================================

        '    sPath = Class_MySQL.GetMySQLFolder()
        '    Dim m_port As String = Nothing

        '    If (Class_System.GetComputerName.Trim.ToUpper() = "WINDOWS-KRQ8CIA") Then 'SERVER
        '        m_port = "3309"
        '    Else
        '        m_port = "3306"
        '    End If


        '    Dim sql As String = Chr(34) & Trim(sPath) & "\MySQL\MySQL Server 5.1\bin\mysqldump.exe" & Chr(34) & " --port " & m_port & " --host " & MDIPrincipal.lbHost.Text.Trim &
        '        " --user=root --password=AES256 --routines db_megacarnes > db_megacarnes.sql" & Chr(34)

        '    Dim Bval As Boolean = Class_MySQL.CrearArchivoBAT("backup.bat", sql)

        '    'Si se escribio el archivo correctamente.
        '    If (File.Exists("backup.bat") = True) Then
        '        System.Diagnostics.Process.Start("backup.bat")

        '        'Tiempo Aproximado en el que se hace el respaldo NOTA : (Con el tiempo este valor tendra que aumentar)
        '        Threading.Thread.Sleep(6000)
        '        'Envia Correo con Archivo Adjunto.
        '        '
        '        Dim m_body As String = "<table>" & vbCrLf
        '        m_body = m_body & "<tr>" & vbCrLf
        '        m_body = m_body & "<th>Version</th>" & vbCrLf
        '        m_body = m_body & "<td>" & m_version & "</td>" & vbCrLf
        '        m_body = m_body & "</tr>" & vbCrLf
        '        m_body = m_body & "<tr>" & vbCrLf

        '        m_body = m_body & "<th>Usurio PV</th>" & vbCrLf
        '        m_body = m_body & "<td>" & datos.usuario_PV & "</td>" & vbCrLf      'Current_Session_Usuario_PV.usuario
        '        m_body = m_body & "<tr>" & vbCrLf
        '        m_body = m_body & "<tr>" & vbCrLf
        '        m_body = m_body & "<th>Usuario Corte</th>" & vbCrLf
        '        m_body = m_body & "<td>" & datos.usuario_Corte & "</td>" & vbCrLf  ' gUsuario_Corte.Trim()
        '        m_body = m_body & "<tr>" & vbCrLf

        '        m_body = m_body & "<tr>" & vbCrLf
        '        m_body = m_body & "<th>Maquina</th>" & vbCrLf
        '        m_body = m_body & "<td>" & Class_System.GetComputerName & "</td>" & vbCrLf
        '        m_body = m_body & "<tr>" & vbCrLf
        '        m_body = m_body & "<th>S.O</th>" & vbCrLf
        '        m_body = m_body & "<td>" & My.Computer.Info.OSFullName & "</td>" & vbCrLf
        '        m_body = m_body & "</tr>" & vbCrLf

        '        Dim Bval1 As Boolean = SendEmail("Respaldo Sucursal " & Current_Session_Usuario_Logeado.sucursal, m_body, gParametros.email_respaldos, "db_megacarnes.sql")

        '        If (Bval1 = True) Then
        '            'Actualizar FECHA del ultimo Respaldo
        '            Call Ejecuta("UPDATE t_parametros SET valor='" & GetCurrentDate() & "',lastUpdate='" & GetCurrentDateTime() & "' WHERE parametro='respaldo'", "432")

        '            Try
        '                Kill("db_megacarnes.sql")
        '            Catch ex As Exception
        '                Console.WriteLine("Error al borrar el archivo" & vbCrLf & "Descripción : " & ex.Message)
        '            End Try
        '        End If

        '        Application.Exit()
        '    End If

        'Catch ex As Exception
        '    MessageBox.Show("No ha podido efectuarse la operación", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub
    Public Sub open_cashdrawer()
        Try
            Shell("Open.bat", vbNormalFocus)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "Functions"


    ''' <summary>
    ''' determina si la cadena contiene valores decimales
    ''' </summary>
    ''' <param name="cadena"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsDecimal(ByVal cadena As String) As Boolean

        Dim Bval As Boolean = True
        Try
            For index As Integer = 1 To cadena.Length
                Dim letra As String = GetChar(cadena, index)
                If (Not IsNumeric(letra)) Then
                    If (letra.Trim = "." Or letra.Trim = "-") Then
                        'No hacer nada, pasar a la siguiente iteración.
                    Else
                        Bval = False
                        Exit For
                    End If
                End If
            Next

            Return Bval
        Catch ex As Exception
            Return Bval
        End Try

        'Return IIf(cadena.Contains("1234567890.-"), True, False)
    End Function

    Public Function getNumeric(ByVal value As String) As String
        Try
            Dim output As StringBuilder = New StringBuilder
            For i = 0 To value.Length - 1
                If IsNumeric(value(i)) Or value(i).ToString.Trim = "." Then
                    output.Append(value(i))
                End If
            Next

            Return output.ToString()
        Catch ex As Exception
            Return "Error"
        End Try
    End Function
    Public Function Get_Global_Configuration() As E_PARAMETROS

        Dim _parametros As E_PARAMETROS = Nothing

        Try
            cmd = New SqlCommand("SELECT * FROM t_parametros_generales", conexion)
            ds = New DataSet
            Adapter = New SqlDataAdapter
            Adapter.SelectCommand = cmd
            Adapter.Fill(ds)

            If (ds.Tables(0).Rows.Count > 0) Then
                With _parametros
                    For Each rw As DataRow In ds.Tables(0).Rows
                        Application.DoEvents()
                        Dim Field As String = rw("parametro").ToString.Trim
                        If (Field = "abanderamiento") Then .abanderamiento = rw("VALOR").ToString.Trim
                        If (Field = "taxis") Then .taxis = rw("VALOR").ToString.Trim
                        If (Field = "AR15") Then .AR15 = rw("VALOR").ToString.Trim
                        If (Field = "AR1M") Then .AR1M = rw("VALOR").ToString.Trim
                        If (Field = "AR3M") Then .AR3M = rw("VALOR").ToString.Trim
                        If (Field = "AR6M") Then .AR6M = rw("VALOR").ToString.Trim
                        If (Field = "AR1A") Then .AR1A = rw("VALOR").ToString.Trim
                        If (Field = "AR1AS") Then .AR1AS = rw("VALOR").ToString.Trim
                        If (Field = "carga") Then .carga = rw("VALOR").ToString.Trim
                        If (Field = "comercial") Then .comercial = rw("VALOR").ToString.Trim
                        If (Field = "residencial") Then .residencial = rw("VALOR").ToString.Trim

                    Next
                End With

                Return _parametros
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Adapter.Dispose()
            ds.Dispose()
            cmd.Dispose()
        End Try
    End Function
    Public Function Bitacora(ByVal m_Modulo As String, ByVal valor As String, ByVal info As String) As Boolean
        Try
            'id, modulo,  valor, usuario, fecha, hora,info
            Return Ejecuta("INSERT INTO t_bitacora(modulo,valor,usuario,fecha,hora,info) VALUES('" & m_Modulo & "','" &
            valor & "','" & Current_Session.usuario & "','" & GetCurrentDate() & "','" & GetCurrentTime() &
            "','" & info & "')", "Bitacora(..")

        Catch ex As Exception
            MessageBox.Show(ex.Message & vbCrLf & "Sender : Bitacora(..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        End Try
    End Function

    Public Function LoginUSers() As Boolean
        Try
            'id, modulo,  valor, usuario, fecha, hora,info
            Return Ejecuta("INSERT INTO t_logins(usuario,lastLogin) VALUES('" & Current_Session.usuario & "','" &
             GetCurrentDateTime() & "')", "Bitacora(..")

        Catch ex As Exception
            MessageBox.Show(ex.Message & vbCrLf & "Sender : Bitacora(..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        End Try
    End Function

    Public Function IsSQLSecure(ByVal m_sql As String) As Boolean

        Try
            Dim keyword As String = m_sql.Trim.Substring(0, 7).Trim

            '=============================================
            '           Identificador UPDATE
            '=============================================
            If (keyword.Trim.ToUpper() = "UPDATE") Then
                Try
                    Dim arre() As String = m_sql.Trim.Split(Chr(32)) 'devuelve un arreglo con las palabras de la instrucción SQL

                    If (arre.Length > 0) Then
                        Dim hasWhere As Boolean = False
                        For Each Strg As String In arre
                            If (Strg.Trim.ToUpper() = "WHERE") Then
                                hasWhere = True
                                Exit For
                            End If
                        Next
                        Return hasWhere
                    Else
                        Return True
                    End If

                Catch ex As Exception
                    'Si hay un error en el string dar por bueno, y esperar la excepción de la función Ejecuta
                    Return True
                End Try
            Else
                Return True
            End If

        Catch ex As Exception
            'Si hay un error en el string dar por bueno, y esperar la excepción de la función Ejecuta
            Return True
        End Try
    End Function
    Public Function Ejecuta(ByVal sSQL As String, ByVal sender As String) As Boolean

        If (IsSQLSecure(sSQL) = False) Then
            MessageBox.Show("La consulta que intenta Ejecutar es Insegura, la operación no puede continuar !" &
            vbCrLf & "Sender:" & sender & vbCrLf, "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
            Exit Function
        End If

        Dim cmd As SqlCommand = Nothing

        Try
            cmd = New SqlCommand(sSQL, conexion)
            cmd.CommandTimeout = 15
            cmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            MessageBox.Show("sender:" + sender + vbCrLf + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            cmd.Dispose()
        End Try
    End Function

    Public Function EjecutaHISS(ByVal sSQL As String, ByVal sender As String) As Boolean

        If (IsSQLSecure(sSQL) = False) Then
            MessageBox.Show("La consulta que intenta Ejecutar es Insegura, la operación no puede continuar !" &
            vbCrLf & "Sender:" & sender & vbCrLf, "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
            Exit Function
        End If

        Dim cmd As SqlCommand = Nothing

        Try
            cmd = New SqlCommand(sSQL, oCn_WMS)
            cmd.CommandTimeout = 15
            cmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            MessageBox.Show("sender:" + sender + vbCrLf + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            cmd.Dispose()
        End Try
    End Function


    ''' <summary>
    ''' Devuelve la Fecha y hora del sistema en formato yyyy-MM-dd hh:mm:ss
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCurrentDateTime()

        Return GetCurrentDate() & " " & GetCurrentTime()
    End Function
    ''' <summary>
    ''' Devuelve la fecha actual del sistema en formato yyyy-MM-dd
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCurrentDate()

        Return Now.Date.ToString("yyyy-MM-dd")
    End Function
    ''' <summary>
    ''' Devuelve la fecha actual del sistema en formato yyyyMMdd
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCurrentDate2()

        Return Now.Date.ToString("yyyyMMdd")
    End Function
    ''' <summary>
    ''' Devuelve la fecha actual del sistema en formato dd/MM/yyyy
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCurrentDate3()

        Return Now.Date.ToString("dd/MM/yyyy")
    End Function
    ''' <summary>
    ''' devuelve los siguientes datos en el siguiente formato :  ddMMyyyyhhmmsss
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCurrentDateFileName()

        Return Now.Date.ToString("ddMMyyyy") & Now.ToString("hhmmss")
    End Function
    ''' <summary>
    ''' Devuelve la hora del sistema en formato hh:mm:ss
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCurrentTime()

        Return Now.ToString("hh:mm:ss")
    End Function

    Public Function GetCurrentTime24()

        Return Now.ToString("HH:mm:ss")
    End Function

    ''' <summary>
    ''' Devuelve la hora del sistema en formato hh:mm:ss A.M 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCurrentTime2()

        Return Now.ToString("hh:mm:ss tt")
    End Function
    Public Function GetCurrentTime3()

        Return Now.ToString("hhmmss")
    End Function

    Public Function sendEmail2(ByVal MailFrom As String, ByVal m_titulo As String, ByVal m_bodyHTML As String, ByVal sDestinatarios As String, Optional ByVal sFileName As String = Nothing, Optional ByVal nombreAdjunto As String = Nothing) As Boolean
        Dim Servidor As System.Net.Mail.SmtpClient = Nothing
        Try
            Dim correo As New System.Net.Mail.MailMessage()
            correo.From = New System.Net.Mail.MailAddress(MailFrom) ' cambiar correo
            correo.Subject = m_titulo.Trim
            Try
                If (sDestinatarios.Length > 0 And sDestinatarios.Trim.Contains("@")) Then
                    Dim arre() As String = sDestinatarios.Split(";")
                    For Each rw As String In arre
                        If rw.Trim <> "" Then
                            correo.To.Add(rw)
                        End If
                    Next
                Else
                    'correo.To.Add("rgfhes@gmail.com")
                End If
            Catch ex As Exception
                'correo.To.Add("rfghies@gmail.com")
            End Try

            '*Si lleva un archivo Adjunto
            Try
                If (Not IsNothing(sFileName)) Then
                    Dim archivo1 As New System.Net.Mail.Attachment(sFileName)
                    archivo1.Name = nombreAdjunto & ".pdf"
                    correo.Attachments.Add(archivo1)
                End If
            Catch ex As Exception

            End Try


            correo.IsBodyHtml = False
            correo.Body = m_bodyHTML
            Servidor = New System.Net.Mail.SmtpClient

            With Configuración_SMTP
                Servidor.Host = .STMP_Host
                Servidor.Port = .STMP_Port
                Servidor.EnableSsl = .STMP_SSL
                Servidor.Credentials = New System.Net.NetworkCredential(.STMP_UserName, .STMP_Password) ' correo y contraseña
            End With
            Servidor.Send(correo)
            Return True
            'Echo("Se ha enviado el historial del servidor al administrador del sistema")
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
            Exit Function
        Finally
            Servidor.Dispose()
            Servidor = Nothing
        End Try
    End Function

    'Public Function SendEmail(ByVal m_titulo As String, ByVal m_bodyHTML As String, ByVal sDestinatarios As String, Optional ByVal sFileName As String = Nothing) As Boolean

    '    ' + lnkSendEmailLOG.LinkClicked, 

    '    'Dim trd As New Threading.Thread(
    '    '      Sub()

    '    Dim Servidor As System.Net.Mail.SmtpClient = Nothing

    '    Try
    '        Dim correo As New System.Net.Mail.MailMessage()
    '        correo.From = New System.Net.Mail.MailAddress("rochadetechnologies@gmail.com") ' cambiar correo
    '        correo.Subject = m_titulo.Trim
    '        'correo.To.Add("soporte@rochade.com.mx") 'destino
    '        correo.To.Add("rochadetechnologies@gmail.com")

    '        Try
    '            If (sDestinatarios.Length > 0 And sDestinatarios.Trim.Contains("@")) Then
    '                Dim arre() As String = sDestinatarios.Split(";")
    '                For Each rw As String In arre
    '                    If rw.Trim <> "" Then
    '                        correo.To.Add(rw)
    '                    End If
    '                Next
    '            Else
    '                ' correo.To.Add("rochadetechnologies@gmail.com")
    '            End If

    '        Catch ex As Exception
    '            ' correo.To.Add("rochadetechnologies@gmail.com")
    '        End Try

    '        'Dim correo As New System.Net.Mail.MailMessage()
    '        'correo.From = New System.Net.Mail.MailAddress("rochadetechnologies@gmail.com") ' cambiar correo
    '        'correo.Subject = m_titulo.Trim
    '        ''correo.To.Add("soporte@rochade.com.mx") 'destino
    '        'correo.To.Add("rochadetechnologies@gmail.com")

    '        '*Si lleva un archivo Adjunto
    '        If (Not IsNothing(sFileName)) Then
    '            Dim archivo1 As New System.Net.Mail.Attachment(sFileName)
    '            archivo1.Name = sFileName '"| db_megacarnes.sql"
    '            correo.Attachments.Add(archivo1)
    '        End If

    '        '==================================================
    '        '      Si hay LOG de Errores de Transacciones
    '        '==================================================
    '        'ENTRADA DE MERCANCIA | PUNTO DE VENTA
    '        'If (File.Exists("errores.log") = True) Then
    '        '    Dim archivo1 As New System.Net.Mail.Attachment("errores.log")
    '        '    archivo1.Name = "errores.log"
    '        '    correo.Attachments.Add(archivo1)
    '        'End If

    '        correo.IsBodyHtml = True
    '        correo.Body = m_bodyHTML ' "<br>Application : Punto de Venta []<p>" &
    '        '"<br><p>Description : LOG del Servidor" &
    '        '"<br><br><p>Versión del Sistema : " & m_version.Trim & vbCrLf

    '        Servidor = New System.Net.Mail.SmtpClient
    '        With Configuración_SMTP
    '            Servidor.Host = .STMP_Host
    '            Servidor.Port = .STMP_Port
    '            Servidor.EnableSsl = .STMP_SSL
    '            Servidor.Credentials = New System.Net.NetworkCredential(.STMP_UserName, .STMP_Password) ' correo y contraseña
    '        End With

    '        Servidor.Send(correo)
    '        Return True
    '        'Echo("Se ha enviado el historial del servidor al administrador del sistema")

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        'EchoError("No ha podido enviarse la información solicitada, Codigo [843], descripción :" & ex.Message.ToString)
    '        Return False
    '        Exit Function
    '    Finally
    '        Servidor.Dispose()
    '        Servidor = Nothing
    '    End Try
    '    'End Sub)
    'End Function
    'Public Function SaveFileAs(ByVal sFileName As String, ByVal text As String, ByVal sobreEscribir As Boolean) As Boolean
    '    Try
    '        Dim file As System.IO.StreamWriter
    '        file = My.Computer.FileSystem.OpenTextFileWriter(sFileName, sobreEscribir)
    '        file.WriteLine(text)
    '        file.Close()
    '        Return True
    '    Catch ex As Exception
    '        MsgBox(ex.Message & vbCrLf & "sender : SaveFileAs(..", vbInformation, "Error")
    '        Return False
    '    End Try
    'End Function
    'Public Function GuardarErrorArchivoLOG(ByVal sFileName As String, ByVal text As String) As Boolean
    '    Try
    '        Dim file As System.IO.StreamWriter
    '        file = My.Computer.FileSystem.OpenTextFileWriter(sFileName, True)
    '        file.WriteLine(text)
    '        file.Close()
    '        Return True
    '    Catch ex As Exception
    '        MsgBox(ex.Message & vbCrLf & "sender : SaveFileAs(..", vbInformation, "Error")
    '        Return False
    '    End Try
    'End Function
    Public Sub LimpiaR(ByRef Contenedor As Control.ControlCollection)

        Try
            Dim tmp As Control
            For Each tmp In Contenedor
                If tmp.GetType Is GetType(GroupBox) Then
                    LimpiaR(DirectCast(tmp, GroupBox).Controls)
                ElseIf tmp.GetType Is GetType(Panel) Then
                    LimpiaR(DirectCast(tmp, Panel).Controls)
                ElseIf tmp.GetType Is GetType(FlowLayoutPanel) Then
                    LimpiaR(DirectCast(tmp, FlowLayoutPanel).Controls)
                ElseIf tmp.GetType Is GetType(TableLayoutPanel) Then
                    LimpiaR(DirectCast(tmp, TableLayoutPanel).Controls)
                ElseIf tmp.GetType Is GetType(SplitContainer) Then
                    LimpiaR(DirectCast(tmp, SplitContainer).Controls)
                ElseIf tmp.GetType Is GetType(TabControl) Then
                    LimpiaR(DirectCast(tmp, TabControl).Controls)
                ElseIf tmp.GetType Is GetType(TabPage) Then
                    LimpiaR(DirectCast(tmp, TabPage).Controls)
                ElseIf tmp.GetType Is GetType(MaskedTextBox) Then
                    LimpiaR(DirectCast(tmp, MaskedTextBox).Controls)
                Else
                    If TypeOf tmp Is TextBox Then DirectCast(tmp, TextBox).Clear()
                    ' If TypeOf tmp Is DataGridView Then DirectCast(tmp, DataGridView).DataSource = Nothing
                    If TypeOf tmp Is ComboBox Then DirectCast(tmp, ComboBox).Text = ""
                    If TypeOf tmp Is CheckBox Then DirectCast(tmp, CheckBox).Checked = False
                    ' If TypeOf tmp Is UCDecimal Then DirectCast(tmp, UCDecimal).Clear() 'Control de Usuario
                    If TypeOf tmp Is MaskedTextBox Then DirectCast(tmp, MaskedTextBox).Clear()
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message & vbCrLf & "Sender : LimpiaR(..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Sub LoadItemsInComboDelitos(ByVal cboTipos As ComboBox, ByVal delito As String)

        Try
            Dim m_sql As String = "SELECT tipo FROM t_delitos WHERE delito='" & delito & "'"
            cmd = New SqlCommand(m_sql, conexion)
            ds = New DataSet
            Adapter = New SqlDataAdapter
            Adapter.SelectCommand = cmd
            Adapter.Fill(ds)

            If (ds.Tables(0).Rows.Count > 0) Then
                For Each rw As DataRow In ds.Tables(0).Rows
                    cboTipos.Items.Add(rw("tipo").ToString)
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message & vbCrLf & "Sender : LoadInComboBox(..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ds = Nothing
            Adapter = Nothing
            cmd = Nothing
        End Try
    End Sub

    Public Sub LoadItemsInComboBox(ByVal cboTipos As ComboBox, ByVal tipo As String)

        Try
            Dim m_sql As String = "SELECT descripcion FROM t_inegi_tipos where tipo='" & tipo & "'"
            cmd = New SqlCommand(m_sql, conexion)
            ds = New DataSet
            Adapter = New SqlDataAdapter
            Adapter.SelectCommand = cmd
            Adapter.Fill(ds)

            If (ds.Tables(0).Rows.Count > 0) Then
                For Each rw As DataRow In ds.Tables(0).Rows
                    cboTipos.Items.Add(rw("descripcion").ToString)
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message & vbCrLf & "Sender : LoadInComboBox(..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ds = Nothing
            Adapter = Nothing
            cmd = Nothing
        End Try
    End Sub

    Public Sub LoadItemsInComboMotivos(ByVal cboTipos As ComboBox)

        Try
            Dim m_sql As String = "SELECT DISTINCT delito FROM t_delitos"
            cmd = New SqlCommand(m_sql, conexion)
            ds = New DataSet
            Adapter = New SqlDataAdapter
            Adapter.SelectCommand = cmd
            Adapter.Fill(ds)

            If (ds.Tables(0).Rows.Count > 0) Then
                For Each rw As DataRow In ds.Tables(0).Rows
                    cboTipos.Items.Add(rw("delito").ToString)
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message & vbCrLf & "Sender : LoadInComboBox(..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ds = Nothing
            Adapter = Nothing
            cmd = Nothing
        End Try
    End Sub
#End Region

End Module