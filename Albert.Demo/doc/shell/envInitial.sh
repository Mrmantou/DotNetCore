#!/bin/bash

workdir=$(cd $(dirname $0); pwd)

echo $workdir

echo "install nginx..."
apt install nginx

echo "install supervisor..."
apt install supervisor


echo "------finished!------"