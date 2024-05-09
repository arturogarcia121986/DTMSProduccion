Imports System.Data.OleDb
Imports System.Runtime.InteropServices


Namespace aaa
    Public Enum WindowID
        ID_MAIN = 0
        ID_CONFIG
        ID_HISTORY
    End Enum

    Public Enum SendMsg
        SaveLog = 0
        MsgBox
        MainList
        UpdateDisplay
        BringToFront
    End Enum

    Public Enum Language
        Korea = 0
        English = 1
        China = 2
    End Enum

    Public Enum BtnType
        OkOnly = 0
        YesNo = 1
        YesNoOption = 2
    End Enum

    Public Enum TitleType
        MsgCaution = 0
        MsgInfo = 1
        MsgChoice = 2
        MsgConfirm = 3
    End Enum

    Public Enum FormType
        Normal = 0
        Modal = 1
    End Enum
    Public Enum MainListMode
        SystemMsg = 0
        ErrMsg = 1
        EventMsg = 2
    End Enum

    Public Enum DateInterval
        Day = 0
        DayOfYear
        Hour
        Minute
        Month
        Quarter
        Second
        Weekday
        WeekOfYear
        Year
    End Enum
    Public Enum CurrModeID
        ID_MANUAL = 0
        ID_AUTO
        ID_ERROR
    End Enum

    Public Enum eMCType
        Common = 0
    End Enum
    Public Class gMC
        <DllImport("kernel32.dll")>
        Public Shared Sub Sleep(ByVal dwMilliseconds As Integer)
        End Sub

        <DllImport("kernel32.dll", EntryPoint:="GetTickCount64")>
        Public Shared Function GetTickCount64() As Integer
        End Function

        <DllImport("kernel32.dll", EntryPoint:="GetTickCount")>
        Public Shared Function GetTickCount() As Integer
        End Function

        <DllImport("winmm.dll", EntryPoint:="timeGetTime")>
        Public Shared Function timeGetTime() As Integer
        End Function

        <DllImport("winmm.dll", EntryPoint:="timeBeginPeriod")>
        Public Shared Function timeBeginPeriod(ByVal uPeriod As Integer) As Integer
        End Function

        Public Shared [Exit] As Boolean
        Public Shared IniRun As Boolean

        Public Shared Window As WindowID
        Public Shared CurrMode As CurrModeID
        Public Shared WorkLevel As Integer

        Public Shared Blink As Boolean
        Public Shared Language As Integer  ' 0 = English  1 = Korean  2 = Chinese

        Public Shared QtyReelUnload As Integer

        Public Shared EngineerPassword As String
        Public Shared MasterPassword As String
        Public Shared AdminPassword As String

        Public Shared ProductStart As Date
        Public Shared ErrorStart As Date
        Public Shared MCType As eMCType
        Public Shared adoConn As OleDbConnection = New OleDbConnection()
    End Class

    Public Class gMsg
        Public Shared FormLoad As Boolean
        Public Shared Title As String
        Public Shared Msg As String
        Public Shared Btn_Name1 As String
        Public Shared Btn_Name2 As String
        Public Shared Btn_Name3 As String
        Public Shared MsgMode As Integer
        Public Shared BtnMode As Integer
        Public Shared MsgOk As Boolean
        Public Shared MsgCancle As Boolean
        Public Shared MsgOption As Boolean
        Public Shared FormType As FormType
    End Class

    Public Class gWaitBox
        Public Shared Title As String
        Public Shared Msg As String
    End Class
    Public Class gMES
        Public Shared MESAdd As String
        Public Shared DataSource As String
        Public Shared ID As String
        Public Shared PW As String
        Public Shared UserID As String
        Public Shared SiteCode As String
        Public Shared ProcessCode As String
        Public Shared MachineCode As String
        Public Shared MajorProcessKind As String

        Public Shared OrderDate As String
        Public Shared OrderShift As String
        Public Shared SubPno As String
        Public Shared LotNo As String
        Public Shared QtyOrder As String
        Public Shared QtyCurr As String
        Public Shared QtyResult As String
        Public Shared Status As String
        Public Shared CarCode As String
        Public Shared CarName As String
        Public Shared ItemCode As String
        Public Shared ItemName As String
        Public Shared HWVersion As String
        Public Shared SWVersion As String
        Public Shared LineCode As String
        Public Shared WorkSeqence As String
        Public Shared DateNow As String

        Public Shared JobOrder As Boolean
        Public Shared Barcode As String
        Public Shared LastBarcode As String
        Public Shared MESProg As Integer
        Public Shared LaserMakingStatus As Integer
    End Class
    Public Class gMessage
        Public Shared ErrorMesg As String = ""
        Public Shared MESStatusMesg As String
    End Class
    Public Class gUse
        Public Shared MES As Boolean
    End Class
    Public Class gLvl
        Public Shared MainHistory As Integer
        Public Shared MainSetUp As Integer
        Public Shared MainTeach As Integer
        Public Shared MainIO As Integer
        Public Shared MainJob As Integer
        Public Shared MainExit As Integer

        Public Shared HistoryDel As Integer
        Public Shared HistoryEtc As Integer
        Public Shared ErrorEdit As Integer
        Public Shared ErrorEtc As Integer
        Public Shared IOTest As Integer
        Public Shared IOEdit As Integer
        Public Shared UserLvl As Integer
        Public Shared UserEdit As Integer
    End Class
    Public Class gDir
        Public Shared AppDir As String = "D:\BCR_MARKING"
        Public Shared Setting As String = AppDir & "\Config"
        Public Shared Log As String = AppDir & "\Log"
        Public Shared History As String = AppDir & "\History"
        Public Shared Language As String = AppDir & "\Language"
        Public Shared Result As String = AppDir & "\Result"
    End Class

    Public Class gFrm
        ' Public Shared Job As frmJob
    End Class
End Namespace
