
Partial Class misreports_mis_consolidatePMreport
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        sql = "EXEC sp_PM_PivotReport1 '" & entrydate.Value.ToString & "','" & todate.Value.ToString & "','" & ddlMF.Text & "'"
        gmod.DataSetRet(sql, "pmcondata")

        GridView1.DataSource = gmod.ds.Tables("pmcondata")
        GridView1.DataBind()
    End Sub
    ' Add this override to prevent the run-time error "Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."
    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' DO NOT remove this override
    End Sub


    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=PMReport.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Dim sw As New System.IO.StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(sw)

        ' Remove paging to export all rows if needed
        GridView1.AllowPaging = False

        sql = "EXEC sp_PM_PivotReport '" & entrydate.Value.ToString & "','" & todate.Value.ToString & "','" & ddlMF.Text & "'"
        gmod.DataSetRet(sql, "pmcondata")

        GridView1.DataSource = gmod.ds.Tables("pmcondata")
        GridView1.DataBind()

        ' Export
        GridView1.RenderControl(hw)
        Response.Output.Write(sw.ToString())
        Response.Flush()
        Response.End()
    End Sub
End Class
