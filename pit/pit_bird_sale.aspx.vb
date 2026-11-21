
Partial Class pit_pit_bird_sale
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim d As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            fillBirdtype()
        End If
    End Sub
    Public Sub fillBirdtype()
        sql = "select [bird_type_code], [bird_type_name] from [dbo].[bird_type_master]"
        gmod.DataSetRet(sql, "shed_loc")
        cmbftype.DataSource = gmod.ds.Tables("shed_loc")
        cmbftype.DataTextField = "bird_type_code"
        cmbftype.DataValueField = "bird_type_code"
        cmbftype.DataBind()
    End Sub

    Public Sub fillgrid()
        sql = "select   convert(varchar, [entry_date], 103) [entry_date],[ftype],[comm_sale] [Female sale],[comm_destroy] [Feamle Mor],[crack_sale] [Male Sale],[crack_destroy] [Male Mor], [user_name] from  [dbo].[pit_sale_des] where entry_date = '" & entrydate.Text & "' and ftype = '" & cmbftype.Text & "'"
        gmod.DataSetRet(sql, "pit_sale_des")
        commeggdata.DataSource = gmod.ds.Tables("pit_sale_des")
        commeggdata.DataBind()
    End Sub

    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        sql = "insert into [dbo].[pit_sale_des] ([entry_date], [ftype], [comm_sale], [comm_dis], [comm_destroy], [crack_sale], [crack_dis], [crack_destroy], [user_name],[isBirds],rate_comm,rate_crack,dmno,partyname,wt1,wt2) values( "
        sql &= "'" & entrydate.Text & "',"
        sql &= "'" & cmbftype.Text & "',"
        sql &= "'" & Val(commsale.Text) & "',"
        sql &= "'" & Val(commdis.Text) & "',"
        sql &= "'" & Val(commdestroy.Text) & "',"
        sql &= "'" & Val(cracksale.Text) & "',"
        sql &= "'" & Val(crackdis.Text) & "',"
        sql &= "'" & Val(crackdestroy.Text) & "',"
        sql &= "'" & Session("user_name") & "',"
        sql &= "'1',"
        sql &= "'" & Val(rate_comm.Text) & "',"
        sql &= "'" & Val(rate_crack.Text) & "',"
        sql &= "'" & txtdmno.Text & "',"
        sql &= "'" & txtpartyname.Text & "',"
        sql &= "'" & Val(wtf.Text) & "',"
        sql &= "'" & Val(wtm.Text) & "')"
        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        fillgrid()
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            sql &= "  select * from pitbirddata where  ftype='" & cmbftype.Text & "'"
            gmod.DataSetRet(sql, "egg_stock")
            commeggs.Text = gmod.ds.Tables("egg_stock").Rows(0)(0)
            crackeggs.Text = gmod.ds.Tables("egg_stock").Rows(0)(1)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        fillgrid()
    End Sub

End Class
