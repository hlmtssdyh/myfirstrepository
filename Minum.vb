Public Class Minum
    Dim strsql As String
    Dim info As String
    Private _idminuman As System.Decimal
    Private _kode_minuman As System.String
    Private _nama_minuman As System.String
    Private _harga As System.Decimal
    Public InsertState As Boolean = False
    Public UpdateState As Boolean = False
    Public DeleteState As Boolean = False
    Public Property idminuman()
        Get
            Return _idminuman
        End Get
        Set(ByVal value)
            _idminuman = value
        End Set
    End Property
    Public Property kode_minuman()
        Get
            Return _kode_minuman
        End Get
        Set(ByVal value)
            _kode_minuman = value
        End Set
    End Property
    Public Property nama_minuman()
        Get
            Return _nama_minuman
        End Get
        Set(ByVal value)
            _nama_minuman = value
        End Set
    End Property
    Public Property harga()
        Get
            Return _harga
        End Get
        Set(ByVal value)
            _harga = value
        End Set
    End Property
    Public Sub Simpan()
        Dim info As String
        DBConnect()
        If (MINUMAN_baru = True) Then
            strsql = "Insert into MINUMAN(idminuman,kode_minuman,nama_minuman,harga) values ('" & _idminuman & "','" & _kode_minuman & "','" & _nama_minuman & "','" & _harga & "')"
            info = "INSERT"
        Else
            strsql = "update MINUMAN set idminuman='" & _idminuman & "', kode_minuman='" & _kode_minuman & "', nama_minuman='" & _nama_minuman & "', harga='" & _harga & "' where KODE_MINUMAN='" & _kode_minuman & "'"
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
    Public Sub CariMINUMAN(ByVal sIDMINUMAN As String)
        DBConnect()
        strsql = "SELECT * FROM MINUMAN WHERE IDMINUMAN='" & sIDMINUMAN & "'"
        mycommand.Connection = conn
        mycommand.CommandText = strsql
        DR = mycommand.ExecuteReader
        If (DR.HasRows = True) Then
            MINUMAN_baru = False
            DR.Read()
            idminuman = Convert.ToString((DR("idminuman")))
            kode_minuman = Convert.ToString((DR("kode_minuman")))
            nama_minuman = Convert.ToString((DR("nama_minuman")))
            harga = Convert.ToString((DR("harga")))
        Else
            MessageBox.Show("Data Tidak Ditemukan.")
            MINUMAN_baru = True
        End If
        DBDisconnect()
    End Sub
    Public Sub Hapus(ByVal sIDMINUMAN As String)
        Dim info As String
        DBConnect()
        strsql = "DELETE FROM MINUMAN WHERE IDMINUMAN='" & sIDMINUMAN & "'"
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
            strsql = "SELECT * FROM MINUMAN"
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
