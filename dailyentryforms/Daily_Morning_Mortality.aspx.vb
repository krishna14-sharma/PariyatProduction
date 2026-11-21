Imports System.Data.SqlClient

Partial Class dailyentryforms_Daily_Morning_Mortality
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        ' Response.Write(areaName.Value.ToString)   ' debug output removed (Response.Write breaks AJAX postbacks)
        sql = "INSERT INTO MorningMortalityEntry (areaName,Flock, mortalityMale, mortalityFemale,"
        sql &= " storeMale, storeFemale, dewormingDay,antibioticDay, Remarks ) VALUES ("
        sql &= "'" & areaName.Value.ToString & "',"
        sql &= "'" & flockno.Text & "',"
        sql &= "'" & mortalityMale.Value.ToString & "',"
        sql &= "'" & mortalityFemale.Value.ToString & "',"
        sql &= "'" & storeMale.Value.ToString & "',"
        sql &= "'" & storeFemale.Value.ToString & "',"
        sql &= "'" & dewormingDay.Value.ToString & "',"
        sql &= "'" & antibioticDay.Value.ToString & "',"
        sql &= "'" & Remarks.Value.ToString & "')"

        'qyeryresult = gmod.SqlExecuteNonQuery(sql)
        'fillgrid()

        'Response.Write(qyeryresult)

        Try
            Using conn As New SqlConnection(gmod.Connstr)
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    lblPrint.Text = "Morning Mortality Data Saved Successfully"
                    fillgrid()
                End Using
            End Using
        Catch ex As Exception

            gmod.LogErrorToFile(ex.Message.ToString, "MorningMortalityEntry", "DeletePM", Session("user_name") + "-" + Session("location_name"))
        End Try
        clearcontrols()

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            areaName.Value = Session("area_code").ToString
            fillflock()
            fillgrid()
        End If
    End Sub
    Sub fillflock()
        sql = "select distinct flock_no from [dbo].[Shed_Entry_Data] where shed_loc='" & Session("area_code").ToString & "' and isAlive = 1 and isEmpty =1"
        gmod.DataSetRet(sql, "moringmotflock")
        flockno.DataSource = gmod.ds.Tables("moringmotflock")
        flockno.DataTextField = "flock_no"
        flockno.DataBind()
    End Sub
    Sub fillgrid()
        sql = "select [AreaName], [Flock], [MortalityMale], [MortalityFemale], [StoreMale], [StoreFemale], [DewormingDay], [AntibioticDay], [Remarks],  CONVERT (varchar(10), [EntryDate], 103) [EntryDate] from MorningMortalityEntry where EntryDate='" & Now.ToShortDateString & "' and AreaName='" & Session("area_code").ToString & "'"
        gmod.DataSetRet(sql, "moringmot")
        GridView1.DataSource = gmod.ds.Tables("moringmot")
        GridView1.DataBind()
    End Sub


    Sub clearcontrols()

        mortalityMale.Value = ""
        mortalityFemale.Value = ""
        storeMale.Value = ""
        storeFemale.Value = ""
        dewormingDay.Value = ""
        antibioticDay.Value = ""
        Remarks.Value = ""
    End Sub

    Protected Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        sql = "delete from MorningMortalityEntry where EntryDate='" & Now.ToShortDateString & "' and AreaName='" & Session("area_code").ToString & "'"
        qyeryresult = gmod.SqlExecuteNonQuery(sql)


        Try
            Using conn As New SqlConnection(gmod.Connstr)
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    lblPrint.Text = "Morning Mortality Data Saved Successfully"
                    fillgrid()
                End Using
            End Using
        Catch ex As Exception
            gmod.LogErrorToFile(ex.Message.ToString, "MorningMortalityEntry", "DeletePM", Session("user_name") + "-" + Session("location_name"))
        End Try
        'fillgrid()
        clearcontrols()
        'Response.Write(qyeryresult)
    End Sub
End Class
