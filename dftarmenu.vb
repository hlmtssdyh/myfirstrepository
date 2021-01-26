Public Class Order
    Dim strsql As String
    Dim info As String
    Private _idorder As System.Decimal
    Private _nomor_bukti As System.String
    Private _tanggal As System.String
    Private _nomor_meja As System.String
    Private _total_bayar As System.String
    Private _status_bayar As System.String
    Private _iddetail As System.Decimal
    Private _kode As System.String
    Private _nama_menu As System.String
    Private _qty As System.Decimal
    Private _harga As System.String
    Private _subtotal As System.String
    Public InsertState As Boolean = False
    Public UpdateState As Boolean = False
    Public DeleteState As Boolean = False
    Public Property idorder()
        Get
            Return _idorder
        End Get
        Set(ByVal value)
            _idorder = value
        End Set
    End Property
    Public Property nomor_bukti()
        Get
            Return _nomor_bukti
        End Get
        Set(ByVal value)
            _nomor_bukti = value
        End Set
    End Property
    Public Property tanggal()
        Get
            Return _tanggal
        End Get
        Set(ByVal value)
            _tanggal = value
        End Set
    End Property
    Public Property nomor_meja()
        Get
            Return _nomor_meja
        End Get
        Set(ByVal value)
            _nomor_meja = value
        End Set
    End Property
    Public Property total_bayar()
        Get
            Return _total_bayar
        End Get
        Set(ByVal value)
            _total_bayar = value
        End Set
    End Property
    Public Property status_bayar()
        Get
            Return _status_bayar
        End Get
        Set(ByVal value)
            _status_bayar = value
        End Set
    End Property
    Public Sub Simpan()
        Dim info As String
        DBConnect()
        If (DAFTARMENU_baru = True) Then
            strsql = "Insert into DAFTARMENU (idorder,nomor_bukti,tanggal,nomor_meja,total_bayar,status_bayar) values ('" & _idorder & "','" & _nomor_bukti & "','" & _tanggal & "','" & _nomor_meja & "','" & _total_bayar & "','" & _status_bayar & "')"
            info = "INSERT"
        Else
            strsql = "update DAFTARMENU set idorder='" & _idorder & "', nomor_bukti='" & _nomor_bukti & "', tanggal='" & _tanggal & "', nomor_meja='" & _nomor_meja & "', total_bayar='" & _total_bayar & "', status_bayar='" & _status_bayar & "' where idorder='" & _idorder & "'"
            info = "UPDATE"
        End If
        myCommand.Connection = conn
        mycommand.CommandText = strsql
        Try
            myCommand.ExecuteNonQuery()
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
    Public Sub CariDAFTARMENU(ByVal sIDMINUMAN As String)
        DBConnect()
        strsql = "SELECT * FROM DAFTAR MENU WHERE IDMINUMAN='" & sIDMINUMAN & "'"
        myCommand.Connection = conn
        myCommand.CommandText = strsql
        DR = myCommand.ExecuteReader
        If (DR.HasRows = True) Then
            DAFTARMENU_baru = False
            DR.Read()
            idorder = Convert.ToString((DR("idorder")))
            nomor_bukti = Convert.ToString((DR("nomor_bukti")))
            tanggal = Convert.ToString((DR("tanggal")))
            nomor_meja = Convert.ToString((DR("nomor_meja")))
            total_bayar = Convert.ToString((DR("total_bayar")))
            status_bayar = Convert.ToString((DR("status_bayar")))
        Else
            MessageBox.Show("Data Tidak Ditemukan.")
            DAFTARMENU_baru = True
        End If
        DBDisconnect()
    End Sub
    Public Sub Hapus(ByVal sIDMINUMAN As String)
        Dim info As String
        DBConnect()
        strsql = "DELETE FROM DAFTARMENU WHERE IDMINUMAN='" & sIDMINUMAN & "'"
        info = "DELETE"
        myCommand.Connection = conn
        myCommand.CommandText = strsql
        Try
            myCommand.ExecuteNonQuery()
            DeleteState = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        DBDisconnect()
    End Sub
    Public Sub getAllData(ByVal dg As DataGridView)
        Try
            DBConnect()
            strsql = "SELECT * FROM DAFTARMEN"
            myCommand.Connection = conn
            myCommand.CommandText = strsql
            myData.Clear()
            myAdapter.SelectCommand = myCommand
            myAdapter.Fill(myData)
            With dg
                .DataSource = myData
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
