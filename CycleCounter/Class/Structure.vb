Module Structures

    Public Structure E_DATOS_CORREO_BACKUP
        Public usuario_PV As String
        Public usuario_Corte As String
    End Structure

    Public Structure Archivo_CFG_DAT
        Public host As String
        Public db_name As String
        Public user As String
        Public pass As String
        Public port As String
        Public printer_reportes As String
        Public printer_default As String
    End Structure
    Public Structure Precios
        Public Columna_Precio As String
        Public Columna_PrecioMin As String
        Public ClaveSucursal As String
    End Structure

    Public Structure xmlConfig
        Public host As String
        Public bascula As String
    End Structure

    Public Structure LectorDigital
        Public Registration_number As String
        Public Verification_code As String
        Public Activation_code As String
        Public Verification_key As String
        Public Serial_number As String
    End Structure

    Public Structure E_PARAMETROS
        Public taxis As String
        Public AR15 As String
        Public AR1M As String
        Public AR3M As String
        Public AR6M As String
        Public AR1A As String
        Public AR1AS As String
        Public carga As String
        Public comercial As String
        Public residencial As String
        Public abanderamiento As String
    End Structure

    Public Structure Email_Cfg
        Public STMP_UserName As String
        Public STMP_Password As String
        Public STMP_Host As String
        Public STMP_Port As String
        Public STMP_SSL As String
        Public emailSpecs As String
        Public emailValidation As String
        Public emailFinished As String
    End Structure

    Public Structure Ticket
        Public dirección_fiscal1 As String 'Calle y Colonia
        Public dirección_fiscal2 As String 'Ciudad y C.P
        Public telefono As String
        Public RFC As String
        Public REGIMEN As String
        Public Fecha As String
        Public FOLIO As String
        Public Caja As String
        Public Vendedor As String
        Public TipoCliente As String
        Public FormaPago As String
        Public Importe As String
        Public Total As String
        Public Cambio As String
    End Structure

    Public Structure Singleton
        Public Param As String 'Parametro completo enviado por frmFoliosVenta
        Public ReadOnly Property Sender As String
            Get
                Try
                    Dim arre() As String = Param.Split("|")
                    Return arre(0).Trim

                Catch ex As Exception
                    Return "~"
                End Try
            End Get
        End Property
        Public ReadOnly Property Folio As String
            Get
                Try
                    Dim arre() As String = Param.Split("|")
                    Return arre(1).Trim

                Catch ex As Exception
                    Return "~"
                End Try
            End Get
        End Property
    End Structure
End Module
