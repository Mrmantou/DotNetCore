## ���ö�վ��


ʹ��dotnet�����mvc����

```
dotnet new mvc -o aspnetApp
```

![Dotnetmvc](doc/image/dotnetmvc.png)

�����ɹ���������ΪaspnetApp���ļ��У������ļ��У�

![Mvcfile](doc/image/mvcfile.png)

�޸ĳ�������˿ڣ�Ĭ��5000��5001�Ѿ�������ĳ�����ʹ�ã�ִ�����

```
vi Properties/launchSettings.json
```
�޸ģ�

![Port6000](doc/image/port6000.png)

�����˳���

���ɡ���������ִ��ָ�
```
dotnet run
```

![Dotnetrun](doc/image/dotnetrun.png)

���Կ����������������ɹ���`Ctrl+C`�Ƴ�����ִ��ָ�
```
dotnet publish
```

![Dotnetpublish](doc/image/dotnetpublish.png)

���Է��ַ����ļ�·��Ϊ`/home/dotnet/aspnetApp/bin/Debug/netcoreapp2.1/publish/`

�����ļ���`/var/www/`�У�
```
cp bin/Debug/netcoreapp2.1/publish /var/www -r
mv /var/www/publish /var/www/aspnetApp
```
�鿴���ƽ����

![Aspnetappwww](doc/image/aspnetappwww.png)

