Imports System.Data.SqlClient
Imports System.IO

Public Class frmScreenshots

    Dim id As String
    Private Sub frmScreenshots_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpDesde.Value = Date.Now.ToShortDateString
    End Sub

    Private Sub MostrarImagenDesdeBaseDeDatos(fecha As String)


        ' 2. Crear el comando SQL
        Using comando As New SqlCommand("SELECT Imagen FROM t_bma_downtime_sc WHERE fecha = @id", conexion)
            comando.Parameters.AddWithValue("@id", fecha)

            Using lector As SqlDataReader = comando.ExecuteReader()
                If lector.Read() Then
                    ' 4. Verificar si la columna Imagen no es DBNull
                    If Not IsDBNull(lector("Imagen")) Then
                        ' 5. Obtener el array de bytes de la imagen
                        Dim imagenBytes As Byte() = DirectCast(lector("Imagen"), Byte())

                        ' 6. Convertir el array de bytes a una imagen
                        Using ms As New MemoryStream(imagenBytes)
                            Dim imagen As Bitmap = New Bitmap(ms)

                            ' 7. Mostrar la imagen en el PictureBox
                            PictureBox5.Image = imagen
                        End Using
                    Else
                        ' Manejar el caso en que la columna Imagen es DBNull
                        PictureBox5.Image = Nothing ' O podrías mostrar una imagen predeterminada
                        MessageBox.Show("No images were found for the specified date.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    ' Manejar el caso en que no se encuentra ninguna fila con el ID especificado
                    PictureBox5.Image = Nothing ' O podrías mostrar una imagen predeterminada
                    MessageBox.Show("No images were found for the specified date.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using
        End Using

    End Sub

    Private Sub bntFiltrar_Click(sender As Object, e As EventArgs) Handles bntFiltrar.Click
        MostrarImagenDesdeBaseDeDatos(ConvierteAdateMySQL(dtpDesde.Value.ToShortDateString))
    End Sub
End Class