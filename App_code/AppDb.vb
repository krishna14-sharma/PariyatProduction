Imports System
Imports System.Web
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' Central place for database connection strings and safe (parameterised) data access.
''' Connection strings live in Web.config &lt;connectionStrings&gt; - never in code.
''' </summary>
Public NotInheritable Class AppDb

    ''' <summary>Name of the main Poultry database connection string in Web.config.</summary>
    Public Const Poultry As String = "PoultryDb"
    ''' <summary>Name of the Feed database connection string in Web.config.</summary>
    Public Const Feed As String = "FeedDb"
    ''' <summary>Name of the Store database connection string in Web.config.</summary>
    Public Const Store As String = "StoreDb"

    Private Sub New()
    End Sub

    ''' <summary>Returns a connection string from Web.config, or throws a clear error if it is missing.</summary>
    Public Shared Function ConnectionString(ByVal name As String) As String
        Dim cs As ConnectionStringSettings = ConfigurationManager.ConnectionStrings(name)
        If cs Is Nothing OrElse String.IsNullOrEmpty(cs.ConnectionString) Then
            Throw New ConfigurationErrorsException("Connection string '" & name & "' is missing from Web.config.")
        End If
        Return cs.ConnectionString
    End Function

    ''' <summary>Creates a SQL parameter. Nothing becomes DBNull.</summary>
    Public Shared Function P(ByVal name As String, ByVal value As Object) As SqlParameter
        If value Is Nothing Then value = DBNull.Value
        Return New SqlParameter(name, value)
    End Function

    ''' <summary>Runs a parameterised SELECT and returns the rows.</summary>
    ''' <example>Dim dt = AppDb.GetTable(AppDb.Poultry, "select * from Users where UserName=@u", AppDb.P("@u", name))</example>
    Public Shared Function GetTable(ByVal connectionName As String, ByVal sql As String, ByVal ParamArray parameters() As SqlParameter) As DataTable
        Dim dt As New DataTable()
        Using con As New SqlConnection(ConnectionString(connectionName))
            Using cmd As SqlCommand = CreateCommand(con, sql, parameters)
                Using adp As New SqlDataAdapter(cmd)
                    adp.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ''' <summary>Runs a parameterised INSERT / UPDATE / DELETE and returns the number of rows affected.</summary>
    Public Shared Function Execute(ByVal connectionName As String, ByVal sql As String, ByVal ParamArray parameters() As SqlParameter) As Integer
        Using con As New SqlConnection(ConnectionString(connectionName))
            Using cmd As SqlCommand = CreateCommand(con, sql, parameters)
                con.Open()
                Return cmd.ExecuteNonQuery()
            End Using
        End Using
    End Function

    ''' <summary>Runs a parameterised query and returns the first column of the first row (or Nothing).</summary>
    Public Shared Function Scalar(ByVal connectionName As String, ByVal sql As String, ByVal ParamArray parameters() As SqlParameter) As Object
        Using con As New SqlConnection(ConnectionString(connectionName))
            Using cmd As SqlCommand = CreateCommand(con, sql, parameters)
                con.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If result Is DBNull.Value Then Return Nothing
                Return result
            End Using
        End Using
    End Function

    Private Shared Function CreateCommand(ByVal con As SqlConnection, ByVal sql As String, ByVal parameters() As SqlParameter) As SqlCommand
        Dim cmd As New SqlCommand(sql, con)
        cmd.CommandTimeout = 300
        If parameters IsNot Nothing Then
            For Each prm As SqlParameter In parameters
                If prm IsNot Nothing Then cmd.Parameters.Add(prm)
            Next
        End If
        Return cmd
    End Function

End Class
