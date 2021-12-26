Consul:http://127.0.0.1:8500

SkyWalking:http://127.0.0.1:9898

identity:http://ip:10000/identity/connect/token

test:http://ip:10000/test/api/WeatherForecast

elk:http://ip:5601



准备环境

// 环境

docker-compose -f docker-compose.env.yml up -d 



//  将配置文件写入consul

docker exec -it dev-consul consul kv put Services/IdentityServiceConfigDev @/consul/kv/IdentityServiceConfig.dev.json

docker exec -it dev-consul consul kv put Services/IdentityServiceConfig @/consul/kv/IdentityServiceConfig.json

docker exec -it dev-consul consul kv put Services/RbacServiceConfigDev @/consul/kv/RbacServiceConfig.dev.json

docker exec -it dev-consul consul kv put Services/RbacServiceConfig @/consul/kv/RbacServiceConfig.json



然后可以选择本地调试启动或通过docker-compose启动

// 应用

docker-compose -f docker-compose.app.yml up -d

