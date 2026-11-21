
Partial Class dailyentryforms_DailyIremReceived
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim gmodStore As New GModule_Store
    Dim valiation As Boolean = True
    Dim rCount As Integer = 0
    Protected Sub btnReceve_Click(sender As Object, e As EventArgs) Handles btnReceve.Click

        sql = "select location_store_id from [dbo].[location_master]  where location_code in (select AreaCode from [dbo].[Users] where UserName='" & Session("user_name") & "')"
        gmod.DataSetRet(sql, "location_store_id")

        sql = " select Issue_No, im.Issue_Date,IssuingPerson,BookNo,IssueNo,ItemName,ItemCode,ItemCatg,ItemAU,IssueQty,id.DeptName,Receipt_No from [dbo].[IssueStock_Master] im inner join "
        sql &= " [dbo].[IssueStock_Details]   id on im.Issue_No = id.IssueNo and im.sessionyear = id.sessionyear "
        sql &= " where  IsReceived = 0 and id.Issue_Date = '" & entrydate.Value & "' and im.AreaName  in (select Area_Name from [dbo].[IssueArea_Master] where Area_Id  =  '" & gmod.ds.Tables("location_store_id").Rows(0)(0).ToString & "' ) "
        sql &= " order by Issue_No desc "

        gmodStore.DataSetRet(sql, "materail_received")

        Dim i As Integer = 0
        For i = 0 To gmodStore.ds.Tables("materail_received").Rows.Count - 1

            sql = "insert into [dbo].[Materail_received]([Rec_No], [Rec_Date], [Rec_Person], "
            sql &= " [Book_No], [Item_Name], [Item_Code], [Item_Category], [Unit], [Rec_Qty], [Dept_Name], [Receipt_No],[loc_code]) values("

            sql &= "'" & gmodStore.ds.Tables("materail_received").Rows(i)("Issue_No") & "',"
            sql &= "'" & gmodStore.ds.Tables("materail_received").Rows(i)("Issue_Date") & "',"
            sql &= "'" & gmodStore.ds.Tables("materail_received").Rows(i)("IssuingPerson") & "',"
            sql &= "'" & gmodStore.ds.Tables("materail_received").Rows(i)("BookNo") & "',"
            sql &= "'" & gmodStore.ds.Tables("materail_received").Rows(i)("ItemName") & "',"
            sql &= "'" & gmodStore.ds.Tables("materail_received").Rows(i)("ItemCode") & "',"
            sql &= "'" & gmodStore.ds.Tables("materail_received").Rows(i)("ItemCatg") & "',"
            sql &= "'" & gmodStore.ds.Tables("materail_received").Rows(i)("ItemAU") & "',"
            sql &= "'" & gmodStore.ds.Tables("materail_received").Rows(i)("IssueQty") & "',"
            sql &= "'" & gmodStore.ds.Tables("materail_received").Rows(i)("DeptName") & "',"
            sql &= "'" & gmodStore.ds.Tables("materail_received").Rows(i)("Receipt_No") & "',"
            sql &= "'" & Session("area_code") & "')"
            gmod.SqlExecuteNonQuery(sql)

            sql = "update IssueStock_Master set IsReceived = 1 where Issue_No='" & gmodStore.ds.Tables("materail_received").Rows(i)("Issue_No") & "'    and sessionyear= '2223'"
            gmodStore.SqlExecuteNonQuery(sql)
        Next
    End Sub

    
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            sql = "select location_store_id from [dbo].[location_master]  where location_code in (select AreaCode from [dbo].[Users] where UserName='" & Session("user_name") & "')"


        gmod.DataSetRet(sql, "location_store_id")

        sql = " select Issue_No, im.Issue_Date,IssuingPerson,BookNo,IssueNo,ItemName,ItemCode,ItemCatg,ItemAU,IssueQty,id.DeptName,Receipt_No from [dbo].[IssueStock_Master] im inner join "
        sql &= " [dbo].[IssueStock_Details]   id on im.Issue_No = id.IssueNo and im.sessionyear = id.sessionyear "
        sql &= " where im.sessionyear='2223' and IsReceived = 0 and id.Issue_Date = '" & entrydate.Value & "' and im.AreaName  in (select Area_Name from [dbo].[IssueArea_Master] where Area_Id  =  '" & gmod.ds.Tables("location_store_id").Rows(0)(0).ToString & "' ) "
        sql &= " order by Issue_No desc "

        gmodStore.DataSetRet(sql, "materail_received")
        ViewState("Count") = gmodStore.ds.Tables("materail_received").Rows.Count

        materialrec_grid.DataSource = gmodStore.ds.Tables("materail_received")
            materialrec_grid.DataBind()


        Catch ex As Exception
            AppUi.ShowError(Me, ex.Message)
        End Try
    End Sub

    
   
    
    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            sql = "select location_store_id from [dbo].[location_master]  where location_code in (select AreaCode from [dbo].[Users] where UserName='" & Session("user_name") & "')"

            gmod.DataSetRet(sql, "location_store_id")

            sql = " select Issue_No, im.Issue_Date,IssuingPerson,BookNo,IssueNo,ItemName,ItemCode,ItemCatg,ItemAU,IssueQty,id.DeptName,Receipt_No from [dbo].[IssueStock_Master] im inner join "
            sql &= " [dbo].[IssueStock_Details]   id on im.Issue_No = id.IssueNo and im.sessionyear = id.sessionyear "
            sql &= " where im.sessionyear='" & gmod.Getsession(Now) & "' and IsReceived = 0 and id.Issue_Date = '" & entrydate.Value & "' and im.AreaName  in (select Area_Name from [dbo].[IssueArea_Master] where Area_Id  =  '" & gmod.ds.Tables("location_store_id").Rows(0)(0).ToString & "' ) "
            sql &= " order by Issue_No desc "

            gmodStore.DataSetRet(sql, "materail_received")
            ViewState("Count") = gmodStore.ds.Tables("materail_received").Rows.Count

            materialrec_grid.DataSource = gmodStore.ds.Tables("materail_received")
            materialrec_grid.DataBind()


        Catch ex As Exception
            AppUi.ShowError(Me, ex.Message)
        End Try
    End Sub
End Class
