���з�ʽ��Ϊʹ�ÿ���̨���ж�����IISExpress���Ա�鿴����debug��Ϣ.
1.��launchSettings.json����IISExpress��ص�����ɾ��, Ȼ��˿ڸ�Ϊ5000.�����Ժ��ԡ�
2.��װIdentity Server4
3.���ùܵ���app.UseIdentityServer();
4.����Identity Server��ConfigureServices��ע�ᵽ����
1. ��ЩAPI����ʹ�����authorization server.
2. ��Щ�ͻ���Client(Ӧ��)����ʹ�����authorization server.
3. ָ������ʹ��authorization server��Ȩ���û�.

GET��
https://localhost:44327/.well-known/openid-configuration

POST��
https://localhost:44327/connect/token

grant_type:client_credentials
client_id:socialnetwork
client_secret:secret


uasename:mail@qq.com
password:password

֤�飺pw:MH
https://slproweb.com/products/Win32OpenSSL.html

OpenSSL> req -newkey rsa:2048 -nodes -keyout socialnetwork.key -x509 -days 365 -out socialnetwork.cer
OpenSSL> pkcs12 -export -in socialnetwork.cer -inkey socialnetwork.key -out socialnetwork.pfx


https://github.com/IdentityServer/IdentityServer4.Quickstart.UI/releases

Install-Package Swashbuckle.AspNetCore