Imports System.Data
Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient

Public Class Local

    Public Shared Meses() As String = {"Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"}

    Public Shared Function StrConnObj(ByVal STRCONN As String) As String
        Dim conn As Object = Nothing
        conn = ConfigurationManager.ConnectionStrings(STRCONN).ConnectionString
        Return conn.ToString()
    End Function

    Public Shared Function DSConsulta(ByVal SQL As String, ByVal STRCONN As String) As DataSet
        Dim DS As New DataSet
        Dim conn As MySqlConnection = Nothing
        Dim Adapt As MySqlDataAdapter = Nothing
        Dim cmd As MySqlCommand = Nothing

        conn = New MySqlConnection(StrConnObj(STRCONN))
        cmd = New MySqlCommand(SQL, conn)
        Adapt = New MySqlDataAdapter(cmd)
        Adapt.Fill(DS)

        Return DS
    End Function

    Public Shared Function DSCarregaTop(ByVal SQLOuCampo As String, ByVal Tabela As String, ByVal STRCONN As String) As Integer
        If Not SQLOuCampo.Contains("SELECT") Then
            SQLOuCampo = "SELECT MAX(" & SQLOuCampo & ") FROM " & Tabela & " GROUP BY " & SQLOuCampo
        End If

        Dim num As Integer = 0
        If DSConsulta(SQLOuCampo, STRCONN).Tables(0).Rows.Count >= 1 Then
            num = CInt(DSConsulta(SQLOuCampo, STRCONN).Tables(0).Rows.Count)
        End If

        If num = 0 Then
            num = 1
        Else
            num += 1
        End If

        Return num
    End Function

    Public Shared Sub DSGrava(ByVal SQL As String, ByVal STRCONN As String)
        Dim conn As MySqlConnection = Nothing
        Dim cmd As MySqlCommand = Nothing

        conn = New MySqlConnection(StrConnObj(STRCONN))
        cmd = New MySqlCommand(SQL, conn)

        conn.Open()
        cmd.ExecuteNonQuery()
        conn.Close()
    End Sub
End Class
