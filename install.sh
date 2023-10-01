#!/bin/bash


if [[ $EUID -ne 0 ]]; then
   echo "Run this script as root!"
   exit 1
fi


EXECUTABLE_PATH="./bin/Debug/net7.0/trantr"
DLL_PATH="./bin/Debug/net7.0/trantr.dll"
CONFIG_PATH="./bin/Debug/net7.0/trantr.runtimeconfig.json"
DEPS_PATH="./bin/Debug/net7.0/trantr.deps.json"


if [ ! -f "$EXECUTABLE_PATH" ] || [ ! -f "$DLL_PATH" ] || [ ! -f "$CONFIG_PATH" ] || [ ! -f "$DEPS_PATH" ]; then
    echo "One or more required files not found."
    exit 1
fi


cp "$EXECUTABLE_PATH" "$DLL_PATH" "$CONFIG_PATH" "$DEPS_PATH" /usr/local/bin/


chmod +x /usr/local/bin/trantr

echo "Done"
