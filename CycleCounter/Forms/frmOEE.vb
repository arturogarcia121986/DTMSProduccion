Imports System.Data.SqlClient

Public Class frmOEE

    Private conHISS As SqlConnection = Nothing
    Dim query As String = ""
    Private Sub frmOEE_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpDesde.Value = Date.Now.ToShortDateString
        checkHISSconection()
    End Sub

    Function checkHISSconection() As Boolean
        Try
            conHISS = New SqlConnection()
            conHISS.ConnectionString = "Data Source=172.30.64.15;Initial Catalog=EIMES_KSGP; User ID=PlantSupport; Password=PlantSupport1120"
            conHISS.Open()

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub bntFiltrar_Click(sender As Object, e As EventArgs) Handles bntFiltrar.Click
        Try
            dgv.Rows.Clear()
            dgvOEE.Rows.Clear()

            Dim fechaCalculo As String = ConvierteAdateMySQL(dtpDesde.Value.ToShortDateString)
            query = "WITH BreaktimeData AS (
    SELECT
        line,
        startup1,
        startup1Fin,
        break1,
        break1fin,
        lunch1,
        lunch1fin,
        break2,
        break2fin,
        startup2,
        startup2fin,
        lunch2,
        lunch2fin,
        break3,
        break3fin,
        break4,
        break4fin
    FROM [db_kyungshin].[dbo].[t_bma_breaktime]
),
ShiftBreaks AS (
    SELECT
        line,
        CONVERT(VARCHAR, CAST(DATEADD(minute, 
            SUM(
                DATEDIFF(minute, ISNULL(startup1, '00:00'), ISNULL(startup1Fin, '00:00')) +
                DATEDIFF(minute, ISNULL(break1, '00:00'), ISNULL(break1fin, '00:00')) +
                DATEDIFF(minute, ISNULL(lunch1, '00:00'), ISNULL(lunch1fin, '00:00')) +
                DATEDIFF(minute, ISNULL(break2, '00:00'), ISNULL(break2fin, '00:00'))
            ), 0) AS TIME), 8) AS totalBreakTimeShift1,
        CONVERT(VARCHAR, CAST(DATEADD(minute, 
            SUM(
                DATEDIFF(minute, ISNULL(startup2, '00:00'), ISNULL(startup2Fin, '00:00')) +
                DATEDIFF(minute, ISNULL(lunch2, '00:00'), ISNULL(lunch2fin, '00:00')) +
                DATEDIFF(minute, ISNULL(break3, '00:00'), ISNULL(break3fin, '00:00')) +
                DATEDIFF(minute, ISNULL(break4, '00:00'), ISNULL(break4fin, '00:00'))
            ), 0) AS TIME), 8) AS totalBreakTimeShift2
    FROM BreaktimeData
    GROUP BY line
),
DowntimeData AS (
    SELECT 
        'M' + LEFT([process], 1) AS line,
        SUBSTRING([process], 2, LEN([process]) - 1) AS process,
        COALESCE(CASE
            WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '07:00' AND '16:45' THEN 1
            WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '17:00' AND '02:00' THEN 2
            ELSE NULL
        END, 0) AS Shift,
        COALESCE(CASE
            WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '07:00' AND '16:45' THEN '07:00'
            WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '17:00' AND '02:00' THEN '17:00'
            ELSE NULL
        END, '00:00') AS startTime,
        COALESCE(CASE
            WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '07:00' AND '16:45' THEN '16:45'
            WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '17:00' AND '02:00' THEN '02:00'
            ELSE NULL
        END, '00:00') AS endTime,
        COALESCE(CASE
            WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '07:00' AND '16:45' 
            THEN CONVERT(VARCHAR, CAST(DATEADD(minute, DATEDIFF(minute, CONVERT(TIME, '07:00'), CONVERT(TIME, '16:45')), 0) AS TIME), 8)
            WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '17:00' AND '02:00'
            THEN CONVERT(VARCHAR, CAST(DATEADD(minute, DATEDIFF(minute, CONVERT(TIME, '17:00'), CONVERT(TIME, '02:00')), 0) AS TIME), 8)
            ELSE NULL
        END, '00:00') AS totalWorkingTime
    FROM [db_kyungshin].[dbo].[t_bma_downtime]
    WHERE CONVERT(date, insert_date) = '" & fechaCalculo & "'
    AND CASE
        WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '07:00' AND '16:45' THEN 1
        WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '17:00' AND '02:00' THEN 2
        ELSE NULL
    END <> 0
),
RunningTimeData AS (
    SELECT 
        'M' + LEFT([process], 1) AS line,
        SUBSTRING([process], 2, LEN([process]) - 1) AS process,
        CASE
            WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '07:00' AND '16:45' THEN 1
            WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '17:00' AND '02:00' THEN 2
        END AS Shift,
        SUM(DATEDIFF(minute, [starttime], [endtime])) AS RunningTimeMinutes
    FROM [db_kyungshin].[dbo].[t_bma_downtime]
    WHERE [status] = 'RUN'
    AND CONVERT(date, [insert_date]) = '" & fechaCalculo & "'
    GROUP BY 'M' + LEFT([process], 1), SUBSTRING([process], 2, LEN([process]) - 1),
        CASE
            WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '07:00' AND '16:45' THEN 1
            WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '17:00' AND '02:00' THEN 2
        END
)
SELECT 
    dd.line,
    dd.process,
    dd.Shift,
    dd.startTime,
    dd.endTime,
    dd.totalWorkingTime,
    CASE dd.Shift
        WHEN 1 THEN sb.totalBreakTimeShift1
        WHEN 2 THEN sb.totalBreakTimeShift2
        ELSE '00:00'
    END AS totalBreakTime,
    CONVERT(VARCHAR, CAST(DATEADD(minute, 
        DATEDIFF(minute, 
            CONVERT(TIME, CASE dd.Shift 
                WHEN 1 THEN sb.totalBreakTimeShift1 
                WHEN 2 THEN sb.totalBreakTimeShift2 
                ELSE '00:00' END),
            CONVERT(TIME, dd.totalWorkingTime)
        ), 0) AS TIME), 8) AS AvailableWorkingTime,
    CONVERT(VARCHAR, CAST(DATEADD(minute, ISNULL(rt.RunningTimeMinutes, 0), 0) AS TIME), 8) AS RunningTime,
    -- Cálculo de Downtime: AvailableWorkingTime (minutos) - RunningTime (minutos)
    CONVERT(VARCHAR, CAST(DATEADD(minute, 
        DATEDIFF(minute, 
            CONVERT(TIME, CASE dd.Shift 
                WHEN 1 THEN sb.totalBreakTimeShift1 
                WHEN 2 THEN sb.totalBreakTimeShift2 
                ELSE '00:00' END),
            CONVERT(TIME, dd.totalWorkingTime)
        ) - ISNULL(rt.RunningTimeMinutes, 0), 0) AS TIME), 8) AS Downtime
