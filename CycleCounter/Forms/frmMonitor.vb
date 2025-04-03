Imports System.IO
Imports System.Data.SqlClient
Imports System.Net
Imports System.Reflection
Imports System.Text
Imports System.Globalization
Imports System.Net.Mail
Imports System.Drawing
Imports System.Timers
Imports System.Xml
Imports System.Runtime.InteropServices ' Importar este espacio de nombres
Imports System.Drawing.Imaging

Public Class frmMonitor

    ' Declaración de la función PrintWindow
    <DllImport("user32.dll")>
    Private Shared Function PrintWindow(hWnd As IntPtr, hdcBlt As IntPtr, nFlags As Integer) As Boolean
    End Function

    ' Declaración de las funciones GetDC y ReleaseDC
    <DllImport("user32.dll")>
    Private Shared Function GetDC(hWnd As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Shared Function ReleaseDC(hWnd As IntPtr, hDC As IntPtr) As Integer
    End Function

    Public versionSistema As String = ""
    Dim capturarPantalla As Integer = 0
    Private contadorSegundos As Integer = 0
    Dim m_log As String = ""
    Dim n = 0
    Dim lastCode As String = ""
    Dim lastStatus As String = ""
    Dim machineL As String = ""
    Dim query As String
    Dim queryLeds As String = ""
    Dim plannedSeconds As Integer = 9000
    Dim labelSelected As Label
    Dim selectedDGV As DataGridView
    Dim panelSelected As Panel
    Dim primerDowntimeDelDia As String = ""
    Dim sumaSegundosAlDowntime As String = ""
    Dim turno As Integer = 0
    Dim queryTotal, queryComplement As String
    Dim dtQty As DataTable = New DataTable
    Dim dgvToFill As DataGridView
    Private posX As Integer
    Private posY As Integer
    Private pantallaPrincipal As Screen
    Dim idToSend, warningQuery As String
    Dim onlyShowShift1 As Boolean = True
    Dim segundosTotales As Integer = 0
    Dim horasTotales As Integer = 24
    Dim blinkBar As Boolean = False

    Dim ProductionPhones As String = QueryRow("SELECT celulares FROM t_bma_downtime_celulares WHERE depto='PRODUCTION'", "celulares", "buscacels")

    'PROGRESS BAR M1 100-------------------------------------
    Dim progressbarrunit As Double
    Dim progressbarwidth, progressbarheight As Integer
    Dim progressbarcomplete, totalProgress As Double
    Dim bmp As Bitmap
    Dim xpos As Double = 0
    Dim currentStatus As String = "D"
    '---------------------------------------------------------

    'PROGRESS BAR M1 400-------------------------------------
    Dim progressbarrunit_M1_400 As Double
    Dim progressbarwidth_M1_400, progressbarheight_M1_400 As Integer
    Dim progressbarcomplete_M1_400, totalProgress_M1_400 As Double
    Dim bmp_M1_400 As Bitmap
    Dim xpos400 As Double = 0
    Dim currentStatusm1_400 As String = "D"
    Dim segundosAlarma400 As Integer = 0
    '---------------------------------------------------------


    'PROGRESS BAR M1 700-------------------------------------
    Dim progressbarrunit_M1_700 As Double
    Dim progressbarwidth_M1_700, progressbarheight_M1_700 As Integer
    Dim progressbarcomplete_M1_700, totalProgress_M1_700 As Double
    Dim bmp_M1_700 As Bitmap
    Dim xpos700 As Double = 0
    Dim currentStatusm1_700 As String = "D"
    Dim segundosAlarma700 As Integer = 0
    '---------------------------------------------------------

    'PROGRESS BAR M2 100-------------------------------------
    Dim progressbarrunit_M2_100 As Double
    Dim progressbarwidth_M2_100, progressbarheight_M2_100 As Integer
    Dim progressbarcomplete_M2_100, totalProgress_M2_100 As Double
    Dim bmp_M2_100 As Bitmap
    Dim xpos2100 As Double = 0
    Dim currentStatusm2_100 As String = "D"
    Dim segundosAlarma2100 As Integer = 0
    '---------------------------------------------------------

    'PROGRESS BAR M2 400-------------------------------------
    Dim progressbarrunit_2400 As Double
    Dim progressbarwidth_2400, progressbarheight_2400 As Integer
    Dim progressbarcomplete_2400, totalProgress_2400 As Double
    Dim bmp_2400 As Bitmap
    Dim xpos2400 As Double = 0
    Dim currentStatus2400 As String = "D"
    Dim segundosAlarma2400 As Integer = "0"
    '---------------------------------------------------------

    'PROGRESS BAR M2 700-------------------------------------
    Dim progressbarrunit_2700 As Double
    Dim progressbarwidth_2700, progressbarheight_2700 As Integer
    Dim progressbarcomplete_2700, totalProgress_2700 As Double
    Dim bmp_2700 As Bitmap
    Dim xpos2700 As Double = 0
    Dim currentStatus2700 As String = "D"
    Dim segundosAlarma2700 As Integer = "0"
    '---------------------------------------------------------

    'PROGRESS BAR M3 100-------------------------------------
    Dim progressbarrunit_3100 As Double
    Dim progressbarwidth_3100, progressbarheight_3100 As Integer
    Dim progressbarcomplete_3100, totalProgress_3100 As Double
    Dim bmp_3100 As Bitmap
    Dim xpos3100 As Double = 0
    Dim currentStatus3100 As String = "D"
    Dim segundosAlarma3100 As Integer = "0"
    '---------------------------------------------------------

    'PROGRESS BAR M3 400-------------------------------------
    Dim progressbarrunit_3400 As Double
    Dim progressbarwidth_3400, progressbarheight_3400 As Integer
    Dim progressbarcomplete_3400, totalProgress_3400 As Double
    Dim bmp_3400 As Bitmap
    Dim xpos3400 As Double = 0
    Dim currentStatus3400 As String = "D"
    Dim segundosAlarma3400 As Integer = "0"
    '---------------------------------------------------------

    'PROGRESS BAR M3 700-------------------------------------
    Dim progressbarrunit_3700 As Double
    Dim progressbarwidth_3700, progressbarheight_3700 As Integer
    Dim progressbarcomplete_3700, totalProgress_3700 As Double
    Dim bmp_3700 As Bitmap
    Dim xpos3700 As Double = 0
    Dim currentStatus3700 As String = "D"
    Dim segundosAlarma3700 As Integer = "0"
    '---------------------------------------------------------

    'PROGRESS BAR M4 100-------------------------------------
    Dim progressbarrunit_4100 As Double
    Dim progressbarwidth_4100, progressbarheight_4100 As Integer
    Dim progressbarcomplete_4100, totalProgress_4100 As Double
    Dim bmp_4100 As Bitmap
    Dim xpos4100 As Double = 0
    Dim currentStatus4100 As String = "D"
    Dim segundosAlarma4100 As Integer = "0"
    '---------------------------------------------------------

    'PROGRESS BAR M4 400-------------------------------------
    Dim progressbarrunit_4400 As Double
    Dim progressbarwidth_4400, progressbarheight_4400 As Integer
    Dim progressbarcomplete_4400, totalProgress_4400 As Double
    Dim bmp_4400 As Bitmap
    Dim xpos4400 As Double = 0
    Dim currentStatus4400 As String = "D"
    Dim segundosAlarma4400 As Integer = "0"
    '---------------------------------------------------------

    'PROGRESS BAR M4 700-------------------------------------
    Dim progressbarrunit_4700 As Double
    Dim progressbarwidth_4700, progressbarheight_4700 As Integer
    Dim progressbarcomplete_4700, totalProgress_4700 As Double
    Dim bmp_4700 As Bitmap
    Dim xpos4700 As Double = 0
    Dim currentStatus4700 As String = "D"
    Dim segundosAlarma4700 As Integer = "0"
    '---------------------------------------------------------

    'PROGRESS BAR M5 100-------------------------------------
    Dim progressbarrunit_5100 As Double
    Dim progressbarwidth_5100, progressbarheight_5100 As Integer
    Dim progressbarcomplete_5100, totalProgress_5100 As Double
    Dim bmp_5100 As Bitmap
    Dim xpos5100 As Double = 0
    Dim currentStatus5100 As String = "D"
    Dim segundosAlarma5100 As Integer = "0"
    '---------------------------------------------------------

    'PROGRESS BAR M5 400-------------------------------------
    Dim progressbarrunit_5400 As Double
    Dim progressbarwidth_5400, progressbarheight_5400 As Integer
    Dim progressbarcomplete_5400, totalProgress_5400 As Double
    Dim bmp_5400 As Bitmap
    Dim xpos5400 As Double = 0
    Dim currentStatus5400 As String = "D"
    Dim segundosAlarma5400 As Integer = "0"
    '---------------------------------------------------------

    Dim progressbarrunit_5700 As Double
    Dim progressbarwidth_5700, progressbarheight_5700 As Integer
    Dim progressbarcomplete_5700, totalProgress_5700 As Double
    Dim bmp_5700 As Bitmap
    Dim xpos5700 As Double = 0
    Dim currentStatus5700 As String = "D"
    Dim segundosAlarma5700 As Integer = "0"

    Private Const CEROS_MAX = 2
    Dim g As Graphics
    Dim totaltiempo As TimeSpan
    Dim tiempoTranscurrido As String
    Dim segundosAlarma As Integer = 0
    Dim m As Integer = 0

    Dim isRunning1100 As Boolean = True
    Dim isRunning1400 As Boolean = True
    Dim isRunning1700 As Boolean = True
    Dim isRunning2100 As Boolean = True
    Dim isRunning2400 As Boolean = False
    Dim isRunning2700 As Boolean = True
    Dim isRunning3100 As Boolean = True
    Dim isRunning3400 As Boolean = True
    Dim isRunning3700 As Boolean = True
    Dim isRunning4100 As Boolean = False
    Dim isRunning4400 As Boolean = False
    Dim isRunning4700 As Boolean = False
    Dim isRunning5100 As Boolean = False
    Dim isRunning5400 As Boolean = False
    Dim isRunning5700 As Boolean = False

    Dim auxStatus As String = ""
    Dim auxStatus14 As String = ""
    Dim auxStatus17 As String = ""
    Dim auxStatus21 As String = ""
    Dim auxStatus24 As String = ""
    Dim auxStatus27 As String = ""
    Dim auxStatus31 As String = ""
    Dim auxStatus34 As String = ""
    Dim auxStatus37 As String = ""
    Dim auxStatus41 As String = ""
    Dim auxStatus44 As String = ""
    Dim auxStatus47 As String = ""
    Dim auxStatus51 As String = ""
    Dim auxStatus54 As String = ""
    Dim auxStatus57 As String = ""

    Dim first1100 As Boolean = True
    Dim first1400 As Boolean = True
    Dim first1700 As Boolean = True
    Dim first2100 As Boolean = True
    Dim first2400 As Boolean = True
    Dim first2700 As Boolean = True
    Dim first3100 As Boolean = True
    Dim first3400 As Boolean = True
    Dim first3700 As Boolean = True
    Dim first4100 As Boolean = True
    Dim first4400 As Boolean = True
    Dim first4700 As Boolean = True
    Dim first5100 As Boolean = True
    Dim first5400 As Boolean = True
    Dim first5700 As Boolean = True

    Public warning1100 As Boolean = False
    Public warning1400 As Boolean = False
    Public warning1700 As Boolean = False
    Public warning2100 As Boolean = False
    Public warning2400 As Boolean = False
    Public warning2700 As Boolean = False
    Public warning3100 As Boolean = False
    Public warning3400 As Boolean = False
    Public warning3700 As Boolean = False
    Public warning4100 As Boolean = False
    Public warning4400 As Boolean = False
    Public warning4700 As Boolean = False
    Public warning5100 As Boolean = False
    Public warning5400 As Boolean = False
    Public warning5700 As Boolean = False


    'variables para mensajes de whatsapp
    Dim w2100Help As Boolean = False
    Dim w2100Panic As Boolean = False
    Dim w2100Online As Boolean = False

    Dim w2400Help As Boolean = False
    Dim w2400Panic As Boolean = False
    Dim w2400Online As Boolean = False

    Dim w2700Help As Boolean = False
    Dim w2700Panic As Boolean = False
    Dim w2700Online As Boolean = False



    Dim w3100Help As Boolean = False
    Dim w3100Panic As Boolean = False
    Dim w3100Online As Boolean = False

    Dim w3400Help As Boolean = False
    Dim w3400Panic As Boolean = False
    Dim w3400Online As Boolean = False

    Dim w3700Help As Boolean = False
    Dim w3700Panic As Boolean = False
    Dim w3700Online As Boolean = False

    'variables para los numeros de whatsapp
    Dim productionNumbers, managersNumbers As String



    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        Control.CheckForIllegalCrossThreadCalls = False

    End Sub


    Private Sub loadBD()
        Try
            '======================================
            '    Leyendo Configuración de BD
            '======================================
iniciar:

            Try
                conexion = New SqlConnection()
                'conexion al RPBS server 

                If debugMode = True Then
                    If GetComputerName() = "arturo-garcia" Then
                        conexion.ConnectionString = "Initial Catalog=db_kyungshin;Data Source=ARTURO-GARCIA\SQLEXPRESS01; Integrated Security=True"
                    Else
                        conexion.ConnectionString = "Initial Catalog=db_kyungshin;Data Source=LAPTOP-ROUA60BG; Integrated Security=True"
                    End If
                Else
                    conexion.ConnectionString = "Initial Catalog=db_kyungshin;Data Source=172.30.61.20; User Id=sistemas;pwd=kyungshin2!"
                End If

                conexion.Open()

            Catch ex As Exception
                MessageBox.Show("Can't connect to database. " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Application.Exit()
            End Try


        Catch ex As Exception
            MessageBox.Show("No se puede conectar al host. " & ex.ToString, "Error de conexión al iniciar sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit()
        End Try
    End Sub
    Public Function ceros(Nro As String, Cantidad As Integer) As String
        Try
            Dim numero As String, cuantos As String, i As Integer
            numero = Trim(Nro)
            cuantos = "0"
            For i = 1 To Cantidad
                cuantos = cuantos & "0"
            Next i
            ceros = Mid(cuantos, 1, Cantidad - Len(numero)) & numero
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Private Sub openBreakTimeEditor()
        Try
            Dim frm As New frmBreaktime()
            frm.ShowDialog()
            If frm.iResult = True Then
                Panel11.Visible = True
                intialLoad()
                lbCounter.Text = "100"
            Else
                Panel11.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub breakTime(picbb As PictureBox, bm As Bitmap, linea As String)
        Dim pbu As Double = picbb.Width / segundosTotales
        g = Graphics.FromImage(bm)
        Dim factor As Double = (picbb.Width / horasTotales) / 4.4  'este factor se calcula con el width del picbox. (width / 24) / 4 (cada 15 mins 15x4=60. Una hora)



        Dim arre2 As DataRowCollection = GetRows("SELECT * FROM [t_bma_breaktime] WHERE line='" & linea & "'")

        If Not IsNothing(arre2) Then
            For Each item As DataRow In arre2

                If onlyShowShift1 = True Then
                    cbShift1.Text = item("startup1").ToString.Trim

                    'startup(1st)                                                                         10 es el ancho de la barra equivalente a n minutos
                    g.FillRectangle(Brushes.SandyBrown, New Rectangle(cbShift1.SelectedIndex * factor, 0, 25, picbb.Height)) '7 a 7:15  valor=295
                    picbb.Image = bm

                    cbShift1.Text = item("break1").ToString.Trim

                    'breaktime1
                    g.FillRectangle(Brushes.SandyBrown, New Rectangle(cbShift1.SelectedIndex * factor, 0, 25, picbb.Height)) '8:45 a9  v=368
                    picbb.Image = bm

                    cbShift1.Text = item("lunch1").ToString.Trim
                    'lunch
                    g.FillRectangle(Brushes.SandyBrown, New Rectangle(cbShift1.SelectedIndex * factor, 0, 50, picbb.Height)) '12:45 a 13:15 v=538
                    picbb.Image = bm

                    cbShift1.Text = item("break2").ToString.Trim
                    'breaktime2
                    g.FillRectangle(Brushes.SandyBrown, New Rectangle(cbShift1.SelectedIndex * factor, 0, 25, picbb.Height)) '16:45 a 17:00 v=706
                    picbb.Image = bm
                Else
                    cboMinutes.Text = item("startup1").ToString.Trim

                    'startup(1st)
                    g.FillRectangle(Brushes.SandyBrown, New Rectangle(cboMinutes.SelectedIndex * factor, 0, 10, picbb.Height)) '7 a 7:15  valor=295
                    picbb.Image = bm

                    cboMinutes.Text = item("break1").ToString.Trim

                    'breaktime1
                    g.FillRectangle(Brushes.SandyBrown, New Rectangle(cboMinutes.SelectedIndex * factor, 0, 10, picbb.Height)) '8:45 a9  v=368
                    picbb.Image = bm

                    cboMinutes.Text = item("lunch1").ToString.Trim
                    'lunch
                    g.FillRectangle(Brushes.SandyBrown, New Rectangle(cboMinutes.SelectedIndex * factor, 0, 20, picbb.Height)) '12:45 a 13:15 v=538
                    picbb.Image = bm

                    cboMinutes.Text = item("break2").ToString.Trim
                    'breaktime2
                    g.FillRectangle(Brushes.SandyBrown, New Rectangle(cboMinutes.SelectedIndex * factor, 0, 10, picbb.Height)) '16:45 a 17:00 v=706
                    picbb.Image = bm

                    cboMinutes.Text = item("startup2").ToString.Trim
                    'startup 2
                    g.FillRectangle(Brushes.SandyBrown, New Rectangle(cboMinutes.SelectedIndex * factor, 0, 10, picbb.Height)) '19:00 a 19:15 v=800
                    picbb.Image = bm

                    cboMinutes.Text = item("lunch2").ToString.Trim
                    'lunch 2
                    g.FillRectangle(Brushes.SandyBrown, New Rectangle(cboMinutes.SelectedIndex * factor, 0, 20, picbb.Height)) '22 a 22:30 v=927
                    picbb.Image = bm

                    cboMinutes.Text = item("break3").ToString.Trim
                    'break time 3
                    g.FillRectangle(Brushes.SandyBrown, New Rectangle(cboMinutes.SelectedIndex * factor, 0, 10, picbb.Height)) '02:30 a 02:45 v=105
                    picbb.Image = bm

                    cboMinutes.Text = item("break4").ToString.Trim
                    'break time 4
                    g.FillRectangle(Brushes.SandyBrown, New Rectangle(cboMinutes.SelectedIndex * factor, 0, 10, picbb.Height)) '05:30 a 05:45 v=231
                    picbb.Image = bm
                End If

            Next
        End If

    End Sub
    Private Sub colorVerde(panelTimer As Panel, lbSeg As Label, lbMins As Label, lbhora As Label, lbpunto1 As Label, lbpunto2 As Label)
        panelTimer.BackColor = Color.Green
        lbSeg.ForeColor = Color.Black
        lbMins.ForeColor = Color.Black
        lbhora.ForeColor = Color.Black
        lbSeg.BackColor = Color.Green
        lbMins.BackColor = Color.Green
        lbhora.BackColor = Color.Green
        lbpunto1.BackColor = Color.Green
        lbpunto2.BackColor = Color.Green
        lbpunto1.ForeColor = Color.Black
        lbpunto2.ForeColor = Color.Black
    End Sub

    Private Sub colorRojo(panelTimer As Panel, lbSeg As Label, lbMins As Label, lbhora As Label, lbpunto1 As Label, lbpunto2 As Label)
        panelTimer.BackColor = Color.Red
        lbSeg.ForeColor = Color.White
        lbMins.ForeColor = Color.White
        lbhora.ForeColor = Color.White
        lbSeg.BackColor = Color.Red
        lbMins.BackColor = Color.Red
        lbhora.BackColor = Color.Red
        lbpunto1.BackColor = Color.Red
        lbpunto2.BackColor = Color.Red
        lbpunto1.ForeColor = Color.White
        lbpunto2.ForeColor = Color.White
    End Sub

    Dim hora2 As DateTime
    Dim hora1 As DateTime
    Dim horaAcumTime, min, seg As String
    Private Function acumTime(hora11 As String, hora21 As String) As String
        '   Dim resta As String = DateDiff("n", , ) / 60

        hora2 = hora21
        hora1 = hora11

        totaltiempo = hora2.Subtract(hora1)
        If totaltiempo.Hours.ToString.Length = 1 Then
            horaAcumTime = "0" & totaltiempo.Hours
        Else
            horaAcumTime = totaltiempo.Hours
        End If
        If totaltiempo.Minutes.ToString.Length = 1 Then
            min = "0" & totaltiempo.Minutes
        Else
            min = totaltiempo.Minutes
        End If
        If totaltiempo.Seconds.ToString.Length = 1 Then
            seg = "0" & totaltiempo.Seconds
        Else
            seg = totaltiempo.Seconds
        End If
        tiempoTranscurrido = horaAcumTime & ":" & min & ":" & seg
        Return tiempoTranscurrido
    End Function



    Private Sub TurnONLedsStatus(numProceso As String, tVerde As Timer, trojo As Timer)
        Dim queryCompl As String = ""

        If numProceso <> "" Then
            queryCompl = " AND d.process='" & numProceso & "' "
        End If

        query = "select top 1 status FROM [db_kyungshin].[dbo].[t_bma_downtime] d 
WHERE  CONVERT(date, insert_date) = '" & Date.Now.ToShortDateString() & "'  " & queryCompl & " 
  order by id desc"

        lastStatus = QueryRow(query, "status", "statusEnd")

        If lastStatus = "RUN" Then
            lbAlarm.Text = ""
            n = 1
            tVerde.Start()
            trojo.Stop()
        Else
            trojo.Start()
            tVerde.Stop()

            n = 0
            query = "select top 1 code FROM [db_kyungshin].[dbo].[t_bma_downtime] d
WHERE CONVERT(date, insert_date) = '" & Date.Now.ToShortDateString() & "' and STATUS='RUN'  " & queryCompl & " 
  order by id desc"

            lastCode = QueryRow(query, "code", "buscaLastCode")

            ' lbAlarma.Text = "Current Alarm: " & lastCode
        End If

    End Sub

    Private Sub clearAlarmTime(lbseg As Label, lbmins As Label, lbhora As Label)
        lbseg.Text = "00"
        lbmins.Text = "00"
        lbhora.Text = "00"
    End Sub

    Declare Auto Function SendMessage Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

    Dim horaAcum As String = ""
    Dim queryCompl As String = ""
    Dim horaS As String = ""
    Dim horaSeparada() As String
    Dim horaDT7am As TimeSpan
    Private Sub loadAll(labelDT As Label, labelRT As Label, labelOF As Label, Optional NumProceso As String = "")

        queryCompl = ""
        If NumProceso <> "" Then
            queryCompl = " AND d.process='" & NumProceso & "' "
        End If

        Erase horaSeparada
        horaAcum = ""
        If NumProceso = "2700" Then
            Console.WriteLine("here")
        End If
        'ACUMULADO DE TIEMPO MUERTO POR TURNOS, 7AM A 7PM

        Try
            If turno = 1 Then
                horaS = QueryRow("exec spGetHours1 '" & Date.Now.ToShortDateString() & "','DOWN','" & NumProceso & "'", "time", "buscaDT")
            Else
                horaS = QueryRow("exec spGetHours2 '" & Date.Now.ToShortDateString() & "','DOWN','" & NumProceso & "'", "time", "buscaDT")
            End If


            If horaS = "" Then
                labelDT.Text = "00:00:00"
            Else
                labelDT.Text = horaS
            End If

        Catch ex As Exception
            labelDT.Text = "00:00:00"
        End Try

        Try
            If turno = 1 Then
                horaS = QueryRow("exec spGetHours1 '" & Date.Now.ToShortDateString() & "','RUN','" & NumProceso & "'", "time", "buscaDT")
            Else
                horaS = QueryRow("exec spGetHours2 '" & Date.Now.ToShortDateString() & "','RUN','" & NumProceso & "'", "time", "buscaDT")
            End If


            If horaS = "" Then
                labelRT.Text = "00:00:00"
            Else
                labelRT.Text = horaS
            End If
        Catch ex As Exception
            labelRT.Text = "00:00:00"
        End Try

        horaDT7am = (TimeSpan.Parse(Date.Now.ToShortTimeString) - (TimeSpan.Parse(labelDT.Text) + TimeSpan.Parse(labelRT.Text))) - TimeSpan.Parse("07:00:00")
        labelDT.Text = (horaDT7am + TimeSpan.Parse(labelDT.Text)).ToString



        ''ACUMULADO DE TIEMPO MUERTO RUN POR TURNOS DE 7AM A 7PM
        'Try
        '    '  horaSeparada = Split(QueryRow("SELECT DATEADD(ms, SUM(DATEDIFF(ms, '00:00:00', acumtime)), '00:00:00') as time FROM [t_bma_downtime] d 
        '    '     where CONVERT(date, insert_date) = '" & Date.Now.ToShortDateString() & "' and status='RUN' " & queryCompl & "", "time", "buscaDT"), " ")
        '    'labelRT.Text = horaSeparada(1)
        '    'acumularHoraActual(labelDT, labelRT, labelOF, NumProceso)
        'Catch ex As Exception

        'End Try

        'Try
        '    ' horaSeparada = Split(QueryRow("SELECT DATEADD(ms, SUM(DATEDIFF(ms, '00:00:00', acumtime)), '00:00:00') as time FROM [t_bma_downtime] d 
        '    '           where CONVERT(date, insert_date) = '" & Date.Now.ToShortDateString() & "' and status='OFF' " & queryCompl & "", "time", "buscaDT"), " ")
        '    'labelOF.Text = horaSeparada(1)
        'Catch ex As Exception

        'End Try

    End Sub

    Dim ultimoRow2 As New DataTable
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            'ESTE SE ENCARGA DEL COLOR DEL LED


            Timer1.Stop()
            'TurnONLedsStatus("1100", timerVerde, timerRojo)
            loadAll(lbDT, LBrUN, lbOtm1100, "1100")

            ultimoRow2 = EjecutaSelects("select top 1 status,alarmedDepto FROM [db_kyungshin].[dbo].[t_bma_downtime] d 
                                    WHERE  Convert(Date, insert_date) = '" & Date.Now.ToShortDateString() & "' and process='1100' 
                                      order by id desc", "buscaultimoROW")
            ' queryLeds = CType(ultimoRow2.Rows(0)("status"), String)

            'queryLeds = "select top 1 status FROM [db_kyungshin].[dbo].[t_bma_downtime] d 
            'WHERE  CONVERT(date, insert_date) = '" & Date.Now.ToShortDateString() & "' and process='1100' 
            '  order by id desc"

            lastStatus = CType(ultimoRow2.Rows(0)("status"), String)

            If lastStatus = "DOWN" Then
                m100.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_OFF
                If first1100 = True Then
                    first1100 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus Then
                    m100.hasChanged = True
                Else
                    m100.hasChanged = False
                End If

                ' AQUI SE CAMBIA EL COLOR DE LED SI UN DEPARTAMENTO ESTA ALARMADO, SI NO, SOLO SERA ROJO O VERDE
                'queryLeds = "select top 1 alarmedDepto FROM [db_kyungshin].[dbo].[t_bma_downtime] d 
                'WHERE  CONVERT(date, insert_date) = '" & Date.Now.ToShortDateString() & "' and process='1100' 
                '  order by id desc"
                m100.depto = CType(ultimoRow2.Rows(0)("alarmedDepto"), String)
            Else
                m100.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_RUN
                If first1100 = True Then
                    first1100 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus Then
                    m100.hasChanged = True
                Else
                    m100.hasChanged = False
                End If

            End If
primeraVez:

            auxStatus = lastStatus
            Timer1.Start()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub timerm1400_Tick(sender As Object, e As EventArgs) Handles timerm1400.Tick
        Try
            timerm1400.Stop()
            loadAll(lbDTm1400, lbRTm1400, lbOtm1400, "1400")

            ultimoRow2 = EjecutaSelects("select top 1 status,alarmedDepto FROM [db_kyungshin].[dbo].[t_bma_downtime] d 
                                    WHERE  Convert(Date, insert_date) = '" & Date.Now.ToShortDateString() & "' and process='1400' 
                                      order by id desc", "buscaultimoROW")

            lastStatus = CType(ultimoRow2.Rows(0)("status"), String)

            If lastStatus = "DOWN" Then
                l1400.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_OFF
                If first1400 = True Then
                    first1400 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus14 Then
                    l1400.hasChanged = True
                Else
                    l1400.hasChanged = False
                End If
                l1400.depto = CType(ultimoRow2.Rows(0)("alarmedDepto"), String)
            Else
                l1400.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_RUN
                If first1400 = True Then
                    first1400 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus14 Then
                    l1400.hasChanged = True
                Else
                    l1400.hasChanged = False
                End If

            End If
primeraVez:

            auxStatus14 = lastStatus
            timerm1400.Start()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs)
        'ProgresoGuardadoLineas()

        Dim frm As New frmResultHour()
        frm.ShowDialog()
    End Sub


    Private Sub timer2400_Tick(sender As Object, e As EventArgs) Handles timer2400.Tick
        Try
            timer2400.Stop()
            loadAll(lbDt2400, lbRT2400, lbOT2400, 2400)

            ultimoRow2 = EjecutaSelects("select top 1 status,alarmedDepto FROM [db_kyungshin].[dbo].[t_bma_downtime] d 
                                    WHERE  Convert(Date, insert_date) = '" & Date.Now.ToShortDateString() & "' and process='2400' 
                                      order by id desc", "buscaultimoROW")

            lastStatus = CType(ultimoRow2.Rows(0)("status"), String)

            If lastStatus = "DOWN" Then
                l2400.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_OFF
                If first2400 = True Then
                    first2400 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus24 Then
                    l2400.hasChanged = True
                Else
                    l2400.hasChanged = False
                End If
                l2400.depto = CType(ultimoRow2.Rows(0)("alarmedDepto"), String)
            Else
                l2400.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_RUN
                If first2400 = True Then
                    first2400 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus24 Then
                    l2400.hasChanged = True
                Else
                    l2400.hasChanged = False
                End If

            End If
primeraVez:

            auxStatus24 = lastStatus
            timer2400.Start()
        Catch ex As Exception
        End Try
    End Sub



    Private Sub timer2700_Tick(sender As Object, e As EventArgs) Handles timer2700.Tick
        Try
            timer2700.Stop()
            loadAll(lbDT2700, lbRt2700, lbOT2700, 2700)

            ultimoRow2 = EjecutaSelects("select top 1 status,alarmedDepto FROM [db_kyungshin].[dbo].[t_bma_downtime] d 
                                    WHERE  Convert(Date, insert_date) = '" & Date.Now.ToShortDateString() & "' and process='2700' 
                                      order by id desc", "buscaultimoROW")

            lastStatus = CType(ultimoRow2.Rows(0)("status"), String)

            If lastStatus = "DOWN" Then
                l2700.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_OFF
                If first2700 = True Then
                    first2700 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus27 Then
                    l2700.hasChanged = True
                Else
                    l2700.hasChanged = False
                End If
                l2700.depto = CType(ultimoRow2.Rows(0)("alarmedDepto"), String)
            Else
                l2700.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_RUN
                If first2700 = True Then
                    first2700 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus27 Then
                    l2700.hasChanged = True
                Else
                    l2700.hasChanged = False
                End If

            End If
primeraVez:

            auxStatus27 = lastStatus
            timer2700.Start()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub timer3100_Tick(sender As Object, e As EventArgs) Handles timer3100.Tick
        Try
            timer3100.Stop()
            loadAll(lbDT3100, lbRT3100, lbOT3100, 3100)

            ultimoRow2 = EjecutaSelects("select top 1 status,alarmedDepto FROM [db_kyungshin].[dbo].[t_bma_downtime] d 
                                    WHERE  Convert(Date, insert_date) = '" & Date.Now.ToShortDateString() & "' and process='3100' 
                                      order by id desc", "buscaultimoROW")

            lastStatus = CType(ultimoRow2.Rows(0)("status"), String)

            If lastStatus = "DOWN" Then
                l3100.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_OFF
                If first3100 = True Then
                    first3100 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus31 Then
                    l3100.hasChanged = True
                Else
                    l3100.hasChanged = False
                End If
                l3100.depto = CType(ultimoRow2.Rows(0)("alarmedDepto"), String)
            Else
                l3100.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_RUN
                If first3100 = True Then
                    first3100 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus31 Then
                    l3100.hasChanged = True
                Else
                    l3100.hasChanged = False
                End If
            End If
primeraVez:

            auxStatus31 = lastStatus
            timer3100.Start()
        Catch ex As Exception
        End Try
    End Sub



    Private Sub timer3400_Tick(sender As Object, e As EventArgs) Handles timer3400.Tick
        Try
            timer3400.Stop()
            loadAll(lbDT3400, lbRT3400, lbOT3400, 3400)

            ultimoRow2 = EjecutaSelects("select top 1 status,alarmedDepto FROM [db_kyungshin].[dbo].[t_bma_downtime] d 
                                    WHERE  Convert(Date, insert_date) = '" & Date.Now.ToShortDateString() & "' and process='3400' 
                                      order by id desc", "buscaultimoROW")

            lastStatus = CType(ultimoRow2.Rows(0)("status"), String)

            If lastStatus = "DOWN" Then
                l3400.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_OFF
                If first3400 = True Then
                    first3400 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus34 Then
                    l3400.hasChanged = True
                Else
                    l3400.hasChanged = False
                End If
                l3400.depto = CType(ultimoRow2.Rows(0)("alarmedDepto"), String)
            Else
                l3400.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_RUN
                If first3400 = True Then
                    first3400 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus34 Then
                    l3400.hasChanged = True
                Else
                    l3400.hasChanged = False
                End If

            End If
primeraVez:

            auxStatus34 = lastStatus
            timer3400.Start()
        Catch ex As Exception
        End Try
    End Sub



    Private Sub timer3700_Tick(sender As Object, e As EventArgs) Handles timer3700.Tick
        Try
            timer3700.Stop()
            loadAll(lbDT3700, lbRT3700, lbOT3700, 3700)

            ultimoRow2 = EjecutaSelects("select top 1 status,alarmedDepto FROM [db_kyungshin].[dbo].[t_bma_downtime] d 
                                    WHERE  Convert(Date, insert_date) = '" & Date.Now.ToShortDateString() & "' and process='3700' 
                                      order by id desc", "buscaultimoROW")



            lastStatus = CType(ultimoRow2.Rows(0)("status"), String)

            If lastStatus = "DOWN" Then
                l3700.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_OFF
                If first3700 = True Then
                    first3700 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus37 Then
                    l3700.hasChanged = True
                Else
                    l3700.hasChanged = False
                End If
                l3700.depto = CType(ultimoRow2.Rows(0)("alarmedDepto"), String)
            Else
                l3700.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_RUN
                If first3700 = True Then
                    first3700 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus37 Then
                    l3700.hasChanged = True
                Else
                    l3700.hasChanged = False
                End If

            End If
primeraVez:

            auxStatus37 = lastStatus
            timer3700.Start()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub timer4100_Tick(sender As Object, e As EventArgs) Handles timer4100.Tick
        Try
            timer4100.Stop()
            loadAll(lbDT4100, lbRT4100, lbOT4100, "4100")

            queryLeds = "select top 1 status FROM [db_kyungshin].[dbo].[t_bma_downtime] d 
            WHERE  CONVERT(date, insert_date) = '" & Date.Now.ToShortDateString() & "' and process='4100' 
              order by id desc"

            lastStatus = QueryRow(queryLeds, "status", "statusEnd")

            If lastStatus = "DOWN" Then
                l4100.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_OFF
                If first4100 = True Then
                    first4100 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus41 Then
                    l4100.hasChanged = True
                Else
                    l4100.hasChanged = False
                End If

            Else
                l4100.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_RUN
                If first4100 = True Then
                    first4100 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus41 Then
                    l4100.hasChanged = True
                Else
                    l4100.hasChanged = False
                End If

            End If
primeraVez:

            auxStatus41 = lastStatus
            timer4100.Start()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub EditBreakTimeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditBreakTimeToolStripMenuItem.Click
        openBreakTimeEditor()
    End Sub

    Private Sub timer4400_Tick(sender As Object, e As EventArgs) Handles timer4400.Tick
        Try
            timer4400.Stop()
            loadAll(lbDT4400, lbRt4400, lbOT4400, 4400)

            queryLeds = "select top 1 status FROM [db_kyungshin].[dbo].[t_bma_downtime] d 
            WHERE  CONVERT(date, insert_date) = '" & Date.Now.ToShortDateString() & "' and process='4400' 
              order by id desc"

            lastStatus = QueryRow(queryLeds, "status", "statusEnd")

            If lastStatus = "DOWN" Then
                l4400.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_OFF
                If first4400 = True Then
                    first4400 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus44 Then
                    l4400.hasChanged = True
                Else
                    l4400.hasChanged = False
                End If

            Else
                l4400.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_RUN
                If first4400 = True Then
                    first4400 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus44 Then
                    l4400.hasChanged = True
                Else
                    l4400.hasChanged = False
                End If

            End If
primeraVez:

            auxStatus44 = lastStatus
            timer4400.Start()
        Catch ex As Exception
        End Try
    End Sub


    Private Sub timer4700_Tick(sender As Object, e As EventArgs) Handles timer4700.Tick
        Try
            timer4700.Stop()
            loadAll(lbDT4700, lbRT4700, lbOT4700, 4700)

            queryLeds = "select top 1 status FROM [db_kyungshin].[dbo].[t_bma_downtime] d 
            WHERE  CONVERT(date, insert_date) = '" & Date.Now.ToShortDateString() & "' and process='4700' 
              order by id desc"

            lastStatus = QueryRow(queryLeds, "status", "statusEnd")

            If lastStatus = "DOWN" Then
                l4700.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_OFF
                If first4700 = True Then
                    first4700 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus47 Then
                    l4700.hasChanged = True
                Else
                    l4700.hasChanged = False
                End If

            Else
                l4700.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_RUN
                If first4700 = True Then
                    first4700 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus47 Then
                    l4700.hasChanged = True
                Else
                    l4700.hasChanged = False
                End If

            End If
primeraVez:

            auxStatus47 = lastStatus
            timer4700.Start()
        Catch ex As Exception
        End Try
    End Sub



    Private Sub timer5100_Tick(sender As Object, e As EventArgs) Handles timer5100.Tick
        Try
            timer5100.Stop()
            loadAll(lbDT5100, lbRT5100, lbOT5100, 5100)

            queryLeds = "select top 1 status FROM [db_kyungshin].[dbo].[t_bma_downtime] d 
            WHERE  CONVERT(date, insert_date) = '" & Date.Now.ToShortDateString() & "' and process='5100' 
              order by id desc"

            lastStatus = QueryRow(queryLeds, "status", "statusEnd")

            If lastStatus = "DOWN" Then
                l5100.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_OFF
                If first5100 = True Then
                    first5100 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus51 Then
                    l5100.hasChanged = True
                Else
                    l5100.hasChanged = False
                End If

            Else
                l5100.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_RUN
                If first5100 = True Then
                    first5100 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus51 Then
                    l5100.hasChanged = True
                Else
                    l5100.hasChanged = False
                End If

            End If
primeraVez:

            auxStatus51 = lastStatus
            timer5100.Start()
        Catch ex As Exception
        End Try
    End Sub



    Private Sub timer5400_Tick(sender As Object, e As EventArgs) Handles timer5400.Tick
        Try
            timer5400.Stop()
            loadAll(lbDT5400, lbRT5400, lbOT5400, 5400)

            queryLeds = "select top 1 status FROM [db_kyungshin].[dbo].[t_bma_downtime] d 
            WHERE  CONVERT(date, insert_date) = '" & Date.Now.ToShortDateString() & "' and process='5400' 
              order by id desc"

            lastStatus = QueryRow(queryLeds, "status", "statusEnd")

            If lastStatus = "DOWN" Then
                l5400.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_OFF
                If first5400 = True Then
                    first5400 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus54 Then
                    l5400.hasChanged = True
                Else
                    l5400.hasChanged = False
                End If

            Else
                l5400.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_RUN
                If first5400 = True Then
                    first5400 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus54 Then
                    l5400.hasChanged = True
                Else
                    l5400.hasChanged = False
                End If

            End If
primeraVez:

            auxStatus54 = lastStatus
            timer5400.Start()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        OpenDetails("1100")
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        OpenDetails("1400")
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        OpenDetails("1700")
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        OpenDetails("2100")
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        OpenDetails("2400")
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        OpenDetails("2700")
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        OpenDetails("3100")
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        OpenDetails("3400")
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        OpenDetails("3700")
    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click
        OpenDetails("4100")
    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        OpenDetails("4400")
    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click
        OpenDetails("4700")
    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs) Handles Label19.Click
        OpenDetails("5100")
    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click
        OpenDetails("5400")
    End Sub

    Private Sub Label17_Click(sender As Object, e As EventArgs) Handles Label17.Click
        OpenDetails("5700")
    End Sub

    Private Sub timer5700_Tick(sender As Object, e As EventArgs) Handles timer5700.Tick
        Try
            timer5700.Stop()
            loadAll(lbDT5700, lbRT5700, lbOt5700, 5700)

            queryLeds = "select top 1 status FROM [db_kyungshin].[dbo].[t_bma_downtime] d 
            WHERE  CONVERT(date, insert_date) = '" & Date.Now.ToShortDateString() & "' and process='5700' 
              order by id desc"

            lastStatus = QueryRow(queryLeds, "status", "statusEnd")

            If lastStatus = "DOWN" Then
                l5700.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_OFF
                If first5700 = True Then
                    first5700 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus57 Then
                    l5700.hasChanged = True
                Else
                    l5700.hasChanged = False
                End If

            Else
                l5700.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_RUN
                If first5700 = True Then
                    first5700 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus57 Then
                    l5700.hasChanged = True
                Else
                    l5700.hasChanged = False
                End If

            End If
primeraVez:

            auxStatus57 = lastStatus
            timer5700.Start()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub hideLine(line As String)
        Try
            Select Case line
                Case "1100"
                    p1100.Visible = True
                    p1100.BringToFront()
                Case "1400"
                    p1400.Visible = True
                    p1400.BringToFront()
                Case "1700"
                    p1700.Visible = True
                    p1700.BringToFront()
                Case "2100"
                    p2100.Visible = True
                    p2100.BringToFront()
                Case "2400"
                    p2400.Visible = True
                    p2400.BringToFront()
                Case "2700"
                    p2700.Visible = True
                    p2700.BringToFront()
                Case "3100"
                    p3100.Visible = True
                    p3100.BringToFront()
                Case "3400"
                    p3400.Visible = True
                    p3400.BringToFront()
                Case "3700"
                    p3700.Visible = True
                    p3700.BringToFront()
                Case "4100"
                    p4100.Visible = True
                    p4100.BringToFront()
                Case "4400"
                    p4400.Visible = True
                    p4400.BringToFront()
                Case "4700"
                    p4700.Visible = True
                    p4700.BringToFront()
                Case "5100"
                    p5100.Visible = True
                    p5100.BringToFront()
                Case "5400"
                    p5400.Visible = True
                    p5400.BringToFront()
                Case "5700"
                    p5700.Visible = True
                    p5700.BringToFront()

            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub HideToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HideToolStripMenuItem.Click
        hideLine(labelSelected.Tag)
    End Sub

    Private Sub Label4_MouseDown(sender As Object, e As MouseEventArgs) Handles Label4.MouseDown
        mouseDownLabel(sender, e)
    End Sub


    Private Sub mouseDownLabel(labelclicked As Label, e As MouseEventArgs)
        Try
            labelSelected = labelclicked
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgv1700_MouseDown(sender As Object, e As MouseEventArgs) Handles dgv1700.MouseDown
        mouseDownDgv(sender, e)
    End Sub

    Private Sub mouseDownDgv(dgvClicked As DataGridView, e As MouseEventArgs)
        Try
            selectedDGV = dgvClicked
        Catch ex As Exception

        End Try
    End Sub

    Private Sub mouseDownPanel(panelClicked As Panel, e As MouseEventArgs)
        Try
            panelSelected = panelClicked
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label5_MouseDown(sender As Object, e As MouseEventArgs) Handles Label5.MouseDown
        mouseDownLabel(sender, e)
    End Sub

    Private Sub Label6_MouseDown(sender As Object, e As MouseEventArgs) Handles Label6.MouseDown
        mouseDownLabel(sender, e)
    End Sub

    Private Sub Label9_MouseDown(sender As Object, e As MouseEventArgs) Handles Label9.MouseDown
        mouseDownLabel(sender, e)
    End Sub

    Private Sub Label8_MouseDown(sender As Object, e As MouseEventArgs) Handles Label8.MouseDown
        mouseDownLabel(sender, e)
    End Sub

    Private Sub Label7_MouseDown(sender As Object, e As MouseEventArgs) Handles Label7.MouseDown
        mouseDownLabel(sender, e)
    End Sub

    Private Sub Label13_MouseDown(sender As Object, e As MouseEventArgs) Handles Label13.MouseDown
        mouseDownLabel(sender, e)
    End Sub

    Private Sub Label12_MouseDown(sender As Object, e As MouseEventArgs) Handles Label12.MouseDown
        mouseDownLabel(sender, e)
    End Sub

    Private Sub Label11_MouseDown(sender As Object, e As MouseEventArgs) Handles Label11.MouseDown
        mouseDownLabel(sender, e)
    End Sub

    Private Sub Label16_MouseDown(sender As Object, e As MouseEventArgs) Handles Label16.MouseDown
        mouseDownLabel(sender, e)
    End Sub

    Private Sub Label15_MouseDown(sender As Object, e As MouseEventArgs) Handles Label15.MouseDown
        mouseDownLabel(sender, e)
    End Sub

    Private Sub Label14_MouseDown(sender As Object, e As MouseEventArgs) Handles Label14.MouseDown
        mouseDownLabel(sender, e)
    End Sub

    Private Sub Label19_MouseDown(sender As Object, e As MouseEventArgs) Handles Label19.MouseDown
        mouseDownLabel(sender, e)
    End Sub

    Private Sub Label18_MouseDown(sender As Object, e As MouseEventArgs) Handles Label18.MouseDown
        mouseDownLabel(sender, e)
    End Sub

    Private Sub Label17_MouseDown(sender As Object, e As MouseEventArgs) Handles Label17.MouseDown
        mouseDownLabel(sender, e)
    End Sub

    Private Sub p1100_MouseDown(sender As Object, e As MouseEventArgs) Handles p1100.MouseDown
        mouseDownPanel(sender, e)
    End Sub

    Private Sub p1400_MouseDown(sender As Object, e As MouseEventArgs) Handles p1400.MouseDown
        mouseDownPanel(sender, e)
    End Sub

    Private Sub p1700_MouseDown(sender As Object, e As MouseEventArgs) Handles p1700.MouseDown
        mouseDownPanel(sender, e)
    End Sub

    Private Sub p2100_MouseDown(sender As Object, e As MouseEventArgs) Handles p2100.MouseDown
        mouseDownPanel(sender, e)
    End Sub

    Private Sub p2400_MouseDown(sender As Object, e As MouseEventArgs) Handles p2400.MouseDown
        mouseDownPanel(sender, e)
    End Sub

    Private Sub p2700_MouseDown(sender As Object, e As MouseEventArgs) Handles p2700.MouseDown
        mouseDownPanel(sender, e)
    End Sub

    Private Sub p3100_MouseDown(sender As Object, e As MouseEventArgs) Handles p3100.MouseDown
        mouseDownPanel(sender, e)
    End Sub

    Private Sub p3400_MouseDown(sender As Object, e As MouseEventArgs) Handles p3400.MouseDown
        mouseDownPanel(sender, e)
    End Sub

    Private Sub p3700_MouseDown(sender As Object, e As MouseEventArgs) Handles p3700.MouseDown
        mouseDownPanel(sender, e)
    End Sub

    Private Sub p4100_MouseDown(sender As Object, e As MouseEventArgs) Handles p4100.MouseDown
        mouseDownPanel(sender, e)
    End Sub

    Private Sub p4400_MouseDown(sender As Object, e As MouseEventArgs) Handles p4400.MouseDown
        mouseDownPanel(sender, e)
    End Sub

    Private Sub p4700_MouseDown(sender As Object, e As MouseEventArgs) Handles p4700.MouseDown
        mouseDownPanel(sender, e)
    End Sub

    Private Sub p5100_MouseDown(sender As Object, e As MouseEventArgs) Handles p5100.MouseDown
        mouseDownPanel(sender, e)
    End Sub

    Private Sub p5400_MouseDown(sender As Object, e As MouseEventArgs) Handles p5400.MouseDown
        mouseDownPanel(sender, e)
    End Sub

    Private Sub p5700_MouseDown(sender As Object, e As MouseEventArgs) Handles p5700.MouseDown
        mouseDownPanel(sender, e)
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        showLine(panelSelected.Name)
    End Sub

    Private Sub showLine(panelname As String)
        Try
            Select Case panelname
                Case "p1100"
                    p1100.Visible = False
                Case "p1400"
                    p1400.Visible = False
                Case "p1700"
                    p1700.Visible = False
                Case "p2100"
                    p2100.Visible = False
                Case "p2400"
                    p2400.Visible = False
                Case "p2700"
                    p2700.Visible = False
                Case "p3100"
                    p3100.Visible = False
                Case "p3400"
                    p3400.Visible = False
                Case "p3700"
                    p3700.Visible = False
                Case "p4100"
                    p4100.Visible = False
                Case "p4400"
                    p4400.Visible = False
                Case "p4700"
                    p4700.Visible = False
                Case "p5100"
                    p5100.Visible = False
                Case "p5400"
                    p5400.Visible = False
                Case "p5700"
                    p5700.Visible = False

            End Select
        Catch ex As Exception

        End Try
    End Sub

    Dim tablaCon2 As New DataTable
    Private Sub cerrarFrmWarning(proceso As String)
        tablaCon2 = EjecutaSelects("select top 2 id FROM [db_kyungshin].[dbo].[t_bma_downtime] d 
                                    WHERE  Convert(Date, insert_date) = '" & Date.Now.ToShortDateString() & "' and process='" & proceso & "' 
                                      order by id desc", "buscaultimoROW")

        Dim get2ndID As String = CType(ultimoRow2.Rows(1)("id"), String)
        Call Ejecuta("UPDATE t_bma_downtime SET step=1,alarmeddepto='PRODUCTION' WHERE id='" & get2ndID & "'", "updStep1SecondID")

    End Sub

    Private Sub screenCapture()

        ' 1. Obtener el identificador de la ventana
        Dim hWnd As IntPtr = Me.Handle

        ' *** 2. Obtener el tamaño de la ventana (incluyendo los controles dinámicos) ***
        Dim rect As New Rectangle(0, 0, Me.Width, Me.Height)

        Dim hdc As IntPtr = GetDC(hWnd)

        Using captura As New Bitmap(rect.Width, rect.Height)
            ' *** Obtener el objeto Graphics del bitmap ***
            Using g As Graphics = Graphics.FromImage(captura)

                ' 4. Obtener el contexto de dispositivo del bitmap
                Dim hdcCaptura As IntPtr = g.GetHdc()

                ' 5. Llamar a la función PrintWindow
                PrintWindow(hWnd, hdcCaptura, 0)

                ' 6. Liberar los contextos de dispositivo
                ReleaseDC(hWnd, hdc)
                g.ReleaseHdc() ' Liberar el contexto del bitmap

                ' *** Guardar la captura en la base de datos ***
                GuardarImagenEnBaseDeDatos(captura)

            End Using ' El bloque Using libera el objeto Graphics automáticamente
        End Using ' El bloque Using libera el objeto Bitmap automáticamente


    End Sub

    Private Sub GuardarImagenEnBaseDeDatos(imagen As Bitmap)


        ' 1. Declarar la variable imagenBytes fuera del bloque Using
        Dim imagenBytes As Byte() = Nothing ' Inicializar a Nothing

        ' 2. Convertir la imagen a un array de bytes
        Using ms As New MemoryStream()
            imagen.Save(ms, Imaging.ImageFormat.Png)
            ' 3. Asignar el array de bytes a la variable declarada
            imagenBytes = ms.ToArray()
        End Using

        ' 3. Crear el comando SQL
        Using comando As New SqlCommand("INSERT INTO t_bma_downtime_sc (imagen, fecha) VALUES (@imagen, @fecha)", conexion)
            comando.Parameters.AddWithValue("@imagen", imagenBytes)
            comando.Parameters.AddWithValue("@fecha", ConvierteAdateMySQL(Date.Now.ToShortDateString))


            comando.ExecuteNonQuery()
        End Using


    End Sub

    Private Sub enviarWhatsappLineasActivas()
        Try
            'whatsapp API
            'M3
            'versionSistema = "PRODUCTION"
            If versionSistema = "PRODUCTION" Then

                'M2========================================================================================
                If l2100.Tipo = UCStatus.UCLed.Type.Panic Then
                    If w2100Panic = False Then
                        sendWhatsapp("M2 (100)", 2)
                        w2100Panic = True
                        w2100Online = True
                    End If

                ElseIf l2100.Tipo = UCStatus.UCLed.Type.Help Then
                    If w2100Help = False Then
                        sendWhatsapp("M2 (100)", 1)
                        w2100Help = True
                        w2100Online = True
                    End If

                ElseIf l2100.Tipo = UCStatus.UCLed.Type.Online Then
                    If w2100Online = True Then
                        w2100Panic = False
                        w2100Help = False
                        sendWhatsapp("M2 (100)", 0)
                        w2100Online = False
                    End If

                End If

                If l2400.Tipo = UCStatus.UCLed.Type.Panic Then
                    If w2400Panic = False Then
                        sendWhatsapp("M2 (400)", 2)
                        w2400Panic = True
                        w2400Online = True
                    End If

                ElseIf l2400.Tipo = UCStatus.UCLed.Type.Help Then
                    If w2400Help = False Then
                        sendWhatsapp("M2 (400)", 1)
                        w2400Help = True
                        w2400Online = True
                    End If

                ElseIf l2400.Tipo = UCStatus.UCLed.Type.Online Then
                    If w2400Online = True Then
                        w2400Panic = False
                        w2400Help = False
                        sendWhatsapp("M2 (400)", 0)
                        w2400Online = False
                    End If
                End If


                If l2700.Tipo = UCStatus.UCLed.Type.Panic Then
                    If w2700Panic = False Then
                        sendWhatsapp("M2 (700)", 2)
                        w2700Panic = True
                        w2700Online = True
                    End If

                ElseIf l2700.Tipo = UCStatus.UCLed.Type.Help Then
                    If w2700Help = False Then
                        sendWhatsapp("M2 (700)", 1)
                        w2700Help = True
                        w2700Online = True
                    End If

                ElseIf l2700.Tipo = UCStatus.UCLed.Type.Online Then
                    If w2700Online = True Then
                        w2700Panic = False
                        w2700Help = False
                        sendWhatsapp("M2 (700)", 0)
                        w2700Online = False
                    End If
                End If




                'M3========================================================================================
                If l3100.Tipo = UCStatus.UCLed.Type.Panic Then
                    If w3100Panic = False Then
                        sendWhatsapp("M3 (100)", 2)
                        w3100Panic = True
                        w3100Online = True
                    End If

                ElseIf l3100.Tipo = UCStatus.UCLed.Type.Help Then
                    If w3100Help = False Then
                        sendWhatsapp("M3 (100)", 1)
                        w3100Help = True
                        w3100Online = True
                    End If

                ElseIf l3100.Tipo = UCStatus.UCLed.Type.Online Then
                    If w3100Online = True Then
                        w3100Panic = False
                        w3100Help = False
                        sendWhatsapp("M3 (100)", 0)
                        w3100Online = False
                    End If

                End If


                If l3400.Tipo = UCStatus.UCLed.Type.Panic Then
                    If w3400Panic = False Then
                        sendWhatsapp("M3 (400)", 2)
                        w3400Panic = True
                        w3400Online = True
                    End If

                ElseIf l3400.Tipo = UCStatus.UCLed.Type.Help Then
                    If w3400Help = False Then
                        sendWhatsapp("M3 (400)", 1)
                        w3400Help = True
                        w3400Online = True
                    End If

                ElseIf l3400.Tipo = UCStatus.UCLed.Type.Online Then
                    If w3400Online = True Then
                        w3400Panic = False
                        w3400Help = False
                        sendWhatsapp("M3 (400)", 0)
                        w3400Online = False
                    End If
                End If


                If l3700.Tipo = UCStatus.UCLed.Type.Panic Then
                    If w3700Panic = False Then
                        sendWhatsapp("M3 (700)", 2)
                        w3700Panic = True
                        w3700Online = True
                    End If

                ElseIf l3700.Tipo = UCStatus.UCLed.Type.Help Then
                    If w3700Help = False Then
                        sendWhatsapp("M3 (700)", 1)
                        w3700Help = True
                        w3700Online = True
                    End If

                ElseIf l3700.Tipo = UCStatus.UCLed.Type.Online Then
                    If w3700Online = True Then
                        w3700Panic = False
                        w3700Help = False
                        sendWhatsapp("M3 (700)", 0)
                        w3700Online = False
                    End If
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub tGeneral_Tick(sender As Object, e As EventArgs) Handles tGeneral.Tick
        tGeneral.Stop()
        lbCounter.Text = CInt(lbCounter.Text) - 1
        If lbCounter.Text = "0" Then
            intialLoad()
            lbCounter.Text = "200"
        End If

        enviarWhatsappLineasActivas()

        If versionSistema = "PRODUCTION" Then
            If m100.Tipo = UCStatus.UCLed.Type.Help Or m100.Tipo = UCStatus.UCLed.Type.Panic Then
                warningQuery = "EXEC spQuery1 @numProceso = '1100', @fecha = '" & ConvierteAdateMySQL(Date.Now.ToShortDateString) & "';"
                'warningQuery = "SELECT top 1 id,step
                '                FROM [db_kyungshin].[dbo].[t_bma_downtime]
                '                where process='1100' 
                '                and CONVERT(DATE	,insert_date)='" & ConvierteAdateMySQL(Date.Now.ToShortDateString) & "'
                '                          order by id desc"
                'primer paso, mostrar la ventana warning para seleccionar departamento
                If warning1100 = False And QueryRow(warningQuery, "step", "getLastRowStep") = "0" Then
                    warning1100 = True
                    idToSend = QueryRow(warningQuery, "id", "getLastID")
                    showWarning("1100", idToSend)
                Else 'step > 0 (1,2,3) segundo paso, cambiar de color los leds y las barras

                End If
            Else
                ' cerrarFrmWarning("1100")
            End If


            If l1400.Tipo = UCStatus.UCLed.Type.Help Or l1400.Tipo = UCStatus.UCLed.Type.Panic Then
                warningQuery = "SELECT top 1 id,step
                            FROM [db_kyungshin].[dbo].[t_bma_downtime]
                            where process='1400' 
                            and CONVERT(DATE	,insert_date)='" & ConvierteAdateMySQL(Date.Now.ToShortDateString) & "'
                                      order by id desc"
                'primer paso, mostrar la ventana warning para seleccionar departamento
                If warning1400 = False And QueryRow(warningQuery, "step", "getLastRowStep") = "0" Then
                    warning1400 = True
                    idToSend = QueryRow(warningQuery, "id", "getLastID")
                    showWarning("1400", idToSend)
                Else 'step > 0 (1,2,3) segundo paso, cambiar de color los leds y las barras

                End If
            Else
                ' CerrarFormularioHijo("1400")
            End If

            If l1700.Tipo = UCStatus.UCLed.Type.Help Or l1700.Tipo = UCStatus.UCLed.Type.Panic Then
                warningQuery = "SELECT top 1 id,step
                            FROM [db_kyungshin].[dbo].[t_bma_downtime]
                            where process='1700' 
                            and CONVERT(DATE	,insert_date)='" & ConvierteAdateMySQL(Date.Now.ToShortDateString) & "'
                                      order by id desc"
                'primer paso, mostrar la ventana warning para seleccionar departamento
                If warning1700 = False And QueryRow(warningQuery, "step", "getLastRowStep") = "0" Then
                    warning1700 = True
                    idToSend = QueryRow(warningQuery, "id", "getLastID")
                    showWarning("1700", idToSend)
                Else 'step > 0 (1,2,3) segundo paso, cambiar de color los leds y las barras

                End If
            Else
                '  CerrarFormularioHijo("1700")
            End If

            If l2100.Tipo = UCStatus.UCLed.Type.Help Or l2100.Tipo = UCStatus.UCLed.Type.Panic Then
                warningQuery = "SELECT top 1 id,step
                            FROM [db_kyungshin].[dbo].[t_bma_downtime]
                            where process='2100' 
                            and CONVERT(DATE	,insert_date)='" & ConvierteAdateMySQL(Date.Now.ToShortDateString) & "'
                                      order by id desc"
                'primer paso, mostrar la ventana warning para seleccionar departamento
                If warning2100 = False And QueryRow(warningQuery, "step", "getLastRowStep") = "0" Then
                    warning2100 = True
                    idToSend = QueryRow(warningQuery, "id", "getLastID")
                    showWarning("2100", idToSend)
                Else 'step > 0 (1,2,3) segundo paso, cambiar de color los leds y las barras

                End If
            Else
                ' CerrarFormularioHijo("2100")
            End If

            If l2400.Tipo = UCStatus.UCLed.Type.Help Or l2400.Tipo = UCStatus.UCLed.Type.Panic Then
                warningQuery = "SELECT top 1 id,step
                            FROM [db_kyungshin].[dbo].[t_bma_downtime]
                            where process='2400' 
                            and CONVERT(DATE	,insert_date)='" & ConvierteAdateMySQL(Date.Now.ToShortDateString) & "'
                                      order by id desc"
                'primer paso, mostrar la ventana warning para seleccionar departamento
                If warning2400 = False And QueryRow(warningQuery, "step", "getLastRowStep") = "0" Then
                    warning2400 = True
                    idToSend = QueryRow(warningQuery, "id", "getLastID")
                    showWarning("2400", idToSend)
                Else 'step > 0 (1,2,3) segundo paso, cambiar de color los leds y las barras

                End If
            End If

            If l2700.Tipo = UCStatus.UCLed.Type.Help Or l2700.Tipo = UCStatus.UCLed.Type.Panic Then
                warningQuery = "SELECT top 1 id,step
                            FROM [db_kyungshin].[dbo].[t_bma_downtime]
                            where process='2700' 
                            and CONVERT(DATE	,insert_date)='" & ConvierteAdateMySQL(Date.Now.ToShortDateString) & "'
                                      order by id desc"
                'primer paso, mostrar la ventana warning para seleccionar departamento
                If warning2700 = False And QueryRow(warningQuery, "step", "getLastRowStep") = "0" Then
                    warning2700 = True
                    idToSend = QueryRow(warningQuery, "id", "getLastID")
                    showWarning("2700", idToSend)
                Else 'step > 0 (1,2,3) segundo paso, cambiar de color los leds y las barras

                End If
            End If

            If l3100.Tipo = UCStatus.UCLed.Type.Help Or l3100.Tipo = UCStatus.UCLed.Type.Panic Then
                warningQuery = "SELECT top 1 id,step
                            FROM [db_kyungshin].[dbo].[t_bma_downtime]
                            where process='3100' 
                            and CONVERT(DATE	,insert_date)='" & ConvierteAdateMySQL(Date.Now.ToShortDateString) & "'
                                      order by id desc"
                'primer paso, mostrar la ventana warning para seleccionar departamento
                If warning3100 = False And QueryRow(warningQuery, "step", "getLastRowStep") = "0" Then
                    warning3100 = True
                    idToSend = QueryRow(warningQuery, "id", "getLastID")
                    showWarning("3100", idToSend)
                Else 'step > 0 (1,2,3) segundo paso, cambiar de color los leds y las barras

                End If
            End If

            If l3400.Tipo = UCStatus.UCLed.Type.Help Or l3400.Tipo = UCStatus.UCLed.Type.Panic Then
                warningQuery = "SELECT top 1 id,step
                            FROM [db_kyungshin].[dbo].[t_bma_downtime]
                            where process='3400' 
                            and CONVERT(DATE	,insert_date)='" & ConvierteAdateMySQL(Date.Now.ToShortDateString) & "'
                                      order by id desc"
                'primer paso, mostrar la ventana warning para seleccionar departamento
                If warning3400 = False And QueryRow(warningQuery, "step", "getLastRowStep") = "0" Then
                    warning3400 = True
                    idToSend = QueryRow(warningQuery, "id", "getLastID")
                    showWarning("3400", idToSend)
                Else 'step > 0 (1,2,3) segundo paso, cambiar de color los leds y las barras

                End If
            End If

            If l3700.Tipo = UCStatus.UCLed.Type.Help Or l3700.Tipo = UCStatus.UCLed.Type.Panic Then
                warningQuery = "SELECT top 1 id,step
                            FROM [db_kyungshin].[dbo].[t_bma_downtime]
                            where process='3700' 
                            and CONVERT(DATE	,insert_date)='" & ConvierteAdateMySQL(Date.Now.ToShortDateString) & "'
                                      order by id desc"
                'primer paso, mostrar la ventana warning para seleccionar departamento
                If warning3700 = False And QueryRow(warningQuery, "step", "getLastRowStep") = "0" Then
                    warning3700 = True
                    idToSend = QueryRow(warningQuery, "id", "getLastID")
                    showWarning("3700", idToSend)
                Else 'step > 0 (1,2,3) segundo paso, cambiar de color los leds y las barras

                End If
            End If

            '4100

            '4400
            If l4400.Tipo = UCStatus.UCLed.Type.Help Or l4400.Tipo = UCStatus.UCLed.Type.Panic Then
                warningQuery = "SELECT top 1 id,step
                            FROM [db_kyungshin].[dbo].[t_bma_downtime]
                            where process='4400' 
                            and CONVERT(DATE	,insert_date)='" & ConvierteAdateMySQL(Date.Now.ToShortDateString) & "'
                                      order by id desc"
                'primer paso, mostrar la ventana warning para seleccionar departamento
                If warning4400 = False And QueryRow(warningQuery, "step", "getLastRowStep") = "0" Then
                    warning4400 = True
                    idToSend = QueryRow(warningQuery, "id", "getLastID")
                    showWarning("4400", idToSend)
                Else 'step > 0 (1,2,3) segundo paso, cambiar de color los leds y las barras

                End If
            End If
        End If

        ' Obtener la hora actual
        Dim horaActual As DateTime = DateTime.Now


        ' Especificar la hora para la captura de pantalla (16:30)
        Dim horaCaptura As DateTime = New DateTime(horaActual.Year, horaActual.Month, horaActual.Day, 17, 27, 0)
        ' Mostrar las horas para verificar
        Console.WriteLine("Hora actual: " & horaActual.ToString("HH:mm:ss"))
        Console.WriteLine("Hora captura: " & horaCaptura.ToString("HH:mm:ss"))
        ' Comparar la hora actual con la hora de captura (rango de 1 segundo)
        Dim horaActualRedondeada As DateTime = New DateTime(horaActual.Year, horaActual.Month, horaActual.Day, horaActual.Hour, horaActual.Minute, 0)

        ' 5. Verificar si existe un registro para la fecha actual en la base de datos

        Try
            Dim query As String = "SELECT 1 FROM [db_kyungshin].[dbo].[t_bma_downtime_sc] WHERE CONVERT(DATE, fecha) = @fecha"
            Using command As New SqlCommand(query, conexion)
                command.Parameters.AddWithValue("@fecha", horaActual.Date)

                Dim resultado As Object = command.ExecuteScalar()

                ' 6. Si NO existe un registro y la hora coincide, realizar la captura de pantalla
                If resultado Is Nothing AndAlso horaActualRedondeada.TimeOfDay = horaCaptura.TimeOfDay AndAlso Not capturaRealizada Then
                    If capturarPantalla = 1 Then
                        screenCapture()
                        capturaRealizada = True
                    End If
                End If
            End Using
        Catch ex As Exception
            Console.WriteLine("Error al verificar la fecha en la base de datos: " & ex.Message)
        End Try



        ' 7. Incrementar el contador y reiniciar si es necesario
        If capturaRealizada Then
            contadorSegundos += 1
            If contadorSegundos >= 60 Then
                capturaRealizada = False
                contadorSegundos = 0 ' Reiniciar el contador
            End If
        End If


        'If horaActualRedondeada.TimeOfDay = horaCaptura.TimeOfDay AndAlso Not capturaRealizada Then
        '    If capturarPantalla = 1 Then
        '        screenCapture()
        '        capturaRealizada = True
        '    End If

        'End If

        '' Incrementar el contador
        'If capturaRealizada Then
        '    contadorSegundos += 1
        '    If contadorSegundos >= 60 Then
        '        capturaRealizada = False
        '        contadorSegundos = 0 ' Reiniciar el contador
        '    End If
        'End If


        tGeneral.Start()
    End Sub

    Private capturaRealizada As Boolean = False
    Private Sub OpenDetails(line As String)
        Try
            Dim frm As New frmDetails(line)
            frm.ShowDialog()

            If frm.iResult = True Then
                Panel11.Visible = True
                'intialLoad()
                'lbCounter.Text = "100"
                Panel11.Visible = False
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sendWhatsapp(line As String, type As String)
        Try
            ' Obtener la hora actual
            Dim horaActual As DateTime = DateTime.Now
            ' Especificar la hora para la captura de pantalla (16:30)
            Dim horaWhatsappTurno1 As DateTime = New DateTime(horaActual.Year, horaActual.Month, horaActual.Day, 16, 45, 0) 'fin del turno 1

            Dim inicioTurno1 As DateTime = New DateTime(horaActual.Year, horaActual.Month, horaActual.Day, 7, 0, 0) 'fin del turno 1


            Dim horaActualRedondeada As DateTime = New DateTime(horaActual.Year, horaActual.Month, horaActual.Day, horaActual.Hour, horaActual.Minute, 0)

            If horaActualRedondeada.TimeOfDay >= inicioTurno1.TimeOfDay And horaActualRedondeada.TimeOfDay <= horaWhatsappTurno1.TimeOfDay Then

                Dim WebRequest As HttpWebRequest
                '  WebRequest = HttpWebRequest.Create("https://api.ultramsg.com/instance81482/messages/chat")
                WebRequest = HttpWebRequest.Create("https://api.ultramsg.com/instance107253/messages/chat")
                Dim postdata As String = ""
                Select Case type
                    Case 0
                        postdata = "token=qm1998bshgxcr8dd&to=" & productionNumbers & "&body=DOWNTIME MONITORING SYSTEM: " & line & " - RUNNING TIME STARTED 🟢 "
                    Case 1
                        postdata = "token=qm1998bshgxcr8dd&to=" & productionNumbers & "&body=DOWNTIME MONITORING SYSTEM: " & line & " - MORE THAN 5 MINUTES STOPPED 🟡 "
                    Case 2
                        postdata = "token=qm1998bshgxcr8dd&to=" & productionNumbers & "," & managersNumbers & "&body=DOWNTIME MONITORING SYSTEM: " & line & " - MORE THAN 15 MINUTES STOPPED 🔴 "
                End Select
                'Dim postdata As String = "token=ihs8oh4i2dpn83iq&to=+528711626091&body= DOWNTIME MONITORING SYSTEM: " & line & " - YELLOW FLAG STARTED (5 MINUTES STOPPED) "
                Dim enc As UTF8Encoding = New System.Text.UTF8Encoding()
                Dim postdatabytes As Byte() = enc.GetBytes(postdata)
                WebRequest.Method = "POST"
                WebRequest.ContentType = "application/x-www-form-urlencoded"
                ' WebRequest.GetRequestStream().Write(postdatabytes)
                WebRequest.GetRequestStream().Write(postdatabytes, 0, postdatabytes.Length)
                Dim ret As New System.IO.StreamReader(WebRequest.GetResponse().GetResponseStream())
                'MsgBox(ret.ReadToEnd())

            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Label30_Click(sender As Object, e As EventArgs) Handles Label30.Click
        OpenDetails("M1")
    End Sub

    Private Sub pm1_Click(sender As Object, e As EventArgs) Handles pm1.Click
        OpenDetails("M1")
    End Sub

    Private Sub Panel4_Click(sender As Object, e As EventArgs) Handles Panel4.Click
        OpenDetails("M2")
    End Sub

    Private Sub frmMonitor_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.F1
                    copyDGV2To1()
                Case Keys.F2
                    copyDGV()
                Case Keys.F3
                    copyDGV2()
            End Select
        Catch ex As Exception

        End Try

    End Sub

    Private Sub copyDGV()
        Try
            dgv2700.Rows.Add(dgv3700.Rows(0).Cells("titulo3700").Value,
                              dgv3700.Rows(0).Cells("t0").Value,
                             dgv3700.Rows(0).Cells("t1").Value,
                             dgv3700.Rows(0).Cells("t2").Value,
                             dgv3700.Rows(0).Cells("t3").Value,
                             dgv3700.Rows(0).Cells("t4").Value,
                             dgv3700.Rows(0).Cells("t5").Value,
                             dgv3700.Rows(0).Cells("t6").Value,
                             dgv3700.Rows(0).Cells("t7").Value,
                             dgv3700.Rows(0).Cells("t8").Value,
                             dgv3700.Rows(0).Cells("t9").Value,
                             dgv3700.Rows(0).Cells("t10").Value,
                             dgv3700.Rows(0).Cells("t11").Value,
                             dgv3700.Rows(0).Cells("t12").Value,
                             dgv3700.Rows(0).Cells("t13").Value,
                             dgv3700.Rows(0).Cells("t14").Value,
                             dgv3700.Rows(0).Cells("t15").Value,
                             dgv3700.Rows(0).Cells("t16").Value,
                             dgv3700.Rows(0).Cells("t17").Value,
                             dgv3700.Rows(0).Cells("t18").Value,
                             dgv3700.Rows(0).Cells("t19").Value,
                             dgv3700.Rows(0).Cells("t20").Value,
                             dgv3700.Rows(0).Cells("t21").Value,
                             dgv3700.Rows(0).Cells("t22").Value,
                             dgv3700.Rows(0).Cells("t23").Value
)
            dgv3700.Rows.Clear()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub copyDGV2To1()
        Try
            dgv1700.Rows.Add(dgv1700.Rows(0).Cells("titulo1700").Value,
                              dgv1700.Rows(0).Cells("t0").Value,
                             dgv1700.Rows(0).Cells("t1").Value,
                             dgv1700.Rows(0).Cells("t2").Value,
                             dgv1700.Rows(0).Cells("t3").Value,
                             dgv1700.Rows(0).Cells("t4").Value,
                             dgv1700.Rows(0).Cells("t5").Value,
                             dgv1700.Rows(0).Cells("t6").Value,
                             dgv1700.Rows(0).Cells("t7").Value,
                             dgv1700.Rows(0).Cells("t8").Value,
                             dgv1700.Rows(0).Cells("t9").Value,
                             dgv1700.Rows(0).Cells("t10").Value,
                             dgv1700.Rows(0).Cells("t11").Value,
                             dgv1700.Rows(0).Cells("t12").Value,
                             dgv1700.Rows(0).Cells("t13").Value,
                             dgv1700.Rows(0).Cells("t14").Value,
                             dgv1700.Rows(0).Cells("t15").Value,
                             dgv1700.Rows(0).Cells("t16").Value,
                             dgv1700.Rows(0).Cells("t17").Value,
                             dgv1700.Rows(0).Cells("t18").Value,
                             dgv1700.Rows(0).Cells("t19").Value,
                             dgv1700.Rows(0).Cells("t20").Value,
                             dgv1700.Rows(0).Cells("t21").Value,
                             dgv1700.Rows(0).Cells("t22").Value,
                             dgv1700.Rows(0).Cells("t23").Value
)
            dgv2700.Rows.Clear()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub copyDGV2()
        Try
            dgv3700.Rows.Add(dgv2700.Rows(0).Cells("titulo2").Value,
                              dgv2700.Rows(0).Cells("d0").Value,
                             dgv2700.Rows(0).Cells("d1").Value,
                             dgv2700.Rows(0).Cells("d2").Value,
                             dgv2700.Rows(0).Cells("d3").Value,
                             dgv2700.Rows(0).Cells("d4").Value,
                             dgv2700.Rows(0).Cells("d5").Value,
                             dgv2700.Rows(0).Cells("d6").Value,
                             dgv2700.Rows(0).Cells("d7").Value,
                             dgv2700.Rows(0).Cells("d8").Value,
                             dgv2700.Rows(0).Cells("d9").Value,
                             dgv2700.Rows(0).Cells("d10").Value,
                             dgv2700.Rows(0).Cells("d11").Value,
                             dgv2700.Rows(0).Cells("d12").Value,
                             dgv2700.Rows(0).Cells("d13").Value,
                             dgv2700.Rows(0).Cells("d14").Value,
                             dgv2700.Rows(0).Cells("d15").Value,
                             dgv2700.Rows(0).Cells("d16").Value,
                             dgv2700.Rows(0).Cells("d17").Value,
                             dgv2700.Rows(0).Cells("d18").Value,
                             dgv2700.Rows(0).Cells("d19").Value,
                             dgv2700.Rows(0).Cells("d20").Value,
                             dgv2700.Rows(0).Cells("d21").Value,
                             dgv2700.Rows(0).Cells("d22").Value,
                             dgv2700.Rows(0).Cells("d23").Value
)
            dgv2700.Rows.Clear()
        Catch ex As Exception

        End Try
    End Sub

    Friend IncrementoX As Integer = 0
    Friend IncrementoY As Integer = 0

    Private WithEvents _formularioHijo As frmInputDepto

    Private Sub AbrirFormularioHijo()
        _formularioHijo = New frmInputDepto()
        _formularioHijo.Owner = Me
        _formularioHijo.Show()
    End Sub



    Private Sub CerrarFormularioHijo(line As String)
        If _formularioHijo IsNot Nothing AndAlso Not _formularioHijo.IsDisposed Then
            _formularioHijo.Close()
            Select Case line
                Case "1100" : warning1100 = False
                Case "1400" : warning1400 = False
                Case "1700" : warning1700 = False
                Case "2100" : warning2100 = False
                Case "2400" : warning2400 = False
                Case "2700" : warning2700 = False
                Case "3100" : warning3100 = False
                Case "3400" : warning3400 = False
                Case "3700" : warning3700 = False
            End Select

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'm100.Tipo = UCStatus.UCLed.Type.Quality
        'showWarning("1100")
        'showWarning("2100")
        'CerrarFormularioHijo("1100")
        '_formularioHijo = New frmInputDepto("M4", "1")
        '_formularioHijo.StartPosition = FormStartPosition.Manual
        '_formularioHijo.Owner = Me
        '_formularioHijo.Location = New Point(Me.Location.X + 450, Me.Location.Y + 660)

        Dim frm As New frmInputDepto("M4", "1")
        frm.StartPosition = FormStartPosition.Manual
        frm.Owner = Me
        frm.Location = New Point(Me.Location.X + 450, Me.Location.Y + 660)
        frm.Show()
    End Sub

    Private Sub showWarning(line As String, id As String)

        _formularioHijo = New frmInputDepto(line, id)
        _formularioHijo.StartPosition = FormStartPosition.Manual
        _formularioHijo.Owner = Me

        If line = "1100" Or line = "1400" Or line = "1700" Then
            _formularioHijo.Location = New Point(Me.Location.X + 450, Me.Location.Y + 94)
        ElseIf line = "2100" Or line = "2400" Or line = "2700" Then
            _formularioHijo.Location = New Point(Me.Location.X + 450, Me.Location.Y + 281)
        ElseIf line = "3100" Or line = "3400" Or line = "3700" Then
            _formularioHijo.Location = New Point(Me.Location.X + 450, Me.Location.Y + 471)
        ElseIf line = "4100" Or line = "4400" Or line = "4700" Then
            _formularioHijo.Location = New Point(Me.Location.X + 450, Me.Location.Y + 660)
        End If

        _formularioHijo.Show()
    End Sub

    Private Sub rbAll_CheckedChanged(sender As Object, e As EventArgs) Handles rbAll.CheckedChanged
        intialLoad()
        lbCounter.Text = "200"
    End Sub

    Private Sub rbShift1_CheckedChanged(sender As Object, e As EventArgs) Handles rbShift1.CheckedChanged
        intialLoad()
        lbCounter.Text = "200"
    End Sub


    'Private Sub menuDGV_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles menuDGV.Opening
    '    Try
    '        Select Case selectedDGV.Name
    '            Case "dgv1700"
    '                menuDGV.Items.Add("Move to M2")

    '        End Select
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub Label31_Click(sender As Object, e As EventArgs) Handles Label31.Click
        OpenDetails("M2")
    End Sub

    Private Sub Panel6_Click(sender As Object, e As EventArgs) Handles Panel6.Click
        OpenDetails("M3")
    End Sub

    Private Sub Label32_Click(sender As Object, e As EventArgs) Handles Label32.Click
        OpenDetails("M3")
    End Sub

    Private Sub Panel8_Click(sender As Object, e As EventArgs) Handles Panel8.Click
        OpenDetails("M4")
    End Sub

    Private Sub Label33_Click(sender As Object, e As EventArgs) Handles Label33.Click
        OpenDetails("M4")
    End Sub

    Private Sub Label34_Click(sender As Object, e As EventArgs) Handles Label34.Click
        OpenDetails("M5")
    End Sub

    Private Sub Panel10_Click(sender As Object, e As EventArgs) Handles Panel10.Click
        OpenDetails("M5")
    End Sub


    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles timerm2100.Tick
        Try
            timerm2100.Stop()
            loadAll(lbDT2100, lbRT2100, lbOT2100, "2100")

            ultimoRow2 = EjecutaSelects("select top 1 status,alarmedDepto FROM [db_kyungshin].[dbo].[t_bma_downtime] d 
                                    WHERE  Convert(Date, insert_date) = '" & Date.Now.ToShortDateString() & "' and process='2100' 
                                      order by id desc", "buscaultimoROW")

            lastStatus = CType(ultimoRow2.Rows(0)("status"), String)

            'queryLeds = "select top 1 status FROM [db_kyungshin].[dbo].[t_bma_downtime] d 
            'WHERE  CONVERT(date, insert_date) = '" & Date.Now.ToShortDateString() & "' and process='2100' 
            '  order by id desc"

            'lastStatus = QueryRow(queryLeds, "status", "statusEnd")

            If lastStatus = "DOWN" Then
                l2100.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_OFF
                If first2100 = True Then
                    first2100 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus21 Then
                    l2100.hasChanged = True
                Else
                    l2100.hasChanged = False
                End If
                l2100.depto = CType(ultimoRow2.Rows(0)("alarmedDepto"), String)
            Else
                l2100.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_RUN
                If first2100 = True Then
                    first2100 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus21 Then
                    l2100.hasChanged = True
                Else
                    l2100.hasChanged = False
                End If

            End If
primeraVez:

            auxStatus21 = lastStatus
            timerm2100.Start()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub timerm1700_Tick(sender As Object, e As EventArgs) Handles timerm1700.Tick
        Try
            timerm1700.Stop()
            loadAll(lbDTm1700, lbRTm1700, lbOtm1700, "1700")

            ultimoRow2 = EjecutaSelects("select top 1 status,alarmedDepto FROM [db_kyungshin].[dbo].[t_bma_downtime] d 
                                    WHERE  Convert(Date, insert_date) = '" & Date.Now.ToShortDateString() & "' and process='1700' 
                                      order by id desc", "buscaultimoROW")

            lastStatus = CType(ultimoRow2.Rows(0)("status"), String)

            If lastStatus = "DOWN" Then
                l1700.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_OFF
                If first1700 = True Then
                    first1700 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus17 Then
                    l1700.hasChanged = True
                Else
                    l1700.hasChanged = False
                End If
                l1700.depto = CType(ultimoRow2.Rows(0)("alarmedDepto"), String)
            Else
                l1700.TipoLastStatus = UCStatus.UCLed.LastStatusDB.S_RUN
                If first1700 = True Then
                    first1700 = False
                    GoTo primeraVez
                End If
                If lastStatus <> auxStatus17 Then
                    l1700.hasChanged = True
                Else
                    l1700.hasChanged = False
                End If

            End If
primeraVez:

            auxStatus17 = lastStatus
            timerm1700.Start()
        Catch ex As Exception
            SaveFileAs("errores.log", ex.Message & "---- Sender: ProgresoGuardado() ------" & DateTime.Now.ToString.ToString, True)
        End Try
    End Sub
    ''' <summary>
    ''' Verificar cuales lineas estan corriendo, es un status en base de datos
    ''' </summary>
    Private Sub loadLines()
        Try
            If QueryRow("SELECT status FROM t_bma_linesSettings WHERE processLine='1100'", "status", "findStat") = 1 Then
                isRunning1100 = True
            Else
                isRunning1100 = False
            End If

            If QueryRow("SELECT status FROM t_bma_linesSettings WHERE processLine='1400'", "status", "findStat") = 1 Then
                isRunning1400 = True
            Else
                isRunning1400 = False
            End If

            If QueryRow("SELECT status FROM t_bma_linesSettings WHERE processLine='1700'", "status", "findStat") = 1 Then
                isRunning1700 = True
            Else
                isRunning1700 = False
            End If


            If QueryRow("SELECT status FROM t_bma_linesSettings WHERE processLine='2100'", "status", "findStat") = 1 Then
                isRunning2100 = True
            Else
                isRunning2100 = False
            End If

            If QueryRow("SELECT status FROM t_bma_linesSettings WHERE processLine='2400'", "status", "findStat") = 1 Then
                isRunning2400 = True
            Else
                isRunning2400 = False
            End If

            If QueryRow("SELECT status FROM t_bma_linesSettings WHERE processLine='2700'", "status", "findStat") = 1 Then
                isRunning2700 = True
            Else
                isRunning2700 = False
            End If



            If QueryRow("SELECT status FROM t_bma_linesSettings WHERE processLine='3100'", "status", "findStat") = 1 Then
                isRunning3100 = True
            Else
                isRunning3100 = False
            End If

            If QueryRow("SELECT status FROM t_bma_linesSettings WHERE processLine='3400'", "status", "findStat") = 1 Then
                isRunning3400 = True
            Else
                isRunning3400 = False
            End If

            If QueryRow("SELECT status FROM t_bma_linesSettings WHERE processLine='3700'", "status", "findStat") = 1 Then
                isRunning3700 = True
            Else
                isRunning3700 = False
            End If


            If QueryRow("SELECT status FROM t_bma_linesSettings WHERE processLine='4100'", "status", "findStat") = 1 Then
                isRunning4100 = True
            Else
                isRunning4100 = False
            End If

            If QueryRow("SELECT status FROM t_bma_linesSettings WHERE processLine='4400'", "status", "findStat") = 1 Then
                isRunning4400 = True
            Else
                isRunning4400 = False
            End If

            If QueryRow("SELECT status FROM t_bma_linesSettings WHERE processLine='4700'", "status", "findStat") = 1 Then
                isRunning4700 = True
            Else
                isRunning4700 = False
            End If


            If QueryRow("SELECT status FROM t_bma_linesSettings WHERE processLine='5100'", "status", "findStat") = 1 Then
                isRunning5100 = True
            Else
                isRunning5100 = False
            End If

            If QueryRow("SELECT status FROM t_bma_linesSettings WHERE processLine='5400'", "status", "findStat") = 1 Then
                isRunning5400 = True
            Else
                isRunning5400 = False
            End If

            If QueryRow("SELECT status FROM t_bma_linesSettings WHERE processLine='5700'", "status", "findStat") = 1 Then
                isRunning5700 = True
            Else
                isRunning5700 = False
            End If


        Catch ex As Exception

        End Try
    End Sub
    Private Sub Shift1Controls()
        If rbShift1.Checked = True Then
            onlyShowShift1 = True
            pbM1shift1.Visible = True
            pbm2shift1.Visible = True
            pbm3shift1.Visible = True
            pbm4shift1.Visible = True
            pbM5shift1.Visible = True
            segundosTotales = 39600
            horasTotales = 10
        ElseIf rbAll.Checked = True Then
            onlyShowShift1 = False
            pbM1shift1.Visible = False
            pbm2shift1.Visible = False
            pbm3shift1.Visible = False
            pbm4shift1.Visible = False
            pbM5shift1.Visible = False
            segundosTotales = 86400
            horasTotales = 24
        End If
    End Sub

    Private Sub frmMonitor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Panel11.Visible = True

            If (File.Exists("Config.xml")) Then
                Dim objXMLDoc As XmlDocument = Nothing

                Try
                    objXMLDoc = New XmlDocument
                    objXMLDoc.Load("Config.xml")

                    With ArchivoXML
                        .host = objXMLDoc.GetElementsByTagName("Host").Item(0).InnerText
                    End With

                    ' Verificar si la etiqueta <sc> existe
                    Dim scNode As XmlNode = objXMLDoc.GetElementsByTagName("sc").Item(0)
                    If scNode IsNot Nothing Then
                        ' Asignar el valor de <sc> a la variable capturarPantalla
                        capturarPantalla = Integer.Parse(scNode.InnerText)
                    Else
                        ' Si no existe, capturarPantalla = 0 (ya está asignado por defecto)
                    End If

                Catch ex As Exception
                    MessageBox.Show("Error al Leer el archivo de configuración XML, Consulte con el administrador del sistema.", "Error de Lectura", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                Finally
                    objXMLDoc = Nothing
                End Try
            Else
                MessageBox.Show("Error al Leer el archivo de configuración XML.", "Error de Lectura", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            versionSistema = ArchivoXML.host

            intialLoad()
            tGeneral.Start()
            Me.Text &= " " & versionSistema & " --Version " & My.Application.Info.Version.ToString
            posX = Me.Location.X
            posY = Me.Location.Y
            pantallaPrincipal = Screen.FromControl(Me)
            productionNumbers = QueryRow("SELECT celulares FROM t_bma_downtime_celulares WHERE depto='PRODUCTION'", "celulares", "CELS")
            managersNumbers = QueryRow("SELECT celulares FROM t_bma_downtime_celulares WHERE depto='MANAGERS'", "celulares", "CELS")
            IniciarParpadeo()
        Catch ex As Exception

        End Try
    End Sub

    Private WithEvents blinkTimer As New Timer()
    Private isOrangeBlink As Boolean = False
    Private Const orangeThreshold As Integer = 50 ' Umbral de tolerancia para el color naranja

    ' Método para iniciar el parpadeo
    Private Sub IniciarParpadeo()
        'blinkTimer.Interval = 500 ' Intervalo de parpadeo en milisegundos (0.5 segundos)
        'blinkTimer.Start()


        Dim trd As New Threading.Thread(AddressOf blinkThread) 'este se encarga de leer el PLC
        trd.Start()
    End Sub


    ' Evento de temporizador para el parpadeo
    Private Sub blinkTimer_Tick(sender As Object, e As EventArgs) Handles blinkTimer.Elapsed
        isOrangeBlink = Not isOrangeBlink
        ' Redibujar el PictureBox para actualizar el parpadeo
        PictureBox1.Refresh()
        pbm1p400.Refresh()
        pbm1p700.Refresh()
        pbm2p100.Refresh()
        pb2400.Refresh()
        pb2700.Refresh()
        pb3100.Refresh()
        pb3400.Refresh()
        pb3700.Refresh()
        pb4400.Refresh()
    End Sub

    Private Sub blinkThread()
        Do While n <> 1
            Threading.Thread.Sleep(1000)
            isOrangeBlink = Not isOrangeBlink
            ' Redibujar el PictureBox para actualizar el parpadeo
            PictureBox1.Refresh()
            pbm1p400.Refresh()
            pbm1p700.Refresh()
            pbm2p100.Refresh()
            pb2400.Refresh()
            pb2700.Refresh()
            pb3100.Refresh()
            pb3400.Refresh()
            pb3700.Refresh()
            pb4400.Refresh()
        Loop

    End Sub


#Region "pictures paint"
    Private Sub PictureBox1_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox1.Paint
        If PictureBox1.Image IsNot Nothing Then
            ' Crear un bitmap auxiliar para el dibujo
            Using bmp As New Bitmap(PictureBox1.Image)
                Using g As Graphics = Graphics.FromImage(bmp)
                    For y As Integer = 0 To bmp.Height - 1
                        For x As Integer = 0 To bmp.Width - 1
                            Dim pixelColor As Color = bmp.GetPixel(x, y)
                            ' Si el color del píxel está dentro del umbral de color naranja
                            If IsOrange(pixelColor) Or IsBlack(pixelColor) Or IsBrown(pixelColor) Or IsDarkGray(pixelColor) Or isPurple(pixelColor) Or isTurquesa(pixelColor) Or isDarkRed(pixelColor) Then
                                ' Si está en el estado de parpadeo, cambiar el color del píxel
                                If isOrangeBlink Then
                                    '  If blinkBar = True Then
                                    bmp.SetPixel(x, y, Color.Yellow) ' Cambiar el color a blanco para el parpadeo
                                    '  End If

                                End If
                            End If
                        Next
                    Next
                End Using

                ' Dibujar el bitmap en el PictureBox
                e.Graphics.DrawImage(bmp, 0, 0)
            End Using
        End If
    End Sub

    Private Sub pbm1p400_Paint(sender As Object, e As PaintEventArgs) Handles pbm1p400.Paint
        If pbm1p400.Image IsNot Nothing Then
            ' Crear un bitmap auxiliar para el dibujo
            Using bmp As New Bitmap(pbm1p400.Image)
                Using g As Graphics = Graphics.FromImage(bmp)
                    For y As Integer = 0 To bmp.Height - 1
                        For x As Integer = 0 To bmp.Width - 1
                            Dim pixelColor As Color = bmp.GetPixel(x, y)
                            ' Si el color del píxel está dentro del umbral de color naranja
                            If IsOrange(pixelColor) Or IsBlack(pixelColor) Or IsBrown(pixelColor) Or IsDarkGray(pixelColor) Or isPurple(pixelColor) Or isTurquesa(pixelColor) Or isDarkRed(pixelColor) Then
                                ' Si está en el estado de parpadeo, cambiar el color del píxel
                                If isOrangeBlink Then
                                    bmp.SetPixel(x, y, Color.Yellow) ' Cambiar el color a blanco para el parpadeo
                                End If
                            End If
                        Next
                    Next
                End Using

                ' Dibujar el bitmap en el PictureBox
                e.Graphics.DrawImage(bmp, 0, 0)
            End Using
        End If
    End Sub

    Private Sub pbm1p700_Paint(sender As Object, e As PaintEventArgs) Handles pbm1p700.Paint
        If pbm1p700.Image IsNot Nothing Then
            ' Crear un bitmap auxiliar para el dibujo
            Using bmp As New Bitmap(pbm1p700.Image)
                Using g As Graphics = Graphics.FromImage(bmp)
                    For y As Integer = 0 To bmp.Height - 1
                        For x As Integer = 0 To bmp.Width - 1
                            Dim pixelColor As Color = bmp.GetPixel(x, y)
                            ' Si el color del píxel está dentro del umbral de color naranja
                            If IsOrange(pixelColor) Or IsBlack(pixelColor) Or IsBrown(pixelColor) Or IsDarkGray(pixelColor) Or isPurple(pixelColor) Or isTurquesa(pixelColor) Or isDarkRed(pixelColor) Then
                                ' Si está en el estado de parpadeo, cambiar el color del píxel
                                If isOrangeBlink Then
                                    bmp.SetPixel(x, y, Color.Yellow) ' Cambiar el color a blanco para el parpadeo
                                End If
                            End If
                        Next
                    Next
                End Using

                ' Dibujar el bitmap en el PictureBox
                e.Graphics.DrawImage(bmp, 0, 0)
            End Using
        End If
    End Sub

    Private Sub pbm2p100_Paint(sender As Object, e As PaintEventArgs) Handles pbm2p100.Paint
        If pbm2p100.Image IsNot Nothing Then
            ' Crear un bitmap auxiliar para el dibujo
            Using bmp As New Bitmap(pbm2p100.Image)
                Using g As Graphics = Graphics.FromImage(bmp)
                    For y As Integer = 0 To bmp.Height - 1
                        For x As Integer = 0 To bmp.Width - 1
                            Dim pixelColor As Color = bmp.GetPixel(x, y)
                            ' Si el color del píxel está dentro del umbral de color naranja
                            If IsOrange(pixelColor) Or IsBlack(pixelColor) Or IsBrown(pixelColor) Or IsDarkGray(pixelColor) Or isPurple(pixelColor) Or isTurquesa(pixelColor) Or isDarkRed(pixelColor) Then
                                ' Si está en el estado de parpadeo, cambiar el color del píxel
                                If isOrangeBlink Then
                                    bmp.SetPixel(x, y, Color.Yellow) ' Cambiar el color a blanco para el parpadeo
                                End If
                            End If
                        Next
                    Next
                End Using

                ' Dibujar el bitmap en el PictureBox
                e.Graphics.DrawImage(bmp, 0, 0)
            End Using
        End If
    End Sub

    Private Sub pb2400_Paint(sender As Object, e As PaintEventArgs) Handles pb2400.Paint
        If pb2400.Image IsNot Nothing Then
            ' Crear un bitmap auxiliar para el dibujo
            Using bmp As New Bitmap(pb2400.Image)
                Using g As Graphics = Graphics.FromImage(bmp)
                    For y As Integer = 0 To bmp.Height - 1
                        For x As Integer = 0 To bmp.Width - 1
                            Dim pixelColor As Color = bmp.GetPixel(x, y)
                            ' Si el color del píxel está dentro del umbral de color naranja
                            If IsOrange(pixelColor) Or IsBlack(pixelColor) Or IsBrown(pixelColor) Or IsDarkGray(pixelColor) Or isPurple(pixelColor) Or isTurquesa(pixelColor) Or isDarkRed(pixelColor) Then
                                ' Si está en el estado de parpadeo, cambiar el color del píxel
                                If isOrangeBlink Then
                                    bmp.SetPixel(x, y, Color.Yellow) ' Cambiar el color a blanco para el parpadeo
                                End If
                            End If
                        Next
                    Next
                End Using

                ' Dibujar el bitmap en el PictureBox
                e.Graphics.DrawImage(bmp, 0, 0)
            End Using
        End If
    End Sub


    Private Sub pb2700_Paint(sender As Object, e As PaintEventArgs) Handles pb2700.Paint
        If pb2700.Image IsNot Nothing Then
            ' Crear un bitmap auxiliar para el dibujo
            Using bmp As New Bitmap(pb2700.Image)
                Using g As Graphics = Graphics.FromImage(bmp)
                    For y As Integer = 0 To bmp.Height - 1
                        For x As Integer = 0 To bmp.Width - 1
                            Dim pixelColor As Color = bmp.GetPixel(x, y)
                            ' Si el color del píxel está dentro del umbral de color naranja
                            If IsOrange(pixelColor) Or IsBlack(pixelColor) Or IsBrown(pixelColor) Or IsDarkGray(pixelColor) Or isPurple(pixelColor) Or isTurquesa(pixelColor) Or isDarkRed(pixelColor) Then
                                ' Si está en el estado de parpadeo, cambiar el color del píxel
                                If isOrangeBlink Then
                                    bmp.SetPixel(x, y, Color.Yellow) ' Cambiar el color a blanco para el parpadeo

                                End If
                            End If
                        Next
                    Next
                End Using

                ' Dibujar el bitmap en el PictureBox
                e.Graphics.DrawImage(bmp, 0, 0)
            End Using
        End If
    End Sub


    Private Sub pb3100_Paint(sender As Object, e As PaintEventArgs) Handles pb3100.Paint
        If pb3100.Image IsNot Nothing Then
            ' Crear un bitmap auxiliar para el dibujo
            Using bmp As New Bitmap(pb3100.Image)
                Using g As Graphics = Graphics.FromImage(bmp)
                    For y As Integer = 0 To bmp.Height - 1
                        For x As Integer = 0 To bmp.Width - 1
                            Dim pixelColor As Color = bmp.GetPixel(x, y)
                            ' Si el color del píxel está dentro del umbral de color naranja
                            If IsOrange(pixelColor) Or IsBlack(pixelColor) Or IsBrown(pixelColor) Or IsDarkGray(pixelColor) Or isPurple(pixelColor) Or isTurquesa(pixelColor) Or isDarkRed(pixelColor) Then
                                ' Si está en el estado de parpadeo, cambiar el color del píxel
                                If isOrangeBlink Then
                                    bmp.SetPixel(x, y, Color.Yellow) ' Cambiar el color a blanco para el parpadeo
                                End If
                            End If
                        Next
                    Next
                End Using

                ' Dibujar el bitmap en el PictureBox
                e.Graphics.DrawImage(bmp, 0, 0)
            End Using
        End If
    End Sub

    Private Sub pb3400_Paint(sender As Object, e As PaintEventArgs) Handles pb3400.Paint
        If pb3400.Image IsNot Nothing Then
            ' Crear un bitmap auxiliar para el dibujo
            Using bmp As New Bitmap(pb3400.Image)
                Using g As Graphics = Graphics.FromImage(bmp)
                    For y As Integer = 0 To bmp.Height - 1
                        For x As Integer = 0 To bmp.Width - 1
                            Dim pixelColor As Color = bmp.GetPixel(x, y)
                            ' Si el color del píxel está dentro del umbral de color naranja
                            If IsOrange(pixelColor) Or IsBlack(pixelColor) Or IsBrown(pixelColor) Or IsDarkGray(pixelColor) Or isPurple(pixelColor) Or isTurquesa(pixelColor) Or isDarkRed(pixelColor) Then
                                ' Si está en el estado de parpadeo, cambiar el color del píxel
                                If isOrangeBlink Then
                                    bmp.SetPixel(x, y, Color.Yellow) ' Cambiar el color a blanco para el parpadeo
                                End If
                            End If
                        Next
                    Next
                End Using

                ' Dibujar el bitmap en el PictureBox
                e.Graphics.DrawImage(bmp, 0, 0)
            End Using
        End If
    End Sub

    Private Sub pb3700_Paint(sender As Object, e As PaintEventArgs) Handles pb3700.Paint
        If pb3700.Image IsNot Nothing Then
            ' Crear un bitmap auxiliar para el dibujo
            Using bmp As New Bitmap(pb3700.Image)
                Using g As Graphics = Graphics.FromImage(bmp)
                    For y As Integer = 0 To bmp.Height - 1
                        For x As Integer = 0 To bmp.Width - 1
                            Dim pixelColor As Color = bmp.GetPixel(x, y)
                            ' Si el color del píxel está dentro del umbral de color naranja
                            If IsOrange(pixelColor) Or IsBlack(pixelColor) Or IsBrown(pixelColor) Or IsDarkGray(pixelColor) Or isPurple(pixelColor) Or isTurquesa(pixelColor) Or isDarkRed(pixelColor) Then
                                ' Si está en el estado de parpadeo, cambiar el color del píxel
                                If isOrangeBlink Then
                                    bmp.SetPixel(x, y, Color.Yellow) ' Cambiar el color a blanco para el parpadeo
                                End If
                            End If
                        Next
                    Next
                End Using

                ' Dibujar el bitmap en el PictureBox
                e.Graphics.DrawImage(bmp, 0, 0)
            End Using
        End If
    End Sub

    Private Sub pb4400_Paint(sender As Object, e As PaintEventArgs) Handles pb4400.Paint
        If pb4400.Image IsNot Nothing Then
            ' Crear un bitmap auxiliar para el dibujo
            Using bmp As New Bitmap(pb4400.Image)
                Using g As Graphics = Graphics.FromImage(bmp)
                    For y As Integer = 0 To bmp.Height - 1
                        For x As Integer = 0 To bmp.Width - 1
                            Dim pixelColor As Color = bmp.GetPixel(x, y)
                            ' Si el color del píxel está dentro del umbral de color naranja
                            If IsOrange(pixelColor) Or IsBlack(pixelColor) Or IsBrown(pixelColor) Or IsDarkGray(pixelColor) Or isPurple(pixelColor) Or isTurquesa(pixelColor) Or isDarkRed(pixelColor) Then
                                ' Si está en el estado de parpadeo, cambiar el color del píxel
                                If isOrangeBlink Then
                                    bmp.SetPixel(x, y, Color.Yellow) ' Cambiar el color a blanco para el parpadeo
                                End If
                            End If
                        Next
                    Next
                End Using

                ' Dibujar el bitmap en el PictureBox
                e.Graphics.DrawImage(bmp, 0, 0)
            End Using
        End If
    End Sub

    Private Function IsOrange(color As Color) As Boolean
        ' Comparar los componentes RGB del color con el naranja
        If Math.Abs(color.R - 255) <= orangeThreshold AndAlso
           Math.Abs(color.G - 165) <= orangeThreshold AndAlso
           Math.Abs(color.B - 0) <= orangeThreshold Then
            Return True
        End If
        Return False
    End Function

    Private Sub GunaAdvenceButton2_Click(sender As Object, e As EventArgs) Handles btnProgress.Click
        Dim frm As New frmResultHour()
        frm.ShowDialog()
    End Sub

    Private Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click

        Dim frm As New frmOEE()
        frm.ShowDialog()
        ' sendWhatsapp("1100", "1")
    End Sub

    Private Function isPurple(color As Color) As Boolean
        ' Comparar los componentes RGB del color con el naranja
        If color.Name = "ffcd46de" Then
            Return True
        End If
        Return False
    End Function

    Private Function isDarkRed(color As Color) As Boolean
        If color.Name = "ff8b0000" Then
            Return True
        End If
        Return False
    End Function

    Private Function IsBlack(color As Color) As Boolean

        If color.Name = "ff000000" Then
            Return True
        End If
        Return False
    End Function

    Private Function IsBrown(color As Color) As Boolean

        If color.Name = "ff9d6015" Then
            Return True
        End If
        Return False
    End Function

    Private Function IsDarkGray(color As Color) As Boolean
        If color.Name = "ff8d8b8b" Then
            Return True
        End If

        Return False
    End Function

    Private Function isTurquesa(color As Color) As Boolean
        If color.Name = "ff66cfd2" Then
            Return True
        End If

        Return False
    End Function
#End Region


    Private Sub frmMonitor_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        n = 1
        Application.Exit()
    End Sub

    Private Sub frmMonitor_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        n = 1
        Application.Exit()
    End Sub

    ''' <summary>
    ''' Revisa en la base de datos si hay datos de la linea
    ''' </summary>
    Private Sub insertOFFline(line As String)
        'Si no hay datos o hay menos de 10 registros, quiere decir que la linea no corrio en todo el dia. 
        Try
            Dim sql As String = "sELECT TOP (1000) [id] 
  FROM [db_kyungshin].[dbo].[t_bma_downtime]
  where process='" & line & "' and CONVERT(date,insert_date)='" & ConvierteAdateMySQL(Date.Now.ToShortDateString) & "'
  order by id desc"

            Dim tabla As New DataTable
            tabla = EjecutaSelects(sql, "buscaDatos2091")
            If tabla.Rows.Count = 0 Then
                sql = "INSERT INTO [dbo].[t_bma_downtime]
           ([process]
           ,[machine]
           ,[status]
           ,[startTime]
           ,[endTime]
           ,[AcumTime]
           ,[code]
           ,[insert_date]
           ,[remarks]
           ,[depto])
     VALUES
           ('" & line & "'
           ,'offInsert'
           ,'DOWN'
           ,'00:00:00'
           ,'00:00:00'
           ,'00:00:00'
           ,''
           ,'" & ConvierteAdateMySQL(Date.Now.ToShortDateString) & " 00:00:00.000'
           ,''
           ,'')

INSERT INTO [dbo].[t_bma_downtime]
           ([process]
           ,[machine]
           ,[status]
           ,[startTime]
           ,[endTime]
           ,[AcumTime]
           ,[code]
           ,[insert_date]
           ,[remarks]
           ,[depto])
     VALUES
           ('" & line & "'
           ,'offInsert'
           ,'RUN'
           ,'00:00:00'
           ,'00:00:00'
           ,'00:00:00'
           ,''
            ,'" & ConvierteAdateMySQL(Date.Now.ToShortDateString) & " 00:00:00.000'
           ,''
           ,'')

INSERT INTO [dbo].[t_bma_downtime]
           ([process]
           ,[machine]
           ,[status]
           ,[startTime]
           ,[endTime]
           ,[AcumTime]
           ,[code]
           ,[insert_date]
           ,[remarks]
           ,[depto])
     VALUES
           ('" & line & "'
           ,'offInsert'
           ,'DOWN'
           ,'00:00:01'
           ,''
           ,''
           ,''
            ,'" & ConvierteAdateMySQL(Date.Now.ToShortDateString) & " 00:00:00.000'
           ,''
           ,'')"

                Call Ejecuta(sql, "insertaOFFPC")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub intialLoad()

        checkEISSconection()


        Me.VerticalScroll.Maximum = 0
        Me.AutoScroll = True
        'Me.AutoScrollPosition = New Point(0, Me.Height)
        loadBD()

        Dim hora As String = Convert.ToDateTime(Date.Now.ToShortTimeString).ToString("HH:mm")


        If hora > "07:00" And hora < "19:00" Then
            turno = 1
        Else
            turno = 2
        End If

        Dim versionCore As String = QueryRow("SELECT valor From t_parametros WHERE parametro='vMonitoringProduction'", "valor", "version3134")
        If My.Application.Info.Version.ToString <> versionCore Then
            'MessageBox.Show("There is a new version available to download. Update the CORE system.")
            Dim ouputFileName As String = Environment.CurrentDirectory & "\Updater.exe"
            System.Diagnostics.Process.Start(ouputFileName)
            Application.Exit()
        End If


        loadLines()
        Shift1Controls()
        'Dim mut As System.Threading.Mutex = New System.Threading.Mutex(False, Application.ProductName)
        'Dim running As Boolean = Not mut.WaitOne(0, False)
        'If running Then
        '    Application.ExitThread()
        '    Return
        'End If

        If isRunning1100 = True Then
            insertOFFline("1100")
            loadAll(lbDT, LBrUN, lbOtm1100, "1100")

            m100.Estado = UCStatus.UCLed.Status.Encendido
            m100.IsRunning = True
            m100.Start()
        End If

        If isRunning1400 = True Then
            insertOFFline("1400")
            loadAll(lbDTm1400, lbRTm1400, lbOtm1400, "1400")
            l1400.Estado = UCStatus.UCLed.Status.Encendido
            l1400.IsRunning = True
            l1400.Start()
        End If

        If isRunning1700 = True Then
            insertOFFline("1700")
            loadAll(lbDTm1700, lbRTm1700, lbOtm1700, "1700")
            'acumularHoraActual(lbDTm1700, lbRTm1700, lbOtm1700, "1700")
            l1700.Estado = UCStatus.UCLed.Status.Encendido
            l1700.IsRunning = True
            l1700.Start()
        End If

        If isRunning2100 = True Then
            insertOFFline("2100")
            loadAll(lbDT2100, lbRT2100, lbOT2100, "2100")
            l2100.Estado = UCStatus.UCLed.Status.Encendido
            l2100.IsRunning = True
            l2100.Start()
        End If

        If isRunning2400 = True Then
            insertOFFline("2400")
            loadAll(lbDt2400, lbRT2400, lbOT2400, "2400")
            l2400.Estado = UCStatus.UCLed.Status.Encendido
            l2400.IsRunning = True
            l2400.Start()
        End If

        If isRunning2700 = True Then
            insertOFFline("2700")
            loadAll(lbDT2700, lbRt2700, lbOT2700, "2700")
            l2700.Estado = UCStatus.UCLed.Status.Encendido
            l2700.IsRunning = True
            l2700.Start()
        End If

        If isRunning3100 = True Then
            insertOFFline("3100")
            loadAll(lbDT3100, lbRT3100, lbOT3100, "3100")
            l3100.Estado = UCStatus.UCLed.Status.Encendido
            l3100.IsRunning = True
            l3100.Start()
        End If

        If isRunning3400 = True Then
            insertOFFline("3400")
            loadAll(lbDT3400, lbRT3400, lbOT3400, "3400")
            l3400.Estado = UCStatus.UCLed.Status.Encendido
            l3400.IsRunning = True
            l3400.Start()
        End If

        If isRunning3700 = True Then
            insertOFFline("3700")
            loadAll(lbDT3700, lbRT3700, lbOT3700, "3700")
            l3700.Estado = UCStatus.UCLed.Status.Encendido
            l3700.IsRunning = True
            l3700.Start()
        End If

        If isRunning4100 = True Then
            insertOFFline("4100")
            loadAll(lbDT4100, lbRT4100, lbOT4100, "4100")
            l4100.Estado = UCStatus.UCLed.Status.Encendido
            l4100.IsRunning = True
            l4100.Start()
        End If

        If isRunning4400 = True Then
            insertOFFline("4400")
            loadAll(lbDT4400, lbRt4400, lbOT4400, "4400")
            l4400.Estado = UCStatus.UCLed.Status.Encendido
            l4400.IsRunning = True
            l4400.Start()
        End If

        If isRunning4700 = True Then
            insertOFFline("4700")
            loadAll(lbDT4700, lbRT4700, lbOT4700, "4700")
            l4700.Estado = UCStatus.UCLed.Status.Encendido
            l4700.IsRunning = True
            l4700.Start()
        End If

        If isRunning5100 = True Then
            insertOFFline("5100")
            loadAll(lbDT5100, lbRT5100, lbOT5100, "5100")
            l5100.Estado = UCStatus.UCLed.Status.Encendido
            l5100.IsRunning = True
            l5100.Start()
        End If

        If isRunning5400 = True Then
            insertOFFline("5400")
            loadAll(lbDT5400, lbRT5400, lbOT5400, "5400")
            l5400.Estado = UCStatus.UCLed.Status.Encendido
            l5400.IsRunning = True
            l5400.Start()
        End If

        If isRunning5700 = True Then
            insertOFFline("5700")
            loadAll(lbDT5700, lbRT5700, lbOt5700, "5700")
            l5700.Estado = UCStatus.UCLed.Status.Encendido
            l5700.IsRunning = True
            l5700.Start()
        End If

        ProgresoGuardadoLineas()
        Panel11.Visible = False
    End Sub
    Private Sub ProgresoGuardadoLineas()

        totalProgress = 0
        totalProgress_M1_400 = 0
        totalProgress_M1_700 = 0
        totalProgress_M2_100 = 0
        totalProgress_2400 = 0
        totalProgress_2700 = 0
        totalProgress_3100 = 0
        totalProgress_3400 = 0
        totalProgress_3700 = 0
        ProgressBar1.Maximum = 100
        ProgressBar1.Increment(10)
        'PROGRESS BAR M1 100--------------------------------------------
        If isRunning1100 = True Then
            progressbarwidth = PictureBox1.Width
            progressbarheight = PictureBox1.Height
            progressbarrunit = PictureBox1.Width / segundosTotales
            progressbarcomplete = 0

            bmp = New Bitmap(progressbarwidth, progressbarheight)
            currentStatus = "R"
            ProgresoGuardado("1100", totalProgress, progressbarrunit, currentStatus, progressbarcomplete, bmp, PictureBox1, progressbarheight, m100, xpos)
            Timer1.Start()
            breakTime(PictureBox1, bmp, "M1")
        End If

        '------------------------------------------------------------------
        ProgressBar1.Increment(5)

        'PROGRESS BAR M1 400--------------------------------------------
        If isRunning1400 = True Then
            progressbarwidth_M1_400 = pbm1p400.Width
            progressbarheight_M1_400 = pbm1p400.Height
            progressbarrunit_M1_400 = pbm1p400.Width / segundosTotales
            progressbarcomplete_M1_400 = 0

            bmp_M1_400 = New Bitmap(progressbarwidth_M1_400, progressbarheight_M1_400)
            currentStatusm1_400 = "R"
            ProgresoGuardado("1400", totalProgress_M1_400, progressbarrunit_M1_400, currentStatusm1_400, progressbarcomplete_M1_400, bmp_M1_400, pbm1p400, progressbarheight_M1_400, l1400, xpos400)
            timerm1400.Start()
            breakTime(pbm1p400, bmp_M1_400, "M1")
        End If

        '------------------------------------------------------------------

        ProgressBar1.Increment(5)
        'PROGRESS BAR M1 700--------------------------------------------
        If isRunning1700 = True Then
            progressbarwidth_M1_700 = pbm1p700.Width
            progressbarheight_M1_700 = pbm1p700.Height
            progressbarrunit_M1_700 = pbm1p700.Width / segundosTotales
            progressbarcomplete_M1_700 = 0

            bmp_M1_700 = New Bitmap(progressbarwidth_M1_700, progressbarheight_M1_700)
            currentStatusm1_700 = "R"
            ProgresoGuardado("1700", totalProgress_M1_700, progressbarrunit_M1_700, currentStatusm1_700, progressbarcomplete_M1_700, bmp_M1_700, pbm1p700, progressbarheight_M1_700, l1700, xpos700)
            timerm1700.Start()
            breakTime(pbm1p700, bmp_M1_700, "M1")
        End If
        '------------------------------------------------------------------

        ProgressBar1.Increment(5)
        'PROGRESS BAR M2 100--------------------------------------------
        If isRunning2100 = True Then
            progressbarwidth_M2_100 = pbm2p100.Width
            progressbarheight_M2_100 = pbm2p100.Height
            progressbarrunit_M2_100 = pbm2p100.Width / segundosTotales
            progressbarcomplete_M2_100 = 0

            bmp_M2_100 = New Bitmap(progressbarwidth_M2_100, progressbarheight_M2_100)
            currentStatusm2_100 = "R"
            ProgresoGuardado("2100", totalProgress_M2_100, progressbarrunit_M2_100, currentStatusm2_100, progressbarcomplete_M2_100, bmp_M2_100, pbm2p100, progressbarheight_M2_100, l2100, xpos2100)
            timerm2100.Start()
            breakTime(pbm2p100, bmp_M2_100, "M2")
        End If

        '------------------------------------------------------------------
        ProgressBar1.Increment(5)
        'PROGRESS BAR M2 400--------------------------------------------
        If isRunning2400 = True Then
            progressbarwidth_2400 = pb2400.Width
            progressbarheight_2400 = pb2400.Height
            progressbarrunit_2400 = pb2400.Width / segundosTotales
            progressbarcomplete_2400 = 0
            bmp_2400 = New Bitmap(progressbarwidth_2400, progressbarheight_2400)
            currentStatus2400 = "R"
            ProgresoGuardado("2400", totalProgress_2400, progressbarrunit_2400, currentStatus2400, progressbarcomplete_2400, bmp_2400, pb2400, progressbarheight_2400, l2400, xpos2400)
            timer2400.Start()
            breakTime(pb2400, bmp_2400, "M2")
        End If

        '------------------------------------------------------------------
        ProgressBar1.Increment(5)
        'PROGRESS BAR M2 700--------------------------------------------
        If isRunning2700 = True Then
            progressbarwidth_2700 = pb2700.Width
            progressbarheight_2700 = pb2700.Height
            progressbarrunit_2700 = pb2700.Width / segundosTotales
            progressbarcomplete_2700 = 0
            bmp_2700 = New Bitmap(progressbarwidth_2700, progressbarheight_2700)
            currentStatus2700 = "R"
            ProgresoGuardado("2700", totalProgress_2700, progressbarrunit_2700, currentStatus2700, progressbarcomplete_2700, bmp_2700, pb2700, progressbarheight_2700, l2700, xpos2700)
            timer2700.Start()
            breakTime(pb2700, bmp_2700, "M2")
        End If
        '------------------------------------------------------------------
        ProgressBar1.Increment(5)
        'PROGRESS BAR M3 100--------------------------------------------
        If isRunning3100 = True Then
            progressbarwidth_3100 = pb3100.Width
            progressbarheight_3100 = pb3100.Height
            progressbarrunit_3100 = pb3100.Width / segundosTotales
            progressbarcomplete_3100 = 0
            bmp_3100 = New Bitmap(progressbarwidth_3100, progressbarheight_3100)
            currentStatus3100 = "R"
            ProgresoGuardado("3100", totalProgress_3100, progressbarrunit_3100, currentStatus3100, progressbarcomplete_3100, bmp_3100, pb3100, progressbarheight_3100, l3100, xpos3100)
            timer3100.Start()
            breakTime(pb3100, bmp_3100, "M3")
        End If
        '------------------------------------------------------------------
        ProgressBar1.Increment(5)
        'PROGRESS BAR M3 400--------------------------------------------
        If isRunning3400 = True Then
            progressbarwidth_3400 = pb3400.Width
            progressbarheight_3400 = pb3400.Height
            progressbarrunit_3400 = pb3400.Width / segundosTotales
            progressbarcomplete_3400 = 0
            bmp_3400 = New Bitmap(progressbarwidth_3400, progressbarheight_3400)
            currentStatus3400 = "R"
            ProgresoGuardado("3400", totalProgress_3400, progressbarrunit_3400, currentStatus3400, progressbarcomplete_3400, bmp_3400, pb3400, progressbarheight_3400, l3400, xpos3400)
            timer3400.Start()
            breakTime(pb3400, bmp_3400, "M3")
        End If
        '------------------------------------------------------------------

        ProgressBar1.Increment(5)
        'PROGRESS BAR M3 700--------------------------------------------
        If isRunning3700 = True Then
            progressbarwidth_3700 = pb3700.Width
            progressbarheight_3700 = pb3700.Height
            progressbarrunit_3700 = pb3700.Width / segundosTotales
            progressbarcomplete_3700 = 0
            bmp_3700 = New Bitmap(progressbarwidth_3700, progressbarheight_3700)
            currentStatus3700 = "R"
            ProgresoGuardado("3700", totalProgress_3700, progressbarrunit_3700, currentStatus3700, progressbarcomplete_3700, bmp_3700, pb3700, progressbarheight_3700, l3700, xpos3700)
            timer3700.Start()
            breakTime(pb3700, bmp_3700, "M3")
        End If
        '------------------------------------------------------------------
        ProgressBar1.Increment(5)
        'PROGRESS BAR M4 100--------------------------------------------
        If isRunning4100 = True Then
            totalProgress_4100 = 0
            progressbarwidth_4100 = pb4100.Width
            progressbarheight_4100 = pb4100.Height
            progressbarrunit_4100 = pb4100.Width / segundosTotales
            progressbarcomplete_4100 = 0
            bmp_4100 = New Bitmap(progressbarwidth_4100, progressbarheight_4100)
            currentStatus4100 = "R"
            ProgresoGuardado("4100", totalProgress_4100, progressbarrunit_4100, currentStatus4100, progressbarcomplete_4100, bmp_4100, pb4100, progressbarheight_4100, l4100, xpos4100)
            timer4100.Start()
            breakTime(pb4100, bmp_4100, "M4")
        End If
        '------------------------------------------------------------------
        ProgressBar1.Increment(15)
        'PROGRESS BAR M4 400--------------------------------------------
        If isRunning4400 = True Then
            totalProgress_4400 = 0
            progressbarwidth_4400 = pb4400.Width
            progressbarheight_4400 = pb4400.Height
            progressbarrunit_4400 = pb4400.Width / segundosTotales
            progressbarcomplete_4400 = 0
            bmp_4400 = New Bitmap(progressbarwidth_4400, progressbarheight_4400)
            currentStatus4400 = "R"
            ProgresoGuardado("4400", totalProgress_4400, progressbarrunit_4400, currentStatus4400, progressbarcomplete_4400, bmp_4400, pb4400, progressbarheight_4400, l4400, xpos4400)
            timer4400.Start()
            breakTime(pb4400, bmp_4400, "M4")
        End If
        '------------------------------------------------------------------
        ProgressBar1.Increment(5)
        'PROGRESS BAR M4 700--------------------------------------------
        If isRunning4700 = True Then
            totalProgress_4700 = 0
            progressbarwidth_4700 = pb4700.Width
            progressbarheight_4700 = pb4700.Height
            progressbarrunit_4700 = pb4700.Width / segundosTotales
            progressbarcomplete_4700 = 0
            bmp_4700 = New Bitmap(progressbarwidth_4700, progressbarheight_4700)
            currentStatus4700 = "R"
            ProgresoGuardado("4700", totalProgress_4700, progressbarrunit_4700, currentStatus4700, progressbarcomplete_4700, bmp_4700, pb4700, progressbarheight_4700, l4700, xpos4700)
            timer4700.Start()
            breakTime(pb4700, bmp_4700, "M4")
        End If
        '------------------------------------------------------------------
        ProgressBar1.Increment(5)
        'PROGRESS BAR M5 100--------------------------------------------
        If isRunning5100 = True Then
            totalProgress_5100 = 0
            progressbarwidth_5100 = pb5100.Width
            progressbarheight_5100 = pb5100.Height
            progressbarrunit_5100 = pb5100.Width / segundosTotales
            progressbarcomplete_5100 = 0
            bmp_5100 = New Bitmap(progressbarwidth_5100, progressbarheight_5100)
            currentStatus5100 = "R"
            ProgresoGuardado("5100", totalProgress_5100, progressbarrunit_5100, currentStatus5100, progressbarcomplete_5100, bmp_5100, pb5100, progressbarheight_5100, l5100, xpos5100)
            timer5100.Start()
            breakTime(pb5100, bmp_5100, "M5")
        End If

        '------------------------------------------------------------------
        ProgressBar1.Increment(5)
        'PROGRESS BAR M5 400--------------------------------------------
        If isRunning5400 = True Then
            totalProgress_5400 = 0
            progressbarwidth_5400 = pb5400.Width
            progressbarheight_5400 = pb5400.Height
            progressbarrunit_5400 = pb5400.Width / segundosTotales
            progressbarcomplete_5400 = 0
            bmp_5400 = New Bitmap(progressbarwidth_5400, progressbarheight_5400)
            currentStatus5400 = "R"
            ProgresoGuardado("5400", totalProgress_5400, progressbarrunit_5400, currentStatus5400, progressbarcomplete_5400, bmp_5400, pb5400, progressbarheight_5400, l5400, xpos5400)
            timer5400.Start()
            breakTime(pb5400, bmp_5400, "M5")
        End If
        '------------------------------------------------------------------
        ProgressBar1.Increment(15)
        If isRunning5700 = True Then
            totalProgress_5700 = 0
            progressbarwidth_5700 = pb5700.Width
            progressbarheight_5700 = pb5700.Height
            progressbarrunit_5700 = pb5700.Width / segundosTotales
            progressbarcomplete_5700 = 0
            bmp_5700 = New Bitmap(progressbarwidth_5700, progressbarheight_5700)
            currentStatus5700 = "R"
            ProgresoGuardado("5700", totalProgress_5700, progressbarrunit_5700, currentStatus5700, progressbarcomplete_5700, bmp_5700, pb5700, progressbarheight_5700, l5700, xpos5700)
            timer5700.Start()
            breakTime(pb5700, bmp_5700, "M5")
        End If
        ProgressBar1.Value = 0
    End Sub

    Dim ultimoStartTime As String = ""
    Dim horasAsumar As TimeSpan
    Dim ultimoRow As DataTable
    Private Sub acumularHoraActual(labelDT As Label, labelRT As Label, labelOF As Label, Optional NumProceso As String = "")
        Try
            ultimoStartTime = ""

            ultimoRow = EjecutaSelects("select top 1 status,startTime FROM [db_kyungshin].[dbo].[t_bma_downtime] d 
            WHERE  CONVERT(date, insert_date) = '" & Date.Now.ToShortDateString() & "' and process='" & NumProceso & "' 
              order by id desc", "buscaDatos392")


            lastStatus = CType(ultimoRow.Rows(0)("status"), String)
            ultimoStartTime = CType(ultimoRow.Rows(0)("startTime"), String)

            '=================================================================================================


            If lastStatus = "DOWN" Then
                horasAsumar = CDate(Date.Now.ToShortTimeString) - CDate(ultimoStartTime)
                labelDT.Text = CDate(labelDT.Text) + horasAsumar

            ElseIf lastStatus = "RUN" Then
                horasAsumar = CDate(Date.Now.ToShortTimeString) - CDate(ultimoStartTime)
                labelRT.Text = CDate(labelRT.Text) + horasAsumar
            End If


        Catch ex As Exception

        End Try
    End Sub

    Dim horaAconvertir As TimeSpan
    Private Sub acumularHoraActualNuevo(hora As String, statusToCheck As String, selectedLabel As Label)
        Try

            If statusToCheck = "DOWN" Then
                horaAconvertir = TimeSpan.Parse("07:00:00") + TimeSpan.Parse(hora)
            ElseIf statusToCheck = "RUN" Then
                horaAconvertir = TimeSpan.Parse("07:00:00") + TimeSpan.Parse(hora)
            End If
            selectedLabel.Text = horaAconvertir.ToString

        Catch ex As Exception

        End Try
    End Sub

    Dim horaSep() As String
    Dim horaSep2() As String
    Dim hora, _hora2 As String

    Dim segundos As Integer = 0
    Dim segHora2 As Integer = 0
    Private Function CalcularPorcentaje(hora1 As String, Optional labelOF As String = "") As String
        Try
            Erase horaSep
            Erase horaSep2

            segundos = 0
            segHora2 = 0


            hora = acumTime("00:00:00", hora1)
            horaSep = hora.Split(":")
            segundos = horaSep(2) + (horaSep(1) * 60) + (horaSep(0) * 3600)


            If labelOF <> "_" Then
                _hora2 = acumTime(labelOF, Date.Now.ToShortTimeString) 'la hra actual es el 100%
            Else
                _hora2 = acumTime("00:00:00", Date.Now.ToShortTimeString) 'la hra actual es el 100%
            End If


            horaSep2 = _hora2.Split(":")
            segHora2 = horaSep2(2) + (horaSep2(1) * 60) + (horaSep2(0) * 3600)

            Return Math.Round((((segundos) * 100) / (segHora2)), 1)

        Catch ex As Exception
            Return ""
        End Try

    End Function

    Private Function CalcularPorcentajeTurno1(proceso As String) As String
        Try
            Erase horaSep
            Erase horaSep2

            segundos = 0
            segHora2 = 0

            horaS = QueryRow("SELECT 
                              CONVERT(VARCHAR(8), DATEADD(SECOND, SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, AcumTime))), '00:00:00'), 108) AS time
                            FROM [db_kyungshin].[dbo].[t_bma_downtime]
                            WHERE
                              process = '" & proceso & "'
                              AND CONVERT(date, insert_date) = '" & Date.Now.ToShortDateString & "'
                              AND status = 'DOWN'
                              AND startTime > '07:00:00';", "time", "buscaDT")

            Try
                horaSeparada = Split(horaS, " ")
            Catch ex As Exception

            End Try

            Try
                horaSep = horaS.Split(":")
                segundos = horaSep(2) + (horaSep(1) * 60) + (horaSep(0) * 3600)
            Catch ex As Exception
                segundos = 1
            End Try




            'obtener el primer DOWN time del turno (PUEDE QUE LA PC NO ESTE CONECTADA AL PLC, O HAYA OFFTIME DESPUES DE LAS 7AM
            primerDowntimeDelDia = QueryRow("sELECT TOP (1) startTime
  FROM [db_kyungshin].[dbo].[t_bma_downtime]
  where process='" & proceso & "' and CONVERT(date,insert_date)='" & Date.Now.ToShortDateString & "' and status='DOWN' and startTime>'07:00:00' and endTime>'06:00:00' 
  order by id asc", "startTime", "2871772")


            '          'si el primer starttime es menor que las 7:00 am quiere decir que el downtime empezo antes de las 7 y hay que sumar los segundos 
            '          'al tiempo muerto acumulado
            '          horaS = QueryRow(" sELECT TOP (1) startTime
            'FROM [db_kyungshin].[dbo].[t_bma_downtime]
            'where process='" & proceso & "' and CONVERT(date,insert_date)='" & Date.Now.ToShortDateString & "' and status='DOWN' and endTime>'07:00:00' 
            'order by id asc", "startTime", "87377")

            '          If horaS < "07:00:00" Then
            '              sumaSegundosAlDowntime = acumTime("07:00:00", primerDowntimeDelDia)
            '              horaSep = sumaSegundosAlDowntime.Split(":")
            '              segundos += horaSep(2) + (horaSep(1) * 60) + (horaSep(0) * 3600)
            '          End If


            '_hora2 = acumTime("07:00:00", Date.Now.ToShortTimeString) 'la hra actual es el 100%
            _hora2 = acumTime(primerDowntimeDelDia, Date.Now.ToShortTimeString) 'la hra actual es el 100%



            horaSep2 = _hora2.Split(":")
            segHora2 = horaSep2(2) + (horaSep2(1) * 60) + (horaSep2(0) * 3600)

            Return Math.Round((((segundos) * 100) / (segHora2)), 1)

        Catch ex As Exception
            Return 100
        End Try

    End Function
    Dim queryPercentage As String = ""
    Private Function calcularRunPercentage(proceso As String) As String
        Try
            queryPercentage = ""
            queryPercentage = "
WITH times AS (
    -- Calcular totaltime
    SELECT 
        SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, AcumTime))) AS total_seconds
    FROM [db_kyungshin].[dbo].[t_bma_downtime]
    WHERE process='" & proceso & "' 
    AND CONVERT(DATE, insert_date)='" & Date.Now.ToShortDateString & "'
    AND startTime >= '07:00:00'
),
runtime AS (
    -- Calcular runtime
    SELECT 
        SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, AcumTime))) AS runtime_seconds
    FROM [db_kyungshin].[dbo].[t_bma_downtime]
    WHERE process='" & proceso & "' 
    AND CONVERT(DATE, insert_date)='" & Date.Now.ToShortDateString & "'
    AND status = 'RUN'
    AND startTime >= '07:00:00'
),
downtime AS (
    -- Calcular downtime
    SELECT 
        SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, AcumTime))) AS downtime_seconds
    FROM [db_kyungshin].[dbo].[t_bma_downtime]
    WHERE process='" & proceso & "' 
    AND CONVERT(DATE, insert_date)='" & Date.Now.ToShortDateString & "'
    AND status = 'DOWN'
    AND startTime >= '07:00:00'
)
SELECT 

    
    -- Porcentaje de RUN respecto a TotalTime
    CAST((r.runtime_seconds * 100.0 / NULLIF(t.total_seconds, 0)) AS DECIMAL(5,2)) AS RunTime_Percentage
    
FROM times t
LEFT JOIN runtime r ON 1=1
LEFT JOIN downtime d ON 1=1;
"
            Return QueryRow(queryPercentage, "RunTime_Percentage", "runPercentage")
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function calcularDownPercentage(proceso As String) As String
        Try
            queryPercentage = ""
            queryPercentage = "
WITH times AS (
    -- Calcular totaltime
    SELECT 
        SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, AcumTime))) AS total_seconds
    FROM [db_kyungshin].[dbo].[t_bma_downtime]
    WHERE process='" & proceso & "' 
    AND CONVERT(DATE, insert_date)='" & Date.Now.ToShortDateString & "'
    AND startTime >= '07:00:00'
),
runtime AS (
    -- Calcular runtime
    SELECT 
        SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, AcumTime))) AS runtime_seconds
    FROM [db_kyungshin].[dbo].[t_bma_downtime]
    WHERE process='" & proceso & "' 
    AND CONVERT(DATE, insert_date)='" & Date.Now.ToShortDateString & "'
    AND status = 'RUN'
    AND startTime >= '07:00:00'
),
downtime AS (
    -- Calcular downtime
    SELECT 
        SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, AcumTime))) AS downtime_seconds
    FROM [db_kyungshin].[dbo].[t_bma_downtime]
    WHERE process='" & proceso & "' 
    AND CONVERT(DATE, insert_date)='" & Date.Now.ToShortDateString & "'
    AND status = 'DOWN'
    AND startTime >= '07:00:00'
)
SELECT 

    
     -- Porcentaje de DOWN respecto a TotalTime
    CAST((d.downtime_seconds * 100.0 / NULLIF(t.total_seconds, 0)) AS DECIMAL(5,2)) AS DownTime_Percentage
    
