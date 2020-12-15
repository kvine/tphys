# !/bin/bash

cd $(dirname "$0")

pwd

tail -f /var/log/Golf3dPhysicsVerify/err.log -n 100