FROM DowntimeData dd
INNER JOIN ShiftBreaks sb ON dd.line = sb.line
LEFT JOIN RunningTimeData rt 
    ON dd.line = rt.line 
    AND dd.process = rt.process 
    AND dd.Shift = rt.Shift
GROUP BY dd.line, dd.process, dd.Shift, dd.startTime, dd.endTime, dd.totalWorkingTime, 
         sb.totalBreakTimeShift1, sb.totalBreakTimeShift2, rt.RunningTimeMinutes
ORDER BY dd.line, dd.process;"

            Dim datosOEE As DataTable = EjecutaSelects(query, "filltabla")


            query = "Select Top 1000 lineCode, qtyOrder, subpno
From [dbo].[TMES_PROCESSSTART] With (Nolock)
Where 1=1
           And [MesSiteCode] = 11
           And [OrderDate] = '" & fechaCalculo & "'
Order By [LotNo]"

            Dim datosPrograma As DataTable = EjecutaSelectsEISS(query, "fillProgram")

            For Each rw As DataRow In datosOEE.Rows
                dgv.Rows.Add(fechaCalculo, "BMA", "program", "model", rw("line").ToString, rw("shift").ToString, rw("process").ToString, "status", "", "",
                             rw("startTime").ToString, rw("endTime").ToString, rw("totalWorkingTime").ToString, rw("availableWorkingTime").ToString,
rw("totalBreakTime").ToString)

                'agregar mismos datos al OEE
                dgvOEE.Rows.Add(fechaCalculo, "BMA", "program", "model", rw("line").ToString, rw("shift").ToString, rw("process").ToString)
            Next

            Dim qtyOrderLookup As New Dictionary(Of String, Tuple(Of Integer, String))

            For Each rw As DataRow In datosPrograma.Rows
                Dim lineCode As String = rw("lineCode").ToString()
                Dim qtyOrder As Integer = CInt(rw("qtyOrder"))
                Dim subpno As String = rw("subpno").ToString()
                Try
                    qtyOrderLookup.Add(lineCode, New Tuple(Of Integer, String)(qtyOrder, subpno))
                Catch ex As Exception
                    'valores repetidos
                End Try

            Next

            ' 2. Iterar a través de las filas del DataGridView y actualizar qtyOrder y running time
            For Each row As DataGridViewRow In dgv.Rows
                Dim linea As String = row.Cells("line").Value.ToString().ToUpper() ' Obtener la línea (M1, M2, M3) en mayúsculas
                Dim lineCode As String = ""
                Dim status As String = row.Cells("status").Value.ToString().ToUpper() ' Obtener el status
                Dim programString As String = "" ' Variable para almacenar el valor de program

                ' 3. Determinar el lineCode correspondiente
                Select Case linea
                    Case "M1" : lineCode = "LINE42"
                    Case "M2" : lineCode = "LINE43"
                    Case "M3" : lineCode = "LINE44"
                    Case Else
                        Console.WriteLine("Advertencia: Línea desconocida: " & linea)
                        Continue For ' Saltar a la siguiente fila si la línea es desconocida
                End Select

                ' 4. Buscar qtyOrder en el diccionario y actualizar la columna productionQty
                If qtyOrderLookup.ContainsKey(lineCode) Then
                    Dim tuple As Tuple(Of Integer, String) = qtyOrderLookup(lineCode)
                    row.Cells("productionQty").Value = tuple.Item1 ' qtyOrder
                    row.Cells("model").Value = tuple.Item2 ' subpno (replace "subpnoColumn" with your actual column name)

                    ' 5. Asignar el valor de program según el subpno
                    Dim subpno As String = tuple.Item2.ToUpper() ' Convertir subpno a mayúsculas para la comparación

                    If subpno.StartsWith("23A-00610") OrElse subpno.StartsWith("23A-00620") OrElse subpno.StartsWith("23A-00627") Then
                        programString = "BEV-G"
                    ElseIf subpno = "23A-00638AN" OrElse subpno = "23A-00651AN" OrElse subpno = "23A-00730AN" OrElse subpno = "23A-00737AN" OrElse subpno = "23A-00752AN" Then
                        programString = "BEV-H"
                    Else
                        programString = "SKO_BEV-H"
                    End If

                    row.Cells("program").Value = programString ' Insertar el valor de program en la celda

                Else
                    Console.WriteLine("Advertencia: lineCode '" & lineCode & "' no encontrado en los datos del programa.")
                    row.Cells("productionQty").Value = 0 ' O un valor predeterminado
                End If


            Next

            query = "SELECT
    'M' + LEFT(process, 1) AS Linea,
    SUBSTRING(process, 2, 3) AS Proceso,
    RIGHT('0' + CAST(SUM(DATEPART(HOUR, TRY_CONVERT(TIME, AcumTime))) + (SUM(DATEPART(MINUTE, TRY_CONVERT(TIME, AcumTime))) + (SUM(DATEPART(SECOND, TRY_CONVERT(TIME, AcumTime)))/60))/60 AS VARCHAR), 2) + ':' +
    RIGHT('0' + CAST((SUM(DATEPART(MINUTE, TRY_CONVERT(TIME, AcumTime))) + (SUM(DATEPART(SECOND, TRY_CONVERT(TIME, AcumTime)))/60))%60 AS VARCHAR), 2) + ':' +
    RIGHT('0' + CAST(SUM(DATEPART(SECOND, TRY_CONVERT(TIME, AcumTime)))%60 AS VARCHAR), 2) AS time,
    'RUN' as status
