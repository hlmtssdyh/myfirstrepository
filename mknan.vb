Public Class Mknan
    Dim strsql As String
    Dim info As String
    Private _idmakanan As System.Decimal
    Private _kode_makanan As System.String
    Private _nama_makanan As System.String
    Private _harga_makanan As System.String
    Public InsertState As Boolean = False
    Public UpdateState As Boolean = False
    Public DeleteState As Boolean = False
    Public Property idmakanan()
        Get
            Return _idmakanan
        End Get
        Set(ByVal value)
            _idmakanan = value
        End Set
    End Property
    Public Property kode_makanan()
        Get
            Return _kode_makanan
        End Get
        Set(ByVal value)
            _kode_makanan = value
        End Set
    End Property
    Public Property nama_makanan()
        Get
            Return _nama_makanan
        End Get
        Set(ByVal value)
            _nama_makanan = value
        End Set
    End Property
    Public Property harga_makanan()
        Get
            Return _harga_makanan
        End Get
        Set(ByVal value)
            _harga_makanan = value
        End Set
    End Property
    Public Sub Simpan()
        Dim info As String
        DBConnect()
        If (MAKANAN_baru = True) Then
            strsql = "Insert into MAKANAN(idmakanan,kode_makanan,nama_makanan,harga_makanan) values ('" & _idmakanan & "','" & _kode_makanan & "','" & _nama_makanan & "','" & _harga_makanan & "')"
            info = "INSERT"
        Else
            strsql = "update MAKANAN set idmakanan='" & _idmakanan & "', kode_makanan='" & _kode_makanan & "', nama_makanan='" & _nama_makanan & "', harga_makanan='" & _harga_makanan & "' where KODE_MAKANAN='" & _kode_makanan & "'"
            info = "UPDATE"
        End If
        mycommand.Connection = conn
        mycommand.CommandText = strsql
        Try
            mycommand.ExecuteNonQuery()
        Catch ex As Exception
            If (info = "INSERT") Then
                InsertState = False
            ElseIf (info = "UPDATE") Then
                UpdateState = False
            Else
            End If
        Finally
            If (info = "INSERT") Then
                InsertState = True
            ElseIf (info = "UPDATE") Then
                UpdateState = True
            Else
            End If
        End Try
        DBDisconnect()
    End Sub
    Public Sub CariMAKANAN(ByVal sIDMAKANAN As String)
        DBConnect()
        strsql = "SELECT * FROM MAKANAN WHERE IDMAKANAN='" & sIDMAKANAN & "'"
        mycommand.Connection = conn
        mycommand.CommandText = strsql
        DR = mycommand.ExecuteReader
        If (DR.HasRows = True) Then
            MAKANAN_baru = False
            DR.Read()
            idmakanan = Convert.ToString((DR("idmakanan")))
            kode_makanan = Convert.ToString((DR("kode_makanan")))
            nama_makanan = Convert.ToString((DR("nama_makanan")))
            harga_makanan = Convert.ToString((DR("harga_makanan")))
        Else
            MessageBox.Show("Data Tidak Ditemukan.")
            MAKANAN_baru = True
        End If
        DBDisconnect()
    End Sub
    Public Sub Hapus(ByVal sIDMAKANAN As String)
        Dim info As String
        DBConnect()
        strsql = "DELETE FROM MAKANAN WHERE IDMAKANAN='" & sIDMAKANAN & "'"
        info = "DELETE"
        mycommand.Connection = conn
        mycommand.CommandText = strsql
        Try
            mycommand.ExecuteNonQuery()
            DeleteState = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        DBDisconnect()
    End Sub
    Public Sub getAllData(ByVal dg As DataGridView)
        Try
            DBConnect()
            strsql = "SELECT * FROM MAKANAN"
            mycommand.Connection = conn
            mycommand.CommandText = strsql
            mydata.Clear()
            myadapter.SelectCommand = mycommand
            myadapter.Fill(mydata)
            With dg
                .DataSource = mydata
                .AllowUserToAddRows = False
                .AllowUserToDeleteRows = False
                .ReadOnly = True
            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        Finally
            DBDisconnect()
        End Try
    End Sub
End Class
