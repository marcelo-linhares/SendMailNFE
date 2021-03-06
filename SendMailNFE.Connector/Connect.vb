Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Public Class Connect
    Private Const ClassName As String = "Connect"

    Private _dataBaseName As String
    Private _sqlConn As SqlConnection
    Private _serverSQL As String
    Private _loginSQL As String
    Private _senhaSQL As String

#Region "Propriedades de configura??o"

    Public Property DataBaseName() As String
        Get
            Return _dataBaseName
        End Get
        Set(ByVal Value As String)
            _dataBaseName = Value
        End Set
    End Property

    Public Property ServerSQL() As String
        Get
            Return _serverSQL
        End Get
        Set(ByVal Value As String)
            _serverSQL = Value
        End Set
    End Property

    Public Property LoginSQL() As String
        Get
            Return _loginSQL
        End Get
        Set(ByVal Value As String)
            _loginSQL = Value
        End Set
    End Property

    Public Property SenhaSQL() As String
        Get
            Return _senhaSQL
        End Get
        Set(ByVal Value As String)
            _senhaSQL = Value
        End Set
    End Property

#End Region

    Public Function GetConnection() As SqlConnection
        Dim ConnectionString As String = "Data Source=" & _serverSQL & "; User ID=" & _loginSQL & ";Password=" & _senhaSQL & ";Persist Security Info=False;"
        Try
            If Me.DataBaseName = "" Then Throw New Exception("Base de dados n?o informada.")

            ConnectionString = "Initial Catalog=" & Me.DataBaseName & ";" & ConnectionString

        Catch ex As Exception
            Throw New Exception(ex.Message & vbCrLf & "[" & ClassName & "].[GetConnection()]")
        End Try
        Return New SqlConnection(ConnectionString)
    End Function

    Public Function GetDataReader(ByVal Command As String) As SqlDataReader
        Dim sqlConn As SqlConnection
        Dim sqlCmd As SqlCommand
        Dim sqlDR As SqlDataReader

        Try
            sqlConn = GetConnection()
            sqlCmd = New SqlCommand

            With sqlCmd
                .CommandType = CommandType.Text
                .CommandText = Command
                .Connection = sqlConn
            End With

            sqlConn.Open()

            sqlDR = sqlCmd.ExecuteReader

            sqlDR.Read()

        Catch ex As Exception
            Throw New Exception(ex.Message & vbCrLf & "[" & ClassName & "].[GetDataReader()]")
        End Try
        GetConnection.Close()
        Return sqlDR
    End Function

    Public Overridable Function GetDataSet(ByVal Command As String, ByVal Name As String) As DataSet
        Dim sqlConn As SqlConnection
        Dim Da As SqlDataAdapter
        Dim Ds As DataSet

        Try
            sqlConn = GetConnection()
            Da = New SqlDataAdapter
            Ds = New DataSet

            sqlConn.Open()

            With Da
                .SelectCommand = New SqlCommand
                With .SelectCommand
                    .CommandType = CommandType.Text
                    .CommandText = Command
                    .Connection = sqlConn
                End With
                .Fill(Ds, Name)
            End With

        Catch ex As Exception
            Throw New Exception(ex.Message & vbCrLf & _
                        "[" & ClassName & "].[GetDataSet(ByVal Command As String, ByVal Name As String)]")
        End Try
        Da.Dispose()
        sqlConn.Close()
        Return Ds
    End Function

    Public Function GetDataSet(ByVal Name As String, ByVal oCommandSQL As SqlCommand) As DataSet
        Dim Da As SqlDataAdapter
        Dim Ds As DataSet

        Try
            If (_sqlConn Is Nothing) Then
                _sqlConn = Me.GetConnection()
                _sqlConn.Open()
            End If

            Da = New SqlDataAdapter
            Ds = New DataSet

            With Da
                .SelectCommand = oCommandSQL
                .SelectCommand.Connection = _sqlConn
                .Fill(Ds, Name)
            End With

        Catch ex As Exception
            Throw New Exception(ex.Message & vbCrLf & _
                        "[" & ClassName & "].[GetDataSet(ByVal Name As String, ByVal oCommandSQL As SqlCommand)]")
        End Try
        Da.Dispose()
        _sqlConn.Close()
        Return Ds
    End Function

    Public Function GetDataSet(ByVal Name As String, ByVal oCommandSQL As SqlCommand, ByVal oSqlTransact As SqlTransaction) As DataSet
        _sqlConn = oSqlTransact.Connection
        Return Me.GetDataSet(Name, oCommandSQL)
    End Function

    Public Function ExecuteScalar(ByVal pCommand As String, Optional ByVal pCommandType As CommandType = CommandType.StoredProcedure) As Object
        Dim oCommand As New SqlCommand(pCommand, Me.GetConnection())
        Dim oValue As Object = Nothing
        Try
            oCommand.CommandType = pCommandType
            oCommand.Connection.Open()
            oValue = oCommand.ExecuteScalar()
        Catch ex As Exception
            Throw New Exception(ex.Message & vbCrLf & _
                                    "[" & ClassName & "].[ExecuteScalar()]")
            Return Nothing
        End Try
        Return oValue
    End Function
End Class