FROM times t
LEFT JOIN runtime r ON 1=1
LEFT JOIN downtime d ON 1=1;
"
            Return QueryRow(queryPercentage, "DownTime_Percentage", "downPerc")
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Dim rectangulo As Rectangle
    Private Sub ProgresoGuardado(NumProceso As String, totalprogreso As Double, progresobarunidad As Double, estatus As String, progresoCompletado As Double,
                                 bitmap As Bitmap, picbox As PictureBox, alturaprogres As Double, userControlLEd As UCStatus.UCLed, xposicion As Double)
        Try
            Dim queryCompl As String = ""

            If NumProceso <> "" Then
                queryCompl = " AND d.process='" & NumProceso & "' "
            End If

            If onlyShowShift1 = True Then
                queryCompl = queryCompl & " AND starttime>'07:00'"

            End If

            query = "SELECT  [id]
                      ,d.process
                      ,[machine]
                      ,[status],startTime
                      ,[AcumTime],remarks,alarmedDepto,ec1,ec2,ec3,ec4,ec5,step
                  FROM [db_kyungshin].[dbo].[t_bma_downtime] d
                  where  CONVERT(date, insert_date)='" & Date.Now.ToShortDateString() & "' " & queryCompl & " order by id asc"
            dgvAux.DataSource = EjecutaSelects(query, "fillAux")
            'ResizeCols(dgvAux)
            Dim horaSep() As String
            Dim hora As String
            Dim segundos As Integer = 0
            Dim ultimahoraFix As String = ""
            For Each rw As DataGridViewRow In dgvAux.Rows

                ' Console.WriteLine(rw.Cells("Starttime").Value)
                If rw.Cells("AcumTime").Value = "" Then

                    query = "select top 1 starttime FROM [db_kyungshin].[dbo].[t_bma_downtime] d
                        WHERE CONVERT(date, insert_date) = '" & Date.Now.ToShortDateString() & "'  " & queryCompl & " 
                          order by id desc"


                    ultimahoraFix = QueryRow(query, "starttime", "ultimoTime")
                    Dim horaActual2 As String = TimeString
                    hora = acumTime(ultimahoraFix, horaActual2)
                Else
                    hora = rw.Cells("AcumTime").Value
                End If

                horaSep = hora.Split(":")
                segundos = horaSep(2) + (horaSep(1) * 60) + (horaSep(0) * 3600)

                If NumProceso = "3700" Then
                    Console.WriteLine("here")
                End If

                If rw.Cells("STATUS").Value = "RUN" Then
                    For i = 0 To segundos
                        If estatus = "O" Or estatus = "R" Then
                            If segundos = 0 Then
                                totalprogreso -= 0
                            Else
                                totalprogreso -= 1
                            End If
                            xposicion = CDbl(totalprogreso * progresobarunidad)
                            progresoCompletado = 0
                        End If
                        estatus = "D"
                        g = Graphics.FromImage(bitmap)
                        'If segundos <> 0 Then
                        g.FillRectangle(Brushes.Green, New Rectangle(xposicion, 0, CDbl(progresoCompletado * progresobarunidad) + 0.5, alturaprogres))
                        'End If

                        picbox.Image = bitmap
                        progresoCompletado += 1
                        '  If segundos <> 0 Then
                        totalprogreso += 1
                        ' End If

                    Next
                ElseIf rw.Cells("STATUS").Value = "DOWN" Then
                    For i = 0 To segundos
                        If estatus = "D" Or estatus = "O" Then
                            If segundos = 0 Then
                                totalprogreso -= 0
                            Else
                                totalprogreso -= 1
                            End If

                            xposicion = CDbl(totalprogreso * progresobarunidad)
                            progresoCompletado = 0
                        End If
                        estatus = "R"
                        g = Graphics.FromImage(bitmap)

                        'rectangulo a colorear
                        rectangulo = New Rectangle(xposicion, 0, CDbl(progresoCompletado * progresobarunidad) + 0.5, alturaprogres)

                        If IsDBNull(rw.Cells("ec1").Value) Then
                            If segundos >= 300 Then

                                Dim newColor As Color = Color.FromArgb(&HFF8D8B8B)
                                Dim brownBrush As New SolidBrush(newColor)
                                g.FillRectangle(brownBrush, rectangulo)
                            Else
                                g.FillRectangle(Brushes.Red, New Rectangle(xposicion, 0, CDbl(progresoCompletado * progresobarunidad) + 0.5, alturaprogres))
                            End If

                        Else
                            'nuevos colores
                            If rw.Cells("ec1").Value = "" And rw.Cells("alarmedDepto").Value = "" Then
                                If segundos >= 300 Then
                                    Dim newColor As Color = Color.FromArgb(&HFF8D8B8B)
                                    Dim brownBrush As New SolidBrush(newColor)
                                    g.FillRectangle(brownBrush, rectangulo)
                                Else
                                    g.FillRectangle(Brushes.Red, New Rectangle(xposicion, 0, CDbl(progresoCompletado * progresobarunidad) + 0.5, alturaprogres))
                                End If
                                'If segundos <> 0 Then
                                ' g.FillRectangle(Brushes.Red, New Rectangle(xposicion, 0, CDbl(progresoCompletado * progresobarunidad) + 0.5, alturaprogres))
                                ' End If

                            Else
                                If rw.Cells("step").Value = "1" And rw.Cells("alarmedDepto").Value = "PRODUCTION" And rw.Cells("ec1").Value = "08" Then
                                    g.FillRectangle(Brushes.DarkGray, New Rectangle(xposicion, 0, CDbl(progresoCompletado * progresobarunidad) + 0.5, alturaprogres))
                                ElseIf rw.Cells("step").Value = "0" And rw.Cells("alarmedDepto").Value = "PRODUCTION" And rw.Cells("ec1").Value = "08" Then
                                    g.FillRectangle(Brushes.DarkGray, New Rectangle(xposicion, 0, CDbl(progresoCompletado * progresobarunidad) + 0.5, alturaprogres))
                                Else
                                    'solo alarmedDepto
                                    If rw.Cells("alarmedDepto").Value <> "" And rw.Cells("EC1").Value = "" And rw.Cells("EC2").Value = "" And
                                        rw.Cells("EC3").Value = "" And rw.Cells("EC4").Value = "" Then
                                        If rw.Cells("alarmedDepto").Value = "MATERIAL" Then
                                            Console.WriteLine("here")

                                        End If

                                        Select Case rw.Cells("alarmedDepto").Value
                                            Case "PRODUCTION" : g.FillRectangle(Brushes.DarkGray, rectangulo)
                                                Dim newColor As Color = Color.FromArgb(&HFF8D8B8B)
                                                Dim brownBrush As New SolidBrush(newColor)
                                                g.FillRectangle(brownBrush, rectangulo)
                                            Case "QUALITY" : g.FillRectangle(Brushes.Purple, rectangulo)
                                                Dim newColor As Color = Color.FromArgb(&HFFCD46DE)
                                                Dim brownBrush As New SolidBrush(newColor)
                                                g.FillRectangle(brownBrush, rectangulo)
                                            Case "MATERIAL"
                                                Dim brownColor As Color = Color.FromArgb(&HFF, &H9D, &H60, &H15)
                                                Dim brownBrush As New SolidBrush(brownColor)
                                                g.FillRectangle(brownBrush, rectangulo)
                                            Case "MAINTENANCE" : g.FillRectangle(Brushes.Black, rectangulo)
                                            Case "OTHER" : g.FillRectangle(Brushes.White, rectangulo)
                                                Dim newColor As Color = Color.FromArgb(&HFF66CFD2)
                                                Dim brownBrush As New SolidBrush(newColor)
                                                g.FillRectangle(brownBrush, rectangulo)
                                        End Select

                                        'alarmeddepto y EC1
                                    ElseIf rw.Cells("alarmedDepto").Value <> "" And rw.Cells("EC1").Value <> "" Then
                                        'If rw.Cells("alarmedDepto").Value = "PRODUCTION" Or rw.Cells("alarmedDepto").Value = "OTHER" Then
                                        '    g.FillRectangle(Brushes.Blue, rectangulo)
                                        'Else
                                        '    g.FillRectangle(Brushes.Yellow, rectangulo)
                                        'End If
                                        ' blinkBar = True
                                        'en esta seccion se cambio el ultimo ddigito de cada color para que deje de parpadear en los metodos isblack, isbrown, etc
                                        Select Case rw.Cells("alarmedDepto").Value
                                            Case "PRODUCTION" : g.FillRectangle(Brushes.DarkGray, rectangulo)
                                                Dim newColor As Color = Color.FromArgb(&HFF8D8B8C)
                                                Dim brownBrush As New SolidBrush(newColor)
                                                g.FillRectangle(brownBrush, rectangulo)
                                            Case "QUALITY" : g.FillRectangle(Brushes.Purple, rectangulo)
                                                Dim newColor As Color = Color.FromArgb(&HFFCD46DF)
                                                Dim brownBrush As New SolidBrush(newColor)
                                                g.FillRectangle(brownBrush, rectangulo)
                                            Case "MATERIAL"
                                                Dim brownColor As Color = Color.FromArgb(&HFF, &H9D, &H60, &H16)
                                                Dim brownBrush As New SolidBrush(brownColor)
                                                g.FillRectangle(brownBrush, rectangulo)
                                            Case "MAINTENANCE" ' g.FillRectangle(Brushes.Black, rectangulo)
                                                '&HFF000000
                                                Dim newColor As Color = Color.FromArgb(&HFF000001)
                                                Dim brownBrush As New SolidBrush(newColor)
                                                g.FillRectangle(brownBrush, rectangulo)
                                            Case "OTHER" : g.FillRectangle(Brushes.White, rectangulo)
                                                Dim newColor As Color = Color.FromArgb(&HFF66CFD3)
                                                Dim brownBrush As New SolidBrush(newColor)
                                                g.FillRectangle(brownBrush, rectangulo)
                                        End Select
                                        '       'alarmedDepto EC1 y EC2
                                        '   ElseIf rw.Cells("alarmedDepto").Value <> "" And rw.Cells("EC1").Value <> "" And rw.Cells("EC2").Value <> "" And
                                        '       rw.Cells("EC3").Value = "" And rw.Cells("EC4").Value = "" Then
                                        '       g.FillRectangle(Brushes.Blue, rectangulo)
                                        '       'AlarmedDepto EC1 EC2 Y EC3
                                        '   ElseIf rw.Cells("alarmedDepto").Value <> "" And rw.Cells("EC1").Value <> "" And rw.Cells("EC2").Value <> "" And
                                        '       rw.Cells("EC3").Value <> "" And rw.Cells("EC4").Value = "" Then
                                        '       g.FillRectangle(Brushes.Blue, rectangulo)
                                        '       'alarmedDepto EC1 EC2 EC3 y EC4
                                        '   ElseIf rw.Cells("alarmedDepto").Value <> "" And rw.Cells("EC1").Value <> "" And rw.Cells("EC2").Value <> "" And
                                        'rw.Cells("EC3").Value <> "" And rw.Cells("EC4").Value <> "" Then
                                        '       g.FillRectangle(Brushes.Blue, rectangulo)
                                        '       'alarmedDepto EC1 EC2 EC3 EC4 y EC5
                                        '   ElseIf rw.Cells("alarmedDepto").Value <> "" And rw.Cells("EC1").Value <> "" And rw.Cells("EC2").Value <> "" And
                                        'rw.Cells("EC3").Value <> "" And rw.Cells("EC4").Value <> "" And rw.Cells("EC5").Value <> "" Then
                                        '       g.FillRectangle(Brushes.Blue, New Rectangle(xposicion, 0, CDbl(progresoCompletado * progresobarunidad) + 0.5, alturaprogres))
                                    End If

                                End If

                            End If

                        End If


                        picbox.Image = bitmap
                        progresoCompletado += 1

                        'esta linea es para que la barra no se pase de la hora actual
                        If segundos <> 0 Then
                            totalprogreso += 1
                        End If
                    Next
                ElseIf rw.Cells("STATUS").Value = "OFF" Then
                    For i = 0 To segundos
                        If estatus = "R" Or estatus = "D" Then
                            totalprogreso -= 1
                            xposicion = CDbl(totalprogreso * progresobarunidad)
                            progresoCompletado = 0
                        End If
                        estatus = "O"
                        Dim newColor As Color = Color.FromArgb(&HFF66CFD3)
                        Dim progressBarBrush As New SolidBrush(newColor)

                        g = Graphics.FromImage(bitmap)
                        g.FillRectangle(progressBarBrush, New Rectangle(xposicion, 0, CDbl(progresoCompletado * progresobarunidad) + 1, alturaprogres))

                        'g = Graphics.FromImage(bitmap)
                        'g.FillRectangle(Brushes.SlateGray, New Rectangle(xposicion, 0, CDbl(progresoCompletado * progresobarunidad) + 1, alturaprogres))
                        picbox.Image = bitmap
                        progresoCompletado += 1
                        totalprogreso += 1
                    Next

                End If
            Next
            Console.WriteLine("")

            'ultima hora
            query = "select top 1 endTime FROM [db_kyungshin].[dbo].[t_bma_downtime] d
