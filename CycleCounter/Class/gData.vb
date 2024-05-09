Imports System
Imports System.Text
Imports System.Data.OleDb
Imports System.IO
Imports System.Runtime.InteropServices

Namespace aaa
    Public Class gData
        <DllImport("kernel32")>
        Private Shared Function WritePrivateProfileString(ByVal section As String, ByVal key As String, ByVal val As String, ByVal filePath As String) As Long
        End Function
        <DllImport("kernel32")>
        Private Shared Function GetPrivateProfileString(ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer
        End Function

        Public Shared Sub LoadConfigData()
            Dim nLen As Integer
            Dim sLoadStr As String
            Dim sBufferStr = New String(19) {}
            Dim sParamsString As StringBuilder = New StringBuilder(80)

            Try
                ' ************************************************************************************************************************
                '                                                   MES Params
                ' ************************************************************************************************************************
                nLen = GetPrivateProfileString("MES Params", "Address", "192.168.0.1", sParamsString, 80, gDir.Setting & "\System.cfg")
                sLoadStr = sParamsString.ToString()
                gMES.MESAdd = sLoadStr.Substring(0, nLen)

                nLen = GetPrivateProfileString("MES Params", "DataSource", "Data", sParamsString, 80, gDir.Setting & "\System.cfg")
                sLoadStr = sParamsString.ToString()
                gMES.DataSource = sLoadStr.Substring(0, nLen)

                nLen = GetPrivateProfileString("MES Params", "ID", "Data", sParamsString, 80, gDir.Setting & "\System.cfg")
                sLoadStr = sParamsString.ToString()
                gMES.ID = sLoadStr.Substring(0, nLen)

                nLen = GetPrivateProfileString("MES Params", "PW", "Data", sParamsString, 80, gDir.Setting & "\System.cfg")
                sLoadStr = sParamsString.ToString()
                gMES.PW = sLoadStr.Substring(0, nLen)

                nLen = GetPrivateProfileString("MES Params", "UserID", "Data", sParamsString, 80, gDir.Setting & "\System.cfg")
                sLoadStr = sParamsString.ToString()
                gMES.UserID = sLoadStr.Substring(0, nLen)

                nLen = GetPrivateProfileString("MES Params", "SiteCode", "Data", sParamsString, 80, gDir.Setting & "\System.cfg")
                sLoadStr = sParamsString.ToString()
                gMES.SiteCode = sLoadStr.Substring(0, nLen)

                nLen = GetPrivateProfileString("MES Params", "ProcessCode", "Data", sParamsString, 80, gDir.Setting & "\System.cfg")
                sLoadStr = sParamsString.ToString()
                gMES.ProcessCode = sLoadStr.Substring(0, nLen)

                nLen = GetPrivateProfileString("MES Params", "MachineCode", "Data", sParamsString, 80, gDir.Setting & "\System.cfg")
                sLoadStr = sParamsString.ToString()
                gMES.MachineCode = sLoadStr.Substring(0, nLen)

                nLen = GetPrivateProfileString("MES Params", "MajorProcessKind", "Data", sParamsString, 80, gDir.Setting & "\System.cfg")
                sLoadStr = sParamsString.ToString()
                gMES.MajorProcessKind = sLoadStr.Substring(0, nLen)

                nLen = GetPrivateProfileString("MES Params", "Last Barcode", "0", sParamsString, 80, gDir.Setting & "\System.cfg")
                sLoadStr = sParamsString.ToString()
                gMES.LastBarcode = sLoadStr.Substring(0, nLen)

                Return
            Catch
                Return
            End Try
        End Sub

        Public Shared Sub SaveConfigData()
            Dim sParamsString As String

            Try

                If File.Exists(gDir.Setting & "\System.cfg") Then
                    'File.Delete(gDir.Setting + "\\System.cfg");
                End If
                ' ************************************************************************************************************************
                '                                                   MES Params
                ' ************************************************************************************************************************

                sParamsString = Convert.ToString(gMES.MESAdd)
                WritePrivateProfileString("MES Params", "Address", sParamsString, gDir.Setting & "\System.cfg")

                sParamsString = Convert.ToString(gMES.DataSource)
                WritePrivateProfileString("MES Params", "DataSource", sParamsString, gDir.Setting & "\System.cfg")

                sParamsString = Convert.ToString(gMES.ID)
                WritePrivateProfileString("MES Params", "ID", sParamsString, gDir.Setting & "\System.cfg")

                sParamsString = Convert.ToString(gMES.PW)
                WritePrivateProfileString("MES Params", "PW", sParamsString, gDir.Setting & "\System.cfg")

                sParamsString = Convert.ToString(gMES.UserID)
                WritePrivateProfileString("MES Params", "UserID", sParamsString, gDir.Setting & "\System.cfg")

                sParamsString = Convert.ToString(gMES.SiteCode)
                WritePrivateProfileString("MES Params", "SiteCode", sParamsString, gDir.Setting & "\System.cfg")

                sParamsString = Convert.ToString(gMES.ProcessCode)
                WritePrivateProfileString("MES Params", "ProcessCode", sParamsString, gDir.Setting & "\System.cfg")

                sParamsString = Convert.ToString(gMES.MachineCode)
                WritePrivateProfileString("MES Params", "MachineCode", sParamsString, gDir.Setting & "\System.cfg")

                sParamsString = Convert.ToString(gMES.MajorProcessKind)
                WritePrivateProfileString("MES Params", "MajorProcessKind", sParamsString, gDir.Setting & "\System.cfg")

                sParamsString = Convert.ToString(gMES.LastBarcode)
                WritePrivateProfileString("MES Params", "Last Barcode", sParamsString, gDir.Setting & "\System.cfg")

                Return
            Catch
                Return
            End Try
        End Sub

    End Class
End Namespace
