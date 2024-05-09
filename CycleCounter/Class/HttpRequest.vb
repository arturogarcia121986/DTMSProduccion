Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Text

Public Class HttpRequest
    Public Shared Function InvokeWebService(ByVal mURL As String, ByVal strSoap As String, ByRef xmlresponse As String) As Boolean

        Dim HttpWResponse As HttpWebResponse = Nothing
        Dim HttpWRequest As HttpWebRequest = Nothing

        Try
            Dim myUri As System.Uri = New System.Uri(mURL)
            HttpWRequest = WebRequest.Create(myUri)

            HttpWRequest.Method = "POST"
            HttpWRequest.Headers.Add("SOAPAction", "asd")
            HttpWRequest.ContentType = "text/xml; encoding='utf-8'"

            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(strSoap)
            HttpWRequest.ContentLength = byteArray.Length

            Dim dataStream As Stream = HttpWRequest.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()

            HttpWResponse = HttpWRequest.GetResponse()

            If (HttpWResponse.StatusCode = 200) Then
                Dim strm As Stream = HttpWResponse.GetResponseStream()
                Dim sr As StreamReader = New StreamReader(strm)
                xmlresponse = sr.ReadToEnd()
                strm.Close()
                strm = Nothing
                sr = Nothing

                Return True
            Else
                Console.WriteLine("Error al invocar el servicio :" & HttpWResponse.StatusDescription.ToString())
                Return False
            End If

        Catch ex As Exception
            Console.WriteLine("Excepción al invocar el servicio :" & ex.Message.ToString)
            Return False
        Finally
            HttpWRequest = Nothing
            HttpWResponse = Nothing
        End Try
    End Function
    Public Shared Function GetStringBetween(ByVal tagName As String, ByVal xml As String) As String

        Dim rx As New Regex("(?<=\<" & tagName & "\>).+(?=\<\/" & tagName & "\>)")
        Return (rx.Match(xml).Value)
    End Function
End Class
