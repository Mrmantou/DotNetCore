#!/bin/bash

workdir=$(cd $(dirname $0); pwd)

gitCloneUrl="https://github.com/Mrmantou/DotNetCore.git"

gitUrl=${workdir}"/mygit/albert.demo"

localTargetProject=${workdir}"/mygit/albert.demo/Albert.Demo/Albert.Demo.Web"

localPublishPath="/var/www/albertdemo"

echo "start......";

printf "need update source code from github?[y/N]:";read result;
echo "get input: $result"
if [[ $result == "Y" || $result == "y" ]]
then
	echo "git update code from remote......"
	git -C $gitUrl pull origin master
fi

echo "start build code......";
dotnet build $localTargetProject

echo "stop supervisor...";
sudo service supervisor stop;

echo "delete old file system";
sudo rm $localPublishPath -rf

echo "start publish......";
sudo dotnet publish $localTargetProject -o $localPublishPath

echo "start supervisor...";
sudo service supervisor start

echo "update OK!";
