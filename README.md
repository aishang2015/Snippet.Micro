Consul:http://127.0.0.1:8500

SkyWalking:http://127.0.0.1:9898

identity:http://ip:10000/identity/connect/token

test:http://ip:10000/test/api/WeatherForecast

elk:http://ip:5601





docker-compose up -d 

//  将配置文件写入consul

docker exec -it dev-consul consul kv put Services/IdentityServiceConfig @/consul/kv/IdentityServiceConfig.json

docker exec -it dev-consul consul kv put Services/RbacServiceConfig @/consul/kv/RbacServiceConfig.json

