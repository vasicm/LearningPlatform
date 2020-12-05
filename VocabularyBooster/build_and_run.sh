#!/bin/bash

sudo docker build . -t vocabulary-booster

sudo docker run -d -p 8090:80 vocabulary-booster