FROM [db_kyungshin].[dbo].[t_bma_downtime]
WHERE 1=1
    AND CONVERT(DATE, insert_date) =  '" & fechaCalculo & "'
    AND status = 'RUN'
    AND CAST(CONVERT(VARCHAR, insert_date, 8) AS TIME) BETWEEN '07:00' AND '16:45'
GROUP BY LEFT(process, 1), SUBSTRING(process, 2, 3)
union all
SELECT
    'M' + LEFT(process, 1) AS Linea,  -- Agrega 'M' al inicio de la línea
    SUBSTRING(process, 2, 3) AS Proceso, 
    CONVERT(VARCHAR, CAST(DATEADD(minute, SUM(DATEDIFF(minute, '00:00', TRY_CONVERT(TIME, AcumTime))), 0) AS TIME), 8) as time,
	'DOWN' as status
FROM [db_kyungshin].[dbo].[t_bma_downtime]
WHERE 1=1
    AND CONVERT(DATE, insert_date) = '" & fechaCalculo & "'
    AND status = 'DOWN'
    AND CAST(CONVERT(VARCHAR, insert_date, 8) AS TIME) BETWEEN '07:01' AND '16:45'
GROUP BY LEFT(process, 1), SUBSTRING(process, 2, 3)
order by linea, proceso, STATUS"


            Dim datosTiempos As DataTable = EjecutaSelects(query, "fillProgram")
            ' 1. Crear un diccionario para búsqueda rápida de tiempos (usando una clave compuesta)

            ' 1. Crear un diccionario para búsqueda rápida de tiempos (usando una clave compuesta)
            ' 1. Crear un diccionario para búsqueda rápida de tiempos (usando una clave compuesta)
            Dim tiemposLookup As New Dictionary(Of String, String) ' Clave: Linea + Proceso + Status

            For Each rw As DataRow In datosTiempos.Rows
                Dim clave As String = rw("linea").ToString() & rw("proceso").ToString() & rw("status").ToString() ' Usar status del DataTable
                tiemposLookup.Add(clave, rw("time").ToString())
            Next

            ' 2. Iterar a través de las filas del DataGridView y actualizar tiempos
            For Each row As DataGridViewRow In dgv.Rows
                Dim lineaDgv As String = row.Cells("line").Value.ToString().ToUpper() ' Línea del DataGridView
                Dim procesoDgv As String = row.Cells("process").Value.ToString() ' Proceso del DataGridView
                Dim statusDgv As String = row.Cells("status").Value.ToString().ToUpper() ' Status del DataGridView
                Dim claveBusquedaInicial As String = ""

                ' 3. Construir la clave inicial para la búsqueda (usando los valores del DataGridView)
                claveBusquedaInicial = lineaDgv & procesoDgv

                ' 4. Buscar *todas* las coincidencias en el diccionario y actualizar la columna correspondiente
                For Each claveEnDiccionario As String In tiemposLookup.Keys
                    If claveEnDiccionario.StartsWith(claveBusquedaInicial) Then
                        Dim statusEnDiccionario As String = claveEnDiccionario.Substring(5).ToUpper() ' Obtener el status del diccionario

                        If statusEnDiccionario = "RUN" Then ' No se compara con statusDgv, se usa el del diccionario
                            row.Cells("running").Value = tiemposLookup(claveEnDiccionario)
                        ElseIf statusEnDiccionario = "DOWN" Then ' No se compara con statusDgv, se usa el del diccionario
                            row.Cells("breakdown").Value = tiemposLookup(claveEnDiccionario)
                        End If
                    End If
                Next ' Bucle Inner For

                ' Ya no es necesario asignar valores predeterminados aquí, se maneja arriba
            Next ' Bucle Outer For





            'tiempos muertos ===============================================================================================

            query = "	SELECT
    depto,
   'M' + LEFT(process, 1) AS Linea,  -- Agrega 'M' al inicio de la línea
      SUBSTRING(process, 2, LEN(process) - 1) AS process, 
    SUM(DATEDIFF(minute, '00:00', TRY_CONVERT(TIME, AcumTime))) AS time
FROM [db_kyungshin].[dbo].[t_bma_downtime]
WHERE
    1 = 1
    AND CONVERT(DATE, insert_date) = '" & fechaCalculo & "'
    AND status = 'DOWN'
    AND CAST(CONVERT(VARCHAR, insert_date, 8) AS TIME) BETWEEN '07:01' AND '16:45'
