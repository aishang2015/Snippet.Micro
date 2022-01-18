开发中。。。🐛

### 架构图

![image](https://github.com/aishang2015/Snippet.Micro/blob/main/img/architecture.png)

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

```bash
// 1.准备环境
// 启动环境需要的镜像
docker-compose -f docker-compose.env.yml up -d

// 将配置文件写入consul，dev是本地调试用的，不带dev的是docker运行时需要的

docker exec -it dev-consul consul kv put Services/IdentityServiceConfigDev @/consul/kv/IdentityServiceConfig.dev.json

docker exec -it dev-consul consul kv put Services/IdentityServiceConfig @/consul/kv/IdentityServiceConfig.json

docker exec -it dev-consul consul kv put Services/RbacServiceConfigDev @/consul/kv/RbacServiceConfig.dev.json

docker exec -it dev-consul consul kv put Services/RbacServiceConfig @/consul/kv/RbacServiceConfig.json

// 2.调试或直接启动
// 可以通过vs调试启动

// 或者使用docker启动
docker-compose -f docker-compose.app.yml up -d
```



服务地址

```
Consul						http://本机ip:8500/
Skywalking 		              http://本机ip:9898/
ELK      					http://本机ip:5601/
内部用网关（服务间调用）		http://本机ip:10000/
对外用网关（客户端调用）		http://本机ip:20000/
	认证中心服务地址				/identity/XXXX
	角色管理服务地址				/rbac/XXXX
	调度任务服务地址				/scheduler/XXXX
		任务调度服务面板	   http://本机ip:10000/scheduler/hangfire

数据库，redis，mongodb以及rabbitmq的地址参照 docker-compose env文件
	
```

