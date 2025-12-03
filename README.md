# Bangumi API 客户端

这是一个基于 .NET 8.0 的 Bangumi API 客户端库，用于访问 [Bangumi 番组计划](https://bgm.tv/) 的 API 接口。

## 项目概述

该项目是使用 Microsoft Kiota 工具根据 Bangumi API 规范自动生成的客户端库。它提供了对 Bangumi 平台数据（如动画、书籍、游戏、音乐、三次元作品以及相关的人物、角色等信息）的访问能力。

## 项目结构

```
BangumiApi/
├── Calendar/           # 时间表相关接口
├── Models/             # 数据模型类（约90个）
├── V0/                 # API v0 版本接口实现
│   ├── Characters/     # 角色相关接口
│   ├── Episodes/       # 剧集相关接口
│   ├── Indices/        # 索引相关接口
│   ├── Me/             # 当前用户相关接口
│   ├── Persons/        # 人物相关接口
│   ├── Revisions/      # 版本修订相关接口
│   ├── Search/         # 搜索相关接口
│   ├── Subjects/       # 条目相关接口
│   └── Users/          # 用户相关接口
├── ApiClient.cs              # API 客户端主入口
├── ClientBuilder.cs          # 客户端构建器
├── BangumiAuthenticationProvider.cs     # 认证提供程序
└── BangumiAuthenticationTokenAccess.cs # Token 访问处理
```

## 主要功能模块

### 1. 数据模型 (Models)
包含约90个数据模型类，对应 Bangumi API 返回的各种数据结构：
- 条目相关：[Subject](./BangumiApi/Models/Subject.cs)、[SlimSubject](./BangumiApi/Models/SlimSubject.cs)、[Subjects](./BangumiApi/Models/Subjects.cs) 等
- 人物相关：[Person](./BangumiApi/Models/Person.cs)、[PersonDetail](./BangumiApi/Models/PersonDetail.cs) 等
- 角色相关：[Character](./BangumiApi/Models/Character.cs)、[CharacterPerson](./BangumiApi/Models/CharacterPerson.cs) 等
- 剧集相关：[Episode](./BangumiApi/Models/Episode.cs)、[EpisodeDetail](./BangumiApi/Models/EpisodeDetail.cs) 等
- 用户相关：[User](./BangumiApi/Models/User.cs)、[UserSubjectCollection](./BangumiApi/Models/UserSubjectCollection.cs) 等

### 2. API 接口 (V0)
按照 Bangumi API v0 版本组织的接口调用类：
- [Characters](./BangumiApi/V0/Characters/) - 角色相关操作
- [Episodes](./BangumiApi/V0/Episodes/) - 剧集相关操作
- [Indices](./BangumiApi/V0/Indices/) - 索引相关操作
- [Me](./BangumiApi/V0/Me/) - 当前用户相关操作
- [Persons](./BangumiApi/V0/Persons/) - 人物相关操作
- [Revisions](./BangumiApi/V0/Revisions/) - 版本修订相关操作
- [Search](./BangumiApi/V0/Search/) - 搜索相关操作
- [Subjects](./BangumiApi/V0/Subjects/) - 条目相关操作
- [Users](./BangumiApi/V0/Users/) - 用户相关操作

### 3. 核心组件
- [ApiClient.cs](./BangumiApi/ApiClient.cs) - API 客户端主入口类
- [ClientBuilder.cs](./BangumiApi/ClientBuilder.cs) - 提供创建客户端实例的便捷方法
- [BangumiAuthenticationProvider.cs](./BangumiApi/BangumiAuthenticationProvider.cs) - 实现 Bangumi API 认证机制

## 使用示例

```csharp
// 创建公共客户端（无需认证）
var client = ClientBuilder.CreatePublicClient();

// 获取条目信息
var subject = await client.V0.Subjects["123"].GetAsync();

// 创建认证客户端
var authProvider = new BangumiAuthenticationProvider("YourApp/1.0", "your-access-token");
var authenticatedClient = ClientBuilder.CreateAuthenticatedClient(authProvider, "your-access-token");

// 获取当前用户信息
var userInfo = await authenticatedClient.V0.Me.GetAsync();
```

## 依赖项

- [.NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Microsoft.Kiota.Bundle](https://www.nuget.org/packages/Microsoft.Kiota.Bundle/) 1.21.0

## 构建与打包

该项目配置为可打包的 NuGet 库，包名为 `BangumiApi.Client`。

```bash
dotnet pack
```

## 许可证

本项目采用 MIT 许可证。