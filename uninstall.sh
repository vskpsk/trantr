#!/bin/bash

if [[ $EUID -ne 0 ]]; then
   echo "Run this script as root!"
   exit 1
fi

EXECUTABLE_PATH="/usr/local/bin/trantr"
DLL_PATH="/usr/local/bin/trantr.dll"
CONFIG_PATH="/usr/local/bin/trantr.runtimeconfig.json"
DEPS_PATH="/usr/local/bin/trantr.deps.json"

rm -f "$EXECUTABLE_PATH" "$DLL_PATH" "$CONFIG_PATH" "$DEPS_PATH"

echo "Uninstallation completed"
