cd /d %~dp0
%windir%\syswow64\inetsrv\Appcmd.exe delete site "Erp.GpPlatform.Service"
%windir%\syswow64\inetsrv\Appcmd.exe delete APPPOOL "Erp.GpPlatform.Service"
set path=%cd%
%windir%\syswow64\inetsrv\Appcmd.exe add site /name:"Erp.GpPlatform.Service" /bindings:http/*:9018:,net.tcp/9019:* /physicalPath:"%path%"
%windir%\syswow64\inetsrv\Appcmd.exe add APPPOOL /name:"Erp.GpPlatform.Service" -managedRuntimeVersion:v4.0 -managedPipelineMode:Integrated
%windir%\syswow64\inetsrv\Appcmd.exe set app "Erp.GpPlatform.Service/" /enabledProtocols:http,net.tcp -applicationPool:"Erp.GpPlatform.Service"