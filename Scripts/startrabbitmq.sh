#!/bin/bash

docker run -d --name rabbitmq -p 15672:15672 -p 5672:5672 masstransit/rabbitmq