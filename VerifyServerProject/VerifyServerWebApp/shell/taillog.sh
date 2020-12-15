# !/bin/bash

cd $(dirname "$0")

pwd

tail -f /var/log/Golf3dPhysicsVerify/out.log -n 100