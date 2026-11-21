Imports CrystalDecisions.CrystalReports.Engine

Partial Class feed_rawstock
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim GlobalModule1 As New GlobalModule
    Dim valiation As Boolean = True

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fill()
    End Sub

    Public Sub fill()
        GlobalModule1.SqlExecuteNonQuery("delete from tmpstock")
        Dim SQL As String, i As Integer, up As String
        Dim w, q As Double
        'inserting all items
        SQL = "insert into tmpstock(itemname,item_code,rate,prod_type)  " _
        & " select [name],item_code,rate,prod_type from itemmaster order by prod_type,item_code "
        GlobalModule1.SqlExecuteNonQuery(SQL)
        'opening stock
        SQL = "select  item_code,itemname ,sum(qtywobag) qty    from dbo.inwreg " _
                & " where inw_date < '" & entrydate.Value.ToString & "' and session=" & GlobalModule1.session(DateTime.Now) & " group by  item_code,itemname  "

        GlobalModule1.DataSetRet(SQL, "open")
        If GlobalModule1.ds.Tables("open").Rows.Count > 0 Then
            For i = 0 To GlobalModule1.ds.Tables("open").Rows.Count - 1
                'issue
                SQL = " select isnull(SUM(qty),0) qty from issue_master im,issue_det id " _
                & " where im.formulaid=id.formulaid and item_code='" & GlobalModule1.ds.Tables("open").Rows(i)("item_code") & "' AND " _
                & " FORMULA_DATE < '" & entrydate.Value.ToString & "' and im.session=" & GlobalModule1.session(DateTime.Now)
                GlobalModule1.DataSetRet(SQL, "iq")
                'wastage
                SQL = "select isnull(SUM(wqty),0) wqty from wastage " _
                & " where item_code='" & GlobalModule1.ds.Tables("open").Rows(i)("item_code") & "' AND " _
                & " wDATE < '" & entrydate.Value.ToString & "' and session=" & GlobalModule1.session(DateTime.Now)
                GlobalModule1.DataSetRet(SQL, "wq")

                q = GlobalModule1.ds.Tables("iq").Rows(0)(0)
                w = GlobalModule1.ds.Tables("wq").Rows(0)(0)
                up = "update tmpstock set open_qty =" & Val(GlobalModule1.ds.Tables("open").Rows(i)("qty")) - q - w & " where  item_code = '" & GlobalModule1.ds.Tables("open").Rows(i)("item_code") & "'"
                GlobalModule1.SqlExecuteNonQuery(up)
            Next
        End If
        'average rate
        'SQL = "select  item_code,itemname ,avg(rate) rate   from inwreg " _
        '        & " where inw_date <= '" & todate.Value.ToShortDateString & "' and session=" & GlobalModule.session(DateTime.Now) & " group by  item_code,itemname  "
        'GlobalModule.DataSetRet(SQL, "INW")
        'If GlobalModule.ds.Tables("INW").Rows.Count > 0 Then
        '    For i = 0 To GlobalModule.ds.Tables("INW").Rows.Count - 1
        '        up = "update tmpstock set avgrate=" & Val(GlobalModule.ds.Tables("INW").Rows(i)("rate")) & " where  item_code = '" & GlobalModule.ds.Tables("INW").Rows(i)("item_code") & "'"
        '        GlobalModule.SqlExecuteNonQuery(up)
        '    Next
        'End If

        'Inserting Inward qty
        SQL = "select  item_code,[item name] ,isnull(sum([qty w/o bags]),0) qty    from dbo.inward_reg " _
        & " where [inward date] between '" & entrydate.Value.ToString & "' and '" & entrydateto.Value.ToString & "' and session=" & GlobalModule1.session(DateTime.Now) & " group by  item_code,[item name]  "
        GlobalModule1.DataSetRet(SQL, "INW")
        If GlobalModule1.ds.Tables("INW").Rows.Count > 0 Then
            For i = 0 To GlobalModule1.ds.Tables("INW").Rows.Count - 1
                up = "update tmpstock set inwqty =" & Val(GlobalModule1.ds.Tables("INW").Rows(i)("qty")) & " where  item_code = '" & GlobalModule1.ds.Tables("INW").Rows(i)("item_code") & "'"
                GlobalModule1.SqlExecuteNonQuery(up)
            Next
        End If
        'issue qty
        SQL = "select item_code,SUM(qty) qty from issue_master im,issue_det id " _
        & " where im.formulaid=id.formulaid " _
        & " AND FORMULA_DATE between '" & entrydate.Value.ToString & "' and '" & entrydateto.Value.ToString & "' and im.session=" & GlobalModule1.session(DateTime.Now) _
        & " GROUP BY ITEM_CODE"
        GlobalModule1.DataSetRet(SQL, "ISSUE")
        If GlobalModule1.ds.Tables("ISSUE").Rows.Count > 0 Then
            For i = 0 To GlobalModule1.ds.Tables("ISSUE").Rows.Count - 1
                up = "update tmpstock set issueqty ='" & GlobalModule1.ds.Tables("ISSUE").Rows(i)("qty") & "' where  item_code = '" & GlobalModule1.ds.Tables("ISSUE").Rows(i)("item_code") & "'"
                GlobalModule1.SqlExecuteNonQuery(up)
            Next
        End If
        'Direct Issue
        SQL = "select item_code,sum(qty) qty ,avg(avgrate) rate" _
        & " from Direct_issue_master dim,direct_issue_det did where  dim.inv_no=did.inv_no and  inv_date between '" & entrydate.Value.ToString & "' and '" & entrydateto.Value.ToString & "' and session=" & GlobalModule1.session(DateTime.Now) _
        & " group by item_code"
        GlobalModule1.DataSetRet(SQL, "DIRECT_ISSUE")
        If GlobalModule1.ds.Tables("DIRECT_ISSUE").Rows.Count > 0 Then
            For i = 0 To GlobalModule1.ds.Tables("DIRECT_ISSUE").Rows.Count - 1
                up = "update tmpstock set directissue_qty ='" & GlobalModule1.ds.Tables("DIRECT_ISSUE").Rows(i)("qty") & "' where  item_code = '" & GlobalModule1.ds.Tables("DIRECT_ISSUE").Rows(i)("item_code") & "'"
                GlobalModule1.SqlExecuteNonQuery(up)
            Next
        End If
        'wastage qty
        SQL = "select sum(wqty) qty ,item_code from wastage where wdate between '" & entrydate.Value.ToString & "' and '" & entrydateto.Value.ToString & "' and session=" & GlobalModule1.session(DateTime.Now) & " group by item_code  "
        GlobalModule1.DataSetRet(SQL, "wqty")
        If GlobalModule1.ds.Tables("wqty").Rows.Count > 0 Then
            For i = 0 To GlobalModule1.ds.Tables("wqty").Rows.Count - 1
                up = "update tmpstock set wastage_qty ='" & GlobalModule1.ds.Tables("wqty").Rows(i)("qty") & "' where  item_code = '" & GlobalModule1.ds.Tables("wqty").Rows(i)("item_code") & "'"
                GlobalModule1.SqlExecuteNonQuery(up)
            Next
        End If

        ' GlobalModule1.SqlExecuteNonQuery("exec dr")

        SQL = "select *,abs((open_qty + inwqty)-(issueqty+wastage_qty+directissue_qty)) qty from tmpstock "
        GlobalModule1.DataSetRet(SQL, "INW_rate")
        If GlobalModule1.ds.Tables("INW_rate").Rows.Count > 0 Then
            For i = 0 To GlobalModule1.ds.Tables("INW_rate").Rows.Count - 1
                up = "update tmpstock set avgrate=" & avgrate(GlobalModule1.ds.Tables("INW_rate").Rows(i)("item_code"), Val(GlobalModule1.ds.Tables("INW_rate").Rows(i)("qty"))) & " where  item_code = '" & GlobalModule1.ds.Tables("INW_rate").Rows(i)("item_code") & "'"
                GlobalModule1.SqlExecuteNonQuery(up)
            Next
        End If
        Dim sqlAcc_wise As String
        sqlAcc_wise = "select * from  tmpstock order by prod_type,item_code"
        GlobalModule1.DataSetRet(sqlAcc_wise, "tmpstock")

        'Dim crinwacc As New frmstockreport1
        'crinwacc.SetDataSource(GlobalModule.ds.Tables("tmpstock"))
        'crinwacc.SetParameterValue("dt", cmbname.Text)
        'crinwacc.SetParameterValue("subhead", "From " & entrydate.Value.ToString & " To " & entrydateto.Value.ToString)
        'CrystalReportViewer1.ReportSource = crinwacc


        Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
        crystalReport.Load(Server.MapPath("frmstockreport1.rpt"))

        crystalReport.SetDataSource(GlobalModule1.ds.Tables("tmpstock"))
        'crystalReport.Refresh()
        crystalReport.SetParameterValue("subhead", "From " & entrydate.Value.ToString & " To " & entrydateto.Value.ToString)

        CrystalReportViewer1.ReportSource = crystalReport

        Session("Report:" & Request.Path) = crystalReport

    End Sub

    Function avgrate(ByVal id As String, ByVal q As Double) As Double
        Dim sql As String
        sql = "select remqtywobag,rate,inw_no from inwreg where item_code='" & id & "' and remqtywobag > 0 and session=" & GlobalModule1.session(DateTime.Now) & " order by inw_no,inw_date"
        GlobalModule1.DataSetRet(sql, "avgrate")

        Dim r As Integer = 0
        Dim rt As Double
        Dim inw As String = ""
        Do While q > 0 And r < GlobalModule1.ds.Tables("avgrate").Rows.Count
            If GlobalModule1.ds.Tables("avgrate").Rows(r)("remqtywobag") > q Then
                rt = rt + GlobalModule1.ds.Tables("avgrate").Rows(r)("rate")
                inw = inw & GlobalModule1.ds.Tables("avgrate").Rows(r)("inw_no") & ">" & q & ","
                r = r + 1
                Exit Do
            Else
                rt = rt + GlobalModule1.ds.Tables("avgrate").Rows(r)("rate")
                inw = inw & GlobalModule1.ds.Tables("avgrate").Rows(r)("inw_no") & ">" & GlobalModule1.ds.Tables("avgrate").Rows(r)("remqtywobag") & ","
            End If

            q = q - GlobalModule1.ds.Tables("avgrate").Rows(r)("remqtywobag")
            r = r + 1
        Loop
        'MsgBox(Math.Round(rt / r, 2))
        Return Math.Round(rt / r, 2)
    End Function
    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.Load
        fill()
    End Sub
End Class


