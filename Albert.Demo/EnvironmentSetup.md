## �����������

#### ʹ�÷���������

ʹ��neofetch�鿴ϵͳ��Ϣ��

![Os](doc/image/os.png)

#### ��װ.NET Core

ע��΢����Կ
```
wget -q https://packages.microsoft.com/config/ubuntu/16.04/packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
```

��װ.NET SDK
```
sudo apt-get install apt-transport-https
sudo apt-get update
sudo apt-get install dotnet-sdk-2.1
```

��װ��ɲ鿴�Ƿ�װ�ɹ���

![Dotnetcoreversion](doc/image/dotnetcoreversion.png)

#### nginx�������

��װnginx��

```
apt install nginx
```

����nginx:
![Nginxconfig](doc/image/nginxconfig.png)

���¼���nginx��

```
nginx -t
nginx -s reload
```
nginx��������
```
nginx -s reload   # �������������ļ�
nginx -s reopen   # ���� Nginx
nginx -s stop     # ֹͣ Nginx
```

#### Supervisor

��װsupervisor��

```
apt install supervisor
```

��װ���֮���� /ect/supervisor/confg.d/ Ŀ¼���½�һ�������ļ���ȡ��Ϊ albertdemo.conf (��ǰ��Ŀ������)

![supervisorconfig](doc/image/catconfig.png)

�������ý��ͣ�
```
[program:TestCore]
command=dotnet TestCore.dll #Ҫִ�е�����
directory=/home/xx/TestCore #����ִ�е�Ŀ¼
environment=ASPNETCORE__ENVIRONMENT=Production #��������
user=www-data #����ִ�е��û����
stopsignal=INT
autostart=true #�Ƿ��Զ�����
autorestart=true #�Ƿ��Զ�����
startsecs=1 #�Զ��������
stderr_logfile=/var/log/TestCore.err.log #��׼������־
stdout_logfile=/var/log/TestCore.out.log #��׼�����־
```

���¼������ã�

```
service supervisor stop
service supervisor start
```