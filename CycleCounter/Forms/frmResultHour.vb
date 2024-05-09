Imports System.Data.SqlClient

Public Class frmResultHour
	Private Sub frmResultHour_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Try
			checkHISSconection()
			Dim query As String = "------------------------------------------------------------------------------------------------------------------------------
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
		T1.[MesSiteCode],
        T2.[MesSiteName],
		T3.[ProcessKindName],
		T1.[ProcessCode],
		T3.[ProcessName],
		--T5.[CarName],
		--T5.[ItemName], 
		--T1.[LotNo],
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
		T4.[LineCode], 
		T1.[MachineCode],
		T4.[MachineName]
	Order By T1.[ProcessCode], T1.[MachineCode]
End
------------------------------------------------------------------------------------------------------------------------------"

			dgvAux.DataSource = EjecutaSelectsEISS(query, "findHoiur")
			ResizeCols(dgvAux)
			dgvAux.Columns(0).Visible = False
			dgvAux.Columns(1).Visible = False
			dgvAux.Columns(2).Visible = False
		Catch ex As Exception

		End Try
	End Sub

	Public conHISS As SqlConnection = Nothing

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
End Class