WHERE CONVERT(date, insert_date) = '" & Date.Now.ToShortDateString() & "'  " & queryCompl & " 
  order by id desc"

            Dim lastHour As String = QueryRow(query, "endTime", "statusEnd")

            If lastHour = "" Then
                query = "select top 1 starttime FROM [db_kyungshin].[dbo].[t_bma_downtime] d
                        WHERE CONVERT(date, insert_date) = '" & Date.Now.ToShortDateString() & "'  " & queryCompl & " 
                          order by id desc"


                lastHour = QueryRow(query, "starttime", "ultimoTime")

                query = "select top 1 status FROM [db_kyungshin].[dbo].[t_bma_downtime] d
                        WHERE CONVERT(date, insert_date) = '" & Date.Now.ToShortDateString() & "'  " & queryCompl & " 
                          order by id desc"

                estatus = QueryRow(query, "status", "statusEnd")

                'If estatus = "DOWN" Then
                '    estatus = "D"
                'ElseIf estatus = "RUN" Then
                '    estatus = "R"
                'End If

            End If
            Dim horaActual As String = TimeString
            hora = acumTime(lastHour, horaActual)
            horaSep = hora.Split(":")

            'asignar el tiempo transcurrido al cronometro de alarma, los segundos pueden estar en negativo porque la hora de la pc que esta insertando registros es
            ' diferente a la hora actual de la pc donde se ejecuta el sistema de monitoreo
            If horaSep(2) < 0 Then
                userControlLEd.Segundos = horaSep(2) * -1
            Else
                userControlLEd.Segundos = horaSep(2)
            End If
            userControlLEd.Minutos = horaSep(1)
            userControlLEd.Horas = horaSep(0)
            '-------------------------------------------------------
            segundos = horaSep(2) + (horaSep(1) * 60) + (horaSep(0) * 3600)


            For i = 0 To segundos
                If estatus = "R" Then
                    If i = 0 Then
                        totalprogreso -= 1
                        xposicion = CDbl(totalprogreso * progresobarunidad)
                        progresoCompletado = 0
                    End If

                    ' estatus = "D"
                    g = Graphics.FromImage(bitmap)
                    'g.Clear(Color.LightBlue)
                    g.FillRectangle(Brushes.Green, New Rectangle(xposicion, 0, CDbl(progresoCompletado * progresobarunidad) + 1, alturaprogres))
                    picbox.Image = bitmap
                    progresoCompletado += 1
                    totalprogreso += 1

                ElseIf estatus = "D" Then
                    If i = 0 Then
                        totalprogreso -= 1
                        xposicion = CDbl(totalprogreso * progresobarunidad)
                        progresoCompletado = 0
                    End If

                    g = Graphics.FromImage(bitmap)
                    'g.Clear(Color.LightBlue)
                    g.FillRectangle(Brushes.Red, New Rectangle(xposicion, 0, CDbl(progresoCompletado * progresobarunidad) + 1, alturaprogres))
                    picbox.Image = bitmap
                    progresoCompletado += 1
                    totalprogreso += 1
                End If
                ' estatus = "D"

            Next

            'EN ESTA SECCION SE CALCULAN LOS PORCENTAJES DE TIEMPO MUERTO Y TIEMPO ACTIVO

            If NumProceso = "1100" Then
                xpos = xposicion
                currentStatus = estatus
                totalProgress = totalprogreso
                'lbPorDTm1100.Text = CalcularPorcentaje(lbDT.Text, lbOtm1100.Text)
                lbPorDTm1100.Text = CalcularPorcentajeTurno1("1100")
                lbPorRTm1100.Text = Math.Round(100 - CDbl(lbPorDTm1100.Text), 1) & "%"
                'lbPorRTm1100.Text = CalcularPorcentaje(LBrUN.Text)
                'lbPorOfm1100.Text = CalcularPorcentaje(lbOtm1100.Text)
                lbPorDTm1100.Text &= "%"
            ElseIf NumProceso = "1700" Then
                xpos700 = xposicion
                currentStatusm1_700 = estatus
                totalProgress_M1_700 = totalprogreso
                'lbPorDtm1700.Text = CalcularPorcentaje(lbDTm1700.Text, lbOtm1700.Text)
                lbPorDtm1700.Text = CalcularPorcentajeTurno1("1700")
                lbPorRtm1700.Text = Math.Round(100 - CDbl(lbPorDtm1700.Text), 1) & "%"
                lbPorDtm1700.Text &= "%"
            ElseIf NumProceso = "1400" Then
                xpos400 = xposicion
                currentStatusm1_400 = estatus
                totalProgress_M1_400 = totalprogreso
                'lbPorDTm1400.Text = CalcularPorcentaje(lbDTm1400.Text, lbOtm1400.Text)
                lbPorDTm1400.Text = CalcularPorcentajeTurno1("1400")
                lbPorRtm1400.Text = Math.Round(100 - CDbl(lbPorDTm1400.Text), 1) & "%"
                'lbPorRtm1400.Text = CalcularPorcentaje(lbRTm1400.Text)
                'lbPorOtm1400.Text = CalcularPorcentaje(lbOtm1400.Text)
                lbPorDTm1400.Text &= "%"
            ElseIf NumProceso = "2100" Then
                xpos2100 = xposicion
                currentStatusm2_100 = estatus
                totalProgress_M2_100 = totalprogreso
                'lbPorDT2100.Text = CalcularPorcentaje(lbDT2100.Text, lbOT2100.Text)
                lbPorDT2100.Text = calcularDownPercentage("2100") & "%"
                lbPorRT2100.Text = calcularRunPercentage("2100") & "%"
            ElseIf NumProceso = "2400" Then
                xpos2400 = xposicion
                currentStatus2400 = estatus
                totalProgress_2400 = totalprogreso

                lbPORDt2400.Text = calcularDownPercentage("2400") & "%"
                lbPOrRT2400.Text = calcularRunPercentage("2400") & "%"

            ElseIf NumProceso = "2700" Then
                xpos2700 = xposicion
                currentStatus2700 = estatus
                totalProgress_2700 = totalprogreso
                'lbPorDT2700.Text = CalcularPorcentaje(lbDT2700.Text, lbOT2700.Text)
                'lbPorDT2700.Text = CalcularPorcentajeTurno1("2700")

                lbPorDT2700.Text = calcularDownPercentage("2700") & "%"
                lbPorRT2700.Text = calcularRunPercentage("2700") & "%"

            ElseIf NumProceso = "3100" Then
                xpos3100 = xposicion
                currentStatus3100 = estatus
                totalProgress_3100 = totalprogreso
                'lbPorDT3100.Text = CalcularPorcentaje(lbDT3100.Text, lbOT3100.Text)
                'lbPorDT3100.Text = CalcularPorcentajeTurno1("3100")
                'lbPorRT3100.Text = Math.Round(100 - CDbl(lbPorDT3100.Text), 1) & "%"
                ''lbPorRT3100.Text = CalcularPorcentaje(lbRT3100.Text)
                ''lbPorOT3100.Text = CalcularPorcentaje(lbOT3100.Text)
                'lbPorDT3100.Text &= "%"

                lbPorDT3100.Text = calcularDownPercentage("3100") & "%"
                lbPorRT3100.Text = calcularRunPercentage("3100") & "%"
            ElseIf NumProceso = "3400" Then
                xpos3400 = xposicion
                currentStatus3400 = estatus
                totalProgress_3400 = totalprogreso
                'lbPorDT3400.Text = CalcularPorcentaje(lbDT3400.Text, lbOT3400.Text)
                'lbPorDT3400.Text = CalcularPorcentajeTurno1("3400")
                'lbPorRT3400.Text = Math.Round(100 - CDbl(lbPorDT3400.Text), 1) & "%"
                ''lbPorRT3400.Text = CalcularPorcentaje(lbRT3400.Text)
                ''lbPorOt3400.Text = CalcularPorcentaje(lbOT3400.Text)
                'lbPorDT3400.Text &= "%"

                lbPorDT3400.Text = calcularDownPercentage("3400") & "%"
                lbPorRT3400.Text = calcularRunPercentage("3400") & "%"
            ElseIf NumProceso = "3700" Then
                xpos3700 = xposicion
                currentStatus3700 = estatus
                totalProgress_3700 = totalprogreso
                'lbPorDT3700.Text = CalcularPorcentaje(lbDT3700.Text, lbOT3700.Text)
                'lbPorDT3700.Text = CalcularPorcentajeTurno1("3700")
                'lbPorRt3700.Text = Math.Round(100 - CDbl(lbPorDT3700.Text), 1) & "%"
                ''lbPorRt3700.Text = CalcularPorcentaje(lbRT3700.Text)
                ''lbPorOT3700.Text = CalcularPorcentaje(lbOT3700.Text)
                'lbPorDT3700.Text &= "%"

                lbPorDT3700.Text = calcularDownPercentage("3700") & "%"
                lbPorRt3700.Text = calcularRunPercentage("3700") & "%"
            ElseIf NumProceso = "4100" Then
                xpos4100 = xposicion
                currentStatus4100 = estatus
                totalProgress_4100 = totalprogreso
                ' lbPorDT4100.Text = CalcularPorcentaje(lbDT4100.Text, lbOT4100.Text)
                lbPorDT4100.Text = CalcularPorcentajeTurno1("4100")
                lbPorRt4100.Text = Math.Round(100 - CDbl(lbPorDT4100.Text), 1) & "%"
                'lbPorRt4100.Text = CalcularPorcentaje(lbRT4100.Text)
                'lbPorOT4100.Text = CalcularPorcentaje(lbOT4100.Text)
                lbPorDT4100.Text &= "%"
            ElseIf NumProceso = "4400" Then
                xpos4400 = xposicion
                currentStatus4400 = estatus
                totalProgress_4400 = totalprogreso
                ' lbporDT4400.Text = CalcularPorcentaje(lbDT4400.Text, lbOT4400.Text)
                lbporDT4400.Text = CalcularPorcentajeTurno1("4400")
                lbPorRt4400.Text = Math.Round(100 - CDbl(lbporDT4400.Text), 1) & "%"
                'lbPorRt4400.Text = CalcularPorcentaje(lbRt4400.Text)
                'lbPorOT4400.Text = CalcularPorcentaje(lbOT4400.Text)
                lbporDT4400.Text &= "%"
            ElseIf NumProceso = "4700" Then
                xpos4700 = xposicion
                currentStatus4700 = estatus
                totalProgress_4700 = totalprogreso
                'lbPorDT4700.Text = CalcularPorcentaje(lbDT4700.Text, lbOT4700.Text)
                lbPorDT4700.Text = CalcularPorcentajeTurno1("4700")
                lbPorRT4700.Text = Math.Round(100 - CDbl(lbPorDT4700.Text), 1) & "%"
                'lbPorRT4700.Text = CalcularPorcentaje(lbRT4700.Text)
                'lbPorOT4700.Text = CalcularPorcentaje(lbOT4700.Text)
                lbPorDT4700.Text &= "%"
            ElseIf NumProceso = "5100" Then
                xpos5100 = xposicion
                currentStatus5100 = estatus
                totalProgress_5100 = totalprogreso
                'lbPorDT5100.Text = CalcularPorcentaje(lbDT5100.Text, lbOT5100.Text)
                lbPorDT5100.Text = CalcularPorcentajeTurno1("5100")
                lbPOrRT5100.Text = Math.Round(100 - CDbl(lbPorDT5100.Text), 1) & "%"
                'lbPOrRT5100.Text = CalcularPorcentaje(lbRT5100.Text)
                'lbPorOT5100.Text = CalcularPorcentaje(lbOT5100.Text)
                lbPorDT5100.Text &= "%"
            ElseIf NumProceso = "5400" Then
                xpos5400 = xposicion
                currentStatus5400 = estatus
                totalProgress_5400 = totalprogreso

                'lbPorDT5400.Text = CalcularPorcentaje(lbDT5400.Text, lbOT5400.Text)
                lbPorDT5400.Text = CalcularPorcentajeTurno1("5400")
                lbPORRT5400.Text = Math.Round(100 - CDbl(lbPorDT5400.Text), 1) & "%"
                'lbPORRT5400.Text = CalcularPorcentaje(lbRT5400.Text)
                'lbPorOT5400.Text = CalcularPorcentaje(lbOT5400.Text)
                lbPorDT5400.Text &= "%"
            ElseIf NumProceso = "5700" Then
                xpos5700 = xposicion
                currentStatus5700 = estatus
                totalProgress_5700 = totalprogreso
                'lbPorDT5700.Text = CalcularPorcentaje(lbDT5700.Text, lbOt5700.Text)
                lbPorDT5700.Text = CalcularPorcentajeTurno1("5700")
                lbPorRT5700.Text = Math.Round(100 - CDbl(lbPorDT5700.Text), 1) & "%"
                'lbPorRT5700.Text = CalcularPorcentaje(lbRT5700.Text)
                'lbPorOt5700.Text = CalcularPorcentaje(lbOt5700.Text)
                lbPorDT5700.Text &= "%"
            End If

            ShowTotalPerHours(NumProceso)

            Console.WriteLine("")
        Catch ex As Exception
            SaveFileAs("errores.log", ex.Message & "---- Sender: ProgresoGuardado() ------" & DateTime.Now.ToString.ToString, True)
            'clear previous requests
            ' System.Diagnostics.Process.Start("errores.log")
        End Try
    End Sub

    Public Function SaveFileAs(ByVal sFileName As String, ByVal text As String, ByVal sobreEscribir As Boolean) As Boolean
        Try
            Dim file As System.IO.StreamWriter
            file = My.Computer.FileSystem.OpenTextFileWriter(sFileName, sobreEscribir)
            file.WriteLine(text)
            file.Close()
            Return True
        Catch ex As Exception
            'MsgBox(ex.Message & vbCrLf & "sender : SaveFileAs(..", vbInformation, "Error")
            Return False
        End Try
    End Function

    Private Sub ShowTotalPerHours(proceso As String)
        Try
            Select Case proceso
                Case "1700"
                    queryComplement = " and t3.ProcessName='[ASSY] C/F Jig (최종검사/포장)' and t4.LineCode='LINE42'"
                Case "2700"
                    queryComplement = " and t3.ProcessName='[ASSY] C/F Jig (최종검사/포장)' and t4.LineCode='LINE42'"   ''LINE44'" temporary change M2 is running as M1
                Case "3700"
                    queryComplement = " and t3.ProcessName='[ASSY] C/F Jig (최종검사/포장)' and t4.LineCode='LINE44'"
                Case "4700"
                    queryComplement = " "
                Case "5700"
                    queryComplement = " "
            End Select

            Dim sqlCompl As String = ""

            If onlyShowShift1 = True Then
                sqlCompl = "	sum(convert(Int, T1.[00:00~01:00])) As [00:00~01:00],
		sum(convert(Int, T1.[07:00~08:00])) As [07:00~08:00],
		sum(convert(Int, T1.[08:00~09:00])) As [08:00~09:00],
		sum(convert(Int, T1.[09:00~10:00])) As [09:00~10:00],
		sum(convert(Int, T1.[10:00~11:00])) As [10:00~11:00],
		sum(convert(Int, T1.[11:00~12:00])) As [11:00~12:00],
		sum(convert(Int, T1.[12:00~13:00])) As [12:00~13:00],
		sum(convert(Int, T1.[13:00~14:00])) As [13:00~14:00],
		sum(convert(Int, T1.[14:00~15:00])) As [14:00~15:00],
		sum(convert(Int, T1.[15:00~16:00])) As [15:00~16:00],
		sum(convert(Int, T1.[16:00~17:00])) As [16:00~17:00]"
            Else
                sqlCompl = "	sum(convert(Int, T1.[00:00~01:00])) As [00:00~01:00],
		sum(convert(Int, T1.[01:00~02:00])) As [01:00~02:00],
		sum(convert(Int, T1.[02:00~03:00])) As [02:00~03:00],
		sum(convert(Int, T1.[03:00~04:00])) As [03:00~04:00],
		sum(convert(Int, T1.[04:00~05:00])) As [04:00~05:00],
		sum(convert(Int, T1.[05:00~06:00])) As [05:00~06:00],
		sum(convert(Int, T1.[06:00~07:00])) As [06:00~07:00],
		sum(convert(Int, T1.[07:00~08:00])) As [07:00~08:00],
		sum(convert(Int, T1.[08:00~09:00])) As [08:00~09:00],
		sum(convert(Int, T1.[09:00~10:00])) As [09:00~10:00],
		sum(convert(Int, T1.[10:00~11:00])) As [10:00~11:00],
		sum(convert(Int, T1.[11:00~12:00])) As [11:00~12:00],
		sum(convert(Int, T1.[12:00~13:00])) As [12:00~13:00],
		sum(convert(Int, T1.[13:00~14:00])) As [13:00~14:00],
		sum(convert(Int, T1.[14:00~15:00])) As [14:00~15:00],
		sum(convert(Int, T1.[15:00~16:00])) As [15:00~16:00],
		sum(convert(Int, T1.[16:00~17:00])) As [16:00~17:00],
	    sum(convert(Int, T1.[17:00~18:00])) As [17:00~18:00],
		sum(convert(Int, T1.[18:00~19:00])) As [18:00~19:00],
		sum(convert(Int, T1.[19:00~20:00])) As [19:00~20:00],
		sum(convert(Int, T1.[20:00~21:00])) As [20:00~21:00],
		sum(convert(Int, T1.[21:00~22:00])) As [21:00~22:00],
		sum(convert(Int, T1.[22:00~23:00])) As [22:00~23:00],
		sum(convert(Int, T1.[23:00~24:00])) As [23:00~24:00]"
            End If
            queryTotal = "------------------------------------------------------------------------------------------------------------------------------
