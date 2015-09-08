虽然是写Unity客户端，但写通信逻辑初期用不到unity，还要每次启动unity才能测试的话太麻烦了。

项目初期我准备用[mono](http://www.mono-project.com/)直接执行C#代码，这样可以在命令行中启动客户端，测试起来比较方便。


### 1. 安装mono
http://www.mono-project.com/docs/getting-started/install/linux/

### 2. 编译
```
mcs -out:test.exe -recurse:'*.cs' /r:SimpleJson.dll /r:pomelo-dotnetClient.dll
```
获得test.exe

### 3. 执行
```
mono test.exe
```
