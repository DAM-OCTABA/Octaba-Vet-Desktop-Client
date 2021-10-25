Imports Flurl
Imports Flurl.Http
Imports System.IdentityModel.Tokens.Jwt

Public Class Form1
    Private Property DeviceCode As String
    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button1.Visible = False
        Dim req = Await "https://octaba.eu.auth0.com" _
            .AppendPathSegments(New String() {"oauth", "device", "code"}) _
            .PostUrlEncodedAsync(New With {
                .client_id = "c03PqrkcMVcBud4cPMjHAT4qJpywtzje",
                .scope = "openid profile"
            })
        Dim res = Await req.GetJsonAsync(Of DeviceResponse)()
        Dim psi = New ProcessStartInfo With {
            .FileName = res.verification_uri_complete,
            .UseShellExecute = True
            }
        Process.Start(psi)
        DeviceCode = res.device_code
        Timer1.Interval = res.interval * 1000
        Timer1.Start()
        Label1.Visible = True
    End Sub

    Private Async Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim req = Await "https://octaba.eu.auth0.com" _
            .AppendPathSegments(New String() {"oauth", "token"}) _
            .AllowAnyHttpStatus() _
            .PostUrlEncodedAsync(New With {
                .grant_type = "urn:ietf:params:oauth:grant-type:device_code",
                .device_code = DeviceCode,
                .client_id = "c03PqrkcMVcBud4cPMjHAT4qJpywtzje"
            })
        If req.StatusCode <= 299 Then
            Dim res = Await req.GetJsonAsync(Of TokenResponse)()
            Dim token = New JwtSecurityToken(jwtEncodedString:=res.id_token)
            Label1.Visible = False
            Label2.Text = $"Benvingut, {token.Claims.FirstOrDefault(Function(c) c.Type = JwtRegisteredClaimNames.Name)?.Value}"
            Label2.Visible = True
        End If
    End Sub

    Class DeviceResponse
        Public Property device_code As String
        Public Property user_code As String
        Public Property verification_uri As String
        Public Property verification_uri_complete As String
        Public Property expires_in As Integer
        Public Property interval As Integer

    End Class

    Class TokenResponse
        Public Property access_token As String
        Public Property id_token As String
        Public Property refresh_token As String
        Public Property token_type As String
        Public Property expires_in As Integer
    End Class
End Class