DECLARE  @MesSiteCode INT = 11
		,@ResultDate NVARCHAR(10) = '" & ConvierteAdateMySQL(Date.Now.ToShortDateString) & "'
		,@ViewTypeCode Nvarchar(30) = 'ByMachine'


--검사 실적 테이블
DECLARE @InspectResult TABLE (
	MesSiteCode INT,
	TimeTable NVARCHAR(20),
	ProcessCode NVARCHAR(50),
	MachineCode NVARCHAR(50),
	LotNo NVARCHAR(20),
	SubPno NVARCHAR(20),  	
	QtyResult INT
)

--합격바코드 테이블
DECLARE @PassBarCodeTable TABLE
(
	[MesSiteCode] [int] NOT NULL,
	[ProcessCode] [nvarchar](20) NOT NULL,
	[LotNo] [nvarchar](20) NOT NULL,
	[SubPno] [nvarchar](50) NOT NULL,
	[MachineCode] [nvarchar](20) NOT NULL,
	[BarCode] [nvarchar](50) NOT NULL,
	Primary Key([MesSiteCode]
               ,[ProcessCode]
			   ,[LotNo]
			   ,[SubPno]
			   ,[MachineCode]
			   ,[BarCode])
)

--조회 결과 테이블
Declare @SearchResultTable Table (
	MesSiteCode INT,
	--TimeTable NVARCHAR(20),
	ProcessCode NVARCHAR(50),
	LotNo NVARCHAR(20),
	SubPno NVARCHAR(20),
	MachineCode NVARCHAR(50),
	[07:00~08:00] NVARCHAR(20),
	[08:00~09:00] NVARCHAR(20),
	[09:00~10:00] NVARCHAR(20),
	[10:00~11:00] NVARCHAR(20),
	[11:00~12:00] NVARCHAR(20),
	[12:00~13:00] NVARCHAR(20),
	[13:00~14:00] NVARCHAR(20),
	[14:00~15:00] NVARCHAR(20),
	[15:00~16:00] NVARCHAR(20),
	[16:00~17:00] NVARCHAR(20),
    [17:00~18:00] NVARCHAR(20),
	[18:00~19:00] NVARCHAR(20),
	[Shift1] NVARCHAR(20),	
	[19:00~20:00] NVARCHAR(20),
	[20:00~21:00] NVARCHAR(20),
	[21:00~22:00] NVARCHAR(20),
	[22:00~23:00] NVARCHAR(20),
	[23:00~24:00] NVARCHAR(20),
	[00:00~01:00] NVARCHAR(20),
	[01:00~02:00] NVARCHAR(20),
	[02:00~03:00] NVARCHAR(20),
	[03:00~04:00] NVARCHAR(20),
	[04:00~05:00] NVARCHAR(20),
	[05:00~06:00] NVARCHAR(20),
	[06:00~07:00] NVARCHAR(20),
	[Shift2] NVARCHAR(20),
	[QtyPass] NVARCHAR(20),
	[QtyFail] NVARCHAR(20)
)


