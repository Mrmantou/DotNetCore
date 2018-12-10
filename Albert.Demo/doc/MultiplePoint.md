## ���ö�վ��


ʹ��dotnet�����mvc����

```
dotnet new mvc -o aspnetApp
```

![Dotnetmvc](image/dotnetmvc.png)

�����ɹ���������ΪaspnetApp���ļ��У������ļ��У�

![Mvcfile](image/mvcfile.png)

�޸ĳ�������˿ڣ�Ĭ��5000��5001�Ѿ�������ĳ�����ʹ�ã�ִ�����

```
vi Properties/launchSettings.json
```
�޸ģ�

![Port6000](image/port6000.png)

�����˳���

���ɡ���������ִ��ָ�
```
dotnet run
```

![Dotnetrun](image/dotnetrun.png)

���Կ����������������ɹ���`Ctrl+C`�Ƴ�����ִ��ָ�
```
dotnet publish
```

![Dotnetpublish](image/dotnetpublish.png)

���Է��ַ����ļ�·��Ϊ`/home/dotnet/aspnetApp/bin/Debug/netcoreapp2.1/publish/`

�����ļ���`/var/www/`�У�
```
cp bin/Debug/netcoreapp2.1/publish /var/www -r
mv /var/www/publish /var/www/aspnetApp
```
�鿴���ƽ����

![Aspnetappwww](image/aspnetappwww.png)

ʹ��supervisor������Ӧ�ã�������ã�

![Aspnetappsupervisorconfig](image/aspnetappsupervisorconfig.png)

��Ӧ�õļ����˿�����Ϊ5000�����nginx�������ã����ڻ�ͷ�鿴nginx�����ã��鿴nginx����Ŀ¼`/etc/nginx`��

![Etcnginx](image/etcnginx.png)

�������`nginx.conf`����Ĭ�ϵ������ļ���ʹ��cat����鿴���ݣ�
```
root@xxxxx:/etc/nginx# cat nginx.conf 
```
```
user www-data;
worker_processes auto;
pid /run/nginx.pid;

events {
	worker_connections 768;
	# multi_accept on;
}

http {

	##
	# Basic Settings
	##

	sendfile on;
	tcp_nopush on;
	tcp_nodelay on;
	keepalive_timeout 65;
	types_hash_max_size 2048;
	# server_tokens off;

	# server_names_hash_bucket_size 64;
	# server_name_in_redirect off;

	include /etc/nginx/mime.types;
	default_type application/octet-stream;

	##
	# SSL Settings
	##

	ssl_protocols TLSv1 TLSv1.1 TLSv1.2; # Dropping SSLv3, ref: POODLE
	ssl_prefer_server_ciphers on;

	##
	# Logging Settings
	##

	access_log /var/log/nginx/access.log;
	error_log /var/log/nginx/error.log;

	##
	# Gzip Settings
	##

	gzip on;
	gzip_disable "msie6";

	# gzip_vary on;
	# gzip_proxied any;
	# gzip_comp_level 6;
	# gzip_buffers 16 8k;
	# gzip_http_version 1.1;
	# gzip_types text/plain text/css application/json application/javascript text/xml application/xml application/xml+rss text/javascript;

	##
	# Virtual Host Configs
	##

	include /etc/nginx/conf.d/*.conf;
	include /etc/nginx/sites-enabled/*;
}


#mail {
#	# See sample authentication script at:
#	# http://wiki.nginx.org/ImapAuthenticateWithApachePhpScript
# 
#	# auth_http localhost/auth.php;
#	# pop3_capabilities "TOP" "USER";
#	# imap_capabilities "IMAP4rev1" "UIDPLUS";
# 
#	server {
#		listen     localhost:110;
#		protocol   pop3;
#		proxy      on;
#	}
# 
#	server {
#		listen     localhost:143;
#		protocol   imap;
#		proxy      on;
#	}
#}
```

һ����˵���������ر��������ǲ����޸�nginx.conf�ļ���������ݡ�������������п���ע�⵽��
```
include /etc/nginx/conf.d/*.conf;
```

���ѿ���������ó�����Server�ڵ��ڲ������Ի��ǶԱ�վ�����������õġ����conf.dĿ¼��ŵ��Ƕ�������Server�ڵ�ͨ���ԵĹ��ܣ�����ڵ������������������дһЩ�ظ��Ե��������ݡ����ǳ�ȡ�������ŵ�һ��ͨ�õ�Ŀ¼�¡�

����ΪaspnetApp���nginx��server���ã�

![Aspnetappnginx](image/aspnetappnginx.png)

�������õ��ⲿ�����˿�Ϊ`8081`���������supervisor�����õ��ڲ������˿�Ϊ5000���������ö�Ӧ��

���¼���nginx��

```
nginx -s reload
```

ͨ�����������ͨ��IP��8081�˿ڷ��ʣ�

![Aspnetapp](image/aspnetapp.png)

���ʳɹ�����ͬʱԭ�е���վҲ�ܹ����ʳɹ����������˼·���Խ�ԭ��Ӧ�õ�nginx��default�г�ȡ����������Ϊ�˷��ʼ�ֱ��ͨ��80�˿�Ĭ�Ϸ��ʲ�����ȡ��