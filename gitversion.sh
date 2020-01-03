#!/bin/bash

#Expects to be run as part of github action using gitversion docker image, something like:
#docker run -it --rm -v ${PWD}:/code --workdir /code --entrypoint ./gitversion.sh gittools/gitversion

echo "::set-output name=version::$(dotnet /app/GitVersion.dll)"