--PCB 검사 실적 시간 별 수량
INSERT INTO @InspectResult (
	MesSiteCode, 
	TimeTable, 
	ProcessCode, 
	MachineCode, 
	LotNo,
	SubPno, 
	QtyResult)
SELECT 
	T1.[MesSiteCode],
	[dbo].[UF_GetShiftTimeTable](@MesSiteCode,T1.INSERT_DATE) AS [TimeTable],
	T1.[ProcessCode],
	T1.[MachineCode],
	T1.[LotNo],
	T1.[SubPno],
	COUNT(Distinct T1.[BarCode]) AS [QtyResult]
FROM [dbo].[TMES_PROCESSINSPECT_PCB] AS T1
Left Outer Join [dbo].[TMES_SAMPLECHECKBARCODE] T9
	On T9.[MesSiteCode] = T1.[MesSiteCode]
		And T9.[ProcessCode] = T1.[ProcessCode]
		And T9.[MachineCode] = T1.[MachineCode]
		And T9.[BarCode] = T1.[BarCode]
WHERE T1.[MesSiteCode] = @MesSiteCode
	AND T1.[ResultDate] = @ResultDate
	And T9.[BarCode] Is Null
	
	
	
	
	
	
	
GROUP BY
	T1.[MesSiteCode],
	[dbo].[UF_GetShiftTimeTable](@MesSiteCode,T1.[INSERT_DATE]) ,
	T1.[ProcessCode],
	T1.[MachineCode],
	T1.[LotNo],
	T1.[SubPno]             	


