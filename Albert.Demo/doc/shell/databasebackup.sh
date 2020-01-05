#!/bin/bash

foldername=`date +%Y%m%d`;

mkdir /home/ubuntu/backup/$foldername;

cp /var/www/albertdemo/Albert.Demo.db /home/ubuntu/backup/$foldername/Albert.Demo.db
