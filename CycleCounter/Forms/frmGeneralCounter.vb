Imports System.IO
Imports System.Data.SqlClient
Imports System.Net
Imports System.Reflection
Imports System.Text

Public Class frmGeneralCounter

    'contadores uptime
    Dim s1100, m1100, h1100, s1400, m1400, h1400, s1700, m1700, h1700 As Integer
    Dim s2100, m2100, h2100, s2400, m2400, h2400, s2700, m2700, h2700 As Integer
    Dim s3100, m3100, h3100, s3400, m3400, h3400, s3700, m3700, h3700 As Integer

    Private Sub t1400_Tick(sender As Object, e As EventArgs) Handles t1400.Tick
        Try
            t1400.Stop()
            getLastStatus("1400", tU1400, tD1400)
            t1400.Start()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tU1400_Tick(sender As Object, e As EventArgs) Handles tU1400.Tick
        Try
            l1400.Tipo = UCStatus.UCLed.Type.Online
            s1400 += 1
            If s1400 >= 60 Then
                s1400 = 0
                m1400 += 1
            End If

            If m1400 >= 60 Then
                m1400 = 0
                h1400 += 1
            End If

            u1400.Text = h1400.ToString("D2") & ":" & m1400.ToString("D2") & ":" & s1400.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tD1400_Tick(sender As Object, e As EventArgs) Handles tD1400.Tick
        Try
            l1400.Tipo = UCStatus.UCLed.Type.Panic
            s1400d += 1
            If s1400d >= 60 Then
                s1400d = 0
                m1400d += 1
            End If

            If m1400d >= 60 Then
                m1400d = 0
                h1400d += 1
            End If

            d1400.Text = h1400d.ToString("D2") & ":" & m1400d.ToString("D2") & ":" & s1400d.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub u1100_DoubleClick(sender As Object, e As EventArgs) Handles u1100.DoubleClick
        Try
            Dim frm As New frmHistory("1100")
            frm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub u1400_DoubleClick(sender As Object, e As EventArgs) Handles u1400.DoubleClick
        Try
            Dim frm As New frmHistory("1400")
            frm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub u1700_DoubleClick(sender As Object, e As EventArgs) Handles u1700.DoubleClick
        Try
            Dim frm As New frmHistory("1700")
            frm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub t2100_Tick(sender As Object, e As EventArgs) Handles t2100.Tick
        Try
            t2100.Stop()
            getLastStatus("2100", tU2100, tD2100)
            t2100.Start()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tU2100_Tick(sender As Object, e As EventArgs) Handles tU2100.Tick
        Try
            l2100.Tipo = UCStatus.UCLed.Type.Online
            s2100 += 1
            If s2100 >= 60 Then
                s2100 = 0
                m2100 += 1
            End If

            If m2100 >= 60 Then
                m2100 = 0
                h2100 += 1
            End If

            u2100.Text = h2100.ToString("D2") & ":" & m2100.ToString("D2") & ":" & s2100.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tD2100_Tick(sender As Object, e As EventArgs) Handles tD2100.Tick
        Try
            l2100.Tipo = UCStatus.UCLed.Type.Panic
            s2100d += 1
            If s2100d >= 60 Then
                s2100d = 0
                m2100d += 1
            End If

            If m2100d >= 60 Then
                m2100d = 0
                h2100d += 1
            End If

            d2100.Text = h2100d.ToString("D2") & ":" & m2100d.ToString("D2") & ":" & s2100d.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub t2400_Tick(sender As Object, e As EventArgs) Handles t2400.Tick
        Try
            t2400.Stop()
            getLastStatus("2400", tU2400, tD2400)
            t2400.Start()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tU2400_Tick(sender As Object, e As EventArgs) Handles tU2400.Tick
        Try
            l2400.Tipo = UCStatus.UCLed.Type.Online
            s2400 += 1
            If s2400 >= 60 Then
                s2400 = 0
                m2400 += 1
            End If

            If m2400 >= 60 Then
                m2400 = 0
                h2400 += 1
            End If

            u2400.Text = h2400.ToString("D2") & ":" & m2400.ToString("D2") & ":" & s2400.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tD2400_Tick(sender As Object, e As EventArgs) Handles tD2400.Tick
        Try
            l2400.Tipo = UCStatus.UCLed.Type.Panic
            s2400d += 1
            If s2400d >= 60 Then
                s2400d = 0
                m2400d += 1
            End If

            If m2400d >= 60 Then
                m2400d = 0
                h2400d += 1
            End If

            d2400.Text = h2400d.ToString("D2") & ":" & m2400d.ToString("D2") & ":" & s2400d.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub t2700_Tick(sender As Object, e As EventArgs) Handles t2700.Tick
        Try
            t2700.Stop()
            getLastStatus("2700", tU2700, tD2700)
            t2700.Start()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tU2700_Tick(sender As Object, e As EventArgs) Handles tU2700.Tick
        Try
            l2700.Tipo = UCStatus.UCLed.Type.Online
            s2700 += 1
            If s2700 >= 60 Then
                s2700 = 0
                m2700 += 1
            End If

            If m2700 >= 60 Then
                m2700 = 0
                h2700 += 1
            End If

            u2700.Text = h2700.ToString("D2") & ":" & m2700.ToString("D2") & ":" & s2700.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tD2700_Tick(sender As Object, e As EventArgs) Handles tD2700.Tick
        Try
            l2700.Tipo = UCStatus.UCLed.Type.Panic
            s2700d += 1
            If s2700d >= 60 Then
                s2700d = 0
                m2700d += 1
            End If

            If m2700d >= 60 Then
                m2700d = 0
                h2700d += 1
            End If

            d2700.Text = h2700d.ToString("D2") & ":" & m2700d.ToString("D2") & ":" & s2700d.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub t3100_Tick(sender As Object, e As EventArgs) Handles t3100.Tick
        Try
            t3100.Stop()
            getLastStatus("3100", tU3100, tD3100)
            t3100.Start()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tU3100_Tick(sender As Object, e As EventArgs) Handles tU3100.Tick
        Try
            l3100.Tipo = UCStatus.UCLed.Type.Online
            s3100 += 1
            If s3100 >= 60 Then
                s3100 = 0
                m3100 += 1
            End If

            If m3100 >= 60 Then
                m3100 = 0
                h3100 += 1
            End If

            u3100.Text = h3100.ToString("D2") & ":" & m3100.ToString("D2") & ":" & s3100.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tD3100_Tick(sender As Object, e As EventArgs) Handles tD3100.Tick
        Try
            l3100.Tipo = UCStatus.UCLed.Type.Panic
            s3100d += 1
            If s3100d >= 60 Then
                s3100d = 0
                m3100d += 1
            End If

            If m3100d >= 60 Then
                m3100d = 0
                h3100d += 1
            End If

            d3100.Text = h3100d.ToString("D2") & ":" & m3100d.ToString("D2") & ":" & s3100d.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub t3400_Tick(sender As Object, e As EventArgs) Handles t3400.Tick
        Try
            t3400.Stop()
            getLastStatus("3400", tU3400, tD3400)
            t3400.Start()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tU3400_Tick(sender As Object, e As EventArgs) Handles tU3400.Tick
        Try
            l3400.Tipo = UCStatus.UCLed.Type.Online
            s3400 += 1
            If s3400 >= 60 Then
                s3400 = 0
                m3400 += 1
            End If

            If m3400 >= 60 Then
                m3400 = 0
                h3400 += 1
            End If

            u3400.Text = h3400.ToString("D2") & ":" & m3400.ToString("D2") & ":" & s3400.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tD3400_Tick(sender As Object, e As EventArgs) Handles tD3400.Tick
        Try
            l3400.Tipo = UCStatus.UCLed.Type.Panic
            s3400d += 1
            If s3400d >= 60 Then
                s3400d = 0
                m3400d += 1
            End If

            If m3400d >= 60 Then
                m3400d = 0
                h3400d += 1
            End If

            d3400.Text = h3400d.ToString("D2") & ":" & m3400d.ToString("D2") & ":" & s3400d.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub t3700_Tick(sender As Object, e As EventArgs) Handles t3700.Tick
        Try
            t3700.Stop()
            getLastStatus("3700", tU3700, tD3700)
            t3700.Start()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tU3700_Tick(sender As Object, e As EventArgs) Handles tU3700.Tick
        Try
            l3700.Tipo = UCStatus.UCLed.Type.Online
            s3700 += 1
            If s3700 >= 60 Then
                s3700 = 0
                m3700 += 1
            End If

            If m3700 >= 60 Then
                m3700 = 0
                h3700 += 1
            End If

            u3700.Text = h3700.ToString("D2") & ":" & m3700.ToString("D2") & ":" & s3700.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tD3700_Tick(sender As Object, e As EventArgs) Handles tD3700.Tick
        Try
            l3700.Tipo = UCStatus.UCLed.Type.Panic
            s3700d += 1
            If s3700d >= 60 Then
                s3700d = 0
                m3700d += 1
            End If

            If m3700d >= 60 Then
                m3700d = 0
                h3700d += 1
            End If

            d3700.Text = h3700d.ToString("D2") & ":" & m3700d.ToString("D2") & ":" & s3700d.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub t4100_Tick(sender As Object, e As EventArgs) Handles t4100.Tick
        Try
            t4100.Stop()
            getLastStatus("4100", tU4100, tD4100)
            t4100.Start()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tU4100_Tick(sender As Object, e As EventArgs) Handles tU4100.Tick
        Try
            l4100.Tipo = UCStatus.UCLed.Type.Online
            s4100 += 1
            If s4100 >= 60 Then
                s4100 = 0
                m4100 += 1
            End If

            If m4100 >= 60 Then
                m4100 = 0
                h4100 += 1
            End If

            u4100.Text = h4100.ToString("D2") & ":" & m4100.ToString("D2") & ":" & s4100.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tD4100_Tick(sender As Object, e As EventArgs) Handles tD4100.Tick
        Try
            l4100.Tipo = UCStatus.UCLed.Type.Panic
            s4100d += 1
            If s4100d >= 60 Then
                s4100d = 0
                m4100d += 1
            End If

            If m4100d >= 60 Then
                m4100d = 0
                h4100d += 1
            End If

            d4100.Text = h4100d.ToString("D2") & ":" & m4100d.ToString("D2") & ":" & s4100d.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub t4400_Tick(sender As Object, e As EventArgs) Handles t4400.Tick
        Try
            t4400.Stop()
            getLastStatus("4400", tU4400, tD4400)
            t4400.Start()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tU4400_Tick(sender As Object, e As EventArgs) Handles tU4400.Tick
        Try
            l4400.Tipo = UCStatus.UCLed.Type.Online
            s4400 += 1
            If s4400 >= 60 Then
                s4400 = 0
                m4400 += 1
            End If

            If m4400 >= 60 Then
                m4400 = 0
                h4400 += 1
            End If

            u4400.Text = h4400.ToString("D2") & ":" & m4400.ToString("D2") & ":" & s4400.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tD4400_Tick(sender As Object, e As EventArgs) Handles tD4400.Tick
        Try
            l4400.Tipo = UCStatus.UCLed.Type.Panic
            s4400d += 1
            If s4400d >= 60 Then
                s4400d = 0
                m4400d += 1
            End If

            If m4400d >= 60 Then
                m4400d = 0
                h4400d += 1
            End If

            d4400.Text = h4400d.ToString("D2") & ":" & m4400d.ToString("D2") & ":" & s4400d.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub t4700_Tick(sender As Object, e As EventArgs) Handles t4700.Tick
        Try
            t4700.Stop()
            getLastStatus("4700", tU4700, tD4700)
            t4700.Start()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tU4700_Tick(sender As Object, e As EventArgs) Handles tU4700.Tick
        Try
            l4700.Tipo = UCStatus.UCLed.Type.Online
            s4700 += 1
            If s4700 >= 60 Then
                s4700 = 0
                m4700 += 1
            End If

            If m4700 >= 60 Then
                m4700 = 0
                h4700 += 1
            End If

            u4700.Text = h4700.ToString("D2") & ":" & m4700.ToString("D2") & ":" & s4700.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tD4700_Tick(sender As Object, e As EventArgs) Handles tD4700.Tick
        Try
            l4700.Tipo = UCStatus.UCLed.Type.Panic
            s4700d += 1
            If s4700d >= 60 Then
                s4700d = 0
                m4700d += 1
            End If

            If m4700d >= 60 Then
                m4700d = 0
                h4700d += 1
            End If

            d4700.Text = h4700d.ToString("D2") & ":" & m4700d.ToString("D2") & ":" & s4700d.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub t5100_Tick(sender As Object, e As EventArgs) Handles t5100.Tick
        Try
            t5100.Stop()
            getLastStatus("5100", tU5100, tD5100)
            t5100.Start()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tU5100_Tick(sender As Object, e As EventArgs) Handles tU5100.Tick
        Try
            l5100.Tipo = UCStatus.UCLed.Type.Online
            s5100 += 1
            If s5100 >= 60 Then
                s5100 = 0
                m5100 += 1
            End If

            If m5100 >= 60 Then
                m5100 = 0
                h5100 += 1
            End If

            u5100.Text = h5100.ToString("D2") & ":" & m5100.ToString("D2") & ":" & s5100.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tD5100_Tick(sender As Object, e As EventArgs) Handles tD5100.Tick
        Try
            l5100.Tipo = UCStatus.UCLed.Type.Panic
            s5100d += 1
            If s5100d >= 60 Then
                s5100d = 0
                m5100d += 1
            End If

            If m5100d >= 60 Then
                m5100d = 0
                h5100d += 1
            End If

            d5100.Text = h5100d.ToString("D2") & ":" & m5100d.ToString("D2") & ":" & s5100d.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub t5400_Tick(sender As Object, e As EventArgs) Handles t5400.Tick
        Try
            t5400.Stop()
            getLastStatus("5400", tU5400, tD5400)
            t5400.Start()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tU5400_Tick(sender As Object, e As EventArgs) Handles tU5400.Tick
        Try
            l5400.Tipo = UCStatus.UCLed.Type.Online
            s5400 += 1
            If s5400 >= 60 Then
                s5400 = 0
                m5400 += 1
            End If

            If m5400 >= 60 Then
                m5400 = 0
                h5400 += 1
            End If

            u5400.Text = h5400.ToString("D2") & ":" & m5400.ToString("D2") & ":" & s5400.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tD5400_Tick(sender As Object, e As EventArgs) Handles tD5400.Tick
        Try
            l5400.Tipo = UCStatus.UCLed.Type.Panic
            s5400d += 1
            If s5400d >= 60 Then
                s5400d = 0
                m5400d += 1
            End If

            If m5400d >= 60 Then
                m5400d = 0
                h5400d += 1
            End If

            d5400.Text = h5400d.ToString("D2") & ":" & m5400d.ToString("D2") & ":" & s5400d.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub t5700_Tick(sender As Object, e As EventArgs) Handles t5700.Tick
        Try
            t5700.Stop()
            getLastStatus("5700", tU5700, tD5700)
            t5700.Start()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tU5700_Tick(sender As Object, e As EventArgs) Handles tU5700.Tick
        Try
            l5700.Tipo = UCStatus.UCLed.Type.Online
            s5700 += 1
            If s5700 >= 60 Then
                s5700 = 0
                m5700 += 1
            End If

            If m5700 >= 60 Then
                m5700 = 0
                h5700 += 1
            End If

            u5700.Text = h5700.ToString("D2") & ":" & m5700.ToString("D2") & ":" & s5700.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tD5700_Tick(sender As Object, e As EventArgs) Handles tD5700.Tick
        Try
            l5700.Tipo = UCStatus.UCLed.Type.Panic
            s5700d += 1
            If s5700d >= 60 Then
                s5700d = 0
                m5700d += 1
            End If

            If m5700d >= 60 Then
                m5700d = 0
                h5700d += 1
            End If

            d5700.Text = h5700d.ToString("D2") & ":" & m5700d.ToString("D2") & ":" & s5700d.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub u2100_DoubleClick(sender As Object, e As EventArgs) Handles u2100.DoubleClick
        Try
            Dim frm As New frmHistory("2100")
            frm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub u2400_DoubleClick(sender As Object, e As EventArgs) Handles u2400.DoubleClick
        Try
            Dim frm As New frmHistory("2400")
            frm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub u2700_DoubleClick(sender As Object, e As EventArgs) Handles u2700.DoubleClick
        Try
            Dim frm As New frmHistory("2700")
            frm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub u3100_DoubleClick(sender As Object, e As EventArgs) Handles u3100.DoubleClick
        Try
            Dim frm As New frmHistory("3100")
            frm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub u3400_DoubleClick(sender As Object, e As EventArgs) Handles u3400.DoubleClick
        Try
            Dim frm As New frmHistory("3400")
            frm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub u3700_DoubleClick(sender As Object, e As EventArgs) Handles u3700.DoubleClick
        Try
            Dim frm As New frmHistory("3700")
            frm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub u4100_DoubleClick(sender As Object, e As EventArgs) Handles u4100.DoubleClick
        Try
            Dim frm As New frmHistory("4100")
            frm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub u4400_DoubleClick(sender As Object, e As EventArgs) Handles u4400.DoubleClick
        Try
            Dim frm As New frmHistory("4400")
            frm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub u4700_DoubleClick(sender As Object, e As EventArgs) Handles u4700.DoubleClick
        Try
            Dim frm As New frmHistory("4700")
            frm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub u5100_DoubleClick(sender As Object, e As EventArgs) Handles u5100.DoubleClick
        Try
            Dim frm As New frmHistory("5100")
            frm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub u5400_DoubleClick(sender As Object, e As EventArgs) Handles u5400.DoubleClick
        Try
            Dim frm As New frmHistory("5400")
            frm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub u5700_DoubleClick(sender As Object, e As EventArgs) Handles u5700.DoubleClick
        Try
            Dim frm As New frmHistory("5700")
            frm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim frm As New frmDetails("M1")
            frm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tD1700_Tick(sender As Object, e As EventArgs) Handles tD1700.Tick
        Try
            l1700.Tipo = UCStatus.UCLed.Type.Panic
            s1700d += 1
            If s1700d >= 60 Then
                s1700d = 0
                m1700d += 1
            End If

            If m1700d >= 60 Then
                m1700d = 0
                h1700d += 1
            End If

            d1700.Text = h1700d.ToString("D2") & ":" & m1700d.ToString("D2") & ":" & s1700d.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tU1700_Tick(sender As Object, e As EventArgs) Handles tU1700.Tick
        Try
            l1700.Tipo = UCStatus.UCLed.Type.Online
            s1700 += 1
            If s1700 >= 60 Then
                s1700 = 0
                m1700 += 1
            End If

            If m1700 >= 60 Then
                m1700 = 0
                h1700 += 1
            End If

            u1700.Text = h1700.ToString("D2") & ":" & m1700.ToString("D2") & ":" & s1700.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub t1700_Tick(sender As Object, e As EventArgs) Handles t1700.Tick
        Try
            t1700.Stop()
            getLastStatus("1700", tU1700, tD1700)
            t1700.Start()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnLine_Click(sender As Object, e As EventArgs) Handles btnLine.Click
        Dim frm As New frmMonitor()
        frm.Show()
    End Sub

    Dim s4100, m4100, h4100, s4400, m4400, h4400, s4700, m4700, h4700 As Integer
    Dim s5100, m5100, h5100, s5400, m5400, h5400, s5700, m5700, h5700 As Integer

    'contadores dowtime
    Dim s1100d, m1100d, h1100d, s1400d, m1400d, h1400d, s1700d, m1700d, h1700d As Integer
    Dim s2100d, m2100d, h2100d, s2400d, m2400d, h2400d, s2700d, m2700d, h2700d As Integer
    Dim s3100d, m3100d, h3100d, s3400d, m3400d, h3400d, s3700d, m3700d, h3700d As Integer
    Dim s4100d, m4100d, h4100d, s4400d, m4400d, h4400d, s4700d, m4700d, h4700d As Integer
    Dim s5100d, m5100d, h5100d, s5400d, m5400d, h5400d, s5700d, m5700d, h5700d As Integer

    Dim query As String = ""
    Dim lastStatus As String = ""
    Dim n As Integer = 0
    Dim lastCode As String = ""

    Private Const CEROS_MAX = 1

    Public Function ceros(Nro As String, Cantidad As Integer) As String
        Try
            Dim numero As String, cuantos As String, i As Integer
            numero = Trim(Nro)
            cuantos = "0"
            For i = 1 To Cantidad
                cuantos = cuantos & "0"
            Next i
            Return Mid(cuantos, 1, Cantidad - Len(numero)) & numero
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Private Sub tU1100_Tick(sender As Object, e As EventArgs) Handles tU1100.Tick
        Try
            l1100.Tipo = UCStatus.UCLed.Type.Online
            s1100 += 1
            If s1100 >= 60 Then
                s1100 = 0
                m1100 += 1
            End If

            If m1100 >= 60 Then
                m1100 = 0
                h1100 += 1
            End If
            u1100.Text = h1100.ToString("D2") & ":" & m1100.ToString("D2") & ":" & s1100.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tD1100_Tick(sender As Object, e As EventArgs) Handles tD1100.Tick
        Try
            l1100.Tipo = UCStatus.UCLed.Type.Panic
            s1100d += 1
            If s1100d >= 60 Then
                s1100d = 0
                m1100d += 1
            End If

            If m1100d >= 60 Then
                m1100d = 0
                h1100d += 1
            End If

            d1100.Text = h1100d.ToString("D2") & ":" & m1100d.ToString("D2") & ":" & s1100d.ToString("D2")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tm1100_Tick(sender As Object, e As EventArgs) Handles tm1100.Tick
        Try
            tm1100.Stop()
            getLastStatus("1100", tU1100, tD1100)
            tm1100.Start()
        Catch ex As Exception

        End Try
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
                        conexion.ConnectionString = "Initial Catalog=db_kyungshin;Data Source=ARTURO-GARCIA\SQLEXPRESS; Integrated Security=True"
                    Else
                        conexion.ConnectionString = "Initial Catalog=db_kyungshin;Data Source=LAPTOP-ROUA60BG; Integrated Security=True"
                    End If
                Else
                    conexion.ConnectionString = "Initial Catalog=db_kyungshin;Data Source=172.30.61.20; User Id=sistemas;pwd=kyungshin2!"
                End If

                conexion.Open()

                'CheckWMS_Connection()
                'CheckWMSDT_Connection()

            Catch ex As Exception
                MessageBox.Show("Can't connect to database. " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Application.Exit()
            End Try

            'cbUsers.Select()
            'cbUsers.Focus()
            'tUsuarios.LoadUsersInComboBox(cbUsers, "SELECT usuario FROM t_usuarios  ORDER BY usuario")
            'Me.Text = "version " & My.Application.Info.Version.ToString
            'If GetComputerName() = "Aplicaciones" Or GetComputerName() = "CPLANTA133SIS" Then
            '    cbUsers.Text = "Administrador"
            '    tbPass.Text = "2404"
            'End If

        Catch ex As Exception
            MessageBox.Show("No se puede conectar al host. " & ex.ToString, "Error de conexión al iniciar sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit()
        End Try
    End Sub
    Private Sub frmGeneralCounter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            loadBD()

            Dim versionCore As String = QueryRow("SELECT valor From t_parametros WHERE parametro='vMonitoring'", "valor", "version3134")
            If My.Application.Info.Version.ToString <> versionCore Then
                'MessageBox.Show("There is a new version available to download. Update the CORE system.")
                Dim ouputFileName As String = Environment.CurrentDirectory & "\Updater.exe"
                System.Diagnostics.Process.Start(ouputFileName)
                Application.Exit()
            End If


            Dim mut As System.Threading.Mutex = New System.Threading.Mutex(False, Application.ProductName)
            Dim running As Boolean = Not mut.WaitOne(0, False)
            If running Then
                Application.ExitThread()
                Return
            End If
            getAcumTime("1100", s1100, m1100, h1100, s1100d, m1100d, h1100d, u1100, d1100)
            getAcumTime("1700", s1700, m1700, h1700, s1700d, m1700d, h1700d, u1700, d1700)
            getAcumTime("1400", s1400, m1400, h1400, s1400d, m1400d, h1400d, u1400, d1400)

            getAcumTime("2100", s2100, m2100, h2100, s2100d, m2100d, h2100d, u2100, d2100)
            getAcumTime("2700", s2700, m2700, h2700, s2700d, m2700d, h2700d, u2700, d2700)
            getAcumTime("2400", s2400, m2400, h2400, s2400d, m2400d, h2400d, u2400, d2400)

            getAcumTime("3100", s3100, m3100, h3100, s3100d, m3100d, h3100d, u3100, d3100)
            getAcumTime("3700", s3700, m3700, h3700, s3700d, m3700d, h3700d, u3700, d3700)
            getAcumTime("3400", s3400, m3400, h3400, s3400d, m3400d, h3400d, u3400, d3400)


            getAcumTime("4100", s4100, m4100, h4100, s4100d, m4100d, h4100d, u4100, d4100)
            getAcumTime("4700", s4700, m4700, h4700, s4700d, m4700d, h4700d, u4700, d4700)
            getAcumTime("4400", s4400, m4400, h4400, s4400d, m4400d, h4400d, u4400, d4400)

            getAcumTime("5100", s5100, m5100, h5100, s5100d, m5100d, h5100d, u5100, d5100)
            getAcumTime("5700", s5700, m5700, h5700, s5700d, m5700d, h5700d, u5700, d5700)
            getAcumTime("5400", s5400, m5400, h5400, s5400d, m5400d, h5400d, u5400, d5400)

            tm1100.Start()
            t1700.Start()
            t1400.Start()

            t2100.Start()
            t2400.Start()
            t2700.Start()

            t3100.Start()
            t3400.Start()
            t3700.Start()

            t4100.Start()
            t4400.Start()
            t4700.Start()

            t5100.Start()
            t5400.Start()
            t5700.Start()


            l1100.IsRunning = True
            l1100.Start()

            l1400.IsRunning = True
            l1400.Start()

            l1700.IsRunning = True
            l1700.Start()

            l2100.IsRunning = True
            l2100.Start()

            l2400.IsRunning = True
            l2400.Start()

            l2700.IsRunning = True
            l2700.Start()

            l3100.IsRunning = True
            l3100.Start()

            l3400.IsRunning = True
            l3400.Start()

            l3700.IsRunning = True
            l3700.Start()

            l4100.IsRunning = True
            l4100.Start()

            l4400.IsRunning = True
            l4400.Start()

            l4700.IsRunning = True
            l4700.Start()

            l5100.IsRunning = True
            l5100.Start()

            l5400.IsRunning = True
            l5400.Start()

            l5700.IsRunning = True
            l5700.Start()

            Me.Text = "Version " & My.Application.Info.Version.ToString
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub


    Private Sub getAcumTime(NumProceso As String, s As Integer, m As Integer, h As Integer, sd As Integer,
                            md As Integer, hd As Integer, up As TextBox, down As TextBox)
        Dim queryCompl As String = ""

        If NumProceso <> "" Then
            queryCompl = " AND d.process='" & NumProceso & "' "
        End If

        query = "SELECT  [id]
                      ,d.process
                      ,[machine]
                      ,[status],startTime
                      ,[AcumTime]
                  FROM [db_kyungshin].[dbo].[t_bma_downtime] d
                  where  CONVERT(date, insert_date)='" & Date.Now.ToShortDateString() & "' " & queryCompl & " order by id asc"
        dgvAux.DataSource = EjecutaSelects(query, "fillAux")
        ResizeCols(dgvAux)
        'Dim horaSep() As String
        'Dim hora As String
        Dim segundos As Integer = 0

        Dim horaSeparada() As String
        Dim horaSeparadaaux() As String

        Try

            horaSeparada = Split(QueryRow("SELECT DATEADD(ms, SUM(DATEDIFF(ms, '00:00:00', acumtime)), '00:00:00') as time FROM [t_bma_downtime] d 
  where CONVERT(date, insert_date) = '" & Date.Now.ToShortDateString() & "' and status='RUN' " & queryCompl & "", "time", "buscaDT"), " ")
            If horaSeparada(0) = "" Then
                up.Text = "00:00:00"
            Else
                horaSeparadaaux = Split(horaSeparada(1), ":")
                s += CInt(horaSeparadaaux(2))
                m += CInt(horaSeparadaaux(1))
                h += CInt(horaSeparadaaux(0))
                up.Text = h.ToString("D2") & ":" & m.ToString("D2") & ":" & s.ToString("D2")
            End If

        Catch ex As Exception
            MsgBox("Error 1" & "aux" & horaSeparadaaux(1))
        End Try

        Try
            horaSeparada = Split(QueryRow("SELECT DATEADD(ms, SUM(DATEDIFF(ms, '00:00:00', acumtime)), '00:00:00') as time FROM [t_bma_downtime] d 
  where CONVERT(date, insert_date) = '" & Date.Now.ToShortDateString() & "' and status='DOWN' " & queryCompl & "", "time", "buscaDT"), " ")
            If horaSeparada(0) = "" Then
                down.Text = "00:00:00"
            Else
                horaSeparadaaux = Split(horaSeparada(1), ":")
                sd += CInt(horaSeparadaaux(2))
                md += CInt(horaSeparadaaux(1))
                hd += CInt(horaSeparadaaux(0))
                down.Text = hd.ToString("D2") & ":" & md.ToString("D2") & ":" & sd.ToString("D2")
            End If


        Catch ex As Exception
            MsgBox("Error 2" & "aux" & horaSeparadaaux(1))
        End Try


        Select Case NumProceso
            Case "1100"
                s1100 = s
                m1100 = m
                h1100 = h
                s1100d = sd
                m1100d = md
                h1100d = hd
            Case "1400"
                s1400 = s
                m1400 = m
                h1400 = h
                s1400d = sd
                m1400d = md
                h1400d = hd

            Case "1700"
                s1700 = s
                m1700 = m
                h1700 = h
                s1700d = sd
                m1700d = md
                h1700d = hd

            Case "2100"
                s2100 = s
                m2100 = m
                h2100 = h
                s2100d = sd
                m2100d = md
                h2100d = hd
            Case "2400"
                s2400 = s
                m2400 = m
                h2400 = h
                s2400d = sd
                m2400d = md
                h2400d = hd

            Case "2700"
                s2700 = s
                m2700 = m
                h2700 = h
                s2700d = sd
                m2700d = md
                h2700d = hd

            Case "3100"
                s3100 = s
                m3100 = m
                h3100 = h
                s3100d = sd
                m3100d = md
                h3100d = hd
            Case "3400"
                s3400 = s
                m3400 = m
                h3400 = h
                s3400d = sd
                m3400d = md
                h3400d = hd

            Case "3700"
                s3700 = s
                m3700 = m
                h3700 = h
                s3700d = sd
                m3700d = md
                h3700d = hd

            Case "4100"
                s4100 = s
                m4100 = m
                h4100 = h
                s4100d = sd
                m4100d = md
                h4100d = hd
            Case "4400"
                s4400 = s
                m4400 = m
                h4400 = h
                s4400d = sd
                m4400d = md
                h4400d = hd

            Case "4700"
                s4700 = s
                m4700 = m
                h4700 = h
                s4700d = sd
                m4700d = md
                h4700d = hd

            Case "5100"
                s5100 = s
                m5100 = m
                h5100 = h
                s5100d = sd
                m5100d = md
                h5100d = hd
            Case "5400"
                s5400 = s
                m5400 = m
                h5400 = h
                s5400d = sd
                m5400d = md
                h5400d = hd

            Case "5700"
                s5700 = s
                m5700 = m
                h5700 = h
                s5700d = sd
                m5700d = md
                h5700d = hd
        End Select
    End Sub
    Private Sub getLastStatus(numProceso As String, timerU As Timer, timerD As Timer)
        Dim queryCompl As String = ""

        If numProceso <> "" Then
            queryCompl = " AND d.process='" & numProceso & "' "
        End If

        query = "select top 1 status FROM [db_kyungshin].[dbo].[t_bma_downtime] d 
WHERE  CONVERT(date, insert_date) = '" & Date.Now.ToShortDateString() & "'  " & queryCompl & " 
  order by id desc"

        lastStatus = QueryRow(query, "status", "statusEnd")

        If lastStatus = "RUN" Then
            n = 1
            Select Case numProceso
                Case "1100"
                    tD1100.Stop()
                    tU1100.Start()
                Case "1400"
                    tD1400.Stop()
                    tU1400.Start()
                Case "1700"
                    tD1700.Stop()
                    tU1700.Start()

                Case "2100"
                    tD2100.Stop()
                    tU2100.Start()
                Case "2400"
                    tD2400.Stop()
                    tU2400.Start()
                Case "2700"
                    tD2700.Stop()
                    tU2700.Start()


                Case "3100"
                    tD3100.Stop()
                    tU3100.Start()
                Case "3400"
                    tD3400.Stop()
                    tU3400.Start()
                Case "3700"
                    tD3700.Stop()
                    tU3700.Start()

                Case "4100"
                    tD4100.Stop()
                    tU4100.Start()
                Case "4400"
                    tD4400.Stop()
                    tU4400.Start()
                Case "4700"
                    tD4700.Stop()
                    tU4700.Start()

                Case "5100"
                    tD5100.Stop()
                    tU5100.Start()
                Case "5400"
                    tD5400.Stop()
                    tU5400.Start()
                Case "5700"
                    tD5700.Stop()
                    tU5700.Start()

            End Select

        Else

            Select Case numProceso
                Case "1100"
                    tD1100.Start()
                    tU1100.Stop()
                Case "1400"
                    tD1400.Start()
                    tU1400.Stop()
                Case "1700"
                    tD1700.Start()
                    tU1700.Stop()

                Case "2100"
                    tD2100.Start()
                    tU2100.Stop()
                Case "2400"
                    tD2400.Start()
                    tU2400.Stop()
                Case "2700"
                    tD2700.Start()
                    tU2700.Stop()

                Case "3100"
                    tD3100.Start()
                    tU3100.Stop()
                Case "3400"
                    tD3400.Start()
                    tU3400.Stop()
                Case "3700"
                    tD3700.Start()
                    tU3700.Stop()

                Case "4100"
                    tD4100.Start()
                    tU4100.Stop()
                Case "4400"
                    tD4400.Start()
                    tU4400.Stop()
                Case "4700"
                    tD4700.Start()
                    tU4700.Stop()

                Case "5100"
                    tD5100.Start()
                    tU5100.Stop()
                Case "5400"
                    tD5400.Start()
                    tU5400.Stop()
                Case "5700"
                    tD5700.Start()
                    tU5700.Stop()
            End Select
            n = 0

        End If

    End Sub

End Class