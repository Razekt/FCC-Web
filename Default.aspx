﻿<%@ Page Title="Fuzzy com Coppe-Cosenza" Language="VB" MasterPageFile="~/PagMaster.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cabecalho" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" Runat="Server">
    <div class="jumbotron">
        <div class="container">
            <h2 class="display-4">Fuzzy Coppe-Cosenza na Web</h2>
        </div>
    </div>

    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Label runat="server" ID="lblDemanda">Matriz de Demanda:</asp:Label><br />
                <asp:Button runat="server" ID="btnInserirLinhaDemanda" CssClass="btn btn-primary" Text="Inserir Linha" />
                <asp:Button runat="server" ID="btnInserirColunaDemanda" CssClass="btn btn-primary" Text="Inserir Coluna" />
                <asp:Button runat="server" ID="btnDeletarColunaDemanda" CssClass="btn btn-primary" Text="Deletar Coluna" />
                <asp:GridView runat="server" ID="grdDemanda" CssClass="table" AutoGenerateDeleteButton="true" AutoGenerateEditButton="true">
                    <HeaderStyle CssClass="table-head" />
                    <RowStyle CssClass="row_grd" />
                    <AlternatingRowStyle CssClass="alternate_row_grd" />
                </asp:GridView>
            </div>
            <div class="col-md-12">
                <asp:Label runat="server" ID="lblOferta">Matriz de Oferta:</asp:Label><br />
                <asp:Button runat="server" ID="btnInserirLinhaOferta" CssClass="btn btn-primary" Text="Inserir Linha" />
                <asp:Button runat="server" ID="btnInserirColunaOferta" CssClass="btn btn-primary" Text="Inserir Coluna" />
                <asp:Button runat="server" ID="btnDeletarColunaOferta" CssClass="btn btn-primary" Text="Deletar Coluna" />
                <asp:GridView runat="server" ID="grdOferta" CssClass="table" AutoGenerateDeleteButton="true" AutoGenerateEditButton="true">
                    <HeaderStyle CssClass="table-head" />
                    <RowStyle CssClass="row_grd" />
                    <AlternatingRowStyle CssClass="alternate_row_grd" />
                </asp:GridView>
            </div>

            <div class="col-md-12">
                <asp:Label runat="server" ID="Label2">Número para matriz de cálculos (Regra de Solução Fuzzy):</asp:Label><br />
                <asp:TextBox ID="txtFuzzy" runat="server"></asp:TextBox>
                <asp:Button runat="server" ID="btnVerificar" CssClass="btn btn-primary" Text="Verificar" /><br /><br />
                <asp:Label runat="server" ID="lblRef" Text=""></asp:Label>
                <asp:GridView runat="server" ID="grdFuzzy" CssClass="table">
                    <HeaderStyle CssClass="table-head" />
                    <RowStyle CssClass="row_grd" />
                    <AlternatingRowStyle CssClass="alternate_row_grd" />
                </asp:GridView>
            </div>

            <div class="col-md-12">
                <asp:Label runat="server" ID="lblAnaliseResultado"></asp:Label><br />
                <span style="text-align:center">
                    <asp:Label runat="server" ID="lblNomeTabela" CssClass="font-weight-bold"></asp:Label>
                </span>
                <asp:GridView runat="server" ID="grdIRC" CssClass="table">
                    <HeaderStyle CssClass="table-head" />
                    <RowStyle CssClass="row_grd" />
                    <AlternatingRowStyle CssClass="alternate_row_grd" />
                </asp:GridView>
                <asp:Button runat="server" ID="btnMudarTabela" CssClass="btn btn-primary" Visible="false" Text="Mudar Tabela" />
            </div>

            <div class="col-md-12">
                <asp:Label runat="server" ID="lblResultado"></asp:Label><br />
                <asp:GridView runat="server" ID="grdResposta" CssClass="table">
                    <HeaderStyle CssClass="table-head" />
                    <RowStyle CssClass="row_grd" />
                    <AlternatingRowStyle CssClass="alternate_row_grd" />
                </asp:GridView>
            </div>
        </div>
    </div>

    <div class="container">
        <hr />
        <footer>
            <p style="text-align:center">
                IME - Instituto Militar de Engenharia<br />
                Fernanda Cipriano - fernandaciprianoc@gmail.com<br />
                Viviane Viana Sofiste de Abreu - vsofiste@gmail.com
            </p>
        </footer>
    </div>
</asp:Content>