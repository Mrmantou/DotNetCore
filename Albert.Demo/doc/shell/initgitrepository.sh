#!/bin/bash

workdir=$(cd $(dirname $0); pwd)

mkdir mygit

mkdir mygit/albert.demo

gitPath="./mygit/albert.demo"

gitSparsePath=${gitPath}"/.git/info/sparse-checkout"
echo ${workdir}
echo ${gitPath}
echo ${gitSparsePath}

git -C ${gitPath} init

git -C ${gitPath} remote add -f origin https://github.com/Mrmantou/DotNetCore

git -C ${gitPath} config core.sparsecheckout true

echo '/Albert.Demo/*' >> ${gitSparsePath}

git -C ${gitPath} pull origin master
