#!/bin/bash
rm -rf bin/Release/net5.0/*.zip 
dotnet publish -c Release -r win10-x64
dotnet publish -c Release -r osx.10.11-x64
dotnet publish -c Release -r linux-x64
cd bin/Release/net5.0/
zip -r win10-x64.zip win10-x64
zip -r osx.10.11-x64.zip osx.10.11-x64 
zip -r linux-x64.zip linux-x64 
