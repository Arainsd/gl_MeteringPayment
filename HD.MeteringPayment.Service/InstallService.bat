cd /d %~dp0
%windir%\syswow64\inetsrv\Appcmd.exe delete site "HD.MeteringPayment.Service"
%windir%\syswow64\inetsrv\Appcmd.exe delete APPPOOL "HD.MeteringPayment.Service"
set path=%cd%
%windir%\syswow64\inetsrv\Appcmd.exe add site /name:"HD.MeteringPayment.Service" /bindings:http/*:9095:,net.tcp/9094:* /physicalPath:"%path%"
%windir%\syswow64\inetsrv\Appcmd.exe add APPPOOL /name:"HD.MeteringPayment.Service" -managedRuntimeVersion:v4.0 -managedPipelineMode:Integrated
%windir%\syswow64\inetsrv\Appcmd.exe set app "HD.MeteringPayment.Service/" /enabledProtocols:http,net.tcp -applicationPool:"HD.MeteringPayment.Service"