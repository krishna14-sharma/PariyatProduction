
Partial Class masterforms_EmpAreaUpdate
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim storeflag As Integer

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'fillOrg()
            fillLocation()
            'fillGrid()
            fillLocationto()
        Else
        End If
    End Sub

    Public Sub fillLocation()
        sql = "select distinct AreaCode location_code ,AreaName location_code from EmpMaster Union select Dept,Dept from Department"
        gmod.DataSetRet(sql, "shed_loc")
        arealist.DataSource = gmod.ds.Tables("shed_loc")
        arealist.DataTextField = "location_code"
        arealist.DataValueField = "location_code"
        arealist.DataBind()

    End Sub


    Public Sub fillLocationto()
        sql = "select distinct AreaCode location_code ,AreaName location_code from EmpMaster Union select Dept,Dept from Department"
        gmod.DataSetRet(sql, "shed_loc1")
        areato.DataSource = gmod.ds.Tables("shed_loc1")
        areato.DataTextField = "location_code"
        areato.DataValueField = "location_code"
        areato.DataBind()

    End Sub

    Protected Sub showemp_Click(sender As Object, e As EventArgs) Handles showemp.Click
        sql = "select  Empname +'#'+ EmpCode as emp, EmpCode from EmpMaster  where AreaCode = '" & arealist.SelectedValue.ToString & "' order by Empname"
        gmod.DataSetRet(sql, "areaemp")
        emplist.DataSource = gmod.ds.Tables("areaemp")
        emplist.DataTextField = "emp"
        emplist.DataValueField = "EmpCode"
        emplist.DataBind()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        sql = "update EmpMaster set AreaCode  ='" & areato.SelectedValue.ToString & "' , AreaName ='" & arealist.Text & "'   where EmpCode ='" & emplist.SelectedValue.ToString & "' and AreaCode = '" & arealist.SelectedValue.ToString & "'"
        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblstring.Text = emplist.Text & "Trasferd to " & areato.Text & "From " & arealist.Text
          
        Else
            lblstring.Text = qyeryresult
        End If

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        sql = "update EmpMaster set Status =0 where EmpCode ='" & emplist.SelectedValue.ToString & "' and AreaCode = '" & arealist.SelectedValue.ToString & "'"
        qyeryresult = gmod.SqlExecuteNonQuery(sql)

        sql = "insert into [EmpLeft] ([EmpCode],[Left_Date]) values('" & emplist.SelectedValue.ToString & "',GETDATE())"
        qyeryresult = gmod.SqlExecuteNonQuery(sql)

        If qyeryresult = "SUCCESS" Then
            lblstring.Text = emplist.Text & "Left Successfully "

        Else
            lblstring.Text = qyeryresult
        End If

    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        sql = "update EmpMaster set Status =1 where EmpCode ='" & emplist.SelectedValue.ToString & "' and AreaCode = '" & arealist.SelectedValue.ToString & "'"
        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblstring.Text = emplist.Text & "Re-Join Successfully "

        Else
            lblstring.Text = qyeryresult
        End If
    End Sub

    
    Protected Sub att_update_Click(sender As Object, e As EventArgs) Handles att_update.Click
        If chkAbsent.Checked = True Then
            sql = "update daily_attendance set absent=-1, leave=0 where empcode='" & emplist.SelectedValue.ToString & "' and date ='" & att_date.Value.ToString & "'"
        ElseIf chkLeave.Checked = True Then
            sql = "update daily_attendance set absent=0, leave=-1 where empcode='" & emplist.SelectedValue.ToString & "' and date ='" & att_date.Value.ToString & "'"
        ElseIf chkPresent.Checked = True Then
            sql = "update daily_attendance set absent=0, leave=0 where empcode='" & emplist.SelectedValue.ToString & "' and date ='" & att_date.Value.ToString & "'"

        End If
        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblstring.Text = emplist.Text & "Updaetd Successfully "
        Else
            lblstring.Text = qyeryresult
        End If
    End Sub
End Class