GROUP BY
    depto,
    process;"


            Dim datosDT As DataTable = EjecutaSelects(query, "fillProgram")
            ' 1. Crear un diccionario para búsqueda rápida de tiempos (usando una clave compuesta)

            Dim tiemposLookup2 As New Dictionary(Of String, String) ' Clave: Linea + Proceso + Status

            For Each rw As DataRow In datosDT.Rows
                Dim clave As String = rw("linea").ToString() & rw("process").ToString() & rw("depto").ToString() ' Usar status del DataTable
                Try
                    tiemposLookup2.Add(clave, rw("time").ToString())
                Catch ex As Exception
                    'repetidos
                End Try

            Next

            ' 2. Iterar a través de las filas del DataGridView y actualizar tiempos
            For Each row As DataGridViewRow In dgv.Rows
                Dim lineaDgv As String = row.Cells("line").Value.ToString().ToUpper() ' Línea del DataGridView
                Dim procesoDgv As String = row.Cells("process").Value.ToString() ' Proceso del DataGridView
                Dim statusDgv As String = row.Cells("status").Value.ToString().ToUpper() ' Status del DataGridView
                Dim claveBusquedaInicial As String = ""

                ' 3. Construir la clave inicial para la búsqueda (usando los valores del DataGridView)
                claveBusquedaInicial = lineaDgv & procesoDgv

                ' 4. Buscar *todas* las coincidencias en el diccionario y actualizar la columna correspondiente
                For Each claveEnDiccionario As String In tiemposLookup2.Keys
                    If claveEnDiccionario.StartsWith(claveBusquedaInicial) Then
                        Dim statusEnDiccionario As String = claveEnDiccionario.Substring(5).ToUpper() ' Obtener el status del diccionario

                        If statusEnDiccionario = "PRODUCTION" Then ' No se compara con statusDgv, se usa el del diccionario
                            row.Cells("productionDT").Value = tiemposLookup2(claveEnDiccionario)
                        ElseIf statusEnDiccionario = "QUALITY" Then ' No se compara con statusDgv, se usa el del diccionario
                            row.Cells("qualityDT").Value = tiemposLookup2(claveEnDiccionario)
                        ElseIf statusEnDiccionario = "MATERIALS" Then ' No se compara con statusDgv, se usa el del diccionario
                            row.Cells("materialsDT").Value = tiemposLookup2(claveEnDiccionario)
                        ElseIf statusEnDiccionario = "MAINTENANCE" Then ' No se compara con statusDgv, se usa el del diccionario
                            row.Cells("maintenanceDT").Value = tiemposLookup2(claveEnDiccionario)
                        ElseIf statusEnDiccionario = "" Then ' No se compara con statusDgv, se usa el del diccionario
                            row.Cells("othersDT").Value = tiemposLookup2(claveEnDiccionario)
                        End If
                    End If
                Next ' Bucle Inner For

                ' Ya no es necesario asignar valores predeterminados aquí, se maneja arriba
            Next ' Bucle Outer For



            '======================================OEE CALCULATION =================================================================

            ' 2. Iterar a través de las filas del DataGridView y actualizar qtyOrder y running time
            For Each row As DataGridViewRow In dgvOEE.Rows
                Dim linea As String = row.Cells("line2").Value.ToString().ToUpper() ' Obtener la línea (M1, M2, M3) en mayúsculas
                Dim lineCode As String = ""
                Dim programString As String = "" ' Variable para almacenar el valor de program

                ' 3. Determinar el lineCode correspondiente
                Select Case linea
                    Case "M1" : lineCode = "LINE42"
                    Case "M2" : lineCode = "LINE43"
                    Case "M3" : lineCode = "LINE44"
                    Case Else
                        Console.WriteLine("Advertencia: Línea desconocida: " & linea)
                        Continue For ' Saltar a la siguiente fila si la línea es desconocida
                End Select

                ' 4. Buscar qtyOrder en el diccionario y actualizar la columna productionQty
                If qtyOrderLookup.ContainsKey(lineCode) Then
                    Dim tuple As Tuple(Of Integer, String) = qtyOrderLookup(lineCode)
                    row.Cells("qtyDaily").Value = tuple.Item1 ' qtyOrder
                    row.Cells("model2").Value = tuple.Item2 ' subpno (replace "subpnoColumn" with your actual column name)

                    ' 5. Asignar el valor de program según el subpno
                    Dim subpno As String = tuple.Item2.ToUpper() ' Convertir subpno a mayúsculas para la comparación

                    If subpno.StartsWith("23A-00610") OrElse subpno.StartsWith("23A-00620") OrElse subpno.StartsWith("23A-00627") Then
                        programString = "BEV-G"
                    ElseIf subpno = "23A-00638AN" OrElse subpno = "23A-00651AN" OrElse subpno = "23A-00730AN" OrElse subpno = "23A-00737AN" OrElse subpno = "23A-00752AN" Then
                        programString = "BEV-H"
                    Else
                        programString = "SKO_BEV-H"
                    End If

                    row.Cells("program2").Value = programString ' Insertar el valor de program en la celda

                Else
                    Console.WriteLine("Advertencia: lineCode '" & lineCode & "' no encontrado en los datos del programa.")
                    row.Cells("qtyDaily").Value = 0 ' O un valor predeterminado
                End If
            Next

            query = "DECLARE  @MesSiteCode INT = 11
       ,@ResultDate NVARCHAR(10) = '" & ConvierteAdateMySQL(dtpDesde.Value.ToShortDateString) & "'
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


--PCB 합격 수량          namjh0831@kyungshin.co.kr
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


--PCB 불합격 수량, 불합격 후 합격된 바코드는 제외          namjh0831@kyungshin.co.kr
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


--ASSY 합격 수량          namjh0831@kyungshin.co.kr
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


--ASSY 불합격 수량, 불합격 후 합격된 바코드는 제외          namjh0831@kyungshin.co.kr
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
  AND T1.[InspectResult] = 'FAIL'
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


--설비별
If(@ViewTypeCode = 'ByMachine')
Begin
  SELECT 
    T1.[ProcessCode],
    T4.[LineCode], 
    sum(convert(Int, T1.[Shift1])) As [Shift1]
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
    T1.[ProcessCode],
    T4.[LineCode]
  HAVING T1.[ProcessCode] IN ('BM0960', 'BM0845', 'BM0770')
  Order By T1.[ProcessCode], T4.[LineCode]
