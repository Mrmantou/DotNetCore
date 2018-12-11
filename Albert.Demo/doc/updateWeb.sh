#!/bin/bash

echo "param check...";

if [ $# != 2 ]
then
	echo "param num error";
	echo "cmd format: ./updateWeb.sh targetWeb source";
	echo "-- targetWeb: the file folder name of target web to update";
	echo "-- source: new source file folder name";
	exit 0;
fi

if [[ ! -e "/var/www/$1" || ! -d "/var/www/$1" ]]
then
	printf "old file folder not exist, do want to continue?[y/N]:";read result;
	echo "get input: $result"
	if [[ $result != "Y" && $result != "y" ]]
	then
		exit 0;
	fi
fi

if [ ! -e "./$2" ]
then
        echo "source file folder not exist";
	exit 0;
fi

if [ ! -d "./$2" ]
then
        echo "source file not folder";
        exit 0;
fi

echo "param check OK!";

echo "execute web $1 update...";

echo "stop supervisor...";
service supervisor stop;

echo "delete old file system";
rm /var/www/$1 -rf

echo "copy new files...";
mv $2 /var/www/$1

echo "start supervisor...";
service supervisor start

echo "update OK!";

