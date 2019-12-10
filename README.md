#Keim.NetCore 常用方法及框架扩展库

**支持框架 NetCore2.0以上  GRPC客户端 Consul扩展类 IdentityServer4扩展**

### Keim NetCore 1.0.10
>作者：路人甲
>邮箱地址：zhaofeng_qjj@163.com
>安装示例：Install-Package Keim.NetCore -Version 1.0.10

KeimNetCore是一种平时工作中常用业务方法及NetCore扩展方法，同时支持NetCore2.0及以上版本。

### 已包含的NuGet内容
包名称  | 版本  |  功能
------------- | -------------|-------------
Consul  | 0.7.2.6 |  服务注册
Google.Protobuf  | 3.7.0  | 序列化
Grpc  | 2.25.0  | Grpc目前只支持GRPC客户端 服务端需要NetCore3.0服务实现
Grpc.Tools  | 2.25.0  | Proto文件支持
IdentityServer4.AccessTokenValidation  | 2.6.0  | OAuth2.0认证支持
NLog.Extensions.Logging  | 1.6.1  | 日志输出
PinYinConverterCore  | 1.0.2  | 汉字转拼音支持
RestSharp  | 106.6.9  | HTTP/1.x请求


### 常用库结构分类
**DTO**
>微服务发现集合
>MicroServiceManager【生成服务集合】

**Extend**
>框架及业务扩展

名称  | 功能
------------- | -------------
AuthenticationExtend |  IdentityServer4服务配置扩展方法
BuildServiceExtend	  | NetCore依赖注入方法，需要抽象接口及父类
EnabledCorsExtend		| NetCore启用跨域请求方法
GrpcClientExtend		| Grpc客户端通道创建
LoggingExtend			| NetCore日志扩展集合
RegisterConsulExtend	| Consul服务注册扩展方法 可注入、可生成MicroServiceManager集合
RestSharpExtend		| Http1.x 请求扩展方法
SerializerSettingExtend	| 框架序列化扩展方法，小驼峰
UseSwaggerExtend		| Swagger框架启用扩展方法 注意：NetCore3.0有所不同


**Helper**
>常用验证及加密方法

名称  | 功能
------------- | -------------
EncryHelper		|	【MD5/ABS/DES加密】
ObjectRegex		|	【对象验证数据类型功能 正则表达示】
TypeHelper		|		【类型转换方法】
EnumHelper		|		【枚举获取DescriptionAttribute特性方法】
StringHelper	|		【字符串验证方法 汉字转拼音】


**Model**
>扩展方法使用到的DTO

**MvvM**
>MvvM项目使用的	【ICommand虚类】	【INotifyPropertyChanged虚类】

**Tools**
>RequestHeaderFilter	【请求日志Filter】

### Keim.NetCore 1.0.11版本计划
>添加通用缓存支持库 【Redis MDB】
>添加RMQ消息通道