End"
            Dim resultadosConsulta As DataTable = EjecutaSelectsEISS(query, "buscaDatos")

            ' Asumiendo que 'dgvDatos' es tu DataGridView y 'resultadosConsulta' es el DataTable
            ' que obtuviste de la ejecución de tu consulta SQL.
            ' El DataTable 'resultadosConsulta' debería tener columnas llamadas "ProcessCode",
            ' "LineCode" y la columna que contiene el total de Shift1 (ajústala si es diferente).

            Dim totalTime As Integer = 585 ' minutos del turno


            For Each rowDGV As DataGridViewRow In dgvOEE.Rows
                ' Obtén los valores de las columnas relevantes del DataGridView
                Dim lineaDGV As String = CStr(rowDGV.Cells("line2").Value).Trim().ToUpper()
                Dim procesoDGV As String = CStr(rowDGV.Cells("process2").Value).Trim().ToUpper()
                Dim qtyDGV As String = CStr(rowDGV.Cells("qtyDaily").Value) ' Por si ya tiene un valor

                Dim totalEncontrado As Integer = 0
                Dim encontrado As Boolean = False

                ' Itera sobre las filas del DataTable 'resultadosConsulta'
                For Each rowConsulta As DataRow In resultadosConsulta.Rows
                    ' Accede a los valores de las columnas del DataTable por nombre
                    Dim procesoConsulta As String = CStr(rowConsulta("ProcessCode")).Trim().ToUpper()
                    Dim lineaConsultaDT As String = CStr(rowConsulta("LineCode")).Trim().ToUpper() ' Cambiado nombre para evitar confusión
                    Dim totalShift1 As Integer = CInt(rowConsulta("Shift1")) ' Asegúrate que "Shift1" sea el nombre correcto de la columna

                    ' Define las correspondencias de línea
                    Dim lineaCorrespondencia As String = ""
                    If procesoDGV = "100" Then
                        If lineaDGV = "M1" Then
                            lineaCorrespondencia = "LINE32"
                        ElseIf lineaDGV = "M2" Then
                            lineaCorrespondencia = "LINE33"
                        ElseIf lineaDGV = "M3" Then
                            lineaCorrespondencia = "LINE34"
                        ElseIf lineaDGV = "M4" Then
                            lineaCorrespondencia = "LINE35"
                        ElseIf lineaDGV = "M5" Then
                            lineaCorrespondencia = "LINE36"
                        End If
                    Else
                        If lineaDGV = "M1" Then
                            lineaCorrespondencia = "LINE42"
                        ElseIf lineaDGV = "M2" Then
                            lineaCorrespondencia = "LINE43"
                        ElseIf lineaDGV = "M3" Then
                            lineaCorrespondencia = "LINE44"
                        ElseIf lineaDGV = "M4" Then
                            lineaCorrespondencia = "LINE45"
                        ElseIf lineaDGV = "M5" Then
                            lineaCorrespondencia = "LINE46"
                        End If
                    End If

                    ' Define las correspondencias de proceso y cantidad
                    Dim cantidadCorrespondencia As Integer = -1
                    If procesoConsulta = "BM0770" AndAlso procesoDGV = "100" Then
                        cantidadCorrespondencia = 100
                    ElseIf procesoConsulta = "BM0845" AndAlso procesoDGV = "400" Then
                        cantidadCorrespondencia = 400
                    ElseIf procesoConsulta = "BM0960" AndAlso procesoDGV = "700" Then
                        cantidadCorrespondencia = 700
                    End If

                    ' Comprueba si hay coincidencia en línea y proceso (si se define una correspondencia)
                    If Not String.IsNullOrEmpty(lineaCorrespondencia) AndAlso lineaConsultaDT = lineaCorrespondencia AndAlso cantidadCorrespondencia <> -1 AndAlso CInt(procesoDGV) = cantidadCorrespondencia Then
                        totalEncontrado = totalShift1
                        encontrado = True
                        Exit For
                    End If
                Next

                ' Si se encontró un total en la consulta, actualiza la celda "qty" del DataGridView
                If encontrado Then
                    rowDGV.Cells("qtyDaily").Value = totalEncontrado
                Else
                    ' Opcional: Puedes poner un valor por defecto si no se encuentra coincidencia
                    rowDGV.Cells("qtyDaily").Value = 0 ' O algún otro valor que desees
                End If

                rowDGV.Cells("totalTime").Value = totalTime ' Agrega esta línea
            Next


            'columna total breaktime

            query = "WITH BreaktimeData AS (
    SELECT
        line,
        startup1,
        startup1Fin,
        break1,
        break1fin,
        lunch1,
        lunch1fin,
        break2,
        break2fin,
        startup2,
        startup2fin,
        lunch2,
        lunch2fin,
        break3,
        break3fin,
        break4,
        break4fin
    FROM [db_kyungshin].[dbo].[t_bma_breaktime]
),
ShiftBreaks AS (
    SELECT
        line,
        SUM(
            DATEDIFF(minute, ISNULL(startup1, '00:00'), ISNULL(startup1Fin, '00:00')) +
            DATEDIFF(minute, ISNULL(break1, '00:00'), ISNULL(break1fin, '00:00')) +
            DATEDIFF(minute, ISNULL(lunch1, '00:00'), ISNULL(lunch1fin, '00:00')) +
            DATEDIFF(minute, ISNULL(break2, '00:00'), ISNULL(break2fin, '00:00'))
        ) AS totalBreakTimeShift1_Minutes,
        SUM(
            DATEDIFF(minute, ISNULL(startup2, '00:00'), ISNULL(startup2Fin, '00:00')) +
            DATEDIFF(minute, ISNULL(lunch2, '00:00'), ISNULL(lunch2fin, '00:00')) +
            DATEDIFF(minute, ISNULL(break3, '00:00'), ISNULL(break3fin, '00:00')) +
            DATEDIFF(minute, ISNULL(break4, '00:00'), ISNULL(break4fin, '00:00'))
        ) AS totalBreakTimeShift2_Minutes
    FROM BreaktimeData
    GROUP BY line
),
DowntimeData AS (
    SELECT 
        'M' + LEFT([process], 1) AS line,
        SUBSTRING([process], 2, LEN([process]) - 1) AS process,
        COALESCE(CASE
            WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '07:00' AND '16:45' THEN 1
            WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '17:00' AND '02:00' THEN 2
            ELSE NULL
        END, 0) AS Shift
    FROM [db_kyungshin].[dbo].[t_bma_downtime]
    WHERE CONVERT(date, insert_date) = '" & fechaCalculo & "'
    AND CASE
        WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '07:00' AND '16:45' THEN 1
        WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '17:00' AND '02:00' THEN 2
        ELSE NULL
    END <> 0
)
SELECT 
    dd.line,
    dd.process,
    dd.Shift,
    CASE dd.Shift
        WHEN 1 THEN sb.totalBreakTimeShift1_Minutes
        WHEN 2 THEN sb.totalBreakTimeShift2_Minutes
        ELSE 0
    END AS totalBreakTime_Minutes
