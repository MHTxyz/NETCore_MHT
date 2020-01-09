运行方式改为使用控制台运行而不是IISExpress，以便查看各种debug信息.
1.打开launchSettings.json，把IISExpress相关的内容删掉, 然后端口改为5000.【可以忽略】
2.安装Identity Server4
3.配置管道：app.UseIdentityServer();
4.配置Identity Server：ConfigureServices，注册到容器
1. 哪些API可以使用这个authorization server.
2. 那些客户端Client(应用)可以使用这个authorization server.
3. 指定可以使用authorization server授权的用户.

GET：
https://localhost:44327/.well-known/openid-configuration

POST：
https://localhost:44327/connect/token

grant_type:client_credentials
client_id:socialnetwork
client_secret:secret


uasename:mail@qq.com
password:password

证书：pw:MH
https://slproweb.com/products/Win32OpenSSL.html

OpenSSL> req -newkey rsa:2048 -nodes -keyout socialnetwork.key -x509 -days 365 -out socialnetwork.cer
OpenSSL> pkcs12 -export -in socialnetwork.cer -inkey socialnetwork.key -out socialnetwork.pfx


https://github.com/IdentityServer/IdentityServer4.Quickstart.UI/releases

Install-Package Swashbuckle.AspNetCore