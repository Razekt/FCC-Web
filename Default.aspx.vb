Imports System.Data

Partial Class _Default
    Inherits System.Web.UI.Page

    Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim D As New Data.DataTable("demanda")
            Dim O As New Data.DataTable("oferta")

            ' Inicializa Demanda
            D.Columns.Add("Variáveis", GetType(String))
            D.Columns.Add("FR1", GetType(String))
            D.Columns.Add("FR2", GetType(String))
            D.Rows.Add("")
            D.Rows.Add("")
            Session("demanda") = D

            'Inicializa Oferta
            O.Columns.Add("Variáveis", GetType(String))
            O.Columns.Add("FR1", GetType(String))
            O.Columns.Add("FR2", GetType(String))
            O.Rows.Add("")
            O.Rows.Add("")
            Session("oferta") = O

            Session("NumTabela") = 0
            Bind()
        End If
    End Sub

    Private Sub grdDemanda_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles grdDemanda.RowUpdating
        Dim i As Integer = 0, j As Integer = 0

        Do
            CType(Session("demanda"), Data.DataTable).Rows(e.RowIndex).Item(i) = e.NewValues(i)
            i += 1
        Loop While i <= CType(Session("demanda"), Data.DataTable).Rows(e.RowIndex).ItemArray.Count - 1

        grdDemanda.EditIndex = -1
        Bind()
    End Sub

    Private Sub grdOferta_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles grdOferta.RowUpdating
        Dim i As Integer = 0, j As Integer = 0

        Do
            CType(Session("oferta"), Data.DataTable).Rows(e.RowIndex).Item(i) = e.NewValues(i)
            i += 1
        Loop While i <= CType(Session("oferta"), Data.DataTable).Rows(e.RowIndex).ItemArray.Count - 1

        grdOferta.EditIndex = -1
        Bind()
    End Sub

    Private Sub CancelarEdicao(sender As Object, e As GridViewCancelEditEventArgs) Handles grdDemanda.RowCancelingEdit, grdOferta.RowCancelingEdit
        sender.EditIndex = -1
        Bind()
    End Sub

    Private Sub grdDemanda_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles grdDemanda.RowDeleting
        DeletarLinha(CType(Session("demanda"), Data.DataTable), e.RowIndex)
    End Sub

    Private Sub grdOferta_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles grdOferta.RowDeleting
        DeletarLinha(CType(Session("oferta"), Data.DataTable), e.RowIndex)
    End Sub

    Private Sub btnInserirLinhaDemanda_Click(sender As Object, e As EventArgs) Handles btnInserirLinhaDemanda.Click
        InserirLinha(CType(Session("demanda"), Data.DataTable))
    End Sub

    Private Sub btnInserirLinhaOferta_Click(sender As Object, e As EventArgs) Handles btnInserirLinhaOferta.Click
        InserirLinha(CType(Session("oferta"), Data.DataTable))
    End Sub

    Private Sub btnInserirColunaDemanda_Click(sender As Object, e As EventArgs) Handles btnInserirColunaDemanda.Click
        InserirCol(CType(Session("demanda"), Data.DataTable))
    End Sub

    Private Sub btnInserirColunaOferta_Click(sender As Object, e As EventArgs) Handles btnInserirColunaOferta.Click
        InserirCol(CType(Session("oferta"), Data.DataTable))
    End Sub

    Private Sub btnVerificar_Click(sender As Object, e As EventArgs) Handles btnVerificar.Click
        If txtFuzzy.Text <> "" And txtFuzzy.Text <> 0 Then
            MatrizCalculo(Val(txtFuzzy.Text))
            AnaliseResultado()
            MatrizResposta()
        End If
        grdResposta.Focus()
    End Sub

    Private Sub EditarLinha(sender As Object, e As GridViewEditEventArgs) Handles grdDemanda.RowEditing, grdOferta.RowEditing
        sender.EditIndex = e.NewEditIndex
        Bind()
    End Sub

    Private Sub btnDeletarColunaDemanda_Click(sender As Object, e As EventArgs) Handles btnDeletarColunaDemanda.Click
        DeletarCol(CType(Session("demanda"), Data.DataTable))
    End Sub

    Private Sub btnDeletarColunaOferta_Click(sender As Object, e As EventArgs) Handles btnDeletarColunaOferta.Click
        DeletarCol(CType(Session("oferta"), Data.DataTable))
    End Sub

    'Coloca os dados de demanda e oferta para dentro do GRID (que aparece em tela).
    Private Sub Bind()
        'Demanda
        grdDemanda.DataSource = Session("demanda")
        grdDemanda.DataBind()

        'Oferta
        grdOferta.DataSource = Session("oferta")
        grdOferta.DataBind()
    End Sub

    Private Sub DeletarLinha(ByRef DT As Data.DataTable, ByVal indice As Integer)
        DT.Rows(indice).Delete()
        Bind()
    End Sub

    Private Sub InserirCol(ByRef DT As Data.DataTable)
        Dim i As Integer = DT.Columns.Count
        DT.Columns.Add("FR" & i, GetType(String))
        Bind()
    End Sub

    Private Sub InserirLinha(ByRef DT As Data.DataTable)
        Dim NOVALINHA As Data.DataRow = DT.NewRow
        DT.Rows.Add(NOVALINHA)
        Bind()
    End Sub

    Private Sub DeletarCol(ByRef DT As Data.DataTable)
        Dim Resposta As String = CType(Page.Master.FindControl("Resp"), TextBox).Text
        If Not Resposta = "" Then
            If DT.Columns.Contains(Resposta) Then
                DT.Columns.Remove(Resposta)
            Else
                Response.Write("<script>alert('Coluna não encontrada na matriz " & UCase(DT.TableName) & "!');</script>")
            End If
        End If
        Bind()
    End Sub

    Private Function Fatorial(ByVal num As Long) As Long
        If num <= 1 Then
            Return 1
        Else
            Return num * Fatorial(num - 1)
        End If
    End Function

    Private Sub MatrizCalculo(ByVal FuzzyN As Integer)
        Dim F As New Data.DataTable("fuzzy")
        Dim aux As Integer = 0

        ' Preenche os cabeçalhos das colunas.
        F.Columns.Add("CABECALHO", GetType(String))
        F.Columns.Add("Supera(A)", GetType(String))
        F.Columns.Add("Atende(B)", GetType(String))
        F.Columns.Add("Insuficiente(C)", GetType(String))
        F.Columns.Add("Inexistente(D)", GetType(String))


        ' Preenche a matriz de cálculo
        For j As Integer = 0 To F.Columns.Count - 1
            If Not j = 0 Then
                For i As Integer = 0 To F.Rows.Count - 1
                    If j = 2 Then
                        F.Rows(i).Item(j) = 1
                    ElseIf j = 1 Then
                        F.Rows(i).Item(j) = FormatNumber(1 + (aux / FuzzyN), 2)
                    ElseIf j = 3 Then
                        F.Rows(i).Item(j) = FormatNumber(1 - (aux / FuzzyN), 2)
                    ElseIf j = 4 Then
                        If i = 0 Then
                            F.Rows(i).Item(j) = 0
                        ElseIf i = 1 Then
                            F.Rows(i).Item(j) = FormatNumber(Math.Round(1 / Fatorial(FuzzyN - 16), 2), 2)
                        ElseIf i = 2 Then
                            F.Rows(i).Item(j) = FormatNumber(Math.Round(1 / Fatorial(FuzzyN - 17), 2), 2)
                        ElseIf i = 3 Then
                            F.Rows(i).Item(j) = FormatNumber(Math.Round(1 / Fatorial(FuzzyN - 18), 2), 2)
                        End If
                    End If
                    aux -= 1
                Next
            Else
                ' Se for a primeira coluna coloca os nomes.
                F.Rows.Add("Crucial(1)")
                F.Rows.Add("Condicionante(2)")
                F.Rows.Add("Pouco Condicionante(3)")
                F.Rows.Add("Irrelevante(4)")
            End If
            aux = F.Rows.Count
        Next

        grdFuzzy.DataSource = F
        grdFuzzy.DataBind()
        grdFuzzy.HeaderRow.Cells(0).Text = ""
        lblRef.Text = "Regra de solução FUZZY para n=" & FuzzyN
    End Sub

    Private Sub MatrizResposta()
        Dim IRC As New Data.DataTable("irc")
        Dim D As Data.DataTable = Session("demanda")
        Dim O As Data.DataTable = Session("oferta")
        Dim F As Data.DataTable = grdFuzzy.DataSource
        Dim R As New Data.DataTable("resposta")

        ' Variáveis de auxílio para preenchimento e contagem de linhas e colunas (índices).
        Dim auxD As Integer = 0, auxO As Integer = 0, soma As Double = 0
        Dim k As Integer = 0, m As Integer = 1

        ' Inicializa a tabela de análise de resposta.
        R.Columns.Add("VAR")

        ' Preenchendo os cabeçalhos da matriz de resposta.
        For i As Integer = 0 To D.Rows.Count - 1
            R.Rows.Add(D.Rows(i)(0))
        Next
        R.Rows.Add("D")

        For i As Integer = 0 To O.Rows.Count - 1
            R.Columns.Add(O.Rows(i)(0))
        Next
        R.Columns.Add("M")

        ' Percorre as duas matrizes oferta e demanda verificando os pares (D, O) para encontrar a referência na matriz de regra básica de fuzzy.
        For i As Integer = 0 To D.Rows.Count - 1
            For x As Integer = 0 To O.Rows.Count - 1
                For j As Integer = 1 To O.Columns.Count - 1
                    auxD = D.Rows(i)(j)

                    Select Case O.Rows(x)(j)
                        Case "A"
                            auxO = 1
                        Case "B"
                            auxO = 2
                        Case "C"
                            auxO = 3
                        Case "D"
                            auxO = 4
                        Case Else
                            auxO = O.Rows(x)(j)
                    End Select

                    soma += F.Rows(auxD - 1)(auxO)

                    If j = O.Columns.Count - 1 Then
                        R.Rows(k)(m) = soma
                        m += 1
                    End If
                Next

                ' Total por linha
                If IsDBNull(R.Rows(k)(R.Columns.Count - 1)) Then
                    R.Rows(k)(R.Columns.Count - 1) = 0
                End If
                R.Rows(k)(R.Columns.Count - 1) += soma
                soma = 0.0
            Next
            k += 1
            m = 1
        Next

        'Total por coluna
        soma = 0.0
        For j As Integer = 1 To R.Columns.Count - 2
            For i As Integer = 0 To R.Rows.Count - 2
                soma += R.Rows(i)(j)
            Next
            R.Rows(R.Rows.Count - 1)(j) = soma
            soma = 0.0
        Next

        ' Matriz resposta com as somas.
        grdResposta.DataSource = R
        grdResposta.DataBind()
        grdResposta.HeaderRow.Cells(0).Text = ""

        lblResultado.Text = "Resultado:"
    End Sub

    Private Sub AnaliseResultado()
        Dim D As Data.DataTable = Session("demanda")
        Dim O As Data.DataTable = Session("oferta")
        Dim F As Data.DataTable = grdFuzzy.DataSource
        Dim DS As New Data.DataSet("AResult")

        Dim auxD As Integer = 0, auxO As Integer = 0
        Dim k As Integer = 0, m As Integer = 1, a As Integer = 0
        Dim valor As Double = 0.0

        For i As Integer = 0 To O.Rows.Count - 1
            DS.Tables.Add(O.Rows(i)(0))
        Next

        For Each DT As Data.DataTable In DS.Tables
            DT.Columns.Add("VAR")

            ' Adiciona as colunas da tabela de análise de resultados.
            For i As Integer = 0 To D.Rows.Count - 1
                DT.Columns.Add(D.Rows(i)(0))
            Next

            ' Adiciona o cabeçalho das linhas da tabela de análise de resultados.
            For i As Integer = 1 To D.Columns.Count - 1
                DT.Rows.Add(D.Columns(i).ColumnName)
            Next

            For x As Integer = 0 To D.Rows.Count - 1
                For j As Integer = 1 To D.Columns.Count - 1
                    auxD = D.Rows(x)(j)

                    Select Case O.Rows(a)(j)
                        Case "A"
                            auxO = 1
                        Case "B"
                            auxO = 2
                        Case "C"
                            auxO = 3
                        Case "D"
                            auxO = 4
                        Case Else
                            auxO = O.Rows(a)(j)
                    End Select

                    valor = F.Rows(auxD - 1)(auxO)

                    If valor <= 0.04 Then
                        DT.Rows(k)(m) = "BAIXO"
                    Else
                        DT.Rows(k)(m) = "ALTO"
                    End If
                    k += 1
                Next
                k = 0
                m += 1
            Next
            m = 1
            a += 1
        Next

        Session("AResult") = DS
        grdIRC.DataSource = DS.Tables(0)
        grdIRC.DataBind()

        lblNomeTabela.Text = DS.Tables(0).TableName
        lblAnaliseResultado.Text = "Análise de Resultados:"

        If DS.Tables.Count > 1 Then
            btnMudarTabela.Visible = True
        End If
    End Sub

    Private Sub IRCBind(ByVal LINHA As Integer)
        grdIRC.DataSource = CType(Session("AResult"), Data.DataSet).Tables(LINHA)
        grdIRC.DataBind()

        lblNomeTabela.Text = CType(Session("AResult"), Data.DataSet).Tables(LINHA).TableName
    End Sub

    Private Sub btnMudarTabela_Click(sender As Object, e As EventArgs) Handles btnMudarTabela.Click
        If Session("NumTabela") < CType(Session("oferta"), Data.DataTable).Rows.Count - 1 Then
            Session("NumTabela") += 1
        Else
            Session("NumTabela") = 0
        End If
        IRCBind(Session("NumTabela"))
        btnMudarTabela.Focus()
    End Sub
End Class