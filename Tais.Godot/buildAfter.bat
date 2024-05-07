set VSLANG=1033
dotnet build ../Tais.Mods.Native/Tais.Mods.Native.csproj
xcopy /e /i /y "../Tais.Mods.Native/bin/Debug/net6.0" "./.mods/native/assembly"

