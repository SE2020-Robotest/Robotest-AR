# CSharp GRPC

## 配置

使用Visual Studio进行编译, 使用其它工具可能会出现依赖问题

### 添加依赖

在依赖项中右键选择`管理NuGet程序包`

搜索并安装以下依赖

```bash
google.protobuf
grpc
grpc.tools
grpc.core
```

## 编译proto文件

+ 新建proto文件
+ 修改`msg.proto`文件的属性
  + 将**生成操作**中的**"无"**修改为**protobuf compile**
  + 若不修改则无法预编译出相应的文件

## AR端消息监听及响应服务

`services/ARServices.cs`

AR端可接收四类消息:

+ `ConfigMap` 地图配置消息
+ `RobotPath` 机器人路径消息
+ `ControlCommand` 控制端发送的命令消息
+ `VoiceResult` 机器人发送的语音识别结果

在`ARServices`类中实现以上四个消息的接收,其中request为接收到的消息, 请在这四个函数中实现你要对消息进行的操作.

### 启动服务

`services/ARServer.cs`

注意修改IP地址及端口号, 端口号默认为8888, 若需要修改端口号, 请与其它端同步

其中实现了静态方法StartServer, 但由于对CSharp不是很懂, 该函数存在一些问题, 必须使用`Console.ReadKey()`才能保持服务不会启动后立刻退出.

## AR端发送消息服务

`services/ARClient.cs`

`SendVoiceByte` 向机器人端发送语音文件

`GenVoiceDataList` 根据传入的文件名, 以二进制形式读取并返回一个voicedatalist

以上两个语音相关的函数都未经过测试, 在使用时可能会发生错误.

两个函数的工作就是以流形式打开文件, 以二进制形式读取. 在`GenVoiceDataList`中将读取的二进制数据拆分成1M的分立数据, 并且将其转为`Google.Protobuf.ByteString`格式. 然后通过`SendVoiceByte`传输到机器人端.

但目前没有进行测试, 不确定两个函数的实现是否正确.