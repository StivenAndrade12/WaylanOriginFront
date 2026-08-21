#!/usr/bin/env bash
set -e

echo "===> Installing .NET SDK 10..."
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 10.0 --install-dir /tmp/dotnet

echo "===> Publishing WaylanOrigin.Client..."
/tmp/dotnet/dotnet publish WaylanOrigin.Client/WaylanOrigin.Client.csproj -c Release -o output_temp

echo "===> Structuring build output..."
mkdir -p output
cp -r output_temp/wwwroot/* output/
if [ -d "output_temp/wwwroot/_framework" ]; then
    echo "===> Verified _framework folder exists in output!"
fi

echo "===> Build completed successfully!"
