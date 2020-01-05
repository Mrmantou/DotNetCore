#!/bin/bash

workdir=$(cd $(dirname $0); pwd)
nginx="/etc/nginx/sites-available/"
nginxConf="/etc/nginx/conf.d/"
supervisorConf="/etc/supervisor/conf.d/"

echo $workdir

configFilePath=${workdir}"/mygit/albert.demo/Albert.Demo/doc/configs/"

echo "install nginx..."
apt install nginx

echo "install supervisor..."
apt install supervisor

echo "copy config files..."
cp ${configFilePath}"albert.demo.conf" ${supervisorConf}"albert.demo.conf"
cp ${configFilePath}"albertDemo.conf" ${nginxConf}"albertDemo.conf"
cp ${configFilePath}"default" ${nginx}"default"

echo "reload nginx..."
nginx -t
nginx -s reload

echo "restart supervisor..."
service supervisor stop
service supervisor start

echo "------finished!------"