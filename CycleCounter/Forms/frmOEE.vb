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
            Next
            Dim qtyOrderLookup As New Dictionary(Of String, Tuple(Of Integer, String))

            For Each rw As DataRow In datosPrograma.Rows
                Dim lineCode As String = rw("lineCode").ToString()
                Dim qtyOrder As Integer = CInt(rw("qtyOrder"))
                Dim subpno As String = rw("subpno").ToString()
                qtyOrderLookup.Add(lineCode, New Tuple(Of Integer, String)(qtyOrder, subpno))
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
                tiemposLookup2.Add(clave, rw("time").ToString())
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

        Catch ex As Exception

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