FROM DowntimeData dd
INNER JOIN ShiftBreaks sb ON dd.line = sb.line
GROUP BY dd.line, dd.process, dd.Shift, sb.totalBreakTimeShift1_Minutes, sb.totalBreakTimeShift2_Minutes
ORDER BY dd.line, dd.process;
"

            Dim totalbreakDT As DataTable = EjecutaSelects(query, "llenabreaktime")

            ' Recorrer cada fila del DataGridView
            For Each dgvRow As DataGridViewRow In dgvOEE.Rows
                ' Tomar valores clave de la fila del DataGridView
                Dim dgvLine As String = dgvRow.Cells("line2").Value.ToString()
                Dim dgvProcess As String = dgvRow.Cells("process2").Value.ToString()
                Dim dgvShift As String = dgvRow.Cells("shift2").Value.ToString()

                ' Buscar la fila correspondiente en el DataTable
                Dim matchingRows As DataRow() = totalbreakDT.Select($"line = '{dgvLine}' AND process = '{dgvProcess}' AND Shift = {dgvShift}")

                If matchingRows.Length > 0 Then
                    ' Si hay coincidencia, tomamos el valor totalBreakTime_Minutes
                    Dim totalBreakMinutes As Integer = Convert.ToInt32(matchingRows(0)("totalBreakTime_Minutes"))

                    ' Asignar el valor a la columna Planned Dowtime en el DataGridView
                    dgvRow.Cells("plannedDT").Value = totalBreakMinutes
                End If
            Next


            'AVAILABLE TIME

            For Each dgvRow As DataGridViewRow In dgvOEE.Rows
                If Not dgvRow.IsNewRow Then
                    Dim totalTime2 As Integer = 0
                    Dim plannedDowntime As Integer = 0

                    ' Intentar convertir los valores a Integer (con seguridad)
                    Integer.TryParse(dgvRow.Cells("totalTime").Value?.ToString(), totalTime2)
                    Integer.TryParse(dgvRow.Cells("plannedDT").Value?.ToString(), plannedDowntime)

                    ' Realizar la resta
                    Dim availableTime As Integer = totalTime2 - plannedDowntime

                    ' Asegurar que no salga negativo (opcional, según tu lógica)
                    If availableTime < 0 Then
                        availableTime = 0
                    End If

                    ' Asignar el valor al DataGridView
                    dgvRow.Cells("available2").Value = availableTime
                End If
            Next


            'dowtime y runningtime

            query = "WITH BreaktimeData AS (
    SELECT
        line,
        startup1, startup1Fin,
        break1, break1fin,
        lunch1, lunch1fin,
        break2, break2fin,
        startup2, startup2Fin,
        lunch2, lunch2Fin,
        break3, break3Fin,
        break4, break4Fin
    FROM [db_kyungshin].[dbo].[t_bma_breaktime]
),
ShiftBreaks AS (
    SELECT
        line,
        SUM(DATEDIFF(minute, ISNULL(startup1, '00:00'), ISNULL(startup1Fin, '00:00')) +
            DATEDIFF(minute, ISNULL(break1, '00:00'), ISNULL(break1fin, '00:00')) +
            DATEDIFF(minute, ISNULL(lunch1, '00:00'), ISNULL(lunch1fin, '00:00')) +
            DATEDIFF(minute, ISNULL(break2, '00:00'), ISNULL(break2fin, '00:00'))
        ) AS totalBreakTimeShift1_Minutes,
        SUM(DATEDIFF(minute, ISNULL(startup2, '00:00'), ISNULL(startup2Fin, '00:00')) +
            DATEDIFF(minute, ISNULL(lunch2, '00:00'), ISNULL(lunch2fin, '00:00')) +
            DATEDIFF(minute, ISNULL(break3, '00:00'), ISNULL(break3fin, '00:00')) +
            DATEDIFF(minute, ISNULL(break4, '00:00'), ISNULL(break4fin, '00:00'))
        ) AS totalBreakTimeShift2_Minutes
    FROM BreaktimeData
    GROUP BY line
),
DowntimeData AS (
    SELECT 
        'M' + LEFT([process], 1) AS line,
        SUBSTRING([process], 2, LEN([process]) - 1) AS process,
        CASE
            WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '07:00' AND '16:45' THEN 1
            WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '17:00' AND '02:00' THEN 2
        END AS Shift,
        DATEDIFF(minute, 
            CASE
                WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '07:00' AND '16:45' THEN '07:00'
                WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '17:00' AND '02:00' THEN '17:00'
                ELSE '00:00'
            END,
            CASE
                WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '07:00' AND '16:45' THEN '16:45'
                WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '17:00' AND '02:00' THEN '02:00'
                ELSE '00:00'
            END
        ) AS totalWorkingTime_Minutes
    FROM [db_kyungshin].[dbo].[t_bma_downtime]
    WHERE CONVERT(date, insert_date) = '" & fechaCalculo & "'
    AND (
        CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '07:00' AND '16:45'
        OR CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '17:00' AND '02:00'
    )
),
RunningTimeData AS (
    SELECT 
        'M' + LEFT([process], 1) AS line,
        SUBSTRING([process], 2, LEN([process]) - 1) AS process,
        CASE
            WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '07:00' AND '16:45' THEN 1
            WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '17:00' AND '02:00' THEN 2
        END AS Shift,
        SUM(DATEDIFF(minute, [starttime], [endtime])) AS RunningTimeMinutes
    FROM [db_kyungshin].[dbo].[t_bma_downtime]
    WHERE [status] = 'RUN'
    AND CONVERT(date, [insert_date]) = '" & fechaCalculo & "'
    GROUP BY 'M' + LEFT([process], 1), SUBSTRING([process], 2, LEN([process]) - 1),
        CASE
            WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '07:00' AND '16:45' THEN 1
            WHEN CAST(CONVERT(VARCHAR, [insert_date], 8) AS TIME) BETWEEN '17:00' AND '02:00' THEN 2
        END
)
SELECT 
    dd.line,
    dd.process,
    ISNULL(rt.RunningTimeMinutes, 0) AS RunningTime_Minutes,
    (dd.totalWorkingTime_Minutes - 
        CASE dd.Shift
            WHEN 1 THEN sb.totalBreakTimeShift1_Minutes
            WHEN 2 THEN sb.totalBreakTimeShift2_Minutes
            ELSE 0
        END
    ) - ISNULL(rt.RunningTimeMinutes, 0) AS Downtime_Minutes
FROM DowntimeData dd
INNER JOIN ShiftBreaks sb ON dd.line = sb.line
LEFT JOIN RunningTimeData rt 
    ON dd.line = rt.line 
    AND dd.process = rt.process 
    AND dd.Shift = rt.Shift
GROUP BY dd.line, dd.process, dd.Shift, dd.totalWorkingTime_Minutes, 
         sb.totalBreakTimeShift1_Minutes, sb.totalBreakTimeShift2_Minutes, rt.RunningTimeMinutes
