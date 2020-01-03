#!/bin/bash

#Expects to be run as part of github action using gitversion docker image, something like:
#docker run -it --rm -v ${PWD}:/code --workdir /code --entrypoint ./gitversion.sh gittools/gitversion

dotnet /app/GitVersion.dll > ./.gitversion.json
#echo "::set-output name=short-sha::$(cat ./.gitversion.json | jq .ShortSha)"
#echo "::set-output name=full-sem-ver::$(cat ./.gitversion.json | jq .FullSemVer)"

