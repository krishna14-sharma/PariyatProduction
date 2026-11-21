
Partial Class dailyentryforms_Grading_Entry
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim birdType As String

    Protected Sub btngradingsave_Click(sender As Object, e As EventArgs) Handles btngradingsave.Click
        sql = "insert into [dbo].[Grading_Data]([Grading_Date], [Grading_Week], [Grading_Number], [Birds_Age], [Flock_No], [Grading_Weight], [Std_Weight], [Qty]) values("
        sql &= "'" & entrydate.Value & "',"
        sql &= "'" & gradingweek.Text & "',"
        sql &= "'" & gradingon.Text & "',"
        sql &= "'" & lblbirdbreed.Text & "',"
        sql &= "'" & lblflockno.Text & "',"
        sql &= "'" & gradingwt.Text & "',"
        sql &= "'" & stdbodywt.Text & "',"
        sql &= "'" & gradingqtybirds.Text & "')"

        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            'lblprint.Text = "Data Saved Successfully"
            fillGrid()
        Else
            'lblprint.Text = qyeryresult
        End If
        'ClearControls()
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            fill_flocks()
            rdFeMale.Checked = False
            rdFeMale.Checked = False
            fillGrid()
        Else

        End If
    End Sub

    Sub fill_flocks()
        sql = "select distinct flock_no from Shed_Entry_Data where shed_loc='" & Session("area_code") & "' and isAlive=1 and isEmpty=1"
        gmod.DataSetRet(sql, "fill_flocks")
        lblflockno.DataSource = gmod.ds.Tables("fill_flocks")
        lblflockno.DataTextField = "flock_no"
        lblflockno.DataBind()
    End Sub
    Sub fillGrid()
        sql = "select * from [dbo].[Grading_Data]"
        gmod.DataSetRet(sql, "Grading_Data_")

        gradingdata.DataSource = gmod.ds.Tables("Grading_Data_")
        gradingdata.DataBind()

    End Sub

    Protected Sub showAge_Click(sender As Object, e As EventArgs) Handles showAge.Click
        Dim month As Integer
        Dim season As String
        sql = "	select DATEDIFF(d,hatch_date,GETDATE())/7 Weeks , (DATEDIFF(d,hatch_date,GETDATE()) % 7)+1  Days , month(hatch_date)  smonth  from flock_master  where flock_no='" & lblflockno.Text & "'"
        gmod.DataSetRet(sql, "shed_data_bird_age")
        lblbirdbreed.Text = gmod.ds.Tables("shed_data_bird_age").Rows(0)("Weeks").ToString + "-" + gmod.ds.Tables("shed_data_bird_age").Rows(0)("Days").ToString

        month = CInt(gmod.ds.Tables("shed_data_bird_age").Rows(0)("smonth"))
        Select Case month
            Case 1
                season = "WINTER"
            Case 2
                season = "SUMMER"
            Case 3
                season = "SUMMER"
            Case 4
                season = "SUMMER"
            Case 5
                season = "SUMMER"
            Case 6
                season = "SUMMER"
            Case 7
                season = "SUMMER"
            Case 8
                season = "WINTER"
            Case 9
                season = "WINTER"
            Case 10
                season = "WINTER"
            Case 11
                season = "WINTER"
            Case 12
                season = "WINTER"
        End Select

        sql = "select body_wt_female_g from [dbo].[Standard] where Age_Weeks = " & gmod.ds.Tables("shed_data_bird_age").Rows(0)("Weeks").ToString & " and Season='" & season & "' and Bird_breed_code='COBB430'"
        gmod.DataSetRet(sql, "body_wt_female_g")
        stdbodywt.Text = gmod.ds.Tables("body_wt_female_g").Rows(0)(0)
    End Sub

    Protected Sub rdFeMale_CheckedChanged(sender As Object, e As EventArgs) Handles rdFeMale.CheckedChanged
        If rdFeMale.Checked = True Then
            rdMale.Checked = False
            birdType = "FeMale"
        End If
    End Sub

    Protected Sub rdMale_CheckedChanged(sender As Object, e As EventArgs) Handles rdMale.CheckedChanged
        If rdMale.Checked = True Then
            rdFeMale.Checked = False
            birdType = "Male"
        End If
    End Sub
End Class