--PCB 검사 실적 Shift 별 수량
INSERT INTO @InspectResult (
	MesSiteCode, 
	TimeTable, 
	ProcessCode, 
	MachineCode, 
	LotNo,
	SubPno, 
	QtyResult)
SELECT 
	T1.[MesSiteCode],
	CASE WHEN
		LEFT( REPLACE( CONVERT(NVARCHAR(10),T1.[INSERT_DATE],108),  ':' ,'') ,4 ) BETWEEN '0700' AND '1900' THEN 'Shift1' 
		ELSE 'Shift2' END AS [TimeTable],
	T1.[ProcessCode],
	T1.[MachineCode],
	T1.[LotNo],
	T1.[SubPno],
	COUNT(Distinct T1.[BarCode]) AS [QtyResult]
FROM [dbo].[TMES_PROCESSINSPECT_PCB] AS T1
Left Outer Join [dbo].[TMES_SAMPLECHECKBARCODE] T9
	On T9.[MesSiteCode] = T1.[MesSiteCode]
		And T9.[ProcessCode] = T1.[ProcessCode]
		And T9.[MachineCode] = T1.[MachineCode]
		And T9.[BarCode] = T1.[BarCode]
WHERE T1.[MesSiteCode] = @MesSiteCode
	AND T1.[ResultDate] = @ResultDate
	And T9.[BarCode] Is Null
	
	
	
	
	
	
	
