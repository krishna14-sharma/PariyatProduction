<%@ Page Language="VB" AutoEventWireup="true" %>

<script runat="server">
    ' Friendly error page (Web.config customErrors). Details are written to Logs/ErrorLog.txt by Global.asax.
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        Response.StatusCode = 500
        Response.TrySkipIisCustomErrors = True
    End Sub
</script>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Something went wrong | Phoenix Poultry</title>
    <style>
        body { margin: 0; min-height: 100vh; display: flex; align-items: center; justify-content: center; padding: 24px; box-sizing: border-box;
               font-family: "Segoe UI", system-ui, -apple-system, Roboto, Arial, sans-serif; background: #f3f5f8; color: #1c2733; }
        .card { background: #fff; border: 1px solid #dfe4ea; border-radius: 10px; padding: 32px; max-width: 460px; width: 100%;
                box-shadow: 0 1px 3px rgba(16, 24, 40, .08); }
        img { height: 26px; margin-bottom: 26px; }
        h1 { font-size: 1.4rem; font-weight: 600; margin: 0 0 8px; }
        p { color: #5b6878; line-height: 1.5; margin: 0 0 22px; }
        a.btn { display: inline-block; text-decoration: none; font-weight: 600; border-radius: 8px; padding: 10px 16px; margin-right: 8px; font-size: .92rem; }
        a.primary { background: #2a6f9e; color: #fff; }
        a.secondary { border: 1px solid #dfe4ea; color: #1f5679; }
    </style>
</head>
<body>
    <div class="card">
        <img src="<%= ResolveUrl("~/images/phoenix_logo.jpg") %>" alt="Phoenix" />
        <h1>Something went wrong</h1>
        <p>The page could not be completed. The problem has been recorded. Please go back and try again; if it keeps happening, contact the system administrator.</p>
        <a class="btn primary" href="javascript:history.back()">Go back</a>
        <a class="btn secondary" href="<%= ResolveUrl("~/loginforms/index.html") %>">Sign in page</a>
    </div>
</body>
</html>
