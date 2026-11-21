
Partial Class masterforms_Standards_Table
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim tblName As String

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If rdSummer.Checked = True Then
            For Each item As DataGridItem In DataGrid1.Items
                ' Response.Write(CType(item.FindControl("Text1"), TextBox).Text)
                sql = "update [Standard_Summer] set " & Combo3.SelectedValue & " = '" & CType(item.FindControl("Text1"), TextBox).Text & "' where  Bird_breed_code='" & Combo2.Text & "' and Age_Weeks ='" & item.Cells(0).Text & "'"
                qyeryresult = gmod.SqlExecuteNonQuery(sql)
            Next

            qyeryresult = gmod.SqlExecuteNonQuery(sql)
            If qyeryresult = "SUCCESS" Then
                lblprint.Text = "Summer Data Saved Successfully"
                'fillGrid()

            Else
                lblprint.Text = qyeryresult
            End If

        ElseIf rdWinter.Checked = True Then


            For Each item As DataGridItem In DataGrid1.Items
                ' Response.Write(CType(item.FindControl("Text1"), TextBox).Text)
                sql = "update [Standard_Winter] set " & Combo3.SelectedValue & " = '" & CType(item.FindControl("Text1"), TextBox).Text & "' where  Bird_breed_code='" & Combo2.Text & "' and Age_Weeks ='" & item.Cells(0).Text & "'"
                qyeryresult = gmod.SqlExecuteNonQuery(sql)
            Next

            qyeryresult = gmod.SqlExecuteNonQuery(sql)
            If qyeryresult = "SUCCESS" Then
                lblprint.Text = "Winter Data Saved Successfully"
                'fillGrid()

            Else
                lblprint.Text = qyeryresult
            End If

        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            rdSummer.Checked = True
            'fillSeason()
            fillBirdtype()
            fillColoumns()
            Combo3_SelectedIndexChanged(sender, e)
            'fillGrid()
            'fillAge()

        Else

        End If
    End Sub

    'Public Sub fillSeason()
    '    sql = "select DISTINCT SeasonName from [dbo].[Season] "
    '    gmod.DataSetRet(sql, "SeasonName")
    '    Combo1.DataSource = gmod.ds.Tables("SeasonName")
    '    Combo1.DataTextField = "SeasonName"
    '    Combo1.DataValueField = "SeasonName"
    '    Combo1.DataBind()

    'End Sub

    Public Sub fillBirdtype()
        sql = "select [bird_type_code], [bird_type_name] from [dbo].[bird_type_master]"
        gmod.DataSetRet(sql, "bird_type_code")
        Combo2.DataSource = gmod.ds.Tables("bird_type_code")
        Combo2.DataTextField = "bird_type_code"
        Combo2.DataValueField = "bird_type_code"
        Combo2.DataBind()
    End Sub

    Public Sub fillColoumns()
        sql = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'Standard_Summer' and  COLUMN_NAME not in ('Bird_breed_code','Age_Weeks','Season','id')"
        gmod.DataSetRet(sql, "COLUMN_NAME")
        Combo3.DataSource = gmod.ds.Tables("COLUMN_NAME")
        Combo3.DataTextField = "COLUMN_NAME"
        Combo3.DataValueField = "COLUMN_NAME"
        Combo3.DataBind()
    End Sub

    Public Sub fillAge()
        Dim i As Integer

        For i = 81 To 100
            sql = "insert into [dbo].[Standard_Summer]([Age_Weeks],[Bird_breed_code],[Season]) values( "
            sql &= "'" & i & "',"
            sql &= "'BV300',"
            sql &= "'-')"
            gmod.SqlExecuteNonQuery(sql)
        Next
    End Sub

    Protected Sub Combo3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Combo3.SelectedIndexChanged
        If rdSummer.Checked = True Then
            sql = "select Age_Weeks,isnull(" & Combo3.SelectedValue & ",0) value from  [dbo].[Standard_Summer] where Bird_breed_code='" & Combo2.Text & "' order by Age_Weeks"
            gmod.DataSetRet(sql, "fillgridData")
            DataGrid1.DataSource = gmod.ds.Tables("fillgridData")
            DataGrid1.DataBind()
            lblprint.Text = ""
        Else
            sql = "select Age_Weeks,isnull(" & Combo3.SelectedValue & ",0) value from  [dbo].[Standard_Winter] where Bird_breed_code='" & Combo2.Text & "' order by Age_Weeks "
            gmod.DataSetRet(sql, "fillgridData")
            DataGrid1.DataSource = gmod.ds.Tables("fillgridData")
            DataGrid1.DataBind()
            lblprint.Text = ""
        End If
    End Sub

    Protected Sub rdSummer_CheckedChanged(sender As Object, e As EventArgs) Handles rdSummer.CheckedChanged
        If rdSummer.Checked = True Then
            tblName = "[Standard_Summer]"
            rdWinter.Checked = False
        Else
            tblName = "[Standard_Winter]"
        End If
    End Sub


    Protected Sub rdWinter_CheckedChanged(sender As Object, e As EventArgs) Handles rdWinter.CheckedChanged
        If rdWinter.Checked = True Then
            tblName = "[Standard_Summer]"
            rdSummer.Checked = False
        Else
            tblName = "[Standard_Winter]"
        End If
    End Sub
End Class
