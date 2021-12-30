开发中。。。🐛



### 系统组成

网关：YARP

服务注册发现：Consul

配置中心：Consul

统一认证中心：IdentityServer

定时服务：Hangfire

链路跟踪：SkyWalking

日志：ELK

消息队列：Rabbit

NoSQL：Redis

Actor：Orleans



启动调试

```
1.准备环境
// 环境
docker-compose -f docker-compose.env.yml up -d

// 将配置文件写入consul
docker exec -it dev-consul consul kv put Services/IdentityServiceConfigDev @/consul/kv/IdentityServiceConfig.dev.json

docker exec -it dev-consul consul kv put Services/IdentityServiceConfig @/consul/kv/IdentityServiceConfig.json

docker exec -it dev-consul consul kv put Services/RbacServiceConfigDev @/consul/kv/RbacServiceConfig.dev.json

docker exec -it dev-consul consul kv put Services/RbacServiceConfig @/consul/kv/RbacServiceConfig.json

2.调试或直接启动
然后可以选择本地调试启动或通过docker-compose启动

// 应用
docker-compose -f docker-compose.app.yml up -d
```