GROUP BY
	T1.[MesSiteCode],
	CASE WHEN
		LEFT( REPLACE( CONVERT(NVARCHAR(10),T1.[INSERT_DATE],108),  ':' ,'') ,4 ) BETWEEN '0700' AND '1900' THEN 'Shift1' 
		ELSE 'Shift2' END,
	T1.[ProcessCode],
	T1.[MachineCode],
	T1.[LotNo],
	T1.[SubPno]     


--PCB 합격 수량		namjh0831@kyungshin.co.kr
INSERT INTO @InspectResult(
	MesSiteCode, 
	TimeTable, 
	ProcessCode, 
	MachineCode,
	LotNo, 
	SubPno, 
	QtyResult)
SELECT 
	T1.[MesSiteCode],
	'QtyPass' AS [TimeTable],
	T1.[ProcessCode],
	T1.[MachineCode],
	T1.[LotNo],
	T1.[SubPno],
	COUNT(Distinct T1.[BarCode]) AS [QtyResult]
FROM [dbo].[TMES_PROCESSINSPECT_PCB] AS T1
Left Outer Join [dbo].[TMES_SAMPLECHECKBARCODE] T9
	On T9.[MesSiteCode] = T1.[MesSiteCode]
		And T9.[ProcessCode] = T1.[ProcessCode]
		And T9.[MachineCode] = T1.[MachineCode]
		And T9.[BarCode] = T1.[BarCode]
WHERE T1.[MesSiteCode] = @MesSiteCode
	AND T1.[ResultDate] = @ResultDate
	And T1.[InspectResult] = 'PASS'
	And T9.[BarCode] Is Null
	
	
	
	
	
	
	
GROUP BY
	T1.[MesSiteCode],
	T1.[ProcessCode],
	T1.[MachineCode],
	T1.[LotNo],
	T1.[SubPno]  
	

--PCB 합격 바코드
INSERT @PassBarCodeTable 
SELECT 
	T1.[MesSiteCode],
	T1.[ProcessCode],
	T1.[LotNo],
	T1.[SubPno],
	T1.[MachineCode],
	T1.[BarCode]
FROM [dbo].[TMES_PROCESSINSPECT_PCB] T1
WHERE T1.[MesSiteCode] = @MesSiteCode
	AND T1.[ResultDate] = @ResultDate
	AND T1.[InspectResult] = 'PASS'
	
	
	
	
	
	
	
GROUP BY 
	T1.[MesSiteCode], 
	T1.[ProcessCode], 
	T1.[LotNo], 
	T1.[SubPno], 
	T1.[MachineCode], 
	T1.[BarCode]


--PCB 불합격 수량, 불합격 후 합격된 바코드는 제외		namjh0831@kyungshin.co.kr
INSERT INTO @InspectResult(
	MesSiteCode, 
	TimeTable, 
	ProcessCode, 
	MachineCode,
	LotNo, 
	SubPno, 
	QtyResult)
SELECT 
	T1.[MesSiteCode],
	'QtyFail' AS [TimeTable],
	T1.[ProcessCode],
	T1.[MachineCode],
	T1.[LotNo],
	T1.[SubPno],
	COUNT(Distinct T1.[BarCode]) AS [QtyResult]
FROM [dbo].[TMES_PROCESSINSPECT_PCB] AS T1
Left Outer Join @PassBarCodeTable As T2
	On T2.[MesSiteCode] = T1.[MesSiteCode]
		And T2.[ProcessCode] = T1.[ProcessCode]
		And T2.[LotNo] = T1.[LotNo]
		And T2.[SubPno] = T1.[SubPno]
		And T2.[MachineCode] = T1.[MachineCode]
		And T2.[BarCode] = T1.[BarCode]
