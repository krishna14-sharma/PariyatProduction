
Partial Class masterforms_Standards
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fillSeason()
            fillBirdtype()
            fillGrid()
        Else

        End If
    End Sub
    Public Sub fillSeason()
        sql = "select DISTINCT SeasonName from [dbo].[Season] "
        gmod.DataSetRet(sql, "SeasonName")
        seasonlist.DataSource = gmod.ds.Tables("SeasonName")
        seasonlist.DataTextField = "SeasonName"
        seasonlist.DataValueField = "SeasonName"
        seasonlist.DataBind()

    End Sub

    Public Sub fillBirdtype()
        sql = "select [bird_type_code], [bird_type_name] from [dbo].[bird_type_master]"
        gmod.DataSetRet(sql, "shed_loc")
        birdtype.DataSource = gmod.ds.Tables("shed_loc")
        birdtype.DataTextField = "bird_type_code"
        birdtype.DataValueField = "bird_type_code"
        birdtype.DataBind()
    End Sub

    Protected Sub btnsavestandard_Click(sender As Object, e As EventArgs) Handles btnsavestandard.Click
        sql = "insert into [Standard]([Bird_breed_code], [Age_Weeks], [Cum_dep_female_per], [Cum_dep_male_per_g], "
        sql &= "[body_wt_female_g], [body_wt_male_g], [Egg_wt_g], [female_feed_comp_per_day_grams], male_feed_comp_per_day_grams,[feed_comp_per_day_grams_cum], [Type_feed],"
        sql &= " [Mortality_per_female], [HDP_Per], [Egg_prd_hen_housed_weekly], [Egg_prd_hen_housed_cum], [Hatching_egg_per],Hatching_egg_per_housed, [Hatch_Per],Season) values("

        sql &= "'" & birdtype.Text & "',"
        sql &= "'" & ageweeks.Text & "',"
        sql &= "'" & cumdepperfemale.Text & "',"
        sql &= "'" & cumdeppermale.Text & "',"
        sql &= "'" & bodywtfemalegrams.Text & "',"
        sql &= "'" & bodywtmalegrams.Text & "',"
        sql &= "'" & eggwt.Text & "',"
        sql &= "'" & feedcompperday.Text & "',"
        sql &= "'" & malefeedcompperday.Text & "',"
        sql &= "'0',"
        sql &= "'-',"
        sql &= "'" & morperfemale.Text & "',"
        sql &= "'" & hdpper.Text & "',"

        sql &= "'" & eggprdhenhouesdweekly.Text & "',"
        sql &= "'" & eggprdhenhouescumulative.Text & "',"

        sql &= "'" & hatdhingegg.Text & "',"
        sql &= "'" & hatdhingegghenhosed.Text & "',"

        sql &= "'0',"
        sql &= "'" & seasonlist.SelectedValue & "')"

        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblprint.Text = "Data Saved Successfully"
            fillGrid()

        Else
            lblprint.Text = qyeryresult
        End If
    End Sub

    Public Sub fillGrid()
        sql = "select * from  [Standard] "
        gmod.DataSetRet(sql, "Standard")
        standard_grid.DataSource = gmod.ds.Tables("Standard")
        standard_grid.DataBind()
    End Sub
End Class
