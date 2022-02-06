#!/usr/bin/env bash
echo "Creating hangfire db..."
mongo admin -u root -p 123456 << EOF
use hangfire 
db.createCollection("temp") 
db.createUser({user: 'hangfire', pwd: '123456', roles:[{role:'dbOwner',db:'hangfire'}]}) 
EOF
echo "Mongo users created."