#!/bin/bash

git add .

echo enter the commit description

read CommitDescription

git commit -m CommitDescription

git push https://gitlab.com/HedariKun/kururur.git master