ORDER BY dd.line, dd.process;
"
            Dim rtdt As DataTable = EjecutaSelects(query, "insertaDTRT")


            For Each row As DataGridViewRow In dgvOEE.Rows
                Dim line As String = row.Cells("line2").Value.ToString()
                Dim process As String = row.Cells("process2").Value.ToString()

                ' Filtrar el DataTable para encontrar la fila correspondiente
                Dim matches = rtdt.Select($"line = '{line}' AND process = '{process}'")

                If matches.Length > 0 Then
                    Dim matchRow As DataRow = matches(0)

                    ' Obtener los valores de Downtime_Minutes y RunningTime_Minutes
                    Dim downtime As Integer = Convert.ToInt32(matchRow("Downtime_Minutes"))
                    Dim runningTime As Integer = Convert.ToInt32(matchRow("RunningTime_Minutes"))

                    ' Asignar los valores a las columnas del DataGridView
                    'row.Cells("breakdowns").Value = downtime
                    row.Cells("running2").Value = runningTime
                Else
                    ' Si no se encuentra, dejar en 0 o vacío
                    ' row.Cells("breakdowns").Value = 0
                    row.Cells("running2").Value = 0
                End If
            Next

            '========================= breakdowns

            query = "SELECT
    'M' + LEFT(process, 1) AS Linea,  -- Agrega 'M' al inicio de la línea
    SUBSTRING(process, 2, LEN(process) - 1) AS process, 
    SUM(DATEDIFF(minute, '00:00', TRY_CONVERT(TIME, AcumTime))) AS total_time_minutes
FROM [db_kyungshin].[dbo].[t_bma_downtime]
WHERE
    CONVERT(DATE, insert_date) = '" & fechaCalculo & "'
    AND status = 'DOWN'
    AND CAST(CONVERT(VARCHAR, insert_date, 8) AS TIME) BETWEEN '07:01' AND '16:45'
GROUP BY
    'M' + LEFT(process, 1),
    SUBSTRING(process, 2, LEN(process) - 1)
ORDER BY
    Linea, process;
"

            Dim dtBreakdowns As DataTable = EjecutaSelects(query, "buscaBreakdowns")



            For Each row As DataGridViewRow In dgvOEE.Rows
                Dim line2 As String = row.Cells("line2").Value.ToString().Trim()
                Dim process2 As String = row.Cells("process2").Value.ToString().Trim()

                ' Quitar la M inicial para comparar solo el número del proceso
                Dim lookupLine As String = line2
                Dim lookupProcess As String = process2

                ' Buscar en el DataTable de breakdowns
                Dim matches = dtBreakdowns.Select($"Linea = '{lookupLine}' AND process = '{lookupProcess}'")

                If matches.Length > 0 Then
                    Dim breakdownMinutes As Integer = Convert.ToInt32(matches(0)("total_time_minutes"))
                    row.Cells("breakdowns").Value = breakdownMinutes
                Else
                    row.Cells("breakdowns").Value = 0 ' Si no encuentra, dejarlo en 0
                End If
            Next


            'tooling 15 minutos al inicio de cada turno, por lo pronto ==============================================

            For Each row As DataGridViewRow In dgvOEE.Rows
                ' Asignar valor fijo 15 a la columna Tooling
                row.Cells("tooling").Value = 15

                ' Calcular running2 = available2 - (breakdowns + tooling)
                Dim available2 As Integer = Convert.ToInt32(row.Cells("available2").Value)
                Dim breakdowns As Integer = Convert.ToInt32(row.Cells("breakdowns").Value)
                Dim tooling As Integer = Convert.ToInt32(row.Cells("tooling").Value)

                Dim running2 As Integer = available2 - (breakdowns + tooling)
                row.Cells("running2").Value = running2

                ' Calcular porcentaje: (running2 / available2) * 100, redondeado a 2 decimales
                Dim percentage As Double = 0
                If available2 <> 0 Then
                    percentage = Math.Round((running2 / available2) * 100, 2)
                End If

                row.Cells("runninge").Value = percentage.ToString("F2") & "%"
            Next

            'UPH============================================================================

            Dim uphQuery As String = "
    SELECT [id], [model], [sma], [p100], [p400], [p700], [pn]
    FROM [db_kyungshin].[dbo].[t_bma_uph]
