# !/bin/bash

cd $(dirname "$0")

cd ..

dotnet publish -c Release
cp -r ./SceneDatas ./bin/Release/netcoreapp2.1/publish/
echo finish!