Public Class Connection
    Private _datasource As String = "xe"
    Private _uname As String
    Private _pwd As String
    Private _datareader As System.Data.OracleClient.OracleDataReader
    Private _found As Boolean = False
    Private _connected As Boolean = False

    ''' <summary>
    ''' Penampung data hasil pencarian fungsi searchone
    ''' </summary>
    Public Property DataReader() As Object
        Get
            Return _datareader
        End Get
        Set(ByVal value As Object)
            _datareader = value
        End Set
    End Property

    ''' <summary>
    ''' Nama Data Source SQL Server Express, default = NamaPC\SQLEXPRESS
    ''' </summary>
    Public Property DataSource() As String
        Get
            Return _datasource
        End Get
        Set(ByVal value As String)
            _datasource = value
        End Set
    End Property

    ''' <summary>
    ''' Username Oracle Express
    ''' </summary>
    Public Property UserID() As String
        Get
            Return _uname
        End Get
        Set(ByVal value As String)
            _uname = value
        End Set
    End Property

    ''' <summary>
    ''' Password Oracle Express sesuai dengan user id nya
    ''' </summary>
    Public Property Password() As String
        Get
            Return _pwd
        End Get
        Set(ByVal value As String)
            _pwd = value
        End Set
    End Property

    ''' <summary>
    ''' Hasil pencarian data (True = Ditemukan, False=Tdk ditemukan)
    ''' </summary>
    Public Property RecordFound() As Boolean
        Get
            Return _found
        End Get
        Set(ByVal value As Boolean)
            _found = value
        End Set
    End Property

    ''' <summary>
    ''' Proses menyambungkan koneksi (SQL Server Express with Integrated Security = True without Username or Password)
    ''' </summary>
    Public Sub Connect()
        Try
            conn.Close()
            conn.ConnectionString = "Data Source=" & _datasource & ";User Id=" & _uname & ";Password=" & _pwd & ";"

            conn.Open()

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        Finally
            If (conn.State = True) Then
                _connected = True
            Else
                _connected = False
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Proses memutuskan koneksi
    ''' </summary>
    Public Sub Disconnect()
        If (conn.State = True) Then
            conn.Close()
            conn.Dispose()
            _connected = False
        End If
    End Sub
End Class


