
Public Class User
    Dim strsql As String
    Dim info As String
    Private _iduser As Integer
    Private _username As String
    Private _nama_lengkap As String
    Private _passwd As String
    Public InsertState As Boolean = False
    Public UpdateState As Boolean = False
    Public DeleteState As Boolean = False
    Public Property username()
        Get
            Return _username
        End Get
        Set(ByVal value)
            _username = value
        End Set
    End Property
    Public Property nama_lengkap()
        Get
            Return _nama_lengkap
        End Get
        Set(ByVal value)
            _nama_lengkap = value
        End Set
    End Property
    Public Property passwd()
        Get
            Return _passwd
        End Get
        Set(ByVal value)
            _passwd = value
        End Set
    End Property
    Public Sub Simpan()
        Dim info As String
        DBConnect()
        If (user_baru = True) Then
            strsql = "Insert into user(username,nama_lengkap,passwd) values ('" & _username & "','" & _nama_lengkap & "','" & _passwd & "')"
            info = "INSERT"
        Else
            strsql = "update user set username='" & _username & "', nama_lengkap='" & _nama_lengkap & "', passwd='" & _passwd & "' where username='" & _username & "'"
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
    Public Sub Cariuser(ByVal susername As String)
        DBConnect()
        strsql = "SELECT * FROM user WHERE username='" & susername & "'"
        mycommand.Connection = conn
        mycommand.CommandText = strsql
        DR = mycommand.ExecuteReader
        If (DR.HasRows = True) Then
            user_baru = False
            DR.Read()
            username = Convert.ToString((DR("username")))
            nama_lengkap = Convert.ToString((DR("nama_lengkap")))
            passwd = Convert.ToString((DR("passwd")))
        Else
            MessageBox.Show("Data Tidak Ditemukan.")
            user_baru = True
        End If
        DBDisconnect()
    End Sub
    Public Sub Hapus(ByVal susername As String)
        Dim info As String
        DBConnect()
        strsql = "DELETE FROM user WHERE username='" & susername & "'"
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
            strsql = "SELECT * FROM user"
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

    Public Function Login(ByVal uname As String, ByVal pwd As String) As Boolean
        Dim pwd_en As String
        pwd_en = getMD5Hash(pwd)
        DBConnect()
        strsql = "SELECT * FROM users WHERE username='" & uname & "' and password ='" & pwd_en & "'"

        mycommand.Connection = conn
        mycommand.CommandText = strsql
        DR = mycommand.ExecuteReader
        If (DR.HasRows = True) Then
            login_valid = True
        Else
            login_valid = False
        End If
        DBDisconnect()
        Return login_valid
    End Function
End Class
