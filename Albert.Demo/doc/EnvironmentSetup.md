## �����������

#### ʹ�÷���������

ʹ��neofetch�鿴ϵͳ��Ϣ��

![Os](image/os.png)

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

![Dotnetcoreversion](image/dotnetcoreversion.png)

#### nginx�������

��װnginx��

```
apt install nginx
```
��װ��ɣ���/varĿ¼������www�ļ��У���Ȩ��Ϊ��

![Nginxwww](nginxwww.png)

����nginx:

![Nginxconfig](image/nginxconfig.png)

```
server {
    listen 80;

    location / {
        proxy_pass http://localhost:6000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
    }
}
```

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


![Catconfig](image/catconfig.png)

�������ý��ͣ�
```
[program:TestCore]
command=dotnet TestCore.dll #Ҫִ�е�����
directory=/home/xx/TestCore #����ִ�е�Ŀ¼
environment=ASPNETCORE_ENVIRONMENT="Development" #��������
environment=ASPNETCORE_URLS="http://localhost:6000" #������������Ķ˿ں�
user=www-data #����ִ�е��û����
stopsignal=INT
autostart=true #�Ƿ��Զ�����
autorestart=true #�Ƿ��Զ�����
startsecs=1 #�Զ��������
stderr_logfile=/var/log/TestCore.err.log #��׼������־
stdout_logfile=/var/log/TestCore.out.log #��׼�����־
```

ע��������ӵ�������������`ASPNETCORE_ENVIRONMENT`��`ASPNETCORE_URLS`������asp.net core�л��ȡ�Ļ������������¼������ã�

```
service supervisor stop
service supervisor start
```