Left Outer Join [dbo].[TMES_SAMPLECHECKBARCODE] T9
	On T9.[MesSiteCode] = T1.[MesSiteCode]
		And T9.[ProcessCode] = T1.[ProcessCode]
		And T9.[MachineCode] = T1.[MachineCode]
		And T9.[BarCode] = T1.[BarCode]
WHERE T1.[MesSiteCode] = @MesSiteCode
	AND T1.[ResultDate] = @ResultDate
	And T1.[InspectResult] = 'FAIL'
	And T2.[BarCode] Is Null
	And T9.[BarCode] Is Null
	
	
	
	
	
	
	
GROUP BY
	T1.[MesSiteCode],
	T1.[ProcessCode],
	T1.[MachineCode],
	T1.[LotNo],
	T1.[SubPno] 




--ASSY 검사 실적 시간별 수량
INSERT INTO @InspectResult(
	MesSiteCode, 
	TimeTable, 
	ProcessCode, 
	MachineCode,
	LotNo, 
	SubPno, 
	QtyResult)
SELECT 
	T1.[MesSiteCode],
	dbo.UF_GetShiftTimeTable(@MesSiteCode,T1.[INSERT_DATE]) AS [TimeTable],
	T1.[ProcessCode],
	T1.[MachineCode],
	T1.[LotNo],
	T1.[SubPno],
	COUNT(Distinct T1.[BarCode]) AS [QtyResult]
FROM [dbo].[TMES_PROCESSINSPECT_ASSY] AS T1
Left Outer Join [dbo].[TMES_SAMPLECHECKBARCODE] T9
	On T9.[MesSiteCode] = T1.[MesSiteCode]
		And T9.[ProcessCode] = T1.[ProcessCode]
		And T9.[MachineCode] = T1.[MachineCode]
		And T9.[BarCode] = T1.[BarCode]
WHERE T1.[MesSiteCode] = @MesSiteCode
	AND T1.[ResultDate] = @ResultDate
	And T9.[BarCode] Is Null
	
	
	
	
	
	
	
GROUP BY
	T1.[MesSiteCode],
	[dbo].[UF_GetShiftTimeTable](@MesSiteCode,T1.[INSERT_DATE]) ,
	T1.[ProcessCode],
	T1.[MachineCode],
	T1.[LotNo],
	T1.[SubPno]      


--ASSY 검사 실적 Shift 별 수량
INSERT INTO @InspectResult(
	MesSiteCode, 
	TimeTable, 
	ProcessCode, 
	MachineCode,
	LotNo, 
	SubPno, 
	QtyResult)
SELECT 
	T1.[MesSiteCode],
	CASE WHEN
	LEFT( REPLACE( CONVERT(NVARCHAR(10),T1.[INSERT_DATE],108),  ':' ,'') ,4 ) BETWEEN '0700' AND '1900' THEN 'Shift1' ELSE 'Shift2' END AS [TimeTable],
	T1.[ProcessCode],
	T1.[MachineCode],
	T1.[LotNo],
	T1.[SubPno],
	COUNT(Distinct T1.[BarCode]) AS [QtyResult]
FROM [dbo].[TMES_PROCESSINSPECT_ASSY] AS T1
Left Outer Join [dbo].[TMES_SAMPLECHECKBARCODE] T9
	On T9.[MesSiteCode] = T1.[MesSiteCode]
		And T9.[ProcessCode] = T1.[ProcessCode]
		And T9.[MachineCode] = T1.[MachineCode]
		And T9.[BarCode] = T1.[BarCode]
WHERE T1.[MesSiteCode] = @MesSiteCode
	AND T1.[ResultDate] = @ResultDate
	And T9.[BarCode] Is Null
	
	
	
	
	
	
	
GROUP BY
	T1.[MesSiteCode],
	CASE WHEN
	LEFT( REPLACE( CONVERT(NVARCHAR(10),T1.[INSERT_DATE],108),  ':' ,'') ,4 ) BETWEEN '0700' AND '1900' THEN 'Shift1' ELSE 'Shift2' END,
	T1.[ProcessCode],
	T1.[MachineCode],
	T1.[LotNo],
	T1.[SubPno]  


--ASSY 합격 수량		namjh0831@kyungshin.co.kr
INSERT INTO @InspectResult(
	MesSiteCode, 
	TimeTable, 
	ProcessCode, 
	MachineCode,
	LotNo, 
	SubPno, 
	QtyResult)
SELECT 
	T1.[MesSiteCode],
	'QtyPass' AS [TimeTable],
	T1.[ProcessCode],
	T1.[MachineCode],
	T1.[LotNo],
	T1.[SubPno],
	COUNT(Distinct T1.[BarCode]) AS [QtyResult]
FROM [dbo].[TMES_PROCESSINSPECT_ASSY] AS T1
Left Outer Join [dbo].[TMES_SAMPLECHECKBARCODE] T9
	On T9.[MesSiteCode] = T1.[MesSiteCode]
		And T9.[ProcessCode] = T1.[ProcessCode]
		And T9.[MachineCode] = T1.[MachineCode]
		And T9.[BarCode] = T1.[BarCode]
WHERE T1.[MesSiteCode] = @MesSiteCode
	AND T1.[ResultDate] = @ResultDate
	And T1.[InspectResult] = 'PASS'
	And T9.[BarCode] Is Null
	
	
	
	
	
	
	
GROUP BY
	T1.[MesSiteCode],
	T1.[ProcessCode],
	T1.[MachineCode],
	T1.[LotNo],
	T1.[SubPno]  


--ASSY 합격 바코드
INSERT @PassBarCodeTable 
SELECT 
	T1.[MesSiteCode],
	T1.[ProcessCode],
	T1.[LotNo],
	T1.[SubPno],
	T1.[MachineCode],
	T1.[BarCode]
FROM [dbo].[TMES_PROCESSINSPECT_ASSY] AS T1
WHERE T1.[MesSiteCode] = @MesSiteCode
	AND T1.[ResultDate] = @ResultDate
	AND T1.[InspectResult] = 'PASS'
	
	
	
	
	
	
	
GROUP BY 
	T1.[MesSiteCode],
	T1.[ProcessCode],
	T1.[LotNo],
	T1.[SubPno], 
	T1.[MachineCode], 
	T1.[BarCode]


--ASSY 불합격 수량, 불합격 후 합격된 바코드는 제외		namjh0831@kyungshin.co.kr
INSERT INTO @InspectResult(
	MesSiteCode, 
	TimeTable, 
	ProcessCode, 
	MachineCode,
	LotNo, 
	SubPno, 
	QtyResult)
SELECT 
	T1.[MesSiteCode],
	'QtyFail' AS [TimeTable],
	T1.[ProcessCode],
	T1.[MachineCode],
	T1.[LotNo],
	T1.[SubPno],
	COUNT(Distinct T1.[BarCode]) AS [QtyResult]
FROM [dbo].[TMES_PROCESSINSPECT_ASSY] AS T1
Left Outer Join @PassBarCodeTable As T2
	On T2.[MesSiteCode] = T1.[MesSiteCode]
		And T2.[ProcessCode] = T1.[ProcessCode]
		And T2.[LotNo] = T1.[LotNo]
		And T2.[SubPno] = T1.[SubPno]
		And T2.[MachineCode] = T1.[MachineCode]
		And T2.[BarCode] = T1.[BarCode]
Left Outer Join [dbo].[TMES_SAMPLECHECKBARCODE] T9
	On T9.[MesSiteCode] = T1.[MesSiteCode]
		And T9.[ProcessCode] = T1.[ProcessCode]
		And T9.[MachineCode] = T1.[MachineCode]
		And T9.[BarCode] = T1.[BarCode]
WHERE T1.[MesSiteCode] = @MesSiteCode
	AND T1.[ResultDate] = @ResultDate
	And T1.[InspectResult] = 'FAIL'
	And T2.[BarCode] Is Null
	And T9.[BarCode] Is Null
	
	
	
	
	
	
	
GROUP BY
	T1.[MesSiteCode],
	T1.[ProcessCode],
	T1.[MachineCode],
	T1.[LotNo],
	T1.[SubPno] 


--[TimeTable] 행렬 변환
Insert into @SearchResultTable
	SELECT * FROM (
		Select 
			T1.[MesSiteCode],
			T1.[TimeTable],
			T1.[ProcessCode],
			T1.[LotNo],
			T1.[SubPno],
			T1.[MachineCode],
			T1.[QtyResult]
		FROM @InspectResult AS T1
					) AS T1
		PIVOT ( SUM(QtyResult) FOR [TimeTable] IN ( 
				[07:00~08:00],
				[08:00~09:00],
				[09:00~10:00],
				[10:00~11:00],
				[11:00~12:00],
				[12:00~13:00],
				[13:00~14:00],
				[14:00~15:00],
				[15:00~16:00],
				[16:00~17:00],
                [17:00~18:00],
				[18:00~19:00],
				[Shift1],				
				[19:00~20:00],
				[20:00~21:00],
				[21:00~22:00],
				[22:00~23:00],
				[23:00~24:00],
				[00:00~01:00],
				[01:00~02:00],
				[02:00~03:00],
				[03:00~04:00],
				[04:00~05:00],
				[05:00~06:00],
				[06:00~07:00],
				[Shift2],
				[QtyPass],
				[QtyFail]
				)) AS T2


--상세히
If(@ViewTypeCode = 'Detail')
Begin
	Select
		T1.[MesSiteCode],
        T2.[MesSiteName],
		T3.[ProcessKindName],
		T1.[ProcessCode],
		T3.[ProcessName],
	    T5.[CarName],
	    T5.[ItemName], 
		T1.[LotNo],
		T1.[SubPno],
		T4.[LineCode], 
		T1.[MachineCode],
		T4.[MachineName],
		convert(Int, T1.[07:00~08:00]) As [07:00~08:00],
		convert(Int, T1.[08:00~09:00]) As [08:00~09:00],
		convert(Int, T1.[09:00~10:00]) As [09:00~10:00],
		convert(Int, T1.[10:00~11:00]) As [10:00~11:00],
		convert(Int, T1.[11:00~12:00]) As [11:00~12:00],
		convert(Int, T1.[12:00~13:00]) As [12:00~13:00],
		convert(Int, T1.[13:00~14:00]) As [13:00~14:00],
		convert(Int, T1.[14:00~15:00]) As [14:00~15:00],
		convert(Int, T1.[15:00~16:00]) As [15:00~16:00],
		convert(Int, T1.[16:00~17:00]) As [16:00~17:00],
	    convert(Int, T1.[17:00~18:00]) As [17:00~18:00],
		convert(Int, T1.[18:00~19:00]) As [18:00~19:00],
		convert(Int, T1.[Shift1]) As [Shift1],	
		convert(Int, T1.[19:00~20:00]) As [19:00~20:00],
		convert(Int, T1.[20:00~21:00]) As [20:00~21:00],
		convert(Int, T1.[21:00~22:00]) As [21:00~22:00],
		convert(Int, T1.[22:00~23:00]) As [22:00~23:00],
		convert(Int, T1.[23:00~24:00]) As [23:00~24:00],
		convert(Int, T1.[00:00~01:00]) As [00:00~01:00],
		convert(Int, T1.[01:00~02:00]) As [01:00~02:00],
		convert(Int, T1.[02:00~03:00]) As [02:00~03:00],
		convert(Int, T1.[03:00~04:00]) As [03:00~04:00],
		convert(Int, T1.[04:00~05:00]) As [04:00~05:00],
		convert(Int, T1.[05:00~06:00]) As [05:00~06:00],
		convert(Int, T1.[06:00~07:00]) As [06:00~07:00],
		convert(Int, T1.[Shift2]) As [Shift2],
		convert(Int, T1.[QtyPass]) As [QtyPass],
		convert(Int, T1.[QtyFail]) As [QtyFail]
	From @SearchResultTable T1
	Left Outer Join [dbo].[TMES_CODESITE] T2
			On T2.[MesSiteCode] = T1.[MesSiteCode]
	Left Outer Join [dbo].[TMES_PROCESS] T3
		On T3.[MesSiteCode] = T1.[MesSiteCode]
			And T3.[ProcessCode] = T1.[ProcessCode]
	Left Outer Join [dbo].[TMES_MACHINEMASTER] T4
		On T4.[MesSiteCode] = T1.[MesSiteCode]
			And T4.[MachineCode] = T1.[MachineCode]
	Outer Apply [dbo].[UF_GetProductInfoBySubPno](@MesSiteCode, T1.[SubPno]) T5
	Where 1=1
		
		
		
	Order By T1.[ProcessCode], T1.[MachineCode], T1.[SubPno], T1.[LotNo]
End
--로트별
Else If(@ViewTypeCode = 'ByLotNo') 
Begin
	SELECT 
		T1.[MesSiteCode],
        T2.[MesSiteName],
		T3.[ProcessKindName],
		T1.[ProcessCode],
		T3.[ProcessName],
		MAX(T5.[CarName]) AS [CarName],
	    MAX(T5.[ItemName]) AS [ItemName],
		T1.[LotNo],
		--T1.[SubPno],
		T4.[LineCode], 
		T1.[MachineCode],
		T4.[MachineName],
		sum(convert(Int, T1.[07:00~08:00])) As [07:00~08:00],
		sum(convert(Int, T1.[08:00~09:00])) As [08:00~09:00],
		sum(convert(Int, T1.[09:00~10:00])) As [09:00~10:00],
		sum(convert(Int, T1.[10:00~11:00])) As [10:00~11:00],
		sum(convert(Int, T1.[11:00~12:00])) As [11:00~12:00],
		sum(convert(Int, T1.[12:00~13:00])) As [12:00~13:00],
		sum(convert(Int, T1.[13:00~14:00])) As [13:00~14:00],
		sum(convert(Int, T1.[14:00~15:00])) As [14:00~15:00],
		sum(convert(Int, T1.[15:00~16:00])) As [15:00~16:00],
		sum(convert(Int, T1.[16:00~17:00])) As [16:00~17:00],
	    sum(convert(Int, T1.[17:00~18:00])) As [17:00~18:00],
		sum(convert(Int, T1.[18:00~19:00])) As [18:00~19:00],
		sum(convert(Int, T1.[Shift1])) As [Shift1],	
		sum(convert(Int, T1.[19:00~20:00])) As [19:00~20:00],
		sum(convert(Int, T1.[20:00~21:00])) As [20:00~21:00],
		sum(convert(Int, T1.[21:00~22:00])) As [21:00~22:00],
		sum(convert(Int, T1.[22:00~23:00])) As [22:00~23:00],
		sum(convert(Int, T1.[23:00~24:00])) As [23:00~24:00],
		sum(convert(Int, T1.[00:00~01:00])) As [00:00~01:00],
		sum(convert(Int, T1.[01:00~02:00])) As [01:00~02:00],
		sum(convert(Int, T1.[02:00~03:00])) As [02:00~03:00],
		sum(convert(Int, T1.[03:00~04:00])) As [03:00~04:00],
		sum(convert(Int, T1.[04:00~05:00])) As [04:00~05:00],
		sum(convert(Int, T1.[05:00~06:00])) As [05:00~06:00],
		sum(convert(Int, T1.[06:00~07:00])) As [06:00~07:00],
		sum(convert(Int, T1.[Shift2])) As [Shift2],
		sum(convert(Int, T1.[QtyPass])) As [QtyPass],
		sum(convert(Int, T1.[QtyFail])) As [QtyFail]
	From @SearchResultTable T1
	Left Outer Join [dbo].[TMES_CODESITE] T2
			On T2.[MesSiteCode] = T1.[MesSiteCode]
	Left Outer Join [dbo].[TMES_PROCESS] T3
		On T3.[MesSiteCode] = T1.[MesSiteCode]
			And T3.[ProcessCode] = T1.[ProcessCode]
	Left Outer Join [dbo].[TMES_MACHINEMASTER] T4
		On T4.[MesSiteCode] = T1.[MesSiteCode]
			And T4.[MachineCode] = T1.[MachineCode]
	Outer Apply [dbo].[UF_GetProductInfoBySubPno](@MesSiteCode, T1.[SubPno]) T5
	Where 1=1
		
		
		
    GROUP BY 
		T1.[MesSiteCode],
        T2.[MesSiteName],
		T3.[ProcessKindName],
		T1.[ProcessCode],
		T3.[ProcessName],
        T1.[LotNo],
		T4.[LineCode], 
		T1.[MachineCode],
		T4.[MachineName]
	Order By T1.[ProcessCode], T1.[MachineCode], T1.[LotNo]
End
--품번별
Else If(@ViewTypeCode = 'BySubPno')
Begin
	SELECT 
		T1.[MesSiteCode],
        T2.[MesSiteName],
		T3.[ProcessKindName],
		T1.[ProcessCode],
		T3.[ProcessName],
		MAX(T5.[CarName]) AS [CarName],
	    MAX(T5.[ItemName]) AS [ItemName],
		--T1.[LotNo],
		T1.[SubPno],
		T4.[LineCode], 
		T1.[MachineCode],
		T4.[MachineName],
		sum(convert(Int, T1.[07:00~08:00])) As [07:00~08:00],
		sum(convert(Int, T1.[08:00~09:00])) As [08:00~09:00],
		sum(convert(Int, T1.[09:00~10:00])) As [09:00~10:00],
		sum(convert(Int, T1.[10:00~11:00])) As [10:00~11:00],
		sum(convert(Int, T1.[11:00~12:00])) As [11:00~12:00],
		sum(convert(Int, T1.[12:00~13:00])) As [12:00~13:00],
		sum(convert(Int, T1.[13:00~14:00])) As [13:00~14:00],
		sum(convert(Int, T1.[14:00~15:00])) As [14:00~15:00],
		sum(convert(Int, T1.[15:00~16:00])) As [15:00~16:00],
		sum(convert(Int, T1.[16:00~17:00])) As [16:00~17:00],
        sum(convert(Int, T1.[17:00~18:00])) As [17:00~18:00],
		sum(convert(Int, T1.[18:00~19:00])) As [18:00~19:00],
		sum(convert(Int, T1.[Shift1])) As [Shift1],		
		sum(convert(Int, T1.[19:00~20:00])) As [19:00~20:00],
		sum(convert(Int, T1.[20:00~21:00])) As [20:00~21:00],
		sum(convert(Int, T1.[21:00~22:00])) As [21:00~22:00],
		sum(convert(Int, T1.[22:00~23:00])) As [22:00~23:00],
		sum(convert(Int, T1.[23:00~24:00])) As [23:00~24:00],
		sum(convert(Int, T1.[00:00~01:00])) As [00:00~01:00],
		sum(convert(Int, T1.[01:00~02:00])) As [01:00~02:00],
		sum(convert(Int, T1.[02:00~03:00])) As [02:00~03:00],
		sum(convert(Int, T1.[03:00~04:00])) As [03:00~04:00],
		sum(convert(Int, T1.[04:00~05:00])) As [04:00~05:00],
		sum(convert(Int, T1.[05:00~06:00])) As [05:00~06:00],
		sum(convert(Int, T1.[06:00~07:00])) As [06:00~07:00],
		sum(convert(Int, T1.[Shift2])) As [Shift2],
		sum(convert(Int, T1.[QtyPass])) As [QtyPass],
		sum(convert(Int, T1.[QtyFail])) As [QtyFail]
	From @SearchResultTable T1
	Left Outer Join [dbo].[TMES_CODESITE] T2
			On T2.[MesSiteCode] = T1.[MesSiteCode]
	Left Outer Join [dbo].[TMES_PROCESS] T3
		On T3.[MesSiteCode] = T1.[MesSiteCode]
			And T3.[ProcessCode] = T1.[ProcessCode]
	Left Outer Join [dbo].[TMES_MACHINEMASTER] T4
		On T4.[MesSiteCode] = T1.[MesSiteCode]
			And T4.[MachineCode] = T1.[MachineCode]
	Outer Apply [dbo].[UF_GetProductInfoBySubPno](@MesSiteCode, T1.[SubPno]) T5
	Where 1=1
		
		
		
    GROUP BY 
		T1.[MesSiteCode],
        T2.[MesSiteName],
		T3.[ProcessKindName],
		T1.[ProcessCode],
		T3.[ProcessName],
        T1.[SubPno],
		T4.[LineCode], 
		T1.[MachineCode],
		T4.[MachineName]
	Order By T1.[ProcessCode], T1.[MachineCode], T1.[SubPno]
End
--설비별
Else If(@ViewTypeCode = 'ByMachine')
Begin
	SELECT 
		concat('Qty/Pass: ', sum(convert(Int, T1.[QtyPass]))) As [QtyPass],
		" & sqlCompl & "
	From @SearchResultTable T1
	Left Outer Join [dbo].[TMES_CODESITE] T2
			On T2.[MesSiteCode] = T1.[MesSiteCode]
	Left Outer Join [dbo].[TMES_PROCESS] T3
		On T3.[MesSiteCode] = T1.[MesSiteCode]
			And T3.[ProcessCode] = T1.[ProcessCode]
	Left Outer Join [dbo].[TMES_MACHINEMASTER] T4
		On T4.[MesSiteCode] = T1.[MesSiteCode]
			And T4.[MachineCode] = T1.[MachineCode]
	Outer Apply [dbo].[UF_GetProductInfoBySubPno](@MesSiteCode, T1.[SubPno]) T5
	Where 1=1 " & queryComplement & "
		
		
		
    GROUP BY 
		T1.[MesSiteCode],
        T2.[MesSiteName],
		T3.[ProcessKindName],
		T1.[ProcessCode],
		T3.[ProcessName],
		T4.[LineCode], 
		T1.[MachineCode],
		T4.[MachineName]
	Order By T1.[ProcessCode], T1.[MachineCode]
End
------------------------------------------------------------------------------------------------------------------------------"

            dtQty = EjecutaSelectsEISS(queryTotal, "fillDTQTY")


            Select Case proceso
                Case "1700"
                    If onlyShowShift1 = True Then
                        dgvm1shift1.Rows.Clear()
                        dgvToFill = dgvm1shift1
                    Else
                        dgvm1shift1.Visible = False
                        dgv1700.Rows.Clear()
                        dgvToFill = dgv1700
                    End If

                Case "2700"
                    If onlyShowShift1 = True Then
                        dgvm2Shift1.Rows.Clear()
                        dgvToFill = dgvm2Shift1
                    Else
                        dgvm2Shift1.Visible = False
                        dgv2700.Rows.Clear()
                        dgvToFill = dgv2700
                    End If

                Case "3700"
                    If onlyShowShift1 = True Then
                        dgvM3shift1.Rows.Clear()
                        dgvToFill = dgvM3shift1
                    Else
                        dgvM3shift1.Visible = False
                        dgv3700.Rows.Clear()
                        dgvToFill = dgv3700
                    End If

                Case "4700"
                    If onlyShowShift1 = True Then

                    Else
                        dgv4700.Rows.Clear()
                        dgvToFill = dgv4700
                    End If

                    Exit Sub
                Case "5700"
                    If onlyShowShift1 = True Then

                    Else
                        dgv5700.Rows.Clear()
                        dgvToFill = dgv5700
                    End If

                    Exit Sub
                Case Else
                    Exit Sub
            End Select

            For Each rw As DataRow In dtQty.Rows
                If onlyShowShift1 = True Then
                    dgvToFill.Rows.Add(rw("QtyPass").ToString,
                                 rw("07:00~08:00").ToString,
                                rw("08:00~09:00").ToString,
                                rw("09:00~10:00").ToString,
                                rw("10:00~11:00").ToString,
                                rw("11:00~12:00").ToString,
                                rw("12:00~13:00").ToString,
                                rw("13:00~14:00").ToString,
                                rw("14:00~15:00").ToString,
                                rw("15:00~16:00").ToString,
                                rw("16:00~17:00").ToString)
                Else
                    dgvToFill.Rows.Add(rw("QtyPass").ToString,
                                rw("00:00~01:00").ToString,
                                rw("01:00~02:00").ToString,
                                rw("02:00~03:00").ToString,
                                rw("03:00~04:00").ToString,
                                rw("04:00~05:00").ToString,
                                rw("05:00~06:00").ToString,
                                rw("06:00~07:00").ToString,
                                rw("07:00~08:00").ToString,
                                rw("08:00~09:00").ToString,
                                rw("09:00~10:00").ToString,
                                rw("10:00~11:00").ToString,
                                rw("11:00~12:00").ToString,
                                rw("12:00~13:00").ToString,
                                rw("13:00~14:00").ToString,
                                rw("14:00~15:00").ToString,
                                rw("15:00~16:00").ToString,
                                rw("16:00~17:00").ToString,
                                rw("17:00~18:00").ToString,
                                rw("18:00~19:00").ToString,
                                rw("19:00~20:00").ToString,
                                rw("20:00~21:00").ToString,
                                rw("21:00~22:00").ToString,
                                rw("22:00~23:00").ToString,
                                rw("23:00~24:00").ToString)
                End If


            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Function EjecutaSelectsEISS(ByVal sSQL As String, ByVal sender As String)
        Dim cmd As SqlCommand = Nothing
        Try
            cmd = New SqlCommand(sSQL, conEISS)
            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            Return dt
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Return sender
    End Function

    Private conEISS As SqlConnection = Nothing
    Function checkEISSconection() As Boolean
        Try
            conEISS = New SqlConnection()
            conEISS.ConnectionString = "Data Source=172.30.64.15;Initial Catalog=EIMES_KSGP; User ID=PlantSupport; Password=PlantSupport1120"
            conEISS.Open()

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function
End Class


