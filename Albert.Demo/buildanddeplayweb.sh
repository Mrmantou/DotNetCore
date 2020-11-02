#!/bin/bash

cp ./Albert.Demo.Web/Data/Albert.Demo.db /home/albert/DockerVolume/albertdemo/Data/

docker build -t albertdemo .

docker run --detach --rm --publish 8080:80 -v /home/albert/DockerVolume/albertdemo/Data:/app/Data --name albertdemo albertdemo