"
            Dim uphDt As DataTable = EjecutaSelects(uphQuery, "obtieneUPH")

            ' Preparamos la tabla de UPH en un diccionario para búsqueda rápida y limpia
            Dim uphDict As New Dictionary(Of String, DataRow)(StringComparer.OrdinalIgnoreCase)

            For Each uphRow As DataRow In uphDt.Rows
                Dim pn As String = uphRow("pn").ToString().Trim()
                If Not uphDict.ContainsKey(pn) Then
                    uphDict.Add(pn, uphRow)
                End If
            Next

            ' Ahora recorremos el DataGridView
            For Each row As DataGridViewRow In dgvOEE.Rows
                Dim model2 As String = row.Cells("model2").Value.ToString().Trim()
                Dim process As String = row.Cells("process2").Value.ToString().Trim()
                Dim uph As Double = 0

                ' Buscamos en el diccionario
                If uphDict.ContainsKey(model2) Then
                    Dim uphRow As DataRow = uphDict(model2)

                    ' Seleccionamos el valor correcto según process
                    Select Case process
                        Case "100"
                            uph = Convert.ToDouble(uphRow("p100"))
                        Case "400"
                            uph = Convert.ToDouble(uphRow("p400"))
                        Case "700"
                            uph = Convert.ToDouble(uphRow("p700"))
                        Case Else
                            uph = 0
                    End Select
                Else
                    ' Si no encuentra match, lo dejamos en 0 o muestra un mensaje
                    uph = 0
                End If

                ' Obtenemos runninge (quitar % y convertir a decimal)
                Dim runningeStr As String = row.Cells("qtyDaily").Value.ToString
                Dim runninge As Double = 0
                If Double.TryParse(runningeStr, runninge) Then
                    runninge = runninge / 100
                End If

                Dim running2 As Double = Convert.ToDouble(row.Cells("running2").Value)

                ' Calculamos productive
                Dim productive As Double = 0
                If running2 * uph <> 0 Then
                    productive = Math.Round(runningeStr / (running2 * uph) * 100, 2)
                End If

                row.Cells("productive").Value = productive.ToString("F2") & "%"
            Next


            'times of breakdown=====================================================

            ' Primero armamos la lista única de process a consultar
            Dim processSet As New HashSet(Of String)()

            For Each row As DataGridViewRow In dgvOEE.Rows
                Dim line2 As String = row.Cells("line2").Value.ToString().Trim().Replace("M", "")
                Dim process2 As String = row.Cells("process2").Value.ToString().Trim()
                Dim combinedProcess As String = line2 & process2
                If Not processSet.Contains(combinedProcess) Then
                    processSet.Add(combinedProcess)
                End If
            Next

            ' Preparamos un diccionario para guardar los counts
            Dim timesDict As New Dictionary(Of String, Integer)()

            ' Para cada proceso único, hacemos la consulta y guardamos el resultado
            For Each processKey As String In processSet
                Dim timesQuery As String = "
        SELECT COUNT(*)
        FROM [db_kyungshin].[dbo].[t_bma_downtime]
        WHERE process = '" & processKey & "'
        AND CONVERT(DATE, insert_date) = '" & fechaCalculo & "'
        AND AcumTime > '00:05:00'
        AND status = 'DOWN'
        AND startTime > '07:00:00'
    "

                Dim timesDt As DataTable = EjecutaSelects(timesQuery, "obtieneTimes")
                Dim count As Integer = 0
                If timesDt.Rows.Count > 0 Then
                    count = Convert.ToInt32(timesDt.Rows(0)(0))
                End If

                timesDict(processKey) = count
            Next

            ' Finalmente llenamos la columna times en el DataGridView
            For Each row As DataGridViewRow In dgvOEE.Rows
                Dim line2 As String = row.Cells("line2").Value.ToString().Trim().Replace("M", "")
                Dim process2 As String = row.Cells("process2").Value.ToString().Trim()
                Dim combinedProcess As String = line2 & process2

                If timesDict.ContainsKey(combinedProcess) Then
                    row.Cells("times").Value = timesDict(combinedProcess)
                Else
                    row.Cells("times").Value = 0
                End If
            Next


            ' defects ==============================================================================

            ' Primero armamos la lista única de process a consultar (reutilizable del paso anterior)
            Dim defectProcessSet As New HashSet(Of String)()

            For Each row As DataGridViewRow In dgvOEE.Rows
                Dim line2 As String = row.Cells("line2").Value.ToString().Trim().Replace("M", "")
                Dim process2 As String = row.Cells("process2").Value.ToString().Trim()
                Dim combinedProcess As String = line2 & process2
                If Not defectProcessSet.Contains(combinedProcess) Then
                    defectProcessSet.Add(combinedProcess)
                End If
            Next

            ' Preparamos un diccionario para guardar los sums de defects
            Dim defectsDict As New Dictionary(Of String, Integer)()

            ' Para cada proceso único, hacemos la consulta y guardamos el resultado
            For Each processKey As String In defectProcessSet
                Dim defectsQuery As String = "
        SELECT SUM(defects)
        FROM [db_kyungshin].[dbo].[t_bma_downtime]
        WHERE process = '" & processKey & "'
        AND CONVERT(DATE, insert_date) = '" & fechaCalculo & "'
    "

                Dim defectsDt As DataTable = EjecutaSelects(defectsQuery, "obtieneDefects")
                Dim sumDefects As Integer = 0
                If defectsDt.Rows.Count > 0 AndAlso Not IsDBNull(defectsDt.Rows(0)(0)) Then
                    sumDefects = Convert.ToInt32(defectsDt.Rows(0)(0))
                End If

                defectsDict(processKey) = sumDefects
            Next

            ' Finalmente llenamos la columna defects en el DataGridView
            For Each row As DataGridViewRow In dgvOEE.Rows
                Dim line2 As String = row.Cells("line2").Value.ToString().Trim().Replace("M", "")
                Dim process2 As String = row.Cells("process2").Value.ToString().Trim()
                Dim combinedProcess As String = line2 & process2

                If defectsDict.ContainsKey(combinedProcess) Then
                    row.Cells("defects").Value = defectsDict(combinedProcess)
                Else
                    row.Cells("defects").Value = 0
                End If
            Next

            '================== OEE ==============================================

            For Each row As DataGridViewRow In dgvOEE.Rows
                ' Leer los valores necesarios
                Dim breakdowns As Double = Convert.ToDouble(row.Cells("breakdowns").Value)
                Dim times As Double = Convert.ToDouble(row.Cells("times").Value)
                Dim running2 As Double = Convert.ToDouble(row.Cells("running2").Value)
                Dim qtyDaily As Double = Convert.ToDouble(row.Cells("qtyDaily").Value)
                Dim defects As Double = Convert.ToDouble(row.Cells("defects").Value)
                Dim available2 As Double = Convert.ToDouble(row.Cells("available2").Value)
                Dim runninge As Double = Convert.ToDouble(row.Cells("runninge").Value.ToString().Trim().Replace("%", "")) / 100 ' Convertir de porcentaje a decimal
                Dim productive As Double = Convert.ToDouble(row.Cells("productive").Value.ToString().Trim().Replace("%", "") / 100)

                ' Evitar división por cero
                Dim mttr As Double = If(times > 0, breakdowns / times, 0)
                Dim mtbf As Double = If(times > 0, running2 / times, 0)
                Dim ratio As Double = If(qtyDaily > 0, (qtyDaily - defects) / qtyDaily, 0)
                Dim durability As Double = If(available2 > 0, Math.Round((breakdowns / available2) * 100, 2), 0)
                Dim oee As Double = runninge * productive * ratio

                ' Asignar a las celdas correspondientes
                row.Cells("mttr").Value = Math.Round(mttr, 2)
                row.Cells("mtbf").Value = Math.Round(mtbf, 2)
                row.Cells("ratio").Value = Math.Round(ratio * 100, 2) & "%" ' Multiplicado por 100 y con símbolo %
                row.Cells("durability").Value = durability
                row.Cells("oee").Value = Math.Round(oee * 100, 2) & "%" ' Multiplicado por 100 y con símbolo %
            Next

            ' Cambiar fuente a más grande (por ejemplo, tamaño 12)
            dgvOEE.DefaultCellStyle.Font = New Font("Segoe UI", 12)

            ' Centrar todas las columnas
            For Each col As DataGridViewColumn In dgvOEE.Columns
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            Next



        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Function EjecutaSelectsEISS(ByVal sSQL As String, ByVal sender As String)
        Dim cmd As SqlCommand = Nothing
        Try
            cmd = New SqlCommand(sSQL, conHISS)
            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            Return dt
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Return sender
    End Function
End Class